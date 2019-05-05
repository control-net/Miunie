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

        public string BotAvatar => _miunieBot.MiunieDiscord.GetBotAvatarUrl() ?? DefaultAvatarUrl;

        public IEnumerable<object> Logs => _logReader.RetrieveLogs(10).Select(m => new { Message = m });

        private readonly MiunieBot _miunieBot;

        private readonly ILogReader _logReader;

        public SettingsPageViewModel(MiunieBot miunie, ILogReader logReader)
        {
            _miunieBot = miunie;
            _logReader = logReader;
            _logReader.LogRecieved += OnLogRecieved;
        }

        private void OnLogRecieved(object sender, EventArgs e)
        {
             RaisePropertyChanged(nameof(Logs));
        }
    }
}
