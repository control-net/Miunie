using Miunie.Core.Attributes;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Logging;
using Miunie.Core.Providers;
using System.Threading.Tasks;

namespace Miunie.Core
{
    [Service]
    public class ProfileService
    {
        private readonly IDiscordMessages _discordMessages;
        private readonly IUserReputationProvider _reputationProvider;
        private readonly ILogWriter _logger;
        private readonly IDiscordConnection _miunieDiscord;

        public ProfileService(IDiscordMessages discordMessages, IUserReputationProvider reputationProvider, ILogWriter logger, IDiscordConnection miunieDiscord)
        {
            _discordMessages = discordMessages;
            _reputationProvider = reputationProvider;
            _logger = logger;
            _miunieDiscord = miunieDiscord;
        }

        public async Task ShowProfileAsync(MiunieUser user, MiunieChannel c)
            => await _discordMessages.SendMessageAsync(c, user);

        public async Task ShowReputationLogAsync(MiunieUser user, int page, MiunieChannel c)
        {
            page -= 1;
            var repGiven = _reputationProvider.GetReputation(user);

            await _discordMessages.SendMessageAsync(c, repGiven, page);
        }

        public async Task ShowReputationLogAsync(MiunieUser invoker, MiunieUser target, int page, MiunieChannel c)
        {
            page -= 1;
            var repGiven = _reputationProvider.GetReputation(target);
            
            await _discordMessages.SendMessageAsync(c, repGiven, page);
        }

        public async Task GiveReputationAsync(MiunieUser invoker, MiunieUser target, MiunieChannel c)
        {
            if (invoker.UserId == target.UserId)
            {
                await _discordMessages.SendMessageAsync(c, PhraseKey.CANNOT_SELF_REP, invoker.Name);
                return;
            }

            if (_reputationProvider.CanAddReputation(invoker, target))
            {
                _logger.Log($"User '{invoker.Name}' has a reputation timeout for User '{target.Name}', ignoring...");
                return;
            }

            _reputationProvider.AddReputation(invoker, target);

            if (_miunieDiscord.UserIsMiunie(target))
            {
                await _discordMessages.SendMessageAsync(c, PhraseKey.REPUTATION_GIVEN_BOT, invoker.Name);
                return;
            }

            await _discordMessages.SendMessageAsync(c, PhraseKey.REPUTATION_GIVEN, target.Name, invoker.Name);
        }

        public async Task RemoveReputationAsync(MiunieUser invoker, MiunieUser target, MiunieChannel c)
        {
            if (invoker.UserId == target.UserId)
            {
                await _discordMessages.SendMessageAsync(c, PhraseKey.CANNOT_SELF_REP, invoker.Name);
                return;
            }

            if (_reputationProvider.CanRemoveReputation(invoker, target)) { return; }

            _reputationProvider.RemoveReputation(invoker, target);

            if (_miunieDiscord.UserIsMiunie(target))
            {
                await _discordMessages.SendMessageAsync(c, PhraseKey.REPUTATION_TAKEN_BOT, invoker.Name);
                return;
            }

            await _discordMessages.SendMessageAsync(c, PhraseKey.REPUTATION_TAKEN, invoker.Name, target.Name);
        }

        public async Task ShowGuildProfileAsync(MiunieGuild guild, MiunieChannel c)
            => await _discordMessages.SendMessageAsync(c, guild);
    }
}
