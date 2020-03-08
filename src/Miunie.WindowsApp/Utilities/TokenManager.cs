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
