using System;
using System.Collections.Generic;
using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class LanguageResources : ILanguageResources
    {
        private IDataStorage _storage;

        private Random _rand;
        private readonly string _collection = "Lang";
        private readonly string _langKeyFormat = "Phrases{0}";
        private string _langKey = "PhrasesEn";

        public LanguageResources(IDataStorage storage, Random rand)
        {
            _storage = storage;
            _rand = rand;
        }
        public void SetLanguage(string langKey)
            => _langKey = GetLangKey(langKey);

        public string GetPhrase(string key, params object[] objs)
        {
            var phrases = GetFromStorage(key);
            var phrase = PickRandom(phrases);
            return String.Format(phrase, objs);
        }

        private string[] GetFromStorage(string key)
        {
            var phrases = _storage.RestoreObject<Dictionary<string, string[]>>(
                                                                    _collection,
                                                                    _langKey);

            phrases.TryGetValue(key, out var values);
            return values;
        }

        private string GetLangKey(string langKey)
            => String.Format(_langKeyFormat, langKey);

        private string PickRandom(string[] collection)
        {
            if (collection is null) { return String.Empty; }
            var index = _rand.Next(collection.Length);
            return collection[index];
        }
    }
}
