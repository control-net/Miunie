using System.Collections.Generic;

namespace Miunie.Core
{
    public class MiunieUser
    {
        public string Name { get; set; }
        public ulong GuildId { get; set; }
        public ulong Id { get; set; }
        public Reputation Reputation { get; set; }
        public List<ulong> NavCursor { get; set; }

        public MiunieUser()
        {
            NavCursor = new List<ulong>();
        }
    }
}

