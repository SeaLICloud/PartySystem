using TommorrowSoft.Examine.Application;
using TomorrowSoft.Framework.Presentation.Mvc;

namespace TomorrowSoft.Examine.Web.Helps.BaseControllers
{
    public class AdminAreaController : BaseController
    {
        protected readonly IAdminService Service;

        public AdminAreaController(IAdminService service)
        {
            this.Service = service;
        }
    }
}
