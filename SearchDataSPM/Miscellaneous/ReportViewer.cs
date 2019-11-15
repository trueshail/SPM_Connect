using SPMConnectAPI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ReportViewer : Form
    {
        private SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public ReportViewer()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            // connectapi.SPM_Connect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (reportname == "BOM")
            {
                this.Text = "Bills of Manufacturing - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportBOM();
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            else if (reportname == "SPAREPARTS")
            {
                this.Text = "Spare Parts - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportSpareParts();
                this.reportViewer1.RefreshReport();
                fillbomreport();
            }
            else if (reportname == "WorkOrder")
            {
                this.Text = "Work Order - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportWorkOrder();
                this.reportViewer1.RefreshReport();
                fillwrokdorderreport();
            }
            else if (reportname == "Purchasereq")
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
                filloneParamter();
            }
            else if (reportname == "ShippingInvPack")
            {
                this.Text = "Shipping Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportShippingInvPack();
                this.reportViewer1.RefreshReport();
                filloneParamter();
            }
            else if (reportname == "MatReAloc")
            {
                this.Text = "Material Re-Allocation Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportMatReAloc();
                this.reportViewer1.RefreshReport();
                filloneParamter();
            }
            else if (reportname == "ECR")
            {
                this.Text = "ECR Invoice - " + itemnumber;
                reportViewer1.ServerReport.ReportPath = connectapi.GetReportECR();
                this.reportViewer1.RefreshReport();
                filloneParamter();
            }

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened ReportViewer, ReportName : " + reportname + ", ItemNumber : " + itemnumber + " by " + System.Environment.UserName);
        }

        private string itemnumber = "";
        private string reportname = "";
        private string totalvalue = "";

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

        private void fillbomreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pCode", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void fillwrokdorderreport()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pWorkOrder", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void fillpurchasereq()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pReqno", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void filloneParamter()
        {
            Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
            reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("pInvno", itemnumber));
            this.reportViewer1.ServerReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void ReportViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed ReportViewer, ReportName : " + reportname + ", ItemNumber : " + itemnumber + " by " + System.Environment.UserName);
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

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log.Error(sender, t.Exception); errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(sender, (Exception)e.ExceptionObject); errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, this);
        }
    }
}