using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaleFashion.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; } 

        [Required(ErrorMessage = "Họ là bắt buộc")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Quốc gia là bắt buộc")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Thành phố là bắt buộc")]
        public string City { get; set; }

        [Required(ErrorMessage = "Quốc gia/State là bắt buộc")]
        public string State { get; set; }

        [Required(ErrorMessage = "Mã bưu điện là bắt buộc")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public string? OrderNotes { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}