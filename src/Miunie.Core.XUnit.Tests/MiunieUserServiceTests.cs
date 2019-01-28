using Miunie.Core.Storage;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieUserServiceTests
    {
        private readonly MiunieUserService _service;
        private readonly IDataStorage _dataStorage;

        public MiunieUserServiceTests()
        {
            _dataStorage = new DataStorageMock();
            _service = new MiunieUserService(_dataStorage);
        }

        [Fact]
        public void NonExistentUserGetsCreated()
        {
            Assert.False(_dataStorage.KeyExists("g200", "u100"));
            var actual = _service.GetById(100, 200);
            Assert.NotNull(actual);
            Assert.True(_dataStorage.KeyExists("g200", "u100"));
        }
    }
}

