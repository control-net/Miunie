using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Miunie.WindowsApp.ViewModels;

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
            => _vm.FetchInfo((ulong)e.Parameter);
    }
}
