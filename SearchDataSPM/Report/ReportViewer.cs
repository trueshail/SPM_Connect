using SPMConnectAPI;
using System;
using System.Windows.Forms;

namespace SearchDataSPM.Report
{
    public partial class ReportViewer : Form
    {
        private readonly SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();
        private readonly string itemnumber = "";
        private readonly string PaymentMode = "";
        private readonly string reportname = "";
        private readonly string WONotes = "";
        private readonly string WORelease = "";
        private log4net.ILog log;

        public ReportViewer(string _reportname, string _item, string paymenttype = "", string woreleasetype = "", string wonotes = "")
        {
            InitializeComponent();

            this.reportname = _reportname;
            this.itemnumber = _item;
            this.PaymentMode = paymenttype;
            this.WONotes = wonotes;
            this.WORelease = woreleasetype;
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

        private void Fillbomreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pCode", itemnumber)
            };
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void FillEFTReportParamter()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pCode", itemnumber),
                new Microsoft.Reporting.WinForms.ReportParameter("pTransNo", PaymentMode)
            };
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void FilloneParamter()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pInvno", itemnumber)
            };
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void Fillpurchasereq()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pReqno", itemnumber)
            };
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void FillServiceReportParamter()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection
            {
                new Microsoft.Reporting.WinForms.ReportParameter("ReqNumber", itemnumber)
            };
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void Fillwrokdorderreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pWorkOrder", itemnumber)
            };
            if (WORelease != "")
            {
                reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("BalloonRef", WORelease));
            }
            if (WONotes != "")
            {
                reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("Notes", WONotes));
            }
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (reportname == "BOM")
            {
                this.Text = "Bills of Manufacturing - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportBOM");
                this.reportViewer1.RefreshReport();
                Fillbomreport();
            }
            else if (reportname == "SPAREPARTS")
            {
                this.Text = "Spare Parts - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportSpareParts");
                this.reportViewer1.RefreshReport();
                Fillbomreport();
            }
            else if (reportname == "WorkOrder")
            {
                this.Text = "Work Order - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportWorkOrder");
                this.reportViewer1.RefreshReport();
                Fillwrokdorderreport();
            }
            else if (reportname == "Purchasereq")
            {
                this.Text = "Purchase Requisition - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportPurchaseReq");
                this.reportViewer1.RefreshReport();
                Fillpurchasereq();
            }
            else if (reportname == "ShippingInvCom")
            {
                this.Text = "Shipping Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportShippingCommercial");
                this.reportViewer1.RefreshReport();
                FilloneParamter();
            }
            else if (reportname == "ShippingInvPack")
            {
                this.Text = "Shipping Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportShippingPacking");
                this.reportViewer1.RefreshReport();
                FilloneParamter();
            }
            else if (reportname == "MatReAloc")
            {
                this.Text = "Material Re-Allocation Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportMatReAlloc");
                this.reportViewer1.RefreshReport();
                FilloneParamter();
            }
            else if (reportname == "ECR")
            {
                this.Text = "ECR Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportECR");
                this.reportViewer1.RefreshReport();
                FilloneParamter();
            }
            else if (reportname == "Service")
            {
                this.Text = "Service Report - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportService");
                this.reportViewer1.RefreshReport();
                FillServiceReportParamter();
            }
            else if (reportname == "EFT")
            {
                this.Text = "Service Report - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetConnectParameterValue("ReportEFT");
                this.reportViewer1.RefreshReport();
                FillEFTReportParamter();
            }

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened ReportViewer, ReportName : " + reportname + ", ItemNumber : " + itemnumber + " ");
        }

        private void ReportViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed ReportViewer, ReportName : " + reportname + ", ItemNumber : " + itemnumber + " ");
            this.Dispose();
        }
    }
}