using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Miunie.Core;

namespace Miunie.WindowsApp.ViewModels
{
    public class ImpersonationChatPageViewModel : ViewModelBase
    {
        private TextChannelView _selectedChannel;

        public TextChannelView SelectedChannel
        {
            get { return _selectedChannel; }
            set { 
                _selectedChannel = value;
                RaisePropertyChanged(nameof(SelectedChannel));
            }
        }


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

        public ICommand SendMessage { get; }


        private readonly MiunieBot _miunie;
        private ulong _currentGuild;

        public ImpersonationChatPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            _channels = new List<TextChannelView>();
            SendMessage = new RelayCommand<string>(SendMessageAsMiunieAsync);
        }

        private async void SendMessageAsMiunieAsync(string message)
        {
            await _miunie.Impersonation.SendTextToChannelAsync(message, SelectedChannel.Id);

            // Needs to be re-worked
            var selectedId = SelectedChannel.Id;
            Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(_currentGuild);
            SelectedChannel = Channels.FirstOrDefault(x => x.Id == selectedId);
        }

        internal async void FetchInfo(ulong guildId)
        {
            Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(guildId);
            _currentGuild = guildId;
        }
    }
}
