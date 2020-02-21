using Discord;
using Discord.WebSocket;
using Miunie.Core.Configuration;
using System;
using System.Threading.Tasks;

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

        public async Task InitializeAsync()
        {
            if (string.IsNullOrWhiteSpace(_botConfig.DiscordToken))
                throw new ArgumentNullException(nameof(_botConfig.DiscordToken));

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
            });

            await Client.LoginAsync(TokenType.Bot, _botConfig.DiscordToken);
        }

        public void DisposeOfClient() => Client = null;
    }
}
