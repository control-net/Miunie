using System;
using System.Collections.Concurrent;

namespace Miunie.Core.Entities
{
    public class Reputation
    {

        public int Value { get; set; }
        public ConcurrentDictionary<ulong, DateTime> PlusRepLog { get; set; }
        public ConcurrentDictionary<ulong, DateTime> MinusRepLog { get; set; }

        public Reputation()
        {
            PlusRepLog = new ConcurrentDictionary<ulong, DateTime>();
            MinusRepLog = new ConcurrentDictionary<ulong, DateTime>();
        }
    }
}
