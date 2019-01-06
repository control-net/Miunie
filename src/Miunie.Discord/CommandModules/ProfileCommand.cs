using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class ProfileCommand
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly ProfileService _profileService;

        public ProfileCommand(EntityConvertor entityConvertor, ProfileService profileService)
        {
            _entityConvertor = entityConvertor;
            _profileService = profileService;
        }

        [Command("profile")]
        public async Task ShowProfile(CommandContext context, DiscordMember member)
        {
            var miunieUser = _entityConvertor.DiscordMemberToMiunieUser(member);
            var miunieChannel = _entityConvertor.DiscordChannelToMiunieUser(context.Channel);
            await _profileService.ShowProfile(miunieUser, miunieChannel);
        }
    }
}
