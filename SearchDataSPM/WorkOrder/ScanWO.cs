using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ScanWO : Form
    {
        private WorkOrder connectapi = new WorkOrder();
        private DateTime _lastKeystroke = new DateTime(0);
        private List<char> _barcode = new List<char>(10);
        private int userinputtime = 100;
        private bool developer = false;
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public ScanWO()
        {
            InitializeComponent();
            //connectapi.SPM_Connect();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timer1.Start();
            woid_txtbox.Focus();
            userinputtime = connectapi.Getuserinputtime();
            developer = connectapi.Checkdeveloper();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Scan Work Order ");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.time_lbl.Text = datetime.ToString();
        }

        private void showaddedtodg()
        {
            timer3.Stop();
            dataGridView1.Visible = true;
            timer3.Enabled = true;
            timer3.Interval = 5000;
            timer3.Start();
            DataTable dtb1 = new DataTable();
            dtb1 = connectapi.ShowWOTrackingStatus(woid_txtbox.Text.Trim());
            dataGridView1.DataSource = dtb1;
            dataGridView1.Update();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private Point myOriginalLocation;
        private bool positionLocked = false;

        private void ScanWO_Move(object sender, EventArgs e)
        {
            if (positionLocked)
            {
                this.Location = myOriginalLocation;
            }
        }

        private void ScanWO_Activated(object sender, EventArgs e)
        {
            if (!positionLocked)
            {
                myOriginalLocation = this.Location;
                positionLocked = true;
            }
        }

        private void woid_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check timing (keystrokes within 100 ms)
            TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            if (elapsed.TotalMilliseconds > userinputtime)
                _barcode.Clear();

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
                        if (connectapi.WOReleased(woid_txtbox.Text.Trim()))
                        {
                            connectapi.Scanworkorder(woid_txtbox.Text.Trim());
                            showaddedtodg();
                            woid_txtbox.Clear();
                            ScanWO.ActiveForm.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Please check the work order number.", "SPM Connet - Work Order Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            woid_txtbox.Clear();
                            woid_txtbox.Focus();
                        }

                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    woid_txtbox.Clear();
                    woid_txtbox.Focus();
                }
            }
        }

        private void ScanWO_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Scan Work Order ");
            this.Dispose();
        }
    }
}