using System;
using System.IO;
using Miunie.Core.Storage;
using Newtonsoft.Json;

namespace Miunie.Storage
{
    public class JsonDataStorage : IDataStorage
    {
        private readonly string resourcesFolder = "Resources";

        public JsonDataStorage()
        {
            Directory.CreateDirectory(resourcesFolder);
        }

        public void StoreObject(object obj, string file)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            string filePath = String.Concat(resourcesFolder, "/", file);
            File.WriteAllText(filePath, json);
        }        

        public T RestoreObject<T>(string file)
        {
            string json = GetOrCreateFileContents(file);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public bool KeyExists(string key)
        {
            return LocalFileExists(key);
        }

        private bool LocalFileExists(string file)
        {
            string filePath = String.Concat(resourcesFolder, "/", file);
            return File.Exists(filePath);
        }

        private string GetOrCreateFileContents(string file)
        {
            string filePath = String.Concat(resourcesFolder, "/", file);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
                return "";
            }
            return File.ReadAllText(filePath);
        }
    }
}
