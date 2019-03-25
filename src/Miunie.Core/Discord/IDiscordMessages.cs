using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IDiscordMessages
    {
        Task SendMessage(MiunieChannel targetChannel, string phraseKey, params object[] parameters);
    }
}

