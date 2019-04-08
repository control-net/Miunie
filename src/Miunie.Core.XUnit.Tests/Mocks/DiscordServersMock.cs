using Miunie.Core.Discord;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class DiscordServersMock : IDiscordServers
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

        public Task<string> GetServerNameByIdAsync(ulong id)
        {
            Assert.Equal(_acceptedId, id);
            return Task.FromResult(_returnedName);
        }

        public Task<string[]> GetChannelNamesAsync(ulong id)
        {
            Assert.Equal(_acceptedId, id);
            return Task.FromResult(_returnedChannelNames);
        }
    }
}

