using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // ID của người dùng yêu cầu nghỉ phép.
        [Required]
        [Display(Name = "Nhân viên")]
        public string Employee_id { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu nghỉ")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Nghỉ đến ngày")]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Lý do")]
        public string? Reason { get; set; }

        // Trạng thái yêu cầu (Pending, Approved, Rejected).
        [Required]
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Pending"; 

        [Display(Name = "Người phê duyệt")]
        public string? ApprovedBy { get; set; } // ID của người phê duyệt (nullable nếu chưa được duyệt).

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày tạo đơn")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public Employee Employee { get; set; }
    }
}
