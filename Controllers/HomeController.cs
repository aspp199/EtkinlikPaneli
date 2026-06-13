using EtkinlikPaneli.Data;
using EtkinlikPaneli.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EtkinlikPaneli.Controllers
{
    public class HomeController : Controller
    {
        private readonly EtkinlikContext _context;

        public HomeController(EtkinlikContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            ViewBag.ToplamEtkinlik = _context.Etkinlikler.Count();

            if (kullaniciId != null)
            {
                ViewBag.KatildigimEtkinlik = _context.Katilimlar.Count(x => x.KullaniciId == kullaniciId);
            }
            else
            {
                ViewBag.KatildigimEtkinlik = 0;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}