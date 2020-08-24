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
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Services
{
    public class ProfileServiceTests
    {
        private readonly Mock<IDiscordMessages> _msgMock;
        private readonly ProfileService _profileService;

        public ProfileServiceTests()
        {
            _msgMock = new Mock<IDiscordMessages>();
            _profileService = new ProfileService(_msgMock.Object, null, null, null);
        }

        [Fact]
        public async Task ShowProfileAsync_ShouldExecuteOnce()
        {
            _ = _msgMock.Setup(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieUser>()))
                .Returns(Task.CompletedTask);

            await _profileService.ShowProfileAsync(null, null);

            _msgMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieUser>()), Times.Once);
        }

        [Fact]
        public async Task ShowGuildProfileAsync_ShouldExecuteOnce()
        {
            _ = _msgMock.Setup(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieGuild>()))
                .Returns(Task.CompletedTask);

            await _profileService.ShowGuildProfileAsync(null, null);

            _msgMock.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.IsAny<MiunieGuild>()), Times.Once);
        }
    }
}
