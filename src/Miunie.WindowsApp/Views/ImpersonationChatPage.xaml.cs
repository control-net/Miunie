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
using Miunie.Core;

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
            this.InitializeComponent();
            _vm = DataContext as ImpersonationChatPageViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) 
            => _vm.FetchInfo((ulong)e.Parameter);

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = e.OriginalSource as Button;
                var panel = button.Parent as Grid;
                var textBox = panel.Children.FirstOrDefault(element => element is TextBox) as TextBox;
                var text = textBox.Text;
                textBox.Text = string.Empty;

                object i = ((FrameworkElement)sender).DataContext;
                var vm = i as TextChannelView;

                await _vm.SendMessageAsMiunieAsync(text, vm.Id);
            }
            catch (NullReferenceException)
            {
                return;
            }
        }
    }
}
