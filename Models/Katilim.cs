using System.ComponentModel.DataAnnotations;
namespace EtkinlikPaneli.Models
{
    public class Katilim
    {
        [Key]
        public int Id { get; set; } 

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public int EtkinlikId { get; set; }
        public Etkinlik etkinlik { get; set; }

        public DateTime KatilimTarihi { get; set; }
    }
}
