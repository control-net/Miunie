using Discord.WebSocket;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    public class MiunieChannelConvertor
    {
        public static MiunieChannel FromDiscordChannel(SocketGuildChannel channel)
        {
            MiunieChannel miunieChannel;
            if (channel is default(SocketGuildChannel))
            {
                miunieChannel = default;
            }
            else
            {
                miunieChannel = new MiunieChannel()
                {
                    ChannelId = channel.Id,
                    GuildId = channel.Guild.Id
                };
            }
            return miunieChannel;
        }
    }
}
