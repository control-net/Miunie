using System;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Core;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;

namespace Miunie.Discord
{
    public class DSharpPlusDiscord : IDiscord
    {
        private DiscordClient _discordClient;
        private CommandsNextModule _commandsNextModule;
        private DependencyCollection _dependencyCollection;
        private IBotConfiguration _botConfiguration;

        public DSharpPlusDiscord(IBotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
        }

        public async Task RunAsync()
        {
            InitializeDependencyCollection();

            await InitializeDiscordClientAsync();

            InitializeCommandsNextModuleAsync();

            await Task.Delay(-1);
        }

        private void InitializeDependencyCollection()
        {
            using (var dependencyCollectionBuilder = new DependencyCollectionBuilder())
            {
                dependencyCollectionBuilder.AddInstance(new EntityConvertor());
                _dependencyCollection = dependencyCollectionBuilder.Build();
            }
        }

        private async Task InitializeDiscordClientAsync()
        {
            var discordConfiguration = GetDefaultDiscordConfiguration();

            _discordClient = new DiscordClient(discordConfiguration);

            await _discordClient.ConnectAsync();
        }

        private void InitializeCommandsNextModuleAsync()
        {
            var commandsNextConfiguration = GetDefaultCommandsNextConfiguration();
            _commandsNextModule = _discordClient.UseCommandsNext(commandsNextConfiguration);
            _commandsNextModule.RegisterCommands<CommandModules.ProfileCommand>();
        }

        private DiscordConfiguration GetDefaultDiscordConfiguration()
        {
            return new DiscordConfiguration()
            {
                Token = _botConfiguration.GetBotToken(),
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Info,
                UseInternalLogHandler = true
            };
        }

        private CommandsNextConfiguration GetDefaultCommandsNextConfiguration()
        {
            return new CommandsNextConfiguration()
            {
                EnableMentionPrefix = true,
                Dependencies = _dependencyCollection
            };
        }
    }
}
