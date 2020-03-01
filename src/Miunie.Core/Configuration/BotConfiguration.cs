namespace Miunie.Core.Configuration
{
    public class BotConfiguration : IBotConfiguration
    {
        public string DiscordToken { get; set; }
        public bool CommandsEnabled { get; set; } = true;
    }
}
