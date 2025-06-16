using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrunStok.UI.Context;
using UrunStok.UI.Models;

namespace UrunStok.UI.Filters
{
    public class UnıqueUrunAdiFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dbContext = (UrunStokDbContext)context.HttpContext.RequestServices.GetService(typeof(UrunStokDbContext));

            var eklenecekUrun = context.ActionArguments["urun"] as Urun; //Action metoduna gelen parametrelerden "urun" adındaki nesneyi alır

            if (eklenecekUrun != null)
            {
                bool aynıAddaUrunVarMi = dbContext.Urunler.Any(u => u.Ad.ToLower() == eklenecekUrun.Ad.ToLower() && u.Id != eklenecekUrun.Id); //Aynı adda başka ürün var mı (Kendisini hariç tutuyoruz)

                if (aynıAddaUrunVarMi)
                {
                    var controller = context.Controller as Controller;

                    controller.ModelState.AddModelError("Ad", "Bu ürün zaten mevcut"); //Ad alanına özel bir hata ekleniyor

                    controller.ViewBag.Kategoriler = new SelectList(dbContext.Kategoriler.ToList(),"Id","Ad"); //Kategoriler yeniden yükleniyor

                    context.Result = new ViewResult
                    {
                        ViewName = context.RouteData.Values["action"]?.ToString(), //Çağırılan acion'a (Create,Edit) geri dön
                        ViewData = controller.ViewData
                    };
                }
            }
        }
    }
}
