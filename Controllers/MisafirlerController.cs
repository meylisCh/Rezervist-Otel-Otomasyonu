using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rezervist.Data;
using Rezervist.Models;

namespace Rezervist.Controllers
{
    public class MisafirlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MisafirlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Misafirler
        public async Task<IActionResult> Index(string aramaKelimesi)
{
    var misafirler = from m in _context.Misafirler
                     select m;

    if (!string.IsNullOrEmpty(aramaKelimesi))
    {
        // Misafirlerde 'AdSoyad' veya 'TCKimlik' içinde arama yapıyoruz
        misafirler = misafirler.Where(s => s.AdSoyad.Contains(aramaKelimesi) 
                                        || s.TCKimlik.Contains(aramaKelimesi));
    }

    return View(await misafirler.ToListAsync());
}

        // GET: Misafirler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var misafir = await _context.Misafirler
                .FirstOrDefaultAsync(m => m.MisafirID == id);
            if (misafir == null)
            {
                return NotFound();
            }

            return View(misafir);
        }

        // GET: Misafirler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Misafirler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MisafirID,AdSoyad,Telefon,TCKimlik")] Misafir misafir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(misafir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(misafir);
        }

        // GET: Misafirler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var misafir = await _context.Misafirler.FindAsync(id);
            if (misafir == null)
            {
                return NotFound();
            }
            return View(misafir);
        }

        // POST: Misafirler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MisafirID,AdSoyad,Telefon,TCKimlik")] Misafir misafir)
        {
            if (id != misafir.MisafirID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(misafir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MisafirExists(misafir.MisafirID))
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
            return View(misafir);
        }

        // GET: Misafirler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var misafir = await _context.Misafirler
                .FirstOrDefaultAsync(m => m.MisafirID == id);
            if (misafir == null)
            {
                return NotFound();
            }

            return View(misafir);
        }

        // POST: Misafirler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var misafir = await _context.Misafirler.FindAsync(id);
            if (misafir != null)
            {
                _context.Misafirler.Remove(misafir);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MisafirExists(int id)
        {
            return _context.Misafirler.Any(e => e.MisafirID == id);
        }
    }
}
