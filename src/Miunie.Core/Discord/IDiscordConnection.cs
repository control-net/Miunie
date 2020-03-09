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

using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordConnection
    {
        event EventHandler ConnectionChanged;

        ConnectionState ConnectionState { get; }

        bool UserIsMiunie(MiunieUser user);

        Task RunAsync(CancellationToken cancellationToken);

        string GetBotAvatarUrl();
    }
}
