using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{

	public partial class ItemInfo : Form

	{
        #region steupvariables
        String connection;		
        DataTable _acountsTb;
        DataTable dt = new DataTable();
        SqlConnection _connection;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        TreeNode root = new TreeNode();
        string iteminfo2;
        int PW;
        bool hiden;
      

        #endregion
        
        public ItemInfo()

		{
			InitializeComponent();
            PW = 500;
            hiden = true;
            SlidePanel.Width = 0;

			connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
			
			try
			{
                _connection = new SqlConnection(connection);

			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}

			
            _acountsTb = new DataTable();            
            _command = new SqlCommand();
            _command.Connection = _connection;
            

            
        }

        public string item(string item)
        {
            if (item.Length > 0)
                return iteminfo2 = item;
            return null;
        }

        private void ParentView_Load(object sender, EventArgs e)
		{
            this.Text = "ItemInfo - SPM Connect ("+iteminfo2+")";
            CheckManagement();
            if (yesmanagement) { showitemstogridview(iteminfo2); }
            
            checkfordata(iteminfo2);


            // filldatatable(iteminfo2);

        }

        bool yesmanagement = false;

        private void CheckManagement()
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Management = '1'", _connection))
            {
                try
                {
                    _connection.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {

                        panel1.Visible = true;
                        yesmanagement = true;

                    }
                    else
                    {
                        panel1.Visible = false;
                        yesmanagement = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    _connection.Close();
                }

            }
        }

        private void showitemstogridview(string itemnumber)
        {
            String sql = "SELECT * FROM [SPM_Database].[dbo].[Item_CostRollup] WHERE ItemId ='" + itemnumber.ToString() + "'";
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(sql, _connection);

                dt.Clear();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Descending);
                //dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[4].Width = 200;
                dataGridView1.Columns[5].Width = 160;
                dataGridView1.Columns[6].Width = 160;
                UpdateFont();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER ENG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
            }
            finally
            {
                _connection.Close();
            }

            //dataGridView.Location = new Point(0, 40);

        }

        private void UpdateFont()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        private void checkfordata(string itemnumber)
        {

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemnumber.ToString() + "'", _connection))
            {
                try
                {
                    _connection.Open();
                    

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        
                        _connection.Close();
                        filldatatable(iteminfo2);

                    }
                    else
                    {
                        SPM_Connect sPM_Connect = new SPM_Connect();
                        sPM_Connect.addcpoieditemtosqltablefromgenius(iteminfo2,iteminfo2);                        
                        filldatatable(iteminfo2);
                        //MessageBox.Show("Data not found on SPM Connect server.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //this.Close();
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Not Licensed User", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
              

            }

        }

        private void filldatatable(string itemnumber)
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='"+itemnumber.ToString()+"'";

            // String sql2 = "SELECT *  FROM [SPM_Database].[dbo].[UnionInventory]";
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                _adapter = new SqlDataAdapter(sql, _connection);
                _adapter.Fill(_acountsTb);
                fillinfo();
               
               

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                _connection.Close();
            }
        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        private void fillinfo()
        {
            DataRow r = _acountsTb.Rows[0];
            ItemTxtBox.Text = r["ItemNumber"].ToString();
            Descriptiontxtbox.Text = r["Description"].ToString();
            oemtxtbox.Text = r["Manufacturer"].ToString();
            oemitemtxtbox.Text = r["ManufacturerItemNumber"].ToString();
            familytxtbox.Text = r["FamilyCode"].ToString();            
            sparetxtbox.Text = r["Spare"].ToString();
            mattxt.Text = r["Material"].ToString();
            designbytxt.Text = r["DesignedBy"].ToString();
            categorytxtbox.Text = r["FamilyType"].ToString();
            surfacetxt.Text = r["SurfaceProtection"].ToString();
            heattreat.Text = r["HeatTreatment"].ToString();
            datecreatedtxt.Text = r["DateCreated"].ToString();
            dateeditxt.Text = r["LastEdited"].ToString();
            Lastsavedtxtbox.Text = r["LastSavedBy"].ToString();
            notestxt.Text = r["Notes"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!hiden)
            {
                button1.Text = "S\nH\nO\nW\n A\nD\nV\nA\nN\nC\nE\n O\nP\nT\nI\nO\nN\nS";
            }
            else
            {
                button1.Text = "H\nI\nD\nE\n A\nD\nV\nA\nN\nC\nE\n O\nP\nT\nI\nO\nN\nS";
               
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hiden)
            {
                SlidePanel.Width = SlidePanel.Width + 50;
                if(SlidePanel.Width >= PW)
                {
                    timer1.Stop();
                    hiden = false;
                    this.Refresh();
                }
            }
            else
            {
                SlidePanel.Width = SlidePanel.Width - 50;
                if(SlidePanel.Width <= 0)
                {
                    timer1.Stop();
                    hiden = true;
                    this.Refresh();
                }
            }
        }

        private string getuserfullname(string username)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                SqlCommand cmd = _connection.CreateCommand();
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
                _connection.Close();
            }
            return null;
        }

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if(salepricetext.Text.Length>0 && qtytxt.Text.Length > 0)
            {
                string itemnumber = ItemTxtBox.Text;
                string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string userfullname = getuserfullname(user).ToString();
                createnewentry(itemnumber, userfullname);
                clearalltextboxes();
                showitemstogridview(itemnumber);
            }
            else
            {
                errorProvider1.SetError(costpricetxt, "Cannot be null");
                errorProvider1.SetError(salepricetext, "Cannot be null");
                MessageBox.Show("SalesPrice and quantity cannot be empty in order to add new entry.","SPM Connect",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }

        private void createnewentry(string itemnumber, string user)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            try
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Item_CostRollup] (ItemId, Cost, SalesPrice, Qty, Notes, Date, LastSavedBy) VALUES('" + itemnumber.ToString() + "','" + costpricetxt.Text.ToString() + "','" + salepricetext.Text.ToString() + "','" + qtytxt.Text.ToString() + "','" + NotesCosttxt.Text.ToString() + "','" + sqlFormattedDate + "','" + user.ToString() + "')";
                cmd.ExecuteNonQuery();
                _connection.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }

        }

        private void clearalltextboxes()
        {
            costpricetxt.Clear();
            salepricetext.Clear();
            qtytxt.Clear();
            NotesCosttxt.Clear();

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

        private void salepricetext_KeyPress(object sender, KeyPressEventArgs e)
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

        private void costpricetxt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void costpricetxt_Leave(object sender, EventArgs e)
        {
            double amount = 0.0d;
            if (Double.TryParse(costpricetxt.Text, NumberStyles.Currency, null, out amount))
            {
                costpricetxt.Text = amount.ToString("C");
            }
        }

        private void salepricetext_Leave(object sender, EventArgs e)
        {

            double amount = 0.0d;
            if (Double.TryParse(salepricetext.Text, NumberStyles.Currency, null, out amount))
            {
                salepricetext.Text = amount.ToString("C");
            }

            if(salepricetext.Text.Length == 0)
            {

            }

        }

        private void ItemInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }

}
