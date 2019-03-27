using System;
using Miunie.Core.Providers;
using Miunie.Core.Infrastructure;
using Xunit;
using Moq;

namespace Miunie.Core.XUnit.Tests.Providers
{
    public class UserReputationProviderTests
    {
        private readonly IUserReputationProvider _reputationProvider;
        private readonly Mock<IDateTime> _dateTimeMock;
        private readonly DummyMiunieUsers _data = new DummyMiunieUsers();

        public UserReputationProviderTests()
        {
            _dateTimeMock = new Mock<IDateTime>();
            _reputationProvider = new UserReputationProvider(new Mock<IMiunieUserProvider>().Object, _dateTimeMock.Object);
        }

        [Fact]
        public void ShouldAddReputation()
        {
            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now);
            var expectedReputation = _data.Senne.Reputation.Value + 1;

            _reputationProvider.AddReputation(_data.Peter, _data.Senne);

            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldGetTimeoutAfterAddingReputation()
        {
            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now);
            var expectedReputation = _data.Senne.Reputation.Value + 1;

            Assert.False(_reputationProvider.AddReputationHasTimeout(_data.Peter, _data.Senne));

            _reputationProvider.AddReputation(_data.Peter, _data.Senne);

            Assert.True(_reputationProvider.AddReputationHasTimeout(_data.Peter, _data.Senne));
            Assert.False(_reputationProvider.AddReputationHasTimeout(_data.Senne, _data.Peter));
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldRemoveReputation()
        {
            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now);
            var expectedReputation = _data.Senne.Reputation.Value - 1;

            _reputationProvider.RemoveReputation(_data.Peter, _data.Senne);

            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldGetTimeoutAfterRemovingReputation()
        {
            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now);
            var expectedReputation = _data.Senne.Reputation.Value - 1;

            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Peter, _data.Senne));

            _reputationProvider.RemoveReputation(_data.Peter, _data.Senne);

            Assert.True(_reputationProvider.RemoveReputationHasTimeout(_data.Peter, _data.Senne));
            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Senne, _data.Peter));
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldRemoveTimeoutForAddingEventually()
        {
            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now);
            var expectedReputation = _data.Senne.Reputation.Value + 1;

            Assert.False(_reputationProvider.AddReputationHasTimeout(_data.Peter, _data.Senne));

            _reputationProvider.AddReputation(_data.Peter, _data.Senne);

            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now.AddHours(1));

            Assert.False(_reputationProvider.AddReputationHasTimeout(_data.Peter, _data.Senne));
            Assert.False(_reputationProvider.AddReputationHasTimeout(_data.Senne, _data.Peter));
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldRemoveTimeoutForRemovingEventually()
        {
            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now);
            var expectedReputation = _data.Senne.Reputation.Value - 1;

            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Peter, _data.Senne));

            _reputationProvider.RemoveReputation(_data.Peter, _data.Senne);

            _dateTimeMock.Setup(dt => dt.Now).Returns(DateTime.Now.AddHours(1));

            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Peter, _data.Senne));
            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Senne, _data.Peter));
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }
    }
}

