using System.Linq;
using GalaSoft.MvvmLight;
using Miunie.Core;

namespace Miunie.WindowsApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
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

        public string BotTokenBeginning => new string(_botToken.Take(5).ToArray());

        public string BotTokenEnd => new string(_botToken.TakeLast(5).ToArray());

        public SettingsPageViewModel(MiunieBot miunie)
        {
            BotToken = miunie.BotConfiguration.DiscordToken;
        }
    }
}
