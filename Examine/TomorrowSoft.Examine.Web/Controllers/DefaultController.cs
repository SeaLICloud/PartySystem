using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TommorrowSoft.Examine.Application;
using TomorrowSoft.Examine.Web.Helps;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Data.SessionFactories;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Examine.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IAdminService _adminService;
        private const string Path = "~/DatabaseScripts";

        public DefaultController(
            ISecurityService securityService, 
            IAdminService adminService)
        {
            _securityService = securityService;
            _adminService = adminService;
        }

        public ActionResult Index(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                ViewData[Keys.Message] = message;
            return View("Index");
        }
        
        public ActionResult ClearData()
        {
            IoC.Get<IUnitOfWork>().UnBindContext();
            Database.Init();
            return RedirectToAction("Index", new { message = "数据已清空" });
        }
        
        [HttpPost]
        public ActionResult InitVersion()
        {
            _securityService.CreateVersion("Web")
                .VersionNumber("0.1")
                .SoftwareTitle("测试平台");
            _securityService.CreateVersion("Database")
                .VersionNumber("0.sql");
            return RedirectToAction("Index", new { message = "数据库初始化成功！" });
        }

        [HttpPost]
        public ActionResult UpdateDatabase()
        {
            var folder = new DirectoryInfo(Server.MapPath(Path));
            var files = folder.GetFiles();

            var dbVersion = _securityService.GetVersionByType("Database");
            var repository = IoC.Get<IRepository>();
            var validFiles = files
                .Where(x => System.String.CompareOrdinal(x.Name, dbVersion.VersionNumber) > 0)
                .OrderBy(x => x.Name);
            if (!validFiles.Any())
                return RedirectToAction("Index", new { message = "数据库已经是最新版本，无需升级！" });
            foreach (var file in validFiles)
            {
                var sr = file.OpenText();
                var sql = sr.ReadToEnd();
                sr.Close();
                repository.ExcuteCommand(sql);
                _securityService
                    .EditVersion("Database")
                    .VersionNumber(file.Name);
            }
            return RedirectToAction("Index", new { message = "数据库升级成功！" });
        }

        [HttpPost]
        public ActionResult InitData()
        {
            return RedirectToAction("Index", new { message = "初始化成功" });
        }

        [HttpPost]
        public ActionResult InitTestData()
        {
            return RedirectToAction("Index", new { message = "初始化成功" });
        }

    }
}
