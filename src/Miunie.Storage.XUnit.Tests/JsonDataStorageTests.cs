using System;
using Xunit;

namespace Miunie.Storage.XUnit.Tests
{
    public class JsonDataStorageTests : IClassFixture<JsonDataStorageFixture>
    {        
        private JsonDataStorageFixture _storageFixture;
        
        public JsonDataStorageTests(JsonDataStorageFixture storageFixture)
        {
            _storageFixture = storageFixture;
        }

        [Fact]
        public void ShouldStoreAndRestoreObject()
        {
            string objectToSave = "hello";
            string fileName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string collection = "Welcome";
            ShouldStoreObject(objectToSave, collection, fileName);
            ShouldRestoreExistingObject(objectToSave, collection, fileName);            
        }

        private void ShouldStoreObject(object objectToSave, string collection, string fileName)
        {
            _storageFixture.Storage.StoreObject(objectToSave, collection, fileName);

            Assert.True(_storageFixture.Storage.KeyExists(collection, fileName));
        }
        
        private void ShouldRestoreExistingObject(object expectedObejct, string collection, string key)
        {            
            var restoredObject = _storageFixture.Storage.RestoreObject<object>(collection, key);

            Assert.Equal(expectedObejct, restoredObject);
        }
    }
}
