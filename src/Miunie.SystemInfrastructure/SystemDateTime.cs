using System;
using Miunie.Core.Infrastructure;

namespace Miunie.SystemInfrastructure
{
    public class SystemDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
