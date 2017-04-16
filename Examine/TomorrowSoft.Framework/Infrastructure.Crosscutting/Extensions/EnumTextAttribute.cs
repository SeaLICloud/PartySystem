using System;
using System.Reflection;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumTextAttribute : Attribute
    {
        public EnumTextAttribute(string text)
        {
            EnumText = text;
        }

        public string EnumText { get; private set; }

        public static string GetText(Type tp, string name)
        {
            MemberInfo[] mi = tp.GetMember(name);
            if (mi.Length > 0)
            {
                var attr = Attribute.GetCustomAttribute(mi[0], typeof(EnumTextAttribute)) as EnumTextAttribute;
                if (attr != null)
                {
                    return attr.EnumText;
                }
            }
            return name;
        }
        public static string GetText(object value)
        {
            if (value != null)
            {
                MemberInfo[] mi = value.GetType().GetMember(value.ToString());
                if (mi.Length > 0)
                {
                    var attr = Attribute.GetCustomAttribute(mi[0], typeof(EnumTextAttribute)) as EnumTextAttribute;
                    if (attr != null)
                    {
                        return attr.EnumText;
                    }
                }
                return value.ToString();
            }
            return null;
        }
    }
}