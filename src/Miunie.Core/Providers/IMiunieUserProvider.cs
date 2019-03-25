namespace Miunie.Core.Providers
{
    public interface IMiunieUserProvider
    {
        MiunieUser GetById(ulong userId, ulong guildId);

        void StoreUser(MiunieUser u);
    }
}
