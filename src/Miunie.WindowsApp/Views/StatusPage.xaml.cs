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
            this.InitializeComponent();
            _vm = DataContext as StatusPageViewModel;
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void ActionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _vm.ActionButtonEnabled = false;
            _vm.ToggleBotStart();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ConnectedAnimationService
                .GetForCurrentView()
                .PrepareToAnimate("MiunieStatusToSettings", MiunieAvatar);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieSettingsToStatus");
            if (animation is null) { return; }
            animation.TryStart(MiunieAvatar);
        }
    }
}
