using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var service = IoC.Get<ISecurityService>();
                service.CreateRequestLog(filterContext.HttpContext.Session.SessionID)
                       .Url(filterContext.HttpContext.Request.Url.ToString())
                       .Time(DateTime.Now)
                       .HttpMethod(filterContext.HttpContext.Request.HttpMethod)
                       .UserIp(filterContext.HttpContext.Request.UserHostAddress)
                       .UserName(filterContext.HttpContext.User.Identity.Name)
                       .ActionParameters(ToJson(filterContext.ActionParameters));
            }
        }

        private string ToJson(IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            foreach (var parameter in parameters)
            {
                if (sb[sb.Length - 1] != '{')
                    sb.Append(",");
                if (parameter.Value is FormCollection)
                {
                    var collection = (parameter.Value as FormCollection);
                    sb.AppendFormat("\"{0}\":[", parameter.Key);
                    foreach (var key in collection.AllKeys)
                    {
                        if (key != FrameworkKeys.Password)
                        {
                            if (sb[sb.Length - 1] != '[')
                                sb.Append(",");
                            sb.AppendFormat("\"{0}\":\"{1}\"", key, collection[key]);
                        }
                    }
                    sb.Append("]");
                }
                else
                    sb.AppendFormat("\"{0}\":\"{1}\"", parameter.Key, parameter.Value);

            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}