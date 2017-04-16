using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Excel
{
    public class Import<T>
    {
        private Func<DataRow, T> constructor;
        private Func<DataRow, IEnumerable<T>, T> constructor2;
        private readonly IList<Action<T, DataRow>> maps;

        public Import()
        {
            this.maps = new List<Action<T, DataRow>>();
        }

        private DataTable ToDataTable(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new FrameworkException("导入数据为空");

            var rows = text.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            DataTable dataTable = new DataTable();
            //Head Row
            var columnHeads = rows[0].Split(new char[] {',', '，'});
            foreach(var column in columnHeads)
            {
                dataTable.Columns.Add(column);
            }
            //Data Row
            for (var i = 1; i < rows.Length; i++)
            {
                var dataRow = dataTable.NewRow();
                var columns = rows[i].Split(new char[] { ',', '，' });
                //如果列太少，则这一行不处理
                if (columns.Length < columnHeads.Length)
                    continue;
                for (var j = 0; j < columns.Length; j++)
                {
                    dataRow[j] = columns[j];
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
        
        public IEnumerable<T> MapTo(string text)
        {
            var dt = ToDataTable(text);
            var items = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = default(T);
                if (constructor != null)
                    item = constructor.Invoke(row);
                else if (constructor2 != null)
                    item = constructor2.Invoke(row, items);
                else
                    throw new FrameworkException("未指定实例化对象的方式");
                foreach (var map in maps)
                {
                    map.Invoke(item, row);
                }
                items.Add(item);
            }
            return items;
        }

        public Import<T> Map(Action<T, DataRow> action)
        {
            maps.Add(action);
            return this;
        }

        public Import<T> New(Func<DataRow, T> func)
        {
            constructor = func;
            return this;
        }

        public Import<T> New(Func<DataRow, IEnumerable<T>, T> func)
        {
            constructor2 = func;
            return this;
        }
    }
}