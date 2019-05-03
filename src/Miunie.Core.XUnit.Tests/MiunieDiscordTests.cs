using Microsoft.Extensions.DependencyInjection;
using Miunie.Core.Configuration;
using Miunie.Discord;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieDiscordTests
    {
        [Fact]
        public async Task RunAsync_ShouldThrowException_IfInvalidToken()
        {
            var config = Mock.Of<IBotConfiguration>(c => c.DiscordToken == "ObviouslyFakeToken");
            var discord = new Mock<MiunieDiscordClient>(config);
            var miunie = ActivatorUtilities.CreateInstance<MiunieDiscord>(ConsoleApp.InversionOfControl.Provider, discord.Object);

            var ex = await Record.ExceptionAsync(async () => await miunie.RunAsync(new System.Threading.CancellationToken()));

            Assert.NotNull(ex);
        }
    }
}
