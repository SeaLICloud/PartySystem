using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public abstract class Column<T>
    {
        public virtual MvcHtmlString HeadExpression { get; protected set; }
        public virtual IList<Expression<Func<T, MvcHtmlString>>> DataExpressions { get; protected set; }
        public virtual string FooterText { get; protected set; }
        public string HeaderRowStyle { get; protected set; }
        public string DataRowStyle { get; protected set; }
        public string FooterRowStyle { get; protected set; }
        public bool IsVisible { get; protected set; }
    }
}