using System.Threading.Tasks;
using DSharpPlus;

namespace Miunie.Discord
{
    public class DSharpClient
    {
        private DiscordClient _discordClient;

        public DSharpClient(DiscordClient discordClient)
        {
            _discordClient = discordClient;
        }

        public async Task StartAsync()
        {
            await _discordClient.ConnectAsync();
        }
    }
}