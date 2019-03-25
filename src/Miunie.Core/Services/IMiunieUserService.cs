namespace Miunie.Core
{
    public interface IMiunieUserService
    {
        MiunieUser GetById(ulong userId, ulong guildId);

        void StoreUser(MiunieUser u);
    }
}
