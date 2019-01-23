using System;
using Xunit;

namespace Miunie.Storage.XUnit.Tests
{
    public class JsonDataStorageTests : IClassFixture<JsonDataStorageFixture>
    {
        private JsonDataStorageFixture _storageFixture;
        private readonly object _objectToSave;
        private readonly string _collection;

        public JsonDataStorageTests(JsonDataStorageFixture storageFixture)
        {
            _storageFixture = storageFixture;
            _objectToSave = "hello";
            _collection = "Welcome";
        }

        [Fact]
        public void ShouldStoreAndRestoreObject()
        {
            string fileName = DateTimeOffset.Now
                .ToUnixTimeMilliseconds()
                .ToString();

            ShouldStoreObject(_objectToSave, _collection, fileName);
            ShouldRestoreExistingObject(_objectToSave, _collection, fileName);
            ShouldRestoreCollection(_collection);
        }

        private void ShouldRestoreCollection(string collection)
        {
            var objects = _storageFixture.Storage
                .RestoreCollection<object>(collection);

            Assert.NotEmpty(collection);
        }

        private void ShouldStoreObject(
            object obj,
            string collection,
            string file)
        {
            _storageFixture.Storage.StoreObject(obj, collection, file);

            Assert.True(_storageFixture.Storage.KeyExists(collection, file));
        }

        private void ShouldRestoreExistingObject(
            object expected,
            string collection,
            string key)
        {
            var restoredObject = _storageFixture.Storage
                .RestoreObject<object>(collection, key);

            Assert.Equal(expected, restoredObject);
        }
    }
}

