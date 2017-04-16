using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.Html
{
    public static class ListGeboExtensions
    {
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IDictionary<string, string> dictionary)
        {
            var sb = new StringBuilder();
            foreach (var item in dictionary)
            {
                var label = new TagBuilder("label");
                label.AddCssClass("radio-inline");
                label.InnerHtml += htmlHelper.RadioButton(name, item.Key);
                label.InnerHtml += item.Value;
                sb.Append(label);
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IDictionary<string, string> dictionary)
        {
            return CheckBoxList(htmlHelper, name, dictionary, "");
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, 
            string name, IDictionary<string, string> dictionary, string selectedKeys)
        {
            var keys = selectedKeys.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            foreach (var item in dictionary)
            {
                var checkbox = new TagBuilder("input");
                checkbox.MergeAttribute("type", "checkbox");
                checkbox.MergeAttribute("name", name);
                checkbox.MergeAttribute("value", item.Key);
                if(keys.Any(x=>x == item.Key))
                {
                    checkbox.MergeAttribute("checked", "checked");
                }

                var label = new TagBuilder("label");
                label.InnerHtml += checkbox;
                label.InnerHtml += item.Value;

                var div = new TagBuilder("div");
                div.AddCssClass("checkbox");
                div.InnerHtml += label;

                sb.Append(div);
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}