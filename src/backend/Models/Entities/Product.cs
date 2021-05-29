using System;
using Models.Base;

namespace Models.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        private Product() { }

        public string Name { get; private set; }
        public Money Price { get; private set; }
        public DateTime CreationDate { get; }

        public Product(string name, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            Price = price ?? throw new ArgumentNullException(nameof(price));
            CreationDate = DateTime.Now;
        }
    }
}