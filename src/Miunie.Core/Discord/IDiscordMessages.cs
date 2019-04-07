using Miunie.Core;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IDiscordMessages
    {
        Task SendMessage(MiunieChannel targetChannel, PhraseKey phraseKey, params object[] parameters);
        Task SendMessage(MiunieChannel targetChannel, MiunieUser user);
        Task SendMessage(MiunieChannel targetChannel, MiunieGuild guild);
        Task SendMessageAsync(MiunieChannel mc, DirectoryListing dl);
    }
}

