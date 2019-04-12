using System.Threading.Tasks;
using Miunie.Core;
using Miunie.Core.Discord;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.Adapters
{
    public class DiscordGuildsAdapter : IDiscordGuilds
    {
        private readonly IDiscord _discord;
        private readonly EntityConvertor _entityConvertor;

        public DiscordGuildsAdapter(IDiscord discord, EntityConvertor entityConvertor)
        {
            _discord = discord;
            _entityConvertor = entityConvertor;
        }

        public async Task<MiunieGuild> FromAsync(MiunieUser user)
        {
            var guild = await _discord.Client.GetGuildAsync(user.GuildId);

            return _entityConvertor.ConvertGuild(guild);
        }
    }
}
