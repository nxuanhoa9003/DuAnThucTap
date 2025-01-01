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
using Web_DonNghiPhep.ViewModels;

namespace Web_DonNghiPhep.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("phong-ban")]
    public class DepartmentsController : Controller
    {
        private readonly MyDBContext _context;

        public DepartmentsController(MyDBContext context)
        {
            _context = context;
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
            var department = await _context.Department.Include(x => x.DepartmentEmployees)
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
        public async Task<IActionResult> Create([Bind("Department_id,DepartmentName, ParentId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.DepartmentParent = new SelectList(_context.Department.ToList(), "Department_id", "DepartmentName", "Department_id");
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

            if (ModelState.IsValid)
            {
                try
                {
                    var dpcurrent = await _context.Department.FirstOrDefaultAsync(d => d.Department_id == id);
                    department.CreatedAt = dpcurrent.CreatedAt;
                    department.UpdatedAt = DateTime.Now;
                    if(dpcurrent.ManagerId != department.ManagerId)
                    {
                      

                        var dpemcurrent = _context.DepartmentEmployee.FirstOrDefault(x => x.DepartmentId == dpcurrent.Department_id && x.EmployeeId == dpcurrent.ManagerId);

                        if (dpemcurrent != null)
                        {
                            dpemcurrent.EmployeeIsManager = false;
                            _context.Update(dpemcurrent);
                        }

                        var dpemexist = _context.DepartmentEmployee.FirstOrDefault(x => x.DepartmentId == department.Department_id && x.EmployeeId == department.ManagerId);

                        if(dpemexist != null)
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

                        _context.Update(dpcurrent);

                    }
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
            ViewBag.DepartmentParent = new SelectList(_context.Department.Where(x => x.Department_id != department.Department_id).ToList(), "Department_id", "DepartmentName", department.ParentId);

            var employeesInDepartment = await _context.DepartmentEmployee
                .Where(de => de.DepartmentId == department.Department_id)
                .Select(de => de.Employee)
                .ToListAsync();

            ViewBag.Employee = new SelectList(employeesInDepartment, "Employee_ID", "FullName", department.ManagerId);
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

            var department = await _context.Department
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
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(string id)
        {
            return _context.Department.Any(e => e.Department_id == id);
        }
    }
}
