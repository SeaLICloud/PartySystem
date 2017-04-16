using System.Collections.Generic;
using System.Linq.Expressions;
using TomorrowSoft.Framework.Domain.CustomType;
using TomorrowSoft.Framework.Presentation.Mvc.HtmlTags;

namespace System.Web.Mvc.Html
{
    public static class TableGeboExtensions
    {
        public static MvcHtmlString Table<T>(this HtmlHelper htmlHelper, IEnumerable<T> dataSource,
           params Expression<Func<Table<T>, Column<T>>>[] columns)
        {
            var table = new Table<T>(htmlHelper)
                .Class("table table-bordered table-striped")
                .DataSource(dataSource);
            foreach (var column in columns)
            {
                column.Compile()(table);
            }
            return MvcHtmlString.Create(table.ToString());
        }

        public static MvcHtmlString Table<T>(this HtmlHelper htmlHelper, IEnumerable<T> dataSource,
            Expression<Func<Table<T>, Group<T>>> group,
            params Expression<Func<Table<T>, Column<T>>>[] columns)
        {
            var table = new Table<T>(htmlHelper)
                .Class("table table-bordered table-striped")
                .DataSource(dataSource);
            group.Compile()(table);
            foreach (var column in columns)
            {
                column.Compile()(table);
            }
            return MvcHtmlString.Create(table.ToString());
        }

        public static MvcHtmlString TreeTable<T>(this HtmlHelper htmlHelper, IEnumerable<T> dataSource,
            params Expression<Func<TreeTable<T>, Column<T>>>[] columns)
            where T : ITreeEnumerable<T>
        {
            var table = new TreeTable<T>(htmlHelper)
                .Class("table table-bordered table-striped")
                .DataSource(dataSource);
            foreach (var column in columns)
            {
                column.Compile()(table);
            }
            return MvcHtmlString.Create(table.ToString());
        }

        public static MvcHtmlString TreeGrid<T>(this HtmlHelper htmlHelper, IEnumerable<T> dataSource,
            Func<T, string> treeId, Func<T, string> parentTreeId,
            params Expression<Func<Table<T>, Column<T>>>[] columns)
        {
            var table = new Table<T>(htmlHelper)
                .TreeStyle(treeId, parentTreeId)
                .Class("table table-bordered table-striped")
                .DataSource(dataSource);
            foreach (var column in columns)
            {
                column.Compile()(table);
            }
            return MvcHtmlString.Create(table.ToString());
        }
    }
}