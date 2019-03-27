using Miunie.Core.Storage;
using Miunie.Core.Providers;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieUserProviderTests
    {
        private readonly IMiunieUserProvider _provider;
        private readonly IDataStorage _dataStorage;

        public MiunieUserProviderTests()
        {
            _dataStorage = new DataStorageMock();
            _provider = new MiunieUserProvider(_dataStorage);
        }

        [Fact]
        public void NonExistentUserGetsCreated()
        {
            Assert.False(_dataStorage.KeyExists("g200", "u100"));
            var actual = _provider.GetById(100, 200);
            Assert.NotNull(actual);
            Assert.True(_dataStorage.KeyExists("g200", "u100"));
        }
    }
}

