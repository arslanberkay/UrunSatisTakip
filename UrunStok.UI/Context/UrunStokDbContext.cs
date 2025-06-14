using Microsoft.EntityFrameworkCore;
using UrunStok.UI.Models;

namespace UrunStok.UI.Context
{
    public class UrunStokDbContext : DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Satis> Satislar { get; set; }

        public UrunStokDbContext(DbContextOptions<UrunStokDbContext> options) :base(options)
        {
                
        }
    }
}
