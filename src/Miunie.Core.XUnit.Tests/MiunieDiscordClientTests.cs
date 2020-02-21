using Miunie.Core.Configuration;
using Miunie.Discord;
using Moq;
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
            var config = Mock.Of<IBotConfiguration>(c => c.DiscordToken == token);
            var client = new MiunieDiscordClient(config);

            Assert.ThrowsAsync<ArgumentNullException>(client.InitializeAsync);
        }
    }
}
