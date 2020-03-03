using Miunie.Core.Configuration;
using Miunie.Core.Discord;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class MiunieBot
    {
        public IBotConfiguration BotConfiguration { get; }
        public IDiscordConnection MiunieDiscord { get; }
        public IDiscordImpersonation Impersonation { get; }

        private CancellationTokenSource _tokenSource;

        public MiunieBot(IDiscordConnection miunieDiscord, IBotConfiguration botConfig, IDiscordImpersonation impersonation)
        {
            MiunieDiscord = miunieDiscord;
            BotConfiguration = botConfig;
            Impersonation = impersonation;
        }

        public async Task StartAsync()
        {
            _tokenSource = new CancellationTokenSource();
            await MiunieDiscord.RunAsync(_tokenSource.Token);
        }

        public void Stop()
        {
            if (_tokenSource is null) { return; }
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }
    }
}
