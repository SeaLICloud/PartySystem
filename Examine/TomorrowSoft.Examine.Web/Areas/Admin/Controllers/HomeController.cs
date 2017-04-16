using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TommorrowSoft.Examine.Application;
using TomorrowSoft.Examine.Web.Helps.BaseControllers;

namespace TomorrowSoft.Examine.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminAreaController
    {
        //
        // GET: /Admin/Home/

        public HomeController(IAdminService service) : base(service)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
