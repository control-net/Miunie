using DSharpPlus.Entities;
using Miunie.Core;
using Miunie.Core.Providers;

namespace Miunie.Discord.Convertors
{
    public class EntityConvertor
    {
        public MiunieChannelConvertor ChannelConvertor { get; }
        public MiunieUserConverter UserConvertor { get; }
        public MiunieGuildConvertor GuildConvertor { get; }

        public EntityConvertor(IMiunieUserProvider miunieUserProvider)
        {
            ChannelConvertor = new MiunieChannelConvertor();
            UserConvertor = new MiunieUserConverter(miunieUserProvider);
            GuildConvertor = new MiunieGuildConvertor();
        }

        public MiunieUser ConvertUser(DiscordMember m)
            => UserConvertor.DiscordMemberToMiunieUser(m);

        public MiunieChannel ConvertChannel(DiscordChannel c)
            => MiunieChannelConvertor.FromDiscordChannel(c);

        internal MiunieGuild ConvertGuild(DiscordGuild g)
            => GuildConvertor.DiscordGuildToMiunieGuild(g);
    }
}
