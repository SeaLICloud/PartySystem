using System;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions
{
    public class FrameworkException:Exception
    {
        public FrameworkException(string message) : base(message)
        {
        }
    }
}