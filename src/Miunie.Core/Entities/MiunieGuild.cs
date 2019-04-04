using System;
using System.Collections.Generic;
using System.Linq;

namespace Miunie.Core
{
    public class MiunieGuild
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MiunieRole> Roles { get; set; }
        public DateTime CreationDate { get; set; }
        public int TextChannelCount { get; set; }
        public int VoiceChannelCount { get; set; }
        public int MemberCount { get; set; }

        public override string ToString()
            => $"{Name}\n" +
            $"With {MemberCount} members chatting in \n" +
            $":pencil: {TextChannelCount} Text Channel/s & \n" +
            $":loudspeaker: {VoiceChannelCount} Voice Channel/s.\n" +
            $"Roles: **{string.Join(", ", Roles.Select(r => r.Name.Replace("@", "")))}**\n" +
            $"Created: {CreationDate:d} at {CreationDate:t} UTC.";
    }
}
