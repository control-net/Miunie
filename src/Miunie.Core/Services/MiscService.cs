using Miunie.Core.Attributes;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Infrastructure;
using Miunie.Core.Providers;
using System.Threading.Tasks;

namespace Miunie.Core
{
    [Service]
    public class MiscService
    {
        private readonly IDiscordMessages _messages;
        private readonly IDateTime _dateTime;
        private readonly IMiunieUserProvider _users;

        public MiscService(IDiscordMessages messages, IDateTime dateTime, IMiunieUserProvider users)
        {
            _messages = messages;
            _dateTime = dateTime;
            _users = users;
        }

        public async Task SendRandomYesNoAnswer(MiunieChannel channel)
        {
            await _messages.SendMessageAsync(channel, PhraseKey.YES_NO_MAYBE);
        }
    }
}
