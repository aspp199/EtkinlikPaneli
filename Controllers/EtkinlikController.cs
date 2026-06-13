using EtkinlikPaneli.Data;
using EtkinlikPaneli.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtkinlikPaneli.Controllers
{
    public class EtkinlikController : Controller
    {
        private readonly EtkinlikContext _context;

        public EtkinlikController(EtkinlikContext context)
        {
            _context = context;
        }
        private bool GirisYapildiMi()
        {
            return HttpContext.Session.GetInt32("KullaniciId") != null;
        }
        public IActionResult Index()
        {
            if (!GirisYapildiMi())
            {
                return RedirectToAction("Login", "Account");
            }

            var etkinlikler = _context.Etkinlikler.ToList();
            return View(etkinlikler);
        }
        public IActionResult Create()
        {
            if (!GirisYapildiMi())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Etkinlik etkinlik)
        {
            if (ModelState.IsValid)
            {
                _context.Etkinlikler.Add(etkinlik);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(etkinlik);
        }
        public IActionResult Details(int id)
        {
            if (!GirisYapildiMi())
            {
                return RedirectToAction("Login", "Account");
            }
            var etkinlik = _context.Etkinlikler.FirstOrDefault(x=>x.Id == id);

            if (etkinlik == null)
            {
                return NotFound();
            }

            return View(etkinlik);
        }
        public IActionResult Edit(int id)
        {
            if (!GirisYapildiMi())
            {
                return RedirectToAction("Login", "Account");
            }
            var etkinlik = _context.Etkinlikler.FirstOrDefault(x => x.Id == id);

            if (etkinlik == null)
            {
                return NotFound();
            }

            return View(etkinlik);
        }

        [HttpPost]
        public IActionResult Edit(Etkinlik etkinlik)
        {
            if (ModelState.IsValid)
            {
                _context.Etkinlikler.Update(etkinlik);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(etkinlik);
        }
        public IActionResult Delete(int id)
        {
            if (!GirisYapildiMi())
            {
                return RedirectToAction("Login", "Account");
            }
            var etkinlik = _context.Etkinlikler.FirstOrDefault(x => x.Id == id);

            if (etkinlik == null)
            {
                return NotFound();
            }

            return View(etkinlik);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var etkinlik = _context.Etkinlikler.FirstOrDefault(x => x.Id == id);

            if (etkinlik != null)
            {
                _context.Etkinlikler.Remove(etkinlik);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
