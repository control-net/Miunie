using DSharpPlus.Entities;
using Miunie.Core;
using Miunie.Core.Providers;

namespace Miunie.Discord.Convertors
{
    public class EntityConvertor
    {
        public MiunieChannelConvertor ChannelConvertor { get; }
        public MiunieUserConvertor UserConvertor { get; }

        public EntityConvertor(IMiunieUserProvider miunieUserProvider)
        {
            ChannelConvertor = new MiunieChannelConvertor();
            UserConvertor = new MiunieUserConvertor(miunieUserProvider);
        }

        public MiunieUser ConvertUser(DiscordMember m)
            => UserConvertor.DiscordMemberToMiunieUser(m);

        public MiunieChannel ConvertChannel(DiscordChannel c)
            => MiunieChannelConvertor.FromDiscordChannel(c);
    }
}
