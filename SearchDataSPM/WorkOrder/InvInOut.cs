using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class InvInOut : Form
    {
        public InvInOut()
        {
            InitializeComponent();
            timer1.Start();
            connectionstring = "Data Source=tcp:DESKTOP-8N7AJNL,49172;Initial Catalog=InventoryManagement;Integrated Security=True";
            imcn = new SqlConnection(connectionstring);
            DataTable dt = new DataTable();
        }

        SqlConnection imcn;
        string connectionstring;

     
        private void Home_Load(object sender, EventArgs e)
        {
            empid_txtbox.Focus();
            woid_txtbox.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.time_lbl.Text = datetime.ToString();
        }

        private void woid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                if (woid_txtbox.Text.Length == 5)
                {
                    if (imcn.State == ConnectionState.Closed)
                        imcn.Open();

                    string query = "select * from dimWorkOrders where WorkOrderId ='" + woid_txtbox.Text.Trim() + "'";

                    SqlDataAdapter sda = new SqlDataAdapter(query, imcn);

                    DataTable dtb1 = new DataTable();

                    sda.Fill(dtb1);

                    if (dtb1.Rows.Count == 1)

                    {
                        //createnewentry();
                        alreadystrted();
                        //MessageBox.Show("New Entry Created");
                        showaddedtodg();
                        empid_txtbox.Clear();
                        woid_txtbox.Clear();
                        InvInOut.ActiveForm.Refresh();
                        empid_txtbox.Focus();
                        woid_txtbox.Enabled = false;
                    }

                    else

                    {
                        MessageBox.Show("Work Order not found!!");
                        woid_txtbox.Clear();
                        woid_txtbox.Focus();
                    }

                }
                else
                {
                    woid_txtbox.Clear();
                    woid_txtbox.Focus();
                }
            }

        }

        private void Connect_SPMSQL()

        {
            try
            {

                SqlConnection imcn = new SqlConnection(connectionstring);

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void empid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (empid_txtbox.Text.Length == 2)
                {
                    if (imcn.State == ConnectionState.Closed)
                        imcn.Open();

                    string query = "select * from Employee where EmpId ='" + empid_txtbox.Text.Trim() + "'";

                    SqlDataAdapter sda = new SqlDataAdapter(query, imcn);

                    DataTable dtb1 = new DataTable();

                    sda.Fill(dtb1);

                    if (dtb1.Rows.Count == 1)

                    {
                        woid_txtbox.Enabled = true;
                        woid_txtbox.Focus();
                      
                    }

                    else

                    {
                        MessageBox.Show("Employee not found!!");
                        empid_txtbox.Clear();
                        empid_txtbox.Focus();
                        woid_txtbox.Enabled = false;
                    }
                }
                else
                {
                    empid_txtbox.Clear();
                    empid_txtbox.Focus();
                    woid_txtbox.Enabled = false;
                }
            }
        }

        private void empid_txtbox_Click(object sender, EventArgs e)
        {
            if (empid_txtbox.Focused)
            {
                empid_txtbox.Focus();
            }

        }

        private void woid_txtbox_Click(object sender, EventArgs e)
        {
            if (woid_txtbox.Focused)
            {
                woid_txtbox.Focus();
            }
        }

        private void empid_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (empid_txtbox.Text.Length == 0)
                woid_txtbox.Enabled = false;
        }

        private void createnewentry()
        {
            DateTime punchin = new DateTime();
            punchin = DateTime.Now;
            if (imcn.State == ConnectionState.Closed)
                imcn.Open();

            string query = "insert into dbo.WOStatus(WorkOderNo,EmpId,PunchInTime)values('" + woid_txtbox.Text.Trim() + "', '" + empid_txtbox.Text.Trim() + "','" + punchin + "')";

            SqlDataAdapter sda = new SqlDataAdapter(query, imcn);
            DataTable dtb1 = new DataTable();
            sda.Fill(dtb1);
            dataGridView1.DataSource = dtb1;
        }

        private void alreadystrted()
        {
            if (imcn.State == ConnectionState.Closed)
                imcn.Open();
            string query = "select * from WOStatus where (isnull(PunchOutTime, '') = '' and WorkOderNo ='" + woid_txtbox.Text.Trim() + "' and EmpId ='" + empid_txtbox.Text.Trim() + "')";

            SqlDataAdapter sda = new SqlDataAdapter(query, imcn);

            DataTable dtb1 = new DataTable();

            sda.Fill(dtb1);

            if (dtb1.Rows.Count == 1)
            {

                DateTime punchout = new DateTime();
                punchout = DateTime.Now;
                if (imcn.State == ConnectionState.Closed)
                    imcn.Open();


                string query2 = "Update dbo.WOStatus set PunchOutTime ='" + punchout + "' where WorkOderNo = '" + woid_txtbox.Text.Trim() + "'";

                SqlDataAdapter sda2 = new SqlDataAdapter(query2, imcn);
                DataTable dtb2 = new DataTable();
                sda2.Fill(dtb2);
                dataGridView1.DataSource = dtb2;
                MessageBox.Show("Closed");
                showaddedtodg();
            }
            else
            {
                createnewentry();

            }

        }

        private void showaddedtodg()
        {
            dataGridView1.Visible = true;
            timer3.Enabled = true;
            timer3.Interval = 10000;
            timer3.Start();

            if (imcn.State == ConnectionState.Closed)
                imcn.Open();

            string query = "select TOP 1 * from invstatus ORDER BY Id Desc ";

            SqlDataAdapter sda = new SqlDataAdapter(query, imcn);
            DataTable dtb1 = new DataTable();

            sda.Fill(dtb1);
            dataGridView1.DataSource = dtb1;
            dataGridView1.Update();

        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void woid_txtbox_Leave(object sender, EventArgs e)
        {
            woid_txtbox.Clear();
        }
    }
}

