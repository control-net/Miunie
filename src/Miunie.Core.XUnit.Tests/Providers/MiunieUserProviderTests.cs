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
            _hasDraxIdAndGuildId = u => u.Id == _users.Drax.Id && u.GuildId == _users.Drax.GuildId;
        }

        [Fact]
        public void GetById_ShouldFindAndReturnExpectedUser_IfUserFound()
        {
            var expected = _users.Drax;
            _storageMock.Setup(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(expected);
            _storageMock.Setup(s => s.Store(It.IsAny<MiunieUser>()));

            var actual = _userProvider.GetById(expected.Id, expected.GuildId);

            _storageMock.Verify(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.Is<MiunieUser>(_hasDraxIdAndGuildId)), Times.Never);
            Assert.True(expected.Id == actual.Id && expected.GuildId == actual.GuildId);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1234, 4321)]
        public void GetById_ShouldStoreAndReturnNewUser_IfUserNotFound(ulong userId, ulong guildID)
        {
            MiunieUser nullUser = null;
            _storageMock.Setup(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(nullUser);
            _storageMock.Setup(s => s.Store(nullUser));

            var newUser = _userProvider.GetById(userId, guildID);

            _storageMock.Verify(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.Is<MiunieUser>(u => u.Id == userId && u.GuildId == guildID)), Times.Once);
            Assert.True(newUser.Id == userId && newUser.GuildId == guildID);
        }

        [Fact]
        public void StoreUser_ShouldUpdateUser_IfUserExists()
        {
            _storageMock.Setup(s => s.Store(It.IsAny<MiunieUser>()));
            _storageMock.Setup(s => s.Update(It.IsAny<MiunieUser>()));
            _storageMock.Setup(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
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
                Id = 0,
                Reputation = new Reputation()
            };
            _storageMock.Setup(s => s.Store(It.IsAny<MiunieUser>()));
            _storageMock.Setup(s => s.Update(It.IsAny<MiunieUser>()));
            _storageMock.Setup(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(false);

            _userProvider.StoreUser(user);

            _storageMock.Verify(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            _storageMock.Verify(s => s.Store(It.IsAny<MiunieUser>()), Times.Once);
            _storageMock.Verify(s => s.Update(It.IsAny<MiunieUser>()), Times.Never);
        }
    }
}
