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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Miunie.WindowsApp.Views
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieStatusToSettings");
            animation?.TryStart(MiunieSettingsAvatar);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ConnectedAnimationService
                .GetForCurrentView()
                .PrepareToAnimate("MiunieSettingsToStatus", MiunieSettingsAvatar);
        }
    }
}
