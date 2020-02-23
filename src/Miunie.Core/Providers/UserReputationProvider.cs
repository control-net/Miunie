using System;
using System.Collections.Concurrent;
using Miunie.Core.Infrastructure;

namespace Miunie.Core.Providers
{
    public class UserReputationProvider : IUserReputationProvider
    {
        public int TimeoutInSeconds { get; } = 1800;

        private readonly IMiunieUserProvider _userProvider;
        private readonly IDateTime _dateTime;

        public UserReputationProvider(IMiunieUserProvider userProvider, IDateTime dateTime)
        {
            _userProvider = userProvider;
            _dateTime = dateTime;
        }

        public void AddReputation(MiunieUser invoker, MiunieUser target)
        {
            target.Reputation.Value++;
            target.Reputation.PlusRepLog.TryAdd(invoker.UserId, _dateTime.UtcNow);
            _userProvider.StoreUser(target);
        }

        public void RemoveReputation(MiunieUser invoker, MiunieUser target)
        {
            target.Reputation.Value--;
            target.Reputation.MinusRepLog.TryAdd(invoker.UserId, _dateTime.UtcNow);
            _userProvider.StoreUser(target);
        }

        public bool CanAddReputation(MiunieUser invoker, MiunieUser target)
            => HasTimeout(target.Reputation.PlusRepLog, invoker);

        public bool CanRemoveReputation(MiunieUser invoker, MiunieUser target)
            => HasTimeout(target.Reputation.MinusRepLog, invoker);

        private bool HasTimeout(ConcurrentDictionary<ulong, DateTime> log, MiunieUser invoker)
        {
            log.TryGetValue(invoker.UserId, out var lastRepDateTime);

            if ((_dateTime.UtcNow - lastRepDateTime).TotalSeconds <= TimeoutInSeconds) { return true; }

            log.TryRemove(invoker.UserId, out _);
            _userProvider.StoreUser(invoker);
            return false;
        }
    }
}
