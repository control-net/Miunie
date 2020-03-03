using Miunie.Core.Entities.Discord;
using Miunie.Core.Storage;
using System.Collections.Generic;

namespace Miunie.Core.Providers
{
    public class MiunieUserProvider : IMiunieUserProvider
    {
        private const string KeyFormat = "u{0}";
        private const string CollectionFormat = "g{0}";
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

        private static string GetKeyById(ulong userId)
            => string.Format(KeyFormat, userId);

        private static string GetCollectionById(ulong guildId)
            => string.Format(CollectionFormat, guildId);
    }
}

