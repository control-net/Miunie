using Miunie.Core.Discord;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Miunie.Core.Providers
{
    public class ListDirectoryProvider : IListDirectoryProvider
    {
        private readonly IDiscordGuilds _discordGuilds;
        private MiunieGuild _currentGuild;

        public ListDirectoryProvider(IDiscordGuilds discordGuilds)
        {
            _discordGuilds = discordGuilds;
        }

        public async Task<DirectoryListing> OfAsync(MiunieUser user)
        {
            _currentGuild = await _discordGuilds.FromAsync(user);

            if (!user.NavCursor.Any())
            {
                return GetRootOf();
            }

            if (user.NavCursor.Count == 1)
            {
                return GetChannelsOf();
            }

            return null;
        }

        private DirectoryListing GetRootOf()
            => new DirectoryListing
            {
                Result = new ReadOnlyCollection<string>(new List<string> { "Data", _currentGuild.Name })
            };

        private DirectoryListing GetChannelsOf()
            => new DirectoryListing
            {
                Result = new ReadOnlyCollection<string>(_currentGuild.ChannelNames.ToList())
            };
    }
}
