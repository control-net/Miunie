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

using Miunie.Core.Entities.Discord;
using System;

namespace Miunie.Core.Providers
{
    public class TimeManipulationProvider : ITimeManipulationProvider
    {
        public TimeSpan? GetTimeSpanFromString(string timeframe, int units)
        {
            if (string.IsNullOrWhiteSpace(timeframe) || units <= 0)
            {
                return null;
            }

            timeframe = timeframe.Trim().ToLower();

            if (timeframe == "hours" || timeframe == "hour" || timeframe == "hrs" || timeframe == "hr")
            {
                return new TimeSpan(units, 0, 0);
            }
            else if (timeframe == "minutes" || timeframe == "minute" || timeframe == "mins" || timeframe == "min")
            {
                return new TimeSpan(0, units, 0);
            }
            else if (timeframe == "seconds" || timeframe == "second" || timeframe == "secs" || timeframe == "sec")
            {
                return new TimeSpan(0, 0, units);
            }
            else
            {
                return null;
            }
        }

        public DateTime? GetDateTimeLocalToUser(DateTime? utcDateTime, MiunieUser user)
        {
            if (!utcDateTime.HasValue)
            {
                return utcDateTime;
            }

            if (user.UtcTimeOffset.HasValue)
            {
                return utcDateTime.Value.Add(user.UtcTimeOffset.Value);
            }

            return utcDateTime;
        }
    }
}
