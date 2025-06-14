using Microsoft.AspNetCore.Mvc;
using UrunStok.UI.Context;
using UrunStok.UI.Models;

namespace UrunStok.UI.Controllers
{
    public class MusteriController : Controller
    {
        private readonly UrunStokDbContext _db;

        public MusteriController(UrunStokDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var musteriler = _db.Musteriler.ToList();
            return View(musteriler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Musteri musteri)
        {
            _db.Musteriler.Add(musteri);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var musteri = _db.Musteriler.Find(id);
            return View(musteri);
        }

        [HttpPost]
        public IActionResult Edit(Musteri musteri)
        {
            _db.Musteriler.Update(musteri);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var musteri = _db.Musteriler.Find(id);
            if (musteri!=null)
            {
                _db.Musteriler.Remove(musteri);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
