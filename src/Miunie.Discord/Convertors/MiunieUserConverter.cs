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
using System;
using System.Linq;

namespace Miunie.Discord.Convertors
{
    public class MiunieUserConverter
    {
        private readonly IMiunieUserProvider _userProvider;

        public MiunieUserConverter(IMiunieUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public MiunieUser DiscordMemberToMiunieUser(SocketGuildUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var mUser = _userProvider.GetById(user.Id, user.Guild.Id);
            mUser.Name = user.Nickname ?? user.Username;
            mUser.AvatarUrl = user.GetAvatarUrl();
            mUser.JoinedAt = user.JoinedAt?.UtcDateTime ?? default;
            mUser.CreatedAt = user.CreatedAt.UtcDateTime;
            mUser.IsBot = user.IsBot;
            mUser.Roles = user.Roles.Select(r => r.DiscordRoleToMiunieRole());
            _userProvider.StoreUser(mUser);
            return mUser;
        }
    }
}
