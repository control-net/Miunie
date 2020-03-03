using Miunie.Core.Discord;
using Miunie.Core.Logging;
using Miunie.Core.Providers;
using Miunie.Core.XUnit.Tests.Data;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Services
{
    public class ProfileServiceReputationTests
    {
        private readonly Mock<IUserReputationProvider> _repProviderMock;
        private readonly ProfileService _profileService;
        private readonly DummyMiunieUsers _users;

        private readonly Expression<Func<MiunieUser, bool>> _hasDraxId;
        private readonly Expression<Func<MiunieUser, bool>> _hasPeterId;
        private readonly Expression<Func<MiunieUser, bool>> _hasSenneId;

        public ProfileServiceReputationTests()
        {
            _repProviderMock = new Mock<IUserReputationProvider>();
            var discordMsgMock = new Mock<IDiscordMessages>();
            _profileService = new ProfileService(discordMsgMock.Object, _repProviderMock.Object, new Mock<ILogWriter>().Object, new Mock<IDiscordConnection>().Object);
            _users = new DummyMiunieUsers();

            _hasDraxId = u => u.UserId == _users.Drax.UserId;
            _hasPeterId = u => u.UserId == _users.Peter.UserId;
            _hasSenneId = u => u.UserId == _users.Senne.UserId;
        }

        [Fact]
        public async Task GiveReputationAsync_ShouldCheckForTimeoutBeforeAdding()
        {
            await _profileService.GiveReputationAsync(_users.Drax, _users.Senne, new MiunieChannel());

            _repProviderMock.Verify(rp => rp.CanAddReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)
            ), Times.Once());

            _repProviderMock.Verify(rp => rp.AddReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)
            ), Times.Once());
        }

        [Fact]
        public async Task GiveReputationAsync_ShouldNotAddRepToSelf()
        {
            await _profileService.GiveReputationAsync(_users.Senne, _users.Senne, new MiunieChannel());

            // NOTE(Peter):
            // Makes sure the service didn't add reputation to self
            // and that it didn't bother with checking the timeout either.
            _repProviderMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GiveReputationAsync_ShouldCheckForTimeoutBeforeRemoving()
        {
            await _profileService.RemoveReputationAsync(_users.Drax, _users.Senne, new MiunieChannel());

            _repProviderMock.Verify(rp => rp.CanRemoveReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)
            ), Times.Once());

            _repProviderMock.Verify(rp => rp.RemoveReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)
            ), Times.Once());
        }

        [Fact]
        public async Task GiveReputationAsync_ShouldNotRemoveRepFromSelf()
        {
            await _profileService.RemoveReputationAsync(_users.Senne, _users.Senne, new MiunieChannel());

            _repProviderMock.VerifyNoOtherCalls();
        }
    }
}
