using System.Threading.Tasks;

namespace Miunie.Core
{
    public class MiunieBot
    {
        private readonly IDiscord _discord;

        public MiunieBot(IDiscord discord)
        {
            _discord = discord;
        }

        public async Task RunAsync()
        {
            await _discord.RunAsync();
        }
    }
}

