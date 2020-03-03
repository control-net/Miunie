using GalaSoft.MvvmLight;
using Miunie.Core;
using Miunie.Core.Views;
using System.Collections.Generic;

namespace Miunie.WindowsApp.ViewModels
{
    public class ImpersonationChatPageViewModel : ViewModelBase
    {
        private IEnumerable<TextChannelView> _channels;

        public IEnumerable<TextChannelView> Channels
        {
            get => _channels;
            set
            {
                _channels = value;
                RaisePropertyChanged(nameof(Channels));
            }
        }

        private readonly MiunieBot _miunie;

        public ImpersonationChatPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            _channels = new List<TextChannelView>();
        }

        internal async void FetchInfo(ulong guildId)
        {
            Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(guildId);
        }
    }
}
