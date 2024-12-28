using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{

    public class LeaveBalance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        [Display(Name = "Tên nhân viên")]
        public string? Employee_id { get; set; }

        [Required]
        [Range(2000, int.MaxValue)]
        [Display(Name = "Năm")]
        public int Year { get; set; }

        [Required]
        [Range(0, 365)]
        [Display(Name = "Số ngày nghỉ tối đa")]
        public int TotalDays { get; set; }

        
        [Range(0, 365)]
        [Display(Name = "Số ngày đã nghỉ")]
        public int UsedDays { get; set; }

        
        [Range(0, 365)]
        [Display(Name = "Số ngày nghỉ còn lại")]
        public int RemainingDays { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        //[Required]
        [NotMapped]
        [Display(Name = "Nhân viên (chọn một hoặc nhiều)")]
        public List<string>? Employee_ids { get; set; }

        [NotMapped]
        public string? FullName { get; set; }
        public virtual Employee? Employee { get; set; }
    }

}
