using System;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Core;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Miunie.Discord.CommandModules;
using Microsoft.Extensions.DependencyInjection;

namespace Miunie.Discord
{
    public class DSharpPlusDiscord : IDiscord, IDiscordMessages
    {
        private DiscordClient _discordClient;
        private CommandsNextExtension _commandService;
        private IServiceProvider _services;
        private readonly IBotConfiguration _botConfiguration;
        private readonly EntityConvertor _entityConvertor;

        public DSharpPlusDiscord(
            IBotConfiguration botConfiguration, 
            EntityConvertor entityConvertor,
            IServiceProvider services)
        {
            _botConfiguration = botConfiguration;
            _entityConvertor = entityConvertor;
            _services = services;
        }

        public async Task RunAsync()
        {
            await InitializeDiscordClientAsync();

            InitializeCommandsNextModuleAsync();

            await Task.Delay(-1);
        }

        private async Task InitializeDiscordClientAsync()
        {
            var discordConfiguration = GetDefaultDiscordConfiguration();

            _discordClient = new DiscordClient(discordConfiguration);

            await _discordClient.ConnectAsync();
        }

        private void InitializeCommandsNextModuleAsync()
        {
            var config = GetDefaultCommandsNextConfiguration();
            _commandService = _discordClient.UseCommandsNext(config);
            _commandService.RegisterCommands<ProfileCommand>();
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
                Services = _services
            };
        }

        public async Task SendMessage(string message, MiunieChannel mc)
        {
            var channel = await _discordClient.GetChannelAsync(mc.ChannelId);
            await channel.SendMessageAsync(message);
        }
    }
}

