using System.ComponentModel.DataAnnotations;

namespace Rezervist.Models
{
    public class Harcama
    {
        [Key]
        public int HarcamaID { get; set; }

        public string? UrunAdi { get; set; } // Örn: Kola, Akşam Yemeği
        public decimal Tutar { get; set; }

        public DateTime IslemTarihi { get; set; } = DateTime.Now;

        // Hangi rezervasyona ait?
        public int RezervasyonID { get; set; }
        public virtual Rezervasyon? Rezervasyon { get; set; }
    }
}