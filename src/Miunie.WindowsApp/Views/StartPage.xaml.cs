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

using GalaSoft.MvvmLight.Ioc;
using Miunie.Core;
using Miunie.Core.Entities;
using Miunie.WindowsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;

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

            _ = MainFrame.Navigate(newPage, null, transitionInfo);
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
            _ = await aboutDialog.ShowAsync();
        }
    }
}
