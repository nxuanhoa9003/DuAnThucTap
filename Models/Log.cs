using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Tên tài khoản")]
        public Guid? UserId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Hành động")]
        public string? Action { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian")]
        public DateTime ActionTime { get; set; } = DateTime.Now;

        public virtual User? User { get; set; }
    }
}
