using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_web.Data;
using Hospital_web.Models;

namespace Hospital_web.Controllers
{
    public class TrackingsController : Controller
    {
        private readonly Hospital_webDatabase _context;

        public TrackingsController(Hospital_webDatabase context)
        {
            _context = context;
        }

        // GET: Trackings
        public async Task<IActionResult> Index()
        {
            var hospital_webDatabase = _context.Trackings.Include(t => t.Parcels);
            return View(await hospital_webDatabase.ToListAsync());
        }

        // GET: Trackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackings = await _context.Trackings
                .Include(t => t.Parcels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trackings == null)
            {
                return NotFound();
            }

            return View(trackings);
        }

        // GET: Trackings/Create
        public IActionResult Create()
        {
            ViewData["ParcelsId"] = new SelectList(_context.Set<Parcels>(), "Id", "Content_type");
            return View();
        }

        // POST: Trackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Expected_date_of_delivery,ParcelsId")] Trackings trackings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trackings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcelsId"] = new SelectList(_context.Set<Parcels>(), "Id", "Content_type", trackings.ParcelsId);
            return View(trackings);
        }

        // GET: Trackings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackings = await _context.Trackings.FindAsync(id);
            if (trackings == null)
            {
                return NotFound();
            }
            ViewData["ParcelsId"] = new SelectList(_context.Set<Parcels>(), "Id", "Content_type", trackings.ParcelsId);
            return View(trackings);
        }

        // POST: Trackings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Expected_date_of_delivery,ParcelsId")] Trackings trackings)
        {
            if (id != trackings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackingsExists(trackings.Id))
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
            ViewData["ParcelsId"] = new SelectList(_context.Set<Parcels>(), "Id", "Content_type", trackings.ParcelsId);
            return View(trackings);
        }

        // GET: Trackings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackings = await _context.Trackings
                .Include(t => t.Parcels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trackings == null)
            {
                return NotFound();
            }

            return View(trackings);
        }

        // POST: Trackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trackings = await _context.Trackings.FindAsync(id);
            _context.Trackings.Remove(trackings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackingsExists(int id)
        {
            return _context.Trackings.Any(e => e.Id == id);
        }
    }
}
