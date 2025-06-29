using System.ComponentModel.DataAnnotations;

namespace MaleFashion.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng.")]
        [StringLength(100, ErrorMessage = "Tên người dùng phải từ {2} đến {1} ký tự.", MinimumLength = 3)] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}

