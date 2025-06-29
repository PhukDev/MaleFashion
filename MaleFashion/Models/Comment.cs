namespace MaleFashion.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
