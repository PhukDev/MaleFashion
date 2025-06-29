using MaleFashion.Data;
using MaleFashion.Models;
using MaleFashion.Models.Interface;
using MaleFashion.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MaleFashion.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdersController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public OrdersController(ICartService cartService, ApplicationDbContext context, ILogger<OrdersController> logger, UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            _logger.LogInformation("Accessing Checkout GET");
            var cart = _cartService.GetCart(HttpContext);
            if (cart == null || !cart.OrderItems.Any())
            {
                _logger.LogWarning("Checkout attempted with empty cart");
                return RedirectToAction("Index", "ShoppingCart");
            }

            foreach (var item in cart.OrderItems)
            {
                if (item.Product == null)
                {
                    item.Product = _context.Products.Find(item.ProductId);
                    if (item.Product == null)
                    {
                        _logger.LogWarning("Product with ID {ProductId} not found for cart item", item.ProductId);
                    }
                }
            }

            var model = new OrderViewModel
            {
                OrderItems = cart.OrderItems.Select(item => new OrderItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "Unknown Product",
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList(),
                Total = cart.OrderItems.Sum(item => item.Price * item.Quantity)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderViewModel model, string paymentMethod, bool createAccount, string password)
        {
            _logger.LogInformation("Processing Checkout POST. Model: FirstName={FirstName}, LastName={LastName}, Email={Email}, Phone={Phone}, PaymentMethod={PaymentMethod}, CreateAccount={CreateAccount}, UserAuthenticated={IsAuthenticated}, UserName={UserName}",
                model.FirstName, model.LastName, model.Email, model.Phone, paymentMethod, createAccount, User.Identity.IsAuthenticated, User.Identity.Name);

            try
            {
                var cart = _cartService.GetCart(HttpContext);
                _logger.LogInformation("Cart retrieved. Item count: {ItemCount}", cart?.OrderItems?.Count ?? 0);

                // Kiểm tra password khi createAccount là true
                if (createAccount && string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("password", "Mật khẩu là bắt buộc khi tạo tài khoản.");
                }
                else if (createAccount && password.Length < 6)
                {
                    ModelState.AddModelError("password", "Mật khẩu phải có ít nhất 6 ký tự.");
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    _logger.LogWarning("Invalid model state during checkout. Errors: {Errors}", string.Join("; ", errors));
                    if (cart != null && cart.OrderItems.Any())
                    {
                        foreach (var item in cart.OrderItems)
                        {
                            if (item.Product == null)
                            {
                                item.Product = await _context.Products.FindAsync(item.ProductId);
                                if (item.Product == null)
                                {
                                    _logger.LogWarning("Product with ID {ProductId} not found for cart item", item.ProductId);
                                }
                            }
                        }
                        model.OrderItems = cart.OrderItems.Select(item => new OrderItemViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = item.Product?.Name ?? "Unknown Product",
                            Price = item.Price,
                            Quantity = item.Quantity
                        }).ToList();
                        model.Total = cart.OrderItems.Sum(item => item.Price * item.Quantity);
                    }
                    else
                    {
                        model.OrderItems = new List<OrderItemViewModel>();
                        model.Total = 0;
                    }
                    return View(model);
                }

                // Kiểm tra định dạng Phone
                if (!new PhoneAttribute().IsValid(model.Phone))
                {
                    ModelState.AddModelError("Phone", "Số điện thoại không hợp lệ");
                    _logger.LogWarning("Invalid phone number: {Phone}", model.Phone);
                    model.OrderItems = cart.OrderItems.Select(item => new OrderItemViewModel
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product?.Name ?? "Unknown Product",
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToList();
                    model.Total = cart.OrderItems.Sum(item => item.Price * item.Quantity);
                    return View(model);
                }

                if (cart == null || !cart.OrderItems.Any())
                {
                    _logger.LogWarning("Checkout attempted with empty cart");
                    return RedirectToAction("Index", "ShoppingCart");
                }

                // Kiểm tra ProductId hợp lệ
                foreach (var item in cart.OrderItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        _logger.LogError("Product with ID {ProductId} does not exist in database", item.ProductId);
                        ModelState.AddModelError("", $"Sản phẩm với ID {item.ProductId} không tồn tại.");
                        model.OrderItems = cart.OrderItems.Select(item => new OrderItemViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = item.Product?.Name ?? "Unknown Product",
                            Price = item.Price,
                            Quantity = item.Quantity
                        }).ToList();
                        model.Total = cart.OrderItems.Sum(item => item.Price * item.Quantity);
                        return View(model);
                    }
                }

                // Tạo tài khoản nếu createAccount là true
                string userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;
                if (createAccount)
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        _logger.LogWarning("Failed to create user account. Errors: {Errors}", string.Join("; ", result.Errors.Select(e => e.Description)));
                        model.OrderItems = cart.OrderItems.Select(item => new OrderItemViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = item.Product?.Name ?? "Unknown Product",
                            Price = item.Price,
                            Quantity = item.Quantity
                        }).ToList();
                        model.Total = cart.OrderItems.Sum(item => item.Price * item.Quantity);
                        return View(model);
                    }
                    userId = user.Id;
                    _logger.LogInformation("User account created successfully. UserId: {UserId}", userId);
                }

                var order = new Order
                {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Country = model.Country,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PostalCode = model.PostalCode,
                    Phone = model.Phone,
                    Email = model.Email,
                    OrderNotes = model.OrderNotes,
                    OrderDate = DateTime.Now,
                    Total = cart.OrderItems.Sum(item => item.Price * item.Quantity),
                    PaymentMethod = paymentMethod,
                    OrderItems = cart.OrderItems.Select(item => new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                };

                _logger.LogInformation("Attempting to save order to database");
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order {OrderId} created successfully", order.Id);

                try
                {
                    await _cartService.CheckoutAsync(order, model.Address, HttpContext);
                    _logger.LogInformation("CartService.CheckoutAsync completed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in _cartService.CheckoutAsync for Order ID: {OrderId}", order.Id);
                }

                _logger.LogInformation("Redirecting to CheckoutComplete for Order ID: {OrderId}", order.Id);
                return RedirectToAction("CheckoutComplete", new { id = order.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing checkout");
                ModelState.AddModelError("", $"Đã có lỗi xảy ra: {ex.Message}");
                var cart = _cartService.GetCart(HttpContext);
                if (cart != null && cart.OrderItems.Any())
                {
                    foreach (var item in cart.OrderItems)
                    {
                        if (item.Product == null)
                        {
                            item.Product = await _context.Products.FindAsync(item.ProductId);
                            if (item.Product == null)
                            {
                                _logger.LogWarning("Product with ID {ProductId} not found for cart item", item.ProductId);
                            }
                        }
                    }
                    model.OrderItems = cart.OrderItems.Select(item => new OrderItemViewModel
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product?.Name ?? "Unknown Product",
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToList();
                    model.Total = cart.OrderItems.Sum(item => item.Price * item.Quantity);
                }
                else
                {
                    model.OrderItems = new List<OrderItemViewModel>();
                    model.Total = 0;
                }
                return View(model);
            }
        }

        public async Task<IActionResult> CheckoutComplete(int id)
        {
            _logger.LogInformation("Processing CheckoutComplete for Order ID: {OrderId}", id);
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                _logger.LogWarning("Order {OrderId} not found for checkout complete", id);
                return NotFound();
            }

            var model = new OrderViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Country = order.Country,
                Address = order.Address,
                City = order.City,
                State = order.State,
                PostalCode = order.PostalCode,
                Phone = order.Phone,
                Email = order.Email,
                OrderNotes = order.OrderNotes,
                OrderDate = order.OrderDate,
                Total = order.Total,
                PaymentMethod = order.PaymentMethod,
                OrderItems = order.OrderItems.Select(item => new OrderItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "Unknown Product",
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            return View(model);
        }
    }
}