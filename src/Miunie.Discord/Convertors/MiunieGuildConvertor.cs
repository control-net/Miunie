using DSharpPlus;
using DSharpPlus.Entities;
using Miunie.Core;
using System.Linq;

namespace Miunie.Discord.Convertors
{
    public class MiunieGuildConvertor
    {
        public MiunieGuild DiscordGuildToMiunieGuild(DiscordGuild g)
            => new MiunieGuild
            {
                Id = g.Id,
                Name = g.Name,
                MemberCount = g.MemberCount,
                ChannelNames = g.Channels.Select(x => x.Name),
                TextChannelCount = g.Channels.Count(x => x.Type == ChannelType.Text),
                VoiceChannelCount = g.Channels.Count(x => x.Type == ChannelType.Voice),
                CreationDate = g.CreationTimestamp.UtcDateTime,
                Roles = g.Roles.Select(r => r.DiscordRoleToMiunieRole())
            };
    }
}
