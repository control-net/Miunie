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
    public class RemoteRepositoryCommand : ModuleBase<SocketCommandContext>
    {
        private readonly RemoteRepositoryService _remoteRepoService;
        private readonly EntityConvertor _entityConvertor;

        public RemoteRepositoryCommand(RemoteRepositoryService remoteRepoService, EntityConvertor entityConvertor)
        {
            _remoteRepoService = remoteRepoService;
            _entityConvertor = entityConvertor;
        }

        [Command("repo")]
        [Summary("Shows the official remote repository hosting the code of this bot")]
        public async Task ShowRepository()
        {
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _remoteRepoService.ShowRepositoryAsync(channel);
        }
    }
}
