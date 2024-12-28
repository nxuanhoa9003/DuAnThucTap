using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_DonNghiPhep.ViewModels
{
    public class DepartmentVM
    {
        [DisplayName("Mã phòng ban")]
        public string Department_id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Tên phòng ban")]
        public string DepartmentName { get; set; }

        
        [Display(Name = "Trưởng phòng")]
        public string? Manager { get; set; }

        [Display(Name = "Phòng ban cấp trên")]
        public string? Parent { get; set; } = null;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
