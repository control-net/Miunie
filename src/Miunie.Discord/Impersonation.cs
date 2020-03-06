using Discord;
using Discord.WebSocket;
using Miunie.Core.Discord;
using Miunie.Core.Entities.Views;
using Miunie.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Miunie.Discord
{
    public class Impersonation : IDiscordImpersonation
    {
        private readonly IDiscord _discord;
        private readonly ILogWriter _logger;

        public event EventHandler MessageReceived;

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

        public async Task<IEnumerable<TextChannelView>> GetAvailableTextChannelsAsync(ulong guildId, bool loadMessages = true)
        {
            if(guildId == 0) { return new TextChannelView[0]; }

            var guild = _discord.Client.GetGuild(guildId);
            var textChannels = guild.Channels.Where(c => c is SocketTextChannel).Cast<SocketTextChannel>();
            var result = new List<TextChannelView>();
            foreach (var channel in textChannels)
            {
                try
                {
                    result.Add(new TextChannelView
                    {
                        Id = channel.Id,
                        Name = $"# {channel.Name}",
                        Messages = loadMessages ? await GetMessagesFrom(channel) : new List<MessageView>()
                    });
                }
                catch (Exception)
                {
                    _logger.Log($"Miunie cannot read from the '{channel.Name}' channel.");
                }
            }

            return result;
        }

        public async Task<IEnumerable<MessageView>> GetMessagesFromTextChannelAsync(ulong guildId, ulong channelId)
        {
            var guild = _discord.Client.GetGuild(guildId);
            var textChannel = guild.Channels.Where(c => c is SocketTextChannel && c.Id == channelId).Cast<SocketTextChannel>().FirstOrDefault();

            if (textChannel == null) { return new MessageView[0]; }

            return await GetMessagesFrom(textChannel);
        }

        public async Task SendTextToChannelAsync(string text, ulong id)
        {
            var textChannel = _discord.Client.GetChannel(id) as SocketTextChannel;
            if (textChannel is null) { return; }

            await textChannel.SendMessageAsync(text);
        }

        public void SubscribeForMessages()
        {
            _discord.Client.MessageReceived += Client_MessageReceivedHandler;
        }

        public void UnsubscribeForMessages()
        {
            _discord.Client.MessageReceived -= Client_MessageReceivedHandler;
        }

        private Task Client_MessageReceivedHandler(SocketMessage m)
        {
            MessageReceived?.Invoke(m, EventArgs.Empty);

            return Task.CompletedTask;
        }

        private async Task<IEnumerable<MessageView>> GetMessagesFrom(SocketTextChannel channel)
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
    }
}
