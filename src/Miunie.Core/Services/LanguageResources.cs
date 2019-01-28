using System;
using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class LanguageResources : ILanguageResources
    {
        private IDataStorage _storage;
        private readonly string _collection = "Lang";

        public LanguageResources(IDataStorage storage)
        {
            _storage = storage;
        }

        public string GetPhrase(string key)
            => _storage.RestoreObject<string>(_collection, key);

        public string GetFormatted(string key, params object[] objs)
        {
            var phrase = GetPhrase(key);
            return String.Format(phrase, objs);
        }
    }
}
