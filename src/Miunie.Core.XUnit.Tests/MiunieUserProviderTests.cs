using Miunie.Core.Providers;
using Miunie.Core.Storage;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Miunie.Core.XUnit.Tests
{
    public class MiunieUserProviderTests
    {
        private readonly Mock<IPersistentStorage> storage;
        private readonly MiunieUserProvider userProvider;

        public MiunieUserProviderTests()
        {
            storage = new Mock<IPersistentStorage>();
            userProvider = new MiunieUserProvider(storage.Object);
        }

        [Fact]
        public void GetById_ShouldFindAndReturnExpectedUser_IfValidIs()
        {
            var expected = new MiunieUser
            {
                GuildId = 0,
                Id = 0,
                Reputation = new Reputation()
            };
            storage.Setup(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(expected);

            var actual = userProvider.GetById(0, 0);

            storage.Verify(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1234,4321)]
        public void GetById_ShouldStoreAndReturnNewUser_IfUserNotFound(ulong userId, ulong guildID)
        {
            MiunieUser nullUser = null;
            storage.Setup(s => s.RestoreSingle(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(nullUser);
            storage.Setup(s => s.Store(It.IsAny<MiunieUser>()));

            var newUser = userProvider.GetById(userId, guildID);

            storage.Verify(s => s.Store(It.IsAny<MiunieUser>()), Times.Once);
            Assert.True(newUser.Id == userId && newUser.GuildId == guildID);
        }

        [Fact]
        public void StoreUser_ShouldUpdateUser_IfUserExists()
        {
            var user = new MiunieUser
            {
                GuildId = 0,
                Id = 0,
                Reputation = new Reputation()
            };
            storage.Setup(s => s.Store(It.IsAny<MiunieUser>()));
            storage.Setup(s => s.Update(It.IsAny<MiunieUser>()));
            storage.Setup(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(true);

            userProvider.StoreUser(user);

            storage.Verify(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            storage.Verify(s => s.Update(It.IsAny<MiunieUser>()), Times.Once);
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
            storage.Setup(s => s.Store(It.IsAny<MiunieUser>()));
            storage.Setup(s => s.Update(It.IsAny<MiunieUser>()));
            storage.Setup(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()))
                .Returns(false);

            userProvider.StoreUser(user);

            storage.Verify(s => s.Exists(It.IsAny<Expression<Func<MiunieUser, bool>>>()), Times.Once);
            storage.Verify(s => s.Store(It.IsAny<MiunieUser>()), Times.Once);
        }
    }
}
