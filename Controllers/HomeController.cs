using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rezervist.Data; // Veritabanı erişimi için
using Rezervist.Models;

namespace Rezervist.Controllers;

[Authorize] // Giriş yapmadan kimse bu verileri göremez!
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // 1. TOPLAM KASA (Sadece 'OdendiMi' true olanları topla)
        decimal toplamKazanc = _context.Rezervasyonlar
            .Where(r => r.OdendiMi == true)
            .Sum(r => r.ToplamTutar);

        // 2. DOLU ODA SAYISI
        int doluOdaSayisi = _context.Odalar.Count(o => o.DoluMu == true);

        // 3. TOPLAM ODA SAYISI
        int toplamOdaSayisi = _context.Odalar.Count();

        // 4. MİSAFİR SAYISI (Şu an aktif konaklayanlar)
        int aktifMisafir = _context.Rezervasyonlar.Count(r => r.Durum == "Giriş Yapıldı");

        // 5. BUGÜN GİRİŞ YAPACAKLAR
        int bugunGelecekler = _context.Rezervasyonlar
            .Count(r => r.GirisTarihi.Date == DateTime.Today.Date && r.Durum == "Bekliyor");

        // Verileri View'a (Sayfaya) Taşı
        ViewBag.Kasa = toplamKazanc;
        ViewBag.DoluOda = doluOdaSayisi;
        ViewBag.ToplamOda = toplamOdaSayisi;
        ViewBag.AktifMisafir = aktifMisafir;
        ViewBag.BugunGelecek = bugunGelecekler;

        // Doluluk Oranı Hesapla (Sıfıra bölme hatası olmasın diye kontrol et)
        ViewBag.DolulukOrani = toplamOdaSayisi > 0 ? (doluOdaSayisi * 100 / toplamOdaSayisi) : 0;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}