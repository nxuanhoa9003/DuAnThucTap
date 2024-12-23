using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_DonNghiPhep.Models
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Role_ID { get; set; }
        [DisplayName("Tên quyền")]
        public string? Role_Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
