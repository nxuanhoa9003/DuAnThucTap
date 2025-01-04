namespace Web_DonNghiPhep.ViewModels
{
    public class LeaveStatisticsViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public int RemainingLeaveDays { get; set; }
        public int UsedLeaveDays { get; set; }
    }
}
