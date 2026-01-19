using Microsoft.EntityFrameworkCore;
using Rezervist.Models;

namespace Rezervist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Oda> Odalar { get; set; }
        public DbSet<Misafir> Misafirler { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Harcama> Harcamalar { get; set; }
    }
}