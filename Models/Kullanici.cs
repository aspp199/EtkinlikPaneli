using System.ComponentModel.DataAnnotations;

namespace EtkinlikPaneli.Models
{
    public class Kullanici
    {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage ="Ad Soyad Boş Bırakılamaz.")]
            public string AdSoyad { get; set; } = string.Empty;
            [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz.")]
            public string KullaniciAdi {  get; set; } = string.Empty;
            [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
            public string Sifre {  get; set; } = string.Empty;

        public List<Katilim> Katilimlar { get; set; } = new List<Katilim>();
        
    }
}
