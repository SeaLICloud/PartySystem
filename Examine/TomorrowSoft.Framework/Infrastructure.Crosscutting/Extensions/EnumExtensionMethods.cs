using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    public static class EnumExtensionMethods
    {
        public static string Text<TEnum>(this TEnum value) where TEnum : struct
        {
            return EnumTextAttribute.GetText(value);
        }

        public static string Value<TEnum>(this TEnum value) where TEnum : struct
        {
            return value.ToString();
        }

        public static string Class<TEnum>(this TEnum value) where TEnum : struct
        {
            return EnumClassAttribute.GetClass(value);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(this Type type)
        {
            var result = new List<SelectListItem>();
            foreach (var name in Enum.GetNames(type))
            {
                result.Add(new SelectListItem
                               {
                                   Value = name,
                                   Text = EnumTextAttribute.GetText(type, name)
                               });
            }
            return result;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(this Type type, string selectedValue)
        {
            var result = new List<SelectListItem>();
            foreach (var name in Enum.GetNames(type))
            {
                result.Add(new SelectListItem
                {
                    Value = name,
                    Text = EnumTextAttribute.GetText(type, name),
                    Selected = name == selectedValue
                });
            }
            return result;
        }

        public static IEnumerable<T> ToList<T>(this Type type)
        {
            var result = new List<T>();
            foreach (var name in Enum.GetNames(type))
            {
                result.Add((T)(Enum.Parse(type, name)));
            }
            return result;
        }

        public static T To<T>(this string value) where T : struct
        {
            foreach (var item in typeof (T).ToList<T>())
            {
                if (item.Value() == value||item.Text()==value)
                    return item;
            }
            return default(T);
        }
    }
}