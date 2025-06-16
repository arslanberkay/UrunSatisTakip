using Microsoft.EntityFrameworkCore;
using System.Text;
using UrunStok.UI.Context;
using UrunStok.UI.Models;

namespace UrunStok.UI.Middlewares
{
    public class UrunSatisLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFolder;
        private readonly string _logFilePath;

        public UrunSatisLogMiddleware(RequestDelegate next)
        {
            _next = next;
            var rootPath = Directory.GetCurrentDirectory();
            _logFolder = Path.Combine(rootPath, "wwwroot", "logs");
            _logFilePath = Path.Combine(_logFolder, "urun_satis_log.txt");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var dbContext = context.RequestServices.GetService(typeof(UrunStokDbContext)) as UrunStokDbContext;

            if (context.Items.ContainsKey("yeniSatis") && context.Items["yeniSatis"] is Satis satis)
            {
                if (!Directory.Exists(_logFolder))
                {
                    Directory.CreateDirectory(_logFolder);
                }

                var detayliSatis = dbContext.Satislar
                    .Include(s => s.Urun)
                    .Include(s => s.Musteri)
                    .FirstOrDefault(s => s.Id == satis.Id);

                var musteriId = detayliSatis.MusteriId;
                var musteriAdi = detayliSatis.Musteri.Ad;
                var urunId = detayliSatis.UrunId;
                var urunAdi = detayliSatis.Urun.Ad;
                var adet = detayliSatis.Adet;
                var fiyat = detayliSatis.Fiyat;
                var tarih = detayliSatis.Tarih.ToString("yyyy.MM.dd HH.mm.ss");

                var logText = $"[{tarih}] Tarihli Satış Bilgileri \n Müşteri ID : {musteriId} - Müşteri Adı : {musteriAdi} \n Ürün ID : {urunId} - Ürün Adı : {urunAdi} \n Adet : {adet} - Fiyat : {fiyat} \n\n";

                await File.AppendAllTextAsync(_logFilePath, logText, Encoding.UTF8);

            }
        }
    }
}
