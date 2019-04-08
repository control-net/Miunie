using DSharpPlus;
using DSharpPlus.CommandsNext;
using Miunie.Core;
using Miunie.Core.Providers;
using Miunie.Discord.CommandModules;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Miunie.Discord.Embeds;
using System;
using System.Threading.Tasks;
using System.Linq;
using Miunie.Core.Discord;
using System.Collections.Generic;
using Miunie.Core.Logging;
using DSharpPlus.EventArgs;

namespace Miunie.Discord
{
    public class DSharpPlusDiscord : IDiscord, IDiscordMessages, IDiscordGuilds
    {
        private DiscordClient _discordClient;
        private CommandsNextExtension _commandService;
        private readonly IServiceProvider _services;
        private readonly IBotConfiguration _botConfiguration;
        private readonly EntityConvertor _entityConvertor;
        private readonly ILanguageProvider _lang;
        private readonly ILogger _logger;

        public DSharpPlusDiscord(IBotConfiguration botConfiguration, EntityConvertor entityConvertor, IServiceProvider services, ILanguageProvider lang, ILogger logger)
        {
            _botConfiguration = botConfiguration;
            _services = services;
            _entityConvertor = entityConvertor;
            _lang = lang;
            _logger = logger;
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
            var discordLogger = _discordClient.DebugLogger;
            discordLogger.LogMessageReceived += Log;
            await _discordClient.ConnectAsync();
        }

        private void Log(object sender, DebugLogMessageEventArgs e)
        {
            if (e.Level == LogLevel.Critical)
            {
                _logger.LogError(e.Message);
            }
            else
            {
                _logger.Log(e.Message);
            }
        }

        private void InitializeCommandService()
        {
            var config = GetDefaultCommandsNextConfiguration();
            _commandService = _discordClient.UseCommandsNext(config);
            _commandService.RegisterCommands<ProfileCommand>();
            _commandService.RegisterCommands<RemoteRepositoryCommand>();
            _commandService.RegisterCommands<DirectoryCommand>();
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
                UseInternalLogHandler = false
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

        public async Task SendMessageAsync(MiunieChannel mc, PhraseKey phraseKey, params object[] parameters)
        {
            var channel = await _discordClient.GetChannelAsync(mc.ChannelId);
            var msg = _lang.GetPhrase(phraseKey.ToString(), parameters);
            await channel.SendMessageAsync(msg);
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieUser mu)
        {
            var channel = await _discordClient.GetChannelAsync(mc.ChannelId);
            await channel.SendMessageAsync(embed: mu.ToEmbed(_lang));
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieGuild mg)
        {
            var channel = await _discordClient.GetChannelAsync(mc.ChannelId);
            await channel.SendMessageAsync(embed: mg.ToEmbed(_lang));
        }

        public async Task SendMessageAsync(MiunieChannel mc, DirectoryListing dl)
        {
            var channel = await _discordClient.GetChannelAsync(mc.ChannelId);
            var result = string.Join("\n", dl.Result.Select(s => $":file_folder: {s}"));
            await channel.SendMessageAsync(result);
        }

        public async Task<MiunieGuild> FromAsync(MiunieUser user)
        {
            var guild = await _discordClient.GetGuildAsync(user.GuildId);

            return _entityConvertor.ConvertGuild(guild);
        }
    }
}
