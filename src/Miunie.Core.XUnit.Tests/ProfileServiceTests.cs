using Miunie.Core.Logging;
using Miunie.Core.Providers;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class ProfileServiceTests
    {
        private readonly Mock<IDiscordMessages> _msgMock;
        private readonly ProfileService _profileService;

        public ProfileServiceTests()
        {
            _msgMock = new Mock<IDiscordMessages>();
            _profileService = new ProfileService(_msgMock.Object, null, null);
        }

        [Fact]
        public async Task ShowProfileAsync_ShouldExecuteOnce()
        {
            _msgMock.Setup(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieUser>()))
                .Returns(Task.CompletedTask);
            
            await _profileService.ShowProfileAsync(null, null);
            
            _msgMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieUser>()), Times.Once);
        }

        [Fact]
        public async Task ShowGuildProfileAsync_ShouldExecuteOnce()
        {
            _msgMock.Setup(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieGuild>()))
                .Returns(Task.CompletedTask);

            await _profileService.ShowGuildProfileAsync(null, null);
            
            _msgMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieGuild>()), Times.Once);
        }
    }
}
