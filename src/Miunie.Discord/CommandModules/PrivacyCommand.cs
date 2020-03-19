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
using Miunie.Discord.Convertors;
using System.Threading.Tasks;

namespace Miunie.Discord.CommandModules
{
    public class PrivacyCommand : ModuleBase<SocketCommandContext>
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly PrivacyService _service;

        public PrivacyCommand(PrivacyService service, EntityConvertor entityConvertor)
        {
            _entityConvertor = entityConvertor;
            _service = service;
        }

        [Command("personal data")]
        public async Task GetMyPersonalData()
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            await _service.OutputUserJsonDataAsync(u);
        }

        [Command("personal data remove")]
        public async Task RemoveMyPersonalData()
        {
            var u = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var c = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _service.RemoveUserData(u, c);
        }
    }
}
