using System;
using System.Linq;
using Miunie.Core.Storage;
using Miunie.Core.Assertion;

namespace Miunie.Core.Language
{
    public class LanguageResources : ILanguageResources
    {
        private readonly IDataStorage _storage;
        private readonly Random _rand;

        private string _langKey = "PhrasesEn";
        private LangResource[] _resources;

        private const string Collection = "Lang";
        private const string LangKeyFormat = "Phrases{0}";

        public LanguageResources(IDataStorage storage, Random rand)
        {
            _storage = storage;
            _rand = rand;
        }

        public void SetLanguage(string langKey)
        {
            _langKey = GetFormattedLangKey(langKey);
            FetchResources();
        }

        public string GetPhrase(string key, params object[] objs)
        {
            EnsureResourcesAreLoaded();
            var resource = GetResourceByKey(key);
            if(resource is null) { return string.Empty; }
            var phrase = resource.GetValue(_rand);
            return string.Format(phrase, objs);
        }

        private void EnsureResourcesAreLoaded()
        {
           if(_resources is null) { FetchResources(); }
           Assert.NotNull(_resources, "Lang Resources couldn't be fetched");
        }

        private void FetchResources()
        {
            _resources = _storage
                .RestoreObject<LangResource[]>(Collection, _langKey);
        }

        private LangResource GetResourceByKey(string key)
            => _resources.FirstOrDefault(r => r.Key == key);

        private static string GetFormattedLangKey(string langKey)
            => string.Format(LangKeyFormat, langKey);
    }
}

