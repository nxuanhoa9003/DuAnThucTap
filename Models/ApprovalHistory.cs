namespace Web_DonNghiPhep.Models
{
    public class ApprovalHistory
    {
        public int Id { get; set; }
        public int LeaveRequestId { get; set; }
        public LeaveRequest LeaveRequest { get; set; }
        public string ApprovedById { get; set; }
        public string Action { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
