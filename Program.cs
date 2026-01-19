using Microsoft.EntityFrameworkCore;
using Rezervist.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // <-- Bu kütüphane şart

// 1. PostgreSQL Tarih Hatasını Çözen Kod (En tepede kalsın)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// 2. Servisleri Ekleme (Services Container)
builder.Services.AddControllersWithViews();

// Veritabanı Bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- KİMLİK DOĞRULAMA (AUTHENTICATION) AYARLARI ---
// DİKKAT: Bu kod bloğu mutlaka 'var app = builder.Build();' satırından ÖNCE olmalı!
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriş yapılmamışsa buraya yönlendir
        options.AccessDeniedPath = "/Account/Login"; // Yetkisiz girişte buraya at
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Oturum süresi
    });
// ---------------------------------------------------

// 3. Uygulamayı İnşa Et (Build)
var app = builder.Build(); // <--- KRİTİK NOKTA: Buradan sonra builder.Services kullanılamaz!

// 4. HTTP İstek Hattı (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- KİMLİK VE YETKİ KONTROLÜ ---
// Sıralama Önemli: Önce Kimlik (Authentication), Sonra Yetki (Authorization)
app.UseAuthentication(); 
app.UseAuthorization();
// --------------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();