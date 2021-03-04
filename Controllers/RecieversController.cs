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
    public class RecieversController : Controller
    {
        private readonly Hospital_webDatabase _context;

        public RecieversController(Hospital_webDatabase context)
        {
            _context = context;
        }

        // GET: Recievers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recievers.ToListAsync());
        }

        // GET: Recievers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recievers = await _context.Recievers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recievers == null)
            {
                return NotFound();
            }

            return View(recievers);
        }

        // GET: Recievers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recievers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email_Id,Address,Mobile_Number")] Recievers recievers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recievers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recievers);
        }

        // GET: Recievers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recievers = await _context.Recievers.FindAsync(id);
            if (recievers == null)
            {
                return NotFound();
            }
            return View(recievers);
        }

        // POST: Recievers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email_Id,Address,Mobile_Number")] Recievers recievers)
        {
            if (id != recievers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recievers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecieversExists(recievers.Id))
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
            return View(recievers);
        }

        // GET: Recievers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recievers = await _context.Recievers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recievers == null)
            {
                return NotFound();
            }

            return View(recievers);
        }

        // POST: Recievers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recievers = await _context.Recievers.FindAsync(id);
            _context.Recievers.Remove(recievers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecieversExists(int id)
        {
            return _context.Recievers.Any(e => e.Id == id);
        }
    }
}
