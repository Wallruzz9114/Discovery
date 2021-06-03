namespace Models.Base
{
    public class ExchangeRate
    {
        public ExchangeRate(string sourceCurrency, string targetCurrency, decimal conversionRate)
        {
            FromCurrency = sourceCurrency;
            ToCurrency = targetCurrency;
            ConversionRate = conversionRate;
        }

        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal ConversionRate { get; set; }
    }
}