using Miunie.Core.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Services
{
    public class TimeServiceTests
    {
        private readonly Mock<IDiscordMessages> _messages;
        private readonly TimeService _service;

        public TimeServiceTests()
        {
            _messages = new Mock<IDiscordMessages>();
            _service = new TimeService(_messages.Object);
        }

        [Fact]
        public async Task NoTimezoneInfo_ShouldOutputWarning()
        {
            MiunieChannel channel = CreateTestChannel();
            MiunieUser user = CreateUserWithoutTimeZone();

            await _service.OutputCurrentTimeForUserAsync(user, channel);

            AssertNoTimezoneInfoSent();
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

        private MiunieUser CreateUserWithoutTimeZone()
        {
            return new MiunieUser
            {
                Name = "User",
                UtcTimeOffset = null
            };
        }

        private void AssertNoTimezoneInfoSent()
        {
            _messages.Verify(m => m.SendMessageAsync(It.IsAny<MiunieChannel>(), It.Is<PhraseKey>(pk => pk == PhraseKey.TIME_NO_TIMEZONE_INFO)));
        }

        private void AssertNoOtherMessages()
        {
            _messages.VerifyNoOtherCalls();
        }
    }
}
