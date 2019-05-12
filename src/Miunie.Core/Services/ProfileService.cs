using Miunie.Core.Logging;
using Miunie.Core.Providers;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class ProfileService
    {
        private readonly IDiscordMessages _discordMessages;
        private readonly IUserReputationProvider _reputationProvider;
        private readonly ILogWriter _logger;

        public ProfileService(IDiscordMessages discordMessages, IUserReputationProvider reputationProvider, ILogWriter logger)
        {
            _discordMessages = discordMessages;
            _reputationProvider = reputationProvider;
            _logger = logger;
        }

        public async Task ShowProfileAsync(MiunieUser user, MiunieChannel c)
            => await _discordMessages.SendMessageAsync(c, user);

        public async Task GiveReputationAsync(MiunieUser invoker, MiunieUser target, MiunieChannel c)
        {
            if (invoker.Id == target.Id)
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
            await _discordMessages.SendMessageAsync(c, PhraseKey.REPUTATION_GIVEN, target.Name, invoker.Name);
        }

        public async Task RemoveReputationAsync(MiunieUser invoker, MiunieUser target, MiunieChannel c)
        {
            if (invoker.Id == target.Id)
            {
                await _discordMessages.SendMessageAsync(c, PhraseKey.CANNOT_SELF_REP, invoker.Name);
                return;
            }

            if (_reputationProvider.CanRemoveReputation(invoker, target)) { return; }

            _reputationProvider.RemoveReputation(invoker, target);
            await _discordMessages.SendMessageAsync(c, PhraseKey.REPUTATION_TAKEN, invoker.Name, target.Name);
        }

        public async Task ShowGuildProfileAsync(MiunieGuild guild, MiunieChannel c)
            => await _discordMessages.SendMessageAsync(c, guild);
    }
}
