using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddProduct(Product product, CancellationToken cancellationToken = default)
        {
            await _dbContext.Products.AddAsync(product, cancellationToken);
        }

        public async Task AddProducts(List<Product> products, CancellationToken cancellationToken = default)
        {
            await _dbContext.Products.AddRangeAsync(products, cancellationToken);
        }

        public async Task<Product> GetProductById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsByIds(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> ListAllProducts(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products.ToListAsync(cancellationToken);
        }
    }
}