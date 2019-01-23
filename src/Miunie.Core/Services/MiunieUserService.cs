using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class MiunieUserService
    {
        private const string KeyFormat = "u{0}";
        private const string CollectionKey = "Users";
        private readonly IDataStorage _dataStorage;

        public MiunieUserService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public MiunieUser GetById(ulong userId)
        {
            var user = _dataStorage
                .RestoreObject<MiunieUser>(CollectionKey, GetKeyById(userId));

            return EnsureExistence(user, userId);
        }

        public MiunieUser EnsureExistence(MiunieUser user, ulong userId)
        {
            if(user is null)
            {
                user = new MiunieUser{ Id = userId };
                StoreUser(user);
            }

           return user;
        }

        public void StoreUser(MiunieUser u)
            => _dataStorage.StoreObject(u, CollectionKey, GetKeyById(u.Id));

        private string GetKeyById(ulong userId)
            => string.Format(KeyFormat, userId);
    }
}

