using System;
using System.Windows.Forms;
using SPMConnectAPI;

namespace SearchDataSPM
{
    public partial class ReportViewer : Form
    {
        SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();

        public ReportViewer()
        {
            InitializeComponent();
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            connectapi.SPM_Connect(connection);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(reportname == "BOM")
            {
                this.Text = "Bills of Manufacturing - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportBOM();
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            else if(reportname == "SPAREPARTS")
            {
                this.Text = "Spare Parts - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportSpareParts();
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            else if (reportname == "WorkOrder")
            {
                this.Text = "Work Order - " + itemnumber;
                reportViewer1.ServerReport.ReportPath =connectapi.GetReportWorkOrder();
                this.reportViewer1.RefreshReport();
                fillwrokdorderreport();
            }
            else if(reportname == "Purchasereq")
            {
                this.Text = "Purchase Requisition - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportPurchaseReq();
                this.reportViewer1.RefreshReport();
                fillpurchasereq();
            }
            else if (reportname == "ShippingInvCom")
            {
                this.Text = "Shipping Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportShippingInvCom();
                this.reportViewer1.RefreshReport();
                fillshippingcom();
            }
            else if (reportname == "ShippingInvPack")
            {
                this.Text = "Shipping Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportShippingInvPack();
                this.reportViewer1.RefreshReport();
                fillshippingpack();
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
        }

        void fillshippingcom()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pInvno", itemnumber));

            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        void fillshippingpack()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pInvno", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
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
