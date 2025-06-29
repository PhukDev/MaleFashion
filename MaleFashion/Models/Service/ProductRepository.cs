using MaleFashion.Data;
using MaleFashion.Models.Interface;
using MaleFashion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaleFashion.Models.Service
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            _logger.LogInformation("Retrieved {Count} products from Products table", products.Count);
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            _logger.LogInformation("Retrieved product with ID {Id}: {Name}", id, product?.Name ?? "Not found");
            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added product: {Name}", product.Name);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated product: {Name}", product.Name);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Deleted product with ID: {Id}", id);
            }
        }
    }
}