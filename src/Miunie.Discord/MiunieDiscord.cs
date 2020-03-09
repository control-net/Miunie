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
using Miunie.Core.Discord;
using Miunie.Core.Entities.Discord;
using Miunie.Core.Logging;
using Miunie.Discord.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Discord
{
    public class MiunieDiscord : IDiscordConnection
    {
        private readonly IDiscord _discord;
        private readonly DiscordLogger _discordLogger;
        private readonly ILogWriter _logger;
        private readonly CommandHandler _commandHandler;
        private Core.Entities.ConnectionState _connectionState;

        public MiunieDiscord(IDiscord discord, DiscordLogger discordLogger, ILogWriter logger, CommandHandler commandHandler)
        {
            _discord = discord;
            _discordLogger = discordLogger;
            _logger = logger;
            _commandHandler = commandHandler;

            _connectionState = Core.Entities.ConnectionState.DISCONNECTED;
        }

        public event EventHandler ConnectionChanged;

        public Core.Entities.ConnectionState ConnectionState
        {
            get => _connectionState;
            private set
            {
                _connectionState = value;
                ConnectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool UserIsMiunie(MiunieUser user)
            => user.UserId == _discord.Client?.CurrentUser?.Id;

        public string GetBotAvatarUrl()
            => _discord.Client?.CurrentUser?.GetAvatarUrl();

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            ConnectionState = Core.Entities.ConnectionState.CONNECTING;

            try
            {
                await _discord.InitializeAsync();
                _discord.Client.Log += _discordLogger.Log;
                _discord.Client.Ready += ClientOnReady;
                await _commandHandler.InitializeAsync();
                await _discord.Client.StartAsync();
                await Task.Delay(-1, cancellationToken);
            }
            catch (Exception ex)
            {
                if (_discord.Client != null)
                {
                    await _discord.Client.LogoutAsync();
                    _discord.DisposeOfClient();
                }

                _logger.LogError(ex.Message);
            }
            finally
            {
                ConnectionState = Core.Entities.ConnectionState.DISCONNECTED;
            }
        }

        private Task ClientOnReady()
        {
            _logger.Log("Client Ready");
#if DEBUG
            _discord.Client.SetGameAsync("Herself being created.", type: ActivityType.Watching);
#endif
            ConnectionState = Core.Entities.ConnectionState.CONNECTED;
            return Task.CompletedTask;
        }
    }
}
