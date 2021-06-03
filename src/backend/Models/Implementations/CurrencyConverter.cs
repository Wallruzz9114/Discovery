using System.Collections.Generic;
using System.Linq;
using Models.Base;
using Models.Interfaces;

namespace Models.Implementations
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public Currency BaseCurrency = Currency.CAD;

        public Currency GetBaseCurrency()
        {
            return BaseCurrency;
        }

        public Money Convert(string fromCurrency, Money value)
        {
            var conversionRate = GetExchangeRates()
                .Single(x => x.FromCurrency == fromCurrency && x.ToCurrency == BaseCurrency.Name);
            var convertedValue = conversionRate.ConversionRate * value;

            return convertedValue;
        }

        private static List<ExchangeRate> GetExchangeRates()
        {
            var conversionRates = new List<ExchangeRate>
            {
                new ExchangeRate(Currency.USD.Name, Currency.CAD.Name, (decimal)0.83),
                new ExchangeRate(Currency.CAD.Name, Currency.USD.Name, (decimal)1.20),
                new ExchangeRate(Currency.USD.Name, Currency.EUR.Name, (decimal)0.82),
                new ExchangeRate(Currency.EUR.Name, Currency.USD.Name, (decimal)1.22)
            };

            return conversionRates;
        }
    }
}