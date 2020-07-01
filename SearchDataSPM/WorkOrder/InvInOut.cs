using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.WorkOrder
{
    public partial class InvInOut : Form
    {
        private readonly List<char> _barcode = new List<char>(10);
        private readonly SPMConnectAPI.WorkOrder connectapi = new SPMConnectAPI.WorkOrder();
        private readonly int userinputtime = 100;
        private DateTime _lastKeystroke = new DateTime(0);
        private bool credentialsverified;
        private bool developer;
        private log4net.ILog log;

        public InvInOut()
        {
            InitializeComponent();
            timer1.Start();
            //connectapi.SPM_Connect();
            userinputtime = connectapi.Getuserinputtime();
        }

        private void Apprvlidtxt_Click(object sender, EventArgs e)
        {
            if (!apprvlidtxt.Focused)
            {
                apprvlidtxt.Focus();
            }
        }

        private void Apprvlidtxt_KeyPress(object sender, KeyPressEventArgs e)
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
                        Performapprovedbutton();
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    apprvlidtxt.Clear();
                    apprvlidtxt.Focus();
                }
            }
        }

        private void CheckInWorkOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanWO scanWO = new ScanWO();
            scanWO.Show();
        }

        private void Clearresettxtboxes()
        {
            empid_txtbox.Clear();
            woid_txtbox.Clear();
            apprvlidtxt.Clear();
            woid_txtbox.Enabled = false;
            apprvlidtxt.Enabled = false;
            empid_txtbox.Enabled = true;
            empid_txtbox.Focus();
        }

        private void CreateNewRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new material reallocation invoice?", "SPM Connect - Create New Invoice?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Enabled = false;

                string status = connectapi.CreateNewMatReallocation();
                if (status.Length > 1)
                {
                    Showshippinginvoice(status);
                }
            }
        }

        private void Empid_txtbox_Click(object sender, EventArgs e)
        {
            if (!empid_txtbox.Focused)
            {
                empid_txtbox.Focus();
            }
        }

        private void Empid_txtbox_KeyPress(object sender, KeyPressEventArgs e)
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
                        //empid_txtbox.Text = empid_txtbox.Text.Substring(empid_txtbox.Text.Length - 2);
                        if (connectapi.EmployeeExits(empid_txtbox.Text.Trim()))
                        {
                            woid_txtbox.Enabled = true;
                            woid_txtbox.Focus();
                            empid_txtbox.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Employee not found. Please contact the admin", "SPM Connect - Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            empid_txtbox.Clear();
                            empid_txtbox.Focus();
                            woid_txtbox.Enabled = false;
                        }
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empid_txtbox.Clear();
                    empid_txtbox.Focus();
                    woid_txtbox.Enabled = false;
                }
            }
        }

        private void Empid_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (empid_txtbox.Text.Length == 0)
                woid_txtbox.Enabled = false;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            empid_txtbox.Focus();
            versionlabel.Text = Getassyversionnumber();
            developer = connectapi.ConnectUser.Developer;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Crib Management ");
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void InventoryBinStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinLog binLog = new BinLog();
            binLog.Show();
        }

        private void InvInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Crib Management ");
            this.Dispose();
        }

        private void Performapprovedbutton()
        {
            if (connectapi.EmployeeExits(apprvlidtxt.Text.Trim()))
            {
                if (connectapi.EmployeeExitsWithCribRights(apprvlidtxt.Text.Trim()))
                {
                    if (empid_txtbox.Text.Trim() == apprvlidtxt.Text.Trim())
                    {
                        if (connectapi.ConnectUser.Dept == Department.Crib)
                            connectapi.Scanworkorder(woid_txtbox.Text.Trim());
                        else MessageBox.Show("Please ask the admin to set you up on Department == Crib", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (connectapi.CheckWOIntoCrib(woid_txtbox.Text.Trim()))
                        {
                            if (connectapi.CheckWoExistsOnInvInOut(woid_txtbox.Text.Trim()))
                            {
                                //work order is already built
                                if (connectapi.IsCompletedInvInOut(woid_txtbox.Text.Trim()))
                                {
                                    MessageBox.Show("Work order has been already closed.", "SPM Connect - WO Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    //updates
                                    if (connectapi.InBuiltInvInOut(woid_txtbox.Text.Trim()))
                                    {
                                        DialogResult result = MessageBox.Show("Is this work order has completed build?", "WO Complete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (result == DialogResult.Yes)
                                        {
                                            connectapi.CheckWOInFromBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim(), "1");
                                            connectapi.Scanworkorder(woid_txtbox.Text.Trim());
                                            Showaddedtodg();
                                        }
                                        else if (result == DialogResult.No)
                                        {
                                            string location = connectapi.CaptureLocation();
                                            connectapi.CheckWOInFromBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim(), "0");
                                            connectapi.UpdateWOBinCribLocation(woid_txtbox.Text.Trim(), location);
                                            Showaddedtodg();
                                        }
                                    }
                                    else
                                    {
                                        DialogResult result = MessageBox.Show("Check out this work order from crib to built?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (result == DialogResult.Yes)
                                        {
                                            string location = connectapi.CaptureLocation();
                                            connectapi.ChekOutWOOutForBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim(), location);
                                            Showaddedtodg();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Check out this work order from crib to built?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    string location = connectapi.CaptureLocation();
                                    connectapi.ChekOutWOOutForBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim(), location);
                                    Showaddedtodg();
                                }
                            }
                        }
                        else
                        {
                            //work order not checked into crib
                            MessageBox.Show("Please check in the work order into crib, before assigning out for built.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    Clearresettxtboxes();
                }
                else
                {
                    MessageBox.Show("Your request for checking in/out work order can't be completed based on your security settings", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    apprvlidtxt.Clear();
                    apprvlidtxt.Focus();
                }
            }
            else
            {
                MessageBox.Show("Employee not found. Please contact the admin", "SPM Connect - Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Clearresettxtboxes();
            }
        }

        private void Performlockdown()
        {
            credentialsverified = false;
            toolStripDropDownButton1.DropDownItems[0].Enabled = false;
            toolStripDropDownButton1.DropDownItems[1].Enabled = false;
            toolStripDropDownButton1.DropDownItems[2].Enabled = false;
            toolStripDropDownButton1.DropDownItems[3].Enabled = false;
        }

        private void Showaddedtodg()
        {
            timer3.Stop();
            dataGridView1.Visible = true;
            timer3.Enabled = true;
            timer3.Interval = 10000;
            timer3.Start();
            _ = new DataTable();
            dataGridView1.DataSource = connectapi.ShowWOInOutStatus(woid_txtbox.Text.Trim());
            dataGridView1.Update();
        }

        private void ShowAllRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatReAllocHome matReAllocHome = new MatReAllocHome();
            matReAllocHome.Show();
        }

        private void Showshippinginvoice(string invoice)
        {
            using (MatReAlloc matReAlloc = new MatReAlloc(invoice))
            {
                matReAlloc.ShowDialog();
                this.Enabled = true;
                this.Show();
                this.Activate();
                this.Focus();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            DateTimeLbl.Text = datetime.ToString();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void ToolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            Verifysecurity();
        }

        private void ToolStripDropDownButton1_DropDownClosed(object sender, EventArgs e)
        {
            Performlockdown();
        }

        private void ToolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
        {
            Verifysecurity();
        }

        private void Verifysecurity()
        {
            if (!credentialsverified)
            {
                string ValueIWantFromProptForm = "";
                this.Enabled = false;
                ScanEmpId invoiceFor = new ScanEmpId();
                if (invoiceFor.ShowDialog() == DialogResult.OK)
                {
                    ValueIWantFromProptForm = invoiceFor.ValueIWant;
                }
                if (ValueIWantFromProptForm.Length > 0)
                {
                    if (connectapi.EmployeeExitsWithCribRights(ValueIWantFromProptForm))
                    {
                        credentialsverified = true;
                        toolStripDropDownButton1.DropDownItems[0].Enabled = true;
                        toolStripDropDownButton1.DropDownItems[1].Enabled = true;
                        toolStripDropDownButton1.DropDownItems[2].Enabled = true;
                        toolStripDropDownButton1.DropDownItems[3].Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Your request for gaining access to inventory options can't be completed based on your security settings.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Please try again. Employee not found.", "SPM Connect - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Enabled = true;
            }
        }

        private void Woid_txtbox_Click(object sender, EventArgs e)
        {
            if (!woid_txtbox.Focused)
            {
                woid_txtbox.Focus();
            }
        }

        private void Woid_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {  // check timing (keystrokes within 100 ms)
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
                            apprvlidtxt.Enabled = true;
                            apprvlidtxt.Focus();
                            woid_txtbox.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Please check the work order number.", "SPM Connect - Work Order Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            woid_txtbox.Clear();
                            woid_txtbox.Focus();
                            apprvlidtxt.Enabled = false;
                        }

                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    woid_txtbox.Clear();
                    woid_txtbox.Focus();
                    apprvlidtxt.Enabled = false;
                }
            }
        }

        private void Woid_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (empid_txtbox.Text.Length == 0)
                apprvlidtxt.Enabled = false;
        }
    }
}