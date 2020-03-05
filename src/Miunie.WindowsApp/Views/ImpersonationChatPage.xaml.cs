using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Miunie.WindowsApp.ViewModels;
using Miunie.Core;
using System;
using Windows.UI.Xaml.Input;
using System.Linq;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _vm.MessageReceived -= MessageReceivedHandler;
            _vm.CleanupHandlers();

            SimpleIoc.Default.Unregister<ImpersonationChatPageViewModel>();
            SimpleIoc.Default.Register<ImpersonationChatPageViewModel>();
        }

        private void MessageReceivedHandler(object sender, EventArgs e)
        {
            MessageList.ScrollIntoView(_vm.Messages.Last());
        }
    }
}
