using System.Threading.Tasks;
using Miunie.Core.Providers;

namespace Miunie.Core
{
    public class ProfileService
    {
        private readonly IDiscordMessages _discordMessages;
        private readonly IMiunieUserProvider _userProvider;

        public ProfileService(IDiscordMessages discordMessages, IMiunieUserProvider userProvider)
        {
            _discordMessages = discordMessages;
            _userProvider = userProvider;
        }

        public async Task ShowProfile(MiunieUser u, MiunieChannel c) 
            => await _discordMessages.SendMessage(c, "SHOW_PROFILE", u.Reputation.Value);

        public async Task GiveReputation(MiunieUser source, MiunieUser target, MiunieChannel c)
        {
            if (source.Id == target.Id)
            {
                await _discordMessages.SendMessage(c, "CANNOT_SELF_REP", source.Name);
                return;
            }

            if(!target.Reputation.CanGetPlusRepFrom(source.Id)) { return; }

            target.Reputation.GiveRepFrom(source);
            _userProvider.StoreUser(target);
            await _discordMessages.SendMessage(c, "REPUTATION_GIVEN", target.Name, source.Name);
        }

        public async Task RemoveReputation(MiunieUser source, MiunieUser target, MiunieChannel c)
        {
            if (!target.Reputation.CanGetMinusRepFrom(source.Id)) { return; }

            if (source.Id == target.Id)
            {
                await _discordMessages.SendMessage(c, "CANNOT_SELF_REP", source.Name);
                return;                
            }        
            
            target.Reputation.RemoveRepFrom(source);
            _userProvider.StoreUser(target);
            await _discordMessages.SendMessage(c, "REPUTATION_TAKEN", source.Name, target.Name);
        }
    }
}
