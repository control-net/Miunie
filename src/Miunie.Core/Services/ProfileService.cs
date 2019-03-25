using System.Threading.Tasks;
using Miunie.Core.Providers;

namespace Miunie.Core
{
    public class ProfileService
    {
        private readonly IDiscordMessages _discordMessages;
        private readonly IUserReputationProvider _reputationProvider;

        public ProfileService(IDiscordMessages discordMessages, IUserReputationProvider reputationProvider)
        {
            _discordMessages = discordMessages;
            _reputationProvider = reputationProvider;
        }

        public async Task ShowProfile(MiunieUser u, MiunieChannel c) 
            => await _discordMessages.SendMessage(c, "SHOW_PROFILE", u.Reputation.Value);

        public async Task GiveReputation(MiunieUser invoker, MiunieUser target, MiunieChannel c)
        {
            if (invoker.Id == target.Id)
            {
                await _discordMessages.SendMessage(c, "CANNOT_SELF_REP", invoker.Name);
                return;
            }

            if (_reputationProvider.AddReputationHasTimeout(invoker, target)) { return; }

            _reputationProvider.AddReputation(invoker, target);
            await _discordMessages.SendMessage(c, "REPUTATION_GIVEN", target.Name, invoker.Name);
        }

        public async Task RemoveReputation(MiunieUser invoker, MiunieUser target, MiunieChannel c)
        {
            if (invoker.Id == target.Id)
            {
                await _discordMessages.SendMessage(c, "CANNOT_SELF_REP", invoker.Name);
                return;                
            }

            if (_reputationProvider.RemoveReputationHasTimeout(invoker, target)) { return; }
            
            _reputationProvider.RemoveReputation(invoker, target);
            await _discordMessages.SendMessage(c, "REPUTATION_TAKEN", invoker.Name, target.Name);
        }
    }
}
