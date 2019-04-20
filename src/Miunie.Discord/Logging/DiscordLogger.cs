using DSharpPlus;
using DSharpPlus.EventArgs;
using Miunie.Core.Logging;

namespace Miunie.Discord.Logging
{
    public class DiscordLogger
    {
        private readonly ILogWriter _logger;

        public DiscordLogger(ILogWriter logger)
        {
            _logger = logger;
        }

        internal void Log(object sender, DebugLogMessageEventArgs e)
        {
            if (e.Level == LogLevel.Critical)
            {
                _logger.LogError(e.Message);
            }
            else
            {
                _logger.Log(e.Message);
            }
        }
    }
}
