using Miunie.Core.Configuration;
using Miunie.Discord;
using System;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieDiscordClientTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Initialize_ShouldThrowArgumentNullException_IfInvalidToken(string token)
        {
            IBotConfiguration config = new BotConfiguration
            {
                DiscordToken = token,
                CommandsEnabled = true
            };
            var client = new MiunieDiscordClient(config);

            Assert.ThrowsAsync<ArgumentNullException>(client.InitializeAsync);
        }
    }
}
