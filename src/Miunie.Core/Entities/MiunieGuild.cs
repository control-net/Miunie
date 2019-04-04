using System;
using System.Collections.Generic;

namespace Miunie.Core.Entities
{
    public class MiunieGuild
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MiunieRole> Roles { get; set; }
        public IEnumerable<MiunieUser> Admins { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
