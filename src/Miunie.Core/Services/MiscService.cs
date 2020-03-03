using Miunie.Core.Attributes;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using System.Threading.Tasks;

namespace Miunie.Core
{
    [Service]
    public class MiscService
    {
        private readonly IDiscordMessages _messages;

        public MiscService(IDiscordMessages messages)
        {
            _messages = messages;
        }

        public async Task SendRandomYesNoAnswerAsync(MiunieChannel channel)
        {
            await _messages.SendMessageAsync(channel, PhraseKey.YES_NO_MAYBE);
        }
    }
}
