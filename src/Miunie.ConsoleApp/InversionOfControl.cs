using Lamar;
using Miunie.Configuration;
using Miunie.Core;
using Miunie.Core.Storage;
using Miunie.Storage;
using Miunie.Discord;
using Miunie.Discord.Configuration;

namespace Miunie.ConsoleApp
{
    public static class InversionOfControl
    {
        private static Container container;

        public static Container Container
        {
            get
            {
                return GetOrInitContainer();
            }
        }

        private static Container GetOrInitContainer()
        {
            if(container is null)
            {
                InitializeContainer();
            }

            return container;
        }

        public static void InitializeContainer()
        {
            container = new Container(c =>
            {
                c.ForSingletonOf<IDiscord>().UseIfNone<DSharpPlusDiscord>();
                c.ForSingletonOf<IDiscordMessages>().UseIfNone<DSharpPlusDiscord>();
                c.ForSingletonOf<IBotConfiguration>().UseIfNone<BotConfiguration>();
                c.ForSingletonOf<IConfiguration>().UseIfNone<ConfigManager>();
                c.ForSingletonOf<IDataStorage>().UseIfNone<JsonDataStorage>();
            });
        }
    }
}
