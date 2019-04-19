using System.Collections.Generic;

namespace Miunie.Core
{
    public interface IDiscordImpersonation
    {
        IEnumerable<GuildView> GetAvailableGuilds();
    }
}
