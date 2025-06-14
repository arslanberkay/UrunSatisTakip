namespace UrunStok.UI.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public List<Urun> Urunler { get; set; }
    }
}
