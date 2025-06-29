using MaleFashion.Data;
using MaleFashion.Models;
using MaleFashion.Models.Interface;
using MaleFashion.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaleFashionCoreMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(ICartService cartService, ApplicationDbContext context, ILogger<ShoppingCartController> logger)
        {
            _cartService = cartService;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cart = _cartService.GetCart(HttpContext);
                _logger.LogInformation("ShoppingCartController retrieved {Count} cart items", cart?.OrderItems?.Count ?? 0);

                var cartViewModel = new List<CartItemViewModel>();
                if (cart?.OrderItems != null && cart.OrderItems.Any())
                {
                    foreach (var item in cart.OrderItems)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                        if (product != null)
                        {
                            cartViewModel.Add(new CartItemViewModel
                            {
                                ProductId = item.ProductId,
                                ProductName = product.Name,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                ImageUrl = product.ImageUrl
                            });
                        }
                    }
                }

                var categories = await _context.Categories.ToListAsync();
                if (categories == null || !categories.Any())
                {
                    _logger.LogWarning("No categories found in the database");
                    ViewBag.Categories = new List<Category>();
                }
                else
                {
                    _logger.LogInformation("ShoppingCartController retrieved {Count} categories", categories.Count);
                    ViewBag.Categories = categories;
                }

                ViewBag.Subtotal = cartViewModel.Sum(item => item.Price * item.Quantity);
                ViewBag.Total = ViewBag.Subtotal;
                ViewBag.CartCount = cart?.OrderItems?.Sum(x => x.Quantity) ?? 0;

                return View(cartViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cart items or categories");
                ViewBag.Categories = new List<Category>();
                ViewBag.Subtotal = 0m;
                ViewBag.Total = 0m;
                ViewBag.CartCount = 0;
                return View(new List<CartItemViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int productId)
        {
            try
            {
                _logger.LogInformation("AddToShoppingCart called with productId: {ProductId}", productId);
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found.", productId);
                    return NotFound();
                }

                await _cartService.AddToCartAsync(productId, HttpContext);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product {ProductId} to cart", productId);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(Dictionary<int, int> quantities)
        {
            try
            {
                var cart = _cartService.GetCart(HttpContext);
                foreach (var item in quantities)
                {
                    var cartItem = cart.OrderItems.FirstOrDefault(x => x.ProductId == item.Key);
                    if (cartItem != null)
                    {
                        if (item.Value <= 0)
                        {
                            cart.OrderItems.Remove(cartItem);
                        }
                        else
                        {
                            cartItem.Quantity = item.Value;
                        }
                    }
                }
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                HttpContext.Session.SetInt32("CartCount", cart.OrderItems.Sum(x => x.Quantity));
                _logger.LogInformation("Cart updated with {Count} items", quantities.Count);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult ApplyCoupon(string couponCode)
        {
            _logger.LogInformation("Coupon code {Code} applied", couponCode);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                var cart = _cartService.GetCart(HttpContext);
                var cartItem = cart.OrderItems.FirstOrDefault(x => x.ProductId == id);
                if (cartItem != null)
                {
                    cart.OrderItems.Remove(cartItem);
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
                    HttpContext.Session.SetInt32("CartCount", cart.OrderItems.Sum(x => x.Quantity));
                    _logger.LogInformation("Removed product {ProductId} from cart", id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product {ProductId} from cart", id);
                return RedirectToAction("Index");
            }
        }
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}