using Microsoft.AspNetCore.Mvc;
using MaleFashion.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MaleFashion.Models;

namespace MaleFashion.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly ApplicationDbContext _context;

        public BlogController(ILogger<BlogController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _logger.LogInformation("BlogController initialized");
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Rendering Blog.cshtml");
            var posts = await _context.BlogPosts.ToListAsync();
            return View("~/Views/Blog/Index.cshtml", posts); 
        }

        public async Task<IActionResult> Details(string id)
        {
            _logger.LogInformation($"Rendering BlogDetails.cshtml with id={id}");
            var post = await _context.BlogPosts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id.ToString() == id);
            if (post == null)
            {
                return NotFound();
            }
            return View("~/Views/Blog/BlogDetails.cshtml", post); 
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(string postId, string userId, string comment)
        {
            _logger.LogInformation($"Comment submitted: PostId={postId}, UserId={userId}, Comment={comment}");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(comment))
            {
                ViewBag.ErrorMessage = "Vui lòng điền đầy đủ các trường bắt buộc.";
                var post = await _context.BlogPosts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.Id.ToString() == postId);
                return View("~/Views/Blog/BlogDetails.cshtml", post);
            }

            var newComment = new Comment
            {
                BlogPostId = int.Parse(postId),
                UserId = userId,
                Content = comment,
                DatePosted = DateTime.Now
            };

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Cảm ơn bạn đã bình luận!";
            var updatedPost = await _context.BlogPosts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id.ToString() == postId);
            return View("~/Views/Blog/BlogDetails.cshtml", updatedPost);
        }
    }
}