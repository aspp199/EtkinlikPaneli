using System.ComponentModel.DataAnnotations;

namespace EtkinlikPaneli.Models
{
    public class Etkinlik
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EtkinlikAdi { get; set; } = string.Empty;
        [Required]
        public string Yer { get; set; } = string.Empty;
        [Required]
        public DateTime Tarih { get; set; }

        public string Aciklama { get; set; } = string.Empty;
        
        public List<Katilim> Katilimlar { get; set; } = new List<Katilim>();
    }
}
