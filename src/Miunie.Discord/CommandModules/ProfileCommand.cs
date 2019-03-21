using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class ProfileCommand : BaseCommandModule
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly ProfileService _profileService;

        public ProfileCommand(
            EntityConvertor entityConvertor,
            ProfileService profileService)
        {
            _entityConvertor = entityConvertor;
            _profileService = profileService;
        }

        [Command("profile")]
        public async Task ShowProfile(CommandContext ctx, MiunieUser m)
        {
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.ShowProfile(m, channel);
        }
    }
}
