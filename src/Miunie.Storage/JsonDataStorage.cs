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
            var resourcesDirectory = Directory
                .CreateDirectory(_resourcesFolder);
        }

        public JsonDataStorage(string resourcesFolder)
        {
            _resourcesFolder = resourcesFolder;
            Directory.CreateDirectory(_resourcesFolder);
        }

        public void StoreObject(object obj, string collection, string key)
        {
            var file = GetFileNameByKey(key);
            EnsureCollectionExists(collection);

            string json = JsonConvert
                .SerializeObject(obj, Formatting.Indented);

            string filePath = Path.Combine(_resourcesFolder, collection, file);
            File.WriteAllText(filePath, json);
        }

        public T RestoreObject<T>(string collection, string key)
        {
            var file = GetFileNameByKey(key);
            EnsureCollectionExists(collection);
            var filePath = String.Concat(collection, "/", file);
            return RestoreByPath<T>(filePath);
        }

        public IEnumerable<T> RestoreCollection<T>(string collection)
        {
            EnsureCollectionExists(collection);
            var collectionPath = Path.Combine(_resourcesFolder, collection);
            var filePaths =  Directory.GetFiles(collectionPath);
            var files = new HashSet<T>();
            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var file = RestoreObject<T>(collection, fileName);
                files.Add(file);
            }
            return files;
        }

        public void WipeData()
        {
            var directories = Directory.GetDirectories(_resourcesFolder);

            foreach(var directory in directories)
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

        public bool KeyExists(string collection, string key)
        {
            var file = GetFileNameByKey(key);
            return LocalFileExists(String.Concat(collection, "/", file));
        }

        private bool LocalFileExists(string file)
        {
            string filePath = String.Concat(_resourcesFolder, "/", file);
            return File.Exists(filePath);
        }

        private string GetFileNameByKey(string key)
            => String.Format(FileTemplate, key);

        private T RestoreByPath<T>(string filePath)
        {
            string json = GetOrCreateFileContent(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private string GetOrCreateFileContent(string path)
        {
            var filePath = String.Concat(_resourcesFolder, "/", path);
            if (File.Exists(filePath)) return File.ReadAllText(filePath);
            File.WriteAllText(filePath, "");
            return "";
        }

        private void EnsureCollectionExists(string collection)
        {
            var collectionDir = Path.Combine(_resourcesFolder, collection);
            Directory.CreateDirectory(collectionDir);
        }
    }
}
