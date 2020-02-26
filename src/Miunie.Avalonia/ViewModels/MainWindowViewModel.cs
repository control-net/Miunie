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

        private string _text = "0";

        public string Text 
        { 
            get { return _text; }
            set { this.RaiseAndSetIfChanged(ref _text, value); }
        }

        public async Task StartButton_ClickCommand()
        {
            _miunie.BotConfiguration.DiscordToken = "NTU5MDU4NzQ4NTkzOTMwMjUx.XlbErA.Y7Zl5AimV9TzVg5a_1UkuuZ9bT4";
            await _miunie.StartAsync();
        }

        public void StopButton_ClickCommand()
        {
            _miunie.Stop();
        }

        private void ConectionStateChanged(object sender, EventArgs e)
        {
            if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.CONNECTING)
                Text = "Connecting";
            else if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.CONNECTED)
                Text = "Connected";
            else if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.DISCONNECTED)
                Text = "Disconnected";
        }
    }
}
