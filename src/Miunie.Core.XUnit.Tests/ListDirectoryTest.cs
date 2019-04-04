using System.Collections.Generic;
using Xunit;
using Moq;
using Miunie.Core.Providers;
using Miunie.Core.Discord;

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
                .Returns(new[] { "ChannelA", "ChannelB", "ChannelC" });

            serversMock
                .Setup(s => s.GetServerNameById(TestServerId))
                .Returns(TestServerName);

            _ls = new ListDirectoryProvider(serversMock.Object);
        }

        [Fact]
        public void EmptyShouldOutputRoot()
        {
            string expected = $"Data\n{TestServerName}";
            var inputUser = new MiunieUser
            {
                GuildId = TestServerId
            };
            var actual = _ls.Of(inputUser);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InServerShouldOutputChannels()
        {
            const string expected = "ChannelA\nChannelB\nChannelC";
            var inputUser = new MiunieUser
            {
                GuildId = TestServerId,
                NavCursor = new List<ulong> { TestServerId }
            };
            var actual = _ls.Of(inputUser);
            Assert.Equal(expected, actual);
        }
    }
}

