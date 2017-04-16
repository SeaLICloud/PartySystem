using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public class CheckBoxColumn<T> : Column<T>
    {
        private readonly string table_id;
        private Expression<Func<T, object>> expression;
        private string name;

        public CheckBoxColumn(string tableId)
        {
            table_id = tableId;
            HeaderRowStyle = "table_checkbox";
            DataRowStyle = "text-align-center";
            IsVisible = true;
        }

        public CheckBoxColumn<T> Value(Expression<Func<T, object>> expression)
        {
            this.expression = expression;
            return this;
        }

        public CheckBoxColumn<T> Name(string name)
        {
            this.name = name;
            return this;
        }

        public CheckBoxColumn<T> Visible(bool visible)
        {
            IsVisible = visible;
            return this;
        }

        public override MvcHtmlString HeadExpression 
        { 
            get
            {
                var html = string.Format("<input type=\"checkbox\" data-tableid=\"{0}\" class=\"select_rows\" />", table_id);
                return MvcHtmlString.Create(html);
            }
        }

        public override IList<Expression<Func<T, MvcHtmlString>>> DataExpressions
        {
            get
            {
                var format = new StringBuilder();
                format.Append("<input type=\"checkbox\" class=\"row_sel\" ");
                if (!string.IsNullOrEmpty(name))
                    format.Append("name=\"{0}\" ");
                if (expression != null)
                    format.Append("value=\"{1}\" ");
                format.Append("/>");
                return new List<Expression<Func<T, MvcHtmlString>>>()
                           {
                               x => MvcHtmlString.Create(string.Format(format.ToString(), name, expression.Compile()(x)))
                           };
            }
        }
    }
}