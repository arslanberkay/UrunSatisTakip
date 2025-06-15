using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UrunStok.UI.TagHelpers
{
    [HtmlTargetElement("custom-login-form")]
    public class LoginTagHelper : TagHelper
    {
        public string HataMesaji { get; set; } //TagHelper içinde yazarken hata-mesaji olarak yazılır

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "form";
            output.Attributes.SetAttribute("class", "custom-login-form");
            output.Attributes.SetAttribute("method", "post");

            var hataSpan = string.IsNullOrWhiteSpace(HataMesaji)
                ? "<span class='text-danger'></span>" : $"<span class='text-danger'>{HataMesaji}</span>";

            output.Content.SetHtmlContent(@$"
<div class='form-group'>
<label> Kullanıcı Adı </label>
<input name = 'KullaniciAdi' required />
</div>

<div class='form-group'>
<label> Şifre </label>
<input name = 'Sifre' type='password'  required />
</div>

<button type='submit'> Giriş Yap </button>
{hataSpan}");
            base.Process(context, output);
        }
    }
}
