using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Aops
{
    public class AopAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AopHandler();
        }
    }
}