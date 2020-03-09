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

using Miunie.Core.Attributes;
using Miunie.Core.Discord;
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using System.Threading.Tasks;

namespace Miunie.Core
{
    [Service]
    public class MiscService
    {
        private readonly IDiscordMessages _messages;

        public MiscService(IDiscordMessages messages)
        {
            _messages = messages;
        }

        public async Task SendRandomYesNoAnswerAsync(MiunieChannel channel)
        {
            await _messages.SendMessageAsync(channel, PhraseKey.YES_NO_MAYBE);
        }
    }
}
