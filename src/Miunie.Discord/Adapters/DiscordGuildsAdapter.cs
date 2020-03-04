using Miunie.Core.Discord;
using Miunie.Core.Entities.Discord;
using Miunie.Discord.Convertors;
using System.Threading.Tasks;

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

        public Task<MiunieGuild> FromAsync(MiunieUser user)
        {
            var guild = _discord.Client.GetGuild(user.GuildId);

            return Task.FromResult(_entityConvertor.ConvertGuild(guild));
        }
    }
}
