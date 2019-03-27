using System;
using System.Collections.Concurrent;
using Miunie.Core.Infrastructure;

namespace Miunie.Core.Providers
{
    public class UserReputationProvider : IUserReputationProvider
    {
        private const int TimeoutInSeconds = 1800;

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
            target.Reputation.PlusRepLog.TryAdd(invoker.Id, _dateTime.Now);
            _userProvider.StoreUser(target);
        }

        public void RemoveReputation(MiunieUser invoker, MiunieUser target)
        {
            target.Reputation.Value--;
            target.Reputation.MinusRepLog.TryAdd(invoker.Id, _dateTime.Now);
            _userProvider.StoreUser(target);
        }

        public bool AddReputationHasTimeout(MiunieUser invoker, MiunieUser target)
            => HasTimeout(target.Reputation.PlusRepLog, invoker);

        public bool RemoveReputationHasTimeout(MiunieUser invoker, MiunieUser target)
            => HasTimeout(target.Reputation.MinusRepLog, invoker);

        private bool HasTimeout(ConcurrentDictionary<ulong, DateTime> log, MiunieUser invoker)
        {
            log.TryGetValue(invoker.Id, out var lastRepDateTime);

            if ((_dateTime.Now - lastRepDateTime).TotalSeconds <= TimeoutInSeconds) { return true; }

            log.TryRemove(invoker.Id, out _);
            _userProvider.StoreUser(invoker);
            return false;
        } 
    }
}
