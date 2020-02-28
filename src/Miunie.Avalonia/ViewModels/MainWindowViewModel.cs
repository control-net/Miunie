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
        private readonly ILogReader _logReader;

        public MainWindowViewModel()
        {
            _miunie = ActivatorUtilities.CreateInstance<MiunieBot>(InversionOfControl.Provider);
            _logReader = InversionOfControl.Provider.GetRequiredService<ILogReader>();
            _miunie.MiunieDiscord.ConnectionChanged += ConectionStateChanged;
        }

        private string _connectionStatus = "0";
        private string _discordToken = "";

        public string ConnectionStatus 
        { 
            get { return _connectionStatus; }
            set { this.RaiseAndSetIfChanged(ref _connectionStatus, value); }
        }

        public string DiscordToken 
        { 
            get { return _discordToken; }
            set { this.RaiseAndSetIfChanged(ref _discordToken, value); }
        }

        public async Task StartButton_ClickCommand()
        {
            if (!string.IsNullOrWhiteSpace(DiscordToken))
            {
                _miunie.BotConfiguration.DiscordToken = DiscordToken;
                await _miunie.StartAsync();
            }
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
            if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.CONNECTING)
                ConnectionStatus = "Connecting";
            else if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.CONNECTED)
                ConnectionStatus = "Connected";
            else if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.DISCONNECTED)
                ConnectionStatus = "Disconnected";
        }
    }
}
