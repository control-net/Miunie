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

using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Infrastructure;
using Miunie.Core.Providers;
using Miunie.Core.XUnit.Tests.Data;
using Moq;
using System;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Providers
{
    public class UserReputationProviderTests
    {
        private readonly IUserReputationProvider _repProvider;
        private readonly Mock<IDateTime> _dateTimeMock;
        private readonly DummyMiunieUsers _users;

        public UserReputationProviderTests()
        {
            _dateTimeMock = new Mock<IDateTime>();
            _repProvider = new UserReputationProvider(new Mock<IMiunieUserProvider>().Object, _dateTimeMock.Object);
            _users = new DummyMiunieUsers();
        }

        [Fact]
        public void ReputationLog_NullUser_ShouldReturnEmptyCollection()
        {
            var result = _repProvider.GetReputation(null);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ReputationLog_NullReputationObject_ShouldReturnEmptyCollection()
        {
            var user = CreateUserWithNullReputation();

            var result = _repProvider.GetReputation(user);

            Assert.Empty(result);
        }

        [Fact]
        public void ReputationLog_NoReputation_ShouldReturnEmptyCollection()
        {
            var user = CreateUserWithoutReputation();

            var result = _repProvider.GetReputation(user);

            Assert.Empty(result);
        }

        [Fact]
        public void AddReputation_ShouldIncrementReputation()
        {
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            var expectedRep = _users.Senne.Reputation.Value + 1;

            _repProvider.AddReputation(_users.Peter, _users.Senne);
            var actualRep = _users.Senne.Reputation.Value;

            Assert.Equal(expectedRep, actualRep);
        }

        [Fact]
        public void RemoveReputation_ShouldDecrementReputation()
        {
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            var expectedRep = _users.Senne.Reputation.Value - 1;

            _repProvider.RemoveReputation(_users.Peter, _users.Senne);
            var actualRep = _users.Senne.Reputation.Value;

            Assert.Equal(expectedRep, actualRep);
        }

        [Fact]
        public void TryAddReputation_ShouldGetTimeoutAfterAddingReputation()
        {
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);

            var peterHasTimeout = _repProvider.CanAddReputation(_users.Peter, _users.Senne);
            _repProvider.AddReputation(_users.Peter, _users.Senne);
            var peterHasTimeoutAgain = _repProvider.CanAddReputation(_users.Peter, _users.Senne);
            var senneHasTimeout = _repProvider.CanAddReputation(_users.Senne, _users.Peter);

            Assert.False(peterHasTimeout);
            Assert.True(peterHasTimeoutAgain);
            Assert.False(senneHasTimeout);
        }

        [Fact]
        public void TryRemoveReputation_ShouldGetTimeoutAfterRemovingReputation()
        {
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);

            var peterHasTimeout = _repProvider.CanRemoveReputation(_users.Peter, _users.Senne);
            _repProvider.RemoveReputation(_users.Peter, _users.Senne);
            var peterHasTimeoutAgain = _repProvider.CanRemoveReputation(_users.Peter, _users.Senne);
            var senneHasTimeout = _repProvider.CanRemoveReputation(_users.Senne, _users.Peter);

            Assert.False(peterHasTimeout);
            Assert.True(peterHasTimeoutAgain);
            Assert.False(senneHasTimeout);
        }

        [Fact]
        public void TryAddReputation_ShouldRemoveTimeoutEventually()
        {
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            _users.Senne.Reputation.Value++;

            var hasAddedRep = _users.Senne.Reputation.PlusRepLog.TryAdd(_users.Peter.UserId, DateTime.Now);
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now.AddSeconds(_repProvider.TimeoutInSeconds + 1));
            var peterHasTimeout = _repProvider.CanAddReputation(_users.Peter, _users.Senne);
            var senneHasTimeout = _repProvider.CanAddReputation(_users.Senne, _users.Peter);

            Assert.True(hasAddedRep);
            Assert.False(peterHasTimeout);
            Assert.False(senneHasTimeout);
        }

        [Fact]
        public void TryRemoveReputation_ShouldRemoveTimeoutEventually()
        {
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            _users.Senne.Reputation.Value--;

            var hasRemovedRep = _users.Senne.Reputation.MinusRepLog.TryAdd(_users.Peter.UserId, DateTime.Now);
            _ = _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now.AddSeconds(_repProvider.TimeoutInSeconds + 1));
            var peterHasTimeout = _repProvider.CanRemoveReputation(_users.Peter, _users.Senne);
            var senneHasTimeout = _repProvider.CanRemoveReputation(_users.Senne, _users.Peter);

            Assert.True(hasRemovedRep);
            Assert.False(peterHasTimeout);
            Assert.False(senneHasTimeout);
        }

        private static MiunieUser CreateUserWithoutReputation()
        {
            return new MiunieUser
            {
                Name = "User",
                Reputation = new Reputation()
            };
        }

        private static MiunieUser CreateUserWithNullReputation()
        {
            return new MiunieUser
            {
                Name = "User",
                Reputation = null
            };
        }
    }
}
