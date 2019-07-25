using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KontinentService.Models;
using Microsoft.AspNetCore.Http;
using KontinentService.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace KontinentService.Controllers
{
    public class SubcategoriesController : Controller
    {
        private readonly DefaultContext _context;
        private IHostingEnvironment _appEnvironment;

        public SubcategoriesController(DefaultContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Subcategories.Include(s => s.Category);
            return View(await defaultContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategoriesModel = await _context.Subcategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategoriesModel == null)
            {
                return NotFound();
            }

            return View(subcategoriesModel);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ShortDescription,Image,UrlRus,CategoryId")] SubcategoriesModel subcategoriesModel, IFormFile imageFile)
        {
            if (imageFile != null)
                subcategoriesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);
            if (ModelState.IsValid)
            {
                _context.Add(subcategoriesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", subcategoriesModel.CategoryId);
            return View(subcategoriesModel);
        }

        // GET: Subcategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategoriesModel = await _context.Subcategories.FindAsync(id);
            if (subcategoriesModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", subcategoriesModel.CategoryId);
            return View(subcategoriesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ShortDescription,Image,UrlRus,CategoryId")] SubcategoriesModel subcategoriesModel, IFormFile imageFile)
        {
            if (id != subcategoriesModel.Id)
            {
                return NotFound();
            }

            if (imageFile != null || subcategoriesModel.Image == null)
                subcategoriesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcategoriesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcategoriesModelExists(subcategoriesModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", subcategoriesModel.CategoryId);
            return View(subcategoriesModel);
        }

        // GET: Subcategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategoriesModel = await _context.Subcategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategoriesModel == null)
            {
                return NotFound();
            }

            return View(subcategoriesModel);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcategoriesModel = await _context.Subcategories.FindAsync(id);
            _context.Subcategories.Remove(subcategoriesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubcategoriesModelExists(int id)
        {
            return _context.Subcategories.Any(e => e.Id == id);
        }
    }
}
