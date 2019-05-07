using System.Collections.Generic;
using System.Linq;
using System;
using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.Core.Logging;

namespace Miunie.WindowsApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUrl = "../Assets/miunie-scarf-transparent.png";

        private string _botToken;
        public string BotToken
        {

            get => _botToken;
            set
            {
                if (value == _botToken) return;
                _botToken = value;
                RaisePropertyChanged(nameof(BotToken));
            }
        }

        public string BotAvatar => _miunieBot.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public string BotTokenBeginning => new string(_botToken?.Take(5).ToArray());

        public string BotTokenEnd => new string(_botToken?.TakeLast(5).ToArray());

        public IEnumerable<object> Logs => _logReader.RetrieveLogs(10).Select(m => new { Message = m });

        private readonly MiunieBot _miunieBot;

        private readonly ILogReader _logReader;

        public SettingsPageViewModel(MiunieBot miunie, ILogReader logReader)
        {
            _miunieBot = miunie;
            _logReader = logReader;
            BotToken = miunie.BotConfiguration.DiscordToken;
            _logReader.LogRecieved += OnLogRecieved;
        }

        private void OnLogRecieved(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Logs));
        }
    }
}
