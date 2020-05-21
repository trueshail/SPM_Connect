using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SearchDataSPM.WorkOrder
{
    public partial class ScanEmpId : MetroFramework.Forms.MetroForm
    {
        private readonly List<char> _barcode = new List<char>(10);
        private bool developer;
        private log4net.ILog log;

        public ScanEmpId()
        {
            InitializeComponent();
            //connectapi.SPM_Connect();
        }

        public string ValueIWant { get; set; }

        private void empid_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check timing (keystrokes within 100 ms)
            //TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            //if (elapsed.TotalMilliseconds > userinputtime)
            //    _barcode.Clear();

            // record keystroke & timestamp
            _barcode.Add(e.KeyChar);

            // process bar code
            if (e.KeyChar == 13 && _barcode.Count > 0)
            {
                string msg = new string(_barcode.ToArray());

                _barcode.Clear();
                if (msg != "\r" || developer)
                {
                    if (e.KeyChar == 13)
                    {
                        ValueIWant = empid_txtbox.Text.Trim();
                        this.DialogResult = DialogResult.OK;
                        Close();
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with bar code reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empid_txtbox.Clear();
                    empid_txtbox.Focus();
                }
            }
        }

        private void JobType_Load(object sender, EventArgs e)
        {
            empid_txtbox.Focus();
            developer = ConnectUser.Developer;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Scan Emp ID ");
        }

        private void ScanEmpId_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Scan Emp ID ");
            this.Dispose();
        }

        private void ScanEmpId_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (empid_txtbox.Text.Length == 0)
            {
                // e.Cancel = true;
            }
        }
    }
}