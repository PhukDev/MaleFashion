using System.ComponentModel.DataAnnotations;
using MaleFashion.Models;
namespace MaleFashion.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Size { get; set; }
        [Required]
        public string Color { get; set; }
        public bool IsNew { get; set; }
        public bool IsOnSale { get; set; }
        public int Rating { get; set; }
    }
}
