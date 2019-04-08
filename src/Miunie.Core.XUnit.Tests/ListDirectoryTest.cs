using System.Collections.Generic;
using Xunit;
using Moq;
using Miunie.Core.Providers;
using Miunie.Core.Discord;
using System.Linq;
using System.Threading.Tasks;

namespace Miunie.Core.XUnit.Tests
{
    public class ListDirectoryTest
    {
        private readonly IListDirectoryProvider _ls;
        private const ulong TestServerId = 123456789;
        private const string TestServerName = "TestServer";

        public ListDirectoryTest()
        {
            var serversMock = new Mock<IDiscordServers>();
            serversMock
                .Setup(s => s.GetChannelNamesFromServer(TestServerId))
                .Returns(Task.FromResult(new[] { "ChannelA", "ChannelB", "ChannelC" }));

            serversMock
                .Setup(s => s.GetServerNameById(TestServerId))
                .Returns(Task.FromResult(TestServerName));

            _ls = new ListDirectoryProvider(serversMock.Object);
        }

        [Fact]
        public async Task EmptyShouldOutputRoot()
        {
            const string expectedData = "Data";

            var inputUser = new MiunieUser
            {
                GuildId = TestServerId
            };
            var actual = await _ls.Of(inputUser);

            Assert.Equal(expectedData, actual.Result.First());
            Assert.Equal(TestServerName, actual.Result.ElementAt(1));
        }

        [Fact]
        public async Task InServerShouldOutputChannels()
        {
            var inputUser = new MiunieUser
            {
                GuildId = TestServerId,
                NavCursor = new List<ulong> { TestServerId }
            };
            var actual = await _ls.Of(inputUser);
            Assert.Equal("ChannelA", actual.Result.ElementAt(0));
            Assert.Equal("ChannelB", actual.Result.ElementAt(1));
            Assert.Equal("ChannelC", actual.Result.ElementAt(2));
        }
    }
}
