using DSharpPlus.Entities;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    public class EntityConvertor
    {
        public MiunieChannelConvertor ChannelConvertor { get; }
        public MiunieUserConvertor UserConvertor { get; }

        public EntityConvertor(IMiunieUserService miunieUserService)
        {
            ChannelConvertor = new MiunieChannelConvertor();
            UserConvertor = new MiunieUserConvertor(miunieUserService);
        }

        public MiunieUser ConvertUser(DiscordMember m)
            => UserConvertor.DiscordMemberToMiunieUser(m);

        public MiunieChannel ConvertChannel(DiscordChannel c)
            => MiunieChannelConvertor.FromDiscordChannel(c);
    }
}
