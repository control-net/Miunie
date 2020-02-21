using Miunie.Core.Infrastructure;
using Miunie.Core.Providers;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Services
{
    public class TimeServiceTests
    {
        private readonly Mock<IDiscordMessages> _messages;
        private readonly Mock<IDateTime> _dateTime;
        private readonly Mock<IMiunieUserProvider> _users;
        private readonly TimeService _service;

        public TimeServiceTests()
        {
            _messages = new Mock<IDiscordMessages>();
            _dateTime = new Mock<IDateTime>();
            _users = new Mock<IMiunieUserProvider>();
            _service = new TimeService(_messages.Object, _dateTime.Object, _users.Object);
        }

        [Fact]
        public async Task NoTimezoneInfo_ShouldOutputWarning()
        {
            var channel = CreateTestChannel();
            var user = CreateUserWithoutOffset();

            await _service.OutputCurrentTimeForUserAsync(user, channel);

            AssertNoTimezoneInfoSent();
            AssertNoOtherMessages();
        }

        [Fact]
        public async Task CorrectOffset_ShouldOutputCorrectTime()
        {
            var channel = CreateTestChannel();
            var user = CreateUserWithOffset(TimeSpan.FromHours(2));
            SetCurrentTime(new DateTime(2020, 2, 21, 20, 30, 0));
            var expectedTime = new DateTime(2020, 2, 21, 22, 30, 0);

            await _service.OutputCurrentTimeForUserAsync(user, channel);

            AssertCorrectTimeInfoSent(expectedTime);
            AssertNoOtherMessages();
        }

        [Fact]
        public async Task SettingNewOffset_ShouldSaveAndOutputInfo()
        {
            SetCurrentTime(new DateTime(2020, 2, 22, 10, 59, 4));
            var channel = CreateTestChannel();
            var user = CreateUserWithoutOffset();
            var enteredDateTime = new DateTime(1, 1, 1, 8, 0, 0);
            var expectedOffset = TimeSpan.FromHours(-2);

            await _service.SetUtcOffsetForUserAsync(enteredDateTime, user, channel);

            AssertOffsetChangedInfoSent();
            AssertUserWithOffsetSaved(expectedOffset);
            AssertNoOtherMessages();
        }

        private MiunieChannel CreateTestChannel()
        {
            return new MiunieChannel
            {
                ChannelId = 10,
                GuildId = 900
            };
        }

        private MiunieUser CreateUserWithoutOffset()
        {
            return new MiunieUser
            {
                Name = "User",
                UtcTimeOffset = null
            };
        }

        private MiunieUser CreateUserWithOffset(TimeSpan offset)
        {
            return new MiunieUser
            {
                Name = "User",
                UtcTimeOffset = offset
            };
        }

        private void AssertOffsetChangedInfoSent()
        {
            _messages.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.Is<PhraseKey>(pk => pk == PhraseKey.TIME_NEW_OFFSET_SET)));
        }

        private void AssertUserWithOffsetSaved(TimeSpan expectedOffset)
        {
            _users.Verify(u => u.StoreUser(It.Is<MiunieUser>(u => u.UtcTimeOffset == expectedOffset)));
        }

        private void AssertCorrectTimeInfoSent(DateTime expectedTime)
        {
            _messages.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.Is<PhraseKey>(pk => pk == PhraseKey.TIME_TIMEZONE_INFO), It.IsAny<string>(), It.Is<DateTime>(dt => dt == expectedTime)));
        }

        private void AssertNoTimezoneInfoSent()
        {
            _messages.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.Is<PhraseKey>(pk => pk == PhraseKey.TIME_NO_TIMEZONE_INFO), It.IsAny<string>()));
        }

        private void AssertNoOtherMessages()
            => _messages.VerifyNoOtherCalls();

        private void SetCurrentTime(DateTime dateTime)
            => _dateTime.Setup(dt => dt.UtcNow).Returns(dateTime);
    }
}
