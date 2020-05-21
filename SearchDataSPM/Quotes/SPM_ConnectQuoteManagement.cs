using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SearchDataSPM.Quotes
{
    public partial class SPM_ConnectQuoteManagement : MetroFramework.Forms.MetroForm
    {
        private readonly SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
        private readonly DataTable dt;
        private log4net.ILog log;

        public SPM_ConnectQuoteManagement()
        {
            InitializeComponent();
            dt = new DataTable();
        }

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
                if (MessageBox.Show(destDirName + " already exists\r\nDo you want to overwrite it?", "Overwrite Folder  - SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
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

        private void addnewbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new quote?", "SPM Connect - Create New Quote?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string user = Getuserfullname(GetUserName());
                string newnunber = getnewnumber();
                bool status = createnewquote(newnunber, user);
                if (status)
                {
                    //createfolders(newnunber);
                    showquotedetails(newnunber);
                }
            }
        }

        private bool createnewquote(string quotenumber, string employee)
        {
            bool success = false;
            DateTime datecreated = DateTime.Now;
            DateTime date = DateTime.Now.Date;

            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlFormattedDate = date.ToString("yyyy-MM-dd");
            const string folderpath = @"\\spm-adfs\SPM\S300 Sales and Project Management\Sales\Opportunities\";

            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Opportunities] (Quote, Quote_Date, Employee, LastSavedby, Lastsaved,DateCreated, FolderPath) VALUES('" + quotenumber + "',@value1,'" + employee + "','" + employee + "',@value2,@value2,'" + folderpath + "')";

                cmd.Parameters.AddWithValue("@value1", sqlFormattedDate);
                cmd.Parameters.AddWithValue("@value2", sqlFormattedDatetime);
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                success = true;
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create Entry On SQL Quotes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            return success;
        }

        private string getnewnumber()
        {
            string lastnumber = "";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM newquotenumber";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    lastnumber = dr["NextQuoteNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get Last Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }

            return lastnumber;
        }

        private string getselectedjobnumber()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];

                //MessageBox.Show(item);
                return Convert.ToString(slectedrow.Cells[0].Value);
            }
            else
            {
                return "";
            }
        }

        private string Getuserfullname(string username)
        {
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
                    string fullname = dr["Name"].ToString();
                    return fullname;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get Full User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            return null;
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dataGridView.Refresh();
        }

        private void Showallitems()
        {
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[Quotes] ORDER BY DateCreated DESC", connectapi.cn);

                dt.Clear();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER Quote", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void showquotedetails(string quotenumber)
        {
            using (QuoteDetails quoteDetails = new QuoteDetails(quotenumber))
            {
                quoteDetails.ShowDialog();
                quoteDetails.Dispose();
                Showallitems();
                this.Show();
                this.Activate();
                this.Focus();
            }
        }

        private void SPM_ConnectQuoteManagement_Load(object sender, EventArgs e)
        {
            Showallitems();
            this.Activate();
            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[3].Width = 250;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            UpdateFont();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Quote Management ");
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

        #region Public Table & variables

        public static string description;

        public static string family;

        public static string Manufacturer;

        public static string oem;

        // variables required outside the functions to perfrom
        private readonly string fullsearch = ("FullSearch LIKE '%{0}%'");

        private DataTable table0 = new DataTable();
        private DataTable table1 = new DataTable();
        private DataTable table2 = new DataTable();
        private DataTable table3 = new DataTable();

        #endregion Public Table & variables

        #region Search Parameters

        public void txtSearch_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Return)

            {
                if (Descrip_txtbox.Visible)
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
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = secondFilter;
                    else
                        dv.RowFilter += " AND " + secondFilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
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
                if (!Descrip_txtbox.Visible)
                {
                    filteroem_txtbox.Hide();
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

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fifthfilter;
                    else
                        dv.RowFilter += " AND " + fifthfilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
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
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = thirdFilter;
                    else
                        dv.RowFilter += " AND " + thirdFilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
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

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fourthfilter;
                    else
                        dv.RowFilter += " AND " + fourthfilter;
                    dataGridView.DataSource = dv;
                    SearchStringPosition();
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
                SearchStringPosition();
                searchtext(txtSearch.Text);
                dataGridView.Refresh();
            }
            catch (Exception)

            {
                MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect");
                txtSearch.Clear();
            }
        }

        #endregion Search Parameters

        #region Highlight Search Results

        private bool IsSelected;

        private string sw;

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && IsSelected)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                if (!string.IsNullOrEmpty(sw))
                {
                    string val = (string)e.FormattedValue;
                    int sindx = val.IndexOf(sw, StringComparison.CurrentCultureIgnoreCase);
                    if (sindx >= 0)
                    {
                        Rectangle hl_rect = new Rectangle
                        {
                            Y = e.CellBounds.Y + 2,
                            Height = e.CellBounds.Height - 5
                        };

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

                        SolidBrush hl_brush = (e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None
                            ? new SolidBrush(Color.Black)
                            : new SolidBrush(Color.FromArgb(126, 206, 253));
                        e.Graphics.FillRectangle(hl_brush, hl_rect);

                        hl_brush.Dispose();
                    }
                }
                e.PaintContent(e.CellBounds);
            }
        }

        private void SearchStringPosition()
        {
            IsSelected = true;
        }

        private void searchtext(string searchkey)
        {
            sw = searchkey;
        }

        #endregion Highlight Search Results

        #region AdminControlLabel

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        #endregion AdminControlLabel

        #region datagridview events

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && dataGridView.SelectedCells.Count == 1)
            {
                showquotedetails(getselectedjobnumber());
            }
        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            _ = dataGridView.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView.ClearSelection();
                dataGridView.Rows[columnindex].Selected = true;
            }
        }

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

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                r.Cells[0] = new DataGridViewLinkCell();
            }
        }

        private void SPM_ConnectJobs_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Quote Management ");
            this.Dispose();
        }

        #endregion datagridview events
    }
}