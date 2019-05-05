namespace Miunie.Core.Providers
{
    public interface IUserReputationProvider
    {
        int TimeoutInSeconds { get; }
        void AddReputation(MiunieUser invoker, MiunieUser target);
        void RemoveReputation(MiunieUser invoker, MiunieUser target);
        bool TryAddReputation(MiunieUser invoker, MiunieUser target);
        bool TryRemoveReputation(MiunieUser invoker, MiunieUser target);
    }
}
