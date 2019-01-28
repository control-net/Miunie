using Miunie.Core;
using Miunie.Core.Discord;
using System.Linq;

namespace Miunie.Core.Services
{
    public class ListDirectoryService
    {
        private readonly IDiscordServers _discordServers;
        private const string Separator = "\n";

        public ListDirectoryService(IDiscordServers discordServers)
        {
            _discordServers = discordServers;
        }

        public string Of(MiunieUser user)
        {
            if(!user.NavCursor.Any())
            {
                return GetRootOf(user);
            }
            if(user.NavCursor.Count == 1)
            {
                return GetChannelsOf(user);
            }

            return string.Empty;
        }

        public string GetRootOf(MiunieUser user)
        {
            var serverName = _discordServers.GetServerNameById(user.GuildId);
            return $"Data{Separator}{serverName}";
        }

        public string GetChannelsOf(MiunieUser user)
        {
            var channelNames = _discordServers
                .GetChannelNamesFromServer(user.GuildId);

            return string.Join(Separator, channelNames);
        }
    }
}

