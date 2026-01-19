using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rezervist.Data;
using Rezervist.Models;
using Microsoft.AspNetCore.Authorization;

namespace Rezervist.Controllers
{
    [Authorize]
    public class RezervasyonlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervasyonlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervasyonlar (Listeleme ve Arama)
        public async Task<IActionResult> Index(string aramaKelimesi)
        {
            var rezervasyonlar = _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .Include(r => r.Oda)
                .AsQueryable();

            if (!string.IsNullOrEmpty(aramaKelimesi))
            {
                rezervasyonlar = rezervasyonlar.Where(r => (r.Misafir != null && r.Misafir.AdSoyad != null && r.Misafir.AdSoyad.Contains(aramaKelimesi)) 
                                                        || (r.Oda != null && r.Oda.OdaNumarasi != null && r.Oda.OdaNumarasi.Contains(aramaKelimesi)));
            }

            return View(await rezervasyonlar.ToListAsync());
        }

        // GET: Sadece otelde konaklayanları listeler
        public async Task<IActionResult> Konaklayanlar()
        {
            var aktifKonaklamalar = _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .Include(r => r.Oda)
                .Where(r => r.Durum == "Giriş Yapıldı");

            return View(await aktifKonaklamalar.ToListAsync());
        }

        // GET: Rezervasyonlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .Include(r => r.Oda)
                .Include(r => r.Harcamalar)
                .FirstOrDefaultAsync(m => m.RezervasyonID == id);
            
            if (rezervasyon == null) return NotFound();

            return View(rezervasyon);
        }

        // GET: Rezervasyonlar/Create
        public IActionResult Create()
        {
            ViewData["OdaID"] = new SelectList(_context.Odalar, "OdaID", "OdaNumarasi");
            
            var model = new Rezervasyon
            {
                GirisTarihi = DateTime.Today,
                CikisTarihi = DateTime.Today.AddDays(1)
            };
            
            return View(model);
        }

        // POST: Rezervasyonlar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezervasyonID,OdaID,GirisTarihi,CikisTarihi")] Rezervasyon rezervasyon, string misafirAd, string misafirTel)
        {
            if (rezervasyon.CikisTarihi <= rezervasyon.GirisTarihi)
            {
                ModelState.AddModelError("", "HATA: Çıkış tarihi giriş tarihinden sonra olmalıdır!");
            }

            bool cakismaVarMi = _context.Rezervasyonlar.Any(r => 
                r.OdaID == rezervasyon.OdaID && 
                r.Durum != "Çıkış Yapıldı" &&
                r.Durum != "İptal" &&
                (
                    (rezervasyon.GirisTarihi < r.CikisTarihi && rezervasyon.CikisTarihi > r.GirisTarihi)
                )
            );

            if (cakismaVarMi)
            {
                ModelState.AddModelError("", "DİKKAT: Seçilen tarihlerde bu oda zaten DOLU! Lütfen başka tarih veya oda seçiniz.");
            }

            var yasakliMisafir = _context.Misafirler.FirstOrDefault(m => m.Telefon == misafirTel && m.KaraListedeMi == true);
            if (yasakliMisafir != null)
            {
                ModelState.AddModelError("", $"UYARI: {yasakliMisafir.AdSoyad} isimli misafir KARA LİSTEDE! ({yasakliMisafir.OzelNotlar}) Rezervasyon yapılamaz.");
                ViewData["OdaID"] = new SelectList(_context.Odalar, "OdaID", "OdaNumarasi", rezervasyon.OdaID);
                return View(rezervasyon);
            }

            if (!string.IsNullOrEmpty(misafirAd) && !string.IsNullOrEmpty(misafirTel))
            {
                var yeniMisafir = new Misafir { AdSoyad = misafirAd, Telefon = misafirTel };
                _context.Misafirler.Add(yeniMisafir);
                await _context.SaveChangesAsync();
                rezervasyon.MisafirID = yeniMisafir.MisafirID;
            }
            else
            {
                ModelState.AddModelError("", "Misafir bilgileri eksik.");
            }

            if (ModelState.IsValid)
            {
                rezervasyon.Durum = "Bekliyor";
                _context.Add(rezervasyon);
                await _context.SaveChangesAsync();
                TempData["Basari"] = "Rezervasyon başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["OdaID"] = new SelectList(_context.Odalar, "OdaID", "OdaNumarasi", rezervasyon.OdaID);
            return View(rezervasyon);
        }

        // GET: Check-In Ekranı
        public async Task<IActionResult> CheckIn(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .FirstOrDefaultAsync(r => r.RezervasyonID == id);

            if (rezervasyon == null) return NotFound();

            return View(rezervasyon);
        }

        // POST: Check-In İşlemi (DÜZELTİLMİŞ)
        [HttpPost]
        public async Task<IActionResult> CheckIn(int id, string tcKimlikNo)
        {
            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .FirstOrDefaultAsync(r => r.RezervasyonID == id);

            if (rezervasyon == null) return NotFound();

            if (string.IsNullOrEmpty(tcKimlikNo) || tcKimlikNo.Length != 11 || !long.TryParse(tcKimlikNo, out _))
            {
                ViewBag.Hata = "HATA: TC Kimlik Numarası mutlaka 11 haneli bir sayı olmalıdır!";
                return View(rezervasyon);
            }

            if (rezervasyon.Misafir != null)
            {
                rezervasyon.Misafir.TCKimlik = tcKimlikNo;
                _context.Update(rezervasyon.Misafir);
            }
            
            rezervasyon.Durum = "Giriş Yapıldı";
            _context.Update(rezervasyon);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Rezervasyonlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon == null) return NotFound();
            
            ViewData["MisafirID"] = new SelectList(_context.Misafirler, "MisafirID", "AdSoyad", rezervasyon.MisafirID);
            ViewData["OdaID"] = new SelectList(_context.Odalar, "OdaID", "OdaNumarasi", rezervasyon.OdaID);
            return View(rezervasyon);
        }

        // POST: Rezervasyonlar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezervasyonID,OdaID,MisafirID,GirisTarihi,CikisTarihi,Durum")] Rezervasyon rezervasyon)
        {
            if (id != rezervasyon.RezervasyonID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervasyon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervasyonExists(rezervasyon.RezervasyonID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MisafirID"] = new SelectList(_context.Misafirler, "MisafirID", "AdSoyad", rezervasyon.MisafirID);
            ViewData["OdaID"] = new SelectList(_context.Odalar, "OdaID", "OdaNumarasi", rezervasyon.OdaID);
            return View(rezervasyon);
        }

        // GET: Rezervasyonlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .Include(r => r.Oda)
                .FirstOrDefaultAsync(m => m.RezervasyonID == id);
            
            if (rezervasyon == null) return NotFound();

            return View(rezervasyon);
        }

        // POST: Rezervasyonlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon != null)
            {
                var oda = await _context.Odalar.FindAsync(rezervasyon.OdaID);
                if(oda != null) { oda.DoluMu = false; _context.Update(oda); }
                
                _context.Rezervasyonlar.Remove(rezervasyon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Check-Out İşlemi
        [HttpPost]
        public async Task<IActionResult> CheckOut(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            
            if (rezervasyon != null)
            {
                if (rezervasyon.OdendiMi == false)
                {
                    TempData["Hata"] = "DİKKAT: Ödeme alınmadığı için çıkış yapılamaz! Lütfen önce ödeme alın.";
                    return RedirectToAction(nameof(Konaklayanlar));
                }

                rezervasyon.Durum = "Çıkış Yapıldı";

                var oda = await _context.Odalar.FindAsync(rezervasyon.OdaID);
                if (oda != null)
                {
                    oda.DoluMu = false; 
                    oda.TemizMi = false;
                    _context.Update(oda);
                }

                _context.Update(rezervasyon);
                await _context.SaveChangesAsync();
                
                TempData["Basari"] = "Çıkış işlemi başarıyla yapıldı. Oda temizlik listesine eklendi.";
            }

            return RedirectToAction(nameof(Konaklayanlar));
        }

        // --- ÖDEME ALMA VE HARCAMA İŞLEMLERİ ---

        // GET: Ödeme Ekranı (DÜZELTİLMİŞ)
        public async Task<IActionResult> OdemeAl(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .Include(r => r.Oda)
                .Include(r => r.Harcamalar)
                .FirstOrDefaultAsync(r => r.RezervasyonID == id);

            if (rezervasyon == null) return NotFound();

            var gunSayisi = (rezervasyon.CikisTarihi - rezervasyon.GirisTarihi).Days;
            if (gunSayisi <= 0) gunSayisi = 1;

            decimal odaUcreti = gunSayisi * 1500;
            decimal ekstraHarcamalar = (rezervasyon.Harcamalar != null) ? rezervasyon.Harcamalar.Sum(h => h.Tutar) : 0;
            
            rezervasyon.ToplamTutar = odaUcreti + ekstraHarcamalar;

            return View(rezervasyon);
        }

        // POST: Ödemeyi Kaydet
        [HttpPost]
        public async Task<IActionResult> OdemeAl(int id, decimal toplamTutar, string odemeTuru)
        {
            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon == null) return NotFound();

            rezervasyon.ToplamTutar = toplamTutar;
            rezervasyon.OdemeTuru = odemeTuru;
            rezervasyon.OdendiMi = true;

            _context.Update(rezervasyon);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Konaklayanlar));
        }

        // POST: Harcama Ekle
        [HttpPost]
        public async Task<IActionResult> HarcamaEkle(int id, string urunAdi, decimal fiyat)
        {
            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon != null)
            {
                var harcama = new Harcama
                {
                    RezervasyonID = id,
                    UrunAdi = urunAdi,
                    Tutar = fiyat
                };
                _context.Harcamalar.Add(harcama);

                rezervasyon.ToplamTutar += fiyat; 
                _context.Update(rezervasyon);
                
                await _context.SaveChangesAsync();
                TempData["Basari"] = $"{urunAdi} ({fiyat} TL) hesaba eklendi.";
            }
            return RedirectToAction("OdemeAl", new { id = id });
        }

        // GET: Fatura Yazdır
        public async Task<IActionResult> FaturaYazdir(int id)
        {
            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Misafir)
                .Include(r => r.Oda)
                .Include(r => r.Harcamalar)
                .FirstOrDefaultAsync(m => m.RezervasyonID == id);
                
            return View(rezervasyon);
        }

        private bool RezervasyonExists(int id)
        {
            return _context.Rezervasyonlar.Any(e => e.RezervasyonID == id);
        }
    } // Class Bitişi
} // Namespace Bitişi