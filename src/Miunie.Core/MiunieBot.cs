using System;
using System.Threading;
using System.Threading.Tasks;
using Miunie.Core.Configuration;

namespace Miunie.Core
{
    public class MiunieBot
    {
        public event EventHandler ConnectionStateChanged;

        public IBotConfiguration BotConfiguration { get; }
        public bool IsRunning => _miunieDiscord.IsRunning;

        private readonly IMiunieDiscord _miunieDiscord;
        private readonly CancellationTokenSource _tokenSource;

        public MiunieBot(IMiunieDiscord miunieDiscord, IBotConfiguration botConfig)
        {
            _miunieDiscord = miunieDiscord;
            BotConfiguration = botConfig;
            _tokenSource = new CancellationTokenSource();

            _miunieDiscord.ConnectionChanged += MiunieDiscordOnConnectionChanged;
        }

        private void MiunieDiscordOnConnectionChanged(object sender, EventArgs e)
        {
            ConnectionStateChanged?.Invoke(sender, e);
        }

        public async Task StartAsync() => await _miunieDiscord.RunAsync(_tokenSource.Token);

        public void Stop() => _tokenSource.Cancel();
    }
}
