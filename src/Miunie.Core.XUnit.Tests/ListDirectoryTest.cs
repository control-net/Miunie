using System;
using Xunit;
using Miunie.Core;
using Miunie.Core.Services;
using Miunie.Core.Discord;

namespace Miunie.Core.XUnit.Tests
{
    public class ListDirectoryTest
    {
        private readonly ListDirectoryService _ls;

        public ListDirectoryTest()
        {
            var serversMock = new DiscordServersMock(
                123456789,
                "TestServer"
            );

            _ls = new ListDirectoryService(serversMock);
        }

        [Fact]
        public void EmptyShouldOutputRoot()
        {
            const string expected = "Data\nTestServer";
            var inputUser = new MiunieUser();
            var inputChannel = new MiunieChannel
            {
                GuildId = 123456789
            };
            var actual = _ls.Of(inputUser, inputChannel);
            Assert.Equal(expected, actual);
        }
    }
}

