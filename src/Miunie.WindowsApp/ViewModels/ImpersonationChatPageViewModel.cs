// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Miunie.Core;
using Miunie.Core.Entities.Views;
using Miunie.Core.Events;
using Miunie.WindowsApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Miunie.WindowsApp.ViewModels
{
    public class ImpersonationChatPageViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;

        private TextChannelView _selectedChannel;
        private IEnumerable<TextChannelView> _channels;
        private ObservableCollection<ObservableMessageView> _messages;
        private string _messageText;
        private ulong _currentGuildId;

        public ImpersonationChatPageViewModel(MiunieBot miunie)
        {
            _miunie = miunie;
            _channels = new List<TextChannelView>();
            _messages = new ObservableCollection<ObservableMessageView>();
        }

        public TextChannelView SelectedChannel
        {
            get => _selectedChannel;
            set
            {
                _selectedChannel = value;
                RaisePropertyChanged(nameof(SelectedChannel));
                RaisePropertyChanged(nameof(IsMessageTextboxEnabled));
                RaisePropertyChanged(nameof(SendMessageInputPlaceholder));
                LoadMessagesAsync();
            }
        }

        public IEnumerable<TextChannelView> Channels
        {
            get => _channels;
            set
            {
                _channels = value;
                RaisePropertyChanged(nameof(Channels));
            }
        }

        public ObservableCollection<ObservableMessageView> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }

        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                RaisePropertyChanged(nameof(MessageText));
            }
        }

        public bool IsMessageTextboxEnabled => _selectedChannel?.CanSendMessages ?? false;

        public string SendMessageInputPlaceholder => _selectedChannel?.CanSendMessages ?? true ? "Type your message here." : "This channel is read only.";

        public ICommand SendMessageCommand => new RelayCommand<string>(SendMessageAsMiunieAsync, CanSendMessage);

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
            MessageText = string.Empty;
            await _miunie.Impersonation.SendTextToChannelAsync(message, SelectedChannel.Id);
        }

        private async void LoadMessagesAsync()
        {
            Messages.Clear();

            if (_selectedChannel != null)
            {
                var messages = await _miunie.Impersonation.GetMessagesFromTextChannelAsync(_currentGuildId, _selectedChannel.Id);

                Messages = new ObservableCollection<ObservableMessageView>(messages
                    .OrderBy(x => x.TimeStamp)
                    .Select(ToObservableMessageView));
            }
        }

        private void Client_MessageReceivedHandler(object sender, MessageReceivedEventArgs args)
        {
            var m = args.Message;

            if (m.ChannelId == SelectedChannel?.Id)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    Messages.Add(ToObservableMessageView(m));
                });
            }
        }

        private ObservableMessageView ToObservableMessageView(MessageView m) => new ObservableMessageView
        {
            AuthorAvatarUrl = m.AuthorAvatarUrl,
            AuthorName = m.AuthorName,
            Content = m.Content,
            TimeStamp = m.TimeStamp
        };
    }
}
