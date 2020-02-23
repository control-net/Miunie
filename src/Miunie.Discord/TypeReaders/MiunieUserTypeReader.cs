using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.TypeReaders
{
    public class MiunieUserTypeReader : TypeReader
    {
        private readonly EntityConvertor _convertor;

        public MiunieUserTypeReader(EntityConvertor convertor)
        {
            _convertor = convertor;
        }

        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var discordUserId = MentionUtils.TryParseUser(input, out var userId);

            if (await context.Guild.GetUserAsync(userId) is SocketGuildUser discordUser)
            {
                var miunieUserResult = _convertor.UserConvertor.DiscordMemberToMiunieUser(discordUser);
                return TypeReaderResult.FromSuccess(miunieUserResult);
            }

            return TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as a MiunieUser.");
        }
    }
}