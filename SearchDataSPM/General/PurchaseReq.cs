using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class PurchaseReqform : Form
    {
        #region Setting up Various Variables to Store information

        PurchaseReq model = new PurchaseReq();
        String connection;
        SqlConnection cn;
        SqlDataAdapter _adapter;
        DataTable itemstable = new DataTable();
        DataTable dt;
        bool formloading = false;
        bool supervisor = false;
        int supervisorid = 0;
        int myid = 0;
        string userfullname = "";
        List<string> Itemstodiscard = new List<string>();

        #endregion

        #region Form Loading

        public PurchaseReqform()
        {
            InitializeComponent();

            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
                cn.Open();

            }
            catch (Exception)
            {

                // MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect - ENG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                cn.Close();
            }
            dt = new DataTable();
            Clear();
            userfullname = getuserfullname(get_username().ToString()).ToString();
        }

        private void PurchaseReq_Load(object sender, EventArgs e)
        {
            formloading = true;
            if (supervisor)
            {                
                managergroupbox.Visible = true;
                managergroupbox.Enabled = true;

            }
            showReqSearchItems(userfullname);

            formloading = false;

        }

        private void showReqSearchItems(string user)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] Where [RequestedBy] = '" + user + "'ORDER BY ReqNumber DESC", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    preparedatagrid();

                }
                catch (Exception)
                {
                    MessageBox.Show("Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Engineering Load(showallitems)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
        }

        void preparedatagrid()
        {
            dataGridView.DataSource = dt;
            DataView dv = dt.DefaultView;
            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[0].HeaderText = "Req No";
            dataGridView.Columns[1].Width = 60;
            dataGridView.Columns[1].HeaderText = "Job";
            dataGridView.Columns[2].Width = 80;
            dataGridView.Columns[3].Width = 80;
            dataGridView.Columns[4].Visible = false;
            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
            dataGridView.Columns[11].Visible = false;
            dataGridView.Columns[12].Visible = false;
            dataGridView.Columns[13].Visible = false;
            dataGridView.Columns[14].Visible = false;
            dataGridView.Columns[15].Visible = false;
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
            UpdateFont();
        }

        #endregion

        #region show edit button for approved req

        private String getapprovalstatus()
        {
            string approved;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                approved = Convert.ToString(slectedrow.Cells["Approved"].Value);
                //MessageBox.Show(username);
                return approved;
            }
            else
            {
                approved = "";
                return approved;
            }
        }

        private void checkforeditrights()
        {
            if (getapprovalstatus().ToString() == "0")
            {
                editbttn.Visible = true;

            }
            else
            {
                editbttn.Visible = false;
            }
            if (supervisor)
            {
                editbttn.Visible = true;
            }
        }

        #endregion

        #region Get User Full Name

        private String get_username()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            this.Text = "SPM Connect Purchase Requisition - " + userName.Substring(4);
            if (userName.Length > 0)
            {

                return userName;
            }
            else
            {
                return null;
            }

        }

        private string getuserfullname(string username)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string fullname = dr["Name"].ToString();
                    supervisorid = Convert.ToInt32(dr["Supervisor"].ToString());
                    myid = Convert.ToInt32(dr["id"].ToString());
                    string manager = dr["PurchaseReqApproval"].ToString();
                    if (manager == "1")
                    {
                        supervisor = true;
                    }
                    return fullname;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        #endregion

        #region Purchase Req Item Search

        private void PurchaseReqSearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {

                if (PurchaseReqSearchTxt.Text.Length > 0)
                {
                    mainsearch();
                }
                else
                {
                    dataGridView.DataSource = null;
                    dataGridView.Refresh();
                    showReqSearchItems(userfullname);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void mainsearch()
        {

            try
            {
                DataView dv = dt.DefaultView;
                dt = dv.ToTable();
                //dv = new DataView(ds.Tables[0], "RequistionNo = '" + search1 + "' ", "RequistionNo Desc", DataViewRowState.CurrentRows);
                dv.RowFilter = string.Format("ReqNumber = {0}", PurchaseReqSearchTxt.Text);
                dataGridView.DataSource = dv;
                dataGridView.Update();
                dataGridView.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                PurchaseReqSearchTxt.Clear();

            }
        }

        #endregion

        #region Create New Purchase Req

        void createnew()
        {
            DialogResult result = MessageBox.Show("Are you sure want to create a new purchase req?", "SPM Connect - Create New?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //clearitemsbeforenewreq();
                //Clear();
                dateTimePicker1.MinDate = DateTime.Today;
                ecitbttn.Visible = false;
                int lastreq = getlastreqnumber();
                createnewreq(lastreq, userfullname.ToString());
                showReqSearchItems(userfullname);
                selectrowbeforeediting(lastreq.ToString());
                populatereqdetails(lastreq);
                PopulateDataGridView();
                processeditbutton(false);
                jobnumbertxt.Text = jobnumbertxt.Text.TrimStart();
                subassytxt.Text = subassytxt.Text.TrimStart();
            }
        }

        void selectrowbeforeediting(string searchValue)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    dataGridView.Rows[rowIndex].Selected = true;

                    break;
                }
            }
        }

        void clearitemsbeforenewreq()
        {
            purchreqtxt.Clear();
            requestbytxt.Clear();
            lastsavedby.Clear();
            datecreatedtxt.Clear();
            jobnumbertxt.Clear();
            subassytxt.Clear();
            pricetxt.Text = "$0.00";
            pricetxt.SelectionStart = pricetxt.Text.Length;
            editbttn.Visible = false;
            dataGridView.Enabled = false;
            dataGridView1.Enabled = true;

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            ecitbttn.Visible = true;
            savebttn.Visible = true;
            groupBox3.Visible = true;
        }

        private int getlastreqnumber()
        {
            int lastreqnumber = 0;
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(ReqNumber) FROM [SPM_Database].[dbo].[PurchaseReqBase]", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    lastreqnumber = (int)cmd.ExecuteScalar();
                    lastreqnumber++;


                    // MessageBox.Show(lastreqnumber.ToString());
                    return lastreqnumber;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Last Req Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return lastreqnumber;
        }

        private void createnewreq(int reqnumber, string employee)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string jobnumber = "";
            string subassy = "";
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[PurchaseReqBase] (ReqNumber, RequestedBy, DateCreated, DateLastSaved, JobNumber, SubAssyNumber,LastSavedBy, Validate, Approved,Total,DateRequired, SupervisorId, DateValidated) VALUES('" + reqnumber + "','" + employee.ToString() + "','" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + jobnumber + "','" + subassy + "','" + employee.ToString() + "','0','0','0','" + sqlFormattedDate + "', '" + supervisorid + "', null)";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create Entry On SQL Purchase Req Base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        #endregion

        #region Calculate Total

        string totalvalue = "";

        void calculatetota()
        {
            totalvalue = "";
            if (dataGridView1.Rows.Count > 0)
            {
                decimal total = 0.00m;
                int qty = 1;
                decimal price = 0.00m;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[3].Value.ToString().Length > 0 && row.Cells[3].Value.ToString() != null)
                    {
                        qty = Convert.ToInt32(row.Cells[3].Value.ToString());
                    }
                    try
                    {
                        if (row.Cells[7].Value.ToString() != null && row.Cells[7].Value.ToString().Length > 0)
                        {
                            price = Convert.ToDecimal(row.Cells[7].Value.ToString());
                        }
                        else
                        {
                            price = 0;
                        }
                        total += (qty * price);
                        totalcostlbl.Text = "Total Cost : $" + string.Format("{0:n}", Convert.ToDecimal(total.ToString()));

                        totalvalue = string.Format("{0:#.00}", total.ToString());
                    }

                    catch (Exception)
                    {

                    }


                }
            }
            else
            {
                totalcostlbl.Text = "";
            }

        }

        #endregion

        #region Perform CRUD Operations

        private void UpdateReq(int reqnumber, string typesave)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string datereq = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string jobnumber = jobnumbertxt.Text.Trim();
            string subassy = subassytxt.Text.Trim();
            string notes = notestxt.Text;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if(typesave == "Normal")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + userfullname.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if(typesave == "Validated")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + userfullname.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',DateValidated = '" + (Validatechk.Checked ? sqlFormattedDate : null) + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if(typesave == "Approved")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + userfullname.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',ApprovedBy = '" + userfullname + "',DateApproved = '" + (approvechk.Checked ? sqlFormattedDate : "") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if (typesave == "ApprovedFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + userfullname.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',ApprovedBy = ' ',DateApproved = null WHERE ReqNumber = '" + reqnumber + "' ";
                }

                //if (approvechk.Checked)
                //{
                //    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + userfullname.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',DateValidated = '" + (Validatechk.Checked ? sqlFormattedDate : "") + "',ApprovedBy = '" + userfullname + "',DateApproved = '" + (approvechk.Checked ? sqlFormattedDate : "") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                //}
                //else
                //{
                //    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + userfullname.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',DateValidated = '" + (Validatechk.Checked ? sqlFormattedDate : null) + "' WHERE ReqNumber = '" + reqnumber + "' ";
                //}

                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update Entry On SQL Purchase Req Base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            Itemstodiscard.Clear();
            processeditbutton(true);
        }

        void processeditbutton(bool showexit)
        {
            dataGridView1.ContextMenuStrip = FormSelector;
            PurchaseReqSearchTxt.Enabled = false;
            dateTimePicker1.Enabled = true;
            dataGridView.Enabled = false;
            editbttn.Visible = false;
            jobnumbertxt.ReadOnly = false;
            subassytxt.ReadOnly = false;
            notestxt.ReadOnly = false;
            jobnumbertxt.SelectionStart = jobnumbertxt.Text.Length;
            subassytxt.SelectionStart = subassytxt.Text.Length;
            if (Validatechk.Checked)
            {
                groupBox3.Visible = false;
            }
            else
            {
                groupBox3.Visible = true;
            }
            if (supervisor && Validatechk.Checked)
            {
                approvechk.Visible = true;
                approvechk.Enabled = true;

            }
            else
            {
                approvechk.Enabled = false;
                approvechk.Visible = false;
            }
            if (approvechk.Checked)
            {
                Validatechk.Visible = false;
            }

            savebttn.Visible = true;
            Validatechk.Visible = true;
            fillitemssource();
            toolbarpanel.Enabled = false;
            if (showexit)
            {
                ecitbttn.Visible = true;
            }
            dateTimePicker1.MinDate = DateTime.Today;
        }

        private void savebttn_Click(object sender, EventArgs e)
        {
            Itemstodiscard.Clear();
            processsavebutton(false,"Normal");
        }

        void processsavebutton(bool validatehit, string typeofsave)
        {
            errorProvider1.Clear();
            if (validatehit)
            {
                UpdateReq(Convert.ToInt32(purchreqtxt.Text),typeofsave);
                showReqSearchItems(userfullname);
                clearaddnewtextboxes();
                processexitbutton();
            }
            else
            {
                if (jobnumbertxt.Text.Trim().Length > 0 && subassytxt.Text.Trim().Length > 0)
                {
                    UpdateReq(Convert.ToInt32(purchreqtxt.Text),typeofsave);
                    showReqSearchItems(userfullname);
                    clearaddnewtextboxes();
                    processexitbutton();

                }
                else
                {
                    errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                    errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                    //MessageBox.Show("Quantity cannot be empty in order to add new entry.", "SPM Connect - Add New Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }

            dateTimePicker1.MinDate = new DateTime(2012, 05, 28);
        }

        private void ecitbttn_Click(object sender, EventArgs e)
        {
            if (savebttn.Visible == true)
            {
                errorProvider1.SetError(savebttn, "Save before closing");
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    errorProvider1.Clear();
                    performdiscarditem();
                    updateorderid(Convert.ToInt32(purchreqtxt.Text));
                    PopulateDataGridView();
                    Itemstodiscard.Clear();
                    processexitbutton();
                }
                else
                {

                }

            }

        }

        void performdiscarditem()
        {
            foreach (string item in Itemstodiscard)
            {
                splittagtovariables(item);
            }
        }

        private void splittagtovariables(string s)
        {
            string[] values = s.Replace("][", "~").Split('~');
            //string[] values = s.Split('][');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();

            }
            removeitems(values[0], values[1]);
        }

        private void removeitems(string itemno, string description)
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[PurchaseReq] WHERE Item ='" + itemno.ToString() + "' AND ReqNumber ='" + description.ToString() + "' ";
                SqlCommand sda = new SqlCommand(query, cn);
                sda.ExecuteNonQuery();
                cn.Close();
                //MetroFramework.MetroMessageBox.Show(this, itemno + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        void processexitbutton()
        {
            tabControl1.TabPages.Remove(PreviewTabPage);
            jobnumbertxt.ReadOnly = true;
            subassytxt.ReadOnly = true;
            notestxt.ReadOnly = true;
            dataGridView.Enabled = true;
            groupBox3.Visible = false;
            editbttn.Visible = false;
            savebttn.Visible = false;
            dateTimePicker1.Enabled = false;
            ecitbttn.Visible = false;
            tabControl1.Visible = true;
            toolbarpanel.Enabled = true;
            PurchaseReqSearchTxt.Enabled = true;
            dataGridView1.ContextMenuStrip = null;
            Validatechk.Visible = false;
            approvechk.Visible = false;
            approvechk.Enabled = false;
            Clear();
            if (tabControl1.TabPages.Count == 0)
            {
                tabControl1.TabPages.Add(PreviewTabPage);
            }
            dateTimePicker1.MinDate = new DateTime(2018, 01, 01);
        }

        private void updateorderid(int reqnumber)
        {
            //using (SqlCommand sqlCommand = new SqlCommand("[SPM_Database].[dbo].[UpdateOrderId]", cn))
            //{
            //    try
            //    {
            //        cn.Open();
            //        sqlCommand.CommandType = CommandType.StoredProcedure;
            //        sqlCommand.Parameters.AddWithValue("@itemnumber", reqnumber);
            //        sqlCommand.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "SPM Connect - Update Order Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        cn.Close();
            //    }
            //}
            using (SqlCommand sqlCommand = new SqlCommand("with cte as(select *, new_row_id = row_number() over(partition by ReqNumber order by ReqNumber)from[dbo].[PurchaseReq] where ReqNumber = @itemnumber)update cte set OrderId = new_row_id", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@itemnumber", reqnumber);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update Order Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        void processdeletebttn()
        {
            if (MessageBox.Show("Are You Sure to Delete this Record ?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SPM_DatabaseEntitiesPurchase db = new SPM_DatabaseEntitiesPurchase())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                        db.PurchaseReqs.Attach(model);
                    db.PurchaseReqs.Remove(model);
                    db.SaveChanges();
                    updateorderid(Convert.ToInt32(purchreqtxt.Text));
                    PopulateDataGridView();
                    Clear();
                    //MessageBox.Show("Deleted Successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Addnewbttn.Enabled = false;
                ecitbttn.Visible = false;
            }
            else
            {
                
                Clear();
                Addnewbttn.Enabled = false;
            }

        }

        void addnewitemtoreq()
        {
            int resultqty = 0;
            //int result = 0;
            //double price12 = 0.00;
            errorProvider1.Clear();
            if (qtytxt.Text.Length > 0)
            {
                int maxSlNo = dataGridView1.Rows.Count;
                maxSlNo++;
                //int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                //MessageBox.Show(this.dataGridView1.Rows[selectedrowindex].HeaderCell.Value.ToString());
                model.OrderId = maxSlNo;
                model.Item = ItemTxtBox.Text.Trim();
                model.Description = Descriptiontxtbox.Text.Trim();
                model.Manufacturer = oemtxt.Text.Trim();
                model.OEMItemNumber = oemitemnotxt.Text.Trim();

                if (int.TryParse(qtytxt.Text, out resultqty))
                    model.Qty = resultqty;
                model.ReqNumber = Convert.ToInt32(purchreqtxt.Text);
                if (decimal.TryParse(pricetxt.Text.Replace(",", "").Replace("$", ""), out decimal result12))
                    model.Price = result12;
                model.Notes = "";
                using (SPM_DatabaseEntitiesPurchase db = new SPM_DatabaseEntitiesPurchase())
                {
                    if (model.ID == 0)//Insert
                        db.PurchaseReqs.Add(model);
                    else //Update
                        db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }

                Clear();
                updateorderid(Convert.ToInt32(purchreqtxt.Text));
                PopulateDataGridView();
                Addnewbttn.Enabled = false;
                itemsearchtxtbox.Focus();


                string itemsonhold = model.Item + "][" + model.ReqNumber;
                Itemstodiscard.Add(itemsonhold);
                model.Qty = null;
                model.Price = null;
                
            }
            else
            {
                errorProvider1.SetError(qtytxt, "Cannot be null");
                //MessageBox.Show("Quantity cannot be empty in order to add new entry.", "SPM Connect - Add New Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        void Clear()
        {
            itemsearchtxtbox.Clear();
            ItemTxtBox.Clear();
            Descriptiontxtbox.Clear();
            oemtxt.Clear();
            oemitemnotxt.Clear();
            pricetxt.Clear();
            qtytxt.Clear();
            itemsearchtxtbox.Text = ItemTxtBox.Text = Descriptiontxtbox.Text = oemtxt.Text = oemitemnotxt.Text = pricetxt.Text = qtytxt.Text = "";
            Addnewbttn.Text = "Add";
            btnDelete.Enabled = false;
            model.ID = 0;
        }

        #endregion

        #region Fill Items Source for search and add

        private void fillitemssource()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ItemsToSelect]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    itemsearchtxtbox.AutoCompleteCustomSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Items Drop Down Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void filldatatable(string itemnumber)
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[UnionInventory] WHERE [ItemNumber]='" + itemnumber.ToString() + "'";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                _adapter = new SqlDataAdapter(sql, cn);
                itemstable.Clear();
                _adapter.Fill(itemstable);
                fillinfo();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Fill Items Details from Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }
        }

        private void fillinfo()
        {
            DataRow r = itemstable.Rows[0];
            ItemTxtBox.Text = r["ItemNumber"].ToString();
            Descriptiontxtbox.Text = r["Description"].ToString();
            oemtxt.Text = r["Manufacturer"].ToString();
            oemitemnotxt.Text = r["ManufacturerItemNumber"].ToString();
        }

        void clearaddnewtextboxes()
        {
            ///itemsearchtxtbox.Clear();
            Descriptiontxtbox.Clear();
            oemitemnotxt.Clear();
            oemtxt.Clear();
            pricetxt.Clear();
            pricetxt.Text = "$0.00";
            qtytxt.Clear();
        }

        #endregion

        #region Populated Details for both datagrids showing purchase req details

        void populatereqdetails(int item) // populates details of selected purchase req
        {
            try
            {
                DataRow[] dr = dt.Select("ReqNumber = '" + item + "'");
                purchreqtxt.Text = dr[0]["ReqNumber"].ToString();
                requestbytxt.Text = dr[0]["RequestedBy"].ToString();
                datecreatedtxt.Text = dr[0]["DateCreated"].ToString();
                jobnumbertxt.Text = dr[0]["JobNumber"].ToString();
                subassytxt.Text = dr[0]["SubAssyNumber"].ToString();
                lastsavedby.Text = dr[0]["LastSavedBy"].ToString();
                lastsavedtxt.Text = dr[0]["DateLastSaved"].ToString();
                notestxt.Text = dr[0]["Notes"].ToString();
                DateTime dateValue = Convert.ToDateTime(dr[0]["DateRequired"]);
                dateTimePicker1.Value = dateValue;

                approvebylabel.Text = "Approved by : " + dr[0]["Approvedby"].ToString();
                apprvonlabel.Text = "Approved on : " + dr[0]["DateApproved"].ToString();

                if (dr[0]["Validate"].ToString().Equals("1"))
                {
                    Validatechk.Checked = true;
                    Validatechk.Text = "Invalidate";

                }
                else
                {
                    Validatechk.Text = "Validate";
                    Validatechk.Checked = false;
                }
                if (dr[0]["Approved"].ToString().Equals("1") && dr[0]["Validate"].ToString().Equals("1"))
                {
                    approvechk.Text = "Approved";
                    approvechk.Checked = true;
                    approvechk.Visible = true;
                    Validatechk.Visible = false;
                    printbttn.Enabled = true;
                    approvebylabel.Visible = true;
                    apprvonlabel.Visible = true;
                }
                else
                {
                    approvechk.Text = "Approve";
                    approvechk.Checked = false;
                    approvechk.Visible = false;
                    printbttn.Enabled = false;
                    approvebylabel.Visible = false;
                    apprvonlabel.Visible = false;
                }

                if (supervisor && dr[0]["Validate"].ToString().Equals("1"))
                {
                    
                    if (dr[0]["Approved"].ToString().Equals("1"))
                    {
                        approvechk.Text = "Approved";
                        approvechk.Checked = true;
                        approvechk.Visible = true;
                        printbttn.Enabled = true;
                        approvebylabel.Visible = true;
                        apprvonlabel.Visible = true;
                        Validatechk.Visible = false;
                        Validatechk.Enabled = false;
                    }
                    else
                    {
                        approvechk.Text = "Approve";
                        approvechk.Checked = false;
                        approvechk.Visible = true;
                        printbttn.Enabled = false;
                        approvebylabel.Visible = false;
                        apprvonlabel.Visible = false;
                        Validatechk.Enabled = true;
                        Validatechk.Visible = true;

                    }


                }
                else if (supervisor && dr[0]["Validate"].ToString().Equals("0"))
                {
                    approvechk.Text = "Approve";
                    approvechk.Checked = false;
                    approvechk.Visible = false;
                    printbttn.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Populate Req Detauils");
            }

        }

        string reqnumber = "";

        void PopulateDataGridView()
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            int item = Convert.ToInt32(slectedrow.Cells[0].Value);
            using (SPM_DatabaseEntitiesPurchase db = new SPM_DatabaseEntitiesPurchase())
            {
                dataGridView1.DataSource = db.PurchaseReqs.Where(s => s.ReqNumber == item).ToList<PurchaseReq>();
            }
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            //}
            reqnumber = item.ToString();

            PreviewTabPage.Text = "ReqNo : " + item;
            UpdateFontdataitems();
            calculatetota();
        }

        #endregion

        #region Form closing

        private void PurchaseReqform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savebttn.Visible == true)
            {
                errorProvider1.Clear();
                e.Cancel = true;
                if (jobnumbertxt.Text.Trim().Length == 0 && subassytxt.Text.Trim().Length == 0)
                {
                    errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                    errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                }
                else
                {
                    errorProvider1.SetError(savebttn, "Save before closing");
                }

            }
        }

        private void PurchaseReqform_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region Open report viewer

        private void reportpurchaereq(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(itemvalue);
            form1.getreport(Reportname);
            form1.gettotal(totalvalue);
            form1.Show();

        }

        #endregion

        #region Validation Check

        private void Validatechk_Click(object sender, EventArgs e)
        {
            if (Validatechk.Checked == false)
            {
                if (getapprovedstatus(Convert.ToInt32(purchreqtxt.Text)))
                {
                    MessageBox.Show("This purchase requisition is approved. Only supervisor can edit the details.", "SPM Connect - Purchase Req already approved", MessageBoxButtons.OK);
                    Validatechk.Checked = true;
                    Validatechk.Text = "Invalidate";
                    groupBox3.Visible = false;
                    processexitbutton();
                    showReqSearchItems(userfullname);
                }
                else
                {
                    Validatechk.Checked = false;
                    Validatechk.Text = "Validate";
                    groupBox3.Visible = true;

                }

            }
            else
            {

                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this purchase req for order?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string reqno = purchreqtxt.Text;
                    processsavebutton(true, "Validated");
                    Validatechk.Text = "Invalidate";
                    SaveReport(reqno, true, "");
                }
                else
                {
                    Validatechk.Checked = false;
                }

            }

        }

        private bool getapprovedstatus(int reqno)
        {
            bool approved = false;
            try
            {

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] where ReqNumber ='" + reqno + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string useractiveblock = dr["Approved"].ToString();
                    if (useractiveblock == "1")
                    {
                        approved = true;
                    }
                    else
                    {
                        approved = false;

                    }
                    return approved;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get approval status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();

            }
            finally
            {
                cn.Close();
            }

            return approved;

        }

        private void Validatechk_CheckedChanged(object sender, EventArgs e)
        {
            if (editbttn.Visible || approvechk.Checked)
            {
                groupBox3.Visible = false;
            }
            else
            {
                groupBox3.Visible = false;
                if (Validatechk.Checked)
                {
                    groupBox3.Visible = false;
                }
                else
                {
                    groupBox3.Visible = true;
                    if (supervisor)
                    {
                        approvechk.Visible = false;
                        approvechk.Enabled = false;
                    }

                }
            }


        }

        #endregion

        #region manager approve check changed

        private void approvechk_Click(object sender, EventArgs e)
        {
            if (supervisor)
            {
                if (approvechk.Checked == false)
                {
                    processsavebutton(true,"ApprovedFalse");
                }
                else
                {
                    DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to approve this purchase requistion for order?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string reqno = purchreqtxt.Text;
                        string requestby = requestbytxt.Text;

                        processsavebutton(true,"Approved");
                        approvechk.Checked = true;
                        SaveReport(reqno, false, requestby);

                    }
                    else
                    {
                        approvechk.Checked = false;
                    }

                }


            }
        }

        #endregion

        #region datagridview events

        private void UpdateFont()
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        private void UpdateFontdataitems()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Tomato;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView1.Columns["Price"].DefaultCellStyle.Format = "n2";
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                if (dataGridView.Rows.Count > 0 && dataGridView.SelectedCells.Count == 1)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    dataGridView1.AutoGenerateColumns = false;
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    int item = Convert.ToInt32(slectedrow.Cells[0].Value);


                    populatereqdetails(item);
                    PopulateDataGridView();
                    tabControl1.Visible = true;
                    totalcostlbl.Visible = true;
                    if (tabControl1.TabPages.Count == 0)
                    {
                        tabControl1.TabPages.Add(PreviewTabPage);
                    }
                    checkforeditrights();

                    Cursor.Current = Cursors.Default;
                }

            }



        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGridView.Rows.Count < 1)
            {
                editbttn.Visible = false;
                tabControl1.Visible = false;
                totalcostlbl.Visible = false;
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {

                int columnindex = e.RowIndex;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[columnindex].Selected = true;


            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            getitemsfromgrid();

        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {

                int columnindex = e.RowIndex;
                dataGridView.ClearSelection();
                dataGridView.Rows[columnindex].Selected = true;

            }
        }

        void getitemsfromgrid()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentRow.Index != -1)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];
                    //string Item = Convert.ToString(slectedrow.Cells[0].Value);

                    model.ID = Convert.ToInt32(slectedrow.Cells["ID"].Value);
                    using (SPM_DatabaseEntitiesPurchase db = new SPM_DatabaseEntitiesPurchase())
                    {
                        model = db.PurchaseReqs.Where(x => x.ID == model.ID).FirstOrDefault();
                        ItemTxtBox.Text = model.Item.ToString();
                        Descriptiontxtbox.Text = model.Description;
                        oemtxt.Text = model.Manufacturer;
                        oemitemnotxt.Text = model.OEMItemNumber;
                        pricetxt.Text = String.Format("{0:c2}", model.Price);
                        qtytxt.Text = model.Qty.ToString();

                    }
                    Addnewbttn.Enabled = true;
                    Addnewbttn.Text = "Update";
                    btnDelete.Enabled = true;
                }
            }

        }

        private void FormSelector_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                FormSelector.Items[0].Enabled = true;
                FormSelector.Items[1].Enabled = true;

            }
            else
            {

                FormSelector.Items[0].Enabled = false;
                FormSelector.Items[1].Enabled = false;
            }
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            getitemsfromgrid();
            processdeletebttn();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ecitbttn.Visible = false;
            Clear();
            getitemsfromgrid();
            qtytxt.Focus();
            qtytxt.SelectAll();
            
        }

        #endregion

        #region save report and send email

        public void SaveReport(string reqno, bool prelim, string requestby)
        {

            RS2005.ReportingService2005 rs;
            RE2005.ReportExecutionService rsExec;

            // Create a new proxy to the web service
            rs = new RS2005.ReportingService2005();
            rsExec = new RE2005.ReportExecutionService();

            // Authenticate to the Web service using Windows credentials
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.Url = "http://spm-sql/reportserver/reportservice2005.asmx";
            rsExec.Url = "http://spm-sql/reportserver/reportexecution2005.asmx";

            string historyID = null;
            string deviceInfo = null;
            string format = "PDF";
            Byte[] results;
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            RE2005.Warning[] warnings = null;
            string[] streamIDs = null;

            // Path of the Report - XLS, PDF etc.
            string fileName = "";

            if (prelim)
            {
                fileName = @"\\spm-adfs\SDBASE\Reports\Prelim\" + reqno + ".pdf";
            }
            else
            {
                fileName = @"\\spm-adfs\SDBASE\Reports\Approved\" + reqno + ".pdf";
            }

            // Name of the report - Please note this is not the RDL file.
            string _reportName = @"/GeniusReports/PurchaseOrder/SPM_PurchaseReq";
            string _historyID = null;
            bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            RS2005.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                RE2005.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);
                RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];

                if (_parameters.Length > 0)
                {
                    parameters[0] = new RE2005.ParameterValue();
                    //parameters[0].Label = "";
                    parameters[0].Name = "pReqno";
                    parameters[0].Value = reqno;
                }
                rsExec.SetExecutionParameters(parameters, "en-us");

                results = rsExec.Render(format, deviceInfo,
                          out extension, out encoding,
                          out mimeType, out warnings, out streamIDs);

                //using (FileStream stream = File.Open(fileName,FileMode.Open,FileAccess.Write,FileShare.Read))
                //{
                //    stream.Write(results, 0, results.Length);
                //    stream.Close();
                //}


                try
                {
                    //FileStream stream = File.Create(fileName, results.Length);

                    //stream.Write(results, 0, results.Length);

                    //stream.Close();

                    File.WriteAllBytes(fileName, results);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (prelim)
                {
                    snedemailtosupervisor(reqno, fileName);
                }
                else
                {
                    sendemailtouser(reqno, fileName, requestby);
                }

            }

        }

        void snedemailtosupervisor(string reqno, string fileName)
        {
            string nameemail = getsupervisornameandemail(supervisorid);

            string[] values = nameemail.Replace("][", "~").Split('~');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();

            }
            string email = values[0];
            string name = values[1];

            string[] names = name.Replace(" ", "~").Split('~');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim();

            }
            name = names[0];
            sendemail(email, reqno + " Purchase Req Approval Required", "Hello " + name + "," + Environment.NewLine + userfullname + " sent this purchase req for approval.", fileName);
        }

        void sendemailtouser(string reqno, string fileName, string requestby)
        {
            string email = getusernameandemail(requestby);

            sendemail(email, reqno + " Purchase Req Approved", "Hello " + requestby + "," + Environment.NewLine + " Your purchase req is approved.", fileName);
        }

        private string getsupervisornameandemail(int id)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + id + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string Email = dr["Email"].ToString();
                    string name = dr["Name"].ToString();

                    return Email + "][" + name;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        private string getusernameandemail(string requestby)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [Name]='" + requestby.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string Email = dr["Email"].ToString();
                    return Email;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        void sendemail(string emailtosend, string subject, string body, string filetoattach)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("spmautomation-com0i.mail.protection.outlook.com");
                message.From = new MailAddress("connect@spm-automation.com", "SPM Connect");
                message.To.Add(emailtosend);
                message.Subject = subject;
                message.Body = body;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(filetoattach);
                message.Attachments.Add(attachment);
                SmtpServer.Port = 25;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        #region add items to purchase req button and text events groupbox 3

        private void pricetxt_Leave(object sender, EventArgs e)
        {
            //Double value;
            //if (Double.TryParse(pricetxt.Text, out value))
            //    pricetxt.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            //else
            //    pricetxt.Text = String.Empty;
        }

        private void itemsearchtxtbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                if (itemsearchtxtbox.Text.Length > 6)
                {
                    clearaddnewtextboxes();
                    filldatatable(itemsearchtxtbox.Text.Trim().Substring(0, 6).ToString());
                    Addnewbttn.Enabled = true;
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void pricetxt_TextChanged(object sender, EventArgs e)
        {
            string value = pricetxt.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0');
            decimal ul;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                pricetxt.TextChanged -= pricetxt_TextChanged;
                //Format the text as currency
                pricetxt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                pricetxt.TextChanged += pricetxt_TextChanged;
                pricetxt.Select(pricetxt.Text.Length, 0);
            }
            bool goodToGo = TextisValid(pricetxt.Text);

            if (!goodToGo)
            {
                pricetxt.Text = "$0.00";
                pricetxt.Select(pricetxt.Text.Length, 0);
            }
        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        private void pricetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            //{


            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void qtytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            addnewitemtoreq();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            processdeletebttn();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            Addnewbttn.Enabled = false;
        }

        private void jobnumbertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.

            }
            else
            {
                e.Handled = true;
            }
        }

        private void subassytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }

        private void jobnumbertxt_Leave(object sender, EventArgs e)
        {
            jobnumbertxt.Text = jobnumbertxt.Text.Trim();
        }

        private void subassytxt_Leave(object sender, EventArgs e)
        {
            subassytxt.Text = subassytxt.Text.Trim();
        }


        #endregion

        #region button click events tool bars


        private void newbttn_Click(object sender, EventArgs e)
        {
            createnew();
        }

        private void printbttn_Click(object sender, EventArgs e)
        {
            reportpurchaereq(reqnumber, "Purchasereq");
        }

        private void bttnneedapproval_Click(object sender, EventArgs e)
        {
            showwaitingonapproval();
        }

        private void bttnshowapproved_Click(object sender, EventArgs e)
        {
            showallapproved();
        }

        private void bttnshowmydept_Click(object sender, EventArgs e)
        {
            showmydeptreq();
        }

        private void bttnshowmyreq_Click(object sender, EventArgs e)
        {
            showReqSearchItems(userfullname);
        }

        #endregion

        #region manager commands to retrieve data

        private void showwaitingonapproval()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Approved = '0' AND Validate = '1' AND SupervisorId = '" + myid + "' ORDER BY ReqNumber DESC", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    preparedatagrid();


                }
                catch (Exception)
                {
                    MessageBox.Show("Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Engineering Load(showallitems)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
        }

        private void showallapproved()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Approved = '1'AND Validate = '1' AND SupervisorId = '" + myid + "' ORDER BY ReqNumber DESC", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    preparedatagrid();

                }
                catch (Exception)
                {
                    MessageBox.Show("Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Engineering Load(showallitems)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
        }

        private void showmydeptreq()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE SupervisorId = '" + myid + "' ORDER BY ReqNumber DESC", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    preparedatagrid();

                }
                catch (Exception)
                {
                    MessageBox.Show("Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Engineering Load(showallitems)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
        }

        #endregion
    }
}
