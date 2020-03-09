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

using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Storage;
using System.Collections.Generic;

namespace Miunie.Core.Providers
{
    public class MiunieUserProvider : IMiunieUserProvider
    {
        private readonly IPersistentStorage _persistentStorage;

        public MiunieUserProvider(IPersistentStorage persistentStorage)
        {
            _persistentStorage = persistentStorage;
        }

        public MiunieUser GetById(ulong userId, ulong guildId)
        {
            var user = _persistentStorage.RestoreSingle<MiunieUser>(u => u.UserId == userId && u.GuildId == guildId);
            return EnsureExists(user, userId, guildId);
        }

        public void StoreUser(MiunieUser user)
        {
            if (_persistentStorage.Exists<MiunieUser>(u => u.UserId == user.UserId && u.GuildId == user.GuildId))
            {
                _persistentStorage.Update(user);
            }
            else
            {
                _persistentStorage.Store(user);
            }
        }

        public IEnumerable<MiunieUser> GetAllUsers()
            => _persistentStorage.RestoreAll<MiunieUser>();

        private MiunieUser EnsureExists(
            MiunieUser user,
            ulong userId,
            ulong guildId)
        {
            if (user is null)
            {
                user = new MiunieUser
                {
                    GuildId = guildId,
                    UserId = userId,
                    Reputation = new Reputation()
                };
                StoreUser(user);
            }

            return user;
        }
    }
}
