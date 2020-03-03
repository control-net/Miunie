using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Services
{
    public class YesNoServiceTests
    {
        [Fact]
        public async Task YesNo_ShouldDelegateProperly()
        {
            var messagesMock = new Mock<IDiscordMessages>();
            var service = new MiscService(messagesMock.Object);

            await service.SendRandomYesNoAnswerAsync(new MiunieChannel()).ConfigureAwait(false);

            messagesMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.Is<PhraseKey>(pk => pk == PhraseKey.YES_NO_MAYBE)));
        }
    }
}
