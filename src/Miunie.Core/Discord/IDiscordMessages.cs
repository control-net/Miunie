using Miunie.Core;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IDiscordMessages
    {
        Task SendMessage(MiunieChannel targetChannel, PhraseKey phraseKey, params object[] parameters);
    }
}

