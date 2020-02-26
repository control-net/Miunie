using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Core.Logging;

namespace Miunie.Discord
{
    public class Impersonation : IDiscordImpersonation
    {
        private readonly IDiscord _discord;
        private readonly ILogWriter _logger;

        public Impersonation(IDiscord discord, ILogWriter logger)
        {
            _discord = discord;
            _logger = logger;
        }

        public IEnumerable<GuildView> GetAvailableGuilds()
            =>_discord.Client?.Guilds.Select(g => new GuildView
            {
                Id = g.Id,
                IconUrl = g.IconUrl,
                Name = g.Name
            });

        public async Task<IEnumerable<TextChannelView>> GetAvailableTextChannelsAsync(ulong guildId)
        {
            var guild = _discord.Client.GetGuild(guildId);
            var textChannels = guild.Channels.Where(c => c is SocketTextChannel);
            var result = new List<TextChannelView>();
            foreach (var channel in textChannels)
            {
                try
                {
                    result.Add(new TextChannelView
                    {
                        Id = channel.Id,
                        Name = $"# {channel.Name}",
                        Messages = await GetMessagesFrom(channel as SocketTextChannel)
                    });
                }
                catch (Exception)
                {
                    _logger.Log($"Miunie cannot read from the '{channel.Name}' channel.");
                }
            }

            return result;
        }

        public async Task<IEnumerable<MessageView>> GetMessagesFrom(SocketTextChannel channel)
        {
            var msgs = await channel.GetMessagesAsync(10).FlattenAsync();
            return msgs.Select(m => new MessageView
            {
                AuthorAvatarUrl = m.Author.GetAvatarUrl(),
                AuthorName = m.Author.Username,
                Content = m.Content,
                TimeStamp = m.CreatedAt.ToLocalTime()
            });
        }

        public async Task SendTextToChannelAsync(string text, ulong id)
        {
            var textChannel = _discord.Client.GetChannel(id) as SocketTextChannel;
            if(textChannel is null) { return; }

            await textChannel.SendMessageAsync(text);
        }
    }
}
