using Miunie.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IDiscordMessages
    {
        Task SendMessageAsync(MiunieChannel targetChannel, PhraseKey phraseKey, params object[] parameters);
        Task SendMessageAsync(MiunieChannel targetChannel, MiunieUser user);
        Task SendMessageAsync(MiunieChannel targetChannel, MiunieGuild guild);
        Task SendMessageAsync(MiunieChannel mc, DirectoryListing dl);
        Task SendMessageAsync(MiunieChannel channel, IEnumerable<CurrencyData> cd);
        Task SendMessageAsync(MiunieChannel mc, CurrencyConversionResult ccr);

        Task SendMessageAsync(MiunieChannel mc, List<ReputationEntry> repEntries, int index);
    }
}

