using System;
using System.Diagnostics;
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
            RS2005.ReportingService2005 rs;
            RE2005.ReportExecutionService rsExec;

            // Create a new proxy to the web service
            rs = new RS2005.ReportingService2005();
            rsExec = new RE2005.ReportExecutionService();

            // Authenticate to the Web service using Windows credentials
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.Url = "http://spm-sql/reportserver/reportservice2005.asmx";
            rsExec.Url = "http://spm-sql/reportserver/reportexecution2005.asmx";

            const string historyID = null;
            const string deviceInfo = null;
            const string format = "PDF";
            Byte[] results;
            const string _reportName = "/GeniusReports/Job/SPM_ServiceReport";

            const string _historyID = null;
            const bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            try
            {
                RS2005.ReportParameter[] _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                RE2005.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);
                RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];

                if (_parameters.Length > 0)
                {
                    parameters[0] = new RE2005.ParameterValue
                    {
                        //parameters[0].Label = "";
                        Name = "ReqNumber",
                        Value = invoiceno
                    };
                }
                rsExec.SetExecutionParameters(parameters, "en-us");

                results = rsExec.Render(format, deviceInfo,
                          out string extension, out string encoding,
                          out string mimeType, out RE2005.Warning[] warnings, out string[] streamIDs);

                try
                {
                    File.WriteAllBytes(fileName, results);
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                    //MessageBox.Show(e.Message, "SPM Connect - Save Report", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
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