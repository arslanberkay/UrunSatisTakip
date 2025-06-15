using Microsoft.AspNetCore.Mvc;
using UrunStok.UI.ViewModel;

namespace UrunStok.UI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminViewModel admin)
        {
            if (admin.KullaniciAdi == "berkay" && admin.Sifre == "8753")
            {
                return RedirectToAction("Index", "Satis");
            }
            TempData["Hata"] = "Kullanıcı Adı veya Şifre Hatalı";
            return View();
        }
    }
}
