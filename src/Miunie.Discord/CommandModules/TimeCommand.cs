// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Core.Entities.Discord;
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
            await _service.OutputFutureTimeForUserAsync(user, verb, units, timeframe, c);
        }

        [Command("time get")]
        public async Task ShowTimeForUserComparedToCurrentUser(DateTime requestTime, string verb, MiunieUser user)
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputCurrentTimeComparedToInputForUserAsync(u, requestTime, verb, user, c);
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

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("time set for")]
        public async Task SetMyTimeOffset(MiunieUser user, DateTime currentTime)
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.SetUtcOffsetForUserByAdminAsync(currentTime, user, c);
        }
    }
}
