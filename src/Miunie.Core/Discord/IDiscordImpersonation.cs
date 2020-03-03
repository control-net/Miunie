using Miunie.Core.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordImpersonation
    {
        IEnumerable<GuildView> GetAvailableGuilds();
        Task<IEnumerable<TextChannelView>> GetAvailableTextChannelsAsync(ulong guildId);
    }
}
