using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public static class DataFormattingExtend
    {
        /// <summary>
        /// 下拉框选项为空列表（只含“请选择”）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToEmptyList(this IList<SelectListItem> list)
        {
            if (list.Count == 0)
                list.Insert(0, new SelectListItem { Text = "--请选择--", Value = "" });
            return list;
        }

        public static IEnumerable<SelectListItem> ToSelectListItem<TSource, TValue, TText>(
            this IEnumerable<TSource> source,
            Func<TSource, TValue> value,
            Func<TSource, TText> text)
        {
            var list = new List<SelectListItem>();
            foreach (var item in source)
            {
                list.Add(new SelectListItem
                {
                    Text = text(item).ToString(),
                    Value = value(item).ToString()
                });
            }
            return list;
        }

        public static IEnumerable<SelectListItem> Selected(
            this IEnumerable<SelectListItem> list,
            string selectedValue)
        {
            foreach (var item in list)
            {
                item.Selected = item.Value == selectedValue;
            }
            return list;
        }

        /// <summary>
        /// 将列表转成Json对象
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToJson(this IList<SelectListItem> items)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            if (items != null)
            {
                foreach (var item in items)
                {
                    sb.AppendFormat("[\"{0}\",\"{1}\"]", item.Value, item.Text);
                    sb.Append(",");
                }
                if (sb.Length > 1)
                    sb.Remove(sb.Length - 1, 1); //去掉最后一个逗号
            }
            sb.Append("]");
            return sb.ToString();
        } 
    }
}