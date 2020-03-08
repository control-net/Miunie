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
using Miunie.Core.Providers;

namespace Miunie.Discord.Convertors
{
    public class EntityConvertor
    {
        public EntityConvertor(IMiunieUserProvider miunieUserProvider)
        {
            ChannelConvertor = new MiunieChannelConvertor();
            UserConvertor = new MiunieUserConverter(miunieUserProvider);
            GuildConvertor = new MiunieGuildConvertor();
        }

        public MiunieChannelConvertor ChannelConvertor { get; }

        public MiunieUserConverter UserConvertor { get; }

        public MiunieGuildConvertor GuildConvertor { get; }

        public MiunieUser ConvertUser(SocketGuildUser m)
            => UserConvertor.DiscordMemberToMiunieUser(m);

        public MiunieChannel ConvertChannel(SocketGuildChannel c)
            => MiunieChannelConvertor.FromDiscordChannel(c);

        internal MiunieGuild ConvertGuild(SocketGuild g)
            => GuildConvertor.DiscordGuildToMiunieGuild(g);
    }
}
