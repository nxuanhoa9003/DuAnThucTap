using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_DonNghiPhep.Data;
using Web_DonNghiPhep.Models;
using Web_DonNghiPhep.Services;
using Web_DonNghiPhep.ViewModels;

namespace Web_DonNghiPhep.Controllers
{
    [Authorize(Roles = "admin")]
    //[Route("phong-ban")]
    public class DepartmentsController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;
        public DepartmentsController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }

        // GET: Departments
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var listdepart = await _context.Department.Include(x => x.DepartmentEmployees)
                .Select(x => new DepartmentVM
                {
                    Department_id = x.Department_id,
                    DepartmentName = x.DepartmentName,
                    Manager = x.DepartmentEmployees
                            .Where(e => e.EmployeeId == x.ManagerId && e.DepartmentId == x.Department_id)
                            .Select(e => e.Employee.FullName)
                            .FirstOrDefault(),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).ToListAsync();


            return View(listdepart);
        }

        // GET: Departments/Details/5
        [HttpGet("chi-tiet-phong-ban/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = _context.DepartmentEmployee.Where(x => x.DepartmentId == id && x.EmployeeIsManager == true).Select(x => x.Employee).FirstOrDefault();
            string? managername = emp?.FullName;
            var department = await _context.Department.Include(x => x.Parent).Include(x => x.DepartmentEmployees)
                .Select(x => new DepartmentVM
                {
                    Department_id = x.Department_id,
                    DepartmentName = x.DepartmentName,
                    Manager = managername,
                    Parent = x.Parent != null ? x.Parent.DepartmentName : null
                })
                .FirstOrDefaultAsync(m => m.Department_id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        [HttpGet("tao-moi-phong-ban")]
        public IActionResult Create()
        {
            ViewBag.DepartmentParent = new SelectList(_context.Department.ToList(), "Department_id", "DepartmentName", "Department_id");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("tao-moi-phong-ban")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Department_id,DepartmentName,ParentId")] Department department)
        {
            ViewBag.DepartmentParent = new SelectList(_context.Department.ToList(), "Department_id", "DepartmentName", "Department_id");


            if (!string.IsNullOrEmpty(department.ParentId))
            {
                var parentDepartment = await _context.Department
                    .FirstOrDefaultAsync(d => d.Department_id == department.ParentId);

                if (parentDepartment == null)
                {
                    ModelState.AddModelError("ParentId", "Phòng ban cấp trên không tồn tại.");
                }
            }
            else
            {
                department.ParentId = null; // Xử lý trường hợp không có cấp trên
            }
            if (ModelState.IsValid)
            {

                if (_context.Department.Any(x => x.Department_id == department.Department_id!.Trim()))
                {
                    ModelState.AddModelError("Department_id", "Mã phòng ban đã tồn tại.");
                    return View(department);
                }

                if (_context.Department.Any(x => x.DepartmentName == department.DepartmentName!.Trim()))
                {
                    ModelState.AddModelError("DepartmentName", "Tên phòng ban đã tồn tại.");
                    return View(department);
                }
                _messageService.SetMessage("Tạo phòng ban thành công");
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        [HttpGet("chinh-sua-phong-ban/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentParent = new SelectList(_context.Department.Where(x => x.Department_id != department.Department_id).ToList(), "Department_id", "DepartmentName", department.ParentId);

            var employeesInDepartment = await _context.DepartmentEmployee
               .Where(de => de.DepartmentId == department.Department_id)
               .Select(de => de.Employee)
               .ToListAsync();

            ViewBag.Employee = new SelectList(employeesInDepartment, "Employee_ID", "FullName", department.ManagerId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("chinh-sua-phong-ban/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Department_id,DepartmentName,ManagerId,ParentId")] Department department)
        {
            if (id != department.Department_id)
            {
                return NotFound();
            }

            ViewBag.DepartmentParent = new SelectList(_context.Department.Where(x => x.Department_id != department.Department_id).ToList(), "Department_id", "DepartmentName", department.ParentId);

            var employeesInDepartment = await _context.DepartmentEmployee
                .Where(de => de.DepartmentId == department.Department_id)
                .Select(de => de.Employee)
                .ToListAsync();

            ViewBag.Employee = new SelectList(employeesInDepartment, "Employee_ID", "FullName", department.ManagerId);

            if (ModelState.IsValid)
            {
                try
                {

                    var dpcurrent = await _context.Department.FirstOrDefaultAsync(d => d.Department_id == id);
                    if (dpcurrent == null)
                    {
                        return NotFound();
                    }
                    if (dpcurrent.DepartmentName != department.DepartmentName && _context.Department.Any(x => x.DepartmentName == department.DepartmentName.Trim()))
                    {
                        ModelState.AddModelError("DepartmentName", "Tên phòng ban đã tồn tại.");
                        return View(department);
                    }

                    department.CreatedAt = dpcurrent.CreatedAt;
                    department.UpdatedAt = DateTime.Now;
                    if (dpcurrent.ManagerId != department.ManagerId)
                    {
                        var dpemcurrent = _context.DepartmentEmployee.FirstOrDefault(x => x.DepartmentId == dpcurrent.Department_id && x.EmployeeId == dpcurrent.ManagerId);

                        if (dpemcurrent != null)
                        {
                            dpemcurrent.EmployeeIsManager = false;
                            _context.Update(dpemcurrent);
                        }

                        var dpemexist = _context.DepartmentEmployee.FirstOrDefault(x => x.DepartmentId == department.Department_id && x.EmployeeId == department.ManagerId);

                        if (dpemexist != null)
                        {
                            dpemexist.EmployeeIsManager = true;
                            _context.Update(dpemexist);
                        }

                        var listrequest = _context.LeaveRequest.Where(x => x.Status == "Pending" && x.NextApproverId == dpcurrent.ManagerId).ToList();

                        dpcurrent.ManagerId = department.ManagerId;

                        var newmanagerdepartment = department.ManagerId;
                        foreach (var request in listrequest)
                        {
                            request.NextApproverId = newmanagerdepartment;
                            _context.Update(request);
                        }
                    }
                    _messageService.SetMessage("Cập nhật phòng ban thành công");

                    dpcurrent.ParentId = department.ParentId;

                    _context.Update(dpcurrent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Department_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        [HttpGet("xoa-phong-ban/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var department = await _context.Department
            //    .FirstOrDefaultAsync(m => m.Department_id == id);
            var emp = _context.DepartmentEmployee.Where(x => x.DepartmentId == id && x.EmployeeIsManager == true).Select(x => x.Employee).FirstOrDefault();
            string? managername = emp?.FullName;
            var department = await _context.Department.Include(x => x.Parent).Include(x => x.DepartmentEmployees)
                .Select(x => new DepartmentVM
                {
                    Department_id = x.Department_id,
                    DepartmentName = x.DepartmentName,
                    Manager = managername,
                    Parent = x.Parent != null ? x.Parent.DepartmentName : null
                })
                .FirstOrDefaultAsync(m => m.Department_id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost("xoa-phong-ban/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var emp = _context.DepartmentEmployee.Where(x => x.DepartmentId == id && x.EmployeeIsManager == true).Select(x => x.Employee).FirstOrDefault();
            string? managername = emp?.FullName;
            var departmentcurrent = await _context.Department.Include(x => x.Parent).Include(x => x.DepartmentEmployees)
                .Select(x => new DepartmentVM
                {
                    Department_id = x.Department_id,
                    DepartmentName = x.DepartmentName,
                    Manager = managername,
                    Parent = x.Parent != null ? x.Parent.DepartmentName : null
                })
                .FirstOrDefaultAsync(m => m.Department_id == id);

            if (departmentcurrent != null)
            {
                var department = await _context.Department.FindAsync(id);
                if (_context.DepartmentEmployee.Any(x => x.DepartmentId == department.Department_id))
                {
                    _messageService.SetMessage("Không thể xoá phòng ban đang có nhân viên", "error");
                    return View(departmentcurrent);

                }
                _messageService.SetMessage("Xoá thành công");
                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(departmentcurrent);
        }

        private bool DepartmentExists(string id)
        {
            return _context.Department.Any(e => e.Department_id == id);
        }

        public async Task<IActionResult> LeaveStatistics(int? yearselect, string? departmentId, int? page = 1)
        {

            var currentYear = DateTime.Now.Year;
            var years = Enumerable.Range(2000, currentYear - 2000 + 1).ToList(); // Từ 2000 đến năm hiện tại

            yearselect = yearselect ?? currentYear;

            var departmentname = _context.Department.FirstOrDefault(x => x.Department_id == departmentId)?.DepartmentName;
            var listempdp = _context.DepartmentEmployee.Where(x => x.DepartmentId == departmentId).Select(x => x.EmployeeId).ToList();
            var listleavebance = await _context.LeaveBalance
                .Include(x => x.Employee)
                .Where(x => x.Year == yearselect && listempdp.Contains(x.Employee_id))
                .Select(x => new LeaveStatisticsViewModel
                {
                    EmployeeId = x.Employee_id,
                    EmployeeName = x.Employee.FullName,
                    DepartmentName = departmentname,
                    RemainingLeaveDays = x.RemainingDays,
                    UsedLeaveDays = x.UsedDays
                })
                .ToListAsync();

            List<string> listfilter = new List<string>();
            listfilter.Add("yearselect=" + yearselect);
            if (departmentId != null)
            {
                listfilter.Add("departmentId=" + departmentId);
            }

            string strfilter = string.Join("&", listfilter);
            ViewBag.Strfilter = strfilter;



            int pageSize = 5;
            int totalItems = listleavebance.Count;

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var listleavebancers = listleavebance.Skip((page.Value - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Action = "LeaveStatistics";

            ViewBag.Years = years;
            ViewBag.CurrentYear = currentYear;
            ViewBag.SelectedYear = yearselect;
            ViewBag.Department = new SelectList(await _context.Department.ToListAsync(), "Department_id", "DepartmentName", departmentId);

            if (listleavebance.Count == 0 && departmentId != null)
            {
                _messageService.SetMessage("Không có dữ liệu", "error");
            }
            return View(listleavebancers);
        }
    }
}
