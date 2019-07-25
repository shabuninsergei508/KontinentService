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
using System.IO;
using KontinentService.Helpers;
using KontinentService.ViewModels;

namespace KontinentService.Controllers
{
    public class ToursController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public ToursController(DefaultContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Tours.Include(t => t.Subcategory);
            return View(await defaultContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toursModel = await _context.Tours
                .Include(t => t.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toursModel == null)
            {
                return NotFound();
            }

            return View(toursModel);
        }

        public IActionResult Create()
        {
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ShortDescription,Image,Price,Discount,UrlRus,SpecialDescription,NumberOfOrders,IndexOnPage,DateIn,DateOut,Route,IsHot,IsTour,IsBustour,IsCruise,SubcategoryId")] ToursModel toursModel, IFormFile imageFile)
        {
            if (imageFile != null)
                toursModel.Image =  ImageHelper.AddImage(_appEnvironment, imageFile);
            if (ModelState.IsValid)
            {
                _context.Add(toursModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Title", toursModel.SubcategoryId);
            return View(toursModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toursModel = await _context.Tours.FindAsync(id);
            if (toursModel == null)
            {
                return NotFound();
            }
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Title", toursModel.SubcategoryId);
            return View(toursModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ShortDescription,Image,Price,Discount,UrlRus,SpecialDescription,NumberOfOrders,IndexOnPage,DateIn,DateOut,Route,IsHot,IsTour,IsBustour,IsCruise,SubcategoryId")] ToursModel toursModel, IFormFile imageFile)
        {
            if (id != toursModel.Id)
            {
                return NotFound();
            }

            if (imageFile != null)
                toursModel.Image = ImageHelper.AddImage(_appEnvironment, imageFile);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toursModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToursModelExists(toursModel.Id))
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
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Id", toursModel.SubcategoryId);
            return View(toursModel);
        }

        public async Task<IActionResult> EditProperties(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toursModel = await _context.Tours.FindAsync(id);
            List<PropertiesModel> properties = await _context.Properties.ToListAsync();
            List<PropertyValuesModel> propertieValues = await _context.PropertyValues.Where(p => p.TourId == id).ToListAsync();
            if (propertieValues.Count != 0)
            {
                foreach (PropertiesModel p in properties)
                {
                    if (propertieValues.FirstOrDefault(pv => pv.PropertyId == p.Id) != null)
                    {
                        p.IsChecked = true;
                    }
                }
            }
            

            ToursExtViewModel tour = new ToursExtViewModel
            {
                Id = toursModel.Id,
                Title = toursModel.Title,
                Properties = properties,
            };

            if (toursModel == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperties(int id, [Bind("Id, Properties")] ToursExtViewModel tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            List<PropertyValuesModel> oldPropertieValues = await _context.PropertyValues.Where(p => p.TourId == id).ToListAsync();
            if (oldPropertieValues.Count != 0)
            {
                foreach (var item in tour.Properties)
                {
                    if (item.IsChecked)
                    {
                        if (oldPropertieValues.FirstOrDefault(pv => pv.PropertyId == item.Id && pv.TourId == tour.Id) == null)
                            _context.Update(new PropertyValuesModel { PropertyId = item.Id, TourId = tour.Id });
                    }
                    else
                    {
                        if (oldPropertieValues.FirstOrDefault(pv => pv.PropertyId == item.Id && pv.TourId == tour.Id) != null)
                            _context.Remove(oldPropertieValues.First(pv => pv.PropertyId == item.Id && pv.TourId == tour.Id));
                    }
                }
            }
            else
            {
                foreach (var item in tour.Properties)
                    if (item.IsChecked)
                        _context.Update(new PropertyValuesModel { PropertyId = item.Id, TourId = tour.Id });
            }

            await _context.SaveChangesAsync();    
            return RedirectToAction(nameof(Index));
        }


        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toursModel = await _context.Tours
                .Include(t => t.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toursModel == null)
            {
                return NotFound();
            }

            return View(toursModel);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toursModel = await _context.Tours.FindAsync(id);
            _context.Tours.Remove(toursModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToursModelExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }
    }
}
