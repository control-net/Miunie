using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Miunie.Core;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Miunie.WindowsApp.ViewModels
{
    public class ImpersonationChatPageViewModel : ViewModelBase, IDisposable
    {
        private TextChannelView _selectedChannel;

        public TextChannelView SelectedChannel
        {
            get => _selectedChannel;
            set { 
                _selectedChannel = value;
                RaisePropertyChanged(nameof(SelectedChannel));
            }
        }


        private IEnumerable<TextChannelView> _channels;

        public IEnumerable<TextChannelView> Channels
        {
            get => _channels;
            set {
                _channels = value;
                RaisePropertyChanged(nameof(Channels));
            }
        }

        public ICommand SendMessageCommand { get; }


        private readonly MiunieBot _miunie;
        private ulong _currentGuild;

        public ImpersonationChatPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            _channels = new List<TextChannelView>();
            SendMessageCommand = new RelayCommand<string>(SendMessageAsMiunieAsync);
        }

        internal void ConfigureMessagesSubscription()
        {
            _miunie.Impersonation.MessageReceived += MessageReceivedHandler;
            _miunie.Impersonation.SubscribeForMessages();
        }

        internal async void FetchInfo(ulong guildId)
        {
            Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(guildId);
            _currentGuild = guildId;
        }

        private async void SendMessageAsMiunieAsync(string message)
        {
            await _miunie.Impersonation.SendTextToChannelAsync(message, SelectedChannel.Id);
        }

        private async void MessageReceivedHandler(object sender, EventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var selectedId = SelectedChannel?.Id;
                Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(_currentGuild);
                SelectedChannel = Channels.FirstOrDefault(x => x.Id == selectedId);
            });
        }

        public void Dispose()
        {
            _miunie.Impersonation.MessageReceived -= MessageReceivedHandler;
            _miunie.Impersonation.UnsubscribeForMessages();
        }
    }
}
