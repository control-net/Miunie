using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class MiunieUserService
    {
        private const string UserStorageKeyTemplate = "u{0}";
        private const string UserStorageCollection = "Users";
        private readonly IDataStorage _dataStorage;

        public MiunieUserService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public MiunieUser GetById(ulong userId)
        {
            var user = _dataStorage.RestoreObject<MiunieUser>(UserStorageCollection, GetKeyById(userId));
            if(user is null)
            {
                user = new MiunieUser{ Id = userId };
                StoreUser(user);
            }

            return user;
        }

        public void StoreUser(MiunieUser user)
            => _dataStorage.StoreObject(user, UserStorageCollection, GetKeyById(user.Id));

        private string GetKeyById(ulong userId)
            => string.Format(UserStorageKeyTemplate, userId);
    }
}
