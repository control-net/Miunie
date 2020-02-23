using Discord.WebSocket;

namespace Miunie.Discord
{
    public interface IDiscord
    {
        DiscordSocketClient Client { get; }
        void Initialize();
        void DisposeOfClient();
    }
}
