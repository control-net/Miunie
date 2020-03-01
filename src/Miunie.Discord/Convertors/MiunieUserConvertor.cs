using Discord.WebSocket;
using Miunie.Core;
using Miunie.Core.Providers;
using System;
using System.Linq;

namespace Miunie.Discord.Convertors
{
    public class MiunieUserConverter
    {
        private readonly IMiunieUserProvider _userProvider;

        public MiunieUserConverter(IMiunieUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public MiunieUser DiscordMemberToMiunieUser(SocketGuildUser user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            var mUser = _userProvider.GetById(user.Id, user.Guild.Id);
            mUser.Name = user.Nickname ?? user.Username;
            mUser.AvatarUrl = user.GetAvatarUrl();
            mUser.JoinedAt = user.JoinedAt?.UtcDateTime ?? default;
            mUser.CreatedAt = user.CreatedAt.UtcDateTime;
            mUser.IsBot = user.IsBot;
            mUser.Roles = user.Roles.Select(r => r.DiscordRoleToMiunieRole());
            _userProvider.StoreUser(mUser);
            return mUser;
        }
    }
}
