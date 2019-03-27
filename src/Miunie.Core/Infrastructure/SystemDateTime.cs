using System;

namespace Miunie.Core.Infrastructure
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

