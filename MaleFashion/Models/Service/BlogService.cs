using MaleFashion.Data;
using MaleFashion.Models.Interface;
using MaleFashion.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaleFashion.Models.Service
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.OrderByDescending(b => b.DatePosted).ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _context.BlogPosts
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddCommentAsync(int blogPostId, string userId, string content)
        {
            var comment = new Comment
            {
                BlogPostId = blogPostId,
                UserId = userId,
                Content = content,
                DatePosted = DateTime.Now
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}