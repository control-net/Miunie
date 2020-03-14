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

using Discord.WebSocket;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Logging;
using Miunie.Core.Providers;
using Miunie.Discord.Embeds;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miunie.Discord.Adapters
{
    public class DiscordMessagesAdapter : IDiscordMessages
    {
        private readonly IDiscord _discord;
        private readonly ILanguageProvider _lang;
        private readonly ILogWriter _log;

        public DiscordMessagesAdapter(IDiscord discord, ILanguageProvider lang, ILogWriter log)
        {
            _discord = discord;
            _lang = lang;
            _log = log;
        }

        public async Task SendMessageAsync(MiunieChannel mc, IEnumerable<ReputationEntry> repEntries, int index)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var embed = EmbedConstructor.CreateReputationLog(repEntries, index, _lang);

            _ = await channel.SendMessageAsync(embed: embed);
        }

        public async Task SendMessageAsync(MiunieChannel mc, PhraseKey phraseKey, params object[] parameters)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var msg = _lang.GetPhrase(phraseKey.ToString(), parameters);
            _ = await channel.SendMessageAsync(msg);
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieUser mu)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;

            if (channel is null)
            {
                LogSocketTextChannelCastFailed();
                return;
            }

            _ = await channel.SendMessageAsync(embed: mu.ToEmbed(_lang));
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieGuild mg)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;

            if (channel is null)
            {
                LogSocketTextChannelCastFailed();
                return;
            }

            _ = await channel.SendMessageAsync(embed: mg.ToEmbed(_lang));
        }

        private void LogSocketTextChannelCastFailed()
        {
            _log.LogError("Invalid cast to SocketTextChannel.");
        }
    }
}
