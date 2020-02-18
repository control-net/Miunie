using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class RemoteRepositoryCommand : ModuleBase<SocketCommandContext>
    {
        private readonly RemoteRepositoryService _remoteRepoService;
        private readonly EntityConvertor _entityConvertor;

        public RemoteRepositoryCommand(RemoteRepositoryService remoteRepoService, EntityConvertor entityConvertor)
        {
            _remoteRepoService = remoteRepoService;
            _entityConvertor = entityConvertor;
        }

        [Command("repo")]
        [Summary("Shows the official remote repository hosting the code of this bot")]
        public async Task ShowRepository()
        {
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _remoteRepoService.ShowRepositoryAsync(channel);
        }
    }
}

