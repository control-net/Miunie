using System;
using System.Collections.Generic;
using System.Linq;

namespace Miunie.Core
{
    public class MiunieUser
    {
        public Guid Id
        {
            get
            {
                return GenerateSeededGuid();
            }
        }

        private Guid GenerateSeededGuid()
        {
            var left = BitConverter.GetBytes(GuildId);
            var right = BitConverter.GetBytes(UserId);
            var bytes = left.Concat(right).ToArray();
            return new Guid(bytes);
        }

        public string Name { get; set; }
        public ulong GuildId { get; set; }
        public ulong UserId { get; set; }
        public Reputation Reputation { get; set; }
        public List<ulong> NavCursor { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBot { get; set; }
        public IEnumerable<MiunieRole> Roles { get; set; }
        public string AvatarUrl { get; set; }

        public MiunieUser()
        {
            NavCursor = new List<ulong>();
        }


    }
}

