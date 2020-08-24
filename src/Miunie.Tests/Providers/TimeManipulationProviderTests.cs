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

using Miunie.Core.Providers;
using Miunie.Core.XUnit.Tests.Data;
using Moq;
using System;
using Xunit;

namespace Miunie.Core.XUnit.Tests.Providers
{
    public class TimeManipulationProviderTests
    {
        private readonly ITimeManipulationProvider _provider;
        private readonly DummyMiunieUsers _dummyUsers;

        public TimeManipulationProviderTests()
        {
            _provider = new TimeManipulationProvider();
            _dummyUsers = new DummyMiunieUsers();
        }

        [Fact]
        public void InvalidTimeFrame_ShouldReturnNull()
        {
            var result = _provider.GetTimeSpanFromString("IncorrectTimeFrame", It.IsAny<int>());

            Assert.Null(result);
        }

        [Theory]
        [InlineData("hours", 1)]
        [InlineData("hrs", 10)]
        [InlineData("hr", 20)]
        [InlineData("hr", 30)]
        [InlineData("hours", 3000)]
        public void TimeFrameFromHours_ShouldReturnTimeSpanWithSetHours(string timeFrame, int amountOfHours)
        {
            var expected = new TimeSpan(amountOfHours, 0, 0);
            var actual = _provider.GetTimeSpanFromString(timeFrame, amountOfHours);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("minutes", 1)]
        [InlineData("minute", 10)]
        [InlineData("mins", 20)]
        [InlineData("min", 21)]
        public void TimeFrameFromMinutes_ShouldReturnTimeSpanWithSetMinutes(string timeFrame, int amountOfMinutes)
        {
            var expected = new TimeSpan(0, amountOfMinutes, 0);
            var actual = _provider.GetTimeSpanFromString(timeFrame, amountOfMinutes);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("seconds", 1)]
        [InlineData("second", 10)]
        [InlineData("secs", 20)]
        [InlineData("sec", 21)]
        public void TimeFrameFromSeconds_ShouldReturnTimeSpanWithSetSeconds(string timeFrame, int amountOfSeconds)
        {
            var expected = new TimeSpan(0, 0, amountOfSeconds);
            var actual = _provider.GetTimeSpanFromString(timeFrame, amountOfSeconds);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void ZeroOrNegativeAmountOfTime_ShouldReturnNull(int amountOfTime)
        {
            const string validTimeFrame = "hours";
            var result = _provider.GetTimeSpanFromString(validTimeFrame, amountOfTime);

            Assert.Null(result);
        }

        [Fact]
        public void EnteredDateTimeIsNull_ReturnsNull()
        {
            var result = _provider.GetDateTimeLocalToUser(null, _dummyUsers.DraxWithUtcTimeOffSet);

            Assert.Null(result);
        }

        [Fact]
        public void UserHasNoTimeSet_ReturnsEnteredDateTime()
        {
            var expectedDateTime = DateTime.UtcNow;

            var actual = _provider.GetDateTimeLocalToUser(expectedDateTime, _dummyUsers.PeterWithNoTimeSet);

            Assert.Equal(expectedDateTime, actual);
        }

        [Fact]
        public void DateTimeNullAndUserTimeNotSet_ShouldReturnNull()
        {
            var result = _provider.GetDateTimeLocalToUser(null, _dummyUsers.PeterWithNoTimeSet);

            Assert.Null(result);
        }

        [Fact]
        public void ValidDateTimeAndUserTimeSet_ShouldReturnDateTimeBasedOnUsersLocalTime()
        {
            var testDateTime = DateTime.UtcNow;
            var expectedUtcPlusOneDateTime = testDateTime.AddHours(1);

            var actual = _provider.GetDateTimeLocalToUser(testDateTime, _dummyUsers.PeterWithUtcPlusOneHourTimeSet);

            Assert.Equal(expectedUtcPlusOneDateTime, actual);
        }
    }
}
