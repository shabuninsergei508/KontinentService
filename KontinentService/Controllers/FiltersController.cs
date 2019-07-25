using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KontinentService.Models;

namespace KontinentService.Controllers
{
    public class FiltersController : Controller
    {
        private readonly DefaultContext _context;

        public FiltersController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Filters
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Filters.Include(f => f.Category);
            return View(await defaultContext.ToListAsync());
        }

        // GET: Filters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filtersModel = await _context.Filters
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filtersModel == null)
            {
                return NotFound();
            }

            return View(filtersModel);
        }

        // GET: Filters/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        // POST: Filters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,IsSpecial,IsTour,IsBustour,IsCruise,CategoryId")] FiltersModel filtersModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filtersModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", filtersModel.CategoryId);
            return View(filtersModel);
        }

        // GET: Filters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filtersModel = await _context.Filters.FindAsync(id);
            if (filtersModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", filtersModel.CategoryId);
            return View(filtersModel);
        }

        // POST: Filters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IsSpecial,IsTour,IsBustour,IsCruise,CategoryId")] FiltersModel filtersModel)
        {
            if (id != filtersModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filtersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiltersModelExists(filtersModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", filtersModel.CategoryId);
            return View(filtersModel);
        }

        // GET: Filters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filtersModel = await _context.Filters
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filtersModel == null)
            {
                return NotFound();
            }

            return View(filtersModel);
        }

        // POST: Filters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filtersModel = await _context.Filters.FindAsync(id);
            _context.Filters.Remove(filtersModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FiltersModelExists(int id)
        {
            return _context.Filters.Any(e => e.Id == id);
        }
    }
}
