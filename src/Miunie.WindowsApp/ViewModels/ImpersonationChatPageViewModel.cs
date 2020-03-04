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
using Miunie.Core.Entities.Views;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Discord.WebSocket;
using System.Collections.Specialized;

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
                if (_selectedChannel != null && _selectedChannel.Messages.Any())
                {
                    Messages = new ObservableCollection<MessageView>(_selectedChannel.Messages);
                    RaisePropertyChanged(nameof(IsMessageTextboxEnabled));
                }
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


        private ICollection<MessageView> _messages;

        public ICollection<MessageView> Messages
        {
            get { return _messages; }
            set { 
                _messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }
        

        private string _messageText;

        public string MessageText
        {
            get { return _messageText; }
            set { 
                _messageText = value;
                RaisePropertyChanged(nameof(MessageText));
            }
        }

        public bool IsMessageTextboxEnabled => _selectedChannel != null;


        private readonly MiunieBot _miunie;

        public ImpersonationChatPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            _channels = new List<TextChannelView>();
            _messages = new List<MessageView>();
        }

        public event EventHandler MessageReceived;

        internal void ConfigureMessagesSubscription()
        {
            _miunie.Impersonation.MessageReceived += MessageReceivedHandler;
            _miunie.Impersonation.SubscribeForMessages();
        }

        internal async void FetchInfo(ulong guildId)
        {
            Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(guildId);
        }

        public ICommand SendMessageCommand => new RelayCommand<string>(SendMessageAsMiunieAsync, CanSendMessage);        

        private bool CanSendMessage(string arg)
        {
            return !string.IsNullOrWhiteSpace(MessageText) && SelectedChannel != null;
        }

        internal async void SendMessageAsMiunieAsync(string message)
        {
            await _miunie.Impersonation.SendTextToChannelAsync(message, SelectedChannel.Id);
        }

        private async void MessageReceivedHandler(object sender, EventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                var m = (SocketMessage)sender;
                Messages.Add(new MessageView
                {
                    AuthorAvatarUrl = m.Author.GetAvatarUrl(),
                    AuthorName = m.Author.Username,
                    Content = m.Content,
                    TimeStamp = m.CreatedAt.ToLocalTime()
                });
                MessageText = "";
                RaisePropertyChanged(nameof(Messages));
                MessageReceived?.Invoke(m, EventArgs.Empty);
            });
        }

        public void Dispose()
        {
            _miunie.Impersonation.MessageReceived -= MessageReceivedHandler;
            _miunie.Impersonation.UnsubscribeForMessages();
        }
    }
}
