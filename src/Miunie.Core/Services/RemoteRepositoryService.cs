using Miunie.Core.Attributes;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using System.Threading.Tasks;

namespace Miunie.Core
{
    [Service]
    public class RemoteRepositoryService
    {
        private readonly IDiscordMessages _discordMessages;

        public RemoteRepositoryService(IDiscordMessages discordMessages)
        {
            _discordMessages = discordMessages;
        }

        public async Task ShowRepositoryAsync(MiunieChannel c)
            => await _discordMessages.SendMessageAsync(c, PhraseKey.SHOW_REMOTE_REPO);
    }
}
