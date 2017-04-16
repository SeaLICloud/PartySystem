using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public interface ITreeEnumerable<T> : IEnumerable<T>
    {
        IEnumerable<T> Children { get; }
        int Level { get; }
    }

    public static class TreeEnumerableExtensions
    {
        public static string Map<T>(this T obj, int parentMap, ref int startMap)
            where T : ITreeEnumerable<T>
        {
            var sb = new StringBuilder();
            sb.Append(parentMap).Append(",");
            parentMap = startMap;
            startMap++;
            foreach (var item in obj.Children)
            {
                sb.Append(item.Map(parentMap, ref startMap));
            }
            return sb.ToString();
        }

        public static string CalculateMap<T>(this IEnumerable<T> list)
            where T:ITreeEnumerable<T>
        {
            int start = 1;
            var sb = new StringBuilder();
            foreach(var item in list)
            {
                sb.Append(item.Map(0, ref start));
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static IEnumerable<SelectListItem> ToTreeSelectListItem<TSource, TKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> key,
            Func<TSource, TValue> value)
            where TSource:ITreeEnumerable<TSource>
        {
            var list = new List<SelectListItem>();
            var tag = "";
            foreach (var root in source)
            {
                foreach (var item in root)
                {
                    for (var i = 2; i <= item.Level; i++)
                        tag += "　　";
                    switch (item.Level)
                    {
                        case 1:
                            tag += "◆";
                            break;
                        case 2:
                            tag += "■";
                            break;
                        case 3:
                            tag += "●";
                            break;
                    }
                    list.Add(new SelectListItem
                        {
                            Text = string.Format("{0} {1}", tag, value(item)),
                            Value = key(item).ToString()
                        });
                    tag = "";
                }
            }
            return list;
        }
    }
}