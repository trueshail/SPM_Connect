using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ScanEmpId : MetroFramework.Forms.MetroForm
    {
        private DateTime _lastKeystroke = new DateTime(0);
        private List<char> _barcode = new List<char>(10);
        private WorkOrder connectapi = new WorkOrder();
        private int userinputtime = 100;
        private bool developer = false;
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public ScanEmpId()
        {
            InitializeComponent();
            //connectapi.SPM_Connect();
        }

        public string ValueIWant { get; set; }

        private void JobType_Load(object sender, EventArgs e)
        {
            empid_txtbox.Focus();
            userinputtime = connectapi.Getuserinputtime();
            developer = connectapi.user.Developer;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Scan Emp ID ");
        }

        private void empid_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check timing (keystrokes within 100 ms)
            //TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            //if (elapsed.TotalMilliseconds > userinputtime)
            //    _barcode.Clear();

            // record keystroke & timestamp
            _barcode.Add(e.KeyChar);
            _lastKeystroke = DateTime.Now;

            // process barcode
            if (e.KeyChar == 13 && _barcode.Count > 0)
            {
                string msg = new string(_barcode.ToArray());

                _barcode.Clear();
                if (msg != "\r" || developer)
                {
                    if (e.KeyChar == 13)
                    {
                        ValueIWant = empid_txtbox.Text.Trim();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        Close();
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empid_txtbox.Clear();
                    empid_txtbox.Focus();
                }
            }
        }

        private void ScanEmpId_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(empid_txtbox.Text.Length > 0))
            {
                // e.Cancel = true;
            }
        }

        private void ScanEmpId_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Scan Emp ID ");
            this.Dispose();
        }
    }
}