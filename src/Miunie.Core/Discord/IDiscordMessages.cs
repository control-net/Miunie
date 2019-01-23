using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IDiscordMessages
    {
        Task SendMessage(string message, MiunieChannel targetChannel);
    }
}

