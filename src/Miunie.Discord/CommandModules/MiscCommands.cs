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

using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Attributes;
using Miunie.Discord.Convertors;
using System.Threading.Tasks;

namespace Miunie.Discord.CommandModules
{
    [Name("Misc")]
    public class MiscCommands : ModuleBase<SocketCommandContext>
    {
        private readonly MiscService _service;
        private readonly EntityConvertor _entityConvertor;

        public MiscCommands(MiscService service, EntityConvertor entityConvertor)
        {
            _service = service;
            _entityConvertor = entityConvertor;
        }

        [Command("what do you think?")]
        [Summary("What do I think? I guess you'll have to find out~")]
        [Examples("what do you think?")]
        public async Task SendRandomYesNoMaybeAnswer()
        {
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.SendRandomYesNoAnswerAsync(c);
        }
    }
}
