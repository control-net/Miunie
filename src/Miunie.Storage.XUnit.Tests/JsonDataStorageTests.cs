using System;
using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace Miunie.Storage.XUnit.Tests
{
    public class JsonDataStorageTests : IClassFixture<JsonDataStorageFixture>
    {
        private readonly JsonDataStorageFixture _storageFixture;
        private readonly object _objectToSave;
        private readonly string _resourcesFolder;
        private readonly string _collection;

        public JsonDataStorageTests(JsonDataStorageFixture storageFixture)
        {
            _storageFixture = storageFixture;
            _resourcesFolder = _storageFixture.ResourcesFolder;
            _objectToSave = "hello";
            _collection = "Welcome";
        }

        [Fact]
        public void ShouldRestoreCollection()
        {
            var objects = _storageFixture.Storage
                .RestoreCollection<object>(_collection);

            Assert.NotEmpty(objects);
        }

        [Fact]
        public void ShouldStoreObject()
        {
            var file = GenerateFileName();

            _storageFixture.Storage.StoreObject(_objectToSave, _collection, file);

            Assert.True(ObjectExists(file));
        }

        [Fact]
        public void ShouldRestoreExistingObject()
        {
            const int expected = 53415;

            var file = SaveObjectInTestCollection(expected);
            var actual = _storageFixture.Storage
                .RestoreObject<int>(_collection, file);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        private void KeyShouldExist()
        {
            const string obj = "a object";
            var file = SaveObjectInTestCollection(obj);

            var expected = ObjectExists(file);
            var actual = _storageFixture.Storage.KeyExists(_collection, file);

            Assert.True(expected == actual);
        }

        private bool ObjectExists(string key)
        {
            var file = string.Concat(key, ".json");
            var path = Path.Combine(_resourcesFolder, _collection, file);
            return File.Exists(path);
        }

        private string SaveObjectInTestCollection(object obj)
        {
            GenerateCollectionDirectory(_collection);
            var file = GenerateFileName();
            var json = JsonConvert.SerializeObject(obj);
            var filePath = Path.Combine(_resourcesFolder, _collection, file);
            var pathWithExtension = string.Concat(filePath, ".json");
            File.WriteAllText(pathWithExtension, json);
            return file;
        }

        private void GenerateCollectionDirectory(string collection)
        {
            var collectionDir = Path.Combine(_resourcesFolder, collection);
            Directory.CreateDirectory(collectionDir);
        }

        private static string GenerateFileName()
            => DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
    }
}
