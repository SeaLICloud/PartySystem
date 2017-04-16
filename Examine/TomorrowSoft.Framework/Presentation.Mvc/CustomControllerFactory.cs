using System.Web.Mvc;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)IoC.Current.Resolve(controllerType, null);
        }
    }
}