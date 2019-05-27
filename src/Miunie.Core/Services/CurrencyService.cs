using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Miunie.Core
{
    public class CurrencyService
    {
        private const string DailyConversionApiUrl = @"https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.xml";
        private const string DailyConversionApiUrlDateParam = @"?date="; // DD.MM.RRRR
        private const string RowElement = "radek";
        private const string AmountAttribute = "mnozstvi";
        private const string CodeAttribute = "kod";
        private const string CzechCrownsAttribute = "kurz";

        private readonly IDiscordMessages _discordMessages;

        private IEnumerable<CurrencyData> _todaysCurrencyData;

        public CurrencyService(IDiscordMessages discordMessages)
        {
            _discordMessages = discordMessages;
        }

        private void FetchUpdatedData()
        {
            string xml;
            using (var wc = new System.Net.WebClient())
                xml = wc.DownloadString(DailyConversionApiUrl);
            var xDoc = XDocument.Parse(xml);

            var data =
                from anyElement
                in xDoc.Descendants(RowElement)
                select anyElement;

            _todaysCurrencyData = data.Select(r => new CurrencyData
            {
                Amount = int.Parse(r.Attribute(AmountAttribute)?.Value ?? "0"),
                Code = r.Attribute(CodeAttribute)?.Value,
                CzechCrowns = decimal.Parse(r.Attribute(CzechCrownsAttribute)?.Value.Replace(",", ".") ?? "0.0", NumberStyles.AllowDecimalPoint)
            });
        }

        public async Task ShowCzkStatus(MiunieChannel channel)
        {
            FetchUpdatedData();
            await _discordMessages.SendMessageAsync(channel, _todaysCurrencyData);
        }
    }
}