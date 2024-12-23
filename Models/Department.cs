using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{
    public class Department
    {
        [Key]
        [DisplayName("Mã phòng ban")]
        public string Department_id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Tên phòng ban")]
        public string DepartmentName { get; set; }

        // Trưởng phòng (Manager)
        [Display(Name = "Trưởng phòng")]
        public string? ManagerId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        //public Employee Manager { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
    }

}
