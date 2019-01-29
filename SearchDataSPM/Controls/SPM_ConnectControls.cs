
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;

namespace SearchDataSPM
{

    public partial class SPM_ConnectControls : Form

    {
        #region SPM Connect Load

        String connection;
        String cntrlconnection;
        SqlConnection cn;
        DataTable dt;
        DataTable _controlsTb = null;
        SqlConnection _connection;
        SqlCommand _command;


        public SPM_ConnectControls()

        {
            InitializeComponent();
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            cntrlconnection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cntrlscn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
                _controlsTb = new DataTable();
                _connection = new SqlConnection(cntrlconnection);
                _command = new SqlCommand();
                _command.Connection = _connection;

            }
            catch (Exception)
            {

                //MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect - Controls", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(0);

            }
            dt = new DataTable();

        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            Showallitems();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            checkadmin();
            this.Text = "SPM Connect Controls - " + userName.ToString().Substring(4);
            chekin("SPM Connect Controls", userName);
            txtSearch.Focus();
            sqlnotifier();

        }

        private void Showallitems()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UnionInventory] ORDER BY ItemNumber DESC", cn);
                dt.Clear();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.Sort(itemNumberDataGridViewTextBoxColumn, ListSortDirection.Descending);
                UpdateFont();
            }
            catch (Exception)
            {
                // MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER CONTROLS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                cn.Close();
            }

            //dataGridView.Location = new Point(0, 40);

        }

        private void Reload_Click(object sender, EventArgs e)
        {
            clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dataGridView.Refresh();
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

        #endregion

        #region Sql Notifier

        SqlTableDependency<UserControl> _dependency;

        public void sqlnotifier()
        {

            // The mapper object is used to map model properties 
            // that do not have a corresponding table column name.
            // In case all properties of your model have same name 
            // of table columns, you can avoid to use the mapper.
            var mapper = new ModelToTableMapper<UserControl>();
            mapper.AddMapping(c => c.username, "User Name");
            mapper.AddMapping(c => c.computername, "Computer Name");

            // Here - as second parameter - we pass table name: 
            // this is necessary only if the model name is different from table name 
            // (in our case we have Customer vs Customers). 
            // If needed, you can also specifiy schema name.

            _dependency = new SqlTableDependency<UserControl>(connection, tableName: "Checkin", mapper: mapper);
            _dependency.OnChanged += _dependency_OnChanged;
            _dependency.OnError += _dependency_OnError;
            _dependency.Start();
            //using (var dep = new SqlTableDependency<cust>(_con, tableName: "USER", mapper: mapper))
            //{
            //    dep.OnChanged += Changed;
            //    dep.Start();

            //    Console.WriteLine("Press a key to exit");
            //    Console.ReadKey();

            //    dep.Stop();
            //}
        }

        private void _dependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<UserControl> e)
        {
            string changed;
            var changedEntity = e.Entity;
            //Console.WriteLine("DML operation: " + e.ChangeType);
            // //Console.WriteLine("ID: " + changedEntity.Id);
            //Console.WriteLine("Name: " + changedEntity.Name);
            //Console.WriteLine("Surame: " + changedEntity.Surname);
            string type = e.ChangeType.ToString();
            changed = string.Format((changedEntity.username));
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            // MessageBox.Show(changed);
            if (changed == userName && type == "Delete")
            {
                //MessageBox.Show(this,"Developer has kicked you out due to maintenance issues.");
                _dependency.Stop();
                System.Environment.Exit(0);
            }
        }

        private void _dependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            throw e.Error;
        }

        #endregion

        #region Public Table & variables

        // variables required outside the functions to perfrom
        string fullsearch = ("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'");
        string ItemNo;
        string description;
        string Manufacturer;
        string oem;
        string family;
        DataTable table0 = new DataTable();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();


        #endregion

        #region Search Parameters

        public void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (txtSearch.Text == "playgame")
                {
                    lettergame lettergame = new lettergame();
                    lettergame.Show();
                    return;
                }

                if (Descrip_txtbox.Visible == true)
                {
                    clearandhide();
                }

                if (txtSearch.Text.Length > 0)
                {

                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");

                }

                Showallitems();
                if (txtSearch.Text.Length > 0)
                {

                    mainsearch();
                }
                else
                {
                    SearchStringPosition(txtSearch.Text);
                    searchtext(txtSearch.Text);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void clearandhide()
        {

            Descrip_txtbox.Hide();
            Descrip_txtbox.Clear();
            filteroem_txtbox.Hide();
            filteroem_txtbox.Clear();
            filteroemitem_txtbox.Hide();
            filteroemitem_txtbox.Clear();
            filter4.Hide();
            filter4.Clear();
            //dt.Clear();
            table0.Clear();
            table1.Clear();
            table2.Clear();
            table3.Clear();
            recordlabel.Text = "";
        }

        private void mainsearch()
        {

            DataView dv = dt.DefaultView;
            dt = dv.ToTable();
            string search1 = txtSearch.Text;
            if (search1.Length > 3)
            {

                if (Char.IsLetter(search1.FirstOrDefault()) && search1.Substring(3, 1) == "%")
                {

                    try
                    {
                        string fullsearch1 = ("ItemNumber LIKE '%{0}%'");
                        search1 = search1.Replace("'", "''");
                        search1 = search1.Replace("[", "[[]");
                        dv.RowFilter = string.Format(fullsearch1, search1);

                        table0 = dv.ToTable();
                        dataGridView.DataSource = dv;
                        dataGridView.Update();
                        SearchStringPosition(txtSearch.Text.Substring(0, txtSearch.Text.Length - 1));
                        searchtext(txtSearch.Text.Substring(0, txtSearch.Text.Length - 1));
                        dataGridView.Refresh();
                        recordlabel.Text = "Found " + table0.Rows.Count.ToString() + " Matching Items.";
                    }
                    catch (Exception)

                    {
                        MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                        txtSearch.Clear();
                        SendKeys.Send("~");
                    }
                    finally
                    {
                        search1 = null;
                        dv = null;
                    }
                }
                else
                {
                    try
                    {
                        search1 = search1.Replace("'", "''");
                        search1 = search1.Replace("[", "[[]");
                        dv.RowFilter = string.Format(fullsearch, search1);

                        table0 = dv.ToTable();
                        dataGridView.DataSource = dv;
                        dataGridView.Update();
                        SearchStringPosition(search1);
                        searchtext(search1);
                        dataGridView.Refresh();
                        recordlabel.Text = "Found " + table0.Rows.Count.ToString() + " Matching Items.";
                    }
                    catch (Exception)

                    {
                        MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                        txtSearch.Clear();
                        SendKeys.Send("~");
                    }
                    finally
                    {
                        search1 = null;
                        dv = null;
                    }
                }
            }
            else
            {
                try
                {
                    search1 = search1.Replace("'", "''");
                    search1 = search1.Replace("[", "[[]");
                    dv.RowFilter = string.Format(fullsearch, search1);

                    table0 = dv.ToTable();
                    dataGridView.DataSource = dv;
                    dataGridView.Update();
                    SearchStringPosition(search1);
                    searchtext(search1);
                    dataGridView.Refresh();
                    recordlabel.Text = "Found " + table0.Rows.Count.ToString() + " Matching Items.";
                }
                catch (Exception)

                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                    txtSearch.Clear();
                    SendKeys.Send("~");
                }
                finally
                {
                    search1 = null;
                    dv = null;
                }
            }
        }

        private void Descrip_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table0.DefaultView;
            table0 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                string search2 = Descrip_txtbox.Text;
                try
                {
                    search2 = search2.Replace("'", "''");
                    search2 = search2.Replace("[", "[[]");
                    var secondFilter = string.Format(fullsearch, search2);
                    if (dv.RowFilter == null || dv.RowFilter.Length == 0)
                        dv.RowFilter = secondFilter;
                    else
                        dv.RowFilter += " AND " + secondFilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition(Descrip_txtbox.Text);
                    searchtext(Descrip_txtbox.Text);
                    table1 = dv.ToTable();
                    dataGridView.Refresh();
                    recordlabel.Text = "Found " + table1.Rows.Count.ToString() + " Matching Items.";
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search2");
                    Descrip_txtbox.Clear();
                    SendKeys.Send("~");
                }
                finally
                {
                    search2 = null;
                    dv = null;
                }

                if (Descrip_txtbox.Text.Length > 0)
                {
                    filteroem_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    filteroem_txtbox.Hide();
                    filteroemitem_txtbox.Hide();
                    filter4.Hide();
                }
                if (Descrip_txtbox.Visible == (false))
                {
                    filteroem_txtbox.Hide();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void filteroem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table1.DefaultView;
            table1 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                string search3 = filteroem_txtbox.Text;
                try
                {
                    search3 = search3.Replace("'", "''");
                    search3 = search3.Replace("[", "[[]");
                    var thirdFilter = string.Format(fullsearch, search3);
                    if (dv.RowFilter == null || dv.RowFilter.Length == 0)
                        dv.RowFilter = thirdFilter;
                    else
                        dv.RowFilter += " AND " + thirdFilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition(filteroem_txtbox.Text);
                    searchtext(filteroem_txtbox.Text);
                    table2 = dv.ToTable();
                    dataGridView.Refresh();
                    recordlabel.Text = "Found " + table2.Rows.Count.ToString() + " Matching Items.";
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search3");
                    filteroem_txtbox.Clear();
                    SendKeys.Send("~");
                }
                finally
                {
                    search3 = null;
                    dv = null;
                }


                if (filteroem_txtbox.Text.Length > 0)
                {
                    filteroemitem_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    filteroemitem_txtbox.Hide();
                    filter4.Hide();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void filteroemitem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table2.DefaultView;
            table2 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                string search4 = filteroemitem_txtbox.Text;
                try
                {
                    search4 = search4.Replace("'", "''");
                    search4 = search4.Replace("[", "[[]");
                    var fourthfilter = string.Format(fullsearch, search4);

                    if (dv.RowFilter == null || dv.RowFilter.Length == 0)
                        dv.RowFilter = fourthfilter;
                    else
                        dv.RowFilter += " AND " + fourthfilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition(filteroemitem_txtbox.Text);
                    searchtext(filteroemitem_txtbox.Text);
                    table3 = dv.ToTable();
                    dataGridView.Refresh();
                    recordlabel.Text = "Found " + table3.Rows.Count.ToString() + " Matching Items.";
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search4");
                    filteroemitem_txtbox.Clear();
                    SendKeys.Send("~");
                }
                finally
                {
                    search4 = null;
                    dv = null;
                }

                if (filteroemitem_txtbox.Text.Length > 0)
                {
                    filter4.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    filter4.Hide();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void filter4_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table3.DefaultView;
            table3 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                string search5 = filter4.Text;
                try
                {
                    search5 = search5.Replace("'", "''");
                    search5 = search5.Replace("[", "[[]");
                    var fifthfilter = string.Format(fullsearch, search5);

                    if (dv.RowFilter == null || dv.RowFilter.Length == 0)
                        dv.RowFilter = fifthfilter;
                    else
                        dv.RowFilter += " AND " + fifthfilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition(filter4.Text);
                    searchtext(filter4.Text);
                    dataGridView.Refresh();
                    recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search5");
                    filter4.Clear();
                    SendKeys.Send("~");
                }
                finally
                {
                    search5 = null;
                    dv = null;
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }


        #endregion

        #region ADD ITEM TO CATALOG - GetItemInfo and datagridview events

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedCells.Count == 1 && c == "0")
            {
                GetRowInfo();
            }

        }

        private void GetRowInfo()
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
            description = Convert.ToString(slectedrow.Cells[1].Value);
            family = Convert.ToString(slectedrow.Cells[2].Value);
            Manufacturer = Convert.ToString(slectedrow.Cells[3].Value);
            oem = Convert.ToString(slectedrow.Cells[4].Value);
            DialogResult result = MessageBox.Show(
                "ItemNumber = " + ItemNo + Environment.NewLine +
                "Description = " + description + Environment.NewLine +
                "Family = " + family + Environment.NewLine +
                "Manufacturer = " + Manufacturer + Environment.NewLine +
                "OEM = " + oem, "Add To AutoCad Catalog?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //code for Yes
                CheckAutoCad();
            }
            else if (result == DialogResult.No)
            {
                //code for No
            }
        }

        private void CheckAutoCad()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Count(*) from [SPMControlCatalog].[dbo].[SPM-Catalog] where (QUERY2 = @item) AND (ASSEMBLYCODE IS NULL OR ASSEMBLYCODE = '') AND (ASSEMBLYLIST IS NULL OR ASSEMBLYLIST = '') ", _connection))
            {
                _connection.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);
                //  sqlCommand.Parameters.AddWithValue("@code", null);
                // sqlCommand.Parameters.AddWithValue("@list", null);
                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    //MessageBox.Show("Item already exists on the catalog." + Environment.NewLine + "Item properties updated!","SPM Connect",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _connection.Close();
                    //call for update
                    UpdateToAutocad();
                }
                else
                {
                    //call Genius check item
                    // MessageBox.Show("not exists");
                    _connection.Close();
                    CheckItemOnGenius();
                }
            }
        }

        private void CheckItemOnGenius()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPMDB].[dbo].[Edb] WHERE Item = @item ", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    // MessageBox.Show("items exists");
                    cn.Close();
                    //insert to autocad catalog
                    InsertToAutocad();
                }
                else
                {
                    //call Genius check item
                    DialogResult result = MessageBox.Show(
                                "ItemNumber = " + ItemNo + ". Does not exist on Genius. Please create the item on Genius in order to add to catalog."
                                , "Item Not Found On Genius!",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                }

            }
        }

        private void InsertToAutocad()
        {
            string sql;
            sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2])" +
                "VALUES(LEFT( '" + oem.ToString() + "',50),'" + oem.ToString() + "','" + ItemNo.ToString() + "',  LEFT('" + Manufacturer.ToString() + "',20)," +
                "'" + Manufacturer.ToString() + "','" + description.ToString() + "',SUBSTRING('" + oem.ToString() + "',51,100),SUBSTRING('" + oem.ToString() + "',151,100))";

            try
            {

                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException)
            {

                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error while inserting to autocad catalog. Please contact the admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                _connection.Close();
            }

        }

        private void UpdateToAutocad()
        {
            string sql;
            sql = "UPDATE [SPMControlCatalog].[dbo].[SPM-Catalog] SET [CATALOG] = LEFT( '" + oem.ToString() + "',50),[TEXTVALUE] = '" + oem.ToString() + "'," +
                "[MANUFACTURER] = LEFT('" + Manufacturer.ToString() + "',20),[USER3] = '" + Manufacturer.ToString() + "' ," +
                "[DESCRIPTION] = '" + description.ToString() + "',[MISC1] = SUBSTRING('" + oem.ToString() + "',51,100), [MISC2] = SUBSTRING('" + oem.ToString() + "',151,100)" +
                " WHERE [QUERY2] = '" + ItemNo.ToString() + "'";
            try
            {

                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                MessageBox.Show("Item already exists on the catalog." + Environment.NewLine + "Item properties updated!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException)
            {

                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Incorrect Data Enrty Found While Upadting. Please contact the system Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                _connection.Close();
            }

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {

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

        private void addToCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRowInfo();
        }


        #endregion

        #region Highlight Search Results 

        bool IsSelected = false;

        private void SearchStringPosition(string Searchstring)
        {
            IsSelected = true;

        }
        string sw;

        private void searchtext(string searchkey)
        {

            sw = searchkey;
        }

        private void dataGridView_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {


            if (e.RowIndex >= 0 & e.ColumnIndex >= 0 & IsSelected)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                if (!string.IsNullOrEmpty(sw))
                {
                    string val = (string)e.FormattedValue;
                    int sindx = val.ToLower().IndexOf(sw.ToLower());
                    if (sindx >= 0)
                    {
                        Rectangle hl_rect = new Rectangle();
                        hl_rect.Y = e.CellBounds.Y + 2;
                        hl_rect.Height = e.CellBounds.Height - 5;

                        string sBefore = val.Substring(0, sindx);
                        string sWord = val.Substring(sindx, sw.Length);
                        Size s1 = TextRenderer.MeasureText(e.Graphics, sBefore, e.CellStyle.Font, e.CellBounds.Size);
                        Size s2 = TextRenderer.MeasureText(e.Graphics, sWord, e.CellStyle.Font, e.CellBounds.Size);

                        if (s1.Width > 5)
                        {
                            hl_rect.X = e.CellBounds.X + s1.Width - 5;
                            hl_rect.Width = s2.Width - 6;
                        }
                        else
                        {
                            hl_rect.X = e.CellBounds.X + 2;
                            hl_rect.Width = s2.Width - 6;
                        }

                        SolidBrush hl_brush = default(SolidBrush);
                        if (((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None))
                        {
                            hl_brush = new SolidBrush(Color.Black);
                        }
                        else
                        {
                            hl_brush = new SolidBrush(Color.FromArgb(126, 206, 253));
                        }

                        e.Graphics.FillRectangle(hl_brush, hl_rect);

                        hl_brush.Dispose();
                    }

                }
                e.PaintContent(e.CellBounds);

            }

        }

        #endregion

        #region AdminControlLabel

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        private void admin_bttn_Click(object sender, EventArgs e)
        {
            spmadmin spmadmin = new spmadmin();
            spmadmin.ShowDialog();
        }

        private void checkadmin()
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Admin = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        admin_bttn.Visible = true;

                    }
                    else
                    {

                        admin_bttn.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        #endregion

        #region Duplicates

        private void showduplicates()
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();

            SqlDataAdapter sda = new SqlDataAdapter("[SPM_Database].[dbo].[ManufactureItemDuplicates]", cn);

            sda.Fill(dt);
            dataGridView.DataSource = dt;
            UpdateFont();

        }

        private void shwduplicatesbttn_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = null;
            dt.Rows.Clear();
            showduplicates();
        }

        #endregion

        #region ParentView

        //public static string whereused;
        //public static string whereusedcontrols;

        private void geniusJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(ItemNo);
                //whereused = ItemNo;
                //ParentView parentView = new ParentView();
                //parentView.Show();
                WhereUsedSPM whereUsed = new WhereUsedSPM();
                whereUsed.item(ItemNo);
                whereUsed.Show();
                //whereused = null;

            }
            else
            {
                //whereused = "";
                //ParentView parentView = new ParentView();
                //parentView.Show();
                WhereUsedSPM whereUsed = new WhereUsedSPM();
                whereUsed.Show();
                //whereused = null;
            }
        }

        private void autoCadCatalogToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(ItemNo);
            //whereusedcontrols = ItemNo;
            AutocadWhereUsed treeView = new AutocadWhereUsed();
            treeView.item(ItemNo);
            treeView.Show();
            //whereusedcontrols = null;

        }

        #endregion

        #region TreeView

        //public static string assytree;

        private void bOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TreeView_Bttn.PerformClick();

        }

        private void geniusToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(ItemNo);
                //assytree = ItemNo;
                TreeViewSPM treeView = new TreeViewSPM();
                treeView.item(ItemNo);
                treeView.Show();
                //assytree = null;
            }
            else
            {
                //assytree = "";
                TreeViewSPM treeView = new TreeViewSPM();
                treeView.Show();
                //assytree = null;
            }
        }

        private void autoCadCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openassytocatalog();
        }


        #endregion

        #region Closing SPMConnect

        private void SPM_ConnectControls_FormClosed(object sender, FormClosedEventArgs e)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            checkout(userName);
            this.Dispose();
        }

        #endregion

        #region create assembly to catalog

        private void createAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Item Not found as an assembly.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            GetRowInfoforassy();

        }

        private void GetRowInfoforassy()
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
            description = Convert.ToString(slectedrow.Cells[1].Value);
            family = Convert.ToString(slectedrow.Cells[2].Value);
            Manufacturer = Convert.ToString(slectedrow.Cells[3].Value);
            oem = Convert.ToString(slectedrow.Cells[4].Value);


            if (family.ToString() == "ASEL" || family.ToString() == "AS" || family.ToString() == "ASPN")
            {
                DialogResult result = MessageBox.Show(
                "ItemNumber = " + ItemNo + Environment.NewLine +
                "Description = " + description + Environment.NewLine +
                "Manufacturer = " + Manufacturer + Environment.NewLine +
                "Family = " + family + Environment.NewLine +
                "OEM = " + oem, "Create an Assembly To AutoCad Catalog?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //code for Yes
                    CheckAutoCadforassy();
                }
                else if (result == DialogResult.No)
                {
                    //code for No
                }

            }
            else
            {
                MessageBox.Show("Item family must be a \"ASEL\" or \"ASPN\" or \"AS\". In order to create an assembly on the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }


        }

        private void CheckAutoCadforassy()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Count(*) from [SPMControlCatalog].[dbo].[SPM-Catalog] where (QUERY2 = @item) AND (ASSEMBLYCODE = @item) AND (ASSEMBLYLIST IS NULL OR ASSEMBLYLIST = '') ", _connection))
            {
                _connection.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);
                //  sqlCommand.Parameters.AddWithValue("@code", null);
                // sqlCommand.Parameters.AddWithValue("@list", null);
                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    _connection.Close();
                    //call for update
                    MessageBox.Show("Assembly already exists on the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    openassytocatalog();
                }
                else if (userCount > 1)
                {
                    MessageBox.Show("Technical error while looking up for assembly in autocad catalog. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //call Genius check item                    
                    _connection.Close();
                    CheckAssyOnGenius();
                }
            }
        }

        private void CheckAssyOnGenius()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPMDB].[dbo].[Edb] WHERE Item = @item ", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    // MessageBox.Show("items exists");
                    cn.Close();
                    //insert to autocad catalog
                    // InsertToAutocad();
                    createassycatalog();
                    // show the new form
                }
                else
                {
                    //call Genius check item
                    DialogResult result = MessageBox.Show(
                                "ItemNumber = " + ItemNo + ". Does not exist on Genius. Please create the item on Genius in order to add to catalog."
                                , "Item Not Found On Genius!",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                }

            }
        }

        private void openassytocatalog()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(ItemNo);
                // assytree = ItemNo;
                AutocadAssembly treeView = new AutocadAssembly();
                treeView.item(ItemNo);
                treeView.Show();
                //assytree = null;
            }
            else
            {
                //assytree = "";
                AutocadAssembly treeView = new AutocadAssembly();
                treeView.Show();
                //assytree = null;
            }
        }

        public static string itemnumberct;
        public static string descriptionct;
        public static string OEMct;
        public static string manufacturerct;
        public static string familyct;

        private void createassycatalog()
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                itemnumberct = Convert.ToString(slectedrow.Cells[0].Value);
                descriptionct = Convert.ToString(slectedrow.Cells[1].Value);
                familyct = Convert.ToString(slectedrow.Cells[2].Value);
                manufacturerct = Convert.ToString(slectedrow.Cells[3].Value);
                OEMct = Convert.ToString(slectedrow.Cells[4].Value);
                //MessageBox.Show(ItemNo);              

                CreateAssyToCatalog treeView = new CreateAssyToCatalog();
                treeView.ShowDialog();
            }

        }
        #endregion

        #region shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Home))
            {

                Reload.PerformClick();

                return true;
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();

                return true;
            }
            if (keyData == (Keys.Control | Keys.Alt | Keys.A))
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        #endregion

        #region datagridview highlight rows

        private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            }
        }

        private void dataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
            }
        }

        #endregion

        #region UserLog

        public void chekin(string applicationname, string username)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("dd-MM-yyyy HH:mm tt");
            string computername = System.Environment.MachineName;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Checkin] ([Last Login],[Application Running],[User Name], [Computer Name]) VALUES('" + sqlFormattedDate + "', '" + applicationname + "', '" + username + "', '" + computername + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - User Checkin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        public void checkout(string username)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("dd-MM-yyyy HH:mm tt");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[Checkin] WHERE [User Name] ='" + username.ToString() + "'";
                SqlCommand sda = new SqlCommand(query, cn);
                sda.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Checkout User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        #endregion

        private void jobsbttn_Click(object sender, EventArgs e)
        {
            SPM_ConnectJobs sPM_ConnectJobs = new SPM_ConnectJobs();
            sPM_ConnectJobs.Show();
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

        private string getactiveblock(string username)
        {

            try
            {

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] where UserName ='" + username.ToString() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string useractiveblock = dr["ActiveBlockNumber"].ToString();
                    if (useractiveblock == "")
                    {
                        MessageBox.Show("User has not been assigned a block number. Please contact the admin.", "SPM Connect - Get Last Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    return useractiveblock;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get User Active Block", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();

            }
            finally
            {
                cn.Close();
            }

            return "";

        }

        private string getlastnumber(string blocknumber)
        {
            if (blocknumber == "")
            {

                return "";
            }
            else
            {
                try
                {

                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT MAX(RIGHT(ItemNumber,5)) AS " + blocknumber.ToString() + " FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber like '" + blocknumber.ToString() + "%' AND LEN(ItemNumber)=6";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        string lastnumber = dr[blocknumber].ToString();
                        if (lastnumber == "")
                        {
                            return blocknumber.Substring(1) + "000";
                        }
                        return lastnumber;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "SPM Connect - Get Last Number", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                finally
                {
                    cn.Close();

                }
                return blocknumber.Substring(1) + "000";
            }


        }

        private void getnewitembttn_Click(object sender, EventArgs e)
        {
            if (validnumber(getlastnumber(getactiveblock(get_username()))) == true)
            {

                string newid = spmnew_idincrement(getlastnumber(getactiveblock(get_username())), getactiveblock(get_username())).ToString();
                MessageBox.Show("Your next itemnumber to use is :- " + newid, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clipboard.SetText(newid);
            }

        }

        private string spmnew_idincrement(string lastnumber, string blocknumber)
        {
            if (lastnumber.Substring(2) == "000")
            {
                string lastnumbergrp1 = blocknumber.Substring(0, 1).ToUpper();
                string newid1 = lastnumbergrp1 + lastnumber.ToString();
                return newid1;
            }
            else
            {
                string lastnumbergrp = blocknumber.Substring(0, 1).ToUpper();
                int lastnumbers = Convert.ToInt32(lastnumber);
                lastnumbers += 1;
                string newid = lastnumbergrp + lastnumbers.ToString();
                return newid;
            }

        }

        private static bool validnumber(string lastnumber)
        {
            bool valid = true;
            if (lastnumber.ToString() != "")
            {
                if (lastnumber.Substring(2) == "999")
                {
                    MessageBox.Show("User block number limit has reached. Please ask the admin to asssign a new block number.", "SPM Connect - Valid Number Limit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    valid = false;
                }

            }
            else
            {
                return false;
            }
            return valid;
        }

        private void billsOfMaunfacturingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "BOM");
        }

        private String getitemnumberselected()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string item;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(ItemNo);
                return item;
            }
            else
            {
                item = "";
                return item;
            }
        }

        private void prorcessreportbom(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(itemvalue);
            form1.getreport(Reportname);
            form1.Show();

        }

        private void sparePartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "SPAREPARTS");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        private void SPM_ConnectControls_FormClosing(object sender, FormClosingEventArgs e)
        {
            int openforms = Application.OpenForms.Count;
            if (openforms > 2)
            {
                e.Cancel = true;
                ListOpenWindows frm2 = new ListOpenWindows();
                bool purchasereqopen = false;

                foreach (Form frm in Application.OpenForms)
                {
                    //if (frm.Name.ToString() == "SPM_Connect" || frm.Name.ToString() == "SPM_ConnectHome")
                    //{

                    //}
                    if (frm.Name.ToString() == "PurchaseReqform")
                    {
                        frm2.listBox1.Items.Add(frm.Text.ToString());
                        purchasereqopen = true;
                    }
                    //else
                    //{
                    //    frm2.listBox1.Items.Add(frm.Text.ToString());
                    //}
                }
                if (purchasereqopen)
                {
                    if (frm2.ShowDialog(this) == DialogResult.OK)
                    {
                    }
                    frm2.Close();
                    frm2.Dispose();
                }
                else
                {
                    e.Cancel = false;
                }

            }
        }

        private static String Makepath(string itemnumber)
        {

            if (itemnumber.Length > 0)
            {
                string first3char = itemnumber.Substring(0, 3) + @"\";
                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                string Pathpart = (spmcadpath + first3char);
                return Pathpart;
            }
            else
            {
                return null;
            }

        }

        private void eModelViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string itemnumber = getitemnumberselected().ToString();
            string Pathpart = Makepath(itemnumber);
            string filename = Pathpart + itemnumber;

            string[] s = Directory.GetFiles(Pathpart, "*" + itemnumber + "*", SearchOption.TopDirectoryOnly).Where(str => !str.Contains(@"\~$")).ToArray();
            int c = Convert.ToInt32(s.Length);
            if (c > 0)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    //MessageBox.Show(s[i]);
                    //MessageBox.Show(Path.GetFileName(s[i]));

                    if (s[i].Contains(".sldprt"))
                    {

                        filename += ".sldprt";
                    }
                    else if (s[i].Contains(".sldasm"))
                    {
                        filename += ".sldasm";
                    }
                }
                Edrawings.EModelViewer modelViewer = new Edrawings.EModelViewer();
                modelViewer.filetoopen(filename);
                modelViewer.Show();

            }
            else
            {
                MessageBox.Show("No Model Found", "SPM Connect",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}

