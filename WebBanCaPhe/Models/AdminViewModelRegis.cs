using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanCaPhe.Models
{
    public class AdminViewModelRegis
    {
        [Required(ErrorMessage = "Bạn chưa nhập tài khoản")]
        public string TaiKhoan { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập họ tên")]
        public string HoTen { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string DiaChi { get; set; }
        public string Role { get; set; }
    }
}