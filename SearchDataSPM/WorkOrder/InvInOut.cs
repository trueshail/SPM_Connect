using SPMConnectAPI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class InvInOut : Form
    {
        WorkOrder connectapi = new WorkOrder();
        bool credentialsverified = false;

        public InvInOut()
        {
            InitializeComponent();
            timer1.Start();
            connectapi.SPM_Connect();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            empid_txtbox.Focus();
            versionlabel.Text = connectapi.getassyversionnumber();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            DateTimeLbl.Text = datetime.ToString();
        }

        private void woid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
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
                e.SuppressKeyPress = true;
            }
        }

        private void empid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
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
                e.SuppressKeyPress = true;
            }
        }

        private void empid_txtbox_Click(object sender, EventArgs e)
        {
            if (!empid_txtbox.Focused)
            {
                empid_txtbox.Focus();
            }

        }

        private void woid_txtbox_Click(object sender, EventArgs e)
        {
            if (!woid_txtbox.Focused)
            {
                woid_txtbox.Focus();
            }
        }

        private void empid_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (empid_txtbox.Text.Length == 0)
                woid_txtbox.Enabled = false;
        }

        private void showaddedtodg()
        {
            dataGridView1.Visible = true;
            timer3.Enabled = true;
            timer3.Interval = 10000;
            timer3.Start();            
            DataTable dtb1 = new DataTable();           
            dataGridView1.DataSource = dtb1;
            dataGridView1.Update();

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void apprvlidtxt_Click(object sender, EventArgs e)
        {
            if (!apprvlidtxt.Focused)
            {
                apprvlidtxt.Focus();
            }
        }

        private void woid_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (empid_txtbox.Text.Length == 0)
                apprvlidtxt.Enabled = false;
        }

        private void apprvlidtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (connectapi.EmployeeExits(apprvlidtxt.Text.Trim()))
                {
                    if (connectapi.EmployeeExitsWithCribRights(apprvlidtxt.Text.Trim()))
                    {
                        if (empid_txtbox.Text.Trim() == apprvlidtxt.Text.Trim())
                        {
                            connectapi.scanworkorder(woid_txtbox.Text.Trim());
                        }
                        else
                        {
                            if (connectapi.CheckWOIntoCrib(woid_txtbox.Text.Trim()))
                            {
                                if (connectapi.CheckWoExistsOnInvInOut(woid_txtbox.Text.Trim()))
                                {
                                    //work order is already built
                                    if (connectapi.IsCompletedInvInOut(woid_txtbox.Text.Trim()))
                                        MessageBox.Show("Work order has been already closed.", "SPM Connect - WO Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                    {
                                        //updates
                                        if (connectapi.InBuiltInvInOut(woid_txtbox.Text.Trim()))
                                        {
                                            DialogResult result = MessageBox.Show("Is this work order completed build?", "WO Complete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                connectapi.CheckWOInFromBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim(),"1");
                                                connectapi.scanworkorder(woid_txtbox.Text.Trim());
                                            }
                                            else if(result == DialogResult.No)
                                            {
                                                connectapi.CheckWOInFromBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim(), "0");
                                            }
                                        }
                                        else
                                        {
                                            DialogResult result = MessageBox.Show("Check out this work order from crib to built?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                connectapi.ChekOutWOOutForBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show("Check out this work order from crib to built?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        connectapi.ChekOutWOOutForBuilt(woid_txtbox.Text.Trim(), empid_txtbox.Text.Trim(), apprvlidtxt.Text.Trim());
                                    }
                                }
                            }
                            else
                            {
                                //work order not checked into crib
                                MessageBox.Show("Please check in the work order into crib, before assigning out for built.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        clearresettxtboxes();
                    }
                    else
                    {
                        MessageBox.Show("Your request for checking in work order can't be completed based on your security settings", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        apprvlidtxt.Clear();
                        apprvlidtxt.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Employee not found. Please contact the admin", "SPM Connect - Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    clearresettxtboxes();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        
        private void clearresettxtboxes()
        {
            empid_txtbox.Clear();
            woid_txtbox.Clear();
            apprvlidtxt.Clear();
            woid_txtbox.Enabled = false;
            apprvlidtxt.Enabled = false;
            empid_txtbox.Enabled = true;
            empid_txtbox.Focus();
        }

        private void checkInWorkOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanWO scanWO = new ScanWO();
            scanWO.Show();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            if (credentialsverified)
            {

            }
            else
            {
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                if (connectapi.EmployeeExitsWithCribRights("73"))
                {
                    credentialsverified = true;
                }
            }

        }

    }
}

