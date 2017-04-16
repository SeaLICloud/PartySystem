using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public class BoundingColumn<T> : Column<T>
    {
        private readonly IEnumerable<T> data_source;

        public BoundingColumn(IEnumerable<T> dataSource)
        {
            data_source = dataSource;
            HeadExpression = MvcHtmlString.Create("");
            DataExpressions = new List<Expression<Func<T, MvcHtmlString>>>();
            IsVisible = true;
        }

        public BoundingColumn<T> Head(MvcHtmlString head)
        {
            HeadExpression = head;
            return this;
        }

        public BoundingColumn<T> Head(string headText)
        {
            HeadExpression = MvcHtmlString.Create(headText);
            return this;
        }

        public BoundingColumn<T> Data(Expression<Func<T, MvcHtmlString>> dataExpression)
        {
            DataExpressions.Add(dataExpression);
            return this;
        }

        public BoundingColumn<T> Data(Expression<Func<T, string>> dataExpression)
        {
            DataExpressions.Add(x => MvcHtmlString.Create(dataExpression.Compile()(x)));
            return this;
        }

        public BoundingColumn<T> Data(string data)
        {
            DataExpressions.Add(x=>MvcHtmlString.Create(data));
            return this;
        }

        public BoundingColumn<T> AutoIndex()
        {
            DataExpressions.Add(x => MvcHtmlString.Create(((data_source as List<T>).IndexOf(x) + 1).ToString()));
            return this;
        }

        public BoundingColumn<T> Footer(string text)
        {
            FooterText += text;
            return this;
        }

        public BoundingColumn<T> HeaderStyle(string style)
        {
            HeaderRowStyle = style;
            return this;
        }

        public BoundingColumn<T> DataStyle(string style)
        {
            DataRowStyle = style;
            return this;
        }

        public BoundingColumn<T> FooterStyle(string style)
        {
            FooterRowStyle = style;
            return this;
        }

        public BoundingColumn<T> Visible(bool visible)
        {
            IsVisible = visible;
            return this;
        }
    }
}