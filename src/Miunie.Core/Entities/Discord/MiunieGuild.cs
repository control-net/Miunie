using System;
using System.Collections.Generic;

namespace Miunie.Core.Entities.Discord
{
    public class MiunieGuild
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MiunieRole> Roles { get; set; }
        public IEnumerable<string> ChannelNames { get; set; }
        public DateTime CreationDate { get; set; }
        public int TextChannelCount { get; set; }
        public int VoiceChannelCount { get; set; }
        public int MemberCount { get; set; }
        public string IconUrl { get; set; }

        public string GetStats()
            => $"{MemberCount} members chatting in \n" +
            $":pencil: {TextChannelCount} Text Channel/s & \n" +
            $":loudspeaker: {VoiceChannelCount} Voice Channel/s.";
    }
}
