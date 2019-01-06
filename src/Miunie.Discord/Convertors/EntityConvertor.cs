using DSharpPlus.Entities;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    /// <summary>
    /// This class purpose is to convert DSharpPlus entities
    /// to Miunie entities.
    /// </summary>
    public class EntityConvertor
    {
        private readonly MiunieUserService _miunieUserService;

        public EntityConvertor(MiunieUserService miunieUserService)
        {
            _miunieUserService = miunieUserService;
        }

        public MiunieUser DiscordMemberToMiunieUser(DiscordMember discordMember)
            => _miunieUserService.GetById(discordMember.Id);
         
        public MiunieChannel DiscordChannelToMiunieUser(DiscordChannel discordChannel)
        {
            var miunieChannel = new MiunieChannel
            {
                ChannelId = discordChannel.Id,
                GuildId = discordChannel.GuildId
            };

            return miunieChannel;
        }
    }
}
