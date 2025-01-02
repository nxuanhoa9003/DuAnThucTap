namespace Web_DonNghiPhep.Models
{
    public class ApprovalHistory
    {
        public int Id { get; set; }
        public int LeaveRequestId { get; set; }
        public string ApprovedById { get; set; }
        public string Action { get; set; }
        public DateTime ProcessedAt { get; set; }

        public virtual LeaveRequest? LeaveRequest { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
