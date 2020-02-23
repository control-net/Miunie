using Discord;
using Discord.WebSocket;
using Miunie.Core.Configuration;

namespace Miunie.Discord
{
    public class MiunieDiscordClient : IDiscord
    {
        public DiscordSocketClient Client { get; private set; }

        private readonly IBotConfiguration _botConfig;

        public MiunieDiscordClient(IBotConfiguration botConfig)
        {
            _botConfig = botConfig;
        }

        public async void Initialize()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
            });

            await Client.LoginAsync(TokenType.Bot, _botConfig.DiscordToken);
        }

        public void DisposeOfClient() => Client = null;
    }
}
