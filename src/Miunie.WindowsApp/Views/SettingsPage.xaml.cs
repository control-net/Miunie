using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public partial class SettingsPage : Page
    {
        public static PasswordBox TokenBox;

        static readonly ApplicationDataContainer _localSettings =
            ApplicationData.Current.LocalSettings;

        public SettingsPage()
        {
            InitializeComponent();
            TokenBox = TokenTxtBox;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_localSettings.Values["token"] != null)
            {
                SetTokenBox(_localSettings.Values["token"].ToString());
            }

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("MiunieStatusToSettings");
            animation?.TryStart(MiunieSettingsAvatar);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _localSettings.Values["token"] = GetTokenBox();

            if (e.SourcePageType == typeof(StatusPage))
            {
                ConnectedAnimationService
                    .GetForCurrentView()
                    .PrepareToAnimate("MiunieSettingsToStatus", MiunieSettingsAvatar);
            }
        }

        public static string GetTokenStorage() => (string)_localSettings.Values["token"];

        protected virtual string GetTokenBox() => TokenBox.Password;

        protected virtual void SetTokenBox(string token) => TokenBox.Password = token;

    }
}
