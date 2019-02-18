using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Miunie.Core.Storage;
using Newtonsoft.Json;

namespace Miunie.Storage
{
    public class JsonDataStorage : IDataStorage
    {
        private readonly string _resourcesFolder = "Resources";
        private const string FileTemplate = "{0}.json";

        public JsonDataStorage()
        {
            Directory.CreateDirectory(_resourcesFolder);
        }

        public JsonDataStorage(string resourcesFolder)
        {
            _resourcesFolder = resourcesFolder;
            Directory.CreateDirectory(resourcesFolder);
        }

        public void StoreObject(object obj, string collection, string key)
        {
            EnsureCollectionDirectoryExists(collection);
            var filePath = GetFullFilePath(collection, key);
            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(filePath, json);
        }

        public T RestoreObject<T>(string collection, string key)
        {
            EnsureCollectionDirectoryExists(collection);
            var filePath = GetFullFilePath(collection, key);
            var json = GetOrCreateFileContent(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public IEnumerable<T> RestoreCollection<T>(string collection)
        {
            EnsureCollectionDirectoryExists(collection);
            var collectionDir = GetCollectionDirectory(collection);
            IEnumerable<string> fileKeys = Directory.GetFiles(collectionDir).Select(Path.GetFileNameWithoutExtension);

            return fileKeys.Select(fileKey => RestoreObject<T>(collection, fileKey));
        }

        public bool KeyExists(string collection, string key)
        {
            return File.Exists(GetFullFilePath(collection, key));
        }

        public void WipeData()
        {
            var directories = Directory.GetDirectories(_resourcesFolder);

            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(directory);
            }

            Directory.Delete(_resourcesFolder);
        }

        private string KeyToFullFileName(string key) => String.Format(FileTemplate, key);

        private string GetFullFilePath(string collection, string key) =>
            Path.Combine(_resourcesFolder, collection, KeyToFullFileName(key));

        private string GetCollectionDirectory(string collection) => Path.Combine(_resourcesFolder, collection);

        private void EnsureCollectionDirectoryExists(string collection)
        {
            var collectionDir = Path.Combine(_resourcesFolder, collection);
            Directory.CreateDirectory(collectionDir);
        }

        private string GetOrCreateFileContent(string filePath)
        {
            if (File.Exists(filePath)) return File.ReadAllText(filePath);
            File.WriteAllText(filePath, "");
            return "";
        }
    }
}
