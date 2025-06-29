using System.ComponentModel.DataAnnotations;

namespace MaleFashion.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        [Required]
        public string Author { get; set; }
        public string Quote { get; set; }
        public string QuoteAuthor { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public int? PreviousPostId { get; set; }
        public int? NextPostId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
