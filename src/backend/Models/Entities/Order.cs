using System;
using System.Collections.Generic;
using System.Linq;
using Models.Base;
using Models.Enums;
using Models.Interfaces;

namespace Models.Entities
{
    public class Order : Entity
    {
        private Order() { }

        public Order(Basket basket, ICurrencyConverter converter)
        {
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Placed;
            BuildOrderLines(basket, converter);
            CalculateTotalPrice(basket.Currency);
        }

        public DateTime OrderDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }
        public bool IsCancelled { get; private set; }
        public Money TotalPrice { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderLine> OrderLines { get; private set; } = new List<OrderLine>();

        private void BuildOrderLines(Basket basket, ICurrencyConverter converter)
        {
            foreach (BasketProduct product in basket.Products)
            {
                OrderLine orderLine = OrderLines.FirstOrDefault(ol => ol.ProductId == product.ProductId);

                if (orderLine == null)
                {
                    OrderLine newOrderLine = new(Id, product.ProductId, product.Price, product.Quantity, basket.Currency, converter);
                    OrderLines.Add(newOrderLine);
                }
                else
                {
                    var basketProduct = basket.Products.Single(p => p.ProductId == product.ProductId);
                    orderLine.ChangeQuantity(basketProduct.Quantity, product.Price, basket.Currency, converter);
                }
            }
        }

        public void Change(Basket basket, ICurrencyConverter currencyConverter)
        {
            ChangeDate = DateTime.UtcNow;
            RemoveOrderLine(basket);
            BuildOrderLines(basket, currencyConverter);
            CalculateTotalPrice(basket.Currency);
        }

        private void CalculateTotalPrice(string currency)
        {
            var total = OrderLines.Sum(x => x.ProductExchangePrice.Value);
            TotalPrice = Money.Of(total, currency);
        }

        private void RemoveOrderLine(Basket basket)
        {
            var orderLines = OrderLines.ToList();

            foreach (var orderLine in orderLines)
            {
                var product = basket.Products.SingleOrDefault(x => x.ProductId == orderLine.ProductId);
                if (product is null) OrderLines.Remove(orderLine);
            }
        }
    }
}