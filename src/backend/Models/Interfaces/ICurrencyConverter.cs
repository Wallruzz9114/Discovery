using Models.Base;

namespace Models.Interfaces
{
    public interface ICurrencyConverter
    {
        Currency GetCurrency();
        Money Convert(string fromCurrency, Money value);
    }
}