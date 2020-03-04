using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Miunie.WindowsApp.ViewModels;
using Miunie.Core;
using System;
using Windows.UI.Xaml.Input;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Miunie.WindowsApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImpersonationChatPage : Page
    {
        private readonly ImpersonationChatPageViewModel _vm;

        public ImpersonationChatPage()
        {
            InitializeComponent();
            _vm = DataContext as ImpersonationChatPageViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm.FetchInfo((ulong)e.Parameter);
            _vm.ConfigureMessagesSubscription();
            _vm.MessageReceived += MessageReceivedHandler;
        }

        private void MessageReceivedHandler(object sender, EventArgs e)
        {
            MessageList.ScrollIntoView(_vm.Messages.Last());
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _vm.MessageReceived -= MessageReceivedHandler;
            _vm.Dispose();
        }
        
        private void MessageTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && _vm.SelectedChannel != null && _vm.SendMessageCommand.CanExecute(MessageTextBox.Text))
            {
                _vm.SendMessageCommand.Execute(MessageTextBox.Text);
            }
        }
    }
}
