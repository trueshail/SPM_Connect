using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
                reportViewer1.ServerReport.ReportPath = "/GeniusReports/BillOfManufacturing/SpareParts";
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            else if (reportname == "WorkOrder")
            {
                this.Text = "Work Order - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = "/GeniusReports/WorkOrder/SPM_WorkOrder";
                this.reportViewer1.RefreshReport();
                fillwrokdorderreport();
            }
            else if(reportname == "Purchasereq")
            {
                this.Text = "Purchase Requisition - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = "/GeniusReports/PurchaseOrder/SPM_PurchaseReq";
                this.reportViewer1.RefreshReport();
                fillpurchasereq();
            }
        }

        string itemnumber = "";
        string reportname = "";
        string totalvalue = "";

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

        public string gettotal(string total)
        {
            if (total.Length > 0)
                return totalvalue = total;
            return null;
        }

        void fillbomreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pCode", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        void fillwrokdorderreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pWorkOrder", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        void fillpurchasereq()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pReqno", itemnumber));
            
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();


            System.Net.WebRequest request = System.Net.HttpWebRequest.Create("http://spm-sql/ReportServer/Pages/ReportViewer.aspx?%2fGeniusReports%2fPurchaseOrder%2fSPM_PurchaseReq&pReqno=1008&rs:Command=Render&rs:Format=PDF");

            request.UseDefaultCredentials = true;

            request.PreAuthenticate = true;

            request.Credentials = CredentialCache.DefaultCredentials;

            System.Net.WebResponse response = request.GetResponse();
            System.Diagnostics.Process.Start("http://spm-sql/ReportServer/Pages/ReportViewer.aspx?%2fGeniusReports%2fPurchaseOrder%2fSPM_PurchaseReq&pReqno=1008&rs:Command=Render&rs:Format=PDF");

            //using (var client = new WebClient())
            //{
            //   System.Diagnostics.Process.Start("http://spm-sql/ReportServer/Pages/ReportViewer.aspx?%2fGeniusReports%2fPurchaseOrder%2fSPM_PurchaseReq&pReqno=1008&rs:Command=Render&rs:Format=PDF", "SPM_PurchaseReq.pdf");
            //}

        }

        private void ReportViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.P))
            {
                reportViewer1.PrintDialog();               
                return true;
            }
            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
