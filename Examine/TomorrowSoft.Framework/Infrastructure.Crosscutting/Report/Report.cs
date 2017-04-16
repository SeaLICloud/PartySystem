using System.Linq;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Report
{
    public abstract class Report
    {
          public Report(string reportPath, ReportDataSource reportDataSource)
        {
            ReportPath = reportPath;
            ReportDataSource = reportDataSource;
        }

          public Report(string reportPath, ReportDataSource reportDataSource, ReportParameter[] myParameterList)
        {
            ReportPath = reportPath;
            ReportDataSource = reportDataSource;
            MyParameterList = myParameterList;
        }

        public string ReportPath { get; private set; }
        public ReportDataSource ReportDataSource { get; private set; }
        public ReportParameter[] MyParameterList { get; private set; }
        
        public virtual byte[] Render()
        {
            return Render();
        }
    }
}