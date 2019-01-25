using System;
using Miunie.Configuration;
using Miunie.Core;
using Miunie.Core.Storage;
using Miunie.Storage;
using Miunie.Discord;
using Miunie.Discord.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddSingleton<IDiscord, DSharpPlusDiscord>()
                .AddSingleton<IDiscordMessages, DSharpPlusDiscord>()
                .AddSingleton<IBotConfiguration, BotConfiguration>()
                .AddSingleton<IConfiguration, ConfigManager>()
                .AddSingleton<IDataStorage, JsonDataStorage>()
                .BuildServiceProvider();
    }
}

