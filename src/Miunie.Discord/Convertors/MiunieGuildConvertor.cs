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

using Discord.WebSocket;
using Miunie.Core.Entities.Discord;
using System;
using System.Linq;

namespace Miunie.Discord.Convertors
{
    public class MiunieGuildConvertor
    {
        public MiunieGuild DiscordGuildToMiunieGuild(SocketGuild g)
            => g != null ? new MiunieGuild
            {
                Id = g.Id,
                Name = g.Name,
                MemberCount = g.MemberCount,
                ChannelNames = g.Channels.Select(x => x.Name),
                TextChannelCount = g.Channels.Count(x => x is SocketTextChannel),
                VoiceChannelCount = g.Channels.Count(x => x is SocketVoiceChannel),
                CreationDate = g.CreatedAt.UtcDateTime,
                Roles = g.Roles.Select(r => r.DiscordRoleToMiunieRole()),
                IconUrl = g.IconUrl
            }
            : throw new ArgumentNullException(nameof(g));
    }
}
