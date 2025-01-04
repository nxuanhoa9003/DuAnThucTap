using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nhân viên")]
        public string? Employee_id { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày bắt đầu nghỉ")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

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
        public string? ApprovedById { get; set; }
        [Display(Name = "Người phê duyệt tiếp theo")]
        public string? NextApproverId { get; set; }
        [Display(Name = "Phòng ban")]
        public string? DepartmentId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        [Display(Name = "Ngày tạo đơn")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Employee? Employee { get; set; }
        [Display(Name = "Người phê duyệt")]
        public virtual Employee? ApprovedBy { get; set; }
        public virtual Employee? NextApprover { get; set; }
        public virtual Department? Department { get; set; }
        public ICollection<ApprovalHistory>? ApprovalHistories { get; set; }
    }
}
