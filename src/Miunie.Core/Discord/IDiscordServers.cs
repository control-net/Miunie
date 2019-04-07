using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordServers
    {
        Task<string> GetServerNameById(ulong id);
        Task<string[]> GetChannelNamesFromServer(ulong id);
    }
}

