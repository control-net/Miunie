using Miunie.WindowsApp.ViewModels;
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
        private readonly SettingsPageViewModel _vm;

        public SettingsPage()
        {
            InitializeComponent();
            _vm = DataContext as SettingsPageViewModel;
            _vm.TokenApplied += TokenAppliedEventHandler;
        }

        private void TokenAppliedEventHandler(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(StatusPage), null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieStatusToSettings");
            animation?.TryStart(MiunieSettingsAvatar);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.SourcePageType == typeof(StatusPage))
            {
                ConnectedAnimationService
                    .GetForCurrentView()
                    .PrepareToAnimate("MiunieSettingsToStatus", MiunieSettingsAvatar);
            }
        }
    }
}
