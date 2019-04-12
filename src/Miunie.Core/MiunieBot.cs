using System.Threading.Tasks;

namespace Miunie.Core
{
    public class MiunieBot
    {
        private readonly IMiunieDiscord _miunieDiscord;

        public MiunieBot(IMiunieDiscord miunieDiscord)
        {
            _miunieDiscord = miunieDiscord;
        }

        public async Task RunAsync()
        {
            await _miunieDiscord.RunAsync();
            await Task.Delay(-1);
        }
    }
}
