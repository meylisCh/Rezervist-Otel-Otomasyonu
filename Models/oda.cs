using System.ComponentModel.DataAnnotations;
namespace Rezervist.Models
{
    public class Oda
    {
        [Key]
        public int OdaID { get; set; }
        public string? OdaNumarasi { get; set; } // Örn: 101
        public string? OdaTipi { get; set; }     // Örn: Tek Kişilik
        public decimal Fiyat { get; set; }
        public bool DoluMu { get; set; }
        public bool TemizMi { get; set; } = true; // Varsayılan olarak temiz başlar
        public string? ResimUrl { get; set; } // Örn: /img/oda1.jpg
    }
}