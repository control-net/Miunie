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
using Miunie.Core.Providers;
using Miunie.Core.Storage;
using Miunie.Core.XUnit.Tests.Data;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Providers
{
    public class MiunieUserProviderTests
    {
        private readonly Mock<IPersistentStorage> _storageMock;
        private readonly MiunieUserProvider _userProvider;
        private readonly DummyMiunieUsers _users;
        private readonly Expression<Func<MiunieUser, bool>> _hasDraxIdAndGuildId;

        public MiunieUserProviderTests()
        {
            _storageMock = new Mock<IPersistentStorage>();
            _userProvider = new MiunieUserProvider(_storageMock.Object);
            _users = new DummyMiunieUsers();
            _hasDraxIdAndGuildId = u => u.UserId == _users.Drax.UserId && u.GuildId == _users.Drax.GuildId;
        }

        [Fact]
        public void GetById_ShouldFindAndReturnExpectedUser_IfUserFound()
        {
            var expected = _users.Drax;
            _ = _storageMock.Setup(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(expected);
            _ = _storageMock.Setup(s => s.Store(It.IsAny<MiunieUser>()));

            var actual = _userProvider.GetById(expected.UserId, expected.GuildId);

            _storageMock.Verify(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.Is<MiunieUser>(_hasDraxIdAndGuildId)), Times.Never);
            Assert.True(expected.UserId == actual.UserId && expected.GuildId == actual.GuildId);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1234, 4321)]
        public void GetById_ShouldStoreAndReturnNewUser_IfUserNotFound(ulong userId, ulong guildID)
        {
            MiunieUser nullUser = null;
            _ = _storageMock.Setup(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(nullUser);
            _ = _storageMock.Setup(s => s.Store(nullUser));

            var newUser = _userProvider.GetById(userId, guildID);

            _storageMock.Verify(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.Is<MiunieUser>(u => u.UserId == userId && u.GuildId == guildID)), Times.Once);
            Assert.True(newUser.UserId == userId && newUser.GuildId == guildID);
        }

        [Fact]
        public void StoreUser_ShouldUpdateUser_IfUserExists()
        {
            _ = _storageMock.Setup(s => s.Store(It.IsAny<MiunieUser>()));
            _ = _storageMock.Setup(s => s.Update(It.IsAny<MiunieUser>()));
            _ = _storageMock.Setup(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(true);

            _userProvider.StoreUser(_users.Drax);

            _storageMock.Verify(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Update(It.IsAny<MiunieUser>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.IsAny<MiunieUser>()), Times.Never);
        }

        [Fact]
        public void StoreUser_ShouldStoreUser_IfUserDoesntExist()
        {
            var user = new MiunieUser
            {
                GuildId = 0,
                UserId = 0,
                Reputation = new Reputation()
            };
            _ = _storageMock.Setup(s => s.Store(It.IsAny<MiunieUser>()));
            _ = _storageMock.Setup(s => s.Update(It.IsAny<MiunieUser>()));
            _ = _storageMock.Setup(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(false);

            _userProvider.StoreUser(user);

            _storageMock.Verify(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.IsAny<MiunieUser>()), Times.Once);
            _storageMock.Verify(s => s.Update(It.IsAny<MiunieUser>()), Times.Never);
        }
    }
}
