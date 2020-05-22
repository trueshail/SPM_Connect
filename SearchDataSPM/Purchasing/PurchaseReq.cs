using SearchDataSPM.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;
using Excel = Microsoft.Office.Interop.Excel;

namespace SearchDataSPM.Purchasing
{
    public partial class PurchaseReqform : Form
    {
        #region Setting up Various Variables to Store information

        private readonly SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
        private DataTable dt;
        private bool formloading;
        private readonly DataTable itemstable = new DataTable();
        private readonly List<string> Itemstodiscard = new List<string>();
        private log4net.ILog log;
        private PurchaseReq model = new PurchaseReq();
        private bool showingwaitingforapproval;
        private bool splashWorkDone;
        private int supervisoridfromreq;

        #endregion Setting up Various Variables to Store information

        #region Form Loading

        public PurchaseReqform()
        {
            InitializeComponent();
            dt = new DataTable();
            Clear();
        }

        private void Changecontrolbuttonnames()
        {
            bttnshowmydept.Text = "Show All";

            if (ConnectUser.PurchaseReqBuyer)
            {
                bttnneedapproval.Text = "Need's PO";
                bttnshowapproved.Text = "Show Purchased";
            }
        }

        private void Preparedatagrid()
        {
            dataGridView.DataSource = dt;
            dataGridView.Columns[0].Width = 35;
            dataGridView.Columns[0].HeaderText = "Req No";
            dataGridView.Columns[1].Width = 35;
            dataGridView.Columns[1].HeaderText = "Job";
            dataGridView.Columns[2].Width = 70;
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
            dataGridView.Columns[16].Visible = false;
            dataGridView.Columns[17].Visible = false;
            dataGridView.Columns[18].Visible = false;
            dataGridView.Columns[19].Visible = false;
            dataGridView.Columns[20].Visible = false;
            dataGridView.Columns[21].Visible = false;
            dataGridView.Columns[22].Visible = false;
            dataGridView.Columns[23].Visible = false;
            dataGridView.Columns[24].Visible = false;
            dataGridView.Columns[25].Visible = false;
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
            UpdateFont();
        }

        private void PurchaseReq_Load(object sender, EventArgs e)
        {
            formloading = true;
            if (ConnectUser.PurchaseReqApproval)
            {
                managergroupbox.Visible = true;
                managergroupbox.Enabled = true;
                dataGridView.ContextMenuStrip = ApprovalMenuStrip;
            }

            if (ConnectUser.PurchaseReqApproval2 || ConnectUser.PurchaseReqBuyer)
            {
                managergroupbox.Visible = true;
                managergroupbox.Enabled = true;
                if (ConnectUser.PurchaseReqApproval2)
                {
                    dataGridView.ContextMenuStrip = ApprovalMenuStrip;
                }
                Changecontrolbuttonnames();
            }

            if (ConnectUser.PurchaseReqApproval2 || ConnectUser.PurchaseReqApproval || ConnectUser.PurchaseReqBuyer)
            {
                //bttnneedapproval.PerformClick();
                PerformNeedApproval(bttnneedapproval);
            }
            else
            {
                ShowReqSearchItems(ConnectUser.Name);
            }

            formloading = false;

            Debug.WriteLine(ConnectUser.ConnectId);
            Debug.WriteLine(ConnectUser.Supervisor);
            Debug.WriteLine(ConnectUser.Name);
            Debug.WriteLine(ConnectUser.PurchaseReqApproval);
            Debug.WriteLine(ConnectUser.PurchaseReqApproval2);
            Debug.WriteLine(ConnectUser.PurchaseReqBuyer);
            Debug.WriteLine(supervisoridfromreq);
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Purchase Req ");
            this.BringToFront();
            this.Focus();
            this.Text = "SPM Connect Purchase Requisition - " + GetUserName().Substring(4);
        }

        private void ShowReqSearchItems(string user)
        {
            showingwaitingforapproval = false;
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] Where [RequestedBy] = '" + user + "'ORDER BY ReqNumber DESC", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    Preparedatagrid();
                }
                catch (Exception)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Show All Req Items For User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        #endregion Form Loading

        #region show edit button for approved req

        private void Checkforeditrights()
        {
            editbttn.Visible = Getapprovalstatus() == "0";

            if (ConnectUser.PurchaseReqApproval)
            {
                editbttn.Visible = true;
            }
            //if (ConnectUser.PurchaseReqApproval2)
            //{
            //    editbttn.Visible = false;
            //}
        }

        private string Getapprovalstatus()
        {
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];

                //MessageBox.Show(username);
                return Convert.ToString(slectedrow.Cells["Approved"].Value);
            }
            else
            {
                return "";
            }
        }

        private string Getrequestname()
        {
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];

                //MessageBox.Show(username);
                return Convert.ToString(slectedrow.Cells["RequestedBy"].Value);
            }
            else
            {
                return "";
            }
        }

        #endregion show edit button for approved req

        #region Purchase Req Item Search

        private void Mainsearch()
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

        private void PurchaseReqSearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (PurchaseReqSearchTxt.Text.Length > 0)
                {
                    Mainsearch();
                }
                else
                {
                    dataGridView.DataSource = null;
                    dataGridView.Refresh();
                    if (managergroupbox.Visible)
                    {
                        //bttnshowapproved.PerformClick();
                        ProcessShowApprovedBttn(bttnshowapproved);
                    }
                    else
                    {
                        ShowReqSearchItems(ConnectUser.Name);
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion Purchase Req Item Search

        #region Create New Purchase Req

        private void Createnew()
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new purchase req?", "SPM Connect - Create New Purchase Requistion?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //clearitemsbeforenewreq();
                //Clear();
                dateTimePicker1.MinDate = DateTime.Today;
                ecitbttn.Visible = false;
                int lastreq = Getlastreqnumber();

                if (Createnewreq(lastreq, ConnectUser.Name))
                {
                    ShowReqSearchItems(ConnectUser.Name);
                    Selectrowbeforeediting(lastreq.ToString());
                    Populatereqdetails(lastreq);
                    PopulateDataGridView();
                    Processeditbutton(true);
                    jobnumbertxt.Text = jobnumbertxt.Text.TrimStart();
                    subassytxt.Text = subassytxt.Text.TrimStart();
                }
            }
        }

        private bool Createnewreq(int reqnumber, string employee)
        {
            bool revtal = false;
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            //string jobnumber = "";
            //string subassy = "";
            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[PurchaseReqBase] (ReqNumber, RequestedBy, DateCreated, DateLastSaved, JobNumber, SubAssyNumber,LastSavedBy, Validate, Approved,Total,Happroved,DateRequired, SupervisorId, DateValidated,PApproval,Papproved) VALUES('" + reqnumber + "','" + employee + "','" + sqlFormattedDate + "','" + sqlFormattedDate + "','','','" + employee + "','0','0','0','0','" + sqlFormattedDate + "', '" + ConnectUser.Supervisor + "', null,'0','0')";
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                revtal = true;
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Create Entry On SQL Purchase Req Base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            return revtal;
        }

        private int Getlastreqnumber()
        {
            int lastreqnumber = 0;
            using (SqlCommand cmd = new SqlCommand("SELECT MAX(ReqNumber) FROM [SPM_Database].[dbo].[PurchaseReqBase]", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    lastreqnumber = (int)cmd.ExecuteScalar();
                    lastreqnumber++;
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Last Req Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }

            return lastreqnumber;
        }

        private void Selectrowbeforeediting(string searchValue)
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        int rowIndex = row.Index;
                        dataGridView.Rows[rowIndex].Selected = true;

                        break;
                    }
                }
            }
        }

        #endregion Create New Purchase Req

        #region Calculate Total

        private string totalvalue = "";

        private decimal Calculatetotal()
        {
            totalvalue = "";
            if (dataGridView1.Rows.Count > 0)
            {
                decimal total = 0.00m;
                int qty = 1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[4].Value.ToString().Length > 0 && row.Cells[4].Value.ToString() != null)
                    {
                        qty = Convert.ToInt32(row.Cells[4].Value.ToString());
                    }
                    try
                    {
                        decimal price = !string.IsNullOrEmpty(row.Cells[8].Value.ToString())
            ? Convert.ToDecimal(row.Cells[8].Value.ToString())
            : 0;
                        total += (qty * price);
                        totalcostlbl.Text = "Total Cost : $" + string.Format("{0:n}", Convert.ToDecimal(total.ToString()));

                        totalvalue = string.Format("{0:#.00}", total.ToString());
                    }
                    catch (Exception ex)
                    {
                        MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect -  Error Getting Total", MessageBoxButtons.OK);
                    }
                }
                return total;
            }
            else
            {
                totalcostlbl.Text = "";
            }
            return 0.00m;
        }

        #endregion Calculate Total

        #region Perform CRUD Operations

        private void Addnewitemtoreq()
        {
            try
            {
                int resultqty = 0;
                //int result = 0;
                //double price12 = 0.00;
                errorProvider1.Clear();
                if (qtytxt.Text.Length > 0 && qtytxt.Text != "0" && pricetxt.Text != "$0.00")
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
                    Updateorderid(Convert.ToInt32(purchreqtxt.Text));
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
                    if (qtytxt.Text.Length > 0 && qtytxt.Text != "0")
                    {
                        errorProvider1.SetError(pricetxt, "Price cannot be null");
                    }
                    else if (pricetxt.Text != "$0.00" && qtytxt.Text.Length != 1)
                    {
                        errorProvider1.SetError(qtytxt, "Cannot be null");
                    }
                    else if (qtytxt.Text == "0")
                    {
                        errorProvider1.SetError(qtytxt, "Qty cannot be zero");
                    }
                    else
                    {
                        errorProvider1.SetError(pricetxt, "Price cannot be null");
                        errorProvider1.SetError(qtytxt, "Cannot be null");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Clear()
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

        private void Ecitbttn_Click(object sender, EventArgs e)
        {
            if (savebttn.Visible)
            {
                errorProvider1.SetError(savebttn, "Save before closing");
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    errorProvider1.Clear();
                    Performdiscarditem();
                    Updateorderid(Convert.ToInt32(purchreqtxt.Text));
                    PopulateDataGridView();
                    Itemstodiscard.Clear();
                    Processexitbutton();
                }
                else
                {
                }
            }
        }

        private void Editbttn_Click(object sender, EventArgs e)
        {
            Itemstodiscard.Clear();
            Processeditbutton(true);
        }

        private string Gethapporvallimit()
        {
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'Limit'", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    limit = (string)cmd.ExecuteScalar();
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Limit for purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }

            return limit;
        }

        private bool Happroval()
        {
            bool req = false;
            //if (totalcostlbl.Text.Length > 0)
            //{
            //    MessageBox.Show(Convert.ToInt32(Convert.ToDecimal(totalvalue.TrimEnd())).ToString());
            //    MessageBox.Show(Convert.ToInt64(Convert.ToDecimal(Gethapporvallimit())).ToString());
            //    if (Convert.ToInt64(Convert.ToDecimal(totalvalue.TrimEnd())) > Convert.ToInt64(Convert.ToDecimal(Gethapporvallimit())))
            //    {
            //        req = true;
            //    }
            //}
            //MessageBox.Show("calculate total" + Calculatetotal());
            //MessageBox.Show(Convert.ToDecimal(Gethapporvallimit()).ToString());
            if (Calculatetotal() > Convert.ToDecimal(Gethapporvallimit()))
            {
                req = true;
            }

            return req;
        }

        private void Performdiscarditem()
        {
            foreach (string item in Itemstodiscard)
            {
                Splittagtovariables(item);
            }
        }

        private void Processdeletebttn()
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
                    Updateorderid(Convert.ToInt32(purchreqtxt.Text));
                    PopulateDataGridView();
                    Clear();
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

        private void Processeditbutton(bool showexit)
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
            groupBox3.Visible = !Validatechk.Checked;

            if (approvechk.Checked)
            {
                Validatechk.Visible = false;
            }
            else
            {
                Validatechk.Enabled = true;
                Validatechk.Visible = true;
            }

            if ((ConnectUser.PurchaseReqApproval || ConnectUser.PurchaseReqBuyer || ConnectUser.PurchaseReqApproval2) && Validatechk.Checked)
            {
                if (ConnectUser.ConnectId == supervisoridfromreq)
                {
                    approvechk.Visible = true;
                    approvechk.Enabled = true;
                }

                Validatechk.Visible = false;
                if (ConnectUser.Name == requestbytxt.Text && !approvechk.Checked)
                {
                    Validatechk.Enabled = true;
                    Validatechk.Visible = true;
                }
            }
            else
            {
                approvechk.Enabled = false;
                approvechk.Visible = false;
                Validatechk.Visible = true;
            }

            savebttn.Visible = true;

            Fillitemssource();
            toolbarpanel.Enabled = false;
            if (showexit)
            {
                ecitbttn.Visible = true;
            }
            dateTimePicker1.MinDate = DateTime.Today;
        }

        private void Processexitbutton()
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
            dateTimePicker1.MinDate = new DateTime(1900, 01, 01);
        }

        private async Task Processsavebutton(bool validatehit, string typeofsave)
        {
            try
            {
                await Task.Run(() => SplashDialog("Saving Data...")).ConfigureAwait(true);

                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                //tabControl1.TabPages.Remove(PreviewTabPage);
                string reqnumber = purchreqtxt.Text;
                errorProvider1.Clear();
                if (validatehit)
                {
                    UpdateReq(Convert.ToInt32(purchreqtxt.Text), typeofsave);
                    if (!(ConnectUser.PurchaseReqApproval2 || ConnectUser.PurchaseReqApproval || ConnectUser.PurchaseReqBuyer))
                    {
                        ShowReqSearchItems(ConnectUser.Name);
                    }
                    Clearaddnewtextboxes();
                    Processexitbutton();
                    if (typeofsave == "Approved" || typeofsave == "Papproved" || typeofsave == "Happroved" || typeofsave == "Rejected" || typeofsave == "HRejected")
                    {
                        // bttnshowapproved.PerformClick();
                        ProcessShowApprovedBttn(bttnshowapproved);
                    }
                    if (typeofsave == "ApprovedFalse" || typeofsave == "HapprovedFalse" || typeofsave == "PapprovedFalse")
                    {
                        //bttnneedapproval.PerformClick();
                        PerformNeedApproval(bttnneedapproval);
                    }

                    if (dataGridView.Rows.Count > 0)
                    {
                        dataGridView.ClearSelection();
                        Selectrowbeforeediting(reqnumber);
                        Populatereqdetails(Convert.ToInt32(reqnumber));
                        PopulateDataGridView();
                    }
                    dateTimePicker1.MinDate = new DateTime(1900, 01, 01);
                    //populatereqdetails(Convert.ToInt32(reqnumber));
                    //PopulateDataGridView();
                }
                else
                {
                    if (jobnumbertxt.Text.Length > 0 && subassytxt.Text.Length > 0)
                    {
                        UpdateReq(Convert.ToInt32(purchreqtxt.Text), typeofsave);
                        if (bttnshowmyreq.Visible)
                        {
                            // bttnshowmyreq.PerformClick();
                            Perfromshowmyreqbuttn();
                        }
                        else
                        {
                            ShowReqSearchItems(ConnectUser.Name);
                        }

                        Clearaddnewtextboxes();
                        Processexitbutton();
                        dateTimePicker1.MinDate = new DateTime(1900, 01, 01);
                        //if (dataGridView.Rows.Count > 0)
                        //{
                        //    selectrowbeforeediting(reqnumber);
                        //}

                        //populatereqdetails(Convert.ToInt32(reqnumber));
                        //PopulateDataGridView();
                    }
                    else
                    {
                        if (jobnumbertxt.Text.Length > 0)
                        {
                            errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                        }
                        else if (subassytxt.Text.Length > 0)
                        {
                            errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                        }
                        else
                        {
                            errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                            errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                        }
                    }
                }

                // t.Abort();
                //this.TopMost = true;
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                this.Focus();
                this.Activate();
                splashWorkDone = true;
            }
            catch
            {
            }
        }

        private void Removeitems(string itemno, string description)
        {
            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[PurchaseReq] WHERE Item ='" + itemno + "' AND ReqNumber ='" + description + "' ";
                SqlCommand sda = new SqlCommand(query, connectapi.cn);
                sda.ExecuteNonQuery();
                connectapi.cn.Close();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Remove Items on Unsave", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private async void Savebttn_Click(object sender, EventArgs e)
        {
            Itemstodiscard.Clear();
            await Processsavebutton(false, "Normal").ConfigureAwait(false);
        }

        private void Splittagtovariables(string s)
        {
            string[] values = s.Replace("][", "~").Split('~');
            //string[] values = s.Split('][');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            Removeitems(values[0], values[1]);
        }

        private void Updateorderid(int reqnumber)
        {
            using (SqlCommand sqlCommand = new SqlCommand("with cte as(select *, new_row_id = row_number() over(partition by ReqNumber order by ReqNumber)from[dbo].[PurchaseReq] where ReqNumber = @itemnumber)update cte set OrderId = new_row_id", connectapi.cn))
            {
                try
                {
                    connectapi.cn.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@itemnumber", reqnumber);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Update Order Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void UpdateReq(int reqnumber, string typesave)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string datereq = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string jobnumber = jobnumbertxt.Text.Trim();
            string subassy = subassytxt.Text.Trim();
            string notes = notestxt.Text;
            bool approval = Happroval();

            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (typesave == "Normal")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',HApproval = '" + (approval ? "1" : "0") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if (typesave == "Validated")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',HApproval = '" + (approval ? "1" : "0") + "',DateValidated = '" + (Validatechk.Checked ? sqlFormattedDate : null) + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if (typesave == "Approved")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',HApproval = '" + (approval ? "1" : "0") + "',ApprovedBy = '" + ConnectUser.Name + "',DateApproved = '" + (approvechk.Checked ? sqlFormattedDate : "") + "',PApproval ='" + (approval ? "0" : "1") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if (typesave == "ApprovedFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',HApproval = '" + (approval ? "1" : "0") + "',ApprovedBy = ' ',DateApproved = null,PApproval = '0' WHERE ReqNumber = '" + reqnumber + "' ";
                }
                if (typesave == "Rejected")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "3" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',HApproval = '" + (approval ? "0" : "0") + "',ApprovedBy = '" + ConnectUser.Name + "',DateApproved = '" + (approvechk.Checked ? sqlFormattedDate : "") + "',PApproval ='" + (approval ? "0" : "0") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                }
                if (typesave == "Happroved")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET HApproved = '" + (happrovechk.Checked ? "1" : "0") + "',HApproval = '" + (approval ? "1" : "0") + "',HApprovedBy = '" + ConnectUser.Name + "',HDateApproved = '" + (happrovechk.Checked ? sqlFormattedDate : "") + "',PApproval = '1' WHERE ReqNumber = '" + reqnumber + "' ";
                }
                if (typesave == "Happrovedfalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET Total = '" + totalvalue + "',HApproval = '" + (approval ? "1" : "0") + "',Happroved = '" + (happrovechk.Checked ? "1" : "0") + "',HApprovedBy = ' ',HDateApproved = null,PApproval = '0' WHERE ReqNumber = '" + reqnumber + "' ";
                }
                if (typesave == "HRejected")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET HApproved = '" + (happrovechk.Checked ? "3" : "0") + "',HApproval = '" + (approval ? "1" : "0") + "',HApprovedBy = '" + ConnectUser.Name + "',HDateApproved = '" + (happrovechk.Checked ? sqlFormattedDate : "") + "',PApproval = '0' WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if (typesave == "Papproved")
                {
                    string ponumber = "";
                    string pdate = "";
                    PODetails pODetails = new PODetails();
                    pODetails.BringToFront();
                    pODetails.TopMost = true;
                    pODetails.Focus();
                    if (pODetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ponumber = pODetails.ValueIWant;
                        pdate = pODetails.Podate;
                    }

                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET Papproved = '" + (purchasedchk.Checked ? "1" : "0") + "',PApprovedBy = '" + ConnectUser.Name + "',PDateApproved = '" + (purchasedchk.Checked ? sqlFormattedDate : "") + "',PApproval = '1',PONumber = '" + ponumber + "',PODate = '" + pdate + "'  WHERE ReqNumber = '" + reqnumber + "' ";
                }

                if (typesave == "Papprovedfalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET Papproved = '" + (purchasedchk.Checked ? "1" : "0") + "',PApprovedBy = ' ',PDateApproved = null,PONumber = ' ',PODate = null WHERE ReqNumber = '" + reqnumber + "' ";
                }

                //if (approvechk.Checked)
                //{
                //    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',DateValidated = '" + (Validatechk.Checked ? sqlFormattedDate : "") + "',ApprovedBy = '" + ConnectUser.Name + "',DateApproved = '" + (approvechk.Checked ? sqlFormattedDate : "") + "' WHERE ReqNumber = '" + reqnumber + "' ";
                //}
                //else
                //{
                //    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "',LastSavedBy = '" + ConnectUser.Name.ToString() + "',DateRequired = '" + datereq + "',Total = '" + totalvalue + "',Approved = '" + (approvechk.Checked ? "1" : "0") + "',Validate = '" + (Validatechk.Checked ? "1" : "0") + "',DateValidated = '" + (Validatechk.Checked ? sqlFormattedDate : null) + "' WHERE ReqNumber = '" + reqnumber + "' ";
                //}

                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Update Entry On SQL Purchase Req Base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        #endregion Perform CRUD Operations

        #region Fill Items Source for search and add

        private void Clearaddnewtextboxes()
        {
            ///itemsearchtxtbox.Clear();
            Descriptiontxtbox.Clear();
            oemitemnotxt.Clear();
            oemtxt.Clear();
            pricetxt.Clear();
            pricetxt.Text = "$0.00";
            qtytxt.Clear();
        }

        private void Filldatatable(string itemnumber)
        {
            string sql = "SELECT *  FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemnumber + "'";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlDataAdapter _adapter = new SqlDataAdapter(sql, connectapi.cn);
                itemstable.Clear();
                _adapter.Fill(itemstable);
            }
            catch (SqlException ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Fill Items Details For Dropdown Selected Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void Fillinfo()
        {
            if (itemstable.Rows.Count > 0)
            {
                DataRow r = itemstable.Rows[0];
                ItemTxtBox.Text = r["ItemNumber"].ToString();
                Descriptiontxtbox.Text = r["Description"].ToString();
                oemtxt.Text = r["Manufacturer"].ToString();
                oemitemnotxt.Text = r["ManufacturerItemNumber"].ToString();
            }
            else
            {
                MessageBox.Show("Item Not found!!", "SPM Connect", MessageBoxButtons.OK);
            }
        }

        private void Fillitemssource()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ItemsToSelect]", connectapi.cn))
            {
                try
                {
                    connectapi.cn.Open();
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
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect New Item - Fill Items Drop Down Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        #endregion Fill Items Source for search and add

        #region Populated Details for both datagrids showing purchase req details

        private string reqnumber = "";

        private void PopulateDataGridView()
        {
            if (dataGridView.Rows.Count > 0)
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
                //dataGridView1.Columns[0].Visible = false;
                //dataGridView1.Columns[9].Visible = false;
                //dataGridView1.Columns[8].Visible = false;
                reqnumber = item.ToString();
                PreviewTabPage.Text = "ReqNo : " + item;
                UpdateFontdataitems();
                Calculatetotal();
            }
        }

        private void Populatereqdetails(int item) // populates details of selected purchase req
        {
            if (dataGridView.Rows.Count > 0)
            {
                try
                {
                    DataRow[] dr = dt.Select("ReqNumber = '" + item + "'");
                    if (dr.Length == 0)
                    {
                        return;
                    }
                    purchreqtxt.Text = dr[0]["ReqNumber"].ToString();
                    requestbytxt.Text = dr[0]["RequestedBy"].ToString();
                    datecreatedtxt.Text = dr[0]["DateCreated"].ToString();
                    jobnumbertxt.Text = dr[0]["JobNumber"].ToString();
                    subassytxt.Text = dr[0]["SubAssyNumber"].ToString();
                    lastsavedby.Text = dr[0]["LastSavedBy"].ToString();
                    lastsavedtxt.Text = dr[0]["DateLastSaved"].ToString();
                    notestxt.Text = dr[0]["Notes"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr[0]["DateRequired"]);

                    approvebylabel.Text = "Approved by : " + dr[0]["Approvedby"].ToString();
                    apprvonlabel.Text = "Approved on : " + dr[0]["DateApproved"].ToString();

                    happrovedbylbl.Text = "Approved by : " + dr[0]["HApprovedBy"].ToString();
                    happroveonlblb.Text = "Approved on : " + dr[0]["HDateApproved"].ToString();

                    purchasebylbl.Text = "Purchased by : " + dr[0]["PApprovedBy"].ToString();
                    purchaseonlbl.Text = "Purchased on : " + dr[0]["PDateApproved"].ToString();
                    ponumberlbl.Text = "PO No. : " + dr[0]["PONumber"].ToString();

                    supervisoridfromreq = Convert.ToInt32(dr[0]["SupervisorId"].ToString());

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
                    else if (dr[0]["Approved"].ToString().Equals("3") && dr[0]["Validate"].ToString().Equals("1"))
                    {
                        approvechk.Text = "Rejected";
                        approvebylabel.Text = "Rejected by : " + dr[0]["Approvedby"].ToString();
                        apprvonlabel.Text = "Rejected on : " + dr[0]["DateApproved"].ToString();
                        approvechk.Checked = true;
                        approvechk.Visible = true;
                        Validatechk.Visible = false;
                        printbttn.Enabled = false;
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

                    if (ConnectUser.PurchaseReqApproval && dr[0]["Validate"].ToString().Equals("1"))
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
                        else if (dr[0]["Approved"].ToString().Equals("3"))
                        {
                            approvechk.Text = "Rejected";
                            approvebylabel.Text = "Rejected by : " + dr[0]["Approvedby"].ToString();
                            apprvonlabel.Text = "Rejected on : " + dr[0]["DateApproved"].ToString();
                            approvechk.Checked = true;
                            approvechk.Visible = true;
                            printbttn.Enabled = false;
                            approvebylabel.Visible = true;
                            apprvonlabel.Visible = true;
                            Validatechk.Visible = false;
                            Validatechk.Enabled = false;
                        }
                        else if (dr[0]["Approved"].ToString().Equals("1") && dr[0]["Happroved"].ToString().Equals("1"))
                        {
                            approvechk.Text = "Approved";
                            approvechk.Checked = true;
                            approvechk.Visible = true;
                            printbttn.Enabled = true;
                            approvebylabel.Visible = true;
                            editbttn.Visible = false;
                            apprvonlabel.Visible = true;
                            Validatechk.Visible = false;
                            Validatechk.Enabled = false;
                        }
                        else
                        {
                            if (ConnectUser.ConnectId == supervisoridfromreq)
                            {
                                approvechk.Text = "Approve";
                                approvechk.Checked = false;
                                approvechk.Visible = true;
                                printbttn.Enabled = false;
                                approvebylabel.Visible = false;
                                apprvonlabel.Visible = false;
                            }
                        }
                    }
                    else if (ConnectUser.PurchaseReqApproval && dr[0]["Validate"].ToString().Equals("0"))
                    {
                        approvechk.Text = "Approve";
                        approvechk.Checked = false;
                        approvechk.Visible = false;
                        printbttn.Enabled = false;
                    }

                    if (dr[0]["HApproval"].ToString().Equals("1"))
                    {
                        if (dr[0]["Approved"].ToString().Equals("1") && dr[0]["Validate"].ToString().Equals("1"))
                        {
                            if (ConnectUser.PurchaseReqApproval2)
                            {
                                if (dr[0]["Happroved"].ToString().Equals("0"))
                                {
                                    hauthoritygroupbox.Visible = true;
                                    hauthoritygroupbox.Enabled = true;
                                    happrovechk.Text = "Final Approve";
                                    happrovechk.Checked = false;
                                }
                                else if (dr[0]["Happroved"].ToString().Equals("3"))
                                {
                                    hauthoritygroupbox.Visible = true;
                                    hauthoritygroupbox.Enabled = false;
                                    happrovechk.Text = "Final Rejected";
                                    happrovedbylbl.Text = "Rejected by : " + dr[0]["HApprovedBy"].ToString();
                                    happroveonlblb.Text = "Rejected on : " + dr[0]["HDateApproved"].ToString();
                                    happrovechk.Checked = true;
                                    editbttn.Visible = false;
                                }
                                else
                                {
                                    hauthoritygroupbox.Visible = true;
                                    hauthoritygroupbox.Enabled = !dr[0]["Papproved"].ToString().Equals("1");

                                    happrovechk.Text = "Final Approved";
                                    happrovechk.Checked = true;
                                    editbttn.Visible = false;
                                }
                            }
                            else
                            {
                                if (ConnectUser.PurchaseReqApproval)
                                {
                                    if (dr[0]["Happroved"].ToString().Equals("1"))
                                    {
                                        hauthoritygroupbox.Visible = true;
                                        hauthoritygroupbox.Enabled = false;
                                        happrovechk.Text = "Final Approved";
                                        happrovechk.Checked = true;
                                        printbttn.Enabled = true;
                                        editbttn.Visible = false;
                                    }
                                    else if (dr[0]["Happroved"].ToString().Equals("3"))
                                    {
                                        hauthoritygroupbox.Visible = true;
                                        hauthoritygroupbox.Enabled = false;
                                        happrovechk.Text = "Final Rejected";
                                        happrovedbylbl.Text = "Rejected by : " + dr[0]["HApprovedBy"].ToString();
                                        happroveonlblb.Text = "Rejected on : " + dr[0]["HDateApproved"].ToString();
                                        happrovechk.Checked = true;
                                        editbttn.Visible = false;
                                        printbttn.Enabled = false;
                                    }
                                    else
                                    {
                                        hauthoritygroupbox.Visible = false;
                                        hauthoritygroupbox.Enabled = false;
                                        happrovechk.Text = "Final Approved";
                                        happrovechk.Checked = false;
                                        printbttn.Enabled = false;
                                        editbttn.Visible = true;
                                    }
                                }
                                else
                                {
                                    if (dr[0]["Happroved"].ToString().Equals("1"))
                                    {
                                        hauthoritygroupbox.Visible = true;
                                        hauthoritygroupbox.Enabled = false;
                                        happrovechk.Text = "Final Approved";
                                        happrovechk.Checked = true;
                                        printbttn.Enabled = true;
                                        editbttn.Visible = false;
                                    }
                                    else if (dr[0]["Happroved"].ToString().Equals("3"))
                                    {
                                        hauthoritygroupbox.Visible = true;
                                        hauthoritygroupbox.Enabled = false;
                                        happrovechk.Text = "Final Rejected";
                                        happrovedbylbl.Text = "Rejected by : " + dr[0]["HApprovedBy"].ToString();
                                        happroveonlblb.Text = "Rejected on : " + dr[0]["HDateApproved"].ToString();
                                        happrovechk.Checked = true;
                                        printbttn.Enabled = false;
                                        editbttn.Visible = false;
                                    }
                                    else
                                    {
                                        hauthoritygroupbox.Visible = false;
                                        hauthoritygroupbox.Enabled = false;
                                        happrovechk.Text = "Final Approved";
                                        happrovechk.Checked = false;
                                        printbttn.Enabled = false;
                                        editbttn.Visible = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            hauthoritygroupbox.Visible = false;
                            hauthoritygroupbox.Enabled = false;
                            happrovechk.Text = "Final Approved";
                            happrovechk.Checked = false;
                            printbttn.Enabled = false;
                        }
                    }
                    else
                    {
                        hauthoritygroupbox.Visible = false;
                        hauthoritygroupbox.Enabled = false;
                        happrovechk.Text = "Final Approved";
                        happrovechk.Checked = false;
                    }

                    /// pruchasing
                    ///
                    if (dr[0]["PApproval"].ToString().Equals("1"))
                    {
                        if (dr[0]["Approved"].ToString().Equals("1") && dr[0]["Validate"].ToString().Equals("1"))
                        {
                            if (ConnectUser.PurchaseReqBuyer)
                            {
                                if (dr[0]["Papproved"].ToString().Equals("0"))
                                {
                                    purchasegrpbox.Visible = true;
                                    purchasegrpbox.Enabled = true;
                                    purchasedchk.Text = "Purchase";
                                    purchasedchk.Checked = false;
                                    editbttn.Visible = true;
                                }
                                else
                                {
                                    purchasegrpbox.Visible = true;
                                    purchasegrpbox.Enabled = false;
                                    purchasedchk.Text = "Purchased";
                                    purchasedchk.Checked = true;
                                    editbttn.Visible = false;
                                }
                            }
                            else
                            {
                                if (ConnectUser.PurchaseReqApproval || ConnectUser.PurchaseReqApproval2)
                                {
                                    if (dr[0]["Papproved"].ToString().Equals("1"))
                                    {
                                        purchasegrpbox.Visible = true;
                                        purchasegrpbox.Enabled = false;
                                        purchasedchk.Text = "Purchased";
                                        purchasedchk.Checked = true;
                                        //printbttn.Enabled = true;
                                        editbttn.Visible = false;
                                    }
                                    else
                                    {
                                        purchasegrpbox.Visible = false;
                                        purchasegrpbox.Enabled = false;
                                        purchasedchk.Text = "Purchase";
                                        purchasedchk.Checked = false;
                                        //printbttn.Enabled = false;
                                        if (ConnectUser.PurchaseReqApproval && ConnectUser.PurchaseReqApproval2)
                                        {
                                            editbttn.Visible = true;
                                            editbttn.Visible = !dr[0]["Happroved"].ToString().Equals("1");
                                        }
                                        else if (ConnectUser.PurchaseReqApproval)
                                        {
                                            editbttn.Visible = true;
                                            if (dr[0]["Happroved"].ToString().Equals("1"))
                                            {
                                                editbttn.Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            editbttn.Visible = Getrequestname() == ConnectUser.Name;
                                        }
                                    }
                                }
                                else
                                {
                                    if (dr[0]["Papproved"].ToString().Equals("1"))
                                    {
                                        purchasegrpbox.Visible = true;
                                        purchasegrpbox.Enabled = false;
                                        purchasedchk.Text = "Purchased";
                                        purchasedchk.Checked = true;
                                        printbttn.Enabled = true;
                                        editbttn.Visible = false;
                                    }
                                    else
                                    {
                                        purchasegrpbox.Visible = false;
                                        purchasegrpbox.Enabled = false;
                                        purchasedchk.Text = "Purchase";
                                        purchasedchk.Checked = false;
                                        // printbttn.Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            purchasegrpbox.Visible = false;
                            purchasegrpbox.Enabled = false;
                            purchasedchk.Text = "Purchase";
                            purchasedchk.Checked = false;
                            printbttn.Enabled = false;
                            editbttn.Visible = Getrequestname() == ConnectUser.Name;
                        }
                    }
                    else
                    {
                        purchasegrpbox.Visible = false;
                        purchasegrpbox.Enabled = false;
                        purchasedchk.Text = "Purchase";
                        purchasedchk.Checked = false;
                    }

                    ///////////////////////////////////////

                    if (ConnectUser.PurchaseReqApproval2 && Getrequestname() == ConnectUser.Name && !happrovechk.Checked && dr[0]["Papproved"].ToString().Equals("0"))
                    {
                        editbttn.Visible = true;
                    }
                }
                catch
                {
                    //MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Populate Req Details", MessageBoxButtons.OK);
                }
            }
            if (savebttn.Visible && ecitbttn.Visible)
                editbttn.Visible = false;
        }

        #endregion Populated Details for both datagrids showing purchase req details

        #region Form closing

        private void PurchaseReqform_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Purchase Req ");
            this.Dispose();
        }

        private void PurchaseReqform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savebttn.Visible)
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

        #endregion Form closing

        #region Open report viewer

        private void Reportpurchaereq(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer(Reportname, itemvalue, totalvalue);
            form1.Show();
        }

        #endregion Open report viewer

        #region Validation Check

        private bool Getapprovedstatus(int reqno)
        {
            bool approved = false;
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] where ReqNumber ='" + reqno + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string useractiveblock = dr["Approved"].ToString();
                    approved = useractiveblock == "1";
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get approval status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
            }
            finally
            {
                connectapi.cn.Close();
            }

            return approved;
        }

        private void SplashDialog(string message)
        {
            splashWorkDone = false;
            ThreadPool.QueueUserWorkItem((_) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + ((this.Width - splashForm.Width) / 2), this.Location.Y + ((this.Height - splashForm.Height) / 2));
                    splashForm.Show();
                    while (!splashWorkDone)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
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
                    if (ConnectUser.PurchaseReqApproval)
                    {
                        approvechk.Visible = false;
                        approvechk.Enabled = false;
                    }
                }
            }
        }

        private async void Validatechk_Click(object sender, EventArgs e)
        {
            if (!Validatechk.Checked)
            {
                if (Getapprovedstatus(Convert.ToInt32(purchreqtxt.Text)))
                {
                    MetroFramework.MetroMessageBox.Show(this, "This purchase requisition is approved. Only ConnectUser.PurchaseReqApproval can edit the details.", "SPM Connect - Purchase Req already approved", MessageBoxButtons.OK);
                    Validatechk.Checked = true;
                    Validatechk.Text = "Invalidate";
                    groupBox3.Visible = false;
                    Processexitbutton();
                    ShowReqSearchItems(ConnectUser.Name);
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
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this purchase req for order?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to respective ConnectUser.PurchaseReqApproval for approval.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (jobnumbertxt.Text.Length > 0 && subassytxt.Text.Length > 0 && dataGridView1.Rows.Count > 0)
                    {
                        string reqno = purchreqtxt.Text;
                        await Processsavebutton(true, "Validated").ConfigureAwait(false);
                        Validatechk.Text = "Invalidate";
                        await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                        Cursor.Current = Cursors.WaitCursor;
                        this.Enabled = false;
                        string filename = Makefilenameforreport(reqno, true);
                        SaveReport(reqno, filename);
                        Preparetosendemail(reqno, true, "", filename, false, "user", false);
                        Cursor.Current = Cursors.Default;
                        this.Enabled = true;
                        this.Focus();
                        this.Activate();
                        splashWorkDone = true;
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (jobnumbertxt.Text.Length > 0)
                        {
                            errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                        }
                        else if (subassytxt.Text.Length > 0)
                        {
                            errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                        }
                        else
                        {
                            errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                            errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                        }
                        if (dataGridView1.Rows.Count < 1 && jobnumbertxt.Text.Length > 0 && subassytxt.Text.Length > 0)
                        {
                            errorProvider1.Clear();
                            MetroFramework.MetroMessageBox.Show(this, "System cannot send out this purchase req for approval as there are no items to order.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        Validatechk.Checked = false;
                    }
                }
                else
                {
                    Validatechk.Checked = false;
                }
            }
        }

        #endregion Validation Check

        #region manager approve check changed

        private async void Approvechk_Click(object sender, EventArgs e)
        {
            if (ConnectUser.PurchaseReqApproval)
            {
                if (!approvechk.Checked)
                {
                    if (Gethapprovedstatus(Convert.ToInt32(purchreqtxt.Text)))
                    {
                        MetroFramework.MetroMessageBox.Show(this, "This purchase requisition is approved by higher authority. Only people at that credentials can edit the details.", "SPM Connect - Purchase Req H-approved", MessageBoxButtons.OK);
                        approvechk.Checked = true;
                        approvechk.Text = "Approved";
                        Processexitbutton();
                    }
                    else
                    {
                        approvechk.Checked = false;
                        approvechk.Text = "Approve";
                        await Processsavebutton(true, "ApprovedFalse").ConfigureAwait(false);
                    }
                }
                else
                {
                    DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to approve this purchase requistion for order?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to requested user attaching the approved purchase req.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (jobnumbertxt.Text.Length > 0 && subassytxt.Text.Length > 0)
                        {
                            string reqno = purchreqtxt.Text;
                            string requestby = requestbytxt.Text;
                            bool happroval = Happroval();

                            await Processsavebutton(true, "Approved").ConfigureAwait(false);
                            approvechk.Checked = true;
                            await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                            this.Enabled = false;

                            string filename = Makefilenameforreport(reqno, false);
                            SaveReport(reqno, filename);
                            Preparetosendemail(reqno, false, requestby, filename, happroval, "ConnectUser.PurchaseReqApproval", false);
                            Exporttoexcel();
                            this.Enabled = true;
                            this.Focus();
                            this.Activate();
                            splashWorkDone = true;
                        }
                        else
                        {
                            errorProvider1.Clear();
                            if (jobnumbertxt.Text.Length > 0)
                            {
                                errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                            }
                            else if (subassytxt.Text.Length > 0)
                            {
                                errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                            }
                            else
                            {
                                errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                                errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                            }

                            approvechk.Checked = false;
                        }
                    }
                    else
                    {
                        approvechk.Checked = false;
                    }
                }
            }
        }

        private bool Gethapprovedstatus(int reqno)
        {
            bool approved = false;
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] where ReqNumber ='" + reqno + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string happroved = dr["Happroved"].ToString();
                    approved = happroved == "1";
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get approval status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
            }
            finally
            {
                connectapi.cn.Close();
            }

            return approved;
        }

        #endregion manager approve check changed

        #region datagridview events

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView.ClearSelection();
                dataGridView.Rows[columnindex].Selected = true;
            }
        }

        private void DataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGridView.Rows.Count < 1)
            {
                editbttn.Visible = false;
                tabControl1.Visible = false;
                totalcostlbl.Visible = false;
                hauthoritygroupbox.Visible = false;
                purchasegrpbox.Visible = false;
            }
        }

        private async void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                if (dataGridView.Rows.Count > 0 && dataGridView.SelectedCells.Count != 0)
                {
                    try
                    {
                        await Task.Run(() => SplashDialog("Loading Data...")).ConfigureAwait(true);
                        Cursor.Current = Cursors.WaitCursor;
                        this.Enabled = false;
                        dataGridView1.AutoGenerateColumns = false;
                        if (dataGridView.SelectedCells[0].RowIndex < 0 || dataGridView.SelectedCells[0] == null)
                        {
                            return;
                        }
                        int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                        DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                        int item = Convert.ToInt32(slectedrow.Cells[0].Value);
                        Checkforeditrights();

                        Populatereqdetails(item);
                        PopulateDataGridView();
                        tabControl1.Visible = true;
                        totalcostlbl.Visible = true;
                        if (tabControl1.TabPages.Count == 0)
                        {
                            tabControl1.TabPages.Add(PreviewTabPage);
                        }
                        Cursor.Current = Cursors.Default;
                        this.Focus();
                        this.Activate();
                        this.Enabled = true;
                        splashWorkDone = true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);
                    }
                }
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Getitemsfromgrid();
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[columnindex].Selected = true;
            }
        }

        private void DeleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            Getitemsfromgrid();
            Processdeletebttn();
        }

        private void EditItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ecitbttn.Visible = false;
            Clear();
            Getitemsfromgrid();
            qtytxt.Focus();
            qtytxt.SelectAll();
        }

        private void FormSelector_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && !Validatechk.Checked)
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

        private void Getitemsfromgrid()
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
                        ItemTxtBox.Text = model.Item;
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

        private void UpdateFont()
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 8.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        private void UpdateFontdataitems()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8.0F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Tomato;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView1.Columns["Price"].DefaultCellStyle.Format = "n2";
        }

        #endregion datagridview events

        #region save report and send email

        public void SaveReport(string reqno, string fileName)
        {
            const string _reportName = "/GeniusReports/PurchaseOrder/SPM_PurchaseReq";

            RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];
            parameters[0] = new RE2005.ParameterValue
            {
                Name = "pReqno",
                Value = reqno
            };

            ReportHelper.SaveReport(fileName, _reportName, parameters);
        }

        private string Makefilenameforreport(string reqno, bool prelim)
        {
            return prelim ? @"\\spm-adfs\SDBASE\Reports\Prelim\" + reqno + ".pdf" : @"\\spm-adfs\SDBASE\Reports\Approved\" + reqno + ".pdf";
        }

        private void Preparetosendemail(string reqno, bool prelim, string requestby, string fileName, bool happroval, string triggerby, bool rejected)
        {
            if (!rejected)
            {
                if (prelim)
                {
                    Sendemailtosupervisor(reqno, fileName);
                }
                else
                {
                    if (happroval)
                    {
                        if (Sendemailyesnohauthority())
                        {
                            Sendmailforhapproval(reqno, fileName);
                        }
                    }
                    else
                    {
                        Sendemailtouser(reqno, fileName, requestby, triggerby, false);
                        if (triggerby != "ConnectUser.PurchaseReqBuyer")
                        {
                            if (Sendemailyesnopbuyer())
                            {
                                Sendmailtopbuyers(reqno, "");
                            }
                        }
                    }
                }
            }
            else
            {
                Sendemailtouser(reqno, fileName, requestby, triggerby, rejected);
            }
        }

        private void Sendemail(string emailtosend, string subject, string name, string body, string filetoattach, string cc)
        {
            if (Sendemailyesno())
            {
                connectapi.TriggerEmail(emailtosend, subject, name, body, filetoattach, cc, "", "Normal");
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Emails are turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Sendemailtosupervisor(string reqno, string fileName)
        {
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.id, ConnectUser.Supervisor.ToString()))
                Sendemail(item.email, reqno + " Purchase Req Approval Required - Job " + jobnumbertxt.Text, item.name, Environment.NewLine + ConnectUser.Name + " sent this purchase req for approval.", fileName, "");
        }

        private void Sendemailtouser(string reqno, string fileName, string requestby, string triggerby, bool rejected)
        {
            string email = connectapi.GetNameEmailByParaValue(UserFields.Name, requestby)[0].email;
            if (!rejected)
            {
                if (triggerby == "ConnectUser.PurchaseReqApproval")
                {
                    Sendemail(email, reqno + " Purchase Req Approved - Job " + jobnumbertxt.Text, requestby, Environment.NewLine + " Your purchase req is approved.", fileName, "");
                }
                else
                {
                    List<NameEmail> supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, supervisoridfromreq.ToString());
                    string supervisoremail = supnameemail[0].email;

                    if (triggerby == "ConnectUser.PurchaseReqBuyer")
                    {
                        Sendemail(email, reqno + " Purchase Req Purchased - Job " + jobnumbertxt.Text, requestby, Environment.NewLine + " Your purchase req is sent out for purchase.", fileName, supervisoremail);
                    }
                    if (triggerby == "highautority")
                    {
                        Sendemail(email, reqno + " Purchase Req Approved - Job " + jobnumbertxt.Text, requestby, Environment.NewLine + " Your purchase req is approved.", fileName, supervisoremail);
                    }
                }
            }
            else
            {
                if (triggerby == "ConnectUser.PurchaseReqApproval")
                {
                    Sendemail(email, reqno + " Purchase Req Rejected - Job " + jobnumbertxt.Text, requestby, Environment.NewLine + " Your purchase req is not approved.", fileName, "");
                }
                else
                {
                    List<NameEmail> supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, supervisoridfromreq.ToString());
                    string supervisoremail = supnameemail[0].email;

                    if (triggerby == "highautority")
                    {
                        Sendemail(email, reqno + " Purchase Req Rejected - Job " + jobnumbertxt.Text, requestby, Environment.NewLine + " Your purchase req is not approved.", fileName, supervisoremail);
                    }
                }
            }
        }

        private bool Sendemailyesno()
        {
            bool sendemail = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailReq'", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    limit = (string)cmd.ExecuteScalar();
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Limit for purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            if (limit == "1")
            {
                sendemail = true;
            }
            return sendemail;
        }

        private void Sendmailforhapproval(string reqno, string fileName)
        {
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.PurchaseReqApproval2, "1"))
                Sendemail(item.email, reqno + " Purchase Req Approval Required - 2nd Approval - Job " + jobnumbertxt.Text, item.name, Environment.NewLine + ConnectUser.Name + " sent this purchase req for second approval.", fileName, "");
        }

        private void Sendmailtopbuyers(string reqno, string fileName)
        {
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.PurchaseReqBuyer, "1"))
                Sendemail(item.email, reqno + " Purchase Req needs PO - Notification - Job " + jobnumbertxt.Text, item.name, Environment.NewLine + ConnectUser.Name + " apporved this purchase req and on its way to be purchased. ", fileName, "");
        }

        #endregion save report and send email

        #region add items to purchase req button and text events groupbox 3

        private bool dontstop = true;

        public bool CheckItemPresentOnGenius(string itemid)
        {
            bool itempresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPMDB].[dbo].[Edb] WHERE [Item]='" + itemid + "'", connectapi.cn))
            {
                try
                {
                    connectapi.cn.Open();

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        //MessageBox.Show("item already exists");
                        itempresent = true;
                    }
                    else
                    {
                        //MessageBox.Show(" move forward");
                        itempresent = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check Item Present On Genius", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            return itempresent;
        }

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            Addnewitemtoreq();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            Addnewbttn.Enabled = false;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Processdeletebttn();
        }

        private void FillPrice(string item)
        {
            if (pricetxt.Text == "$0.00")
            {
                DataTable iteminfo = new DataTable();
                iteminfo.Clear();
                iteminfo = Getpriceforitem(item);
                if (iteminfo.Rows.Count > 0)
                {
                    DataRow r = iteminfo.Rows[0];
                    string price = string.Format("{0:c2}", Convert.ToDecimal(r["PriceItem"].ToString()));

                    string Currency = r["Currency"].ToString();
                    string PurchaseOrder = r["PurchaseOrder"].ToString();
                    if (price != "$0.00")
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "System has found below values for the selected item. Would you like to use these values?" + Environment.NewLine + " Price = " + price + Environment.NewLine +
                                                "Currency = " + Currency + Environment.NewLine +
                                                "PO No. = " + PurchaseOrder + Environment.NewLine + "", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            dontstop = false;
                            pricetxt.Text = price;
                            notestxt.Text += Environment.NewLine + string.Format("Price for item {0} is referred from PO# {1}." + Environment.NewLine + "{2}", item, PurchaseOrder, Currency.Length > 0 ? "Currency is in " + Currency + "." : "");
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    iteminfo.Clear();
                }
            }
            dontstop = true;
        }

        private DataTable Getpriceforitem(string itemnumber)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP (1) * FROM [SPM_Database].[dbo].[PriceItemsFromPO] WHERE [Item] = '" + itemnumber + "' order by LastUpdate Desc", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Item Price From PriceItemsPo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            return dt;
        }

        private void Itemsearchtxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (itemsearchtxtbox.Text.Length >= 6)
                {
                    string item = itemsearchtxtbox.Text.Trim().Substring(0, 6);
                    if (CheckItemPresentOnGenius(item))
                    {
                        Clearaddnewtextboxes();
                        Filldatatable(item);
                        if (itemstable.Rows.Count > 0)
                        {
                            Fillinfo();
                            Addnewbttn.Enabled = true;
                            FillPrice(item);
                        }
                        else
                        {
                            MessageBox.Show("Item Not found!!", "SPM Connect", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item Not found on Genius.!! Please make sure to the item you are trying to add exists on Genius in order to be purchased.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Jobnumbertxt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Jobnumbertxt_Leave(object sender, EventArgs e)
        {
            jobnumbertxt.Text = jobnumbertxt.Text.Trim();
        }

        private void Pricetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            //{
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void Pricetxt_Leave(object sender, EventArgs e)
        {
            //Double value;
            //if (Double.TryParse(pricetxt.Text, out value))
            //    pricetxt.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            //else
            //    pricetxt.Text = String.Empty;
        }

        private void Pricetxt_TextChanged(object sender, EventArgs e)
        {
            if (dontstop)
            {
                string value = pricetxt.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0');
                //Check we are indeed handling a number
                if (decimal.TryParse(value, out decimal ul))
                {
                    ul /= 100;
                    //Unsub the event so we don't enter a loop
                    pricetxt.TextChanged -= Pricetxt_TextChanged;
                    //Format the text as currency
                    pricetxt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    pricetxt.TextChanged += Pricetxt_TextChanged;
                    pricetxt.Select(pricetxt.Text.Length, 0);
                }
            }
            bool goodToGo = TextisValid(pricetxt.Text);

            if (!goodToGo)
            {
                pricetxt.Text = "$0.00";
                pricetxt.Select(pricetxt.Text.Length, 0);
            }
        }

        private void Qtytxt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Subassytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (sender as TextBox)?.SelectionStart == 0 && e.KeyChar == (char)Keys.Space;
        }

        private void Subassytxt_Leave(object sender, EventArgs e)
        {
            subassytxt.Text = subassytxt.Text.Trim();
        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        #endregion add items to purchase req button and text events groupbox 3

        #region button click events tool bars

        private void Bttnneedapproval_Click(object sender, EventArgs e)
        {
            PerformNeedApproval(sender);
        }

        private void Bttnshowapproved_Click(object sender, EventArgs e)
        {
            ProcessShowApprovedBttn(sender);
        }

        private async void Bttnshowmydept_Click(object sender, EventArgs e)
        {
            await Task.Run(() => SplashDialog("Loading Data...")).ConfigureAwait(true);
            this.Enabled = false;
            Showmydeptreq();
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.FromArgb(255, 128, 0);
            //t.Abort();
            //this.TopMost = true;
            this.Enabled = true;
            this.Focus();
            this.Activate();
            splashWorkDone = true;
        }

        private void Bttnshowmyreq_Click(object sender, EventArgs e)
        {
            Perfromshowmyreqbuttn();
        }

        private void Newbttn_Click(object sender, EventArgs e)
        {
            Createnew();
        }

        private async void PerformNeedApproval(object sender)
        {
            await Task.Run(() => SplashDialog("Loading Data...")).ConfigureAwait(true);

            this.Enabled = false;
            Showwaitingonapproval();
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.FromArgb(255, 128, 0);
            //t.Abort();
            //this.TopMost = true;
            this.Enabled = true;
            this.Focus();
            this.Activate();

            splashWorkDone = true;
        }

        private async void Perfromshowmyreqbuttn()
        {
            await Task.Run(() => SplashDialog("Loading Data...")).ConfigureAwait(true);

            this.Enabled = false;
            ShowReqSearchItems(ConnectUser.Name);
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            //set the clicked control to a different color
            //Control o = (Control)sender;
            bttnshowmyreq.BackColor = Color.FromArgb(255, 128, 0);
            //t.Abort();
            //this.TopMost = true;
            this.Enabled = true;
            this.Focus();
            this.Activate();
            splashWorkDone = true;
        }

        private void Printbttn_Click(object sender, EventArgs e)
        {
            // this.TopMost = false;
            Reportpurchaereq(reqnumber, "Purchasereq");
        }

        private async void ProcessShowApprovedBttn(object sender)
        {
            await Task.Run(() => SplashDialog("Loading Data...")).ConfigureAwait(true);
            this.Enabled = false;
            Showallapproved();
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.FromArgb(255, 128, 0);
            //t.Abort();
            //this.TopMost = true;
            this.Enabled = true;
            this.Focus();
            this.Activate();
            splashWorkDone = true;
        }

        #endregion button click events tool bars

        #region manager commands to retrieve data

        private void Showallapproved()
        {
            showingwaitingforapproval = false;

            if (ConnectUser.PurchaseReqApproval2)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE (Approved = '1' OR Approved = '3') AND Validate = '1' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Show All Approved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (ConnectUser.PurchaseReqBuyer)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Papproved = '1' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Show All Approved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (ConnectUser.PurchaseReqApproval)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE (Approved = '1' OR Approved = '3') AND Validate = '1' AND SupervisorId = '" + ConnectUser.ConnectId + "' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Show All Approved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
        }

        private void Showmydeptreq()
        {
            showingwaitingforapproval = false;
            if (ConnectUser.PurchaseReqApproval2)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Validate != '5'  ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect-  Show All Dept)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (ConnectUser.PurchaseReqBuyer)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE PApproval = '1'  ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect-  Show All Dept)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
                return;
            }
            else if (ConnectUser.PurchaseReqApproval)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE SupervisorId = '" + ConnectUser.ConnectId + "' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect-  Show All Dept)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
        }

        private void Showwaitingonapproval()
        {
            showingwaitingforapproval = true;

            if (ConnectUser.PurchaseReqApproval2 && !ConnectUser.PurchaseReqApproval)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Approved = '1' AND Validate = '1' AND HApproval = '1' AND Happroved = '0' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (ConnectUser.PurchaseReqApproval2 && ConnectUser.PurchaseReqApproval)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Approved = '1' AND Validate = '1' AND HApproval = '1' AND Happroved = '0' UNION SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Validate = '1' AND Approved = '0' AND SupervisorId = '" + ConnectUser.ConnectId + "' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (ConnectUser.PurchaseReqBuyer)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Approved = '1' AND Validate = '1' AND PApproval = '1' AND Papproved = '0' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (ConnectUser.PurchaseReqApproval && !ConnectUser.PurchaseReqApproval2)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] WHERE Approved = '0' AND Validate = '1' AND SupervisorId = '" + ConnectUser.ConnectId + "' ORDER BY ReqNumber DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                        Preparedatagrid();
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
        }

        #endregion manager commands to retrieve data

        #region Happroval

        private async void Happrovechk_Click(object sender, EventArgs e)
        {
            if (ConnectUser.PurchaseReqApproval2)
            {
                if (!happrovechk.Checked)
                {
                    await Processsavebutton(true, "Happrovedfalse").ConfigureAwait(false);
                }
                else
                {
                    DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to approve this purchase requistion for order?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to requested user attaching the approved purchase req.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string reqno = purchreqtxt.Text;
                        string requestby = requestbytxt.Text;

                        await Processsavebutton(true, "Happroved").ConfigureAwait(false);
                        happrovechk.Checked = true;
                        await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                        this.Enabled = false;

                        string filename = Makefilenameforreport(reqno, false);
                        //SaveReport(reqno, filename);

                        Preparetosendemail(reqno, false, requestby, filename, false, "highautority", false);

                        this.Enabled = true;
                        this.Focus();
                        this.Activate();
                        splashWorkDone = true;
                    }
                    else
                    {
                        happrovechk.Checked = false;
                    }
                }
            }
        }

        private bool Sendemailyesnohauthority()
        {
            bool sendemail = false;
            string yesno = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailHapproval'", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    yesno = (string)cmd.ExecuteScalar();
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Send email higher authority", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            if (yesno == "1")
            {
                sendemail = true;
            }
            return sendemail;
        }

        #endregion Happroval

        #region Pbuyer

        private async void Purchasedchk_Click(object sender, EventArgs e)
        {
            if (ConnectUser.PurchaseReqBuyer)
            {
                if (!purchasedchk.Checked)
                {
                    await Processsavebutton(true, "Papprovedfalse").ConfigureAwait(false);
                }
                else
                {
                    DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to confirm this purchase requistion is placed for order?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to requested user stating that purchase req is on placed in order.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string reqno = purchreqtxt.Text;
                        string requestby = requestbytxt.Text;

                        await Processsavebutton(true, "Papproved").ConfigureAwait(false);
                        DataGridView_SelectionChanged(sender, e);
                        //this.TopMost = false;

                        //Thread t = new Thread(new ThreadStart(Splashemail));
                        //t.Start();
                        await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                        this.Enabled = false;
                        purchasedchk.Checked = true;

                        Preparetosendemail(reqno, false, requestby, "", false, "ConnectUser.PurchaseReqBuyer", false);

                        //t.Abort();
                        //this.TopMost = true;
                        this.Enabled = true;
                        this.Focus();
                        this.Activate();
                        splashWorkDone = true;
                    }
                    else
                    {
                        purchasedchk.Checked = false;
                    }
                }
            }
        }

        private bool Sendemailyesnopbuyer()
        {
            bool sendemail = false;
            string yesno = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailPbuyer'", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    yesno = (string)cmd.ExecuteScalar();
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Send email higher authority", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            if (yesno == "1")
            {
                sendemail = true;
            }
            return sendemail;
        }

        #endregion Pbuyer

        #region export to excel

        private void CopyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void Exporttoexcel()
        {
            try
            {
                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "Excel Documents (*.xls)|*.xls";
                //sfd.FileName = "Inventory_Adjustment_Export.xls";

                string filepath = Getsupervisorsharepath(GetUserName()) + @"\SPM_Connect\PreliminaryPurchases\";
                System.IO.Directory.CreateDirectory(filepath);
                filepath += purchreqtxt.Text + " - " + requestbytxt.Text + ".xls";
                // Copy DataGridView results to clipboard
                CopyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application
                {
                    DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                };
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[2, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("G:G").Cells;
                delRng.Delete();
                delRng = xlWorkSheet.get_Range("F:F").Cells;
                delRng.Delete();
                delRng = xlWorkSheet.get_Range("E:E").Cells;
                delRng.Delete();
                delRng = xlWorkSheet.get_Range("D:D").Cells;
                delRng.Delete();
                delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete();

                Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                rng.NumberFormat = "@";

                xlWorkSheet.Cells[1, 1] = "Item";
                xlWorkSheet.Cells[1, 2] = "AllocatedQuantity";

                //xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(filepath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dataGridView1.ClearSelection();

                // Open the newly saved excel file
                //if (File.Exists(filepath))
                //    System.Diagnostics.Process.Start(filepath);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                //MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Error Saving excel file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string Getsupervisorsharepath(string username)
        {
            string path = "";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    path = dr["SharesFolder"].ToString();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Error Getting share folder path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            return path;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion export to excel

        #region Approval Tool Menu Strip

        private void ApprovalMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 1 || !showingwaitingforapproval)
            {
                e.Cancel = true;
            }
        }

        private async void Approvetoolstrip_Click(object sender, EventArgs e)
        {
            if (!approvechk.Checked)
            {
                if (ConnectUser.PurchaseReqApproval)
                {
                    approvechk.Checked = !approvechk.Checked;

                    if (!approvechk.Checked)
                    {
                        if (Gethapprovedstatus(Convert.ToInt32(purchreqtxt.Text)))
                        {
                            MetroFramework.MetroMessageBox.Show(this, "This purchase requisition is approved by higher authority. Only people at that credentials can edit the details.", "SPM Connect - Purchase Req H-approved", MessageBoxButtons.OK);
                            approvechk.Checked = true;
                            approvechk.Text = "Approved";
                            Processexitbutton();
                        }
                        else
                        {
                            approvechk.Checked = false;
                            approvechk.Text = "Approve";
                            await Processsavebutton(true, "ApprovedFalse").ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to approve this purchase requistion for order?" + Environment.NewLine +
                        " " + Environment.NewLine +
                        "This will send email to requested user attaching the approved purchase req.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            if (jobnumbertxt.Text.Length > 0 && subassytxt.Text.Length > 0)
                            {
                                string reqno = purchreqtxt.Text;
                                string requestby = requestbytxt.Text;
                                bool happroval = Happroval();
                                await Processsavebutton(true, "Approved").ConfigureAwait(false);
                                approvechk.Checked = true;
                                await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                                this.Enabled = false;

                                string filename = Makefilenameforreport(reqno, false);
                                SaveReport(reqno, filename);
                                Preparetosendemail(reqno, false, requestby, filename, happroval, "ConnectUser.PurchaseReqApproval", false);
                                Exporttoexcel();
                                this.Enabled = true;
                                this.Focus();
                                this.Activate();
                                splashWorkDone = true;
                            }
                            else
                            {
                                errorProvider1.Clear();
                                if (jobnumbertxt.Text.Length > 0)
                                {
                                    errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                                }
                                else if (subassytxt.Text.Length > 0)
                                {
                                    errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                                }
                                else
                                {
                                    errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                                    errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                                }

                                approvechk.Checked = false;
                            }
                        }
                        else
                        {
                            approvechk.Checked = false;
                        }
                    }
                }
            }
            else if (approvechk.Checked)
            {
                if (ConnectUser.PurchaseReqApproval2)
                {
                    happrovechk.Checked = !happrovechk.Checked;

                    if (!happrovechk.Checked)
                    {
                        await Processsavebutton(true, "Happrovedfalse").ConfigureAwait(false);
                    }
                    else
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to approve this purchase requistion for order?" + Environment.NewLine +
                        " " + Environment.NewLine +
                        "This will send email to requested user attaching the approved purchase req.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            string reqno = purchreqtxt.Text;
                            string requestby = requestbytxt.Text;

                            await Processsavebutton(true, "Happroved").ConfigureAwait(false);
                            happrovechk.Checked = true;
                            await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                            this.Enabled = false;

                            string filename = Makefilenameforreport(reqno, false);
                            //SaveReport(reqno, filename);

                            Preparetosendemail(reqno, false, requestby, filename, false, "highautority", false);

                            // t.Abort();
                            // this.TopMost = true;
                            this.Enabled = true;
                            this.Focus();
                            this.Activate();
                            splashWorkDone = true;
                        }
                        else
                        {
                            happrovechk.Checked = false;
                        }
                    }
                }
            }
        }

        private async void Rejecttoolstrip_Click(object sender, EventArgs e)
        {
            if (!approvechk.Checked)
            {
                if (ConnectUser.PurchaseReqApproval)
                {
                    approvechk.Checked = !approvechk.Checked;

                    if (!approvechk.Checked)
                    {
                        if (Gethapprovedstatus(Convert.ToInt32(purchreqtxt.Text)))
                        {
                            MetroFramework.MetroMessageBox.Show(this, "This purchase requisition is approved by higher authority. Only people at that credentials can edit the details.", "SPM Connect - Purchase Req H-approved", MessageBoxButtons.OK);
                            approvechk.Checked = true;
                            approvechk.Text = "Approved";
                            Processexitbutton();
                        }
                        else
                        {
                            approvechk.Checked = false;
                            approvechk.Text = "Approve";
                            await Processsavebutton(true, "ApprovedFalse").ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to reject this purchase requistion from ordering?" + Environment.NewLine +
                        " " + Environment.NewLine +
                        "This will send email to requested user stating that purchase req is rejected.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (result == DialogResult.Yes)
                        {
                            if (jobnumbertxt.Text.Length > 0 && subassytxt.Text.Length > 0)
                            {
                                string reqno = purchreqtxt.Text;
                                string requestby = requestbytxt.Text;
                                bool happroval = Happroval();
                                await Processsavebutton(true, "Rejected").ConfigureAwait(false);
                                approvechk.Checked = true;
                                await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                                this.Enabled = false;

                                //string filename = makefilenameforreport(reqno, false).ToString();
                                //SaveReport(reqno, filename);
                                Preparetosendemail(reqno, false, requestby, "", happroval, "ConnectUser.PurchaseReqApproval", true);
                                //exporttoexcel();
                                //t.Abort();
                                //this.TopMost = true;
                                this.Enabled = true;
                                this.Focus();
                                this.Activate();
                                splashWorkDone = true;
                            }
                            else
                            {
                                errorProvider1.Clear();
                                if (jobnumbertxt.Text.Length > 0)
                                {
                                    errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                                }
                                else if (subassytxt.Text.Length > 0)
                                {
                                    errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                                }
                                else
                                {
                                    errorProvider1.SetError(jobnumbertxt, "Job Number cannot be empty");
                                    errorProvider1.SetError(subassytxt, "Sub Assy No cannot be empty");
                                }

                                approvechk.Checked = false;
                            }
                        }
                        else
                        {
                            approvechk.Checked = false;
                        }
                    }
                }
            }
            else if (approvechk.Checked)
            {
                if (ConnectUser.PurchaseReqApproval2)
                {
                    happrovechk.Checked = !happrovechk.Checked;

                    if (!happrovechk.Checked)
                    {
                        await Processsavebutton(true, "Happrovedfalse").ConfigureAwait(false);
                    }
                    else
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to reject this purchase requistion from ordering?" + Environment.NewLine +
                        " " + Environment.NewLine +
                        "This will send email to requested user stating that purchase req is rejected.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (result == DialogResult.Yes)
                        {
                            string reqno = purchreqtxt.Text;
                            string requestby = requestbytxt.Text;

                            await Processsavebutton(true, "HRejected").ConfigureAwait(false);
                            happrovechk.Checked = true;
                            await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                            this.Enabled = false;

                            //string filename = makefilenameforreport(reqno, false).ToString();
                            //SaveReport(reqno, filename);

                            Preparetosendemail(reqno, false, requestby, "", false, "highautority", true);
                            this.Enabled = true;
                            this.Focus();
                            this.Activate();
                            splashWorkDone = true;
                        }
                        else
                        {
                            happrovechk.Checked = false;
                        }
                    }
                }
            }
        }

        #endregion Approval Tool Menu Strip

        private void Itemsearchtxtbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                itemsearchtxtbox.Focus();
            }
        }
    }
}