using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Data.SessionFactories;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;
using TomorrowSoft.Framework.Presentation.Mvc;

namespace TomorrowSoft.Examine.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new {controller = "Default", action = "Index", id = UrlParameter.Optional}, // 参数默认值
                new[] {"TomorrowSoft.Examine.Web.Controllers"}
                );

        }

        protected void Application_Start()
        {
            Database.Prepare();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
        }

        protected void Application_BeginRequest(object sender, EventArgs s)
        {
            if (Request.CurrentExecutionFilePathExtension == "" || Request.CurrentExecutionFilePathExtension == ".aspx")
            {
                IoC.Get<IUnitOfWork>().BindContext();
            }
        }

        protected void Application_EndRequest(object sender, EventArgs s)
        {
            if (Request.CurrentExecutionFilePathExtension == "" || Request.CurrentExecutionFilePathExtension == ".aspx")
            {
                IoC.Get<IUnitOfWork>().UnBindContext();
            }
        }
    }
}