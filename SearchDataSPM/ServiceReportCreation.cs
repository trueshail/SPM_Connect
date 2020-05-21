using SearchDataSPM.Miscellaneous;
using System.IO;

namespace SearchDataSPM
{
    public class ServiceReportCreation
    {
        public ServiceReportCreation(string report)
        {
            SaveReport(report);
        }

        #region Save Report

        public void SaveReport(string invoiceno, string fileName)
        {
            const string _reportName = "/GeniusReports/Job/SPM_ServiceReport";
            RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];
            parameters[0] = new RE2005.ParameterValue
            {
                Name = "ReqNumber",
                Value = invoiceno
            };

            ReportHelper.SaveReport(fileName, _reportName, parameters);
        }

        private void SaveReport(string reqno)
        {
            const string filepath = @"\\spm-adfs\Shares\Shail\vs\";
            Directory.CreateDirectory(filepath);
            string fileName = filepath + reqno + ".pdf";
            SaveReport(reqno, fileName);
        }

        #endregion Save Report
    }
}