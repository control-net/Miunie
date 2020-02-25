using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Miunie.Core
{
    [Service]
    public class CurrencyService
    {
        private const string DailyConversionApiUrl = @"https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.xml";
        private const string DailyConversionApiUrlDateParam = @"?date=";
        private const string RowElement = "radek";
        private const string AmountAttribute = "mnozstvi";
        private const string CodeAttribute = "kod";
        private const string CzechCrownsAttribute = "kurz";

        private readonly IDiscordMessages _discordMessages;

        public CurrencyService(IDiscordMessages discordMessages)
        {
            _discordMessages = discordMessages;
        }

        private IEnumerable<CurrencyData> FetchUpdatedData(DateTime dateTime)
        {
            string xml;
            using (var wc = new System.Net.WebClient())
                xml = wc.DownloadString($"{DailyConversionApiUrl}{DailyConversionApiUrlDateParam}{dateTime:dd.MM.yyyy}");
            var xDoc = XDocument.Parse(xml);

            var data =
                from anyElement
                in xDoc.Descendants(RowElement)
                select anyElement;

            return data.Select(r => new CurrencyData
            {
                Amount = int.Parse(r.Attribute(AmountAttribute)?.Value ?? "0"),
                Code = r.Attribute(CodeAttribute)?.Value,
                CzechCrowns = decimal.Parse(r.Attribute(CzechCrownsAttribute)?.Value.Replace(",", ".") ?? "0.0", NumberStyles.AllowDecimalPoint)
            });
        }

        private IEnumerable<CurrencyData> FetchUpdatedData() => FetchUpdatedData(DateTime.Now);

        public async Task ShowCzkStatus(MiunieChannel channel)
        {
            var data = FetchUpdatedData();
            await _discordMessages.SendMessageAsync(channel, data);
        }

        public async Task ShowCzkStatus(MiunieChannel channel, DateTime dateTime)
        {
            var data = FetchUpdatedData();
            await _discordMessages.SendMessageAsync(channel, data);
        }

        public async Task ShowConversionCzkToForeign(MiunieChannel channel, string code, decimal amount)
        {
            var data = FetchUpdatedData();
            var currency = data.FirstOrDefault(c => c.Code == code.ToUpper());

            if(currency is null) { return; }

            var result = new CurrencyConversionResult
            {
                FromCode = "CZK",
                ToCode = code.ToUpper(),
                FromValue = amount,
                ToValue = amount / (currency.CzechCrowns / currency.Amount)
            };

            await _discordMessages.SendMessageAsync(channel, result);
        }

        public async Task ShowConversionForeignToCzk(MiunieChannel channel, string code, decimal amount)
        {
            var data = FetchUpdatedData();
            var currency = data.FirstOrDefault(c => c.Code == code.ToUpper());

            if (currency is null) { return; }

            var result = new CurrencyConversionResult
            {
                FromCode = code.ToUpper(),
                ToCode = "CZK",
                FromValue = amount,
                ToValue = (currency.CzechCrowns / currency.Amount) * amount
            };

            await _discordMessages.SendMessageAsync(channel, result);
        }
    }
}
