using Microsoft.AspNetCore.Mvc;
using UrunStok.UI.Context;
using UrunStok.UI.Models;

namespace UrunStok.UI.Controllers
{
    public class KategoriController : Controller
    {
        private readonly UrunStokDbContext _db;

        public KategoriController(UrunStokDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var kategoriler = _db.Kategoriler.ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Kategori kategori)
        {
            _db.Kategoriler.Add(kategori);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var kategori = _db.Kategoriler.Find(id);
            return View(kategori);
        }

        [HttpPost]
        public IActionResult Edit(Kategori kategori)
        {
            _db.Kategoriler.Update(kategori);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var kategori = _db.Kategoriler.Find(id);
            if (kategori != null)
            {
                _db.Kategoriler.Remove(kategori);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}
