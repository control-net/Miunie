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

using Miunie.WindowsApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Miunie.WindowsApp.Views
{
    public sealed partial class StatusPage : Page
    {
        private readonly StatusPageViewModel _vm;

        public StatusPage()
        {
            InitializeComponent();
            _vm = DataContext as StatusPageViewModel;
            NavigationCacheMode = NavigationCacheMode.Enabled;
            _vm.AvatarChanged = OnAvatarChanged;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.SourcePageType == typeof(SettingsPage))
            {
                _ = ConnectedAnimationService
                    .GetForCurrentView()
                    .PrepareToAnimate("MiunieStatusToSettings", MiunieAvatar);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieSettingsToStatus");
            if (animation is null) { return; }
            _ = animation.TryStart(MiunieAvatar);
            _vm.RaisePropertyChanged(nameof(_vm.SettingsButtonIsVisable));
        }

        private void OnAvatarChanged()
        {
            FadeOutAvatarStoryboard.Begin();

            if (_vm.IsConnecting)
            {
                return;
            }

            FadeOutAvatarStoryboard.Completed += (object sbs, object sbe) =>
            {
                FadeInAvatarStoryboard.Begin();
            };
        }

        private void SettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _ = Frame.Navigate(typeof(SettingsPage), null);
        }
    }
}
