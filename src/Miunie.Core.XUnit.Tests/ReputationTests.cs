using Xunit;
using Moq;
using System.Threading.Tasks;

namespace Miunie.Core.XUnit.Tests
{
    public class ReputationTests
    {
        private const ulong TestGuildId = 420;

        private readonly MiunieUser Senne = new MiunieUser
        {
            Id = 69420911,
            GuildId = TestGuildId,
            Name = "Senne",
            Reputation = new Reputation()
        };

        private readonly MiunieUser Peter = new MiunieUser
        {
            Id = 182941761801420802,
            GuildId = TestGuildId,
            Name = "Peter",
            Reputation = new Reputation()
        };

        private readonly MiunieUser Drax = new MiunieUser
        {
            Id = 123456789,
            GuildId = TestGuildId,
            Name = "Drax",
            Reputation = new Reputation()
        };

        private readonly MiunieChannel TestChannel = new MiunieChannel();

        [Fact]
        public async Task ShouldGiveReputationAndRespond()
        {
            const int ExpectedRep = 1;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.GiveReputation(Drax, Senne, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
        }

        [Fact]
        public async Task ShouldNotGiveReputationTwice()
        {
            const int ExpectedRep = 1;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.GiveReputation(Peter, Senne, TestChannel);
            await sut.GiveReputation(Peter, Senne, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
        }

        [Fact]
        public async Task ShouldNotGiveReputationToSelf()
        {
            const int ExpectedRep = 0;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.GiveReputation(Senne, Senne, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
        }

        [Fact]
        public async Task ShouldNotGiveReputationTwiceToOneUser()
        {
            const int ExpectedRep = 1;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.GiveReputation(Peter, Senne, TestChannel);
            await sut.GiveReputation(Peter, Senne, TestChannel);
            await sut.GiveReputation(Peter, Drax, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
            Assert.Equal(ExpectedRep, Drax.Reputation.Value);
        }

        [Fact]
        public async Task ShouldRemoveReputationAndRespond()
        {
            const int ExpectedRep = -1;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.RemoveReputation(Drax, Senne, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
        }

        [Fact]
        public async Task ShouldNotRemoveReputationTwice()
        {
            const int ExpectedRep = -1;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.RemoveReputation(Peter, Senne, TestChannel);
            await sut.RemoveReputation(Peter, Senne, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
        }

        [Fact]
        public async Task ShouldNotRemoveReputationFromSelf()
        {
            const int ExpectedRep = 0;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.RemoveReputation(Senne, Senne, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
        }

        [Fact]
        public async Task ShouldNotRemoveReputationTwiceFromOneUser()
        {
            const int ExpectedRep = -1;
            var miunieUserServiceMock = GetUserServiceMock();
            var discordMessageMock = new Mock<IDiscordMessages>();
            var sut = new ProfileService(discordMessageMock.Object, miunieUserServiceMock.Object);

            await sut.RemoveReputation(Peter, Senne, TestChannel);
            await sut.RemoveReputation(Peter, Senne, TestChannel);
            await sut.RemoveReputation(Peter, Drax, TestChannel);

            discordMessageMock.Verify(dm => dm.SendMessage(It.IsAny<MiunieChannel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce());
            Assert.Equal(ExpectedRep, Senne.Reputation.Value);
            Assert.Equal(ExpectedRep, Drax.Reputation.Value);
        }

        private Mock<IMiunieUserService> GetUserServiceMock()
        {
            var miunieUserServiceMock = new Mock<IMiunieUserService>();
            miunieUserServiceMock
                .Setup(us => us.GetById(Senne.Id, TestGuildId))
                .Returns(Senne);

            miunieUserServiceMock
                .Setup(us => us.GetById(Peter.Id, TestGuildId))
                .Returns(Peter);
            
            miunieUserServiceMock
                .Setup(us => us.GetById(Drax.Id, TestGuildId))
                .Returns(Drax);

            return miunieUserServiceMock;
        }
    }
}
