using Miunie.Core;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using System;

namespace Miunie.Discord.Convertors
{
    public class MiunieChannelConvertor : IArgumentConverter<MiunieChannel>
    {
        private readonly DiscordChannelConverter _dcConverter;

        public MiunieChannelConvertor()
        {
            _dcConverter = new DiscordChannelConverter();
        }

        public async Task<Optional<MiunieChannel>> ConvertAsync(string userInput, CommandContext context)
        {
            var result = await _dcConverter.ConvertAsync(userInput, context);
            return FromDiscordChannel(result.Value);
        }

        public MiunieChannel FromDiscordChannel(DiscordChannel channel)
        {
            MiunieChannel miunieChannel;
            if (channel is default(DiscordChannel))
            {
                miunieChannel = default(MiunieChannel);
            }
            else
            {
                miunieChannel = new MiunieChannel()
                {
                    ChannelId = channel.Id,
                    GuildId = channel.GuildId
                };
            }
            return miunieChannel;
        }
    }
}
