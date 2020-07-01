using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM.WorkOrder
{
    public partial class ScanWO : Form
    {
        private readonly SPMConnectAPI.WorkOrder connectapi = new SPMConnectAPI.WorkOrder();
        private DateTime _lastKeystroke = new DateTime(0);
        private readonly List<char> _barcode = new List<char>(10);
        private int userinputtime = 100;
        private bool developer;
        private log4net.ILog log;

        public ScanWO()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timer1.Start();
            woid_txtbox.Focus();
            userinputtime = connectapi.Getuserinputtime();
            developer = connectapi.ConnectUser.Developer;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Scan Work Order ");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.time_lbl.Text = datetime.ToString();
        }

        private void Showaddedtodg()
        {
            timer3.Stop();
            dataGridView1.Visible = true;
            timer3.Enabled = true;
            timer3.Interval = 5000;
            timer3.Start();
            _ = new DataTable();
            dataGridView1.DataSource = connectapi.ShowWOTrackingStatus(woid_txtbox.Text.Trim());
            dataGridView1.Update();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private Point myOriginalLocation;
        private bool positionLocked;

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

        private void Woid_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check timing (keystrokes within 100 ms)
            TimeSpan elapsed = DateTime.Now - _lastKeystroke;
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
                            Showaddedtodg();
                            woid_txtbox.Clear();
                            ActiveForm.Refresh();
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