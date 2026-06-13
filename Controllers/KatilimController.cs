using EtkinlikPaneli.Data;
using EtkinlikPaneli.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtkinlikPaneli.Controllers
{
    public class KatilimController : Controller
    {
        private readonly EtkinlikContext _context;

        public KatilimController(EtkinlikContext context)
        {
            _context = context;
        }

        public IActionResult Katil(int etkinlikId)
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var dahaOnceKatildiMi = _context.Katilimlar.Any(x =>
                x.KullaniciId == kullaniciId && x.EtkinlikId == etkinlikId);

            if (!dahaOnceKatildiMi)
            {
                Katilim katilim = new Katilim
                {
                    KullaniciId = kullaniciId.Value,
                    EtkinlikId = etkinlikId,
                    KatilimTarihi = DateTime.Now
                };

                _context.Katilimlar.Add(katilim);
                _context.SaveChanges();

                TempData["Mesaj"] = "Etkinliğe katılımınız kaydedildi.";
            }
            else
            {
                TempData["Mesaj"] = "Bu etkinliğe zaten katıldınız.";
            }

            return RedirectToAction("Index", "Etkinlik");
        }
        public IActionResult KatildigimEtkinlikler()
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var katilimlar = _context.Katilimlar
                .Include(x => x.etkinlik)
                .Where(x => x.KullaniciId == kullaniciId)
                .ToList();

            return View(katilimlar);
        }
        public IActionResult Iptal(int etkinlikId)
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var katilim = _context.Katilimlar.FirstOrDefault(x =>
                x.KullaniciId == kullaniciId && x.EtkinlikId == etkinlikId);

            if (katilim != null)
            {
                _context.Katilimlar.Remove(katilim);
                _context.SaveChanges();

                TempData["Mesaj"] = "Katılımınız iptal edildi.";
            }

            return RedirectToAction("KatildigimEtkinlikler");
        }
    }
}