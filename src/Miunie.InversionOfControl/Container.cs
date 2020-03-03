using Microsoft.Extensions.DependencyInjection;
using Miunie.Core;
using Miunie.Core.Attributes;
using Miunie.Core.Configuration;
using Miunie.Core.Discord;
using Miunie.Core.Providers;
using Miunie.Core.Storage;
using Miunie.Discord;
using Miunie.Discord.Adapters;
using Miunie.Discord.Convertors;
using Miunie.Discord.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace Miunie.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddMiunieTypes(this IServiceCollection collection)
            => collection.AddSingleton<EntityConvertor>()
                .AddScoped<ILanguageProvider, LanguageProvider>()
                .AddSingleton<IDiscord, MiunieDiscordClient>()
                .AddSingleton<IMiunieDiscord, MiunieDiscord>()
                .AddScoped<IDiscordMessages, DiscordMessagesAdapter>()
                .AddScoped<IDiscordGuilds, DiscordGuildsAdapter>()
                .AddSingleton<IDiscordImpersonation, Impersonation>()
                .AddSingleton<DiscordLogger>()
                .AddSingleton<IBotConfiguration, BotConfiguration>()
                .AddSingleton<IPersistentStorage, LiteDbStorage.PersistentStorage>()
                .AddSingleton<Random>()
                .AddSingleton<IMiunieUserProvider, MiunieUserProvider>()
                .AddScoped<IUserReputationProvider, UserReputationProvider>()
                .AddSingleton<ITimeManipulationProvider, TimeManipulationProvider>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<MiunieUserConverter>()
                .AddMiunieServices();

        private static IServiceCollection AddMiunieServices(this IServiceCollection collection)
        {
            var serviceAttributeType = typeof(ServiceAttribute);
            var serviceTypes = Assembly.GetAssembly(serviceAttributeType)
                                       .GetTypes()
                                       .Where(x => x.GetCustomAttributes(serviceAttributeType, true).Length > 0).ToList();

            serviceTypes.ForEach((x) => {
                var attribute = x.GetCustomAttribute(serviceAttributeType) as ServiceAttribute;
                switch (attribute.ServiceType)
                {
                    case ServiceType.SCOPED:
                        collection.AddScoped(x);
                        break;
                    case ServiceType.SINGLETON:
                        collection.AddSingleton(x);
                        break;
                    case ServiceType.TRANSIENT:
                        collection.AddTransient(x);
                        break;
                    default:
                        break;
                }
            });

            return collection;
        }
    }
}
