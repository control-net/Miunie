using Miunie.Core;

namespace Miunie.Discord
{
    public class MiunieDiscord : IDiscord
    {
        private DSharpClient _client;
        private CommandHandler _commandHandler;

        public MiunieDiscord(DSharpClient client, CommandHandler commandHandler)
        {
            _client = client;
            _commandHandler = commandHandler;
        }
    }
}