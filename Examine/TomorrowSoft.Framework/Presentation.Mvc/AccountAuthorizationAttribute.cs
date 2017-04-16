using System;
using System.Linq;
using System.Web.Mvc;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AccountAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly IAuthenticationService service;

        public AccountAuthorizationAttribute()
            : this(IoC.Get<IAuthenticationService>())
        {
        }

        private AccountAuthorizationAttribute(IAuthenticationService service)
        {
            this.service = service;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //如果没有通过身份验证
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            //判断会话中是否记录了当前用户的权限
            if (!service.HasAuthorities())
            {
                filterContext.Controller.ViewData[FrameworkKeys.ErrorMessage] = "已超时，请重新登录！";
                filterContext.Controller.ViewData[FrameworkKeys.ErrorStackTrace] = "已超时，请重新<a href=\"/Account/Login\">登录</a>！";
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = filterContext.Controller.ViewData
                };
                return;
            }

            //判断能使用该功能
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            var area = filterContext.RouteData.DataTokens["area"].ToString();
            if(!service.GetAuthorities().Permit(area, controller, action))
            {
                filterContext.Controller.ViewData[FrameworkKeys.ErrorMessage] = "页面不存在！";
                filterContext.Controller.ViewData[FrameworkKeys.ErrorStackTrace] = "页面不存在！";
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = filterContext.Controller.ViewData
                };
            }
        }
    }
}