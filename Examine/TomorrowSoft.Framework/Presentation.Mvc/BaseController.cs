using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    [Log]
    [Transaction]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]//去掉IE对Ajax的缓存
    public abstract class BaseController : Controller
    {
         
    }
}