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
    public class SendersController : Controller
    {
        private readonly Hospital_webDatabase _context;

        public SendersController(Hospital_webDatabase context)
        {
            _context = context;
        }

        // GET: Senders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Senders.ToListAsync());
        }

        // GET: Senders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var senders = await _context.Senders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (senders == null)
            {
                return NotFound();
            }

            return View(senders);
        }

        // GET: Senders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Senders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email_Id,Address,Mobile_Number")] Senders senders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(senders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(senders);
        }

        // GET: Senders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var senders = await _context.Senders.FindAsync(id);
            if (senders == null)
            {
                return NotFound();
            }
            return View(senders);
        }

        // POST: Senders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email_Id,Address,Mobile_Number")] Senders senders)
        {
            if (id != senders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(senders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SendersExists(senders.Id))
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
            return View(senders);
        }

        // GET: Senders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var senders = await _context.Senders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (senders == null)
            {
                return NotFound();
            }

            return View(senders);
        }

        // POST: Senders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var senders = await _context.Senders.FindAsync(id);
            _context.Senders.Remove(senders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SendersExists(int id)
        {
            return _context.Senders.Any(e => e.Id == id);
        }
    }
}
