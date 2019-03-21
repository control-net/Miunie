using Miunie.Core.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Miunie.Core.XUnit.Tests
{
    public class DataStorageMock : IDataStorage
    {
        private readonly Dictionary<string, object> _storage;

        public DataStorageMock()
        {
            _storage = new Dictionary<string, object>();
        }

        public bool KeyExists(string collection, string key)
            => _storage.ContainsKey($"{collection}/{key}");

        public IEnumerable<T> RestoreCollection<T>(string collection)
            => _storage.Where(p => p.Key.StartsWith($"{collection}/"))
                    .ToList()
                    .Cast<T>();

        public T RestoreObject<T>(string collection, string key)
        {
            var result = _storage.TryGetValue($"{collection}/{key}", out var obj);
            if(result)
            {
                return (T) obj;
            }
            return default(T);
        }

        public void StoreObject(object obj, string collection, string key)
            => _storage.TryAdd($"{collection}/{key}", obj);
    }
}
