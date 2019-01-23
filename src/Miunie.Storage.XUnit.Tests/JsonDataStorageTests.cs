using System;
using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace Miunie.Storage.XUnit.Tests
{
    public class JsonDataStorageTests : IClassFixture<JsonDataStorageFixture>
    {
        private JsonDataStorageFixture StorageFixture;
        private readonly object ObjectToSave;
        private readonly string ResourcesFolder;
        private readonly string Collection;

        public JsonDataStorageTests(JsonDataStorageFixture storageFixture)
        {
            StorageFixture = storageFixture;
            ResourcesFolder = StorageFixture.ResourcesFolder;
            ObjectToSave = "hello";
            Collection = "Welcome";
        }

        [Fact]
        public void ShouldRestoreCollection()
        {
            var objects = StorageFixture.Storage
                .RestoreCollection<object>(Collection);

            Assert.NotEmpty(objects);
        }

        [Fact]
        public void ShouldStoreObject()
        {
            string file = GenerateFileName();

            StorageFixture.Storage.StoreObject(ObjectToSave, Collection, file);

            Assert.True(ObjectExists(file));
        }

        [Fact]
        public void ShouldRestoreExistingObject()
        {
            int expected = 53415;

            var file = SaveObjectInTestCollection(expected);
            var actual = StorageFixture.Storage
                .RestoreObject<int>(Collection, file);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        private void KeyShouldExist()
        {
            var obj = "a object";
            var file = SaveObjectInTestCollection(obj);

            var expected = ObjectExists(file);
            var actual = StorageFixture.Storage.KeyExists(Collection, file);

            Assert.True(expected == actual);
        }

        private bool ObjectExists(string key)
        {
            var file = String.Concat(key, ".json");
            var path = Path.Combine(ResourcesFolder, Collection, file);
            return File.Exists(path);
        }

        private string SaveObjectInTestCollection(object obj)
        {
            GenerateCollectionDirectory(Collection);
            string file = GenerateFileName();
            string json = JsonConvert.SerializeObject(obj);
            string filePath = Path.Combine(ResourcesFolder, Collection, file);
            string pathWithExtension = String.Concat(filePath, ".json");
            File.WriteAllText(pathWithExtension, json);
            return file;
        }

        private void GenerateCollectionDirectory(string collection)
        {
            var collectionDir = Path.Combine(ResourcesFolder, collection);
            Directory.CreateDirectory(collectionDir);
        }

        private string GenerateFileName()
            => DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
    }
}
