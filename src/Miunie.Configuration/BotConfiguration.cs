using Miunie.Discord.Configuration;

namespace Miunie.Configuration
{
    public class BotConfiguration : IBotConfiguration
    {
        private IConfiguration _config;

        private const string DiscordBotTokenKey = "DiscordToken";

        public BotConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string GetBotToken()
        => _config.GetValueFor(DiscordBotTokenKey);
    }
}

