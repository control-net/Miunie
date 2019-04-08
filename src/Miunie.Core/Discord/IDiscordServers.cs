using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordServers
    {
        Task<string> GetServerNameByIdAsync(ulong id);
        Task<string[]> GetChannelNamesAsync(ulong id);
    }
}

