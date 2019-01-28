using System;
using System.Collections.Generic;
using Xunit;
using Miunie.Core;
using Miunie.Core.Services;
using Miunie.Core.Discord;

namespace Miunie.Core.XUnit.Tests
{
    public class ListDirectoryTest
    {
        private readonly ListDirectoryService _ls;
        private const ulong TestServerId = 123456789;
        private const string TestServerName = "TestServer";

        public ListDirectoryTest()
        {
            var serversMock = new DiscordServersMock(
                TestServerId,
                TestServerName,
                new []{ "ChannelA", "ChannelB", "ChannelC" }
            );

            _ls = new ListDirectoryService(serversMock);
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

