using System.Threading.Tasks;
using Miunie.Core;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class RemoteRepositoryCommand : BaseCommandModule
    {
        private readonly RemoteRepositoryService _remoteRepoService;
        private readonly EntityConvertor _entityConvertor;

        public RemoteRepositoryCommand(RemoteRepositoryService remoteRepoService, EntityConvertor entityConvertor)
        {
            _remoteRepoService = remoteRepoService;
            _entityConvertor = entityConvertor;
        }

        [Command("repo")]
        [Description("Shows the official remote repository hosting the code of this bot")]
        public async Task ShowRepository(CommandContext ctx)
        {
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _remoteRepoService.ShowRepository(channel);
        }
    }
}

