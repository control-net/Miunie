using System.Collections.Generic;
using System.Linq;

namespace Miunie.Core.XUnit.Tests.Language
{
    public static class LanguageTestHelper
    {
        public static LangResource[] ToLangResources(this Dictionary<string, string> res)
            => res.Select(r => new LangResource
            {
                Key = r.Key,
                Pool = new[] {r.Value}
            }).ToArray();
    }
}