using System.Collections.Generic;
using System.Linq;
using System;
using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.Core.Logging;
using Miunie.WindowsApp.Utilities;
using GalaSoft.MvvmLight.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Views;

namespace Miunie.WindowsApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        public string BotToken => _miunie.BotConfiguration.DiscordToken;

        public string BotAvatar => _miunie.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public IEnumerable<object> Logs => _logReader.RetrieveLogs(10).Select(m => new { Message = m });

        private readonly MiunieBot _miunie;
        private readonly ILogReader _logReader;
        private readonly TokenManager _tokenManager;

        public ICommand ApplyTokenCommand => new RelayCommand<string>(ApplyToken, CanApplyToken);

        public SettingsPageViewModel(MiunieBot miunie, ILogReader logReader, TokenManager tokenManager)
        {
            _miunie = miunie;
            _logReader = logReader;
            _tokenManager = tokenManager;
            _logReader.LogRecieved += OnLogRecieved;
        }

        public event EventHandler TokenApplied;

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
