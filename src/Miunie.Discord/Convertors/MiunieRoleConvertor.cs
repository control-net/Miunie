using Discord.WebSocket;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    public static class MiunieRoleConvertor
    {
        public static MiunieRole DiscordRoleToMiunieRole(this SocketRole role)
            => new MiunieRole
            {
                Id = role.Id,
                Name = role.Name
            };
    }
}
