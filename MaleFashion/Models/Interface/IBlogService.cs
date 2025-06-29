using MaleFashion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaleFashion.Models.Interface
{
    public interface IBlogService
    {
        Task<List<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost> GetBlogPostByIdAsync(int id);
        Task AddCommentAsync(int blogPostId, string userId, string content);
    }
}