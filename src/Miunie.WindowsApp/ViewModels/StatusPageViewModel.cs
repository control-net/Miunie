using System;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Miunie.Core;
using Miunie.Core.Entities;

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

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public Action AvatarChanged;

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

        public bool IsConnecting { get => _miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTING; }

        public async void ToggleBotStart()
        {
            if (_miunie.MiunieDiscord.ConnectionState != ConnectionState.CONNECTED)
            {
                if (_miunie.BotConfiguration.DiscordToken == null)
                {
                    ErrorMessage = "No key found, input your key inside Settings!";
                    return;
                }
                
                await _miunie?.StartAsync();
            }
            else
            {
                _miunie.Stop();
            }

            RaisePropertyChanged(nameof(ActionButtonText));
        }

        private void MiunieOnConnectionStateChanged(object sender, EventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(
                () =>
                {
                    ConnectionStatus = _miunie.MiunieDiscord.ConnectionState.ToString();
                    ErrorMessage = "";
                    RaisePropertyChanged(nameof(ActionButtonText));
                    RaisePropertyChanged(nameof(ActionButtonIsVisible));
                    RaisePropertyChanged(nameof(ProgressBarIsVisible));
                    RaisePropertyChanged(nameof(BotAvatar));
                    AvatarChanged?.Invoke();
                });
        }
    }
}
