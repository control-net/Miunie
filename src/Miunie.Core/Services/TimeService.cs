using Miunie.Core.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class TimeService
    {
        private readonly IDiscordMessages _messages;
        private readonly IDateTime _dateTime;

        public TimeService(IDiscordMessages messages, IDateTime dateTime)
        {
            _messages = messages;
            _dateTime = dateTime;
        }

        public async Task OutputCurrentTimeForUserAsync(MiunieUser user, MiunieChannel channel)
        {
            if (!user.UtcTimeOffset.HasValue)
            {
                await _messages.SendMessageAsync(channel, PhraseKey.TIME_NO_TIMEZONE_INFO, user.Name);
                return;
            }

            var targetDateTime = _dateTime.UtcNow + user.UtcTimeOffset.Value;
            await _messages.SendMessageAsync(channel, PhraseKey.TIME_TIMEZONE_INFO, user.Name, targetDateTime);
        }
    }
}
