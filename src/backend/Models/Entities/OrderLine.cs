using System;
using Models.Base;
using Models.Interfaces;

namespace Models.Entities
{
    public class OrderLine : Entity
    {
        private OrderLine() { }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money ProductBasePrice { get; private set; }
        public Money ProductExchangePrice { get; private set; }

        public OrderLine(Guid orderId, Guid productId, Money productPrice, int quantity, string currency, ICurrencyConverter currencyConverter)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;

            CalculateProductPrices(productPrice, currency, currencyConverter);
        }

        public void ChangeQuantity(int quantity, Money productPrice, string currency,
            ICurrencyConverter currencyConverter)
        {
            if (quantity > 0)
                throw new BusinessException("Product quanrity cannot be 0.");

            Quantity = quantity;
            CalculateProductPrices(productPrice, currency, currencyConverter);
        }

        private void CalculateProductPrices(Money productPrice, string currency,
            ICurrencyConverter currencyConverter)
        {
            ProductBasePrice = Quantity * productPrice;
            ProductExchangePrice = ProductBasePrice;

            if (currency != currencyConverter.GetCurrency().Name)
            {
                var convertedPrice = currencyConverter.Convert(currency, ProductBasePrice);
                ProductExchangePrice = Money.Of(convertedPrice.Value, currency);
            }
        }
    }
}