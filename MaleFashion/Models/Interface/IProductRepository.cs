using MaleFashion.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaleFashion.Models.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}