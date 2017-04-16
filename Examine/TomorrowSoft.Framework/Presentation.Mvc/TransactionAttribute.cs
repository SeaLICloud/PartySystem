using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Keys;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            filterContext.Result = new JsonResult() {Data = new {exception.Message},};
            filterContext.Controller.ViewData[FrameworkKeys.ErrorMessage] = exception.Message;
            filterContext.Controller.ViewData[FrameworkKeys.ErrorStackTrace] = exception.StackTrace;
            filterContext.ExceptionHandled = true;
            var keys = IoC.Get<IConfigurationKeys>();
            if (keys.MessageMode == MessageMode.Debug)
            {
                filterContext.Result = new ViewResult
                                           {
                                               ViewName = "Error",
                                               ViewData = filterContext.Controller.ViewData
                                           };
            }
            else
            {
                filterContext.Result = new JavaScriptResult()
                                           {
                                               Script = JavascriptHelper.Alert(exception.Message, null, AlertCategory.Error)
                                           };
            }
            IoC.Get<IUnitOfWork>().Rollback();
        }
    }
}