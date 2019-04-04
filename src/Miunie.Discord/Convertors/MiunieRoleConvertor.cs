using DSharpPlus.Entities;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    public static class MiunieRoleConvertor
    {
        public static MiunieRole DiscordRoleToMiunieRole(this DiscordRole role)
            => new MiunieRole
            {
                Id = role.Id,
                Name = role.Name
            };
    }
}
