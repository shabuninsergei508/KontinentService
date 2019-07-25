using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KontinentService.Models;
using KontinentService.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace KontinentService.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public CategoriesController(DefaultContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var categoriesModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesModel == null)
            {
                return NotFound();
            }

            return View(categoriesModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ShortDescription,Image,UrlRus")] CategoriesModel categoriesModel, IFormFile imageFile)
        {
            if (imageFile != null)
                categoriesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);
            if (ModelState.IsValid)
            {
                _context.Add(categoriesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriesModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesModel = await _context.Categories.FindAsync(id);
            if (categoriesModel == null)
            {
                return NotFound();
            }
            return View(categoriesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ShortDescription,Image,UrlRus")] CategoriesModel categoriesModel, IFormFile imageFile)
        {
            if (id != categoriesModel.Id)
            {
                return NotFound();
            }

            if (imageFile != null)
                categoriesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesModelExists(categoriesModel.Id))
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
            return View(categoriesModel);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesModel == null)
            {
                return NotFound();
            }

            return View(categoriesModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriesModel = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoriesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesModelExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
