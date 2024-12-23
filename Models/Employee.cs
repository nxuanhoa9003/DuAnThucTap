using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Mã nhân viên")]
        [StringLength(20, MinimumLength = 5)]
        public string Employee_ID { get; set; }

        [Required]
        [DisplayName("Họ và tên")]
        [StringLength(100, MinimumLength = 3)]
        public string? FullName { get; set; }

        [Required]
        [DisplayName("Ngày sinh")]
        public DateTime? Dob { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DisplayName("Số điện thoại")]
        [Phone]
        public string? PhoneNumber { get; set; }

        // Khóa ngoại tới Department (Phòng ban)
        [DisplayName("Phòng ban")]
        public string? Department_id { get; set; }

        [DisplayName("Chức vụ")]
        public string? Title_id { get; set; }

        [DisplayName("Phòng ban")]
        public virtual Department Department { get; set; }
        [DisplayName("Chức danh")]
        public virtual Titles Title { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
    }

}


