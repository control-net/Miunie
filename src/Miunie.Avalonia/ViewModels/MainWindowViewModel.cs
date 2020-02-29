using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Miunie.Core;
using Miunie.Core.Logging;
using ReactiveUI;

namespace Miunie.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;

        public MainWindowViewModel()
        {
            _miunie = ActivatorUtilities.CreateInstance<MiunieBot>(InversionOfControl.Provider);
            _miunie.MiunieDiscord.ConnectionChanged += ConectionStateChanged;
        }

        private string _connectionStatusText = "Disconnected";

        public string ConnectionStatusText 
        { 
            get { return _connectionStatusText; }
            set { this.RaiseAndSetIfChanged(ref _connectionStatusText, value); }
        }

        private string _discordToken = "";

        public string DiscordToken 
        { 
            get { return _discordToken; }
            set { this.RaiseAndSetIfChanged(ref _discordToken, value); }
        }

        public async Task StartButton_ClickCommand()
        {
            if (string.IsNullOrWhiteSpace(DiscordToken))
                return;

            _miunie.BotConfiguration.DiscordToken = DiscordToken;
            await _miunie.StartAsync();
        }

        public void StopButton_ClickCommand()
        {
            if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.CONNECTED)
            {
                _miunie.Stop();
            }
        }

        private void ConectionStateChanged(object sender, EventArgs e)
        {
            _connectionStatusText = _miunie.MiunieDiscord.ConnectionState switch
            {
                ConnectionState.CONNECTED => "Connecting",
                ConnectionState.CONNECTING => "Connected",
                ConnectionState.DISCONNECTED => "Disconnected",
                _ => "Unknown State"
            };
        }
    }
}
