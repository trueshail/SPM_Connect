using System;
using System.Windows.Forms;

namespace SearchDataSPM.General
{
    public partial class PODetails : Form
    {
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public PODetails()
        {
            InitializeComponent();
        }

        public string ValueIWant { get; set; }
        public string podate { get; set; }

        private void ImportFileSelector_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Enter PO Details For PurchaseReq ");
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

        private void PODetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Enter PO Details For PurchaseReq ");
            this.Dispose();
        }
    }
}