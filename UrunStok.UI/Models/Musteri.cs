namespace UrunStok.UI.Models
{
    public class Musteri
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }

        public List<Satis> Satislar { get; set; }
    }
}
