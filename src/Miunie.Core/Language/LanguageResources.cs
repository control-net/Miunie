using System;
using System.Linq;
using Miunie.Core.Storage;

namespace Miunie.Core.Language
{
    public class LanguageResources : ILanguageResources
    {
        private readonly LangResource[] _resources;
        private readonly Random _rand;

        private const string Collection = "Lang";
        private const string LangKeyFormat = "Phrases{0}";
        private const string LangKey = "PhrasesEn";

        public LanguageResources(IDataStorage storage, Random rand)
        {
            _resources = storage.RestoreObject<LangResource[]>(Collection, LangKey);
            _rand = rand;
        }

        public string GetPhrase(string key, params object[] objs)
        {
            var resource = GetResourceByKey(key);
            if(resource is null) { return string.Empty; }
            var phrase = resource.GetValue(_rand);
            return string.Format(phrase, objs);
        }

        private LangResource GetResourceByKey(string key)
            => _resources.FirstOrDefault(r => r.Key == key);

        private static string GetFormattedLangKey(string langKey)
            => string.Format(LangKeyFormat, langKey);
    }
}
