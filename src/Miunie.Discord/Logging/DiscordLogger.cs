using Discord;
using Miunie.Core.Logging;
using System.Threading.Tasks;

namespace Miunie.Discord.Logging
{
    public class DiscordLogger
    {
        private readonly ILogWriter _logger;

        public DiscordLogger(ILogWriter logger)
        {
            _logger = logger;
        }

        internal Task Log(LogMessage evt)
        {
            if (evt.Severity == LogSeverity.Critical)
            {
                _logger.LogError(evt.Message);
            }
            else
            {
                _logger.Log(evt.Message);
            }

            return Task.CompletedTask;
        }
    }
}
