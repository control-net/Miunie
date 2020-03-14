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

using Miunie.Core;
using System.Linq;

namespace Miunie.WindowsApp.Utilities
{
    public class TokenManager
    {
        public bool StringHasValidTokenStructure(string possibleToken)
            => possibleToken.Length == 59 && possibleToken.ElementAt(24) == '.' && possibleToken.ElementAt(31) == '.';

        public void ApplyToken(string token, MiunieBot miunie)
        {
            miunie.BotConfiguration.DiscordToken = token;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["MIUNIE_UWP_TOKEN"] = token;
        }

        public void LoadToken(MiunieBot miunie)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var token = localSettings.Values["MIUNIE_UWP_TOKEN"]?.ToString();

            if (!string.IsNullOrWhiteSpace(token))
            {
                miunie.BotConfiguration.DiscordToken = token;
            }
        }
    }
}
