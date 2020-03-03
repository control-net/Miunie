using Miunie.Core.Discord;
using Miunie.Core.Infrastructure;
using Miunie.Core.Providers;
using System;
using System.Threading.Tasks;

namespace Miunie.Core
{
    [Service]
    public class TimeService
    {
        private readonly IDiscordMessages _messages;
        private readonly IDateTime _dateTime;
        private readonly IMiunieUserProvider _users;
        private readonly ITimeManipulationProvider _timeManipulator;

        public TimeService(IDiscordMessages messages, IDateTime dateTime, IMiunieUserProvider users, ITimeManipulationProvider timeManipulator)
        {
            _messages = messages;
            _dateTime = dateTime;
            _users = users;
            _timeManipulator = timeManipulator;
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

        public async Task OutputCurrentTimeComparedToInputForUserAsync(MiunieUser requestUser, DateTime requestTime, string verb, MiunieUser user, MiunieChannel channel)
        {
            if (verb.Trim().ToLower() != "for")
            {
                await _messages.SendMessageAsync(channel, PhraseKey.INCORRECT_VERB, verb);
                return;
            }

            var requesterOffset = requestUser.UtcTimeOffset ?? new TimeSpan();
            var otherUserOffSet = user.UtcTimeOffset ?? new TimeSpan();

            var requestUtcTime = requestTime - requesterOffset;
            var otherUserTime = requestUtcTime + otherUserOffSet;

            await _messages.SendMessageAsync(channel, PhraseKey.TIME_USERTIME_FROM_LOCAL, requestTime, user.Name, otherUserTime);
        }

        public async Task OutputFutureTimeForUserAsync(MiunieUser user, string verb, int units, string timeframe, MiunieChannel channel)
        {
            if (verb.Trim().ToLower() != "in")
            {
                await _messages.SendMessageAsync(channel, PhraseKey.INCORRECT_VERB, verb);
                return;
            }

            var timeFromLocal = _timeManipulator.GetTimeSpanFromString(timeframe, units);           

            if (timeFromLocal is null)
            {
                await _messages.SendMessageAsync(channel, PhraseKey.TIME_USERTIME_IN_FUTURE_UNPARSABLE, units.ToString(), timeframe);
                return;
            }

            var usersOffset = user.UtcTimeOffset ?? new TimeSpan();
            var usersLocalTime = _dateTime.UtcNow + usersOffset;
            var usersFutureTime = usersLocalTime + timeFromLocal;

            await _messages.SendMessageAsync(channel, PhraseKey.TIME_USERTIME_IN_FUTURE, user.Name, usersFutureTime, units.ToString(), timeframe);
        }

        public async Task OutputMessageTimeAsLocalAsync(ulong messageId, DateTimeOffset? createdTimeOffset, DateTimeOffset? editTimeOffset, MiunieUser user, MiunieChannel channel)
        {
            if (createdTimeOffset is null)
            {
                await _messages.SendMessageAsync(channel, PhraseKey.TIME_NO_MESSAGE, messageId.ToString());
                return;
            }

            var messageCreated = _timeManipulator.GetDateTimeLocalToUser(createdTimeOffset?.UtcDateTime, user);
            var messageEdited = _timeManipulator.GetDateTimeLocalToUser(editTimeOffset?.UtcDateTime, user);

            if (messageEdited.HasValue)
            {
                await _messages.SendMessageAsync(channel, PhraseKey.TIME_MESSAGE_INFO_EDIT, messageId.ToString(), messageCreated, messageEdited);
                return;
            }

            await _messages.SendMessageAsync(channel, PhraseKey.TIME_MESSAGE_INFO_NO_EDIT, messageId.ToString(), messageCreated);
        }

        public async Task SetUtcOffsetForUserAsync(DateTime userTime, MiunieUser user, MiunieChannel channel)
        {
            var offset = TimeSpan.FromHours(userTime.Hour - _dateTime.UtcNow.Hour);
            user.UtcTimeOffset = offset;
            _users.StoreUser(user);
            await _messages.SendMessageAsync(channel, PhraseKey.TIME_NEW_OFFSET_SET);
        }

        public async Task SetUtcOffsetForUserByAdminAsync(DateTime userTime, MiunieUser user, MiunieChannel channel)
        {
            var offset = TimeSpan.FromHours(userTime.Hour - _dateTime.UtcNow.Hour);
            user.UtcTimeOffset = offset;
            _users.StoreUser(user);
            await _messages.SendMessageAsync(channel, PhraseKey.TIME_NEW_OFFSET_SET_ADMIN, user.Name);
        }
    }
}
