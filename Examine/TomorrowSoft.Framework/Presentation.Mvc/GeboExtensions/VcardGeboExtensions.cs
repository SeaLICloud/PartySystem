namespace System.Web.Mvc.Html
{
    public static class VcardGeboExtensions
    {
        public static MvcHtmlString VcardItem(this HtmlHelper htmlHelper, string value)
        {
            return VcardItem(htmlHelper, new MvcHtmlString(value));
        }

        public static MvcHtmlString VcardItem(this HtmlHelper htmlHelper, params MvcHtmlString[] controls)
        {
            var li = new TagBuilder("li");
            foreach (var control in controls)
            {
                li.InnerHtml += control.ToHtmlString();
            }
            return MvcHtmlString.Create(li.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString VcardItem(this HtmlHelper htmlHelper, string key, string value)
        {
            return VcardItem(htmlHelper, key, new MvcHtmlString(value));
        }

        public static MvcHtmlString VcardItem(this HtmlHelper htmlHelper, string key, params MvcHtmlString[] controls)
        {
            var li = new TagBuilder("li");
            var span = new TagBuilder("span");
            span.AddCssClass("item-key");
            span.SetInnerText(key);
            var div = new TagBuilder("div");
            div.AddCssClass("vcard-item");
            foreach (var control in controls)
            {
                div.InnerHtml += control.ToHtmlString();
            }
            li.InnerHtml += span.ToString();
            li.InnerHtml += div.ToString();
            return MvcHtmlString.Create(li.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString VcardHeading(this HtmlHelper htmlHelper, string text)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("v-heading");
            li.InnerHtml += new MvcHtmlString(text);
            return MvcHtmlString.Create(li.ToString(TagRenderMode.Normal));
        }
    }
}