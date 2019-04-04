using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Core;
using Miunie.Core.Providers;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Miunie.Discord.CommandModules;
using Miunie.Core.Entities;

namespace Miunie.Discord
{
    public class DSharpPlusDiscord : IDiscord, IDiscordMessages
    {
        private DiscordClient _discordClient;
        private CommandsNextExtension _commandService;
        private readonly IServiceProvider _services;
        private readonly IBotConfiguration _botConfiguration;
        private readonly EntityConvertor _entityConvertor;
        private readonly ILanguageProvider _lang;

        public DSharpPlusDiscord(IBotConfiguration botConfiguration, EntityConvertor entityConvertor, IServiceProvider services, ILanguageProvider lang)
        {
            _botConfiguration = botConfiguration;
            _services = services;
            _entityConvertor = entityConvertor;
            _lang = lang;
        }

        public async Task RunAsync()
        {
            await InitializeDiscordClientAsync();
            InitializeCommandService();
            await Task.Delay(-1);
        }

        private async Task InitializeDiscordClientAsync()
        {
            var discordConfiguration = GetDefaultDiscordConfiguration();
            _discordClient = new DiscordClient(discordConfiguration);
            await _discordClient.ConnectAsync();
        }

        private void InitializeCommandService()
        {
            var config = GetDefaultCommandsNextConfiguration();
            _commandService = _discordClient.UseCommandsNext(config);
            _commandService.RegisterCommands<ProfileCommand>();
            RegisterConvertors();
        }

        private void RegisterConvertors()
        {
            _commandService.RegisterConverter(_entityConvertor.ChannelConvertor);
            _commandService.RegisterConverter(_entityConvertor.UserConvertor);
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

        public async Task SendMessage(MiunieChannel mc, PhraseKey phraseKey, params object[] parameters)
        {
            var channel = await _discordClient.GetChannelAsync(mc.ChannelId);
            var msg = _lang.GetPhrase(phraseKey.ToString(), parameters);
            await channel.SendMessageAsync(msg);
        }
    }
}
