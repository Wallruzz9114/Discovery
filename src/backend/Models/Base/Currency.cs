using System;
using System.Collections.Generic;

namespace Models.Base
{
    public class Currency : ValueObject
    {
        private Currency() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Symbol;
        }

        public Currency(string name, string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentNullException(nameof(symbol));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Amount cannot be null or whitespace.", nameof(name));

            Symbol = symbol;
            Name = name;
        }

        public static Currency FromCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            Currency currency = code switch
            {
                "USD" => new Currency(USD.Name, USD.Symbol),
                "CAD" => new Currency(CAD.Name, CAD.Symbol),
                "EUR" => new Currency(EUR.Name, EUR.Symbol),
                // "RMB" => new Currency(RMB.Name, RMB.Symbol),
                // "GBP" => new Currency(GBP.Name, GBP.Symbol),
                // "CFA" => new Currency(CFA.Name, CFA.Symbol),
                // "RUB" => new Currency(RUB.Name, RUB.Symbol),
                _ => throw new ArgumentException($"Invalid code: { code }", nameof(code))
            };

            return currency;
        }

        public string Name { get; set; }
        public string Symbol { get; set; }

        public static Currency USD => new("USD", "$");
        public static Currency CAD => new("CAD", "$");
        public static Currency EUR => new("EUR", "€");
        // public static Currency RMB => new("RMB", "¥");
        // public static Currency GBP => new("GBP", "£");
        // public static Currency CFA => new("CFA", "CFA");
        // public static Currency RUB => new("RUB", "₽");

        public static List<string> SupportedCurrencies()
        {
            return new List<string>()
            {
                CAD.Name,
                USD.Name,
                EUR.Name,
                // RMB.Name,
                // GBP.Name,
                // CFA.Name,
                // RUB.Name
            };
        }
    }
}