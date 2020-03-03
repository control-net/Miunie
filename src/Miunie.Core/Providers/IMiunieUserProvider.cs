using Miunie.Core.Entities.Discord;
using System.Collections.Generic;

namespace Miunie.Core.Providers
{
    public interface IMiunieUserProvider
    {
        MiunieUser GetById(ulong userId, ulong guildId);

        void StoreUser(MiunieUser u);
        IEnumerable<MiunieUser> GetAllUsers();
    }
}
