using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
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
            //fillitemssource();
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
                    dataGridView.Columns[1].Width = 60;
                    dataGridView.Columns[2].Width = 80;
                    dataGridView.Columns[3].Width = 80;
                    dataGridView.Columns[4].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    UpdateFont();

                }
                catch (Exception)
                {
                    MessageBox.Show("Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Engineering Load(showallitems)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();

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

        private static String get_username()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
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
            editbttn.Visible = false;
            dataGridView1.Enabled = false;
            dataGridView1.Refresh();
            PreviewTabPage.Text = "New Req : ";
            ecitbttn.Visible = true;
            savebttn.Visible = true;
            groupBox3.Visible = true;
            fillitemssource();
        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            dataGridView.Enabled = false;
            editbttn.Visible = false;
            groupBox3.Visible = true;
            savebttn.Visible = true;
            ecitbttn.Visible = true;
            fillitemssource();
            MenuStrip.Enabled = true;
            MenuStrip.Visible = false;
        }

        private void savebttn_Click(object sender, EventArgs e)
        {
            ecitbttn.Visible = false;
            savebttn.Visible = false;
            editbttn.Visible = true;
            clearaddnewtextboxes();
        }

        private void ecitbttn_Click(object sender, EventArgs e)
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
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Items Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clearaddnewtextboxes();
                filldatatable(itemsearchtxtbox.Text.Trim().Substring(0, 6).ToString());
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            //itemsearchtxtbox.Clear();
            Descriptiontxtbox.Clear();
            oemitemnotxt.Clear();
            oemtxt.Clear();
            pricetxt.Clear();
            qtytxt.Clear();
        }

        private void pricetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9\.+\b]"))
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
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9\.+\b]"))
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
            int maxSlNo = dataGridView1.Rows.Count;
            maxSlNo++;
            //int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            //MessageBox.Show(this.dataGridView1.Rows[selectedrowindex].HeaderCell.Value.ToString());
            model.OrderId =maxSlNo;
            model.Item = ItemTxtBox.Text.Trim();
            model.Description = Descriptiontxtbox.Text.Trim();
            model.Manufacturer = oemtxt.Text.Trim();
            model.OEMItemNumber = oemitemnotxt.Text.Trim();
            int result = 0;
            int resultqty = 0;
            if (int.TryParse(pricetxt.Text, out result))
            if (int.TryParse(qtytxt.Text, out resultqty))

            model.Price = result;
            model.Qty = resultqty;
            model.ReqNumber = Convert.ToInt32(purchreqtxt.Text);
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
            PopulateDataGridView();
            MessageBox.Show("Submitted Successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void Clear()
        {
            itemsearchtxtbox.Text = ItemTxtBox.Text = Descriptiontxtbox.Text = oemtxt.Text = oemitemnotxt.Text = pricetxt.Text = qtytxt.Text = "";
            Addnewbttn.Text = "Add";
            btnDelete.Enabled = false;
            model.ID = 0;
        }

        void PopulateDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            int item = Convert.ToInt32(slectedrow.Cells[0].Value);
            purchreqtxt.Text = item.ToString();

            using (SPM_DatabaseEntitiesPurchase db = new SPM_DatabaseEntitiesPurchase())
            {
                dataGridView1.DataSource = db.PurchaseReqs.Where(s => s.ReqNumber == item ).ToList<PurchaseReq>();
               

            }
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            //}
            
            PreviewTabPage.Text = "ReqNo : " + item;
            UpdateFontdataitems();
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Deleted Successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
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
                Addnewbttn.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void updateorderid(int reqnumber)
        {
            using (SqlCommand sqlCommand = new SqlCommand("[SPM_Database].[dbo].[UpdateOrderId]", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
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

    }
}
