using System.Collections.Generic;

namespace Miunie.Core.Providers
{
    public interface IUserReputationProvider
    {
        int TimeoutInSeconds { get; }
        void AddReputation(MiunieUser invoker, MiunieUser target);
        void RemoveReputation(MiunieUser invoker, MiunieUser target);
        bool CanAddReputation(MiunieUser invoker, MiunieUser target);
        bool CanRemoveReputation(MiunieUser invoker, MiunieUser target);
        List<ReputationEntry> GetReputation(MiunieUser user);
    }
}
