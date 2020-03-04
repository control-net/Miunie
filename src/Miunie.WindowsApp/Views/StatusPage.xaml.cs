using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Miunie.WindowsApp.ViewModels;
using Windows.UI.Xaml.Media.Animation;

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

        private void OnAvatarChanged()
        {
            FadeOutAvatarStoryboard.Begin();

            if (_vm.IsConnecting) return;

            FadeOutAvatarStoryboard.Completed += (object sbs, object sbe) =>
            {
                FadeInAvatarStoryboard.Begin();
            };
        }

        private void SettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage), null);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.SourcePageType == typeof(SettingsPage))
            {
                ConnectedAnimationService
                    .GetForCurrentView()
                    .PrepareToAnimate("MiunieStatusToSettings", MiunieAvatar);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieSettingsToStatus");
            if (animation is null) { return; }
            animation.TryStart(MiunieAvatar);
            _vm.RaisePropertyChanged(nameof(_vm.SettingsButtonIsVisable));
        }
    }
}
