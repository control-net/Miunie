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

using Discord;
using Discord.WebSocket;
using Miunie.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace Miunie.Discord
{
    public class MiunieDiscordClient : IDiscord
    {
        private readonly IBotConfiguration _botConfig;

        public MiunieDiscordClient(IBotConfiguration botConfig)
        {
            _botConfig = botConfig;
        }

        public DiscordSocketClient Client { get; private set; }

        public async Task InitializeAsync()
        {
            if (string.IsNullOrWhiteSpace(_botConfig.DiscordToken))
            {
                throw new ArgumentNullException(nameof(_botConfig.DiscordToken));
            }

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
            });

            await Client.LoginAsync(TokenType.Bot, _botConfig.DiscordToken);
        }

        public void DisposeOfClient() => Client = null;
    }
}
