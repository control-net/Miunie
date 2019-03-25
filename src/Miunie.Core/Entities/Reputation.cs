using System;
using System.Collections.Concurrent;

namespace Miunie.Core
{
    public class Reputation
    {
        private const int TimeoutInSeconds = 1800;

        public int Value { get; set; }
        public ConcurrentDictionary<ulong, DateTime> PlusRepLog { get; set; }
        public ConcurrentDictionary<ulong, DateTime> MinusRepLog { get; set; }

        public Reputation()
        {   
            PlusRepLog = new ConcurrentDictionary<ulong, DateTime>();
            MinusRepLog = new ConcurrentDictionary<ulong, DateTime>();
        }

        public void GiveRepFrom(MiunieUser source)
        {
            Value++;
            PlusRepLog.TryAdd(source.Id, DateTime.Now);
        }

        public void RemoveRepFrom(MiunieUser source)
        {
            Value--;
            MinusRepLog.TryAdd(source.Id, DateTime.Now);
        }

        public bool CanGetPlusRepFrom(ulong targetId)
            => TimeoutReached(PlusRepLog, targetId);

        public bool CanGetMinusRepFrom(ulong targetId)
            => TimeoutReached(MinusRepLog, targetId);

        private bool TimeoutReached(ConcurrentDictionary<ulong, DateTime> log, ulong targetId)
        {
            log.TryGetValue(targetId, out DateTime value);

            if ((DateTime.Now - value).TotalSeconds <= TimeoutInSeconds) { return false; }

            log.TryRemove(targetId, out DateTime ignore);
            return true;
        } 
    }
}
