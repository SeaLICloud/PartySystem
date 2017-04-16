using System;
using System.Reflection;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumClassAttribute : Attribute
    {
        public EnumClassAttribute(string @class)
        {
            EnumClass = @class;
        }

        public string EnumClass { get; private set; }

        public static string GetClass(object value)
        {
            if (value != null)
            {
                MemberInfo[] mi = value.GetType().GetMember(value.ToString());
                if (mi.Length > 0)
                {
                    var attr = Attribute.GetCustomAttribute(mi[0], typeof(EnumClassAttribute)) as EnumClassAttribute;
                    if (attr != null)
                    {
                        return attr.EnumClass;
                    }
                }
                return value.ToString();
            }
            return null;
        }
    }
}