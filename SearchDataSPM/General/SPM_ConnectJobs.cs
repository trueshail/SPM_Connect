
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;



namespace SearchDataSPM
{

    public partial class SPM_ConnectJobs : Form

    {
        #region SPM Connect Load

        String connection;
        SqlConnection cn;
        DataTable dt;
        SqlCommand _command;
        // SqlDataAdapter _adapter;


        public SPM_ConnectJobs()

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
                Environment.Exit(0);

            }
            finally
            {
                cn.Close();
            }
            dt = new DataTable();
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            CheckManagement();
            Checkdeveloper();
            Showallitems();

        }

        private void Showallitems()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[SPMJobs] ORDER BY Job DESC", cn);

                dt.Clear();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
                dataGridView.Columns[8].Visible = false;
                dataGridView.Columns[9].Visible = false;
                dataGridView.Columns[0].Width = 60;
                dataGridView.Columns[1].Width = 60;
                dataGridView.Columns[2].Width = 60;
                dataGridView.Columns[3].Width = 250;
                dataGridView.Columns[4].Width = 60;
                dataGridView.Columns[5].Width = 160;
                dataGridView.Columns[6].Width = 40;
                dataGridView.Columns[7].Width = 40;
                UpdateFont();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER ENG", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region Public Table & variables

        // variables required outside the functions to perfrom
        string fullsearch = ("FullSearch LIKE '%{0}%'");
        public static string ItemNo;
        public static string description;
        public static string Manufacturer;
        public static string oem;
        public static string family;

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
                if (Descrip_txtbox.Visible == true)
                {
                    clearandhide();
                }
                Showallitems();
                mainsearch();
                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");

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
            table0.Clear();
            table1.Clear();
            table2.Clear();
            table3.Clear();
        }

        private void mainsearch()
        {

            DataView dv = dt.DefaultView;
            string search1 = txtSearch.Text;
            try
            {
                search1 = search1.Replace("'", "''");
                search1 = search1.Replace("[", "[[]");
                dv.RowFilter = string.Format(fullsearch, search1);
                dataGridView.DataSource = dt;
                table0 = dv.ToTable();
                dataGridView.Update();
                SearchStringPosition(txtSearch.Text);
                searchtext(txtSearch.Text);
                dataGridView.Refresh();
            }
            catch (Exception)

            {
                MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                txtSearch.Clear();

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
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    Descrip_txtbox.Clear();
                    SendKeys.Send("~");
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    filteroem_txtbox.Clear();
                    SendKeys.Send("~");
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    filteroemitem_txtbox.Clear();
                    SendKeys.Send("~");
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                    filter4.Clear();
                    SendKeys.Send("~");
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Home))
            {

                Reload.PerformClick();

                return true;
            }
            if (keyData == (Keys.Control | Keys.B))
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
                    item = Convert.ToString(slectedrow.Cells[2].Value);
                    //MessageBox.Show(ItemNo);

                }
                else
                {
                    item = "";
                }
                processbom(item);

                return true;
            }
            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();
                return true;
            }

            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();

                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
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

        private void CheckManagement()
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Management = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {

                        CreateFolderButton.Visible = true;
                    }
                    else
                    {


                        CreateFolderButton.Visible = false;
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

        private void Checkdeveloper()
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Developer = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        purchasereq.Enabled = true;
                        purchasereq.Visible = true;

                    }
                    else
                    {
                        purchasereq.Enabled = false;
                        purchasereq.Visible = false;

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

        #region datagridview events

            private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            }
        }

        private void dataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
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

        #endregion

        #region Get BOM

        private void getBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string item;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[2].Value);
                //MessageBox.Show(ItemNo);

            }
            else
            {
                item = "";
            }

            processbom(item);
        }

        // public static string jobtree;

        private void processbom(string itemvalue)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Department = 'Controls'", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@username", userName);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    cn.Close();
                    //jobtree = itemvalue;
                    TreeViewSPM treeView = new TreeViewSPM();
                    treeView.item(itemvalue);
                    treeView.Show();

                }
                else
                {
                    cn.Close();
                    //jobtree = itemvalue;
                    TreeView treeView = new TreeView();
                    treeView.item(itemvalue);
                    treeView.Show();
                    //jobtree = null;

                }
            }


        }

        #endregion

        #region GetProjectEng

        private void projectEngineeringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checksqltable(getjob(), getbomitem());

        }

        private string getjob()
        {
            string job;

            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                job = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(job);
                return job;

            }
            return null;
        }

        private string getbomitem()
        {
            string bom;

            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                bom = Convert.ToString(slectedrow.Cells[2].Value);
                //MessageBox.Show(job);
                return bom;
            }
            return null;
        }

        private void checksqltable(string job, string bom)
        {
            contextMenuStrip1.Visible = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[SPMJobsPath] WHERE JobNo = '" + job + "' AND BOMNo = '" + bom + "' AND PAth is not null", cn))
            {
                cn.Open();

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {

                    cn.Close();
                    grabpathfromtable(job, bom);

                }
                else
                {
                    cn.Close();
                    DialogResult result = MessageBox.Show("Project folder not assigned. Would you like to assign one now?", "SPM Connect",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        //code for Yes
                        createnewpath(job, bom);
                    }
                    else if (result == DialogResult.No)
                    {
                        //code for No
                    }

                }
            }

        }

        private void createnewpath(string job, string bom)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(openFileDialog1.FileName);
                //MessageBox.Show(folderPath);
                //Process.Start(folderPath);
                createnewentry(job, bom, folderPath);

                //OpenFileDialog folderBrowser = new OpenFileDialog();
                //// Set validate names and check file exists to false otherwise windows will
                //// not let you select "Folder Selection."
                //folderBrowser.ValidateNames = false;
                //folderBrowser.CheckFileExists = false;
                //folderBrowser.CheckPathExists = true;
                //// Always default to Folder Selection.
                //folderBrowser.FileName = "Folder Selection.";
                //if (folderBrowser.ShowDialog() == DialogResult.OK)
                //{
                //    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                //    MessageBox.Show(folderPath);
                //    Process.Start(folderPath);
                //    // ...
                //}
            }
        }

        private void createnewentry(string job, string bom, string folderPath)
        {
            string sql;
            sql = "INSERT INTO [SPM_Database].[dbo].[SPMJobsPath] ([JobNo], [BOMNo],[Path])" +
                "VALUES( '" + job + "','" + bom + "','" + folderPath + "')";

            try
            {

                cn.Open();
                _command = new SqlCommand();
                _command.Connection = cn;
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                // MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException)
            {

                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error occured while saving the path. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            openprojecteng(folderPath);

        }

        private void grabpathfromtable(string job, string bom)
        {
            DataTable _acountsTb = new DataTable();
            try
            {

                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[SPMJobsPath] WHERE  JobNo = '" + job + "' AND BOMNo = '" + bom + "' ", cn);

                sda.Fill(_acountsTb);


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }

            string path = _acountsTb.Rows[0]["Path"].ToString();
            openprojecteng(path);
        }

        private void openprojecteng(string folderPath)
        {
            Process.Start(folderPath);
        }

        #endregion

        #region RemapFolderPath

        private void remapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Would you like to re-assign the folder?", "SPM Connect",
            //                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    //code for Yes
            //    string job = getjob();
            //    string bom = getbomitem();
            //    deleterecord(job, bom);
            //    checksqltable(job, bom);
            //}
            //else if (result == DialogResult.No)
            //{
            //    //code for No
            //}
            string job = getjob();
            string bom = getbomitem();
            deleterecord(job, bom);
            checksqltable(job, bom);

        }

        private void deleterecord(string job, string bom)
        {
            string sql;
            sql = "DELETE FROM [SPM_Database].[dbo].[SPMJobsPath] WHERE  JobNo = '" + job + "' AND BOMNo = '" + bom + "'";

            try
            {

                cn.Open();
                _command = new SqlCommand();
                _command.Connection = cn;
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                // MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException)
            {

                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error while updating to autocad catalog. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
        }

        #endregion

        #region CreateFolders

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                MessageBox.Show(
                  "Source directory does not exist or could not be found: "
                  + sourceDirName);
                return;
            }
            if (Directory.Exists(destDirName))
            {
                if (MessageBox.Show(destDirName + " already exists\r\nDo you want to overwrite it?", "Overwrite Folder  - SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                {
                    DirectoryInfo[] dirs = dir.GetDirectories();
                    // If the destination directory doesn't exist, create it.
                    if (!Directory.Exists(destDirName))
                    {
                        Directory.CreateDirectory(destDirName);
                    }

                    // Get the files in the directory and copy them to the new location.
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string temppath = Path.Combine(destDirName, file.Name);
                        file.CopyTo(temppath, true);
                    }

                    // If copying subdirectories, copy them and their contents to new location.
                    if (copySubDirs)
                    {
                        foreach (DirectoryInfo subdir in dirs)
                        {
                            string temppath = Path.Combine(destDirName, subdir.Name);
                            DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                        }
                    }
                }
                else { return; }
            }
            else
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }

        }

        private void CreateFolderButton_Click(object sender, EventArgs e)
        {

            string jobnumber = getjobnumber().ToString();
            string jobdescription = getjobdescription().ToString();
            string customer = getcustomeralias(getcutomerid().ToString()).ToString();
            DialogResult result = MessageBox.Show(
               "JobNumber = " + jobnumber + Environment.NewLine +
               "Job Description = " + jobdescription + Environment.NewLine +
               "Customer = " + customer, "Create Job Folders?",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                createjobfolders(jobnumber, customer, jobdescription);
            }
        }

        private void createjobfolders(string jobnumber, string customer, string jobdescription)
        {
            this.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            string ValueIWantFromProptForm = "";
            JobType jobtype = new JobType();
            if (jobtype.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ValueIWantFromProptForm = jobtype.ValueIWant;
            }
            new Thread(() => new Engineering.WaitFormCreatingFolders().ShowDialog()).Start();
            Thread.Sleep(2000);
            string destpatheng = "";
            string destpaths300 = "";
            string sourcepathseng = "";
            string sourcepaths300 = "";

            if (ValueIWantFromProptForm == "project")
            {
                sourcepathseng = @"\\spm-adfs\SPM\S500 Engineering\Project_Engineering_Info\XXXXX_CUSTOMER_Job Name";
                destpatheng = @"\\spm-adfs\SPM\S500 Engineering\Project_Engineering_Info\" + jobnumber + "_" + customer + "_" + jobdescription;
                sourcepaths300 = @"\\spm-adfs\SPM\S300 Sales and Project Management\Project Management - Financial\XXXXX_CUSTOMER_JOB NAME";
                destpaths300 = @"\\spm-adfs\SPM\S300 Sales and Project Management\Project Management - Financial\Genius Job numbers\" + jobnumber + "_" + customer + "_" + jobdescription;
            }
            else if (ValueIWantFromProptForm == "spare")
            {
                sourcepathseng = @"\\spm-adfs\SPM\S500 Engineering\Project_Engineering_Info\XXXXX_CUSTOMER_Spare Parts_Job Name";
                destpatheng = @"\\spm-adfs\SPM\S500 Engineering\Project_Engineering_Info\" + jobnumber + "_" + customer + "_Spare Parts" + "_" + jobdescription;
                sourcepaths300 = @"\\spm-adfs\SPM\S300 Sales and Project Management\Project Management - Financial\XXXXX_CUSTOMER_Spare Parts";
                destpaths300 = @"\\spm-adfs\SPM\S300 Sales and Project Management\Project Management - Financial\Genius Job numbers\" + jobnumber + "_" + customer + "_Spare Parts" + "_" + jobdescription;
            }
            else if (ValueIWantFromProptForm == "service")
            {
                sourcepathseng = @"\\spm-adfs\SPM\S500 Engineering\Project_Engineering_Info\XXXXX_CUSTOMER_Service_Job Name";
                destpatheng = @"\\spm-adfs\SPM\S500 Engineering\Project_Engineering_Info\" + jobnumber + "_" + customer + "_Service" + "_" + jobdescription;
                sourcepaths300 = @"\\spm-adfs\SPM\S300 Sales and Project Management\Project Management - Financial\XXXXX_CUSTOMER_Service";
                destpaths300 = @"\\spm-adfs\SPM\S300 Sales and Project Management\Project Management - Financial\Genius Job numbers\" + jobnumber + "_" + customer + "_Service" + "_" + jobdescription;
            }
            DirectoryCopy(sourcepathseng, destpatheng, true);
            DirectoryCopy(sourcepaths300, destpaths300, true);
            Engineering.WaitFormCreatingFolders f = new Engineering.WaitFormCreatingFolders();
            f = (Engineering.WaitFormCreatingFolders)Application.OpenForms["WaitFormCreatingFolders"];
            f.Invoke(new ThreadStart(delegate { f.Close(); }));
            MessageBox.Show("Job folders created sucessfully!.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
            this.Enabled = true;
        }

        private String getjobnumber()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string jobnumber;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                jobnumber = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(ItemNo);
                return jobnumber;
            }
            else
            {
                jobnumber = "";
                return jobnumber;
            }
        }

        private String getjobdescription()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string jobdescription;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                jobdescription = Convert.ToString(slectedrow.Cells[3].Value);
                //MessageBox.Show(ItemNo);
                return jobdescription;
            }
            else
            {
                jobdescription = "";
                return jobdescription;
            }
        }

        private String getcustomeralias(string customerid)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Alias FROM [SPM_Database].[dbo].[Customers] WHERE [CustomerID]='" + customerid.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string customername = dr["Alias"].ToString();
                    return customername;
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

        private String getcutomerid()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string customer;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                customer = Convert.ToString(slectedrow.Cells[9].Value);
                //MessageBox.Show(ItemNo);
                return customer;
            }
            else
            {
                customer = "";
                return customer;
            }
        }

        #endregion

        private void purchasereq_Click(object sender, EventArgs e)
        {
           
            PurchaseReqform purchaseReq = new PurchaseReqform();
            purchaseReq.Show();
           
        }

        private void getWorkOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SPM_ConnectWM sPM_ConnectWM = new SPM_ConnectWM();
            sPM_ConnectWM.Show();
        }
    }
}

