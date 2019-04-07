using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Miunie.Core.Services;
using Miunie.Discord.Convertors;
using Miunie.Core;

namespace Miunie.Discord.CommandModules
{
    public class DirectoryCommand : BaseCommandModule
    {
        private EntityConvertor _entityConvertor;
        private DirectoryService _directoryService;

        public DirectoryCommand(EntityConvertor entityConvertor, DirectoryService directoryService)
        {
            _directoryService = directoryService;
            _entityConvertor = entityConvertor;
        }

        [Command("ls")]
        [Description("List information about the FILEs in the current directory.")]
        public async Task ListDirectoryAsync(CommandContext ctx)
        {
            var chan = _entityConvertor.ConvertChannel(ctx.Channel);
            var user = _entityConvertor.ConvertUser((DiscordMember)ctx.User);
            await _directoryService.ListDirectoryAsync(chan, user);
        }
    }
}
