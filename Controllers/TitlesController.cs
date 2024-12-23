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

namespace Web_DonNghiPhep.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("chuc-danh")]
    public class TitlesController : Controller
    {
        private readonly MyDBContext _context;

        public TitlesController(MyDBContext context)
        {
            _context = context;
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
                _context.Add(title);
                await _context.SaveChangesAsync();
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
                try
                {
                    _context.Update(title);
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
