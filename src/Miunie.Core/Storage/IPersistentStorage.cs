using System.Collections.Generic;

namespace Miunie.Core.Storage
{
    public interface IPersistentStorage
    {
        void Store<T>(T item, string collection, string key);
        IEnumerable<T> RestoreMany<T>(string collection, string pattern = "*");
        T RestoreSingle<T>(string collection, string pattern);
    }
}
