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
using Miunie.Discord.Convertors;
using System;
using System.Threading.Tasks;

namespace Miunie.Discord.TypeReaders
{
    public class MiunieUserTypeReader : TypeReader
    {
        private readonly EntityConvertor _convertor;

        public MiunieUserTypeReader(EntityConvertor convertor)
        {
            _convertor = convertor;
        }

        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var discordUserId = MentionUtils.TryParseUser(input, out var userId);

            if (await context.Guild.GetUserAsync(userId) is SocketGuildUser discordUser)
            {
                var miunieUserResult = _convertor.UserConvertor.DiscordMemberToMiunieUser(discordUser);
                return TypeReaderResult.FromSuccess(miunieUserResult);
            }

            return TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as a MiunieUser.");
        }
    }
}
