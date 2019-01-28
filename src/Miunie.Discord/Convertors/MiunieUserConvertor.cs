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
        private readonly MiunieUserService _userService;

        public MiunieUserConvertor(MiunieUserService userService)
        {
            _dmConverter = new DiscordMemberConverter();
            _userService = userService;
        }

        public async Task<Optional<MiunieUser>> ConvertAsync(
                                                    string userInput,
                                                    CommandContext context)
        {
            var result = await _dmConverter.ConvertAsync(userInput, context);
            return DiscordMemberToMiunieUser(result.Value);
        }

        public MiunieUser DiscordMemberToMiunieUser(DiscordMember user)
        {
            MiunieUser miunieUser;
            if (user is default(DiscordMember))
            {
                miunieUser = default(MiunieUser);
            }
            else
            {
                miunieUser = _userService.GetById(user.Id);
            }
            return miunieUser;
        }
    }
}