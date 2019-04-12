using Miunie.Core;
using Miunie.Discord.Logging;
using System.Threading.Tasks;

namespace Miunie.Discord
{
    public class MiunieDiscord : IMiunieDiscord
    {
        private readonly IDiscord _discord;

        public MiunieDiscord(IDiscord discord, DiscordLogger discordLogger, CommandServiceFactory cmdServiceFactory)
        {
            _discord = discord;
            _discord.Client.DebugLogger.LogMessageReceived += discordLogger.Log;
            cmdServiceFactory.Create(_discord.Client);
        }

        public async Task RunAsync() => await _discord.Client.ConnectAsync();
    }
}
