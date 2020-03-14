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
using Miunie.Core.Logging;
using Miunie.WindowsApp.Utilities;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Miunie.WindowsApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        private readonly MiunieBot _miunie;
        private readonly ILogReader _logReader;
        private readonly TokenManager _tokenManager;

        public SettingsPageViewModel(MiunieBot miunie, ILogReader logReader, TokenManager tokenManager)
        {
            _miunie = miunie;
            _logReader = logReader;
            _tokenManager = tokenManager;
            _logReader.LogRecieved += OnLogRecieved;
        }

        public event EventHandler TokenApplied;

        public string BotToken => _miunie.BotConfiguration.DiscordToken;

        public string BotAvatar => _miunie.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public string Logs => string.Join(Environment.NewLine, _logReader.RetrieveLogs(10));

        public ICommand ApplyTokenCommand => new RelayCommand<string>(ApplyToken, CanApplyToken);

        private bool CanApplyToken(string arg)
        {
            return !string.IsNullOrEmpty(arg);
        }

        private async void ApplyToken(string token)
        {
            if (!_tokenManager.StringHasValidTokenStructure(token))
            {
                var possiblyWrongTokenDialog = new ContentDialog
                {
                    Title = "That doesn't look like a token.",
                    Content = "The token you provided doesn't follow the basic token length and content structure.",
                    PrimaryButtonText = "Apply anyway",
                    CloseButtonText = "Cancel"
                };

                var result = await possiblyWrongTokenDialog.ShowAsync();

                if (result != ContentDialogResult.Primary) { return; }
            }

            _tokenManager.ApplyToken(token, _miunie);
            RaisePropertyChanged(nameof(BotToken));

            TokenApplied?.Invoke(this, EventArgs.Empty);
        }

        private void OnLogRecieved(object sender, EventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(
                () =>
                {
                    RaisePropertyChanged(nameof(Logs));
                });
        }
    }
}
