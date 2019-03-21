using System.Collections.Generic;

namespace Miunie.Core.Storage
{
    public interface IDataStorage
    {
        void StoreObject(object obj, string collection, string key);
        T RestoreObject<T>(string collection, string key);
        bool KeyExists(string collection, string key);
        IEnumerable<T> RestoreCollection<T>(string collection);
    }
}
