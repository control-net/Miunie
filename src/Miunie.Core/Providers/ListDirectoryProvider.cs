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

        public async Task<DirectoryListing> OfAsync(MiunieUser user)
        {
            if (!user.NavCursor.Any())
            {
                return await GetRootOfAsync(user);
            }

            if (user.NavCursor.Count == 1)
            {
                return await GetChannelsOfAsync(user);
            }

            return null;
        }

        private async Task<DirectoryListing> GetRootOfAsync(MiunieUser user)
        {
            var serverName = await _discordServers.GetServerNameByIdAsync(user.GuildId);

            return new DirectoryListing
            {
                Result = new ReadOnlyCollection<string>(new List<string> { "Data", serverName })
            };
        }

        private async Task<DirectoryListing> GetChannelsOfAsync(MiunieUser user)
        {
            var channelNames = await _discordServers.GetChannelNamesAsync(user.GuildId);

            return new DirectoryListing
            {
                Result = new ReadOnlyCollection<string>(channelNames)
            };
        }
    }
}
