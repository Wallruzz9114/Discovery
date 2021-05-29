namespace Models.Base
{
    public class ExchangeRate
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal ConversionRate { get; set; }
    }
}