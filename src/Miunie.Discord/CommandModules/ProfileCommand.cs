using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class ProfileCommand
    {
        EntityConvertor _entityConvertor;

        public ProfileCommand(EntityConvertor entityConvertor)
        {
            _entityConvertor = entityConvertor;
        }

        [Command("profile")]
        public async Task ShowProfile(CommandContext context, DiscordMember memeber)
        {
            //TODO: (Charly) uncomment once EntityConvertor and ProfileService are implemented, along with MiunieEntities
            //var miunieUser = _entityConvertor.DiscordMemberToMiunieUser(member);
            //var miunieChannel = _entityConvertor.DiscordChannelToMiunieUser(context.Channel);
            //_profileService.ShowProfile(miunieUser, miunieChannel);
        }
    }
}
