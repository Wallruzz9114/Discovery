using System.Collections.Generic;

namespace Models.Base
{
    public class Money : ValueObject
    {
        private Money() { }

        public decimal Value { get; }
        public Currency Currency { get; }

        private Money(decimal value, string currency)
        {
            Value = value;
            Currency = Currency.FromCode(currency);
        }

        public static Money Of(decimal value, string currency)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new BusinessException("Money must have currency.");
            }

            return new Money(value, currency);
        }

        public static Money operator *(decimal number, Money rightValue)
        {
            return new Money(number * rightValue.Value, rightValue.Currency.Name);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Currency;
        }
    }
}