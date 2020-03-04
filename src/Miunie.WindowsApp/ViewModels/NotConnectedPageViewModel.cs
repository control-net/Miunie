using GalaSoft.MvvmLight;
using Miunie.Core;

namespace Miunie.WindowsApp.ViewModels
{
    public class NotConnectedPageViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;

        public NotConnectedPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
        }

        public bool StartIsEnabled => 
            !string.IsNullOrWhiteSpace(_miunie.BotConfiguration.DiscordToken);

        public string ConnectionStatus => 
            _miunie.MiunieDiscord.ConnectionState.ToString();
    }
}
