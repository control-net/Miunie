using GalaSoft.MvvmLight;
using Miunie.Core;
using System.Collections.Generic;

namespace Miunie.WindowsApp.ViewModels
{
    public class NotConnectedPageViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;

        public NotConnectedPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
        }

        public string ConnectionStatus => 
            _miunie.MiunieDiscord.ConnectionState.ToString();
    }
}
