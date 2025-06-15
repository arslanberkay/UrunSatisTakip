using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections;

namespace UrunStok.UI.TagHelpers
{
    [HtmlTargetElement("custom-dropdown")]
    public class CustomDropdownTagHelper : TagHelper
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public IEnumerable Items { get; set; }
        public string IdField { get; set; } = "Value";
        public string TextField { get; set; } = "Text";

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; } //Validation için model ifadesi

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-group");

            var labelTag = $"<label> {Label} </label>";
            var selectTag = $"<select name =\"{Name}\" class =\"form-control\" required> <option value=\"\">Seçiniz</option>";

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    var idProp = item.GetType().GetProperty(IdField);
                    var textProp = item.GetType().GetProperty(TextField);

                    if (idProp != null && textProp != null)
                    {
                        var value = idProp.GetValue(item)?.ToString();
                        var text = textProp.GetValue(item)?.ToString();

                        selectTag += $"<option value =\"{value}\">{text}</option>";
                    }
                }
            }

            selectTag += "</select>";

            var validationSpan = $"<span asp-validation-for =\"{For?.Name}\" class=\"text-danger\"></span>";

            output.Content.SetHtmlContent(labelTag + selectTag + validationSpan);
        }
    }
}
