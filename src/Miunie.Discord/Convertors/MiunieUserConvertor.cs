using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using Miunie.Core;
using Miunie.Core.Providers;

namespace Miunie.Discord.Convertors
{
    public class MiunieUserConvertor : IArgumentConverter<MiunieUser>
    {
        private readonly DiscordMemberConverter _dmConverter;
        private readonly IMiunieUserProvider _userProvider;

        public MiunieUserConvertor(IMiunieUserProvider userProvider)
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
            _userProvider.StoreUser(mUser);
            return mUser;
        }
    }
}
