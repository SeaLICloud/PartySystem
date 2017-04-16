using System.Web.Mvc.Ajax;

namespace System.Web.Mvc.Ajax
{
    public static class AjaxOptionsExtensions
    {

        public static AjaxOptions UpdateTargetId(this AjaxOptions ajaxOptions, string updateTargetId)
        {
            ajaxOptions.UpdateTargetId = updateTargetId;
            return ajaxOptions;
        }

        public static AjaxOptions Post(this AjaxOptions ajaxOptions)
        {
            ajaxOptions.HttpMethod = "POST";
            return ajaxOptions;
        }

        public static AjaxOptions Confirm(this AjaxOptions ajaxOptions, string message)
        {
            ajaxOptions.Confirm = message;
            return ajaxOptions;
        }

        public static AjaxOptions Target(this AjaxOptions ajaxOptions, string targetId)
        {
            ajaxOptions.UpdateTargetId = targetId;
            return ajaxOptions;
        }

        public static AjaxOptions Success(this AjaxOptions ajaxOptions, string success)
        {
            ajaxOptions.OnSuccess = success;
            return ajaxOptions;
        }
        public static AjaxOptions Get(this AjaxOptions ajaxOptions)
        {
            ajaxOptions.HttpMethod = "Get";
            return ajaxOptions;
        }

        public static AjaxOptions Begin(this AjaxOptions ajaxOptions, string begin)
        {
            ajaxOptions.OnBegin = begin;
            return ajaxOptions;
        }
    }
}