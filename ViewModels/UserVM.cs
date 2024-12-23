using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web_DonNghiPhep.ViewModels
{
    public class UserVM
    {
        [Required]
        [DisplayName("Tên đăng nhập")]
        [StringLength(20, MinimumLength = 3)]
        public string? UserName { get; set; }
        [Required]
        [DisplayName("Mật khẩu")]
        [StringLength(20, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
