using Miunie.WindowsApp.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Miunie.WindowsApp.Views
{
    public sealed partial class SettingsPage : Page
    {
        private readonly SettingsPageViewModel _vm;

        public SettingsPage()
        {
            InitializeComponent();
            _vm = DataContext as SettingsPageViewModel;
            _vm.TokenApplied += TokenAppliedEventHandler;
        }

        private void TokenAppliedEventHandler(object sender, EventArgs e)
        {
            _ = Frame.Navigate(typeof(StatusPage), null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieStatusToSettings");
            _ = animation?.TryStart(MiunieSettingsAvatar);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.SourcePageType == typeof(StatusPage))
            {
                _ = ConnectedAnimationService
                    .GetForCurrentView()
                    .PrepareToAnimate("MiunieSettingsToStatus", MiunieSettingsAvatar);
            }
        }
    }
}
