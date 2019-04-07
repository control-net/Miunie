using System.Threading.Tasks;
using Miunie.Core.Providers;

namespace Miunie.Core
{
    public class RemoteRepositoryService
    {
        private readonly IDiscordMessages _discordMessages;

        public RemoteRepositoryService(IDiscordMessages discordMessages)
        {
            _discordMessages = discordMessages;
        }

        public async Task ShowRepository(MiunieChannel c)
            => await _discordMessages.SendMessage(c, PhraseKey.SHOW_REMOTE_REPO);
    }
}

