using DSharpPlus;
using Miunie.Core.Configuration;

namespace Miunie.Discord
{
    public class MiunieDiscordClient : IDiscord
    {
        public DiscordClient Client { get; private set; }

        private readonly IBotConfiguration _botConfig;

        public MiunieDiscordClient(IBotConfiguration botConfig)
        {
            _botConfig = botConfig;
        }

        public void Initialize()
        {
            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = _botConfig.DiscordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Info,
                UseInternalLogHandler = false
            });
        }

        public void DisposeOfClient() => Client = null;
    }
}
