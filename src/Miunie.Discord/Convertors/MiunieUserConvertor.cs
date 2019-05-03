using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using Miunie.Core;
using Miunie.Core.Providers;
using System.Linq;
using System.Threading.Tasks;

namespace Miunie.Discord.Convertors
{
    public class MiunieUserConverter : IArgumentConverter<MiunieUser>
    {
        private readonly DiscordMemberConverter _dmConverter;
        private readonly IMiunieUserProvider _userProvider;

        public MiunieUserConverter(IMiunieUserProvider userProvider)
        {
            _dmConverter = new DiscordMemberConverter();
            _userProvider = userProvider;
        }

        public async Task<Optional<MiunieUser>> ConvertAsync(string userInput, CommandContext context)
        {
            var result = await _dmConverter.ConvertAsync(userInput, context);
            return DiscordMemberToMiunieUser(result.Value);
        }

        public MiunieUser DiscordMemberToMiunieUser(DiscordMember user)
        {
            var mUser = _userProvider.GetById(user.Id, user.Guild.Id);
            mUser.Name = user.Nickname ?? user.Username;
            mUser.AvatarUrl = user.AvatarUrl;
            mUser.JoinedAt = user.JoinedAt.UtcDateTime;
            mUser.CreatedAt = user.CreationTimestamp.UtcDateTime;
            mUser.IsBot = user.IsBot;
            mUser.Roles = user.Roles.Select(r => r.DiscordRoleToMiunieRole());
            _userProvider.StoreUser(mUser);
            return mUser;
        }
    }
}
