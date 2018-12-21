
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EModelView;
using ExtractLargeIconFromFile;
using wpfPreviewFlowControl;

namespace SearchDataSPM
{

    public partial class SPM_ConnectProduction : Form

    {
        #region SPM Connect Load

        String connection;
        SqlConnection cn;
        DataTable dt;

        public SPM_ConnectProduction()

        {
            InitializeComponent();
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect - Production", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
            dt = new DataTable();
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = Width >= screen.Width ? screen.Width : ((screen.Width + Width) / 3) + 30;
            int h = Height >= screen.Height ? screen.Height : ((screen.Height + Height) / 3) + 60;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);

        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            Showallitems();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            checkadmin();
            this.Text = "SPM Connect Production - " + userName.ToString().Substring(4);
            chekin("SPM Connect Production", userName);
            txtSearch.Focus();
            
        }

        private void Showallitems()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UnionInventory] ORDER BY ItemNumber DESC", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                    dataGridView.DataSource = dt;
                    DataView dv = dt.DefaultView;
                    dataGridView.Sort(itemNumberDataGridViewTextBoxColumn, ListSortDirection.Descending);
                    UpdateFont();

                }
                catch (Exception)
                {
                    MessageBox.Show("Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - Engineering Load(showallitems)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    cn.Close();
                }

            }

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
        string fullsearch = ("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'");
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
            dt.Dispose();
            table0.Clear();
            table1.Clear();
            table2.Clear();
            table3.Clear();
            table0.Dispose();
            table1.Dispose();
            table2.Dispose();
            table3.Dispose();
            listFiles.Clear();
            listView.Clear();
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

        #region OpenModel and datagridview events

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string itemnumber;
            if (dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                itemnumber = Convert.ToString(slectedrow.Cells[0].Value);
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.item(itemnumber);
                itemInfo.Show();
            }
        }

        private void checkforspmfile(string ItemNo)
        {
            string ItemNumbero;
            ItemNumbero = ItemNo + "-0";

            if (!String.IsNullOrWhiteSpace(ItemNo) && ItemNo.Length == 6)

            {
                string first3char = ItemNo.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char + ItemNo + ".sldprt");
                string Pathassy = (spmcadpath + first3char + ItemNo + ".sldasm");
                string PathPartNo = (spmcadpath + first3char + ItemNumbero + ".sldprt");
                string PathAssyNo = (spmcadpath + first3char + ItemNumbero + ".sldasm");



                if (File.Exists(Pathassy) && File.Exists(Pathpart))
                {

                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo." + ItemNo + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo. " + ItemNumbero + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNo + "and Assembly file " + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(Pathassy) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNumbero + "and Assembly file" + ItemNo + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathPartNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part two files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathassy))
                {
                    MessageBox.Show($"System has found a assembly files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else if (File.Exists(Pathassy))
                {
                    Process.Start("explorer.exe", Pathassy);
                   

                }
                else if (File.Exists(PathAssyNo))
                {

                    Process.Start("explorer.exe", PathAssyNo);
                    

                }
                else if (File.Exists(Pathpart))
                {

                    Process.Start("explorer.exe", Pathpart);
                  
                }
                else if (File.Exists(PathPartNo))
                {

                    Process.Start("explorer.exe", PathPartNo);
                  

                }
                else
                {
                    MessageBox.Show($"A file with the part number" + ItemNo + " does not have Solidworks CAD Model. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }

            }
            else
            {

            }

        }

        private void checkforspmdrwfile(string str)
        {
            string ItemNumbero = str + "-0";


            if (!String.IsNullOrWhiteSpace(str) && str.Length == 6)

            {
               string first3char = str.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + str + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");


                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + str + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                else if (File.Exists(Drawpath))
                {

                    Process.Start("explorer.exe", Drawpath);

                }
                else if (File.Exists(drawpathno))
                {

                    Process.Start("explorer.exe", drawpathno);

                }
                else
                {

                    MessageBox.Show($"A file with the part number" + str + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

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

        private void openModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string Item = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(ItemNo);
            //  checkforprocess();
            checkforspmfile(Item);
        }

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string str = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(str);
            // checkforprocess();
            checkforspmdrwfile(str);

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

        #region AdminControl

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            //string x = Microsoft.VisualBasic.Interaction.InputBox("What's the Keyword?", "Admin Control", "SPM Automation");
            //if (x == "spmuser")
            //{
                

            //}
            //else if (x == "deleteitem")
            //{
            //    delete.Visible = true;
            //}

        }

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

        public void shwduplicatesbttn_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = null;
            dt.Rows.Clear();
            showduplicates();
        }

        #endregion

        #region ParentView

        //public static string whereused;

        private void ParentView_Click(object sender, EventArgs e)
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

            }
            else
            {
                item = "";
            }

            processwhereused(item);
        }

        private void processwhereused(string item)
        {
           // whereused = item;
            WhereUsed whereUsed = new WhereUsed();
            whereUsed.item(item);
            whereUsed.Show();
            //whereUsed = null;
        }

        private void whereUsedToolStripMenuItem_Click(object sender, EventArgs e)
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

            }
            else
            {
                item = "";
            }

            processwhereused(item);
        }


        #endregion

        #region TreeView

        //public static string assytree;

        private void TreeView_Bttn_Click(object sender, EventArgs e)
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

            }
            else
            {
                item = "";
            }

            processbom(item);
        }

        private void processbom(string itemvalue)
        {
            //assytree = itemvalue;
            TreeView treeView = new TreeView();
            treeView.item(itemvalue);
            treeView.Show();
            //assytree = null;
        }

        private void bOMToolStripMenuItem_Click(object sender, EventArgs e)
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

            }
            else
            {
                item = "";
            }

            processbom(item);
        }

        #endregion

        #region Closing SPMConnect

        private void SPM_ConnectProduction_FormClosed(object sender, FormClosedEventArgs e)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            checkout(userName);
            this.Dispose();
        }

        #endregion

        #region UserLogs

        private void chekin(string applicationname, string username)
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

        private void checkout(string username)
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

        #region drag and drop

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

        #region shortcuts

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
                string ItemNo;
                if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
                {
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
                    //MessageBox.Show(ItemNo);

                }
                else
                {
                    ItemNo = "";
                }
                processbom(ItemNo);

                return true;
            }
            if (keyData == (Keys.Control | Keys.W))
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

                }
                else
                {
                    item = "";
                }

                processwhereused(item);
                return true;
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();

                return true;
            }
            if (keyData == (Keys.Control | Keys.J))
            {
                openjobmodule();

                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }


        #endregion

        #region ItemListView

        private void getitemstodisplay(string Pathpart, string ItemNo)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart, "*" + ItemNo.ToString() + "*").Where(str => !str.Contains(@"\~$")).OrderByDescending(fi => fi))
                {
                    try
                    {

                        string sDocFileName = item;
                        wpfThumbnailCreator pvf;
                        pvf = new wpfThumbnailCreator();
                        System.Drawing.Size size = new Size();
                        size.Width = 120;
                        size.Height = 120;
                        pvf.DesiredSize = size;
                        System.Drawing.Bitmap pic = pvf.GetThumbNail(sDocFileName);
                        imageList.Images.Add(pic);
                        //axEModelViewControl1 = new EModelViewControl();
                        //axEModelViewControl1.OpenDoc(item, false, false, true, "");

                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message);

                        var size = ShellEx.IconSizeEnum.ExtraLargeIcon;
                        imageList.Images.Add(ShellEx.GetBitmapFromFilePath(item, size));
                        // imageList.Images.Add(GetIcon(item));
                    }

                    // imageList.Images.Add(GetIcon(item));

                    FileInfo fi = new FileInfo(item);
                    listFiles.Add(fi.FullName);
                    listView.Items.Add(fi.Name, imageList.Images.Count - 1);


                }
            }
            
        }

        List<string> listFiles = new List<string>();
        //private EModelViewControl axEModelViewControl1;

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView.FocusedItem != null)
            // Process.Start(listFiles[listView.FocusedItem.Index]);
        }

        [DllImport("shell32.dll")]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        public static Icon GetIconOldSchool(string fileName)
        {
            ushort uicon;
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        public static Icon GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                ShellEx.IconSizeEnum ExtraLargeIcon = default(ShellEx.IconSizeEnum);
                var size = (ShellEx.IconSizeEnum)ExtraLargeIcon;

                ShellEx.GetBitmapFromFilePath(fileName, size);

                return icon;
                
            }
            catch
            {
                try
                {
                    Icon icon2 = GetIconOldSchool(fileName);
                    return icon2;
                }
                catch
                {
                    
                    return null;
                }
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (listView.FocusedItem != null)
                    Process.Start(listFiles[listView.FocusedItem.Index]);
            }
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                string[] fList = new string[1];
                fList[0] = Pathpart;

                DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
                DragDropEffects eff = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect");
            }
            //listView.DoDragDrop(listView.SelectedItems, DragDropEffects.Copy);


        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                listFiles.Clear();
                listView.Items.Clear();
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                string item = Convert.ToString(slectedrow.Cells[0].Value);
                // MessageBox.Show(ItemNo);
                //getfilepathname(ItemNo);
                string first3char = item.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char);
                System.IO.Directory.CreateDirectory(Pathpart);
                var fileCount = (from file in Directory.EnumerateFiles(Pathpart, "*" + item.ToString() + "*", SearchOption.AllDirectories)
                                 select file).Count();

                if (fileCount > 0)
                {
                    getitemstodisplay(Pathpart, item);
                }


            }
            catch (Exception)
            {
                 return;
                
            }

        }

        string Pathpart;

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (listView.FocusedItem != null)
            {
                //string txt = listView.SelectedItems[0].Text;
                string txt = listView.FocusedItem.Text;
                // string path = listView.FocusedItem.Text;
                string first3char = txt.Substring(0, 3) + @"\";
                // //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                Pathpart = (spmcadpath + first3char + txt);
                // //MessageBox.Show(Pathpart);
            }

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                dataGridView.Enabled = false;
                dataGridView.GetNextControl(dataGridView, true).Focus();
                dataGridView.Enabled = true;
                e.Handled = true;
            }
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (listView.FocusedItem != null)
                        Process.Start(listFiles[listView.FocusedItem.Index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect");
                }
            }
        }

        private void listView_Enter(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                listView.ContextMenuStrip = Listviewcontextmenu;
            }
            else
            {
                listView.ContextMenuStrip = null;
            }
        }

        #endregion

        #region Listview dropdownmenu

        private void bomlistviewmenustrpc_Click(object sender, EventArgs e)
        {

            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                processbom(txt);

            }
        }

        private void whereusedlistviewStripMenu_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                processwhereused(txt);

            }
        }




        private void Listviewcontextmenu_Opening(object sender, CancelEventArgs e)
        {

            if (listView.SelectedItems.Count == 1)
            {
                Listviewcontextmenu.Enabled = true;
            }
            else
            {
                Listviewcontextmenu.Enabled = false;
            }
        }


        #endregion

        private void jobsbttn_Click(object sender, EventArgs e)
        {
            openjobmodule();
        }

        void openjobmodule()
        {
            SPM_ConnectJobs sPM_ConnectJobs = new SPM_ConnectJobs();
            sPM_ConnectJobs.Show();
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

        private void SPM_ConnectProduction_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}

