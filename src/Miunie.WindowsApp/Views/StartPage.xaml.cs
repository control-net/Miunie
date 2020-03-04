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
using CommonServiceLocator;
using Miunie.Core.Providers;
using Miunie.Core;
using Windows.UI.Popups;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Miunie.Core.Entities;

namespace Miunie.WindowsApp.Views
{
    public sealed partial class StartPage : Page
    {
        private readonly List<(string Tag, Type Page)> _navigationPages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(StatusPage)),
            ("servers", typeof(ServersPage)),
            ("notconnected", typeof(NotConnectedPage))
        };

        private readonly StartPageViewModel _vm;
        private readonly MiunieBot _miunie;

        public StartPage()
        {
            InitializeComponent();
            _vm = DataContext as StartPageViewModel;
            _miunie = SimpleIoc.Default.GetInstance<MiunieBot>();
        }

        private void MainNavigationView_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainNavigationView.SelectedItem = MainNavigationView.MenuItems[0];
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());
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
                if (_miunie.MiunieDiscord.ConnectionState == ConnectionState.CONNECTED || navItemTag == "home")
                {
                    NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
                    return;
                }

                NavView_Navigate("notconnected", args.RecommendedNavigationTransitionInfo);
            }
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

        private async void AboutView_Navigate(object sender, TappedRoutedEventArgs e)
        {
            var aboutDialog = new MessageDialog(_vm.MiunieAboutText, "Miunie");

            aboutDialog.Commands.Add(new UICommand("Close"));

            aboutDialog.CancelCommandIndex = 0;
            await aboutDialog.ShowAsync();
        }
    }
}
