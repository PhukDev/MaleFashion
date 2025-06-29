using MaleFashion.Data;
using MaleFashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MaleFashionCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _context.Products
                    .OrderBy(p => p.Name) 
                    .Take(8)
                    .ToListAsync();
                var latestPosts = await _context.BlogPosts
                    .OrderByDescending(p => p.DatePosted)
                    .Take(3)
                    .ToListAsync();
                _logger.LogInformation("HomeController retrieved {Count} products and {PostCount} blog posts", products.Count, latestPosts.Count);
                ViewBag.LatestPosts = latestPosts;
                ViewData["CartCount"] = 0; // Cập nhật nếu có logic giỏ hàng
                ViewData["CartTotal"] = 0m; // Cập nhật nếu có logic giỏ hàng
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products or blog posts");
                return View(new List<MaleFashion.Models.Product>());
            }
        }

        public IActionResult About()
        {
            _logger.LogInformation("Rendering About.cshtml");
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string message)
        {
            _logger.LogInformation($"Contact form submitted: Name={name}, Email={email}, Message={message}");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                ViewBag.ErrorMessage = "Vui lòng điền đầy đủ các trường bắt buộc.";
                return View();
            }

            ViewBag.SuccessMessage = "Cảm ơn bạn đã liên hệ!";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}