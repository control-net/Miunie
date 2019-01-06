using System;
using System.IO;
using Miunie.Core.Storage;
using Newtonsoft.Json;

namespace Miunie.Storage
{
    public class JsonDataStorage : IDataStorage
    {
        private readonly string _resourcesFolder = "Resources";

        public JsonDataStorage()
        {
            Directory.CreateDirectory(_resourcesFolder);
        }

        public JsonDataStorage(string resourcesFolder)
        {
            _resourcesFolder = resourcesFolder;
            Directory.CreateDirectory(_resourcesFolder);
        }
        
        public void StoreObject(object obj, string file)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            string filePath = String.Concat(_resourcesFolder, "/", file);
            File.WriteAllText(filePath, json);
        }        

        public T RestoreObject<T>(string file)
        {
            // NOTE(Peter): This is to ensure files like "Users/u123"
            // are valid and create a subdirectory if needed.
            Directory.CreateDirectory($"{_resourcesFolder}/{Path.GetDirectoryName(file)}");

            string json = GetOrCreateFileContents(file);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void WipeData()
        {
            var files = Directory.GetFiles(_resourcesFolder);
            foreach(var file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(_resourcesFolder);
        }

        public bool KeyExists(string key)
        {
            return LocalFileExists(key);
        }

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
