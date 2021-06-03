using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repositories;
using Models.Base;
using Models.Entities;
using Models.Interfaces;

namespace Data.Seed
{
    public static class DataSeeder
    {
        public async static Task SeedProductData(IAppUnitOfWork appUnitOfWork, ICurrencyConverter currencyConverter)
        {
            var productsFromDb = await appUnitOfWork.ProductRepository.ListAllProducts();

            if (productsFromDb is null)
            {
                var products = new List<Product>();
                var random = new Random();

                for (int i = 0; i < 50; i++)
                {
                    var price = new decimal(random.NextDouble());
                    products.Add(
                        new Product($"Product {i}",
                        Money.Of(price, currencyConverter.GetBaseCurrency().Name))
                    );
                }

                await appUnitOfWork.ProductRepository.AddProducts(products);
                await appUnitOfWork.CommitAsync();
            }
        }
    }
}