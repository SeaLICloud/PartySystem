using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TomorrowSoft.Framework.Domain.CustomType;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public class TreeTable<T> where T : ITreeEnumerable<T>
    {
        private readonly HtmlHelper htmlHelper;
        private readonly string id;
        private string @class;
        private IEnumerable<T> dataSource;
        private readonly IList<Column<T>> columns;

        public TreeTable(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            id = Guid.NewGuid().ToString();
            columns = new List<Column<T>>();
        }

        public TreeTable<T> Class(string @class)
        {
            this.@class = @class;
            return this;
        }

        public TreeTable<T> DataSource(IEnumerable<T> dataSource)
        {
            this.dataSource = dataSource;
            return this;
        }

        public BoundingColumn<T> Column()
        {
            var column = new BoundingColumn<T>(dataSource);
            columns.Add(column);
            return column;
        }

        public BoundingColumn<T> AutoIndexColumn()
        {
            var column = new BoundingColumn<T>(dataSource)
                .AutoIndex();
            columns.Add(column);
            return column;
        }

        public CheckBoxColumn<T> CheckBoxColumn()
        {
            var column = new CheckBoxColumn<T>(id);
            columns.Add(column);
            return column;
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<table id=\"{0}\" class=\"{1}\">", id, @class);
            sb.AppendLine("    <thead>");
            sb.AppendLine("        <tr>");
            foreach (var column in this.columns)
            {
                sb.Append("           <th");
                if (column is CheckBoxColumn<T>)
                    sb.Append(" class=\"table_checkbox\"");
                sb.Append(">");
                sb.Append(column.HeadExpression);
                sb.Append("</th>");
                sb.AppendLine();

            }
            sb.AppendLine("        </tr>");
            sb.AppendLine("    </thead>");
            sb.AppendLine("    <tbody id=\"treetable1\">");
            foreach (var item in this.dataSource)
            {
                foreach (var row in item)
                {
                    sb.AppendLine("        <tr>");
                    foreach (var column in this.columns)
                    {
                        sb.AppendLine("               <td style=\"padding:2px 8px;\">");
                        foreach (var exp in column.DataExpressions)
                        {
                            sb.Append("                 ").AppendLine(exp.Compile()(row).ToString());
                        }
                        sb.AppendLine("               </td>");
                    }
                    sb.AppendLine("        </tr>");
                }
            }
            sb.AppendLine("    </tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("$(function(){");
            sb.AppendFormat("    var map=[{0}];", dataSource.CalculateMap()).AppendLine();
            sb.AppendFormat("    var options = {{openImg: \"{0}/tv-collapsable.gif\", shutImg: \"{0}/tv-expandable.gif\", leafImg: \"{0}/tv-item.gif\", lastOpenImg: \"{0}/tv-collapsable-last.gif\", lastShutImg: \"{0}/tv-expandable-last.gif\", lastLeafImg: \"{0}/tv-item-last.gif\", vertLineImg: \"{0}/vertline.gif\", blankImg: \"{0}/blank.gif\", collapse: false, column: 0, striped: true, highlight: true,  state:false}};",
                "/Scripts/jqtreetable/images").AppendLine();
            sb.AppendLine("    if(map!=null&&map.length>0){$(\"#treetable1\").jqTreeTable(map, options);}");
            sb.AppendLine("    });");
            sb.AppendLine("</script>");
            return sb.ToString();
        }

        //public Table<T> CheckBoxColumn(string name)
        //{
        //    return Column(
        //        htmlHelper.CheckBox(name + "s", new Dictionary<string, object>
        //                                            {
        //                                                {"data-tableid", id},
        //                                                {"class", "select_rows"}
        //                                            }),
        //        x => htmlHelper.CheckBox(name, new {@class = "select_row"}));
        //}
    }
}