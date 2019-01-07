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
            var resourcesDirectory = Directory.CreateDirectory(_resourcesFolder);
            var _groups = resourcesDirectory.GetDirectories().ToList();
        }

        public JsonDataStorage(string resourcesFolder)
        {
            _resourcesFolder = resourcesFolder;
            Directory.CreateDirectory(_resourcesFolder);
        }
        
        public void StoreObject(object obj, string collection, string key)
        {
            Directory.CreateDirectory($"{_resourcesFolder}/{collection}");
            var file = GetFileNameFromKey(key);
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            string filePath = String.Concat(_resourcesFolder, "/", collection, "/", file);
            File.WriteAllText(filePath, json);
        }        

        public T RestoreObject<T>(string collection, string key)
        {
            var file = GetFileNameFromKey(key);
            // NOTE(Peter): This is to ensure files like "Users/u123"
            // are valid and create a subdirectory if needed.
            Directory.CreateDirectory($"{_resourcesFolder}/{collection}");
            var filePath = String.Concat(collection, "/", file);
            string json = GetOrCreateFileContents(filePath);
            return JsonConvert.DeserializeObject<T>(json);
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
            var file = GetFileNameFromKey(key);
            return LocalFileExists(String.Concat(collection, "/", file));
        }
        
        private string GetFileNameFromKey(string key)
            => String.Format(FileTemplate, key);
        private bool LocalFileExists(string file)
        {
            string filePath = String.Concat(_resourcesFolder, "/", file);
            return File.Exists(filePath);
        }

        private string GetOrCreateFileContents(string file)
        {
            string filePath = String.Concat(_resourcesFolder, "/", file);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
                return "";
            }
            return File.ReadAllText(filePath);
        }
    }
}
