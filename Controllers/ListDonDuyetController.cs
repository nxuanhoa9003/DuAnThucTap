using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_DonNghiPhep.Data;
using Web_DonNghiPhep.Services;
using Web_DonNghiPhep.ViewModels;

namespace Web_DonNghiPhep.Controllers
{
    [Authorize]
    public class ListDonDuyetController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;
        public ListDonDuyetController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        // LỊCH SỬ ĐƠN DUYỆT
        public async Task<IActionResult> Index(string searchString, string status, DateTime? fromDate, DateTime? toDate)
        {
           
            var managerId = User.FindFirst("EmployeeId")?.Value;

            if (string.IsNullOrEmpty(managerId))
            {
                return Unauthorized();
            }

            
            var approvedRequestsQuery = _context.LeaveRequest
                .Include(r => r.Employee)
                .Include(r => r.ApprovalHistories)
                .Where(r => r.ApprovalHistories.Any(h => h.ApprovedById == managerId));

            // Lọc theo trạng thái (nếu có chọn)
            if (!string.IsNullOrEmpty(status) && status != "Tất cả")
            {
                approvedRequestsQuery = approvedRequestsQuery.Where(r => r.Status == status);
            }

            // Lọc theo ngày (nếu có chọn)
            if (fromDate.HasValue)
            {
                approvedRequestsQuery = approvedRequestsQuery.Where(r => r.StartDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                approvedRequestsQuery = approvedRequestsQuery.Where(r => r.EndDate <= toDate.Value);
            }

            
            if (!string.IsNullOrEmpty(searchString))
            {
                approvedRequestsQuery = approvedRequestsQuery.Where(r =>
                    r.Id.ToString().Contains(searchString) ||
                    EF.Functions.Like(r.Reason, $"%{searchString}%"));
            }

            
            var approvedRequests = await approvedRequestsQuery
                .Select(r => new ListNghiPhepVM
                {
                    MaDon = r.Id,
                    NgayBatDau = r.StartDate,
                    NgayKetThuc = r.EndDate,
                    SoNgay = (r.EndDate - r.StartDate).Days + 1,
                    LyDo = r.Reason,
                    TrangThai = r.Status,
                    TenNhanVien = r.Employee.FullName,
                    NgayXuLy = r.UpdatedAt
                }).ToListAsync();

            if(approvedRequests.Count  == 0)
            {
                _messageService.SetMessage("Không tìm thấy đơn nào phù hợp", "warning");
            }

            return View(approvedRequests);
        }

        public async Task<IActionResult> ChiTiet(string maDon)
        {

            var managerId = User.FindFirst("EmployeeId")?.Value;

            if (string.IsNullOrEmpty(managerId))
            {
                return Unauthorized();
            }

            if (string.IsNullOrEmpty(maDon))
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequest
                .Include(r => r.Employee)
                .Include(x => x.ApprovalHistories)
                .FirstOrDefaultAsync(r => r.Id.ToString() == maDon);
            
            if (leaveRequest == null)
            {
                return NotFound();
            }

           

            var model = new ListNghiPhepVM
            {
                MaDon = leaveRequest.Id,
                NgayBatDau = leaveRequest.StartDate,
                NgayKetThuc = leaveRequest.EndDate,
                SoNgay = (leaveRequest.EndDate - leaveRequest.StartDate).Days + 1,
                LyDo = leaveRequest.Reason,
                TrangThai = leaveRequest.ApprovalHistories.FirstOrDefault(x => x.ApprovedById == managerId && x.LeaveRequestId == int.Parse(maDon)).Action,
                TenNhanVien = leaveRequest.Employee.FullName,
                NgayXuLy = leaveRequest.UpdatedAt,
                MaPhongBan = leaveRequest.DepartmentId
            };

            return View(model);
        }
    };

          

}
