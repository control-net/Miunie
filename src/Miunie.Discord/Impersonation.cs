using System.Collections.Generic;
using System.Linq;
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
    }
}
