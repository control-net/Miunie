using Miunie.Core.Configuration;
using Miunie.Discord;
using Moq;
using System.Threading;
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
            
            var miunie = new MiunieDiscord(discord.Object, null, null, null);

            var ex = await Record.ExceptionAsync(async () => await miunie.RunAsync(new CancellationToken()));

            Assert.NotNull(ex);
        }
    }
}
