using Miunie.Configuration;
using Miunie.Core;
using Miunie.Core.Storage;
using Miunie.Core.Language;
using Miunie.Storage;
using Miunie.Discord;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Microsoft.Extensions.DependencyInjection;
using System;
using Miunie.Core.Providers;

namespace Miunie.ConsoleApp
{
    public static class InversionOfControl
    {
        private static ServiceProvider _provider;

        public static ServiceProvider Provider => GetOrInitProvider();

        private static ServiceProvider GetOrInitProvider()
        {
            if(_provider is null)
            {
                InitializeProvider();
            }

            return _provider;
        }

        private static void InitializeProvider()
            => _provider = new ServiceCollection()
                .AddSingleton<EntityConvertor>()
                .AddSingleton<ProfileService>()
                .AddTransient<ILanguageResources, LanguageResources>()
                .AddSingleton<DSharpPlusDiscord>()
                .AddSingleton<IDiscord>(s =>
                    s.GetRequiredService<DSharpPlusDiscord>())
                .AddSingleton<IDiscordMessages>(s =>
                    s.GetRequiredService<DSharpPlusDiscord>())
                .AddSingleton<IBotConfiguration, BotConfiguration>()
                .AddSingleton<IConfiguration, ConfigManager>()
                .AddSingleton<IDataStorage, JsonDataStorage>()
                .AddSingleton<Random>()
                .AddSingleton<IMiunieUserProvider, MiunieUserProvider>()
                .AddTransient<IUserReputationProvider, UserReputationProvider>()
                .BuildServiceProvider();
    }
}
