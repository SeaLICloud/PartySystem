using System.Web.Routing;
using TomorrowSoft.Framework.Domain.CustomType;

namespace System.Web.Mvc.Html
{
    public static class PaginationGeboExtensions
    {
        public static MvcHtmlString Pagination(this HtmlHelper htmlHelper, string actionName, string controllerName, PageInfo pageInfo)
        {
            return Pagination(htmlHelper, actionName, controllerName, pageInfo, null);
        }
        public static MvcHtmlString Pagination(this HtmlHelper htmlHelper, string actionName, string controllerName, PageInfo pageInfo, object routeValueDictionary)
        {
            var rowCount = pageInfo.TotalCount;
            var pageSize = pageInfo.PageSize;
            var currentPage = pageInfo.CurrentPage;

            const int neighbor = 2;
            var pageCount = rowCount % pageSize == 0 ? rowCount / pageSize : rowCount / pageSize + 1;

            //分页页码栏
            var divPagination = new TagBuilder("div");
            divPagination.MergeAttribute("class", "dataTables_paginate paging_bootstrap");
            var ul = new TagBuilder("ul");
            ul.MergeAttribute("class", "pagination pagination-sm");
            //上一页
            if (currentPage <= 1)
                ul.InnerHtml += LiInPagination(htmlHelper, "< 上一页", PaginationLiClass.Disabled);
            else
                ul.InnerHtml += LiInPagination(htmlHelper, "< 上一页", currentPage - 1, pageSize, actionName, controllerName, routeValueDictionary);

            //是否在前面加...
            if (currentPage > 3)
            {
                //第一页
                if (currentPage == 1)
                    ul.InnerHtml += LiInPagination(htmlHelper, "1", PaginationLiClass.Active);
                else
                    ul.InnerHtml += LiInPagination(htmlHelper, "1", 1, pageSize, actionName, controllerName, routeValueDictionary);
            }
            if (currentPage > 4)
            {
                ul.InnerHtml += LiInPagination(htmlHelper, "...", PaginationLiClass.Disabled);
            }
            //前2页、当前页、后2页
            for (var i = currentPage - neighbor; i <= currentPage + neighbor; i++)
            {
                if (i >= 1 && i <= pageCount)
                {
                    if (currentPage == i)
                        ul.InnerHtml += LiInPagination(htmlHelper, i.ToString(), PaginationLiClass.Active);
                    else
                        ul.InnerHtml += LiInPagination(htmlHelper, i.ToString(), i, pageSize, actionName, controllerName, routeValueDictionary);
                }
            }
            //是否在后面加...
            if (currentPage < pageCount - 3)
            {
                ul.InnerHtml += LiInPagination(htmlHelper, "...", PaginationLiClass.Disabled);
            }
            if (currentPage < pageCount - 2)
            {
                //最后一页
                if (currentPage == pageCount)
                    ul.InnerHtml += LiInPagination(htmlHelper, pageCount.ToString(), PaginationLiClass.Active);
                else
                    ul.InnerHtml += LiInPagination(htmlHelper, pageCount.ToString(), pageCount, pageSize, actionName, controllerName, routeValueDictionary);
            }

            //下一页
            if (currentPage >= pageCount)
                ul.InnerHtml += LiInPagination(htmlHelper, "下一页 >", PaginationLiClass.Disabled);
            else
                ul.InnerHtml += LiInPagination(htmlHelper, "下一页 >", currentPage + 1, pageSize, actionName, controllerName, routeValueDictionary);
            divPagination.InnerHtml = ul.ToString();

            //分页信息
            var start = pageInfo.Start;
            var end = pageInfo.End;
            var info = string.Format("共 {0} 条记录，当前显示第 {1} - {2} 条", rowCount, start, end);
            var divInfo = new TagBuilder("div");
            divInfo.MergeAttribute("class", "dataTables_info");
            divInfo.SetInnerText(info);

            //分页组合
            var divPart1 = new TagBuilder("div");
            divPart1.MergeAttribute("class", "col-sm-5");
            divPart1.InnerHtml = divInfo.ToString();
            var divPart2 = new TagBuilder("div");
            divPart2.MergeAttribute("class", "col-sm-7");
            divPart2.InnerHtml = divPagination.ToString();

            return MvcHtmlString.Create(string.Format("{0}{1}", divPart1.ToString(), divPart2.ToString()));
        }

        private static string LiInPagination(this HtmlHelper htmlHelper, string text, int currentPage, int pageSize, string actionName, string controllerName, object routeValueDictionary)
        {
            var dict = new RouteValueDictionary();
            dict.Add("pageSize", pageSize);
            dict.Add("currentPage", currentPage);
            if (routeValueDictionary != null)
            {
                var pies = routeValueDictionary.GetType().GetProperties();
                foreach (var pi in pies)
                {
                    var key = pi.Name;
                    var value = pi.GetValue(routeValueDictionary, null);
                    dict.Add(key, value);
                }
            }
            var a = htmlHelper.ActionLink(text, actionName, controllerName, dict, null);
            var li = new TagBuilder("li");
            li.InnerHtml = a.ToString();
            return li.ToString();
        }

        private static string LiInPagination(this HtmlHelper htmlHelper, string text, PaginationLiClass classAttribute = PaginationLiClass.Default)
        {
            var a = new TagBuilder("a");
            a.SetInnerText(text);
            a.MergeAttribute("href", "#");
            var li = new TagBuilder("li");
            li.InnerHtml = a.ToString();
            if (classAttribute != PaginationLiClass.Default)
                li.MergeAttribute("class", classAttribute.ToString().ToLower());
            return li.ToString();
        }
    }
}