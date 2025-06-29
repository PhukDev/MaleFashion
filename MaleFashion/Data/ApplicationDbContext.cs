using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MaleFashion.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MaleFashion.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Định nghĩa quan hệ khóa ngoại
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(bp => bp.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Order>()
                .Property(o => o.UserId)
                .IsRequired(false);

          
            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Seed Categories 
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Túi xách" },
                new Category { Id = 2, Name = "Quần áo" },
                new Category { Id = 3, Name = "Giày dép" },
                new Category { Id = 4, Name = "Phụ kiện" }
            );

            // Seed Products 
            modelBuilder.Entity<Product>().HasData(
                 new Product { Id = 1, Name = "Piqué Biker Jacket", Description = "A stylish black biker jacket made from premium piqué fabric.", Price = 67.24m, ImageUrl = "/img/product/product-1.jpg", IsNew = true, IsOnSale = false, Rating = 0, Color = "Black", Size = "M", CategoryId = 2 },
                 new Product { Id = 2, Name = "Piqué Biker Jacket 2", Description = "A modern blue biker jacket with a sleek design.", Price = 67.24m, ImageUrl = "/img/product/product-2.jpg", IsNew = false, IsOnSale = true, Rating = 0, Color = "Blue", Size = "L", CategoryId = 2 },
                 new Product { Id = 3, Name = "Multi-pocket Chest Bag", Description = "A versatile black chest bag with multiple pockets for convenience.", Price = 43.48m, ImageUrl = "/img/product/product-3.jpg", IsNew = false, IsOnSale = true, Rating = 4, Color = "Black", Size = "One Size", CategoryId = 1 },
                 new Product { Id = 4, Name = "Diagonal Textured Cap", Description = "A trendy grey cap with a unique diagonal texture.", Price = 60.90m, ImageUrl = "/img/product/product-4.jpg", IsNew = false, IsOnSale = false, Rating = 0, Color = "Grey", Size = "One Size", CategoryId = 4 },
                 new Product { Id = 5, Name = "Lether Backpack", Description = "A durable brown leather backpack for everyday use.", Price = 31.37m, ImageUrl = "/img/product/product-5.jpg", IsNew = true, IsOnSale = false, Rating = 0, Color = "Brown", Size = "One Size", CategoryId = 1 },
                 new Product { Id = 6, Name = "Ankle Boots", Description = "Classic black ankle boots crafted for comfort and style.", Price = 98.49m, ImageUrl = "/img/product/product-6.jpg", IsNew = false, IsOnSale = true, Rating = 4, Color = "Black", Size = "42", CategoryId = 3 },
                 new Product { Id = 7, Name = "T-shirt Contrast Pocket", Description = "A casual white t-shirt with a contrast pocket design.", Price = 49.66m, ImageUrl = "/img/product/product-7.jpg", IsNew = true, IsOnSale = false, Rating = 0, Color = "White", Size = "M", CategoryId = 2 },
                 new Product { Id = 8, Name = "Basic Flowing Scarf", Description = "A lightweight red scarf for a stylish accessory.", Price = 26.28m, ImageUrl = "/img/product/product-8.jpg", IsNew = false, IsOnSale = false, Rating = 0, Color = "Red", Size = "One Size", CategoryId = 4 },
                 new Product { Id = 9, Name = "Slim Fit Jeans", Description = "Comfortable blue slim fit jeans for a modern look.", Price = 55.99m, ImageUrl = "/img/product/product-9.jpg", IsNew = true, IsOnSale = false, Rating = 3, Color = "Blue", Size = "32", CategoryId = 2 },
                 new Product { Id = 10, Name = "Leather Sneakers", Description = "Stylish white leather sneakers for casual wear.", Price = 89.99m, ImageUrl = "/img/product/product-10.jpg", IsNew = false, IsOnSale = true, Rating = 4, Color = "White", Size = "41", CategoryId = 3 },
                 new Product { Id = 11, Name = "Woolen Hat", Description = "A cozy grey woolen hat for cold weather.", Price = 15.99m, ImageUrl = "/img/product/product-11.jpg", IsNew = true, IsOnSale = false, Rating = 2, Color = "Grey", Size = "One Size", CategoryId = 4 },
                 new Product { Id = 12, Name = "Canvas Tote Bag", Description = "A practical beige canvas tote bag for daily use.", Price = 39.99m, ImageUrl = "/img/product/product-12.jpg", IsNew = false, IsOnSale = false, Rating = 3, Color = "Beige", Size = "One Size", CategoryId = 1 },
                 new Product { Id = 13, Name = "Hooded Sweatshirt", Description = "A comfortable green hooded sweatshirt for casual wear.", Price = 45.00m, ImageUrl = "/img/product/product-13.jpg", IsNew = true, IsOnSale = false, Rating = 0, Color = "Green", Size = "L", CategoryId = 2 },
                 new Product { Id = 14, Name = "Sunglasses", Description = "Sleek black sunglasses with UV protection.", Price = 29.99m, ImageUrl = "/img/product/product-14.jpg", IsNew = false, IsOnSale = true, Rating = 5, Color = "Black", Size = "One Size", CategoryId = 4 }
            );

            // Seed BlogPosts
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost { Id = 1, Title = "Những loại máy uốn tóc nào tốt nhất", Content = "Hydroderm là kem chống lão hóa rất được ưa chuộng. Sản phẩm này ngăn ngừa các dấu hiệu lão hóa sớm trên da và giữ cho làn da trẻ trung, săn chắc và khỏe mạnh hơn. Nó làm giảm nếp nhăn và sự lỏng lẻo của da. Kem này nuôi dưỡng làn da và mang lại vẻ rạng rỡ đã mất trong những năm bận rộn.", DatePosted = new DateTime(2020, 2, 16), Author = "Deercreative", Quote = "Khi thiết kế một quảng cáo cho một sản phẩm cụ thể, cần nghiên cứu nhiều thứ như nơi nó nên được hiển thị.", QuoteAuthor = "John Smith", Tags = "#Fashion,#Trending,#2020", ImageUrl = "/img/blog/blog-1.jpg", PreviousPostId = null, NextPostId = 2 },
                new BlogPost { Id = 2, Title = "Nhẫn Eternity bền mãi mãi", Content = "Nhẫn Eternity là biểu tượng của tình yêu vĩnh cửu. Bài viết này sẽ giới thiệu về thiết kế và ý nghĩa của nhẫn Eternity, mang lại sự lựa chọn hoàn hảo cho những dịp đặc biệt.", DatePosted = new DateTime(2020, 2, 21), Author = "Deercreative", Quote = "Tình yêu bền vững như những viên kim cương trên nhẫn Eternity.", QuoteAuthor = "Jane Doe", Tags = "#Jewelry,#Wedding", ImageUrl = "/img/blog/blog-2.jpg", PreviousPostId = 1, NextPostId = 3 },
                new BlogPost { Id = 3, Title = "Lợi ích sức khỏe của kính mát", Content = "Kính mát không chỉ là phụ kiện thời trang mà còn bảo vệ mắt khỏi tia UV. Bài viết này khám phá các lợi ích sức khỏe của việc đeo kính mát.", DatePosted = new DateTime(2020, 2, 28), Author = "Deercreative", Quote = "Bảo vệ đôi mắt cũng là bảo vệ sức khỏe.", QuoteAuthor = "Dr. Lee", Tags = "#Health,#Fashion", ImageUrl = "/img/blog/blog-3.jpg", PreviousPostId = 2, NextPostId = 4 },
                new BlogPost { Id = 4, Title = "Phẫu thuật nâng ngực - nâng cao sắc đẹp", Content = "Phẫu thuật nâng ngực là lựa chọn phổ biến để cải thiện vóc dáng. Bài viết này cung cấp thông tin về quy trình và lợi ích.", DatePosted = new DateTime(2020, 2, 16), Author = "Deercreative", Quote = "Tự tin với cơ thể là chìa khóa của vẻ đẹp.", QuoteAuthor = "Anna Smith", Tags = "#Beauty,#Health", ImageUrl = "/img/blog/blog-4.jpg", PreviousPostId = 3, NextPostId = 5 },
                new BlogPost { Id = 5, Title = "Nhẫn cưới - Món quà cả đời", Content = "Nhẫn cưới là biểu tượng của tình yêu và cam kết. Bài viết này chia sẻ các mẫu nhẫn cưới đẹp và ý nghĩa.", DatePosted = new DateTime(2020, 2, 21), Author = "Deercreative", Quote = "Một chiếc nhẫn, một lời hứa vĩnh cửu.", QuoteAuthor = "Emily Brown", Tags = "#Wedding,#Jewelry", ImageUrl = "/img/blog/blog-5.jpg", PreviousPostId = 4, NextPostId = 6 },
                new BlogPost { Id = 6, Title = "Các phương pháp tẩy lông khác nhau", Content = "Tẩy lông là một phần quan trọng trong chăm sóc cá nhân. Bài viết này so sánh các phương pháp tẩy lông phổ biến.", DatePosted = new DateTime(2020, 2, 28), Author = "Deercreative", Quote = "Làn da mịn màng là sự tự tin.", QuoteAuthor = "Sarah Wilson", Tags = "#Beauty,#Skincare", ImageUrl = "/img/blog/blog-6.jpg", PreviousPostId = 5, NextPostId = 7 },
                new BlogPost { Id = 7, Title = "Bông tai vòng - phong cách từ lịch sử", Content = "Bông tai vòng đã tồn tại qua nhiều thế kỷ và vẫn là xu hướng thời trang. Bài viết này khám phá lịch sử và phong cách của chúng.", DatePosted = new DateTime(2020, 2, 16), Author = "Deercreative", Quote = "Đơn giản nhưng nổi bật.", QuoteAuthor = "Lisa Taylor", Tags = "#Fashion,#Jewelry", ImageUrl = "/img/blog/blog-7.jpg", PreviousPostId = 6, NextPostId = 8 },
                new BlogPost { Id = 8, Title = "Phẫu thuật mắt Lasik - Bạn đã sẵn sàng chưa", Content = "Phẫu thuật Lasik mang lại thị lực rõ nét mà không cần kính. Bài viết này giải thích quy trình và những điều cần biết.", DatePosted = new DateTime(2020, 2, 21), Author = "Deercreative", Quote = "Nhìn thế giới rõ hơn với Lasik.", QuoteAuthor = "Dr. Kim", Tags = "#Health,#Vision", ImageUrl = "/img/blog/blog-8.jpg", PreviousPostId = 7, NextPostId = 9 },
                new BlogPost { Id = 9, Title = "Phẫu thuật mắt Lasik - Bạn đã sẵn sàng chưa", Content = "Phẫu thuật Lasik mang lại thị lực rõ nét mà không cần kính. Bài viết này cung cấp thêm chi tiết về lợi ích và rủi ro.", DatePosted = new DateTime(2020, 2, 28), Author = "Deercreative", Quote = "Tự do khỏi kính mắt với Lasik.", QuoteAuthor = "Dr. Park", Tags = "#Health,#Vision", ImageUrl = "/img/blog/blog-9.jpg", PreviousPostId = 8, NextPostId = null }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, BlogPostId = 1, UserId = "user1@example.com", Content = "Bài viết rất hữu ích, cảm ơn!", DatePosted = new DateTime(2020, 2, 17) },
                new Comment { Id = 2, BlogPostId = 1, UserId = "user2@example.com", Content = "Tôi đang tìm loại máy uốn tóc tốt, bài này giúp tôi hiểu rõ hơn!", DatePosted = new DateTime(2020, 2, 18) },
                new Comment { Id = 3, BlogPostId = 2, UserId = "user3@example.com", Content = "Nhẫn Eternity thật sự rất đẹp!", DatePosted = new DateTime(2020, 2, 22) },
                new Comment { Id = 4, BlogPostId = 3, UserId = "user4@example.com", Content = "Tôi không biết kính mát có lợi ích sức khỏe, cảm ơn bài viết!", DatePosted = new DateTime(2020, 2, 29) }
            );
        }
    }
}