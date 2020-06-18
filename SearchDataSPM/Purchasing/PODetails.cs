using SPMConnectAPI;
using System;
using System.Windows.Forms;

namespace SearchDataSPM.Purchasing
{
    public partial class PODetails : Form
    {
        private log4net.ILog log;

        public PODetails()
        {
            InitializeComponent();
        }

        public string Podate { get; set; }
        public string ValueIWant { get; set; }

        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ImportFileSelector_Load(object sender, EventArgs e)
        {
            WinTopMost.SetWindowPos(this.Handle, WinTopMost.HWND_TOPMOST, 0, 0, 0, 0, WinTopMost.TOPMOST_FLAGS);
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Enter PO Details For PurchaseReq ");
        }

        private void PODetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Enter PO Details For PurchaseReq ");
            this.Dispose();
        }

        private void PODetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ponumbertxt.Text.Length == 0) e.Cancel = true;
        }

        private void Ponumbertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void Savebttn_Click(object sender, EventArgs e)
        {
            ValueIWant = ponumbertxt.Text.Trim();
            Podate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}