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
    public class PropertiesController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public PropertiesController(DefaultContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Properties.Include(p => p.Category);
            return View(await defaultContext.ToListAsync());
        }


        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertiesModel = await _context.Properties
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertiesModel == null)
            {
                return NotFound();
            }

            return View(propertiesModel);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ShortDescription,Image,CategoryId")] PropertiesModel propertiesModel, IFormFile imageFile)
        {
            if (imageFile != null)
                propertiesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);
            if (ModelState.IsValid)
            {
                _context.Add(propertiesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", propertiesModel.CategoryId);
            return View(propertiesModel);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertiesModel = await _context.Properties.FindAsync(id);
            if (propertiesModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", propertiesModel.CategoryId);
            return View(propertiesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ShortDescription,Image,CategoryId")] PropertiesModel propertiesModel, IFormFile imageFile)
        {
            if (id != propertiesModel.Id)
            {
                return NotFound();
            }
            if (imageFile != null)
                propertiesModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertiesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertiesModelExists(propertiesModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", propertiesModel.CategoryId);
            return View(propertiesModel);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertiesModel = await _context.Properties
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertiesModel == null)
            {
                return NotFound();
            }

            return View(propertiesModel);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertiesModel = await _context.Properties.FindAsync(id);
            var propValues = _context.PropertyValues.Where(pV => pV.PropertyId == id).ToList();
            if(propValues.Count != 0)
                foreach (var pV in propValues)
                    _context.PropertyValues.Remove(pV);
            _context.Properties.Remove(propertiesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertiesModelExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
