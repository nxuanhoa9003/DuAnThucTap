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
    [Authorize(Roles = "admin")]
    [Route("chuc-danh")]
    public class TitlesController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;

        public TitlesController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;

        }

        // GET: Titles
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Title.ToListAsync());
        }

        // GET: Titles/Details/5
        [HttpGet("chi-tiet-chuc-danh/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titles = await _context.Title
                .FirstOrDefaultAsync(m => m.Title_id == id);
            if (titles == null)
            {
                return NotFound();
            }

            return View(titles);
        }

        // GET: Titles/Create
        [HttpGet("tao-moi-chuc-danh")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("tao-moi-chuc-danh")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title_id,Title_name")] Titles title)
        {

            if (ModelState.IsValid)
            {
                if (await _context.Title.AnyAsync(x => x.Title_id == title.Title_id!.Trim()))
                {
                    ModelState.AddModelError("Title_id", "Mã chức danh đã tồn tại");
                    return View(title);
                }

                if (await _context.Title.AnyAsync(x => x.Title_name == title.Title_name!.Trim()))
                {
                    ModelState.AddModelError("Title_name", "Tên chức danh đã tồn tại");
                    return View(title);
                }

                _context.Add(title);
                await _context.SaveChangesAsync();
                _messageService.SetMessage("Thêm chức danh thành công");
                return RedirectToAction(nameof(Index));
            }
            
            return View(title);
        }

        // GET: Titles/Edit/5
        [HttpGet("sua-chuc-danh")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titles = await _context.Title.FindAsync(id);
            if (titles == null)
            {
                return NotFound();
            }
            return View(titles);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("sua-chuc-danh")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title_id,Title_name")] Titles title)
        {
            if (id != title.Title_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var titlecurrent = await _context.Title.AsNoTracking().FirstOrDefaultAsync(x => x.Title_id == id);
                if(titlecurrent == null)
                {
                    return NotFound();
                }
                
                if (titlecurrent.Title_name != title.Title_name && await _context.Title.AnyAsync(x => x.Title_name == title.Title_name!.Trim()))
                {
                    ModelState.AddModelError("Title_name", "Tên chức danh đã tồn tại");
                    return View(title);
                }

                try
                {
                    _context.Update(title);
                    _messageService.SetMessage("Cập nhật chức danh thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitlesExists(title.Title_id))
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
            _messageService.SetMessage("Cập nhật chức danh thất bại");
            return View(title);
        }

        // GET: Titles/Delete/5
        [HttpGet("xoa-chuc-danh")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titles = await _context.Title
                .FirstOrDefaultAsync(m => m.Title_id == id);
            if (titles == null)
            {
                return NotFound();
            }

            return View(titles);
        }

        // POST: Titles/Delete/5
        [HttpPost("xoa-chuc-danh"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var titles = await _context.Title.FindAsync(id);
            if (titles != null)
            {
                if(await _context.Employee.AnyAsync(x => x.Title_id == titles.Title_id))
                {
                    _messageService.SetMessage("Không thể xoá chức danh", "error");
                    return RedirectToAction(nameof(Index));
                }
                _context.Title.Remove(titles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TitlesExists(string id)
        {
            return _context.Title.Any(e => e.Title_id == id);
        }
    }
}
