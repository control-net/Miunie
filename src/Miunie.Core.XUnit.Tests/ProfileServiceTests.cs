using Miunie.Core.Logging;
using Miunie.Core.Providers;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class ProfileServiceTests
    {
        private readonly Mock<IDiscordMessages> msgMock;
        private readonly ProfileService profileService;

        public ProfileServiceTests()
        {
            msgMock = new Mock<IDiscordMessages>();
            profileService = new ProfileService(msgMock.Object, null, null);
        }

        [Fact]
        public async Task ShowProfileAsync_ShouldExecuteOnce()
        {
            msgMock.Setup(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieUser>()))
                .Returns(Task.CompletedTask);
            
            await profileService.ShowProfileAsync(null, null);
            
            msgMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieUser>()), Times.Once);
        }

        [Fact]
        public async Task ShowGuildProfileAsync_ShouldExecuteOnce()
        {
            msgMock.Setup(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieGuild>()))
                .Returns(Task.CompletedTask);

            await profileService.ShowGuildProfileAsync(null, null);
            
            msgMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieGuild>()), Times.Once);
        }
    }
}
