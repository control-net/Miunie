using Miunie.Configuration;
using Miunie.Core;
using Miunie.Core.Providers;
using Miunie.Core.Services;
using Miunie.Core.Storage;
using Miunie.Core.Infrastructure;
using Miunie.Storage;
using Miunie.Discord;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Microsoft.Extensions.DependencyInjection;
using System;
using Miunie.Core.Logging;
using Miunie.Logger;
using Miunie.Core.Discord;
using Miunie.Discord.Adapters;
using Miunie.Discord.Logging;

namespace Miunie.ConsoleApp
{
    public static class InversionOfControl
    {
        private static ServiceProvider _provider;

        public static ServiceProvider Provider => GetOrInitProvider();

        private static ServiceProvider GetOrInitProvider()
        {
            if (_provider is null)
            {
                InitializeProvider();
            }

            return _provider;
        }

        private static void InitializeProvider()
            => _provider = new ServiceCollection()
                .AddSingleton<EntityConvertor>()
                .AddSingleton<ProfileService>()
                .AddScoped<ILanguageProvider, LanguageProvider>()
                .AddSingleton<IDiscord, MiunieDiscordClient>()
                .AddSingleton<IMiunieDiscord, MiunieDiscord>()
                .AddScoped<IDiscordMessages, DiscordMessagesAdapter>()
                .AddScoped<IDiscordGuilds, DiscordGuildsAdapter>()
                .AddSingleton<DiscordLogger>()
                .AddScoped<CommandServiceFactory>()
                .AddSingleton<IBotConfiguration, BotConfiguration>()
                .AddSingleton<IConfiguration, ConfigManager>()
                .AddSingleton<IPersistentStorage, JsonPersistentStorage>()
                .AddSingleton<Random>()
                .AddSingleton<IMiunieUserProvider, MiunieUserProvider>()
                .AddScoped<IUserReputationProvider, UserReputationProvider>()
                .AddTransient<IDateTime, SystemDateTime>()
                .AddSingleton<ILogger, ConsoleLogger>()
                .AddScoped<IListDirectoryProvider, ListDirectoryProvider>()
                .AddSingleton<RemoteRepositoryService>()
                .AddScoped<DirectoryService>()
                .BuildServiceProvider();
    }
}
