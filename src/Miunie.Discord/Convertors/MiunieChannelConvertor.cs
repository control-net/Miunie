using Discord.WebSocket;
using Miunie.Core;
using System;

namespace Miunie.Discord.Convertors
{
    public class MiunieChannelConvertor
    {
        public static MiunieChannel FromDiscordChannel(SocketGuildChannel channel)
        {
            if (channel is null) throw new ArgumentNullException(nameof(channel));

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
