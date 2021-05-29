using System;
using System.Collections.Generic;
using Models.Entities;

namespace Models.Base
{
    public class Basket
    {
        public Basket(string currency)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new BusinessException("The used currency must be informed.");
            }

            Currency = currency;
        }

        public void AddProduct(Guid productId, Money price, int quantity)
        {
            if (price == null) throw new ArgumentNullException(nameof(price));

            Products.Add(new BasketProduct
            {
                Id = productId,
                Price = price,
                Quantity = quantity
            });
        }

        public string Currency { get; private set; }
        public List<BasketProduct> Products { get; private set; } = new List<BasketProduct>();
    }
}