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
    public class ParcelsController : Controller
    {
        private readonly Hospital_webDatabase _context;

        public ParcelsController(Hospital_webDatabase context)
        {
            _context = context;
        }

        // GET: Parcels
        public async Task<IActionResult> Index()
        {
            var hospital_webDatabase = _context.Parcels.Include(p => p.Companies).Include(p => p.Recievers).Include(p => p.Senders);
            return View(await hospital_webDatabase.ToListAsync());
        }

        // GET: Parcels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcels = await _context.Parcels
                .Include(p => p.Companies)
                .Include(p => p.Recievers)
                .Include(p => p.Senders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcels == null)
            {
                return NotFound();
            }

            return View(parcels);
        }

        // GET: Parcels/Create
        public IActionResult Create()
        {
            ViewData["CompaniesId"] = new SelectList(_context.Companies, "Id", "Email_Id");
            ViewData["RecieversId"] = new SelectList(_context.Recievers, "Id", "Email_Id");
            ViewData["SendersId"] = new SelectList(_context.Senders, "Id", "Email_Id");
            return View();
        }

        // POST: Parcels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Delivery_address,Parcel_weight,Content_type,Shipping_cost,SendersId,CompaniesId,RecieversId")] Parcels parcels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompaniesId"] = new SelectList(_context.Companies, "Id", "Email_Id", parcels.CompaniesId);
            ViewData["RecieversId"] = new SelectList(_context.Recievers, "Id", "Email_Id", parcels.RecieversId);
            ViewData["SendersId"] = new SelectList(_context.Senders, "Id", "Email_Id", parcels.SendersId);
            return View(parcels);
        }

        // GET: Parcels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcels = await _context.Parcels.FindAsync(id);
            if (parcels == null)
            {
                return NotFound();
            }
            ViewData["CompaniesId"] = new SelectList(_context.Companies, "Id", "Email_Id", parcels.CompaniesId);
            ViewData["RecieversId"] = new SelectList(_context.Recievers, "Id", "Email_Id", parcels.RecieversId);
            ViewData["SendersId"] = new SelectList(_context.Senders, "Id", "Email_Id", parcels.SendersId);
            return View(parcels);
        }

        // POST: Parcels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Delivery_address,Parcel_weight,Content_type,Shipping_cost,SendersId,CompaniesId,RecieversId")] Parcels parcels)
        {
            if (id != parcels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelsExists(parcels.Id))
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
            ViewData["CompaniesId"] = new SelectList(_context.Companies, "Id", "Email_Id", parcels.CompaniesId);
            ViewData["RecieversId"] = new SelectList(_context.Recievers, "Id", "Email_Id", parcels.RecieversId);
            ViewData["SendersId"] = new SelectList(_context.Senders, "Id", "Email_Id", parcels.SendersId);
            return View(parcels);
        }

        // GET: Parcels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcels = await _context.Parcels
                .Include(p => p.Companies)
                .Include(p => p.Recievers)
                .Include(p => p.Senders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcels == null)
            {
                return NotFound();
            }

            return View(parcels);
        }

        // POST: Parcels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcels = await _context.Parcels.FindAsync(id);
            _context.Parcels.Remove(parcels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelsExists(int id)
        {
            return _context.Parcels.Any(e => e.Id == id);
        }
    }
}
