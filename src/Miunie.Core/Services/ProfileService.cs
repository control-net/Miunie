using System;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class ProfileService
    {
        private readonly IDiscordMessages _discordMessages;

        public ProfileService(IDiscordMessages discordMessages)
        {
            _discordMessages = discordMessages;
        }

        public async Task ShowProfile(MiunieUser u, MiunieChannel c)
        {
            // TODO(Charly): Replace the following code with the commented code,
            // once the new service collection is implemented and the discord
            // wrapper updated to v4:

            // var rep = u.Reputation;
            // var response = _langResources.GetFormatted("SHOW_PROFILE", rep);
            // await _discordMessages.SendMessage(response, c);

            var response = ":frame_photo: **USER PROFILE**\n";
            response += $"Reputation: {u.Reputation}";

            await _discordMessages.SendMessage(response, c);
        }
    }
}

