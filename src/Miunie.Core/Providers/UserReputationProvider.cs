using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public List<ReputationEntry> GetReputation(MiunieUser invoker)
        {
            var rep = new List<ReputationEntry>();

            foreach (MiunieUser user in _userProvider.GetAllUsers().Where(x => x.Id != invoker.Id))
            {
                if (user.Reputation.PlusRepLog.ContainsKey(invoker.UserId))
                    rep.Add(new ReputationEntry(user.UserId, user.Name, user.Reputation.PlusRepLog[invoker.UserId], ReputationType.Plus, true));
                else if (user.Reputation.MinusRepLog.ContainsKey(invoker.UserId))
                    rep.Add(new ReputationEntry(user.UserId, user.Name, user.Reputation.PlusRepLog[invoker.UserId], ReputationType.Minus, true));
            }

            foreach(KeyValuePair<ulong, DateTime> entry in invoker.Reputation.PlusRepLog)
            {
                var user = _userProvider.GetById(entry.Key, invoker.GuildId);
                rep.Add(new ReputationEntry(user.UserId, user.Name, entry.Value, ReputationType.Plus));
            }

            foreach(KeyValuePair<ulong, DateTime> entry in invoker.Reputation.MinusRepLog)
            {

                var user = _userProvider.GetById(entry.Key, invoker.GuildId);
                rep.Add(new ReputationEntry(user.UserId, user.Name, entry.Value, ReputationType.Minus));
            }

            return rep;
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
