// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Microsoft.Extensions.DependencyInjection;
using Miunie.Core.Attributes;
using Miunie.Core.Configuration;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
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
                .AddSingleton<IDiscordConnection, MiunieDiscord>()
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

            serviceTypes.ForEach((x) =>
            {
                var attribute = x.GetCustomAttribute(serviceAttributeType) as ServiceAttribute;
                switch (attribute.ServiceType)
                {
                    case ServiceType.SCOPED:
                        _ = collection.AddScoped(x);
                        break;
                    case ServiceType.SINGLETON:
                        _ = collection.AddSingleton(x);
                        break;
                    case ServiceType.TRANSIENT:
                        _ = collection.AddTransient(x);
                        break;
                    default:
                        break;
                }
            });

            return collection;
        }
    }
}
