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

using Miunie.Core.Configuration;
using Miunie.Core.Discord;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public class MiunieBot
    {
        private CancellationTokenSource _tokenSource;

        public MiunieBot(IDiscordConnection miunieDiscord, IBotConfiguration botConfig, IDiscordImpersonation impersonation)
        {
            MiunieDiscord = miunieDiscord;
            BotConfiguration = botConfig;
            Impersonation = impersonation;
        }

        public IBotConfiguration BotConfiguration { get; }

        public IDiscordConnection MiunieDiscord { get; }

        public IDiscordImpersonation Impersonation { get; }

        public async Task StartAsync()
        {
            _tokenSource = new CancellationTokenSource();
            await MiunieDiscord.RunAsync(_tokenSource.Token);
        }

        public void Stop()
        {
            if (_tokenSource is null) { return; }
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }
    }
}
