using System;

namespace Miunie.Core.Providers
{
    public interface ITimeManipulationProvider
    {
        TimeSpan? GetTimeSpanFromString(string timeframe, int units);
        (DateTime createdLocalTime, DateTime? editedLocalTime) GetMessageTimesLocalToUser(DateTime createdTime, DateTime? editedTime, MiunieUser user);
    }
}
