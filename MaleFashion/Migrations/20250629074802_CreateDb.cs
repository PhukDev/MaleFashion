using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaleFashion.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousPostId = table.Column<int>(type: "int", nullable: true),
                    NextPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Author", "Content", "DatePosted", "ImageUrl", "NextPostId", "PreviousPostId", "Quote", "QuoteAuthor", "Tags", "Title" },
                values: new object[,]
                {
                    { 1, "Deercreative", "Hydroderm là kem chống lão hóa rất được ưa chuộng. Sản phẩm này ngăn ngừa các dấu hiệu lão hóa sớm trên da và giữ cho làn da trẻ trung, săn chắc và khỏe mạnh hơn. Nó làm giảm nếp nhăn và sự lỏng lẻo của da. Kem này nuôi dưỡng làn da và mang lại vẻ rạng rỡ đã mất trong những năm bận rộn.", new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-1.jpg", 2, null, "Khi thiết kế một quảng cáo cho một sản phẩm cụ thể, cần nghiên cứu nhiều thứ như nơi nó nên được hiển thị.", "John Smith", "#Fashion,#Trending,#2020", "Những loại máy uốn tóc nào tốt nhất" },
                    { 2, "Deercreative", "Nhẫn Eternity là biểu tượng của tình yêu vĩnh cửu. Bài viết này sẽ giới thiệu về thiết kế và ý nghĩa của nhẫn Eternity, mang lại sự lựa chọn hoàn hảo cho những dịp đặc biệt.", new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-2.jpg", 3, 1, "Tình yêu bền vững như những viên kim cương trên nhẫn Eternity.", "Jane Doe", "#Jewelry,#Wedding", "Nhẫn Eternity bền mãi mãi" },
                    { 3, "Deercreative", "Kính mát không chỉ là phụ kiện thời trang mà còn bảo vệ mắt khỏi tia UV. Bài viết này khám phá các lợi ích sức khỏe của việc đeo kính mát.", new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-3.jpg", 4, 2, "Bảo vệ đôi mắt cũng là bảo vệ sức khỏe.", "Dr. Lee", "#Health,#Fashion", "Lợi ích sức khỏe của kính mát" },
                    { 4, "Deercreative", "Phẫu thuật nâng ngực là lựa chọn phổ biến để cải thiện vóc dáng. Bài viết này cung cấp thông tin về quy trình và lợi ích.", new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-4.jpg", 5, 3, "Tự tin với cơ thể là chìa khóa của vẻ đẹp.", "Anna Smith", "#Beauty,#Health", "Phẫu thuật nâng ngực - nâng cao sắc đẹp" },
                    { 5, "Deercreative", "Nhẫn cưới là biểu tượng của tình yêu và cam kết. Bài viết này chia sẻ các mẫu nhẫn cưới đẹp và ý nghĩa.", new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-5.jpg", 6, 4, "Một chiếc nhẫn, một lời hứa vĩnh cửu.", "Emily Brown", "#Wedding,#Jewelry", "Nhẫn cưới - Món quà cả đời" },
                    { 6, "Deercreative", "Tẩy lông là một phần quan trọng trong chăm sóc cá nhân. Bài viết này so sánh các phương pháp tẩy lông phổ biến.", new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-6.jpg", 7, 5, "Làn da mịn màng là sự tự tin.", "Sarah Wilson", "#Beauty,#Skincare", "Các phương pháp tẩy lông khác nhau" },
                    { 7, "Deercreative", "Bông tai vòng đã tồn tại qua nhiều thế kỷ và vẫn là xu hướng thời trang. Bài viết này khám phá lịch sử và phong cách của chúng.", new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-7.jpg", 8, 6, "Đơn giản nhưng nổi bật.", "Lisa Taylor", "#Fashion,#Jewelry", "Bông tai vòng - phong cách từ lịch sử" },
                    { 8, "Deercreative", "Phẫu thuật Lasik mang lại thị lực rõ nét mà không cần kính. Bài viết này giải thích quy trình và những điều cần biết.", new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-8.jpg", 9, 7, "Nhìn thế giới rõ hơn với Lasik.", "Dr. Kim", "#Health,#Vision", "Phẫu thuật mắt Lasik - Bạn đã sẵn sàng chưa" },
                    { 9, "Deercreative", "Phẫu thuật Lasik mang lại thị lực rõ nét mà không cần kính. Bài viết này cung cấp thêm chi tiết về lợi ích và rủi ro.", new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/img/blog/blog-9.jpg", null, 8, "Tự do khỏi kính mắt với Lasik.", "Dr. Park", "#Health,#Vision", "Phẫu thuật mắt Lasik - Bạn đã sẵn sàng chưa" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Túi xách" },
                    { 2, "Quần áo" },
                    { 3, "Giày dép" },
                    { 4, "Phụ kiện" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BlogPostId", "Content", "DatePosted", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Bài viết rất hữu ích, cảm ơn!", new DateTime(2020, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@example.com" },
                    { 2, 1, "Tôi đang tìm loại máy uốn tóc tốt, bài này giúp tôi hiểu rõ hơn!", new DateTime(2020, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@example.com" },
                    { 3, 2, "Nhẫn Eternity thật sự rất đẹp!", new DateTime(2020, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3@example.com" },
                    { 4, 3, "Tôi không biết kính mát có lợi ích sức khỏe, cảm ơn bài viết!", new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "user4@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Color", "Description", "ImageUrl", "IsNew", "IsOnSale", "Name", "Price", "Rating", "Size" },
                values: new object[,]
                {
                    { 1, 2, "Black", "A stylish black biker jacket made from premium piqué fabric.", "~/img/product/product-1.jpg", true, false, "Piqué Biker Jacket", 67.24m, 0, "M" },
                    { 2, 2, "Blue", "A modern blue biker jacket with a sleek design.", "~/img/product/product-2.jpg", false, true, "Piqué Biker Jacket 2", 67.24m, 0, "L" },
                    { 3, 1, "Black", "A versatile black chest bag with multiple pockets for convenience.", "~/img/product/product-3.jpg", false, true, "Multi-pocket Chest Bag", 43.48m, 4, "One Size" },
                    { 4, 4, "Grey", "A trendy grey cap with a unique diagonal texture.", "~/img/product/product-4.jpg", false, false, "Diagonal Textured Cap", 60.90m, 0, "One Size" },
                    { 5, 1, "Brown", "A durable brown leather backpack for everyday use.", "~/img/product/product-5.jpg", true, false, "Lether Backpack", 31.37m, 0, "One Size" },
                    { 6, 3, "Black", "Classic black ankle boots crafted for comfort and style.", "~/img/product/product-6.jpg", false, true, "Ankle Boots", 98.49m, 4, "42" },
                    { 7, 2, "White", "A casual white t-shirt with a contrast pocket design.", "~/img/product/product-7.jpg", true, false, "T-shirt Contrast Pocket", 49.66m, 0, "M" },
                    { 8, 4, "Red", "A lightweight red scarf for a stylish accessory.", "~/img/product/product-8.jpg", false, false, "Basic Flowing Scarf", 26.28m, 0, "One Size" },
                    { 9, 2, "Blue", "Comfortable blue slim fit jeans for a modern look.", "~/img/product/product-9.jpg", true, false, "Slim Fit Jeans", 55.99m, 3, "32" },
                    { 10, 3, "White", "Stylish white leather sneakers for casual wear.", "~/img/product/product-10.jpg", false, true, "Leather Sneakers", 89.99m, 4, "41" },
                    { 11, 4, "Grey", "A cozy grey woolen hat for cold weather.", "~/img/product/product-11.jpg", true, false, "Woolen Hat", 15.99m, 2, "One Size" },
                    { 12, 1, "Beige", "A practical beige canvas tote bag for daily use.", "~/img/product/product-12.jpg", false, false, "Canvas Tote Bag", 39.99m, 3, "One Size" },
                    { 13, 2, "Green", "A comfortable green hooded sweatshirt for casual wear.", "~/img/product/product-13.jpg", true, false, "Hooded Sweatshirt", 45.00m, 0, "L" },
                    { 14, 4, "Black", "Sleek black sunglasses with UV protection.", "~/img/product/product-14.jpg", false, true, "Sunglasses", 29.99m, 5, "One Size" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
