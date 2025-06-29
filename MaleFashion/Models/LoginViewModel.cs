﻿using System.ComponentModel.DataAnnotations;

namespace MaleFashion.Models
{
    public class LoginViewModel
    {
         [Required(ErrorMessage = "Vui lòng nhập email.")] 
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")] 
        public string Email { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}

