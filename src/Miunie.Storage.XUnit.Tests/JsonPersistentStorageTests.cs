using System.Collections.Generic;
using System.Linq;
using Miunie.Core.Logging;
using Miunie.Core.Storage;
using Miunie.Storage.XUnit.Tests.Helpers;
using Moq;
using Xunit;

namespace Miunie.Storage.XUnit.Tests
{
    public class JsonPersistentStorageTests
    {
        private readonly TestDataHelper _testDataHelper;
        private readonly IPersistentStorage _storage;
        private const string Collection = "Test/Collection";

        public JsonPersistentStorageTests()
        {
            _testDataHelper = new TestDataHelper();
            _storage = new JsonPersistentStorage(new Mock<ILogger>().Object);
        }

        [Fact]
        public void Dispose()
            => _testDataHelper.DeleteTestData();

        [Fact]
        public void ShouldRestoreByExactPattern()
        {
            const string pattern = "DataKey-A";
            var expected = _testDataHelper.GetDummyByKey(pattern);

            var actual = _storage.RestoreSingle<DummyDataHolder>(Collection, pattern);

            Assert.Equal(expected.Text, actual.Text);
            Assert.Equal(expected.Number, actual.Number);
        }

        [Fact]
        public void ShouldRestoreEverything()
        {
            var expected = _testDataHelper.GetAllDummyFiles();

            var actual = _storage.RestoreMany<DummyDataHolder>(Collection);

            AssertDummyCollectionsMatch(expected, actual);
        }

        [Fact]
        public void ShouldRestoreSome()
        {
            const string pattern = "DataKey-*";
            var expected = _testDataHelper.GetDummyByKeyContains(pattern.Replace("*", ""));

            var actual = _storage.RestoreMany<DummyDataHolder>(Collection, pattern);

            AssertDummyCollectionsMatch(expected, actual);
        }

        [Fact]
        public void ShouldStoreAndRestore()
        {
            const string collection = "My/Collection/ABC";
            const string key = "ToStoreKey";
            var expected = new DummyDataHolder
            {
                Text = "Hello, World!",
                Number = 420
            };

            _storage.Store(expected, collection, key);
            var actual = _storage.RestoreSingle<DummyDataHolder>(collection, key);

            Assert.Equal(expected.Text, actual.Text);
            Assert.Equal(expected.Number, actual.Number);
        }

        private void AssertDummyCollectionsMatch(IEnumerable<DummyDataHolder> expected, IEnumerable<DummyDataHolder> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            foreach(var expectedItem in expected)
            {
                Assert.Contains(actual, actualItem 
                        => DummyDataHoldersAreEqual(actualItem, expectedItem));
            }
        }

        private bool DummyDataHoldersAreEqual(DummyDataHolder a, DummyDataHolder b)
            => a.Text == b.Text && a.Number == b.Number;
    }
}