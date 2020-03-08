// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Miunie.Core.Logging;
using System;

namespace Miunie.Core.Providers
{
    public class LanguageProvider : ILanguageProvider
    {
        private static readonly string[] ResourceSeparators = { "{{OR}}" };

        private readonly ILogWriter _logger;
        private readonly Random _random;

        public LanguageProvider(ILogWriter logger, Random random)
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
