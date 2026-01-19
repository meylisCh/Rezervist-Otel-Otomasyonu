using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rezervist.Data;

namespace Rezervist.Controllers
{
    [Authorize]
    public class OnBuroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OnBuroController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Sadece 'Giriş Yapıldı' (İçeridekiler) ve 'Bekliyor' (Gelecekler) durumundakileri getir.
            // Çıkış yapanlarla işimiz bitti.
            var aktifKayitlar = await _context.Rezervasyonlar
                .Include(r => r.Oda)
                .Include(r => r.Misafir)
                .Where(r => r.Durum == "Giriş Yapıldı" || r.Durum == "Bekliyor")
                .OrderBy(r => r.Oda.OdaNumarasi) // Oda numarasına göre sırala
                .ToListAsync();

            return View(aktifKayitlar);
        }
    }
}