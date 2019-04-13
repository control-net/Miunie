using System;
using Microsoft.Extensions.DependencyInjection;
using Miunie.Configuration;
using Miunie.Core;
using Miunie.Core.Discord;
using Miunie.Core.Providers;
using Miunie.Core.Services;
using Miunie.Core.Storage;
using Miunie.Discord;
using Miunie.Discord.Adapters;
using Miunie.Discord.Configuration;
using Miunie.Discord.Convertors;
using Miunie.Discord.Logging;
using Miunie.Storage;

namespace Miunie.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddMiunieTypes(this IServiceCollection collection)
            => collection.AddSingleton<EntityConvertor>()
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
                .AddScoped<IListDirectoryProvider, ListDirectoryProvider>()
                .AddSingleton<RemoteRepositoryService>()
                .AddScoped<DirectoryService>();
    }
}
