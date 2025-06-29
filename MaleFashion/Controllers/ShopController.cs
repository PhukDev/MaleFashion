using MaleFashion.Data;
using MaleFashion.Models.Interface;
using MaleFashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MaleFashionCoreMVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IProductRepository productRepository, ApplicationDbContext context, ILogger<ShopController> logger)
        {
            _productRepository = productRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            try
            {
                // Lấy danh sách sản phẩm
                var products = await _productRepository.GetAllProductsAsync();
                _logger.LogInformation("ShopController received {Count} products", products?.Count ?? 0);

                // Lấy danh sách danh mục từ cơ sở dữ liệu
                var categories = await _context.Categories.ToListAsync();
                if (categories == null || !categories.Any())
                {
                    _logger.LogWarning("No categories found in the database");
                    ViewBag.Categories = new List<Category>(); // Gán danh sách rỗng để tránh lỗi null
                }
                else
                {
                    _logger.LogInformation("ShopController retrieved {Count} categories", categories.Count);
                    ViewBag.Categories = categories;
                }

                // Lọc sản phẩm theo categoryId nếu có
                if (categoryId.HasValue)
                {
                    products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
                    _logger.LogInformation("Filtered to {Count} products for category {CategoryId}", products.Count, categoryId);
                }

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found, returning empty list");
                    return View(new List<Product>());
                }

                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products or categories");
                ViewBag.Categories = new List<Category>(); // Gán danh sách rỗng trong trường hợp lỗi
                return View(new List<Product>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // Lấy sản phẩm theo ID
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {Id} not found", id);
                    return NotFound();
                }

                // Lấy danh sách danh mục để hiển thị trong sidebar
                var categories = await _context.Categories.ToListAsync();
                if (categories == null || !categories.Any())
                {
                    _logger.LogWarning("No categories found in the database");
                    ViewBag.Categories = new List<Category>();
                }
                else
                {
                    _logger.LogInformation("ShopController retrieved {Count} categories for Details", categories.Count);
                    ViewBag.Categories = categories;
                }

                // Lấy danh mục của sản phẩm để hiển thị tên danh mục
                var category = await _context.Categories.FindAsync(product.CategoryId);
                ViewBag.CategoryName = category?.Name ?? "Unknown";

                // Lấy sản phẩm liên quan (cùng danh mục, trừ sản phẩm hiện tại, lấy tối đa 4 sản phẩm)
                var relatedProducts = await _productRepository.GetAllProductsAsync();
                relatedProducts = relatedProducts
                    .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                    .OrderBy(p => p.Name) // Thêm OrderBy để tránh cảnh báo
                    .Take(4)
                    .ToList();
                ViewBag.RelatedProducts = relatedProducts;

                _logger.LogInformation("Retrieved product ID {Id} with {RelatedCount} related products", id, relatedProducts.Count);

                return View("~/Views/Shop/ShopDetails.cshtml", product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product details for ID {Id}", id);
                ViewBag.Categories = new List<Category>();
                ViewBag.RelatedProducts = new List<Product>();
                return View("~/Views/Shop/ShopDetails.cshtml", null);
            }
        }
    }
}