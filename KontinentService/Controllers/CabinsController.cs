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
    public class CabinsController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment _appEnvironment;


        public CabinsController(DefaultContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Cabins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cabins.ToListAsync());
        }

        // GET: Cabins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinsModel = await _context.Cabins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cabinsModel == null)
            {
                return NotFound();
            }

            return View(cabinsModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Image,Price,ShipName")] CabinsModel cabinsModel, IFormFile imageFile)
        {
            if (imageFile != null)
                cabinsModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);
            if (ModelState.IsValid)
            {
                _context.Add(cabinsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cabinsModel);
        }

        // GET: Cabins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinsModel = await _context.Cabins.FindAsync(id);
            if (cabinsModel == null)
            {
                return NotFound();
            }
            return View(cabinsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image,Price,ShipName")] CabinsModel cabinsModel, IFormFile imageFile)
        {
            if (id != cabinsModel.Id)
            {
                return NotFound();
            }

            if (imageFile != null)
                cabinsModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cabinsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CabinsModelExists(cabinsModel.Id))
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
            return View(cabinsModel);
        }

        // GET: Cabins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinsModel = await _context.Cabins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cabinsModel == null)
            {
                return NotFound();
            }

            return View(cabinsModel);
        }

        // POST: Cabins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cabinsModel = await _context.Cabins.FindAsync(id);
            _context.Cabins.Remove(cabinsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CabinsModelExists(int id)
        {
            return _context.Cabins.Any(e => e.Id == id);
        }
    }
}
