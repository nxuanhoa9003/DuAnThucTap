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

namespace Web_DonNghiPhep.Controllers
{
    [Authorize(Roles = "quản lý")]
    public class LeaveBalancesController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;

        public LeaveBalancesController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }

        // GET: LeaveBalances
        public async Task<IActionResult> Index(int? yearselect)
        {

            var currentYear = DateTime.Now.Year;
            var years = Enumerable.Range(2000, currentYear - 2000 + 1).ToList(); // Từ 2000 đến năm hiện tại
            ViewBag.Years = years;
            ViewBag.SelectedYear = yearselect;

            var employeeid = User.FindFirst("Employeeid")?.Value;
            var department = _context.Department.SingleOrDefault(x => x.ManagerId == employeeid);

            if (employeeid != null)
            {
                var listleave = _context.LeaveBalance
                   .Include(l => l.Employee)
                   .ThenInclude(de => de.DepartmentEmployees)
                   .Where(x =>  x.Employee.DepartmentEmployees.Any(z => z.DepartmentId == department.ManagerId)
                    && x.Employee_id != employeeid
                    && department.ManagerId == employeeid
                   );

                if (yearselect != null)
                {
                    listleave = listleave.Where(x => x.Year == yearselect);
                }
                return View(await listleave.ToListAsync());
            }
            return View(null);
        }

        // GET: LeaveBalances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveBalance = await _context.LeaveBalance
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            return View(leaveBalance);
        }

        // GET: LeaveBalances/Create
        public IActionResult Create()
        {
            var employeeid = User.FindFirst("Employeeid")?.Value;
            var department = _context.Department.SingleOrDefault(x => x.ManagerId == employeeid);
            var listemp = _context.DepartmentEmployee.Where(x => x.DepartmentId == department.Department_id && x.EmployeeId != employeeid);
            ViewData["Employee_id"] = new SelectList(listemp, "Employee_ID", "FullName");
            return View();
        }

        // POST: LeaveBalances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year,TotalDays")] LeaveBalance leaveBalance, List<string> Employee_ids)
        {
            var employeeid = User.FindFirst("Employeeid")?.Value;
            var department = _context.Department.SingleOrDefault(x => x.ManagerId == employeeid);
            var listemp = _context.DepartmentEmployee.Where(x => x.DepartmentId == department.Department_id && x.EmployeeId != employeeid);

            var existingEmployees = _context.LeaveBalance
                                    .Include(x => x.Employee)
                                    .Where(x => Employee_ids.Contains(x.Employee_id) && x.Year == leaveBalance.Year)
                                    .Select(x => new { x.Employee.FullName, x.Employee_id })
                                    .ToList();


            var newEmployees = Employee_ids.Except(existingEmployees.Select(e => e.Employee_id)).ToList();


            if (existingEmployees.Any())
            {
                foreach (var emp in existingEmployees)
                {
                    ModelState.AddModelError("", $"Nhân viên {emp.FullName} ({emp.Employee_id}) đã được đặt số ngày nghỉ trong năm {leaveBalance.Year}.");
                }
            }


            if (!newEmployees.Any())
            {
                ViewData["Employee_id"] = new SelectList(listemp, "Employee_ID", "FullName", leaveBalance.Employee_id);
                return View(leaveBalance);
            }


            if (ModelState.IsValid)
            {
                var currentYear = DateTime.Now.Year;
                if (leaveBalance.Year < currentYear)
                {
                    ModelState.AddModelError("", $"Năm cài đặt không được nhỏ hơn năm hiện tại");
                    ViewData["Employee_id"] = new SelectList(listemp, "Employee_ID", "FullName", leaveBalance.Employee_id);
                    return View(leaveBalance);
                }
                foreach (var emid in newEmployees)
                {
                    var newLeaveBalance = new LeaveBalance
                    {
                        Employee_id = emid,
                        Year = leaveBalance.Year,
                        TotalDays = leaveBalance.TotalDays
                    };
                    _context.Add(newLeaveBalance);
                }

                await _context.SaveChangesAsync();
                _messageService.SetMessage("Cài đặt thành công");
                return RedirectToAction(nameof(Index));
            }

            _messageService.SetMessage("Cài đặt thất bại", "error");
            ViewData["Employee_id"] = new SelectList(listemp, "Employee_ID", "FullName", leaveBalance.Employee_id);
            return View(leaveBalance);
        }

        // GET: LeaveBalances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveBalance = await _context.LeaveBalance.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);
            leaveBalance.FullName = leaveBalance.Employee.FullName;
            if (leaveBalance == null)
            {
                return NotFound();
            }

            return View(leaveBalance);
        }

        // POST: LeaveBalances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Employee_id,FullName,Year,TotalDays")] LeaveBalance leaveBalance)
        {
            if (id != leaveBalance.Id)
            {
                return NotFound();
            }

            var currentYear = DateTime.Now.Year;
            if (leaveBalance.Year < currentYear)
            {
                ModelState.AddModelError("", $"Năm cài đặt không được nhỏ hơn năm hiện tại");
                return View(leaveBalance);
            }

            var existingLeaveBalance = await _context.LeaveBalance.FirstOrDefaultAsync(x => x.Id == id);
            if (existingLeaveBalance == null)
            {
                return NotFound();
            }


            if (existingLeaveBalance.Employee_id != leaveBalance.Employee_id || existingLeaveBalance.Year != leaveBalance.Year)
            {
                bool isConflict = await _context.LeaveBalance.AnyAsync(x =>
                    x.Employee_id == leaveBalance.Employee_id && x.Year == leaveBalance.Year && x.Id != leaveBalance.Id);

                if (isConflict)
                {
                    ModelState.AddModelError("", "Nhân viên này đã được đặt số ngày nghỉ trong năm.");
                    return View(leaveBalance);
                }
            }

            if (ModelState.IsValid)
            {
               
                try
                {

                    existingLeaveBalance.Year = leaveBalance.Year;
                    existingLeaveBalance.TotalDays = leaveBalance.TotalDays;
                    existingLeaveBalance.UsedDays = leaveBalance.UsedDays;
                    existingLeaveBalance.RemainingDays = leaveBalance.TotalDays - leaveBalance.UsedDays;
                    existingLeaveBalance.UpdatedAt = DateTime.Now;

                    _context.Update(existingLeaveBalance);
                    await _context.SaveChangesAsync();
                    _messageService.SetMessage("Cập nhật thành công");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveBalanceExists(leaveBalance.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            return View(leaveBalance);
        }

        // GET: LeaveBalances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveBalance = await _context.LeaveBalance
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            return View(leaveBalance);
        }

        // POST: LeaveBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveBalance = await _context.LeaveBalance.FindAsync(id);
            if (leaveBalance != null)
            {
                _context.LeaveBalance.Remove(leaveBalance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveBalanceExists(int id)
        {
            return _context.LeaveBalance.Any(e => e.Id == id);
        }
    }
}
