using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;

namespace ServiceReport
{
    public class Heartbeat
    {
        private readonly Timer _timer;
        private SqlTableDependency<ServiceReport> _serviceDependency;
        public Heartbeat()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() };
            File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);
        }

        public void Start()
        {
            // _timer.Start();
            Servicesqlnotifier();
        }

        public void Stop()
        {
            // _timer.Stop();
            _serviceDependency.Stop();
        }
        public void Servicesqlnotifier()
        {
            var mapper = new ModelToTableMapper<ServiceReport>();
            mapper.AddMapping(c => c.Title, "Title");
            mapper.AddMapping(c => c.ProjectNo, "ProjectNo");

            _serviceDependency = new SqlTableDependency<ServiceReport>("Data Source=spm-sql;Initial Catalog=SPM_Database;User ID=SPM_Agent;password=spm5445", tableName: "spservicereports", mapper: mapper);
            _serviceDependency.OnChanged += _serviceDependency_OnChanged;
            _serviceDependency.OnError += _serviceDependency_OnError;
            _serviceDependency.Start();

        }
        private void _serviceDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<ServiceReport> e)
        {
            var changedEntity = e.Entity;
            string type = e.ChangeType.ToString();
            if (type == "Insert")
            {
                //string fileName = "";
                //string filepath = @"\\spm-adfs\Shares\Shail\vs\";
                //System.IO.Directory.CreateDirectory(filepath);
                //fileName = filepath + changedEntity.Title + ".pdf";
                //SaveReport(changedEntity.Title, fileName);
                makereportAsync(changedEntity.Title);
                //Console.WriteLine("New Report got added");
                //string fileName = "";
                //string filepath = @"\\spm-adfs\Shares\Shail\vs\";
                //fileName = filepath + changedEntity.Title + ".pdf";

                //SendEmail("shail@spm-automation.com", "SPM CONNECT - PROBLEM CASE ID: " + Path.GetRandomFileName(), changedEntity.Title, "");

            }
        }

        private void _serviceDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            throw e.Error;
        }

        #region Save Report

        private async void makereportAsync(string invoiceno)
        {
            await Foo(invoiceno);
        }

        public async Task Foo(string invoiceno)
        {
            Console.WriteLine("Received new report : " + invoiceno);
            Console.WriteLine("Waiting for 2 minutes to complete report download...");
            await Task.Delay(2 * 60 * 1000);

            Console.WriteLine("Starting to create the report");
            string fileName = "";
            string filepath = @"\\spm-adfs\Shares\Shail\vs\";
            System.IO.Directory.CreateDirectory(filepath);
            fileName = filepath + invoiceno + ".pdf";
            SaveReport(invoiceno, fileName);

        }

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

            string historyID = null;
            string deviceInfo = null;
            string format = "PDF";
            Byte[] results;
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            RE2005.Warning[] warnings = null;
            string[] streamIDs = null;
            string _reportName = @"/GeniusReports/Job/SPM_ServiceReport";

            string _historyID = null;
            bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            RS2005.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
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
                          out extension, out encoding,
                          out mimeType, out warnings, out streamIDs);

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
                throw ex;
            }
            finally
            {
            }
            Console.WriteLine("Report Creation completed successfully");
            Console.WriteLine("Sending email out");
            SendEmail("shail@spm-automation.com", "New Service Report " + invoiceno, invoiceno, fileName);

        }

        #endregion Save Report


        public void SendEmail(string emailTo, string emailSubject, string emailBody, string emailFileAttachments)
        {
            MailMessage message = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("spmautomation-com0i.mail.protection.outlook.com");
            message.From = new MailAddress("connect@spm-automation.com", "SPM Connect");
            System.Net.Mail.Attachment attachment;
            emailTo = emailTo.TrimEnd(';', ',');
            string[] emailArr = emailTo.Split(';', ',');
            foreach (string email in emailArr)
            {
                if (string.IsNullOrEmpty(email) == false)
                {
                    message.To.Add(email);
                }
            }

            message.Subject = emailSubject;
            if (emailFileAttachments == "")
            {
            }
            else
            {
                attachment = new System.Net.Mail.Attachment(emailFileAttachments);
                message.Attachments.Add(attachment);
            }


            message.Body = emailBody;

            //Save the email message to the Drafts folder (where it can be retrieved, updated, and sent at a later time).

            try
            {
                SmtpServer.Port = 25;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);
                //System.Windows.Forms.MessageBox.Show("SPM Connect ran into error!! Error report has been sent - PLEASE CONTACT " + "shail@spm-automation.com" + "!", "Email sending Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show("FAILED TO SEND EMAIL - PLEASE CONTACT " + "shail@spm-automation.com" + "!", "Email sending failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            Console.WriteLine("Report process completed");
        }
    }
}
