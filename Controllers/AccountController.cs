using EtkinlikPaneli.Data;
using EtkinlikPaneli.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EtkinlikPaneli.Controllers
{
    public class AccountController : Controller
    {
        private readonly EtkinlikContext _context;

        public AccountController(EtkinlikContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                _context.kullanicilar.Add(kullanici);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(kullanici);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Kullanici giris)
        {
            var kullanici = _context.kullanicilar.FirstOrDefault(x =>
                x.KullaniciAdi == giris.KullaniciAdi && x.Sifre == giris.Sifre);

            if (kullanici != null)
            {
                HttpContext.Session.SetInt32("KullaniciId", kullanici.Id);
                HttpContext.Session.SetString("KullaniciAdi", kullanici.KullaniciAdi);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Profil()
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
            {
                return RedirectToAction("Login");
            }

            var kullanici = _context.kullanicilar.FirstOrDefault(x => x.Id == kullaniciId);

            if (kullanici == null)
            {
                return RedirectToAction("Login");
            }

            return View(kullanici);
        }
    }
}
