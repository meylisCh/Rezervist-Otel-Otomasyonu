using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Rezervist.Data;
using Rezervist.Models;

namespace Rezervist.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Giriş Sayfası
        public IActionResult Login()
        {
            return View();
        }

        // POST: Giriş Yap
        [HttpPost]
        public async Task<IActionResult> Login(string kadi, string sifre)
        {
            var user = _context.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == kadi && x.Sifre == sifre);

            if (user != null)
            {
                // Kullanıcı bulundu, kimlik kartı oluşturuluyor
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.KullaniciAdi ?? string.Empty)
                };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

                // Siteye giriş yap (Cookie bırak)
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home"); // Anasayfaya gönder
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // GET: Kayıt Ol Sayfası
        public IActionResult Register()
        {
            return View();
        }

        // POST: Kayıt Ol
        [HttpPost]
        public async Task<IActionResult> Register(Kullanici p)
        {
            _context.Kullanicilar.Add(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        // Çıkış Yap
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}