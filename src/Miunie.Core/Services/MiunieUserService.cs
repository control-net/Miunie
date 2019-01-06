using Miunie.Core.Storage;

namespace Miunie.Core
{
    public class MiunieUserService
    {
        private const string UserStorageKeyTemplate = "Users/u{0}";
        private readonly IDataStorage _dataStorage;

        public MiunieUserService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public MiunieUser GetById(ulong userId)
            => _dataStorage.RestoreObject<MiunieUser>(GetKeyById(userId));

        private string GetKeyById(ulong userId)
            => string.Format(UserStorageKeyTemplate, userId);
    }
}
