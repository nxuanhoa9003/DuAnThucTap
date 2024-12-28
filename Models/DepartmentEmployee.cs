namespace Web_DonNghiPhep.Models
{
    public class DepartmentEmployee
    {
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
        public bool EmployeeIsManager { get; set; } = false;
        public Department Department { get; set; }
        public Employee Employee { get; set; }
    }
}
