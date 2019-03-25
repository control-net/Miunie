using Miunie.Core.Providers;
using Xunit;
using Moq;

namespace Miunie.Core.XUnit.Tests.Providers
{
    public class UserReputationProviderTests
    {
        private readonly IUserReputationProvider _reputationProvider;
        private readonly DummyMiunieUsers _data = new DummyMiunieUsers();

        public UserReputationProviderTests()
        {
            _reputationProvider = new UserReputationProvider(new Mock<IMiunieUserProvider>().Object);
        }

        [Fact]
        public void ShouldAddReputation()
        {
            var expectedReputation = _data.Senne.Reputation.Value + 1;
            
            _reputationProvider.AddReputation(_data.Peter, _data.Senne);
            
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldGetTimeoutAfterAddingReputation()
        {
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
            var expectedReputation = _data.Senne.Reputation.Value - 1;
            
            _reputationProvider.RemoveReputation(_data.Peter, _data.Senne);
            
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }

        [Fact]
        public void ShouldGetTimeoutAfterRemovingReputation()
        {
            var expectedReputation = _data.Senne.Reputation.Value - 1;

            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Peter, _data.Senne));

            _reputationProvider.RemoveReputation(_data.Peter, _data.Senne);

            Assert.True(_reputationProvider.RemoveReputationHasTimeout(_data.Peter, _data.Senne));
            Assert.False(_reputationProvider.RemoveReputationHasTimeout(_data.Senne, _data.Peter));
            Assert.Equal(expectedReputation, _data.Senne.Reputation.Value);
        }
    }
}