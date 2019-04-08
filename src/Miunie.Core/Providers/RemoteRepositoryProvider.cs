using Miunie.Core.Storage;

namespace Miunie.Core.Providers
{
    public class RemoteRepositoryProvider : IRemoteRepositoryProvider
    {
        private readonly IPersistentStorage _storage;
        private string _collection = "RemoteRepository";
        private string _key = "repository";

        public RemoteRepositoryProvider(IPersistentStorage storage)
        {
            _storage = storage;
        }
        public string GetRemoteUrl()
        {
            return _storage.RestoreSingle<string>(_collection, _key);
        }

        public void SetRemoteUrl(string url)
        {
            _storage.Store(url, _collection, _key);
        }
    }
}

