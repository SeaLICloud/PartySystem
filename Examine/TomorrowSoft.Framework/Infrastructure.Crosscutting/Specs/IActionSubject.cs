using System.Web.Mvc;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Specs
{
    public interface IActionSubject
    {
        ActionResult Invoke();
        IActionSubject RequestFile(string key, string content);
    }
}