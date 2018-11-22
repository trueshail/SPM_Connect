using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class PurchaseReqform : Form
    {
        PurchaseReq model = new PurchaseReq();
        String connection;
        SqlConnection cn;
        SqlDataAdapter _adapter;
        DataTable itemstable = new DataTable();
        DataTable dt;

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

        }

        string userfullname;

        private void PurchaseReq_Load(object sender, EventArgs e)
        {
            Clear();
            showReqSearchItems();
            userfullname = getuserfullname(get_username().ToString()).ToString();
        }

        private void showReqSearchItems()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[PurchaseReqBase] ORDER BY ReqNumber DESC", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    dataGridView.DataSource = dt;
                    DataView dv = dt.DefaultView;
                    dataGridView.Columns[0].Width = 60;
                    dataGridView.Columns[0].HeaderText = "Req No";
                    dataGridView.Columns[1].Width = 60;
                    dataGridView.Columns[2].Width = 80;
                    dataGridView.Columns[3].Width = 80;
                    dataGridView.Columns[4].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    dataGridView.Columns[6].Visible = false;
                    dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
                    UpdateFont();

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
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0 && dataGridView.SelectedCells.Count == 1)
            {
                dataGridView1.AutoGenerateColumns = false;
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                int item = Convert.ToInt32(slectedrow.Cells[0].Value);
                populatereqdetails(item);
                PopulateDataGridView();

                tabControl1.Visible = true;
                if (tabControl1.TabPages.Count == 0)
                {
                    tabControl1.TabPages.Add(PreviewTabPage);
                }
                checkforeditrights();
            }
        }

        private String getusernamefromgrid()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string username;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                username = Convert.ToString(slectedrow.Cells[2].Value);
                //MessageBox.Show(ItemNo);
                return username;
            }
            else
            {
                username = "";
                return username;
            }
        }

        private void checkforeditrights()
        {
            if (getusernamefromgrid().ToString() == userfullname)
            {
                editbttn.Visible = true;

            }
            else
            {
                editbttn.Visible = false;
            }
        }

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

        private object getuserfullname(string username)
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
                    showReqSearchItems();
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

        private void createNewPurchaseReqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to create a new purchase req?", "SPM Connect - Create New?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //clearitemsbeforenewreq();
                //Clear();

                ecitbttn.Visible = false;
                int lastreq = getlastreqnumber();
                createnewreq(lastreq, userfullname.ToString());
                showReqSearchItems();
                selectrowbeforeediting(lastreq.ToString());
                populatereqdetails(lastreq);
                PopulateDataGridView();
                processeditbutton(false);
                //PreviewTabPage.Text = "New Req : " + lastreq;
                //purchreqtxt.Text = lastreq.ToString();
                //fillitemssource();
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

        void calculatetota()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int total = 0;
                int qty = 1;
                int price = 0;
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
                            price = Convert.ToInt32(row.Cells[7].Value.ToString());
                        }
                        else
                        {
                            price = 0;
                        }
                        total += (qty * price);
                        totalcostlbl.Text = "Total Cost : $" + string.Format("{0:#.00}", Convert.ToDecimal(total.ToString()));
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

        void clearitemsbeforenewreq()
        {
            purchreqtxt.Clear();
            requestbytxt.Clear();
            lastsavedtxt.Clear();
            datecreatedtxt.Clear();
            jobnumbertxt.Clear();
            subassytxt.Clear();

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
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string jobnumber = "";
            string subassy = "";
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[PurchaseReqBase] (ReqNumber, RequestedBy, DateCreated, DateLastSaved, JobNumber, SubAssyNumber) VALUES('" + reqnumber + "','" + employee.ToString() + "','" + sqlFormattedDate + "','" + sqlFormattedDate + "','" + jobnumber + "','" + subassy + "'  )";
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

        private void UpdateReq(int reqnumber)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string jobnumber = jobnumbertxt.Text.Trim();
            string subassy = subassytxt.Text.Trim();
            string notes = notestxt.Text;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[PurchaseReqBase] SET DateLastSaved = '" + sqlFormattedDate + "',JobNumber = '" + jobnumber + "',SubAssyNumber = '" + subassy + "' ,Notes = '" + notes + "' WHERE ReqNumber = '" + reqnumber + "' ";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            processeditbutton(true);
        }

        void processeditbutton(bool showexit)
        {
            dataGridView1.ContextMenuStrip = FormSelector;
            PurchaseReqSearchTxt.Enabled = false;
            dataGridView.Enabled = false;
            editbttn.Visible = false;
            jobnumbertxt.ReadOnly = false;
            subassytxt.ReadOnly = false;
            notestxt.ReadOnly = false;
            jobnumbertxt.SelectionStart = jobnumbertxt.Text.Length;
            subassytxt.SelectionStart = subassytxt.Text.Length;
            groupBox3.Visible = true;
            savebttn.Visible = true;
            fillitemssource();
            MenuStrip.Enabled = true;
            MenuStrip.Visible = false;
            if (showexit)
            {
                ecitbttn.Visible = true;
            }
        }

        private void savebttn_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (jobnumbertxt.Text.Trim().Length > 0 && subassytxt.Text.Trim().Length > 0)
            {
                UpdateReq(Convert.ToInt32(purchreqtxt.Text));
                showReqSearchItems();
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

        private void ecitbttn_Click(object sender, EventArgs e)
        {
            processexitbutton();
        }

        void processexitbutton()
        {
            tabControl1.TabPages.Remove(PreviewTabPage);
            dataGridView.Enabled = true;
            groupBox3.Visible = false;
            editbttn.Visible = false;
            savebttn.Visible = false;
            ecitbttn.Visible = false;
            tabControl1.Visible = true;
            MenuStrip.Enabled = true;
            MenuStrip.Visible = true;
            PurchaseReqSearchTxt.Enabled = true;
            dataGridView1.ContextMenuStrip = null;
            Clear();
            if (tabControl1.TabPages.Count == 0)
            {
                tabControl1.TabPages.Add(PreviewTabPage);
            }
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
            qtytxt.Clear();
        }

        private void pricetxt_KeyPress(object sender, KeyPressEventArgs e)
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
            int resultqty = 0;
            int result = 0;
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
                if (int.TryParse(pricetxt.Text, out result))
                    model.Price = result;
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

        void populatereqdetails(int item)
        {
            DataRow[] dr = dt.Select("ReqNumber = '" + item + "'");
            purchreqtxt.Text = dr[0]["ReqNumber"].ToString();
            requestbytxt.Text = dr[0]["RequestedBy"].ToString();
            datecreatedtxt.Text = dr[0]["DateCreated"].ToString();
            jobnumbertxt.Text = dr[0]["JobNumber"].ToString();
            subassytxt.Text = dr[0]["SubAssyNumber"].ToString();
            lastsavedtxt.Text = dr[0]["DateLastSaved"].ToString();
            notestxt.Text = dr[0]["Notes"].ToString();
        }

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

            PreviewTabPage.Text = "ReqNo : " + item;
            UpdateFontdataitems();
            calculatetota();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            processdeletebttn();
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
            }
            else
            {
                Clear();
                Addnewbttn.Enabled = false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            Addnewbttn.Enabled = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            getitemsfromgrid();

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
                        pricetxt.Text = model.Price.ToString();
                        qtytxt.Text = model.Qty.ToString();

                    }
                    Addnewbttn.Enabled = true;
                    Addnewbttn.Text = "Update";
                    btnDelete.Enabled = true;
                }
            }

        }

        private void jobnumbertxt_Leave(object sender, EventArgs e)
        {
            jobnumbertxt.Text = jobnumbertxt.Text.Trim();
        }

        private void subassytxt_Leave(object sender, EventArgs e)
        {
            subassytxt.Text = subassytxt.Text.Trim();
        }

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

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            getitemsfromgrid();
            processdeletebttn();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            getitemsfromgrid();
            qtytxt.Focus();
            qtytxt.SelectAll();
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

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

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

        private void PurchaseReqform_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
