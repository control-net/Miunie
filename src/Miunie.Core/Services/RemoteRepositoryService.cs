using System.Threading.Tasks;
using Miunie.Core.Providers;

namespace Miunie.Core
{
    public class RemoteRepositoryService
    {
        private readonly IDiscordMessages _discordMessages;
        private readonly IRemoteRepositoryProvider _remoteProvider;

        public RemoteRepositoryService(IDiscordMessages discordMessages, IRemoteRepositoryProvider remoteProvider)
        {
            _discordMessages = discordMessages;
            _remoteProvider = remoteProvider;
        }

        public async Task ShowRepository(MiunieChannel c)
            => await _discordMessages.SendMessage(c, PhraseKey.SHOW_REMOTE_REPO, _remoteProvider.GetRemoteUrl());

        public void SetRepository(string repository)
            => _remoteProvider.SetRemoteUrl(repository);
    }
}

