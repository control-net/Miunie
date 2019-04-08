using System.Threading.Tasks;

namespace Miunie.Core
{
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
