// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Miunie.Core.Discord;
using Miunie.Core.Entities.Discord;
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
        private readonly Expression<Func<MiunieUser, bool>> _hasSenneId;

        public ProfileServiceReputationTests()
        {
            _repProviderMock = new Mock<IUserReputationProvider>();
            var discordMsgMock = new Mock<IDiscordMessages>();
            _profileService = new ProfileService(discordMsgMock.Object, _repProviderMock.Object, new Mock<ILogWriter>().Object, new Mock<IDiscordConnection>().Object);
            _users = new DummyMiunieUsers();

            _hasDraxId = u => u.UserId == _users.Drax.UserId;
            _hasSenneId = u => u.UserId == _users.Senne.UserId;
        }

        [Fact]
        public async Task GiveReputationAsync_ShouldCheckForTimeoutBeforeAdding()
        {
            await _profileService.GiveReputationAsync(_users.Drax, _users.Senne, new MiunieChannel());

            _repProviderMock.Verify(
                rp => rp.CanAddReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)), Times.Once());

            _repProviderMock.Verify(
                rp => rp.AddReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)), Times.Once());
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

            _repProviderMock.Verify(
                rp => rp.CanRemoveReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)), Times.Once());

            _repProviderMock.Verify(
                rp => rp.RemoveReputation(
                It.Is(_hasDraxId),
                It.Is(_hasSenneId)), Times.Once());
        }

        [Fact]
        public async Task GiveReputationAsync_ShouldNotRemoveRepFromSelf()
        {
            await _profileService.RemoveReputationAsync(_users.Senne, _users.Senne, new MiunieChannel());

            _repProviderMock.VerifyNoOtherCalls();
        }
    }
}
