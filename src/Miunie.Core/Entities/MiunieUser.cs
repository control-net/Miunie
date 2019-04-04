using System;
using System.Collections.Generic;
using System.Linq;

namespace Miunie.Core
{
    public class MiunieUser
    {
        public string Name { get; set; }
        public ulong GuildId { get; set; }
        public ulong Id { get; set; }
        public Reputation Reputation { get; set; }
        public List<ulong> NavCursor { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsBot { get; set; }
        public IEnumerable<MiunieRole> Roles { get; set; }

        public MiunieUser()
        {
            NavCursor = new List<ulong>();
        }

        public override string ToString()
            => $"Name: {Name}\n" + 
            $"{(IsBot ? "Is a bot." : "A real human being.")}\n" +
            $"Reputation: {Reputation.Value}\n" +
            $"Roles:\n**{string.Join("\n", Roles.Select(r => r.Name))}**\n" +
            $"Joined {JoinedAt:d} at {JoinedAt:t} UTC.";
    }
}

