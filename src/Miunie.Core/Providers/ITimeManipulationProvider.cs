using System;

namespace Miunie.Core.Providers
{
    public interface ITimeManipulationProvider
    {
        TimeSpan? GetTimeSpanFromString(string timeframe, int units);
        DateTime? GetDateTimeLocalToUser(DateTime? utcDateTime, MiunieUser user);
    }
}
