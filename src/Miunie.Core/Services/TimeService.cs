using Miunie.Core.Infrastructure;
using Miunie.Core.Providers;
using System;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class TimeService
    {
        private readonly IDiscordMessages _messages;
        private readonly IDateTime _dateTime;
        private readonly IMiunieUserProvider _users;

        public TimeService(IDiscordMessages messages, IDateTime dateTime, IMiunieUserProvider users)
        {
            _messages = messages;
            _dateTime = dateTime;
            _users = users;
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

        public async Task OutputCurrentTimeComparedToInputForUserAsync(MiunieUser requestUser, DateTime requestTime, MiunieUser user, MiunieChannel channel)
        {
            var requesterOffset = requestUser.UtcTimeOffset ?? new TimeSpan();
            var otherUserOffSet = user.UtcTimeOffset ?? new TimeSpan();

            var requestUtcTime = requestTime - requesterOffset;
            var otherUserTime = requestUtcTime + otherUserOffSet;

            var formattedRequestTIme = requestTime.ToShortTimeString();
            var formattedOtherUserTime = otherUserTime.ToShortTimeString();

            await _messages.SendMessageAsync(channel, PhraseKey.TIME_USERTIME_FROM_LOCAL, formattedRequestTIme, user.Name, formattedOtherUserTime);
        }

        public async Task SetUtcOffsetForUserAsync(DateTime userTime, MiunieUser user, MiunieChannel channel)
        {
            var offset = TimeSpan.FromHours(userTime.Hour - _dateTime.UtcNow.Hour);
            user.UtcTimeOffset = offset;
            _users.StoreUser(user);
            await _messages.SendMessageAsync(channel, PhraseKey.TIME_NEW_OFFSET_SET);
        }
    }
}
