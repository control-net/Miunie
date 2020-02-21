using System;

namespace Miunie.Core.Infrastructure
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}

