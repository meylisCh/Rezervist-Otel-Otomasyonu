using System.ComponentModel.DataAnnotations;

namespace Rezervist.Models
{
    public class Misafir
    {
        [Key]
        public int MisafirID { get; set; }
        public string? AdSoyad { get; set; }
        public string? Telefon { get; set; }
        
        // Soru işareti (?) bu alanın başta boş olabileceğini söyler
        public string? TCKimlik { get; set; } 
        public bool KaraListedeMi { get; set; } = false; // Yasaklı mı?
public string? OzelNotlar { get; set; } // Örn: "Deniz manzarası sever"
    }
}