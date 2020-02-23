using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class CurrencyCommand : ModuleBase<SocketCommandContext>
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly CurrencyService _currencyService;

        public CurrencyCommand(EntityConvertor convertor, CurrencyService currencyService)
        {
            _entityConvertor = convertor;
            _currencyService = currencyService;
        }

        [Command("CZK")]
        public async Task ShowCzkStatus()
        {
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _currencyService.ShowCzkStatus(channel);
        }

        [Command("CZK")]
        public async Task ShowCzkStatusForDate(string dateTime)
        {
            if (!DateTime.TryParse(dateTime, out var value)) { return; }
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _currencyService.ShowCzkStatus(channel, value);
        }

        [Command("Convert")]
        public async Task ConvertCurrency(decimal value, string fromCode, string verb, string toCode)
        {
            if (verb != "to") { return; }

            if (fromCode != "CZK" && toCode != "CZK") { return; } // Not supported

            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);

            if (fromCode.ToUpper() == "CZK")
            {
                await _currencyService.ShowConversionCzkToForeign(channel, toCode, value);
                return;
            }

            await _currencyService.ShowConversionForeignToCzk(channel, fromCode, value);
        }
    }
}
