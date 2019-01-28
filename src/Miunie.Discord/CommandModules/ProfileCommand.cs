using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
        public async Task ShowProfile(CommandContext ctx, DiscordMember m)
        {
            var miunieUser = _entityConvertor
                .DiscordMemberToMiunieUser(m);
            var miunieChannel = _entityConvertor
                .DiscordChannelToMiunieUser(ctx.Channel);

            await _profileService.ShowProfile(miunieUser, miunieChannel);
        }
    }
}

