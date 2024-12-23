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
            var listdepart = await _context.Department.Include(x => x.Employees)
                .Select(x => new DepartmentVM
                {
                    Department_id = x.Department_id,
                    DepartmentName = x.DepartmentName,
                    Manager = x.Employees
                            .Where(e => e.Employee_ID == x.ManagerId)
                            .Select(e => e.FullName)
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

            var department = await _context.Department
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
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("tao-moi-phong-ban")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Department_id,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
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
            
            ViewBag.Employee = new SelectList(_context.Employee.Where(x => x.Department_id == id), "Employee_ID", "FullName", department.ManagerId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("chinh-sua-phong-ban/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Department_id,DepartmentName,ManagerId")] Department department)
        {
            if (id != department.Department_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dpcurrent = await _context.Department.AsNoTracking().FirstOrDefaultAsync(d => d.Department_id == id);
                    department.CreatedAt = dpcurrent.CreatedAt;
                    department.UpdatedAt = DateTime.Now;

                    _context.Update(department);
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
            ViewBag.Employee = new SelectList(_context.Employee.Where(x => x.Department_id == id), "Employee_ID", "FullName", department.ManagerId);
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
