using System;
using Miunie.Core.Logging;

namespace Miunie.Core.Providers
{
    public class LanguageProvider : ILanguageProvider
    {
        private readonly ILogger _logger;
        private readonly Random _random;

        private static readonly string[] ResourceSeparators = { "{{OR}}" };

        public LanguageProvider(ILogger logger, Random random)
        {
            _logger = logger;
            _random = random;
        }

        public string GetPhrase(string key, params object[] objs)
        {
            var resource = Strings.ResourceManager.GetString(key);
            if (resource is null)
            {
                _logger.LogError($"Unable to find Language Resource with the following key: {key}");
                return string.Empty;
            }

            var pool = resource.Split(ResourceSeparators, StringSplitOptions.RemoveEmptyEntries);
            return string.Format(pool[_random.Next(0, pool.Length)], objs);
        }
    }
}
