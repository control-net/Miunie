using System;
using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class LanguageResources : ILanguageResources
    {
        private IDataStorage _storage;

        private Random _rand;
        private readonly string _collection = "Lang";

        public LanguageResources(IDataStorage storage, Random rand)
        {
            _storage = storage;
            _rand = rand;
        }

        public string GetPhrase(string key)
        {
            var phrases =_storage.RestoreObject<string[]>(_collection, key);
            return PickRandom(phrases);
        }

        private string PickRandom(string[] collection)
        {
            if(collection is null)
            {
                return String.Empty;
            }
            if(collection.Length == 1)
            {
                return collection[0];
            }
            var index = _rand.Next(collection.Length);
            return collection[index];
        }

        public string GetFormatted(string key, params object[] objs)
        {
            var phrase = GetPhrase(key);
            return String.Format(phrase, objs);
        }
    }
}
