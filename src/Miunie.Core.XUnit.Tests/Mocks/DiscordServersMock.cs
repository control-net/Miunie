using Miunie.Core.Discord;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class DiscordServersMock : IDiscordGuilds
    {
        private readonly ulong _acceptedId;
        private readonly string _returnedName;
        private readonly string[] _returnedChannelNames;

        public DiscordServersMock(
                ulong acceptedId,
                string returnedName,
                string[] returnedChannelNames)
        {
            _acceptedId = acceptedId;
            _returnedName = returnedName;
            _returnedChannelNames = returnedChannelNames;
        }

        public Task<MiunieGuild> FromAsync(MiunieUser user)
        {
            Assert.Equal(_acceptedId, user.GuildId);
            return Task.FromResult(new MiunieGuild
            {
                Name = _returnedName,
                Id = user.GuildId,
                ChannelNames = _returnedChannelNames
            });
        }
    }
}

