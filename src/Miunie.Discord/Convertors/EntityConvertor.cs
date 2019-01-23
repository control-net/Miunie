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

        public MiunieUser DiscordMemberToMiunieUser(DiscordMember member)
            => _miunieUserService.GetById(member.Id);

        public MiunieChannel DiscordChannelToMiunieUser(DiscordChannel c)
        {
            var miunieChannel = new MiunieChannel
            {
                ChannelId = c.Id,
                GuildId = c.GuildId
            };

            return miunieChannel;
        }
    }
}

