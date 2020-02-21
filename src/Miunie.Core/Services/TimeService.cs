using System;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class TimeService
    {
        private IDiscordMessages _messages;

        public TimeService(IDiscordMessages messages)
        {
            _messages = messages;
        }

        public async Task OutputCurrentTimeForUserAsync(MiunieUser user, MiunieChannel channel)
        {
            if (!user.UtcTimeOffset.HasValue)
                await _messages.SendMessageAsync(channel, PhraseKey.TIME_NO_TIMEZONE_INFO, user.Name);
        }
    }
}
