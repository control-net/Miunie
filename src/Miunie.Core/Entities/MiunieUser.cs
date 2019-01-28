using System.Collections.Generic;

namespace Miunie.Core
{
    public class MiunieUser
    {
        public ulong GuildId { get; set; }
        public ulong Id { get; set; }
        public long Reputation { get; set; }
        public List<ulong> NavCursor { get; set; }

        public MiunieUser()
        {
            NavCursor = new List<ulong>();
        }
    }
}

