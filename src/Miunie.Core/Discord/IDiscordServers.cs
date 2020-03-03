using Miunie.Core.Entities.Discord;
using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordGuilds
    {
        Task<MiunieGuild> FromAsync(MiunieUser user);
    }
}

