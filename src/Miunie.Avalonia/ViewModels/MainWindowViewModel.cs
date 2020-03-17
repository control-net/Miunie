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

using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using Miunie.Avalonia.Utilities;
using Miunie.Core;
using Miunie.Core.Entities;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Miunie.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MiunieBot _miunie;
        private readonly UrlImageConverter _urlImageConverter;
        private readonly Bitmap _miunieBitmap;
        private string _connectionStatusText = "Disconnected";
        private Bitmap _botAvatarImage;
        private string _discordToken = string.Empty;

        public MainWindowViewModel(MiunieBot miunie, UrlImageConverter urlImageConverter)
        {
            _miunie = miunie;
            _urlImageConverter = urlImageConverter;
            _miunieBitmap = new Bitmap("Assets/miunie-icon.png");
            _botAvatarImage = _miunieBitmap;
            _miunie.MiunieDiscord.ConnectionChanged += ConectionStateChanged;
        }

        public string ConnectionStatusText
        {
            get => _connectionStatusText;
            set => _ = this.RaiseAndSetIfChanged(ref _connectionStatusText, value);
        }

        public Bitmap BotAvatarImage
        {
            get => _botAvatarImage;
            set => _ = this.RaiseAndSetIfChanged(ref _botAvatarImage, value);
        }

        public string DiscordToken
        {
            get => _discordToken;
            set => _ = this.RaiseAndSetIfChanged(ref _discordToken, value);
        }

        public async Task StartButton_ClickCommand()
        {
            if (string.IsNullOrWhiteSpace(DiscordToken))
            {
                return;
            }

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
