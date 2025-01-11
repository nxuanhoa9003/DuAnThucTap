using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_DonNghiPhep.Models;

namespace Web_DonNghiPhep.ViewModels
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Mã Nhân viên")]
        public string? Employee_id { get; set; }

        [Display(Name = "Họ tên nhân viên")]
        public string? Employee_Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày bắt đầu nghỉ")]
        public DateTime StartDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        [Display(Name = "Nghỉ đến ngày")]
        public DateTime EndDate { get; set; }

        [MaxLength(500, ErrorMessage = "Lý do không được quá 500 ký tự")]
        [MinLength(5, ErrorMessage = "Lý do nghỉ phép phải có ít nhất 5 ký tự!")]
        [Display(Name = "Lý do")]
        public string? Reason { get; set; }

        // Trạng thái yêu cầu (Pending, Approved, Rejected).
       
        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Người phê duyệt")]
        public string? FullNameApprovedBy { get; set; } // ID của người phê duyệt (nullable nếu chưa được duyệt).

        [Display(Name = "Phòng ban")]
        public string DepartmentName { set; get; }

        [Display(Name = "Số ngày xin nghỉ")]
        public int? TotalDayOff { set; get; }

        [Display(Name = "Số ngày nghỉ tối đa")]
        public int? MaxDaysoff { set; get; }
        [Display(Name = "Số ngày đã dùng")]
        public int? UseDaysoff { set; get; }
        [Display(Name = "Số ngày còn lại")]
        public int? RemainDayOff { set; get; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        [Display(Name = "Ngày tạo đơn")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public string? NextApproverId { get; set; }
    }
}
