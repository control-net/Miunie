// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Miunie.Core;
using Miunie.Core.Entities;
using Miunie.Core.Logging;
using Miunie.WindowsApp.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Miunie.WindowsApp.ViewModels
{
    public class StatusPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        private readonly MiunieBot _miunie;
        private readonly TokenManager _tokenManager;
        private readonly ILogWriter _logWriter;
        private string _connectedStatus;
        private string _errorMessage;

        public StatusPageViewModel(MiunieBot miunie, TokenManager tokenManager, ILogWriter logWriter)
        {
            _miunie = miunie;
            _tokenManager = tokenManager;
            _logWriter = logWriter;
            miunie.MiunieDiscord.ConnectionChanged += MiunieOnConnectionStateChanged;
            ConnectionStatus = "Not connected";
            _tokenManager.LoadToken(_miunie);
            CheckForTokenInClipboard();
        }

        public ICommand ActionCommand => new RelayCommand(ToggleBotStart, CanToggleStart);

        public bool IsConnecting => _miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTING;

        public string ConnectionStatus
        {
            get => _connectedStatus;
            set
            {
                if (value == _connectedStatus)
                {
                    return;
                }

                _connectedStatus = value;
                RaisePropertyChanged(nameof(ConnectionStatus));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value == _errorMessage)
                {
                    return;
                }

                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public Action AvatarChanged { get; set; }

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

        private bool CanToggleStart()
        {
            return !string.IsNullOrEmpty(_miunie.BotConfiguration.DiscordToken);
        }

        private async void CheckForTokenInClipboard()
        {
            if (!string.IsNullOrWhiteSpace(_miunie.BotConfiguration.DiscordToken))
            {
                RaisePropertyChanged(nameof(SettingsButtonIsVisable));
                RaisePropertyChanged(nameof(ActionCommand));
                return;
            }

            var possibleToken = await TryGetClipboardContents();

            if (!_tokenManager.StringHasValidTokenStructure(possibleToken)) { return; }

            var clipboardTokenDialog = new ContentDialog
            {
                Title = "Paste copied bot token?",
                Content = "It looks like you have a bot token copied.\nDo you want to use it?",
                PrimaryButtonText = "Sure",
                CloseButtonText = "No, thanks"
            };

            if (await clipboardTokenDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                _tokenManager.ApplyToken(possibleToken, _miunie);
                RaisePropertyChanged(nameof(SettingsButtonIsVisable));
                RaisePropertyChanged(nameof(ActionCommand));
            }
        }

        private async Task<string> TryGetClipboardContents()
        {
            try
            {
                var clipboardContent = Clipboard.GetContent();

                if (!clipboardContent.AvailableFormats.Contains(StandardDataFormats.Text)) { return string.Empty; }

                return await Clipboard.GetContent().GetTextAsync();
            }
            catch (Exception ex)
            {
                _logWriter.LogError($"Unable to query clipboard: {ex.Message}");
                return string.Empty;
            }
        }

        private void MiunieOnConnectionStateChanged(object sender, EventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(
                () =>
                {
                    ConnectionStatus = _miunie.MiunieDiscord.ConnectionState.ToString();
                    ErrorMessage = string.Empty;
                    RaisePropertyChanged(nameof(ActionButtonText));
                    RaisePropertyChanged(nameof(ActionButtonIsVisible));
                    RaisePropertyChanged(nameof(ProgressBarIsVisible));
                    RaisePropertyChanged(nameof(BotAvatar));
                    AvatarChanged?.Invoke();
                });
        }
    }
}
