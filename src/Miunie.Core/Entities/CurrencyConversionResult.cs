namespace Miunie.Core
{
    public class CurrencyConversionResult
    {
        public string FromCode { get; set; }
        public string ToCode { get; set; }
        public decimal FromValue { get; set; }
        public decimal ToValue { get; set; }
    }
}