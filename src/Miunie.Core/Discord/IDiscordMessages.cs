using Miunie.Core.Entities.Discord;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordMessages
    {
        Task SendMessageAsync(MiunieChannel targetChannel, PhraseKey phraseKey, params object[] parameters);
        Task SendMessageAsync(MiunieChannel targetChannel, MiunieUser user);
        Task SendMessageAsync(MiunieChannel targetChannel, MiunieGuild guild);
        Task SendMessageAsync(MiunieChannel mc, IEnumerable<ReputationEntry> repEntries, int index);
    }
}

