using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace UrunStok.UI.Models
{
    public class Satis
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Adet zorunludur")]
        public int Adet { get; set; }

        [Required(ErrorMessage ="Fiyat zorunludur")]
        public decimal Fiyat { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        [Required(ErrorMessage ="Lütfen bir müşteri seçiniz")]
        public int? MusteriId { get; set; } //int? olmalı ki value="" geldiğinde null olarak algılansın ve [Required] devreye girsin

        [ValidateNever]
        public Musteri Musteri { get; set; }

        [Required(ErrorMessage ="Lütfen bir ürün seçimi yapınız")]
        public int? UrunId { get; set; }

        [ValidateNever]
        public Urun Urun { get; set; }
    }
}
