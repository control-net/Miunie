namespace Miunie.Core.Providers
{
    public interface IUserReputationProvider
    {
        void AddReputation(MiunieUser invoker, MiunieUser target);
        void RemoveReputation(MiunieUser invoker, MiunieUser target);
        bool AddReputationHasTimeout(MiunieUser invoker, MiunieUser target);
        bool RemoveReputationHasTimeout(MiunieUser invoker, MiunieUser target);
    }
}
