using System;

namespace Miunie.Core.Providers
{
    public interface ITimeManipulationProvider
    {
        TimeSpan? GetTimeSpanFromString(string timeframe, int units);
        MessageTimes GetMessageTimesLocalToUser(DateTime createdUtcTime, DateTime? editedUtcTime, MiunieUser user);
    }
}
