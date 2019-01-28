using Miunie.Core.Discord;
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

        public string GetServerNameById(ulong id)
        {
            Assert.Equal(_acceptedId, id);
            return _returnedName;
        }

        public string[] GetChannelNamesFromServer(ulong id)
        {
            Assert.Equal(_acceptedId, id);
            return _returnedChannelNames;
        }
    }
}

