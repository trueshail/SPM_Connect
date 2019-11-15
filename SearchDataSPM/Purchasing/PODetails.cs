using System;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM.General
{
    public partial class PODetails : Form
    {
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public PODetails()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
        }

        public string ValueIWant { get; set; }
        public string podate { get; set; }

        private void ImportFileSelector_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Enter PO Details For PurchaseReq by " + System.Environment.UserName);
        }

        private void savebttn_Click(object sender, EventArgs e)
        {
            ValueIWant = ponumbertxt.Text.Trim();
            podate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void ponumbertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void PODetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(ponumbertxt.Text.Length > 0)) e.Cancel = true;
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log.Error(sender, t.Exception); errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(sender, (Exception)e.ExceptionObject); errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, this);
        }

        private void PODetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Enter PO Details For PurchaseReq by " + System.Environment.UserName);
            this.Dispose();
        }
    }
}