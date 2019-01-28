using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using Miunie.Core;

namespace Miunie.Discord.Convertors
{
    public class EntityConvertor
    {
        public MiunieChannelConvertor ChannelConvertor { get; }
        public MiunieUserConvertor UserConvertor { get; }

        public EntityConvertor(MiunieUserService miunieUserService)
        {
            ChannelConvertor = new MiunieChannelConvertor();
            UserConvertor = new MiunieUserConvertor(miunieUserService);
        }

        public MiunieUser ConvertUser(DiscordMember m)
            => UserConvertor.DiscordMemberToMiunieUser(m);

        public MiunieChannel ConvertChannel(DiscordChannel c)
            => ChannelConvertor.DiscordChannelToMiunieChannel(c);
    }
}
