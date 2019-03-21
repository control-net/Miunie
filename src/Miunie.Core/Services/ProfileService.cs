using System.Threading.Tasks;

namespace Miunie.Core
{
    public class ProfileService
    {
        private readonly IDiscordMessages _discordMessages;
        private readonly ILanguageResources _lang;

        public ProfileService(
                IDiscordMessages discordMessages,
                ILanguageResources lang)
        {
            _discordMessages = discordMessages;
            _lang = lang;
        }

        public async Task ShowProfile(MiunieUser u, MiunieChannel c)
        {            
            var response = _lang.GetPhrase("SHOW_PROFILE", u.Reputation);
            await _discordMessages.SendMessage(response, c);
        }
    }
}
