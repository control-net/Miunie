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

namespace Miunie.WindowsApp.Views
{
    public sealed partial class StatusPage : Page
    {
        private readonly StatusPageViewModel _vm;

        public StatusPage()
        {
            this.InitializeComponent();
            _vm = DataContext as StatusPageViewModel;
        }

        private void ActionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _vm.ActionButtonEnabled = false;
            _vm.ToggleBotStart();
        }
    }
}
