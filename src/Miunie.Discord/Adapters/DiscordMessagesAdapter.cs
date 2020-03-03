using Discord.WebSocket;
using Miunie.Core;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
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

        public DiscordMessagesAdapter(IDiscord discord, ILanguageProvider lang)
        {
            _discord = discord;
            _lang = lang;
        }

        public async Task SendMessageAsync(MiunieChannel mc, IEnumerable<ReputationEntry> repEntries, int index)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var embed = EmbedConstructor.CreateReputationLog(repEntries, index, _lang);

            await channel.SendMessageAsync(embed: embed);
        }

        public async Task SendMessageAsync(MiunieChannel mc, PhraseKey phraseKey, params object[] parameters)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel ?? throw new SocketTextChannelCastException();
            var msg = _lang.GetPhrase(phraseKey.ToString(), parameters);
            await channel.SendMessageAsync(msg);
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieUser mu)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel ?? throw new SocketTextChannelCastException();
            await channel.SendMessageAsync(embed: mu.ToEmbed(_lang));
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieGuild mg)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel ?? throw new SocketTextChannelCastException();
            await channel.SendMessageAsync(embed: mg.ToEmbed(_lang));
        }
    }
}
