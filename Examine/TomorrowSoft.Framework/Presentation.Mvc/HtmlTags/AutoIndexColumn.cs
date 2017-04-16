using System.Web.Mvc;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public class AutoIndexColumn<T> : Column<T>
    {
        public AutoIndexColumn()
        {
            HeaderRowStyle = "table_autoindex";
            DataRowStyle = "text-align-center";
            IsVisible = true;
        }

        public AutoIndexColumn<T> Head(MvcHtmlString head)
        {
            HeadExpression = head;
            return this;
        }

        public AutoIndexColumn<T> Head(string headText)
        {
            HeadExpression = MvcHtmlString.Create(headText);
            return this;
        }

        public AutoIndexColumn<T> Visible(bool visible)
        {
            IsVisible = visible;
            return this;
        }

    }
}