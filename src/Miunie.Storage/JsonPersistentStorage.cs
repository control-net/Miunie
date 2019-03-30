using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Miunie.Core.Logging;
using Miunie.Core.Storage;
using Newtonsoft.Json;

namespace Miunie.Storage
{
    public class JsonPersistentStorage : IPersistentStorage
    {
        private const string JsonDataDirectory = "Resources/";

        private readonly ILogger _logger;

        public JsonPersistentStorage(ILogger logger)
        {
            _logger = logger;
        }

        private static string ToStoragePath(MemberInfo objType, string collection)
            => Path.Combine(JsonDataDirectory, objType.Name, collection);

        public void Store<T>(T obj, string collection, string key)
        {
            var path = ToStoragePath(typeof(T), collection);
            var filePath = Path.Combine(path, $"{key}.json");
#if DEBUG
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
#else
            var json = JsonConvert.SerializeObject(obj);
#endif

            Directory.CreateDirectory(path);
            File.WriteAllText(filePath, json);
        }

        public IEnumerable<T> RestoreMany<T>(string collection, string pattern = "*")
        {
            var path = ToStoragePath(typeof(T), collection);
            EnsureDirectoryExists(path);
            var files = Directory.GetFiles(path, $"{pattern}.json");
            return files.Select(RestoreFromFile<T>);
        }

        public T RestoreSingle<T>(string collection, string pattern)
            => RestoreMany<T>(collection, pattern).FirstOrDefault();

        private static void EnsureDirectoryExists(string path)
            => Directory.CreateDirectory(path);

        private T RestoreFromFile<T>(string filePath)
        {
            AssertFileExists(filePath);
            var json = File.ReadAllText(filePath);
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonReaderException)
            {
                _logger.LogError($"Json Storage could not read the following file's JSON:\n{filePath}");
                throw new Exception("Failed to parse a JSON file.");
            }
        }

        private void AssertFileExists(string filePath)
        {
            if (File.Exists(filePath)) return;
            _logger.LogError($"Persistent storage could not find the following file:\n{filePath}");
            throw new FileNotFoundException();
        }
    }
}
