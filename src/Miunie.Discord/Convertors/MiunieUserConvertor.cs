using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    public class MiunieUserConvertor : IArgumentConverter<MiunieUser>
    {
        private readonly DiscordMemberConverter _dmConverter;
        private readonly IMiunieUserService _userService;

        public MiunieUserConvertor(IMiunieUserService userService)
        {
            _dmConverter = new DiscordMemberConverter();
            _userService = userService;
        }

        public async Task<Optional<MiunieUser>> ConvertAsync(string userInput, CommandContext context)
        {
            var result = await _dmConverter.ConvertAsync(userInput, context);
            return DiscordMemberToMiunieUser(result.Value);
        }

        public MiunieUser DiscordMemberToMiunieUser(DiscordMember user)
        {
            var mUser = _userService.GetById(user.Id, user.Guild.Id);
            mUser.Name = user.Nickname ?? user.Username;
            _userService.StoreUser(mUser);
            return mUser;
        }
    }
}
