using System.Threading.Tasks;
using Miunie.Core;

namespace Miunie.Core.Discord
{
    public interface IDiscordGuilds
    {
        Task<MiunieGuild> FromAsync(MiunieUser user);
    }
}

