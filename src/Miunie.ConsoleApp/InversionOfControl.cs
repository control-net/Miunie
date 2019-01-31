using Miunie.Configuration;
using Miunie.Core;
using Miunie.Core.Storage;
using Miunie.Storage;
using Miunie.Discord;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Miunie.ConsoleApp
{
    public static class InversionOfControl
    {
        private static ServiceProvider provider;

        public static ServiceProvider Provider
        {
            get
            {
                return GetOrInitProvider();
            }
        }

        private static ServiceProvider GetOrInitProvider()
        {
            if(provider is null)
            {
                InitializeProvider();
            }
            
            return provider;
        }

        private static void InitializeProvider()
            => provider = new ServiceCollection()
                .AddSingleton<ConfigurationFileEditor>()
                .AddSingleton<EntityConvertor>() // TODO (Peter): Depricate
                .AddSingleton<MiunieUserService>()
                .AddSingleton<ProfileService>()
                .AddSingleton<ILanguageResources, LanguageResources>()
                .AddSingleton<DSharpPlusDiscord>()
                .AddSingleton<IDiscord>(s =>                
                    s.GetRequiredService<DSharpPlusDiscord>())
                .AddSingleton<IDiscordMessages>(s =>
                    s.GetRequiredService<DSharpPlusDiscord>())
                .AddSingleton<IBotConfiguration, BotConfiguration>()
                .AddSingleton<IConfiguration, ConfigManager>()
                .AddSingleton<IDataStorage, JsonDataStorage>()
                .AddSingleton<Random>()
                .BuildServiceProvider();
    }
}

