using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web_DonNghiPhep.ViewModels
{
    public class EmployeeVM
    {

        [Required]
        [DisplayName("Mã nhân viên")]
        [StringLength(20, MinimumLength = 5)]
        public string? Employee_ID { get; set; }


        [Required]
        [DisplayName("Họ và tên")]
        [StringLength(20, MinimumLength = 3)]
        public string? FullName { get; set; }


        [Required]
        [DisplayName("Mật khẩu")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d]{6,}$",
         ErrorMessage = "{0} phải có từ 6 đến 40 ký tự, chứa ít nhất một chữ cái viết hoa, một chữ cái viết thường và một chữ số.")]
        [StringLength(20, MinimumLength = 6)]
        public string? Password { get; set; }

        [Required]
        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }


        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string? Email { get; set; }


        [Required]
        [DisplayName("Số điện thoại")]
        [Phone]
        [RegularExpression(@"^(0\d{9})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Trạng thái tài khoản")]
        public bool Status { get; set; } = true;


        [DisplayName("Phòng ban")]
        public string? Department_name { get; set; }

        [Required]
        [DisplayName("Phòng ban")]
        public string? Department_id { get; set; }


        [DisplayName("Chức vụ")]
        public string? Title_name { get; set; }

        [Required]
        [DisplayName("Chức vụ")]
        public string? Title_id { get; set; }


        [Required]
        [DisplayName("Tên đăng nhập")]
        [StringLength(20, MinimumLength = 3)]
        public string? UserName { get; set; }


        [Required]
        [DisplayName("Vai trò")]
        public List<int> Role_IDs { get; set; }


        [DisplayName("Vai trò")]
        public List<string>? Roles_Name { get; set; }

        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyy}")]

        public DateTime created_at { get; set; }
        [DisplayName("Ngày cập nhật")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyy}")]

        public DateTime updated_at { get; set; }

    }
}
