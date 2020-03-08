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

using Miunie.Core.Discord;
using Miunie.Core.Entities.Discord;
using Miunie.Discord.Convertors;
using System.Threading.Tasks;

namespace Miunie.Discord.Adapters
{
    public class DiscordGuildsAdapter : IDiscordGuilds
    {
        private readonly IDiscord _discord;
        private readonly EntityConvertor _entityConvertor;

        public DiscordGuildsAdapter(IDiscord discord, EntityConvertor entityConvertor)
        {
            _discord = discord;
            _entityConvertor = entityConvertor;
        }

        public Task<MiunieGuild> FromAsync(MiunieUser user)
        {
            var guild = _discord.Client.GetGuild(user.GuildId);

            return Task.FromResult(_entityConvertor.ConvertGuild(guild));
        }
    }
}
