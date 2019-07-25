using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KontinentService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using KontinentService.Helpers;

namespace KontinentService.Controllers
{
    public class FilterAllowablesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FilterAllowablesController(DefaultContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: FilterAllowables
        public async Task<IActionResult> Index(int? id)
        {
            if(id != null)
            {
                var filter = _context.Filters.Find(id);
                var defaultContext = _context.FilterAllowables.Include(f => f.Filter).Where(f => f.FilterId == id);
                ViewBag.Filter = filter;
                return View(await defaultContext.ToListAsync());
            }
            else
            {
                var filter = _context.Filters.Find(id);
                var defaultContext = _context.FilterAllowables.Include(f => f.Filter);
                return View(await defaultContext.ToListAsync());
            }        
        }

        // GET: FilterAllowables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterAllowablesModel = await _context.FilterAllowables
                .Include(f => f.Filter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filterAllowablesModel == null)
            {
                return NotFound();
            }

            return View(filterAllowablesModel);
        }

        // GET: FilterAllowables/Create
        public IActionResult Create(int? id)
        {
            ViewData["FilterId"] = new SelectList(_context.Filters, "Id", "Title");
            if (id != null)
                ViewBag.Filter = _context.Filters.Find(id);
            return View();
        }

        // POST: FilterAllowables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Image,FilterId")] FilterAllowablesModel filterAllowablesModel, IFormFile imageFile)
        {
            //if (imageFile != null)
            //    filterAllowablesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);
             if (ModelState.IsValid)
            {
                _context.Add(filterAllowablesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = filterAllowablesModel.FilterId });
            }
            ViewData["FilterId"] = new SelectList(_context.Filters, "Id", "Title");
            ViewBag.Filter = _context.Filters.Find(filterAllowablesModel.FilterId);
            return View(filterAllowablesModel);
        }

        // GET: FilterAllowables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterAllowablesModel = await _context.FilterAllowables.FindAsync(id);
            filterAllowablesModel.Filter = await _context.Filters.FindAsync(filterAllowablesModel.FilterId);
            if (filterAllowablesModel == null)
            {
                return NotFound();
            }
            return View(filterAllowablesModel);
        }

        // POST: FilterAllowables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Image,FilterId")] FilterAllowablesModel filterAllowablesModel, IFormFile imageFile)
        {
            if (id != filterAllowablesModel.Id)
            {
                return NotFound();
            }

            //if (imageFile != null)
            //    filterAllowablesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filterAllowablesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterAllowablesModelExists(filterAllowablesModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = filterAllowablesModel.FilterId });
            }
            
            return View(filterAllowablesModel);
        }

        // GET: FilterAllowables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterAllowablesModel = await _context.FilterAllowables
                .Include(f => f.Filter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filterAllowablesModel == null)
            {
                return NotFound();
            }

            return View(filterAllowablesModel);
        }

        // POST: FilterAllowables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filterAllowablesModel = await _context.FilterAllowables.FindAsync(id);
            _context.FilterAllowables.Remove(filterAllowablesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = filterAllowablesModel.FilterId });
        }

        private bool FilterAllowablesModelExists(int id)
        {
            return _context.FilterAllowables.Any(e => e.Id == id);
        }
    }
}
