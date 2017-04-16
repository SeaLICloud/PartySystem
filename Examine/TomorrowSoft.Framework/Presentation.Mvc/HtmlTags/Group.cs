using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public class Group<T>
    {
        public Func<T, string> Key { get; private set; }
        public virtual IList<Expression<Func<T, MvcHtmlString>>> DataExpressions { get; protected set; }
        public string DataRowStyle { get; private set; }

        public Group()
        {
            DataExpressions = new List<Expression<Func<T, MvcHtmlString>>>();
        }

        public Group<T> By(Func<T, string> key)
        {
            Key = key;
            return this;
        }

        public Group<T> Data(Expression<Func<T, MvcHtmlString>> dataExpression)
        {
            DataExpressions.Add(dataExpression);
            return this;
        }

        public Group<T> Data(Expression<Func<T, string>> dataExpression)
        {
            DataExpressions.Add(x => MvcHtmlString.Create(dataExpression.Compile()(x)));
            return this;
        }

        public Group<T> DataStyle(string style)
        {
            DataRowStyle = style;
            return this;
        }
    }
}