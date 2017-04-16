using System;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Aops
{
    public class AopHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var type = input.Target.GetType();

            MethodInfo beforeMethod = null;
            MethodInfo afterMethod = null;

            var mies = type.GetMethods();
            foreach(var mi in mies)
            {
                var attributes = mi.GetCustomAttributes(true);
                foreach(Attribute attr in attributes)
                {
                    if (attr is BeforeInvokeAttribute)
                        beforeMethod = mi;
                    if (attr is AfterInvokeAttribute)
                        afterMethod = mi;
                }

            }

            //执行加有BeforeAttribute的方法
            if(beforeMethod != null)
            {
                beforeMethod.Invoke(input.Target, null);
            }

            //执行当前方法
            var retvalue = getNext()(input, getNext);

            //执行加油AfterAttribute的方法
            if (afterMethod != null)
            {
                afterMethod.Invoke(input.Target, null);
            }

            return retvalue;
        }

        public int Order { get; set; }
    }
}