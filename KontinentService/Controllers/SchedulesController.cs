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
    public class SchedulesController : Controller
    {
        private readonly DefaultContext _context;

        public SchedulesController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Schedules.Include(s => s.Tour);
            return View(await defaultContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedulesModel = await _context.Schedules
                .Include(s => s.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedulesModel == null)
            {
                return NotFound();
            }

            return View(schedulesModel);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,TourId")] SchedulesModel schedulesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedulesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id", schedulesModel.TourId);
            return View(schedulesModel);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedulesModel = await _context.Schedules.FindAsync(id);
            if (schedulesModel == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id", schedulesModel.TourId);
            return View(schedulesModel);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,TourId")] SchedulesModel schedulesModel)
        {
            if (id != schedulesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedulesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedulesModelExists(schedulesModel.Id))
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
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Id", schedulesModel.TourId);
            return View(schedulesModel);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedulesModel = await _context.Schedules
                .Include(s => s.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedulesModel == null)
            {
                return NotFound();
            }

            return View(schedulesModel);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedulesModel = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(schedulesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulesModelExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
