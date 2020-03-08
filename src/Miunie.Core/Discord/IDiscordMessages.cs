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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordMessages
    {
        Task SendMessageAsync(MiunieChannel targetChannel, PhraseKey phraseKey, params object[] parameters);

        Task SendMessageAsync(MiunieChannel targetChannel, MiunieUser user);

        Task SendMessageAsync(MiunieChannel targetChannel, MiunieGuild guild);

        Task SendMessageAsync(MiunieChannel mc, IEnumerable<ReputationEntry> repEntries, int index);
    }
}
