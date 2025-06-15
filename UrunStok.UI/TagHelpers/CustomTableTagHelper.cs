using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Text;

namespace UrunStok.UI.TagHelpers
{
    [HtmlTargetElement("custom-table")] //<custom-table> etiketiyle kullanılabilir
    public class CustomTableTagHelper : TagHelper
    {
        public IEnumerable Items { get; set; } //items="Model" Model listesini buraya bağlıyacağız. Tabloya gönderilen veri listesi . 
        public string ControllerName { get; set; } //Düzenle ve Sil butonlarında kullanılacak controller ismi ÖR: controller-name="Musteri"
        public string Columns { get; set; } //Kolon isimleri ve başlıkları (Id: ID, Ad: Adı, Eposta :E-posta gibi geliyor. Sol taraf prop adı, sağ taraf kolon başlığı) Kullanıcı hangi sütunları göstermek istiyorsa

        public override void Process(TagHelperContext context, TagHelperOutput output)
        { 
            output.TagName = "table"; //<custom-table> etiketi yerine <table> tagı oluşturur
            output.Attributes.SetAttribute("class", "table table-bordered");

            var columnDefs = Columns.Split(',') //Kolonları , ile ayırır
                .Select(c =>
                {     
                    var parts = c.Split(":"); //Her bir kolonu : ile ayırır
                    return new { Property = parts[0].Trim(), Header = parts.Length > 1 ? parts[1].Trim() : parts[0].Trim() };
                    //Property : Model içindeki özellik adı ("Müşteri Adı","Id" vb)
                    //Header : Tabloda görünecek başlık ("Id","Ad")
                }).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("<thead><tr>"); //Tablonun <thead> kısmını oluşturur

            foreach (var col in columnDefs) //Her kolona <th> hücresi koyar
            {
                sb.AppendLine($"<th>{col.Header}</th>"); 
            }

            sb.AppendLine("<th>İşlemler</th>");
            sb.AppendLine("</tr></thead>");


            sb.AppendLine("<tbody>"); //Tablonun <tbody> kısmı

            foreach (var item in Items) //Model listesindeki her bir kayıt için bir satır başlatır
            {
                var type = item.GetType(); //Reflection işlemleri için item nesnesinin tipini aldık
                sb.AppendLine("<tr>");

                foreach(var col in columnDefs) //Kullanıcının belirttiği kolonlara göre hücre oluşturulur. Ör:"Ad,Soyad,Eposta"
                {
                    object value = item; 
                    foreach (var prop in col.Property.Split(".")) //Eğer Musteri.Ad gibi nested prop ise bunu "Musteri" ve "Ad" olarak ikiye böler
                    {
                        if (value==null)
                        {
                            break;
                        }
                        var pi = value.GetType().GetProperty(prop); //Musteri tipinde Ad propertysini bulur pi=Musteri.Ad
                        value = pi?.GetValue(value); // Musteri.Ad değerini döndürür. Ör: Berkay
                    }
                    sb.AppendLine($"<td>{value}</td>"); //Yukarıda elde edilen value artık bir string,int, date vs değerdir. Bunu <td> hücresi içine ekler. ÖR : <td>Berkay</td>
                }

                //item.Id nin değerini bulur
                var idProp = type.GetProperty("Id"); //Modelin tipinden Id adındaki propertyi bulur
                var idVal = idProp?.GetValue(item); //Sonra o Id propertysinin değerini alır ÖR:5

                sb.AppendLine("<td>");
                sb.AppendLine($"<a href = '/{ControllerName}/Edit/{idVal}' class='btn btn-success'>Düzenle</a>");
                sb.AppendLine($"<a href='/{ControllerName}/Delete/{idVal}' class='btn btn-danger' onclik =\"return confirm('Silmek istediğinize emin misiniz?')\">Sil</a>");
                sb.AppendLine("<td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            output.Content.SetHtmlContent(sb.ToString());
        }

    }
}
