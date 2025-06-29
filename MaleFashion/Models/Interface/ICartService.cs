using MaleFashion.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MaleFashion.Models.Interface
{
    public interface ICartService
    {
        Order GetCart(HttpContext httpContext);
        Task AddToCartAsync(int productId, HttpContext httpContext);
        Task CheckoutAsync(Order order, string address, HttpContext httpContext);
    }
}