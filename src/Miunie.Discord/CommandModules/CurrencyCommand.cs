using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class CurrencyCommand : BaseCommandModule
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly CurrencyService _currencyService;

        public CurrencyCommand(EntityConvertor convertor, CurrencyService currencyService)
        {
            _entityConvertor = convertor;
            _currencyService = currencyService;
        }

        [Command("CZK")]
        public async Task AddReputationAsync(CommandContext ctx)
        {
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _currencyService.ShowCzkStatus(channel);
        }

        [Command("CZK")]
        public async Task AddReputationAsync(CommandContext ctx, string dateTime)
        {
            if (!DateTime.TryParse(dateTime, out var value)) { return; }
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _currencyService.ShowCzkStatus(channel, value);
        }
    }
}