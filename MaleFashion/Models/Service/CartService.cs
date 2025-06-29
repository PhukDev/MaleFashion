using MaleFashion.Data;
using MaleFashion.Models.Interface;
using MaleFashion.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MaleFashion.Models.Service
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Order GetCart(HttpContext httpContext)
        {
            var session = httpContext.Session;
            var cartJson = session.GetString("Cart"); 
            if (string.IsNullOrEmpty(cartJson))
            {
                var cart = new Order { OrderItems = new List<OrderItem>() };
                session.SetString("Cart", JsonSerializer.Serialize(cart));
                return cart;
            }
            return JsonSerializer.Deserialize<Order>(cartJson); 
        }

        public async Task AddToCartAsync(int productId, HttpContext httpContext)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return;

            var cart = GetCart(httpContext);
            var cartItem = cart.OrderItems.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem == null)
            {
                cart.OrderItems.Add(new OrderItem { ProductId = productId, Quantity = 1, Price = product.Price });
            }
            else
            {
                cartItem.Quantity++;
            }
            httpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            httpContext.Session.SetInt32("CartCount", cart.OrderItems.Sum(x => x.Quantity));
        }

        public async Task CheckoutAsync(Order order, string address, HttpContext httpContext)
        {
            httpContext.Session.Remove("Cart");
            httpContext.Session.Remove("CartCount");
            await Task.CompletedTask;
        }
    }
}