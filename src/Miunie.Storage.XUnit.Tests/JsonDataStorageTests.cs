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
            
            ShouldStoreObject(objectToSave, fileName);
            ShouldRestoreExistingObject(objectToSave, fileName);            
        }

        private void ShouldStoreObject(object objectToSave, string fileName)
        {
            _storageFixture.Storage.StoreObject(objectToSave, fileName);

            Assert.True(_storageFixture.Storage.KeyExists(fileName));
        }
        
        private void ShouldRestoreExistingObject(object expectedObejct, string fileName)
        {            
            var restoredObject = _storageFixture.Storage.RestoreObject<object>(fileName);

            Assert.Equal(expectedObejct, restoredObject);
        }
    }
}
