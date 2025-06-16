using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrunStok.UI.Context;
using UrunStok.UI.Filters;
using UrunStok.UI.Models;

namespace UrunStok.UI.Controllers
{
    public class UrunController : Controller
    {
        private readonly UrunStokDbContext _db;

        public UrunController(UrunStokDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var urunler = _db.Urunler
                .Include(u => u.Kategori)
                .ToList();
            return View(urunler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var kategoriler = _db.Kategoriler.ToList();
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [UnıqueUrunAdiFilter]
        public IActionResult Create(Urun urun)
        {
            if (urun.KategoriId == null)
            {
                ModelState.AddModelError("KategoriId", "Lütfen bir kategori seçiniz");

                ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "Id", "Ad"); //ViewBag tekrar dolduruluyor, yoksa select list boş gelir.

                return View(urun);
            }

            _db.Urunler.Add(urun);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var urun = _db.Urunler.Find(id);
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "Id", "Ad");
            return View(urun);
        }

        [HttpPost]
        [UnıqueUrunAdiFilter]
        public IActionResult Edit(Urun urun)
        {
            _db.Urunler.Update(urun);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var urun = _db.Urunler.Find(id);
            if (urun!=null)
            {
                _db.Urunler.Remove(urun);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");

        }


    }
}
