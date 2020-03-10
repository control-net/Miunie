// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Discord;
using Discord.WebSocket;
using Miunie.Core.Discord;
using Miunie.Core.Entities.Views;
using Miunie.Core.Events;
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

        public Impersonation(IDiscord discord, ILogWriter logger)
        {
            _discord = discord;
            _logger = logger;
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public IEnumerable<GuildView> GetAvailableGuilds()
            => _discord.Client?.Guilds.Select(g => new GuildView
            {
                Id = g.Id,
                IconUrl = g.IconUrl,
                Name = g.Name
            });

        public Task<IEnumerable<TextChannelView>> GetAvailableTextChannelsAsync(ulong guildId)
        {
            if (guildId == 0) { return CompletedTextChannelViewTask(new TextChannelView[0]); }

            var guild = _discord.Client.GetGuild(guildId);
            var textChannels = guild.Channels
                .Where(IsViewableTextChannel)
                .Cast<SocketTextChannel>()
                .Select(ToTextChannelView);

            return CompletedTextChannelViewTask(textChannels);
        }

        public async Task<IEnumerable<MessageView>> GetMessagesFromTextChannelAsync(ulong guildId, ulong channelId)
        {
            if (guildId == 0) { return new MessageView[0]; }

            var guild = _discord.Client.GetGuild(guildId);
            var textChannel = guild.Channels
                .Where(c => c.Id == channelId && IsViewableTextChannel(c))
                .Cast<SocketTextChannel>()
                .FirstOrDefault();

            if (textChannel == null) { return new MessageView[0]; }

            var result = new List<MessageView>();

            try
            {
                result.AddRange(await GetMessagesFrom(textChannel));
            }
            catch (Exception ex)
            {
                _logger.Log($"Miunie cannot read from the '{textChannel.Name}' channel. {ex.Message}");
            }

            return result;
        }

        public async Task SendTextToChannelAsync(string text, ulong id)
        {
            var textChannel = _discord.Client.GetChannel(id) as SocketTextChannel;
            if (textChannel is null) { return; }

            _ = await textChannel.SendMessageAsync(text);
        }

        public void SubscribeForMessages()
        {
            _discord.Client.MessageReceived += Client_MessageReceivedHandler;
        }

        public void UnsubscribeForMessages()
        {
            _discord.Client.MessageReceived -= Client_MessageReceivedHandler;
        }

        private Task<IEnumerable<TextChannelView>> CompletedTextChannelViewTask(IEnumerable<TextChannelView> channels)
            => Task.FromResult(channels);

        private TextChannelView ToTextChannelView(SocketTextChannel channel)
            => new TextChannelView
            {
                Id = channel.Id,
                Name = $"# {channel.Name}",
                Messages = new MessageView[0],
                CanSendMessages = CanSendMessagesTo(channel)
            };

        private bool CanSendMessagesTo(SocketTextChannel channel)
            => channel.GetUser(_discord.Client.CurrentUser.Id)?.GetPermissions(channel).SendMessages ?? false;

        private bool IsViewableTextChannel(SocketGuildChannel c)
        {
            if (!(c is SocketTextChannel)) { return false; }

            return c.GetUser(_discord.Client.CurrentUser.Id) != null;
        }

        private Task Client_MessageReceivedHandler(SocketMessage m)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(ToMessageView(m)));

            return Task.CompletedTask;
        }

        private async Task<IEnumerable<MessageView>> GetMessagesFrom(SocketTextChannel channel)
        {
            var socketMessages = await channel.GetMessagesAsync(10).FlattenAsync();

            return socketMessages.Select(ToMessageView);
        }

        private MessageView ToMessageView(IMessage message) => new MessageView
        {
            ChannelId = message.Channel.Id,
            AuthorAvatarUrl = message.Author.GetAvatarUrl(),
            AuthorName = message.Author.Username,
            Content = message.Content,
            TimeStamp = message.CreatedAt.ToLocalTime()
        };
    }
}
