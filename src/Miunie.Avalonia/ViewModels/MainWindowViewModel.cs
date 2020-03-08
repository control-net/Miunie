using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Miunie.Avalonia.Utilities;
using Miunie.Core;
using Miunie.Core.Entities;
using Miunie.Core.Logging;
using ReactiveUI;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Miunie.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;
        private readonly UrlImageConverter _urlImageConverter;
        private readonly Bitmap _miunieBitmap;

        public MainWindowViewModel()
        {
            _miunie = ActivatorUtilities.CreateInstance<MiunieBot>(InversionOfControl.Provider);
            _urlImageConverter = InversionOfControl.Provider.GetRequiredService<UrlImageConverter>();
            _miunieBitmap = new Bitmap("Assets/miunie-icon.png");
            _botAvatarImage = _miunieBitmap;
            _miunie.MiunieDiscord.ConnectionChanged += ConectionStateChanged;
        }

        private string _connectionStatusText = "Disconnected";

        public string ConnectionStatusText 
        { 
            get { return _connectionStatusText; }
            set { this.RaiseAndSetIfChanged(ref _connectionStatusText, value); }
        }

        private Bitmap _botAvatarImage;

        public Bitmap BotAvatarImage
        {
            get { return _botAvatarImage; }
            set { this.RaiseAndSetIfChanged(ref _botAvatarImage, value); }
        }

        private string _discordToken = "";

        public string DiscordToken 
        { 
            get { return _discordToken; }
            set { this.RaiseAndSetIfChanged(ref _discordToken, value); }
        }

        public async Task StartButton_ClickCommand()
        {
            if (string.IsNullOrWhiteSpace(DiscordToken))
                return;

            _miunie.BotConfiguration.DiscordToken = DiscordToken;
            await _miunie.StartAsync();
        }

        public void StopButton_ClickCommand()
        {
            if (_miunie.MiunieDiscord.ConnectionState is ConnectionState.CONNECTED)
            {
                _miunie.Stop();
            }
        }

        private void ConectionStateChanged(object sender, EventArgs e)
        {
            UpdateConectionStateText();
            UpdateBotAvatar();
        }

        private void UpdateConectionStateText()
        {
            ConnectionStatusText = _miunie.MiunieDiscord.ConnectionState switch
            {
                ConnectionState.CONNECTING => "Connecting",
                ConnectionState.CONNECTED => "Connected",
                ConnectionState.DISCONNECTED => "Disconnected",
                _ => "Unknown State"
            };
        }

        private async void UpdateBotAvatar()
        {
            BotAvatarImage = _miunie.MiunieDiscord.ConnectionState switch
            {
                ConnectionState.CONNECTED => await _urlImageConverter.UrlToBitmapAsync(_miunie.MiunieDiscord.GetBotAvatarUrl()),
                ConnectionState.CONNECTING => _miunieBitmap,
                ConnectionState.DISCONNECTED => _miunieBitmap,
                _ => _miunieBitmap
            };
        }
    }
}
