using System;
using Models.Base;

namespace Models.Entities
{
    public class BasketProduct
    {
        public Guid ProductId { get; set; }
        public Money Price { get; set; }
        public int Quantity { get; set; }
    }
}