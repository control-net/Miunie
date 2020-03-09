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
using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Services
{
    public class YesNoServiceTests
    {
        [Fact]
        public async Task YesNo_ShouldDelegateProperly()
        {
            var messagesMock = new Mock<IDiscordMessages>();
            var service = new MiscService(messagesMock.Object);

            await service.SendRandomYesNoAnswerAsync(new MiunieChannel()).ConfigureAwait(false);

            messagesMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.Is<PhraseKey>(pk => pk == PhraseKey.YES_NO_MAYBE)));
        }
    }
}
