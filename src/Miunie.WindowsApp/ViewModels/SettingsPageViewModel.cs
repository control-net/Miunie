using System.Collections.Generic;
using System.Linq;
using System;
using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.Core.Logging;
using Miunie.WindowsApp.Utilities;
using GalaSoft.MvvmLight.Threading;

namespace Miunie.WindowsApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        public string BotToken => _miunieBot.BotConfiguration.DiscordToken;

        public TokenValidator TokenValidator { get; }

        public string BotAvatar => _miunieBot.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public string BotTokenBeginning => new string(BotToken?.Take(5).ToArray());

        public string BotTokenEnd => new string(BotToken?.TakeLast(5).ToArray());

        public IEnumerable<object> Logs => _logReader.RetrieveLogs(10).Select(m => new { Message = m });

        private readonly MiunieBot _miunieBot;

        private readonly ILogReader _logReader;

        public SettingsPageViewModel(MiunieBot miunie, ILogReader logReader, TokenValidator tokenValidator)
        {
            _miunieBot = miunie;
            _logReader = logReader;
            _logReader.LogRecieved += OnLogRecieved;
            TokenValidator = tokenValidator;
        }

        internal void ApplyToken(string token)
        {
            _miunieBot.BotConfiguration.DiscordToken = token;
            RaisePropertyChanged(nameof(BotToken));
            RaisePropertyChanged(nameof(BotTokenBeginning));
            RaisePropertyChanged(nameof(BotTokenEnd));
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
