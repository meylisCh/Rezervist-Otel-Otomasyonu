using System.ComponentModel.DataAnnotations;

namespace Rezervist.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciID { get; set; }

        public string? KullaniciAdi { get; set; }

        public string? Sifre { get; set; }
    }
}