using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Miunie.WindowsApp.ViewModels;
using muxc = Microsoft.UI.Xaml.Controls;

namespace Miunie.WindowsApp.Views
{
    public sealed partial class StartPage : Page
    {
        private readonly List<(string Tag, Type Page)> _navigationPages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(StatusPage))
        };

        private bool _shouldCheckForClipboardToken = true;
        private readonly StartPageViewModel _vm;

        public StartPage()
        {
            InitializeComponent();
            _vm = DataContext as StartPageViewModel;
        }

        private void MainNavigationView_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainNavigationView.SelectedItem = MainNavigationView.MenuItems[0];
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());
        }

        private void StartPage_OnGotFocus(object sender, RoutedEventArgs e)
        {
            CheckForTokenInClipboard();
        }

        private void MainNavigationView_OnItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private async void CheckForTokenInClipboard()
        {
            if (!_shouldCheckForClipboardToken) { return; }
            _shouldCheckForClipboardToken = false;

            var clipboardContent = Clipboard.GetContent();

            if (!clipboardContent.AvailableFormats.Contains(StandardDataFormats.Text)) { return; }

            var possibleToken = await Clipboard.GetContent().GetTextAsync();

            if (!_vm.TokenValidator.StringHasValidTokenStructure(possibleToken)) { return; }

            var clipboardTokenDialog = new ContentDialog
            {
                Title = "Paste copied bot token?",
                Content = "It looks like you have a bot token copied.\nDo you want to use it?",
                PrimaryButtonText = "Sure",
                CloseButtonText = "No, thanks"
            };

            var result = await clipboardTokenDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) { _vm.ApplyToken(possibleToken); }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            var newPage = GetPageTypeByNavItemTag(navItemTag);

            if (newPage is null || MainFrame.CurrentSourcePageType == newPage) { return; }

            MainFrame.Navigate(newPage, null, transitionInfo);
        }

        private Type GetPageTypeByNavItemTag(string navItemTag)
        {
            Type newPage;

            if (navItemTag == "settings")
            {
                newPage = typeof(SettingsPage);
            }
            else
            {
                var (_, type) = _navigationPages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                newPage = type;
            }

            return newPage;
        }
    }
}
