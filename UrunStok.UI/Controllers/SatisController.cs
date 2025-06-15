using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrunStok.UI.Context;
using UrunStok.UI.Models;

namespace UrunStok.UI.Controllers
{
    public class SatisController : Controller
    {
        private readonly UrunStokDbContext _db;

        public SatisController(UrunStokDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var satislar = _db.Satislar
                  .Include(s => s.Musteri)
                  .Include(s => s.Urun)
                  .ToList();

            return View(satislar);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //Combobox'ta gözüken müşteri bilgisinin Ad Soyad olarak olmasını istediğim için SelectListItem kullandım
            ViewBag.Musteriler = _db.Musteriler
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Ad + " " + m.Soyad
                })
                .ToList();

            ViewBag.Urunler = new SelectList(_db.Urunler, "Id", "Ad");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Satis satis)
        {
           


            if (!ModelState.IsValid)
            {
                
                ViewBag.Musteriler = _db.Musteriler
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Ad + " " + m.Soyad
                })
                .ToList();

                ViewBag.Urunler = new SelectList(_db.Urunler, "Id", "Ad");
                return View(satis);
            }

            _db.Satislar.Add(satis);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
