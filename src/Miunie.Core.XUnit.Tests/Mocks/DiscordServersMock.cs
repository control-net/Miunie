using Miunie.Core.Discord;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class DiscordServersMock : IDiscordServers
    {
        private readonly ulong _acceptedId;
        private readonly string _returnedName;

        public DiscordServersMock(ulong acceptedId, string returnedName)
        {
            _acceptedId = acceptedId;
            _returnedName = returnedName;
        }

        public string GetServerNameById(ulong id)
        {
            Assert.Equal(_acceptedId, id);
            return _returnedName;
        }
    }
}

