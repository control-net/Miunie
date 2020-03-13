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
using Miunie.Discord.Attributes;
using Miunie.Discord.Convertors;
using System;
using System.Threading.Tasks;

namespace Miunie.Discord.CommandModules
{
    [Name("Time")]
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
        [Summary("Gets the local time for the specified user.")]
        [Examples("time for @Miunie")]
        public async Task ShowTimeForUser(MiunieUser user)
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputCurrentTimeForUserAsync(user, c);
        }

        [Command("time for")]
        [Summary("Gets the local time offset by a specified amount for a user.")]
        [Examples("time for @Miunie in 4 hours", "time for @Peter in 2 minutes", "time for @Draxis in 1 second")]
        public async Task ShowTimeForUserWithOffset(MiunieUser user, string verb, int units, string timeframe)
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputFutureTimeForUserAsync(user, verb, units, timeframe, c);
        }

        [Command("time get")]
        [Summary("Compares a user's time with your time.")]
        [Examples("time get 03:30 for @Miunie")]
        public async Task ShowTimeForUserComparedToCurrentUser(DateTime requestTime, string verb, MiunieUser user)
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.OutputCurrentTimeComparedToInputForUserAsync(u, requestTime, verb, user, c);
        }

        [Command("time of")]
        [Summary("Shows your local time for the specified message.")]
        [Examples("time of 647141579345100840")]
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
        [Summary("Set your current time offset.")]
        [Examples("time set 16:17")]
        public async Task SetMyTimeOffset(DateTime currentTime)
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.SetUtcOffsetForUserAsync(currentTime, u, c);
        }

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("time set for")]
        [Summary("Set a user's time offset.")]
        [Examples("time set for @Draxis 00:00")]
        public async Task SetMyTimeOffset(MiunieUser user, DateTime currentTime)
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.SetUtcOffsetForUserByAdminAsync(currentTime, user, c);
        }
    }
}
