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
        private readonly Mock<ITimeManipulationProvider> _timeManipulator;
        private readonly TimeService _service;

        public TimeServiceTests()
        {
            _messages = new Mock<IDiscordMessages>();
            _dateTime = new Mock<IDateTime>();
            _users = new Mock<IMiunieUserProvider>();
            _timeManipulator = new Mock<ITimeManipulationProvider>();
            _service = new TimeService(_messages.Object, _dateTime.Object, _users.Object, _timeManipulator.Object);
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
