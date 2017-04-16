using System.Linq;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Report
{
    public  class ExcelReport : Report
    {
        public ExcelReport(string reportPath, ReportDataSource reportDataSource) : base(reportPath, reportDataSource)
        {
        }

        public ExcelReport(string reportPath, ReportDataSource reportDataSource, ReportParameter[] myParameterList) : base(reportPath, reportDataSource, myParameterList)
        {
        }

        public override byte[] Render()
        {
            var localReport = new LocalReport();
            localReport.ReportPath = ReportPath;
            localReport.DataSources.Add(ReportDataSource);
            if (MyParameterList != null)
            {
                localReport.SetParameters(MyParameterList);
            }

            //从rdlc文件中读取页面设置
            var doc = XElement.Load(ReportPath);
            var page = doc.Elements().FirstOrDefault(x => x.Name.LocalName == "Page");

            string reportType = "Excel";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo = string.Format(@"<DeviceInfo>
    <OutputFormat>Excel</OutputFormat>
    <PageWidth>{0}</PageWidth>
    <PageHeight>{1}</PageHeight>
    <MarginTop>{2}</MarginTop>
    <MarginLeft>{3}</MarginLeft>
    <MarginRight>{4}</MarginRight>
    <MarginBottom>{5}</MarginBottom>
</DeviceInfo>",
              page.Elements().FirstOrDefault(x => x.Name.LocalName == "PageWidth").Value,
              page.Elements().FirstOrDefault(x => x.Name.LocalName == "PageHeight").Value,
              page.Elements().FirstOrDefault(x => x.Name.LocalName == "TopMargin").Value,
              page.Elements().FirstOrDefault(x => x.Name.LocalName == "LeftMargin").Value,
              page.Elements().FirstOrDefault(x => x.Name.LocalName == "RightMargin").Value,
              page.Elements().FirstOrDefault(x => x.Name.LocalName == "BottomMargin").Value);

            Warning[] warnings;
            string[] streams;

            //Render the report
            return localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
        }
    }
}