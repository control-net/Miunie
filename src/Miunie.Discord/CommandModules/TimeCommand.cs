using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Convertors;
using System;
using System.Threading.Tasks;

namespace Miunie.Discord.CommandModules
{
    public class TimeCommand : ModuleBase<SocketCommandContext>
    {
        private readonly TimeService _service;
        private readonly EntityConvertor _entityConvertor;

        public TimeCommand(TimeService service, EntityConvertor entityConvertor)
        {
            _service = service;
            _entityConvertor = entityConvertor;
        }

        [Command("time for")]
        public async Task ShowTimeForUser(MiunieUser user)
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputCurrentTimeForUserAsync(user, c);
        }

        [Command("time for")]
        public async Task ShowTimeForUserWithOffset(MiunieUser user, string verb, int units, string timeframe)
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputFutureTimeForUserAsync(user, units, timeframe, c);
        }

        [Command("time get")]
        public async Task ShowTimeForUserComparedToCurrentUser(DateTime requestTime, string verb, MiunieUser user)
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputCurrentTimeComparedToInputForUserAsync(u, requestTime, user, c);
        }

        [Command("time of")]
        public async Task ShowTimeForMessage(ulong messageId)
        {
            var m = await Context.Channel.GetMessageAsync(messageId);
            var ct = m?.CreatedAt;
            var et = m?.EditedTimestamp;
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);            
            await _service.OutputMessageTimeAsLocalAsync(messageId, ct, et, u, c);
        }

        [Command("time set")]
        public async Task SetMyTimeOffset(DateTime currentTime)
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.SetUtcOffsetForUserAsync(currentTime, u, c);
        }
    }
}
