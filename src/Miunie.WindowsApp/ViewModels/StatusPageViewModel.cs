using System;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Miunie.Core;

namespace Miunie.WindowsApp.ViewModels
{
    public class StatusPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        private string _connectedStatus;
        public string ConnectionStatus
        {

            get => _connectedStatus;
            set
            {
                if (value == _connectedStatus) return;
                _connectedStatus = value;
                RaisePropertyChanged(nameof(ConnectionStatus));
            }
        }

        public Visibility ActionButtonIsVisible => _miunie.MiunieDiscord.ConnectionState != ConnectionState.CONNECTING 
            ? Visibility.Visible 
            : Visibility.Collapsed;

        public Visibility ProgressBarIsVisible => _miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTING
            ? Visibility.Visible
            : Visibility.Collapsed;

        public string BotAvatar => _miunie.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public string ActionButtonText => _miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTED ? "Stop" : "Start";

        private readonly MiunieBot _miunie;

        public StatusPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            miunie.MiunieDiscord.ConnectionChanged += MiunieOnConnectionStateChanged;
            ConnectionStatus = "Not connected";
        }

        public async void ToggleBotStart()
        {
            if (_miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTED)
            {
                _miunie.Stop();
            }
            else if(_miunie.MiunieDiscord.ConnectionState == ConnectionState.DISCONNECTED)
            {
                await _miunie.StartAsync();
            }
            else
            {
                RaisePropertyChanged(nameof(ActionButtonText));
            }
        }

        private void MiunieOnConnectionStateChanged(object sender, EventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(
                () =>
                {
                    ConnectionStatus = _miunie.MiunieDiscord.ConnectionState.ToString();
                    RaisePropertyChanged(nameof(ActionButtonText));
                    RaisePropertyChanged(nameof(ActionButtonIsVisible));
                    RaisePropertyChanged(nameof(ProgressBarIsVisible));
                    RaisePropertyChanged(nameof(BotAvatar));
                });
        }
    }
}
