using Miunie.Core;
using Miunie.Core.Discord;
using System.Linq;

namespace Miunie.Core.Services
{
    public class ListDirectoryService
    {
        private readonly IDiscordServers _discordServers;

        public ListDirectoryService(IDiscordServers discordServers)
        {
            _discordServers = discordServers;
        }

        public string Of(MiunieUser user, MiunieChannel channel)
        {
            if(!user.NavCursor.Any())
            {
                return GetRootOf(channel);
            }

            return string.Empty;
        }

        public string GetRootOf(MiunieChannel channel)
        {
            var serverName = _discordServers.GetServerNameById(channel.GuildId);
            return $"Data\n{serverName}";
        }
    }
}

