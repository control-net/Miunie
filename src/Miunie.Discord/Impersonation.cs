using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using Miunie.Core;

namespace Miunie.Discord
{
    public class Impersonation : IDiscordImpersonation
    {
        private readonly IDiscord _discord;

        public Impersonation(IDiscord discord)
        {
            _discord = discord;
        }

        public IEnumerable<GuildView> GetAvailableGuilds()
            =>_discord.Client?.Guilds.Select(g => new GuildView
            {
                Id = g.Value.Id,
                IconUrl = g.Value.IconUrl,
                Name = g.Value.Name
            });

        public async Task<IEnumerable<TextChannelView>> GetAvailableTextChannelsAsync(ulong guildId)
        {
            var guild = await _discord.Client.GetGuildAsync(guildId);
            return guild.Channels.Where(c => c.Type == ChannelType.Text).Select(c => new TextChannelView
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
