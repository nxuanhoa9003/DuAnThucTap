using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web_DonNghiPhep.ViewModels
{
    public class UserVM
    {
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        [DisplayName("Tên đăng nhập")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        public string? Password { get; set; }
    }
}
