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
    public class OdalarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OdalarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Odalar
public async Task<IActionResult> Index()
{
    var odalar = await _context.Odalar.ToListAsync();
    var bugun = DateTime.Now;

    // OTOMATİK SENKRONİZASYON:
    // Her oda için kontrol et: Şu an bu odada "Giriş Yapıldı" durumunda olan bir rezervasyon var mı?
    foreach (var oda in odalar)
    {
        bool suanDoluMu = _context.Rezervasyonlar.Any(r => 
            r.OdaID == oda.OdaID && 
            r.Durum == "Giriş Yapıldı" &&
            r.GirisTarihi <= bugun && 
            r.CikisTarihi > bugun // Henüz çıkmamış
        );

        // Eğer veritabanındaki durum ile gerçek durum farklıysa düzelt
        if (oda.DoluMu != suanDoluMu)
        {
            oda.DoluMu = suanDoluMu;
            _context.Update(oda);
        }
    }
    
    // Değişiklik varsa kaydet
    await _context.SaveChangesAsync();

    return View(odalar);
}


        // GET: Odalar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oda = await _context.Odalar
                .FirstOrDefaultAsync(m => m.OdaID == id);
            if (oda == null)
            {
                return NotFound();
            }

            return View(oda);
        }

        // GET: Odalar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Odalar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OdaID,OdaNumarasi,OdaTipi,Fiyat,DoluMu")] Oda oda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oda);
        }

        // GET: Odalar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oda = await _context.Odalar.FindAsync(id);
            if (oda == null)
            {
                return NotFound();
            }
            return View(oda);
        }

        // POST: Odalar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OdaID,OdaNumarasi,OdaTipi,Fiyat,DoluMu")] Oda oda)
        {
            if (id != oda.OdaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdaExists(oda.OdaID))
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
            return View(oda);
        }

        // GET: Odalar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oda = await _context.Odalar
                .FirstOrDefaultAsync(m => m.OdaID == id);
            if (oda == null)
            {
                return NotFound();
            }

            return View(oda);
        }

        // POST: Odalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oda = await _context.Odalar.FindAsync(id);
            if (oda != null)
            {
                _context.Odalar.Remove(oda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdaExists(int id)
        {
            return _context.Odalar.Any(e => e.OdaID == id);
        }
        // GET: Temizlik Listesi (Sadece kirli odaları gösterir)
public async Task<IActionResult> TemizlikListesi()
{
    // Dolu olmayan ama Kirli olan odalar (Veya dolu olup temizlik isteyenler)
    // Basit mantık: Temiz olmayan tüm odaları getir.
    var kirliOdalar = await _context.Odalar.Where(o => o.TemizMi == false).ToListAsync();
    return View(kirliOdalar);
}

// POST: Odayı Temizle (Tek Tıkla Temizlendi İşaretle)
[HttpPost]
public async Task<IActionResult> Temizle(int id)
{
    var oda = await _context.Odalar.FindAsync(id);
    if (oda != null)
    {
        oda.TemizMi = true; // Tertemiz oldu
        _context.Update(oda);
        await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(TemizlikListesi)); // Listeye geri dön
}
    }
}
