using System.ComponentModel.DataAnnotations;

namespace Rezervist.Models
{
    public class Rezervasyon
    {
        [Key]
        public int RezervasyonID { get; set; }

        public int OdaID { get; set; }
        public virtual Oda? Oda { get; set; }

        public int MisafirID { get; set; }
        public virtual Misafir? Misafir { get; set; }

        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }

        public string Durum { get; set; } = "Bekliyor"; // Bekliyor, Giriş Yapıldı, Çıkış Yapıldı, İptal

        // --- PARA İŞLEMLERİ ---
        public decimal ToplamTutar { get; set; }
        public string? OdemeTuru { get; set; }
        public bool OdendiMi { get; set; } = false;

        // --- EKSİK OLAN KISIM BURASIYDI, BUNU EKLEYİN: ---
public virtual ICollection<Harcama>? Harcamalar { get; set; } = new List<Harcama>();    }
}