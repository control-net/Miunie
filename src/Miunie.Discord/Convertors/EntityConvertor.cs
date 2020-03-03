using Discord.WebSocket;
using Miunie.Core.Entities.Discord;
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

        public MiunieUser ConvertUser(SocketGuildUser m)
            => UserConvertor.DiscordMemberToMiunieUser(m);

        public MiunieChannel ConvertChannel(SocketGuildChannel c)
            => MiunieChannelConvertor.FromDiscordChannel(c);

        internal MiunieGuild ConvertGuild(SocketGuild g)
            => GuildConvertor.DiscordGuildToMiunieGuild(g);
    }
}
