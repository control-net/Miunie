namespace Miunie.Core.Storage
{
    public interface IDataStorage
    {
        void StoreObject(object obj, string key);
        T RestoreObject<T>(string key);
        bool KeyExists(string key);
    }
}
