using System;
using System.Collections.Generic;
using System.Text;

namespace Miunie.Core.Providers
{
    public class TimeManipulationProvider : ITimeManipulationProvider
    {
        public TimeSpan? GetTimeSpanFromString(string timeframe, int units)
        {
            if (string.IsNullOrWhiteSpace(timeframe) || units <= 0)
                return null;

            timeframe = timeframe.Trim().ToLower();

            if (timeframe == "hours" || timeframe == "hour" || timeframe == "hrs" || timeframe == "hr")
                return new TimeSpan(units, 0, 0);
            else if (timeframe == "minutes" || timeframe == "minute" || timeframe == "mins" || timeframe == "min")
                return new TimeSpan(0, units, 0);
            else if (timeframe == "seconds" || timeframe == "second" || timeframe == "secs" || timeframe == "sec")
                return new TimeSpan(0, 0, units);
            else
                return null;
        }
    }
}
