using System;
using System.Data;
using System.Linq;
using Miunie.Core.Logging;
using Miunie.Core.Storage;

namespace Miunie.Core.Language
{
    public class LanguageResources : ILanguageResources
    {
        private readonly LanguageResourceCollection _langCollection;
        private readonly Random _rand;
        private readonly ILogger _logger;

        private const string Collection = "Lang";
        private const string LangKey = "PhrasesEn";

        public LanguageResources(IPersistentStorage storage, Random rand, ILogger logger)
        {
            _langCollection = storage.RestoreSingle<LanguageResourceCollection>(Collection, LangKey);
            _rand = rand;
            _logger = logger;
            AssertNotNull(_langCollection);
        }

        private void AssertNotNull(LanguageResourceCollection langCollection)
        {
            if (!(langCollection is null)) { return; }
            _logger.Log("ERROR: LanguageResources could not restore the ResourceCollection from the Persistent Storage.");
            throw new NoNullAllowedException("LanguageResourceCollection is null.");
        }

        public string GetPhrase(string key, params object[] objs)
        {
            var resource = GetResourceByKey(key);
            if(resource is null) { return string.Empty; }
            var phrase = resource.GetValue(_rand);
            return string.Format(phrase, objs);
        }

        private LangResource GetResourceByKey(string key)
            => _langCollection.Resources.FirstOrDefault(r => r.Key == key);
    }
}
