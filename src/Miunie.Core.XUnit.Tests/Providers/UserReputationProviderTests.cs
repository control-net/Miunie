using System;
using Miunie.Core.Providers;
using Miunie.Core.Infrastructure;
using Xunit;
using Moq;
using Miunie.Core.XUnit.Tests.Data;

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
        public void AddReputation_ShouldIncrementReputation()
        {
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            var expectedRep = _users.Senne.Reputation.Value + 1;

            _repProvider.AddReputation(_users.Peter, _users.Senne);
            var actualRep = _users.Senne.Reputation.Value;

            Assert.Equal(expectedRep, actualRep);
        }

        [Fact]
        public void RemoveReputation_ShouldDecrementReputation()
        {
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            var expectedRep = _users.Senne.Reputation.Value - 1;

            _repProvider.RemoveReputation(_users.Peter, _users.Senne);
            var actualRep = _users.Senne.Reputation.Value;

            Assert.Equal(expectedRep, actualRep);
        }

        [Fact]
        public void TryAddReputation_ShouldGetTimeoutAfterAddingReputation()
        {
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);

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
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);

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
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            _users.Senne.Reputation.Value++;

            var hasAddedRep = _users.Senne.Reputation.PlusRepLog.TryAdd(_users.Peter.UserId, DateTime.Now);
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now.AddSeconds(_repProvider.TimeoutInSeconds + 1));
            var peterHasTimeout = _repProvider.CanAddReputation(_users.Peter, _users.Senne);
            var senneHasTimeout = _repProvider.CanAddReputation(_users.Senne, _users.Peter);

            Assert.True(hasAddedRep);
            Assert.False(peterHasTimeout);
            Assert.False(senneHasTimeout);
        }

        [Fact]
        public void TryRemoveReputation_ShouldRemoveTimeoutEventually()
        {
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now);
            _users.Senne.Reputation.Value--;

            var hasRemovedRep = _users.Senne.Reputation.MinusRepLog.TryAdd(_users.Peter.UserId, DateTime.Now);
            _dateTimeMock.Setup(dt => dt.UtcNow).Returns(DateTime.Now.AddSeconds(_repProvider.TimeoutInSeconds + 1));
            var peterHasTimeout = _repProvider.CanRemoveReputation(_users.Peter, _users.Senne);
            var senneHasTimeout = _repProvider.CanRemoveReputation(_users.Senne, _users.Peter);

            Assert.True(hasRemovedRep);
            Assert.False(peterHasTimeout);
            Assert.False(senneHasTimeout);
        }
    }
}

