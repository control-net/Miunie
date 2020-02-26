using System;

namespace Miunie.Core.Providers
{
    public interface ITimeManipulationProvider
    {
        TimeSpan? GetTimeSpanFromString(string timeframe, int units);
    }
}