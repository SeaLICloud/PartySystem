using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Common.Domain;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.CustomType;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.StatePattern;
using TomorrowSoft.Framework.Presentation.Mvc;
using TomorrowSoft.Framework.Presentation.Mvc.HtmlTags;

namespace System.Web.Mvc.Html
{
    public static class GeboExtensions
    {
        public static AjaxOptions Options(this AjaxHelper ajaxHelper)
        {
            return new AjaxOptions { UpdateTargetId = FrameworkKeys.MainContent };
        }

        public static MvcHtmlString BreadCrumb(this HtmlHelper htmlHelper, params MvcHtmlString[] nodes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div id=\"jCrumbs\" class=\"breadCrumb module\">");
            sb.AppendLine("    <ul>");
            sb.AppendLine("        <li>");
            sb.AppendLine("            <a href=\"#\"><i class=\"icon-home\"></i></a>");
            sb.AppendLine("        </li>");

            foreach (var node in nodes)
            {
                sb.AppendLine("        <li>");
                sb.AppendFormat("            {0}", node).AppendLine();
                sb.AppendLine("        </li>");
            }
            sb.AppendLine("    </ul>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ControlGroup(this HtmlHelper htmlHelper, string label, params MvcHtmlString[] controls)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            if (!string.IsNullOrEmpty(label))
            {
                sb.AppendLine("     <label class=\"col-sm-3 control-label\">");
                sb.AppendFormat("          {0}", label).AppendLine();
                sb.AppendLine("     </label>");
                sb.AppendLine("     <div class=\"col-sm-9\">");
            }
            else
                sb.AppendLine("     <div class=\"col-sm-offset-3 col-sm-9\">");
            foreach (var control in controls)
            {
                sb.AppendFormat("          {0}", control).AppendLine();
            }
            sb.AppendLine("     </div>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString UploadControlGroup(this HtmlHelper htmlHelper, string label, string id)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("     <label class=\"col-sm-3 control-label\">");
            sb.AppendFormat("          {0}", label).AppendLine();
            sb.AppendLine("     </label>");
            sb.AppendFormat("     <div class=\"col-sm-9\" id=\"{0}\" >",id);
            sb.AppendLine("     </div>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ControlGroup(this HtmlHelper htmlHelper, string label, string text)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"form-group\">");
            if (!string.IsNullOrEmpty(label))
            {
                sb.AppendLine("     <label class=\"col-sm-3 control-label\">");
                sb.AppendFormat("          {0}", label).AppendLine();
                sb.AppendLine("     </label>");
            }
            sb.AppendLine("     <div class=\"col-sm-9\">");
            sb.Append("          <p class=\"form-control-static\"><strong>").Append(text).Append("</strong></p>").AppendLine();
            sb.AppendLine("     </div>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
        
        public static MvcHtmlString Strong(this HtmlHelper htmlHelper, string text)
        {
            var strong = new TagBuilder("strong");
            strong.SetInnerText(text);
            return MvcHtmlString.Create(strong.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Replace(this MvcHtmlString mvcHtmlString, string oldString, string newString)
        {
            return MvcHtmlString.Create(mvcHtmlString.ToHtmlString().Replace(oldString, newString));
        }

        public static MvcHtmlString Alert(this HtmlHelper htmlHelper, 
            string strongMessage, string message = "", AlertCategory alertCategory = AlertCategory.Default)
        {
            var tagBuilder = new TagBuilder("div");
            switch (alertCategory)
            {
                case AlertCategory.Error:
                    tagBuilder.MergeAttribute("class", "alert alert-danger");
                    break;
                case AlertCategory.Success:
                    tagBuilder.MergeAttribute("class", "alert alert-success");
                    break;
                case AlertCategory.Info:
                    tagBuilder.MergeAttribute("class", "alert alert-info");
                    break;
                default:
                    tagBuilder.MergeAttribute("class", "alert");
                    break;
            }
            var a = new TagBuilder("a");
            a.MergeAttribute("class", "close");
            a.MergeAttribute("data-dismiss", "alert");
            a.SetInnerText("×");
            var strong = new TagBuilder("strong");
            strong.SetInnerText(strongMessage);
            tagBuilder.InnerHtml = string.Format("{0}{1}{2}",
                                                 a.ToString(TagRenderMode.Normal),
                                                 strong.ToString(TagRenderMode.Normal),
                                                 message);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Modal(this HtmlHelper htmlHelper,
            string id, string headerText, string footerButtonText = "", params MvcHtmlString[] bodyControls)
        {
            var sb = new StringBuilder();
            foreach (var control in bodyControls)
            {
                sb.AppendLine(control.ToString());
            }
            var tag = string.Format(@"
                <div id=""{0}"" class=""modal"">
                    <div class=""modal-dialog"">
					    <div class=""modal-content"">
                            <div class=""modal-header"">
                                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
                                <h3 class=""modal-title"">{1}</h3>
                            </div>
                            <div class=""modal-body"" style=""height:280px;"">
                                {2}
                            </div>
                            <div class=""modal-footer"">
                                <button type=""submit"" class=""btn btn-primary"" onclick=""$('#{0}').modal('hide')"">{3}</button>
                                <a data-dismiss=""modal"" class=""btn btn-default"" href=""#"">关闭</a>
                            </div>
                        </div>
                    </div>
                </div>", id, headerText, sb.ToString(), footerButtonText);
            return MvcHtmlString.Create(tag);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, 
            string name, Type enumType, string optionLabel)
        {
            return htmlHelper.DropDownList(name, enumType.ToSelectListItems(), optionLabel);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper,
            string name, Type enumType, string selectedValue, string optionLabel)
        {
            return htmlHelper.DropDownList(name, enumType.ToSelectListItems(selectedValue), optionLabel);
        }

        //public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IBusinessIdentifier selectedValue, string optionLabel)
        //{
        //    return htmlHelper.DropDownList(name, selectedValue, optionLabel, null /* htmlAttributes */);
        //}

        //public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IBusinessIdentifier selectedValue, string optionLabel, object htmlAttribute)
        //{
        //    var selectList = htmlHelper.ViewData.Eval(name) as IEnumerable<SelectListItem>;
        //    //selectList.FirstOrDefault(x => x.Value == selectedValue.ToString()).Selected = true;
        //    return htmlHelper.DropDownList(name, selectList, optionLabel);
        //}

        public static MvcHtmlString Visible<T>(this MvcHtmlString html, T entity, params Type[] stateTypes) 
            where T : Entity<T>
        {
            var type = StateFactory.GetCurrentState(entity).GetType();
            if (stateTypes.Any(x => x == type || type.IsSubclassOf(x)))
                return html;
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Visible(this MvcHtmlString html, bool isVisible)
        {
            return isVisible ? html : MvcHtmlString.Empty;
        }

        public static bool IsPermit(this HtmlHelper html, string area, string controller, string action)
        {
            var service = IoC.Get<IAuthenticationService>();
            return service.GetAuthorities().Permit(area, controller, action);
        }
        
        public static MvcHtmlString FormValidate(this HtmlHelper htmlHelper, string formName, params Rule[] rules)
        {
            var format = @"
<script type=""text/javascript"">
    $(document).ready(function(){{  
{3}     
        $(""#{0}"").validate({{
            onkeyup: false,
			errorClass: 'error',
			validClass: 'valid',
			highlight: function(element) {{
				$(element).closest('div').addClass(""f_error"");
			}},
			unhighlight: function(element) {{
				$(element).closest('div').removeClass(""f_error"");
			}},
            errorPlacement: function(error, element) {{
                $(element).closest('div').append(error);
            }},  
            rules:{{
                {1}
            }},
            messages:{{
                {2}
            }}
        }}); 
    }});
</script>";
            var sbRules = new StringBuilder();
            for (var i = 0; i < rules.Length; i++)
            {
                sbRules.Append(rules[i].ToRules());
                if (i < rules.Length - 1)
                    sbRules.Append(",").AppendLine().Append("                ");
            }
            var sbMessages = new StringBuilder();
            for (var i = 0; i < rules.Length; i++)
            {
                sbMessages.Append(rules[i].ToMessages());
                if (i < rules.Length - 1)
                    sbMessages.Append(",").AppendLine().Append("                ");
            }

            //为必填的控件加上*号
            var sbRequires = new StringBuilder();
            foreach (var rule in rules)
            {
                foreach (var r in rule.Rules)
                {
                    if (r.Key == "required" && r.Value == "true")
                    {
                        sbRequires.Append("        ");
                        sbRequires.AppendFormat("$(\"[name='{0}']\").closest('div').prev('label.control-label').append('<span class=\"f_req\">*</span>');", rule.TagName).AppendLine();
                        break;
                    }
                }
            }
            var sb = new StringBuilder();
            sb.AppendFormat(format, formName, sbRules, sbMessages, sbRequires);
            return MvcHtmlString.Create(sb.ToString());
        }
        
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src)
        {
            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", "");
            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
        
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, int width)
        {
            var img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", "");
            img.MergeAttribute("width", string.Format("{0}px", width));
            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString GlyphiconIcon(this HtmlHelper htmlHepler, string iconName)
        {
            var i = new TagBuilder("i");
            i.MergeAttribute("class", string.Format("glyphicon glyphicon-{0}", iconName));
            return MvcHtmlString.Create(i.ToString(TagRenderMode.SelfClosing));
        }
        
        public static MvcHtmlString Icon(this HtmlHelper htmlHepler, string iconName)
        {
            var i = new TagBuilder("i");
            i.MergeAttribute("class", iconName);
            return MvcHtmlString.Create(i.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Tag(this HtmlHelper htmlHelper, string tag, string text)
        {
            var tagBuilder = new TagBuilder(tag);
            tagBuilder.SetInnerText(text);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
        
        public static MvcHtmlString Tag(this HtmlHelper htmlHelper, string tag, string text, string style)
        {
            var tagBuilder = new TagBuilder(tag);
            tagBuilder.MergeAttribute("class", style);
            tagBuilder.SetInnerText(text);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Tag(this HtmlHelper htmlHelper, string tag, string style, params MvcHtmlString[] controls)
        {
            var tagBuilder = new TagBuilder(tag);
            tagBuilder.MergeAttribute("class", style);
            foreach (var control in controls)
            {
                tagBuilder.InnerHtml += control;
            }
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}