using Models.Base;

namespace Models.Interfaces
{
    public interface ICurrencyConverter
    {
        Currency GetBaseCurrency();
        Money Convert(string fromCurrency, Money value);
    }
}