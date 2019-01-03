using Lamar;
using Miunie.Configuration;
using Miunie.Core;
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
                // c.ForSingletonOf<Logger>().UseIfNone<Logger>();
                // c.ForSingletonOf<TriviaGames>().UseIfNone<TriviaGames>();
                // c.ForSingletonOf<DiscordEventHandler>().UseIfNone<DiscordEventHandler>();
                // c.ForSingletonOf<CommandHandler>().UseIfNone<CommandHandler>();
                // c.ForSingletonOf<CommandService>().UseIfNone<CommandService>();
                // c.ForSingletonOf<DiscordSocketClient>().UseIfNone(DiscordClientFactory.GetBySettings(settings));
                // c.ForSingletonOf<ApplicationSettings>().UseIfNone(settings);
                // c.ForSingletonOf<IDataStorage>().UseIfNone<JsonDataStorage>();
                // c.ForSingletonOf<ListManager>().UseIfNone<ListManager>();
                // c.ForSingletonOf<IOnboarding>().UseIfNone<Onboarding>();
                // c.ForSingletonOf<Features.Onboarding.Tasks.HelloWorldTask>().UseIfNone<Features.Onboarding.Tasks.HelloWorldTask>();
                // c.ForSingletonOf<IGlobalUserAccountProvider>().UseIfNone<GlobalUserAccountProvider>();
                // c.ForSingletonOf<IDiscordSocketClient>().UseIfNone<DiscordSocketClientAbstraction>();
                // c.ForSingletonOf<IDailyMiunies>().UseIfNone<Daily>();
                // c.ForSingletonOf<IMiuniesTransfer>().UseIfNone<Transfer>();
                c.ForSingletonOf<IDiscord>().UseIfNone<DSharpPlusDiscord>();
                c.ForSingletonOf<IBotConfiguration>().UseIfNone<BotConfiguration>();
                c.ForSingletonOf<IConfiguration>().UseIfNone<ConfigManager>();
            });
        }
    }
}
