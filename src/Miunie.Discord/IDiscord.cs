using Discord.WebSocket;
using System.Threading.Tasks;

namespace Miunie.Discord
{
    public interface IDiscord
    {
        DiscordSocketClient Client { get; }
        Task InitializeAsync();
        void DisposeOfClient();
    }
}
