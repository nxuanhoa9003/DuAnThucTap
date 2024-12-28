using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_DonNghiPhep.Data;
using Web_DonNghiPhep.Models;
using Web_DonNghiPhep.Services;
using Web_DonNghiPhep.ViewModels;

namespace Web_DonNghiPhep.Controllers
{
    [Authorize(Roles = "nhân viên, quản lý")]
    public class LeaveRequestsController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;
        public LeaveRequestsController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        // GET: LeaveRequestsController

        public async Task<IActionResult> Index()
        {
            var employeeid = User.FindFirst("Employeeid")?.Value;
            var listlvrequest = await _context.LeaveRequest
                .Include(x => x.ApprovedBy)
                .Where(x => x.Employee_id == employeeid)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return View(listlvrequest);
        }

        // GET: LeaveRequestsController/Details/5
        [HttpGet("chi-tiet-don-nghi-phep/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnghiphep = _context.LeaveRequest.Include(x => x.ApprovedBy).FirstOrDefault(x => x.Id == id);
            if (donnghiphep == null)
            {
                return NotFound();
            }
            return View(donnghiphep);
        }

        // GET: LeaveRequestsController/CreateLeaveRequest
        [HttpGet("tao-don-nghi-phep")]
        public ActionResult CreateLeaveRequest()
        {
            return View();
        }

        // POST: LeaveRequestsController/Create
        [HttpPost("tao-don-nghi-phep")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLeaveRequest([Bind("StartDate, EndDate, Reason")] LeaveRequest leaveRequest)
        {
            DateTime currentTime = DateTime.Now;
            string strer = null;
            if (leaveRequest.StartDate < currentTime)
            {
                strer = "Ngày nghỉ không được nhỏ hơn ngày hiện tại";
            }
            else if (leaveRequest.EndDate < currentTime)
            {
                strer = "Ngày kết thúc nghỉ không được nhỏ hơn ngày hiện tại";
            }
            else if (leaveRequest.EndDate < leaveRequest.StartDate)
            {
                strer = "Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ";
            }

            if (!string.IsNullOrEmpty(strer))
            {
                ModelState.AddModelError("", strer);
                return View(leaveRequest);
            }

            var employeeid = User.FindFirst("Employeeid")?.Value;

            if (string.IsNullOrEmpty(employeeid))
            {

                ModelState.AddModelError("", "Không thể xác định được mã nhân viên. Vui lòng đăng nhập lại.");
                return View(leaveRequest);
            }

            leaveRequest.Employee_id = employeeid;



            ModelState.Remove(nameof(leaveRequest.Employee_id));

            if (ModelState.IsValid)
            {

                // tìm phòng ban
                var department = await _context.DepartmentEmployee.Include(x => x.Department).ThenInclude(d => d.Parent)
                        .Where(de => de.EmployeeId == employeeid)
                        .Select(de => de.Department)
                        .FirstOrDefaultAsync();
                
                leaveRequest.DepartmentId = department.Department_id;

                // tìm trưởng phòng
                if (department.ManagerId == employeeid)
                {
                    var deparent = department.Parent;

                    while (deparent != null)
                    {
                        if (deparent.ManagerId != null)
                        {
                            leaveRequest.NextApproverId = deparent.ManagerId;
                            break;
                        }

                        department = deparent.Parent;
                    }
                }
                else
                {
                    leaveRequest.NextApproverId = department.ManagerId;
                }



                leaveRequest.CreatedAt = DateTime.Now;
                leaveRequest.UpdatedAt = DateTime.Now;

                _context.Add(leaveRequest);
                var rsc = await _context.SaveChangesAsync();
                if (rsc > 0)
                {
                    _messageService.SetMessage("Tạo đơn thành công");
                }
                else
                {
                    _messageService.SetMessage("Tạo đơn thất bại", "error");
                }
                return RedirectToAction(nameof(Index));
            }
            foreach (var state in ModelState)
            {
                Console.WriteLine($"Key: {state.Key}");
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            return View(leaveRequest);
        }

        // GET: LeaveRequestsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequestsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "quản lý")]
        [HttpGet]
        public async Task<ActionResult> ApproveRequests()
        {
            var employeeid = User.FindFirst("Employeeid")?.Value;
            var listrequests = _context.LeaveRequest
                .Include(x => x.Employee)
                    .ThenInclude(x => x.LeaveBalances)
                .Include(x => x.Employee)
                    .ThenInclude(x => x.DepartmentEmployees)
                    .ThenInclude(x => x.Department)
                .Select(x => new LeaveRequestVM
                {
                    Id = x.Id,
                    Employee_id = x.Employee.Employee_ID,
                    Employee_Name = x.Employee.FullName,
                    Reason = x.Reason,
                    Status = x.Status,
                    DepartmentName = _context.Department.Where(z => z.Department_id == x.DepartmentId).First().DepartmentName,
                    TotalDayOff = (x.EndDate - x.StartDate).Days + 1,
                    NextApproverId = x.NextApproverId,

                })
                .Where(x => x.NextApproverId == employeeid && x.Status == "Pending");

            return View(await listrequests.ToListAsync());
        }


        [Authorize(Roles = "quản lý")]
        [HttpGet]
        public async Task<ActionResult> DetailsRequest(int? id)
        {
            if (id == null) return NotFound();

            var requestemp = await _context.LeaveRequest
                .Include(x => x.Employee)
                    .ThenInclude(x => x.LeaveBalances)
                .Include(x => x.Employee)
                    .ThenInclude(x => x.DepartmentEmployees)
                        .ThenInclude(de => de.Department)
                .Where(x => x.Id == id)
                .Select(x => new LeaveRequestVM
                {
                    Id = x.Id,
                    Employee_id = x.Employee.Employee_ID,
                    Employee_Name = x.Employee.FullName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Reason = x.Reason,
                    Status = x.Status,
                    DepartmentName = x.Employee.DepartmentEmployees.FirstOrDefault().Department.DepartmentName,
                    TotalDayOff = EF.Functions.DateDiffDay(x.StartDate, x.EndDate) + 1,
                    MaxDaysoff = x.Employee.LeaveBalances.FirstOrDefault(lb => lb.Year == x.CreatedAt.Year).TotalDays,
                    UseDaysoff = x.Employee.LeaveBalances.FirstOrDefault(lb => lb.Year == x.CreatedAt.Year).UsedDays,
                    RemainDayOff = x.Employee.LeaveBalances.FirstOrDefault(lb => lb.Year == x.CreatedAt.Year).RemainingDays,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .SingleOrDefaultAsync();
            return View(requestemp);
        }

        [Authorize(Roles = "quản lý")]
        [HttpPost]
        public async Task<IActionResult> HandleRequest(int? id, string action)
        {
            if (id == null) return NotFound();

            var request = await _context.LeaveRequest.FirstOrDefaultAsync(x => x.Id == id);

            if (request == null) return NotFound();

            var employeeid = User.FindFirst("Employeeid")?.Value;

            if (action == "approve")
            {
                var department = _context.Department.Include(x => x.Parent).FirstOrDefault(x => x.Department_id == request.DepartmentId);

                if (request.NextApproverId != null)
                {
                    department = _context.Department.Include(x => x.Parent).FirstOrDefault(x => x.ManagerId == request.NextApproverId);

                }

                if (department == null) return NotFound();

                if (department.Parent != null)
                {
                    request.NextApproverId = department.Parent.ManagerId;
                }
                else
                {
                    request.ApprovedById = employeeid;
                    request.Status = "Approved";
                }


            }
            else if (action == "reject")
            {
                request.Status = "Rejected";
                request.ApprovedById = employeeid;
                request.NextApproverId = null;
            }
            _context.Update(request);
            await _context.SaveChangesAsync();
            _messageService.SetMessage("Đã xác nhận đơn nghỉ phép");
            return RedirectToAction("ApproveRequests");
        }

    }
}
