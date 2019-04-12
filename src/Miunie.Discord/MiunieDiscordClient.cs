using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Discord.Configuration;

namespace Miunie.Discord
{
    public class MiunieDiscordClient : IDiscord
    {
        public DiscordClient Client { get; }

        public MiunieDiscordClient(IBotConfiguration botConfig)
        {
            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = botConfig.GetBotToken(),
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Info,
                UseInternalLogHandler = false
            });
        }
    }
}
