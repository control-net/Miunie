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
using Discord.WebSocket;
using Miunie.WindowsApp.Models;

namespace Miunie.WindowsApp.ViewModels
{
    public class ImpersonationChatPageViewModel : ViewModelBase
    {
        private TextChannelView _selectedChannel;

        public TextChannelView SelectedChannel
        {
            get => _selectedChannel;
            set
            {
                _selectedChannel = value;
                RaisePropertyChanged(nameof(SelectedChannel));
                RaisePropertyChanged(nameof(IsMessageTextboxEnabled));
                LoadMessagesAsync();
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

        private ObservableCollection<ObservableMessageView> _messages;

        public ObservableCollection<ObservableMessageView> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }

        private string _messageText;

        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                RaisePropertyChanged(nameof(MessageText));
            }
        }

        public bool IsMessageTextboxEnabled => _selectedChannel != null;

        public ICommand SendMessageCommand => new RelayCommand<string>(SendMessageAsMiunieAsync, CanSendMessage);

        private readonly MiunieBot _miunie;
        private ulong _currentGuildId;

        public ImpersonationChatPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            _channels = new List<TextChannelView>();
            _messages = new ObservableCollection<ObservableMessageView>();
        }

        internal void CleanupHandlers()
        {
            _miunie.Impersonation.MessageReceived -= Client_MessageReceivedHandler;
            _miunie.Impersonation.UnsubscribeForMessages();
        }

        internal void ConfigureMessagesSubscription()
        {
            _miunie.Impersonation.MessageReceived += Client_MessageReceivedHandler;
            _miunie.Impersonation.SubscribeForMessages();
        }

        internal async Task FetchInfoAsync(ulong guildId)
        {
            _currentGuildId = guildId;
            Channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(_currentGuildId);
        }

        private bool CanSendMessage(string arg)
        {
            return !string.IsNullOrWhiteSpace(_messageText) && _selectedChannel != null;
        }

        private async void SendMessageAsMiunieAsync(string message)
        {
            MessageText = "";
            await _miunie.Impersonation.SendTextToChannelAsync(message, SelectedChannel.Id);
        }

        private async void LoadMessagesAsync()
        {
            Messages.Clear();

            if (_selectedChannel != null)
            {
                var currentMessages = await _miunie.Impersonation.GetMessagesFromTextChannelAsync(_currentGuildId, _selectedChannel.Id);
                var sortedMessages = currentMessages.OrderBy(x => x.TimeStamp).Select(m => new ObservableMessageView
                {
                    AuthorAvatarUrl = m.AuthorAvatarUrl,
                    AuthorName = m.AuthorName,
                    Content = m.Content,
                    TimeStamp = m.TimeStamp
                });

                Messages = new ObservableCollection<ObservableMessageView>(currentMessages
                    .OrderBy(x => x.TimeStamp)
                    .Select(m => new ObservableMessageView
                    {
                        AuthorAvatarUrl = m.AuthorAvatarUrl,
                        AuthorName = m.AuthorName,
                        Content = m.Content,
                        TimeStamp = m.TimeStamp
                    }));
            }
        }

        private async void Client_MessageReceivedHandler(object sender, EventArgs e)
        {
            var m = (SocketMessage)sender;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                if (m.Channel.Id == SelectedChannel?.Id)
                {
                    Messages.Add(new ObservableMessageView
                    {
                        AuthorAvatarUrl = m.Author.GetAvatarUrl(),
                        AuthorName = m.Author.Username,
                        Content = m.Content,
                        TimeStamp = m.CreatedAt.ToLocalTime(),
                        Images = new ObservableCollection<ObservableImage>(m.Attachments
                                        .Select(x => new ObservableImage
                                        {
                                            ProxyUrl = x.ProxyUrl,
                                            Width = x.Width,
                                            Height = x.Height
                                        }))
                    });
                }
            });
        }
    }
}
