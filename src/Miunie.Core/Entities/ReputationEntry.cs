using System;

namespace Miunie.Core
{
    public class ReputationEntry
    {
        public ReputationEntry(ulong targetId, string targetName, DateTime givenAt, ReputationType type, bool fromInvoker = false)
        {
            TargetId = targetId;
            TargetName = targetName;
            GivenAt = givenAt;
            Type = type;
            FromInvoker = fromInvoker;
        }

        public ulong TargetId { get; set; }
        public string TargetName { get; set; }
        public DateTime GivenAt { get; set; }
        public ReputationType Type { get; set; }

        public bool FromInvoker { get; set; }
        
    }
}
