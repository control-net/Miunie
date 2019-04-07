using Miunie.Core.Discord;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Miunie.Core;
using System.Threading.Tasks;

namespace Miunie.Core.Providers
{
    public class ListDirectoryProvider : IListDirectoryProvider
    {
        private readonly IDiscordServers _discordServers;
        private const string Separator = "\n";

        public ListDirectoryProvider(IDiscordServers discordServers)
        {
            _discordServers = discordServers;
        }

        public async Task<DirectoryListing> Of(MiunieUser user)
        {
            if (!user.NavCursor.Any())
            {
                return await GetRootOf(user);
            }

            if (user.NavCursor.Count == 1)
            {
                return await GetChannelsOf(user);
            }

            return null;
        }

        private async Task<DirectoryListing> GetRootOf(MiunieUser user)
        {
            var serverName = await _discordServers.GetServerNameById(user.GuildId);

            return new DirectoryListing
            {
                Result = new ReadOnlyCollection<string>(new List<string> { "Data", serverName })
            };
        }

        private async Task<DirectoryListing> GetChannelsOf(MiunieUser user)
        {
            var channelNames = await _discordServers.GetChannelNamesFromServer(user.GuildId);

            return new DirectoryListing
            {
                Result = new ReadOnlyCollection<string>(channelNames)
            };
        }
    }
}
