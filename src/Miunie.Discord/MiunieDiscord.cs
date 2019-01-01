using System.Threading.Tasks;
using Miunie.Core;

namespace Miunie.Discord
{
    public class MiunieDiscord : IDiscord
    {
        private DSharpClient _client;
        private CommandHandler _commandHandler;

        public MiunieDiscord(DSharpClient client, CommandHandler commandHandler)
        {
            _client = client;
            _commandHandler = commandHandler;
        }

        public async Task RunAsync()
        {
            await _client.ConnectAsync();
            await _commandHandler.InitializeAsync();
        }
    }
}