using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Web_DonNghiPhep.Models.LeaveBalance;

namespace Web_DonNghiPhep.Models
{
    public class User
    {
        [Key]
        public Guid UserID { set; get; } = Guid.NewGuid();
        [Required]
        [ForeignKey(nameof(EmployeeUs))]
        [StringLength(20, MinimumLength = 5)]
        [DisplayName("Mã nhân viên")]
        public string Employee_ID { get; set; }
        [Required]
        [DisplayName("Tên đăng nhập")]
        [StringLength(20, MinimumLength = 3)]
        public string? UserName { get; set; }
        [Required]
        [DisplayName("Mật khẩu")]
        [StringLength(20, MinimumLength = 6)]
        public string? Password { get; set; }

        [DisplayName("Trạng thái tài khoản")]
        public bool Status { get; set; } = true;
        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime created_at { get; set; } = DateTime.Now;
        [DisplayName("Ngày cập nhật")]
        [DataType(DataType.Date)]
        public DateTime updated_at { get; set; } = DateTime.Now;

        public virtual Employee EmployeeUs { get; set; }
        public virtual ICollection<Log>? Logs { get; set; }

        [DisplayName("Vai trò")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
