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

        public string Of(MiunieUser user, MiunieChannel channel)
        {
            if(!user.NavCursor.Any())
            {
                return GetRootOf(channel);
            }
            if(user.NavCursor.Count == 1)
            {
                return GetChannelsOf(channel);
            }

            return string.Empty;
        }

        public string GetRootOf(MiunieChannel channel)
        {
            var serverName = _discordServers.GetServerNameById(channel.GuildId);
            return $"Data{Separator}{serverName}";
        }

        public string GetChannelsOf(MiunieChannel channel)
        {
            var channelNames = _discordServers
                .GetChannelNamesFromServer(channel.GuildId);

            return string.Join(Separator, channelNames);
        }
    }
}

