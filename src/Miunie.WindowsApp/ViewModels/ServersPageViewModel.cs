using GalaSoft.MvvmLight;
using Miunie.Core;
using System.Collections.Generic;

namespace Miunie.WindowsApp.ViewModels
{
    public class ServersPageViewModel : ViewModelBase
    {
        public IEnumerable<GuildView> AvailableGuilds => _miunie.Impersonation.GetAvailableGuilds();

        private readonly MiunieBot _miunie;

        public ServersPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
        }
    }
}
