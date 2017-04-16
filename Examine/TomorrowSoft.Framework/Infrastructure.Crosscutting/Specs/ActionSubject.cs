using System;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Rhino.Mocks;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Specs
{
    public class ActionSubject<TController> : IActionSubject
        where TController : Controller
    {
        private readonly TController controller;
        private Expression<Func<TController, ActionResult>> action;

        public ActionSubject(TController controller, Expression<Func<TController, ActionResult>> action)
        {
            this.controller = controller;
            this.action = action;
        }

        public ActionResult Invoke()
        {
            var result = action.Compile().Invoke(controller);
            IoC.Get<IUnitOfWork>().Commit();
            return result;
        }

        public IActionSubject RequestFile(string key, string content)
        {
            var stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(content));
            var context = MockRepository.GenerateMock<HttpContextBase>();
            var file = MockRepository.GenerateMock<HttpPostedFileBase>();
            file.Stub(x => x.ContentLength).Return((int) stream.Length);
            file.Stub(x => x.InputStream).Return(stream);
            context.Stub(x => x.Request.Files[key]).Return(file);

            var controllerContext = new ControllerContext();
            controllerContext.HttpContext = context;
            this.controller.ControllerContext = controllerContext;
            return this;
        }
    }
}