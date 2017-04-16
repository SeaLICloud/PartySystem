using System.Collections.Generic;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;
using TomorrowSoft.Framework.Presentation.Mvc;

namespace System.Web.Mvc.Html
{
    public static class ButtonGeboExtensions
    {
        public static MvcHtmlString Button(this HtmlHelper htmlHelper,
            string title, string url, string icon = ButtonIcon.Null, 
            ButtonOption option = ButtonOption.Default, ButtonSize size = ButtonSize.Small)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", url);
            a.MergeAttribute("data-pjax", "");
            a.AddCssClass("btn");
            a.AddCssClass(option.Text());
            a.AddCssClass(size.Text());

            if (icon != ButtonIcon.Null)
            {
                var i = new TagBuilder("i");
                i.MergeAttribute("class", string.Format("{0}", icon));
                a.InnerHtml += i;
                a.InnerHtml += " ";
            }

            a.InnerHtml += title;
            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }
        
        public static MvcHtmlString ConfirmButton(this HtmlHelper htmlHelper,
            string title, string url, string confirmMessage, string icon,
            ButtonOption option = ButtonOption.Default, ButtonSize size = ButtonSize.Small)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", url);
            a.AddCssClass("btn");
            a.AddCssClass(option.Text());
            a.AddCssClass(size.Text());
            a.MergeAttribute("data-ajax", "true");
            a.MergeAttribute("data-ajax-confirm", confirmMessage);
            a.MergeAttribute("data-ajax-method", "POST");
            a.MergeAttribute("data-ajax-mode", "replace");
            a.MergeAttribute("data-ajax-update", "#" + FrameworkKeys.MainContent);

            if (icon != ButtonIcon.Null)
            {
                var i = new TagBuilder("i");
                i.MergeAttribute("class", string.Format("{0}", icon));
                a.InnerHtml += i;
                a.InnerHtml += " ";
            }

            a.InnerHtml += title;
            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Submit(this HtmlHelper htmlHelper,
            string title, 
            string icon = ButtonIcon.Save, 
            ButtonOption option = ButtonOption.Primary, 
            ButtonSize size = ButtonSize.Small)
        {
            var button = new TagBuilder("button");
            button.AddCssClass("btn");
            button.AddCssClass(option.Text());
            button.AddCssClass(size.Text());
            button.MergeAttribute("type", "submit");

            if (icon != ButtonIcon.Null)
            {
                var i = new TagBuilder("i");
                i.MergeAttribute("class", string.Format("{0}", icon));
                button.InnerHtml += i;
                button.InnerHtml += " ";
            }

            button.InnerHtml += title;
            return MvcHtmlString.Create(button.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Submit(this HtmlHelper htmlHelper,
            string title,
            object htmlAttributes,
            string icon = ButtonIcon.Save,
            ButtonOption option = ButtonOption.Primary,
            ButtonSize size = ButtonSize.Small)
        {
            var button = new TagBuilder("button");
            button.AddCssClass("btn");
            button.AddCssClass(option.Text());
            button.AddCssClass(size.Text());
            button.MergeAttribute("type", "submit");

            var attributes = ((IDictionary<string, object>) HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            button.MergeAttributes(attributes);

            if (icon != ButtonIcon.Null)
            {
                var i = new TagBuilder("i");
                i.MergeAttribute("class", string.Format("{0}", icon));
                button.InnerHtml += i;
                button.InnerHtml += " ";
            }

            button.InnerHtml += title;
            return MvcHtmlString.Create(button.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString BackButton(this HtmlHelper htmlHelper, 
            string icon = ButtonIcon.Null,
            ButtonOption option = ButtonOption.Default, 
            ButtonSize size = ButtonSize.Small)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.MergeAttribute("data-pjax", "");
            a.AddCssClass("btn");
            a.AddCssClass(option.Text());
            a.AddCssClass(size.Text());
            a.MergeAttribute("onclick", "history.go(-1)");

            if (icon != ButtonIcon.Null)
            {
                var i = new TagBuilder("i");
                i.MergeAttribute("class", string.Format("{0}", icon));
                a.InnerHtml += i;
                a.InnerHtml += " ";
            }

            a.InnerHtml += "返回";
            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }
    }

    public enum ButtonSize
    {
        [EnumText("btn-lg")] Large,
        [EnumText("")] Default,
        [EnumText("btn-sm")] Small,
        [EnumText("btn-xs")] ExtraSmall
    }

    public enum ButtonOption
    {
        [EnumText("btn-default")] Default,
        [EnumText("btn-primary")] Primary,
        [EnumText("btn-success")] Success,
        [EnumText("btn-info")] Info,
        [EnumText("btn-warning")] Warning,
        [EnumText("btn-danger")] Danger,
        [EnumText("btn-link")] Link
    }

    public static class ButtonIcon
    {
        public const string Null = "";
        public const string Back = "fa fa-reply";
        public const string Create = "fa fa-plus";
        public const string Remove = "fa fa-trash";
        public const string Edit = "fa fa-pencil";
        public const string Save = "fa fa-save";
        public const string Confirm = "fa fa-check";
        public const string Pass = "fa fa-check-circle";
        public const string Fail = "fa fa-times-circle";
        public const string Users = "fa fa-users";
    }
}