using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Core;
using Miunie.Discord.Configuration;

namespace Miunie.Discord
{
    public class MiunieDiscord : IDiscord
    {
        private DiscordClient _discordClient;
        private CommandsNextModule _commandsNextModule;
        private IBotConfiguration _botConfiguration;

        public MiunieDiscord(IBotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
        }

        public async Task RunAsync()
        {
            await InitializeDiscordClientAsync();

            await InitializeCommandsNextModuleAsync();
        }

        private async Task InitializeDiscordClientAsync()
        {
            var discordConfiguration = GetDefaultDiscordConfiguration();

            _discordClient = new DiscordClient(discordConfiguration);

            await _discordClient.ConnectAsync();
        }

        private async Task InitializeCommandsNextModuleAsync()
        {
            var commandsNextConfiguration = GetDefaultCommandsNextConfiguration();
            _commandsNextModule = _discordClient.UseCommandsNext(commandsNextConfiguration);

            //TODO (Charly) : Uncomment the next line to register commands, with the parameter being the assembly
            //await _commandsNextModule.RegisterCommands();
        }

        private DiscordConfiguration GetDefaultDiscordConfiguration()
        {
            return new DiscordConfiguration()
            {
                Token = _botConfiguration.GetBotToken(),
                TokenType = TokenType.User,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };
        }

        private CommandsNextConfiguration GetDefaultCommandsNextConfiguration()
        {
            return new CommandsNextConfiguration()
            {
                EnableMentionPrefix = true
            };
        }
    }
}
