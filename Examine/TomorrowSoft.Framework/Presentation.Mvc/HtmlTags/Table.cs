using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TomorrowSoft.Framework.Presentation.Mvc.HtmlTags
{
    public class Table<T>
    {
        private readonly HtmlHelper htmlHelper;
        private readonly string id;
        private string @class;
        private IEnumerable<T> dataSource;
        private Group<T> group; 
        private readonly IList<Column<T>> columns;
        private Func<T, string> treeId;
        private Func<T, string> parentTreeId;

        public Table(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            id = Guid.NewGuid().ToString();
            columns = new List<Column<T>>();
        }

        public Table<T> Class(string @class)
        {
            this.@class = @class;
            return this;
        }

        public Table<T> DataSource(IEnumerable<T> dataSource)
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

        public AutoIndexColumn<T> AutoIndexColumn()
        {
            var column = new AutoIndexColumn<T>();
            columns.Add(column);
            return column;
        }

        public CheckBoxColumn<T> CheckBoxColumn()
        {
            var column = new CheckBoxColumn<T>(id);
            columns.Add(column);
            return column;
        }

        public Group<T> Group()
        {
            var group = new Group<T>();
            this.group = group;
            return group;
        }
        
        public override string ToString()
        {
            var columnCount = columns.Count(x => x.IsVisible);

            var sb = new StringBuilder();
            sb.AppendFormat("<table id=\"{0}\" class=\"{1}\">", id, @class);
            sb.AppendLine("    <thead>");
            sb.AppendLine("        <tr>");
            foreach (var column in this.columns)
            {
                if(!column.IsVisible)
                    continue;
                sb.Append("           <th");
                if (!string.IsNullOrEmpty(column.HeaderRowStyle))
                    sb.AppendFormat(" class=\"{0}\"", column.HeaderRowStyle);
                sb.Append(">");
                sb.Append(column.HeadExpression);
                sb.Append("</th>");
                sb.AppendLine();

            }
            sb.AppendLine("        </tr>");
            sb.AppendLine("    </thead>");
            sb.AppendLine("    <tbody>");
            int index = 0;
            if (group == null)
            {
                foreach (var row in this.dataSource)
                {
                    string style = "";
                    if (treeId != null)
                        style = "treegrid-" + treeId(row);
                    if (parentTreeId != null && !string.IsNullOrEmpty(parentTreeId(row)))
                        style = style + " treegrid-parent-" + parentTreeId(row);
                    sb.AppendFormat("        <tr class=\"{0}\">", style).AppendLine();
                    foreach (var column in this.columns)
                    {
                        if (!column.IsVisible)
                            continue;
                        if (!string.IsNullOrEmpty(column.DataRowStyle))
                            sb.AppendFormat("               <td class=\"{0}\">", column.DataRowStyle);
                        else
                            sb.AppendLine("               <td>");
                        sb.Append("                 ");
                        if (column is AutoIndexColumn<T>)
                            sb.AppendLine((++index).ToString());
                        else
                        {
                            foreach (var item in column.DataExpressions)
                            {
                                sb.AppendLine(item.Compile()(row).ToString());
                            }
                        }
                        sb.AppendLine("               </td>");
                    }
                    sb.AppendLine("        </tr>");
                }
            }
            else
            {
                foreach (var rowGroup in this.dataSource.GroupBy(this.group.Key))
                {
                    sb.AppendFormat("<tr class=\"{0}\">", group.DataRowStyle).AppendLine();
                    sb.AppendFormat("<td colspan=\"{0}\">", columnCount).AppendLine();
                    foreach (var item in group.DataExpressions)
                    {
                        sb.AppendLine(item.Compile()(rowGroup.First()).ToString());
                    }
                    sb.AppendLine("</td>");
                    sb.AppendLine("</tr>");
                    foreach (var row in rowGroup)
                    {
                        string style = "";
                        if (treeId != null)
                            style = "treegrid-" + treeId(row);
                        if (parentTreeId != null && !string.IsNullOrEmpty(parentTreeId(row)))
                            style = style + " treegrid-parent-" + parentTreeId(row);
                        sb.AppendFormat("        <tr class=\"{0}\">", style).AppendLine();
                        foreach (var column in this.columns)
                        {
                            if (!column.IsVisible)
                                continue;
                            if (!string.IsNullOrEmpty(column.DataRowStyle))
                                sb.AppendFormat("               <td class=\"{0}\">", column.DataRowStyle);
                            else
                                sb.AppendLine("               <td>");
                            sb.Append("                 ");
                            if (column is AutoIndexColumn<T>)
                                sb.AppendLine((++index).ToString());
                            else
                            {
                                foreach (var item in column.DataExpressions)
                                {
                                    sb.AppendLine(item.Compile()(row).ToString());
                                }
                            }
                            sb.AppendLine("               </td>");
                        }
                        sb.AppendLine("        </tr>");
                    }
                }
            }
            if(HasFooter())
            {
                sb.AppendLine("        <tr>");
                foreach (var column in this.columns)
                {
                    if (!column.IsVisible)
                        continue;
                    if (!string.IsNullOrEmpty(column.FooterRowStyle))
                        sb.AppendFormat("               <td class=\"{0}\">", column.FooterRowStyle);
                    else
                        sb.AppendLine("               <td>");
                    sb.Append("                 ").AppendLine(column.FooterText);
                    sb.AppendLine("               </td>");
                }
                sb.AppendLine("        </tr>");
            }
            sb.AppendLine("    </tbody>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        private bool HasFooter()
        {
            return columns.Any(column => !string.IsNullOrEmpty(column.FooterText));
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

        public Table<T> TreeStyle(Func<T, string> tree_id, Func<T, string> parent_tree_id)
        {
            this.treeId = tree_id;
            this.parentTreeId = parent_tree_id;
            return this;
        }
    }
}