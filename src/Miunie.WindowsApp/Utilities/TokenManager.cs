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
        }
    }
}
