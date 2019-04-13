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
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Extensions.DependencyInjection;
using Miunie.Core;
using Miunie.Core.Logging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Miunie.WindowsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly MiunieBot _miunie;

        public MainPage()
        {
            this.InitializeComponent();
            DispatcherHelper.Initialize();
            var logger = InversionOfControl.Provider.GetRequiredService<ILogger>() as UwpLogger;
            if (!(logger is null))
            {
                logger.LogReceived += LoggerOnLogReceived;
            }
            
            _miunie = ActivatorUtilities.CreateInstance<MiunieBot>(InversionOfControl.Provider);
        }

        private void LoggerOnLogReceived(object sender, EventArgs e)
        {
            var args = e as LogEventArgs;
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                LogListBox.Items?.Add(args?.Message);
            });
        }

        private void StartMiunieBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _ = _miunie.StartAsync();
            StartMiunieBtn.IsEnabled = false;
        }

        private void ApplyTokenBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var token = TokenPasswordBox.Password;
            _miunie.BotConfiguration.DiscordToken = token;
        }
    }
}
