using System;
using System.Linq;
using System.Web.Mvc;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RoleAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly IAuthenticationService service;
        private string[] roleNames;

        public RoleAuthorizationAttribute(params object[] role)
            : this(IoC.Get<IAuthenticationService>(), role)
        {
        }

        private RoleAuthorizationAttribute(IAuthenticationService service, params object[] role)
        {
            roleNames = role.Select(x => x.ToString()).ToArray();
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

            //判断会话中是否角色记录
            if (!service.HasRole())
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

            //判断角色是否有权限
            if (!service.GetRole().Any(x=>roleNames.Contains(x.Id.RoleName)))
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