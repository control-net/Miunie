using System;
using System.Collections.Generic;
using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class LanguageResources : ILanguageResources
    {
        private IDataStorage _storage;

        private Random _rand;
        private readonly string _collection = "LangResources";
        private readonly string _langKeyFormat = "Phrases{0}";
        private readonly string _defaultLang = "En";

        public LanguageResources(IDataStorage storage, Random rand)
        {
            _storage = storage;
            _rand = rand;
        }

        public string GetPhrase(string key)
            => GetPhrase(key, _defaultLang);

        public string GetPhrase(string key, string lang)
        {
            var langKey = GetLangKey(lang);
            var phrases = _storage.RestoreObject<Dictionary<string, string[]>>(
                                                                    _collection,
                                                                    langKey);

            phrases.TryGetValue(key, out var values);
            return PickRandom(values);            
        }

        private string PickRandom(string[] collection)
        {
            if(collection is null) { return String.Empty; }
            if(collection.Length == 1){ return collection[0]; }
            var index = _rand.Next(collection.Length);
            return collection[index];
        }

        public string GetFormatted(string key, params object[] objs)
            => GetTranslatedFormatted(key, _defaultLang, objs);

        public string GetTranslatedFormatted(
                                string key,
                                string lang,
                                params object[] objs)
        {
            var phrase = GetPhrase(key, lang);
            return String.Format(phrase, objs);
        }

        private string GetLangKey(string langKey)
            => String.Format(_langKeyFormat, langKey);
    }
}
