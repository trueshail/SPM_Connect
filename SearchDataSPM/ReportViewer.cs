using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ReportViewer : Form
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(reportname == "BOM")
            {
                this.Text = "Bills of Manufacturing - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = "/GeniusReports/BillOfManufacturing/BillOfManufacturing";
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            else if(reportname == "SPAREPARTS")
            {
                this.Text = "Spare Parts - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = "/GeniusReports/BillOfManufacturing/stdrptspareparts";
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            
        }

        string itemnumber = "";
        string reportname = "";

        public string item(string item)
        {
            if (item.Length > 0)
                return itemnumber = item;
            return null;
        }

        public string getreport(string report)
        {
            if (report.Length > 0)
                return reportname = report;
            return null;
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

            
        }

        void fillbomreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pCode", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Execute().Wait();

        }

        static async Task Execute()
        {
            //var apiKey = Environment.GetEnvironmentVariable("SPM Connect");
            var client = new SendGridClient("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");
            var from = new EmailAddress("spmconnect@spm-automation.com", "SPM Connect");
            var subject = "SPM Connect Email Testing";
            var to = new EmailAddress("eiwan@spm-automation.com");
            var plainTextContent = "Hello ";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,"");
            var response = client.SendEmailAsync(msg).Result;


        }
    }
}
