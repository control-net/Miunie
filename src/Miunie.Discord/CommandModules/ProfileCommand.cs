using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Miunie.Core;
using Miunie.Core.Language;
using Miunie.Core.Logging;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class ProfileCommand : BaseCommandModule
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly ProfileService _profileService;

        public ProfileCommand(EntityConvertor entityConvertor, ProfileService profileService, ILanguageResources lang, IDiscordMessages discordMessages, ILogger logger)
        {
            _entityConvertor = entityConvertor;
            _profileService = profileService;
        }

        [Command("profile")]
        public async Task ShowProfile(CommandContext ctx, MiunieUser m = null)
        {
            if (m is null)
            {
                m = _entityConvertor.ConvertUser(ctx.Member);
            }
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.ShowProfile(m, channel);
        }

        [Command("+rep")]
        public async Task AddReputation(CommandContext ctx, MiunieUser m)
        {
            var source = _entityConvertor.ConvertUser(ctx.Member);
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.GiveReputation(source, m, channel);
        }

        [Command("-rep")]
        public async Task RemoveReputation(CommandContext ctx, MiunieUser m)
        {
            var source = _entityConvertor.ConvertUser(ctx.Member);
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.RemoveReputation(source, m, channel);
        }
    }
}
