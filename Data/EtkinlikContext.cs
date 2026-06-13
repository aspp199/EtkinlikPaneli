using EtkinlikPaneli.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
namespace EtkinlikPaneli.Data
{
    public class EtkinlikContext :DbContext
    {
        public EtkinlikContext(DbContextOptions<EtkinlikContext> options)  : base(options)
        {
        } 
        public DbSet<Kullanici> kullanicilar { get; set; }
        public DbSet<Etkinlik> Etkinlikler { get; set; }
        public DbSet<Katilim> Katilimlar { get; set; }
    }
}
