using System;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Miunie.Core;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Miunie.Core.Entities;
using Windows.ApplicationModel.DataTransfer;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Miunie.WindowsApp.Utilities;

namespace Miunie.WindowsApp.ViewModels
{
    public class StatusPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        private readonly MiunieBot _miunie;
        private readonly TokenManager _tokenManager;

        public StatusPageViewModel(MiunieBot miunie, TokenManager tokenManager)
        {
            _miunie = miunie;
            _tokenManager = tokenManager;
            miunie.MiunieDiscord.ConnectionChanged += MiunieOnConnectionStateChanged;
            ConnectionStatus = "Not connected";
            CheckForTokenInClipboard();
        }

        public ICommand ActionCommand => new RelayCommand(ToggleBotStart, CanToggleStart);

        private bool CanToggleStart()
        {
            return !string.IsNullOrEmpty(_miunie.BotConfiguration.DiscordToken);
        }

        public bool IsConnecting { get => _miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTING; }

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

        public Visibility SettingsButtonIsVisable => string.IsNullOrWhiteSpace(_miunie.BotConfiguration.DiscordToken)
            ? Visibility.Visible
            : Visibility.Collapsed;

        public string BotAvatar => _miunie.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public string ActionButtonText => _miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTED ? "Stop" : "Start";

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

        private async void CheckForTokenInClipboard()
        {
            if (!string.IsNullOrWhiteSpace(_miunie.BotConfiguration.DiscordToken)) { return; }

            var clipboardContent = Clipboard.GetContent();

            if (!clipboardContent.AvailableFormats.Contains(StandardDataFormats.Text)) { return; }

            var possibleToken = await Clipboard.GetContent().GetTextAsync();

            if (!_tokenManager.StringHasValidTokenStructure(possibleToken)) { return; }

            var clipboardTokenDialog = new ContentDialog
            {
                Title = "Paste copied bot token?",
                Content = "It looks like you have a bot token copied.\nDo you want to use it?",
                PrimaryButtonText = "Sure",
                CloseButtonText = "No, thanks"
            };

            var result = await clipboardTokenDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) { 
                _tokenManager.ApplyToken(possibleToken, _miunie);
                RaisePropertyChanged(nameof(SettingsButtonIsVisable));
                RaisePropertyChanged(nameof(ActionCommand));
            }
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
