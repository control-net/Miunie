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

        public async Task ShowProfile(MiunieUser sourceUser, MiunieChannel sourceChannel)
        {
            // TODO(Peter): This should probably come from some kind of a Language
            // Service to enable things like translation and easy edits in case
            // of a misspell.
            var response = $":frame_photo: **USER PROFILE**\nReputation: {sourceUser.Reputation}";

            await _discordMessages.SendMessage(response, sourceChannel);
        }
    }
}
