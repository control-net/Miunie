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
using Miunie.Discord;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieDiscordTests
    {
        [Fact]
        public async Task RunAsync_ShouldThrowException_IfInvalidToken()
        {
            var config = Mock.Of<IBotConfiguration>(c => c.DiscordToken == "ObviouslyFakeToken");
            var discord = new Mock<MiunieDiscordClient>(config);

            var miunie = new MiunieDiscord(discord.Object, null, null, null);

            var ex = await Record.ExceptionAsync(async () => await miunie.RunAsync(CancellationToken.None));

            Assert.NotNull(ex);
        }
    }
}
