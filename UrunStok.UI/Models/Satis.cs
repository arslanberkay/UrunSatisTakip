namespace UrunStok.UI.Models
{
    public class Satis
    {
        public int Id { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime Tarih { get; set; }

        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; }

        public int UrunId { get; set; }
        public Urun Urun { get; set; }
    }
}
