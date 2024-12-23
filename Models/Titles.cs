using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_DonNghiPhep.Models
{
    public class Titles
    {
        [Key]
        [Required]
        [DisplayName("Mã chức danh")]
        public string? Title_id { get; set; }

        [Required]
        [DisplayName("Tên chức danh")]
        public string? Title_name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
