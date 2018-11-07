
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ExtractLargeIconFromFile;
using wpfPreviewFlowControl;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Threading;
using System.Management;
using System.Deployment.Application;

namespace SearchDataSPM
{

    public partial class SPM_Connect : Form

    {
        #region SPM Connect Load

        String connection;
        SqlConnection cn;
        DataTable dt;
        Stopwatch stopwatch = new Stopwatch();

        public SPM_Connect()

        {
            InitializeComponent();
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception)
            {

                //MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect - Engineering Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                //Environment.Exit(0);

            }

            dt = new DataTable();
            //Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            //int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            //int h = Height >= screen.Height ? screen.Height : (screen.Height + 600) / 2;            
            //this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            //this.Size = new Size(w, h);

        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
           
            Showallitems();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            checkadmin();
            Checkdeveloper();
            this.Text = "SPM Connect Engineering - " + userName.ToString().Substring(4);
            Log(string.Format("{0} - Opened by - {1}", "SPM Connect Eng", userName));
            chekin(string.Format("{0} , {1} ", "SPM Connect Eng", userName));
            //GetAllControls(this).OfType<Button>().ToList()
            //    .ForEach(x => x.Click += ButtonClick);
            stopwatch.Start();
            timer2.Start();
            txtSearch.Focus();
            //mysolidworks.ActiveModelDocChangeNotify += this.mysolidworks_activedocchange;

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
        //string ItemNo;
        //string str;
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
            table0.Dispose();
            table1.Dispose();
            table2.Dispose();
            dt.Dispose();
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

        public void checkforspmfile(string Item_No)
        {
            string ItemNumbero;
            ItemNumbero = Item_No + "-0";

            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)
            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char + Item_No + ".sldprt");
                string Pathassy = (spmcadpath + first3char + Item_No + ".sldasm");
                string PathPartNo = (spmcadpath + first3char + ItemNumbero + ".sldprt");
                string PathAssyNo = (spmcadpath + first3char + ItemNumbero + ".sldasm");



                if (File.Exists(Pathassy) && File.Exists(Pathpart))
                {

                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo." + Item_No + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo. " + ItemNumbero + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part file " + Item_No + "and Assembly file " + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(Pathassy) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNumbero + "and Assembly file" + Item_No + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathPartNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part two files " + Item_No + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathassy))
                {
                    MessageBox.Show($"System has found a assembly files " + Item_No + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else if (File.Exists(Pathassy))
                {
                    //Process.Start("explorer.exe", Pathassy);
                    //fName = Pathassy;
                    if (solidworks_running() == true)
                    {
                        Open_assy(Pathassy);
                    }

                }
                else if (File.Exists(PathAssyNo))
                {

                    //Process.Start("explorer.exe", PathAssyNo);
                    // fName = PathAssyNo;
                    if (solidworks_running() == true)
                    {
                        Open_assy(PathAssyNo);
                    }

                }
                else if (File.Exists(Pathpart))
                {

                    //Process.Start("explorer.exe", Pathpart);
                    //fName = Pathpart;
                    if (solidworks_running() == true)
                    {
                        Open_model(Pathpart);
                    }
                }
                else if (File.Exists(PathPartNo))
                {

                    //Process.Start("explorer.exe", PathPartNo);
                    //fName = PathPartNo;
                    if (solidworks_running() == true)
                    {
                        Open_model(PathPartNo);
                    }

                }
                else
                {
                    MessageBox.Show($"A file with the part number " + Item_No + " does not have Solidworks CAD Model. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //fName = "";
                }

            }

        }

        private void checkforspmdrwfile(string Item_No)
        {
            string ItemNumbero = Item_No + "-0";


            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)

            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + Item_No + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");


                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + Item_No + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                else if (File.Exists(Drawpath))
                {

                    //Process.Start("explorer.exe", Drawpath);
                    if (solidworks_running() == true)
                    {
                        Open_drw(Drawpath);
                    }

                }
                else if (File.Exists(drawpathno))
                {

                    //Process.Start("explorer.exe", drawpathno);
                    if (solidworks_running() == true)
                    {
                        Open_drw(drawpathno);
                    }

                }
                else
                {

                    MessageBox.Show($"A file with the part number" + Item_No + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

            }

        }

        //private string fName;

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
            string item = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(str);
            // checkforprocess();
            checkforspmdrwfile(item);

        }

        private void dataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //DataGridViewRow row = dataGridView.Rows[e.RowIndex];// get you required index
                //                                                    // check the cell value under your specific column and then you can toggle your colors
                //row.DefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
            }
            // dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Blue; // just to highlight single column
        }

        private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            }
        }

        private void jobsbttn_Click(object sender, EventArgs e)
        {
            openjobmodule();
        }

        void openjobmodule()
        {
            SPM_ConnectJobs sPM_ConnectJobs = new SPM_ConnectJobs();
            sPM_ConnectJobs.Show();
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
            InstallUpdateSyncWithInfo();
        }

        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
            }
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

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Admin = '1'", cn))
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

                        //AddNewBttn.Visible = true;
                        //editbttn.Visible = true;
                        //saveascopybttn.Visible = true;
                        FormSelector.Items[5].Enabled = true;
                        FormSelector.Items[5].Visible = true;
                        FormSelector.Items[6].Enabled = true;
                        FormSelector.Items[6].Visible = true;
                    }
                    else
                    {


                        //AddNewBttn.Visible = false;
                        //editbttn.Visible = false;
                        //saveascopybttn.Visible = false;
                        FormSelector.Items[5].Enabled = false;
                        FormSelector.Items[5].Visible = false;
                        FormSelector.Items[6].Enabled = false;
                        FormSelector.Items[6].Visible = false;
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

        //private void showduplicates()
        //{
        //    if (cn.State == ConnectionState.Closed)
        //        cn.Open();

        //    SqlDataAdapter sda = new SqlDataAdapter("[SPM_Database].[dbo].[ManufactureItemDuplicates]", cn);

        //    sda.Fill(dt);
        //    dataGridView.DataSource = dt;
        //    UpdateFont();

        //}

        //public void shwduplicatesbttn_Click(object sender, EventArgs e)
        //{
        //    dataGridView.DataSource = null;
        //    dt.Rows.Clear();
        //    showduplicates();
        //}

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
            //whereused = item;
            WhereUsed whereUsed = new WhereUsed();
            whereUsed.item(item);
            whereUsed.Show();
            //whereused = null;
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

            TreeView treeView = new TreeView();
            treeView.item(itemvalue);
            treeView.Show();


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
            //TreeView_Bttn.PerformClick();
            // TreeView_Bttn_Click(sender, e);
        }

        #endregion

        #region Closing SPMConnect

        private void SPM_Connect_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                Log(string.Format("{0} - Closed by - {1}", "SPM Connect Eng", userName));
                stopwatch.Stop();
                Log(string.Format("Session Time : {0:hh\\:mm\\:ss}", stopwatch.Elapsed));

                string LinesToDelete = userName;
                var Lines = File.ReadAllLines(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\chekin.txt");
                var newLines = Lines.Where(line => !line.Contains(LinesToDelete));
                File.WriteAllLines(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\chekin.txt", newLines);
            }
            catch
            {

            }
           
        }

        #endregion

        #region DELETE Item

        private void deleteitem(string _itemno)
        {
            DialogResult result = MessageBox.Show("Are you sure want to delete " + _itemno + "?", "SPM Connect - Delete Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_itemno.Length > 0)
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    try
                    {
                        string query = "DELETE FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber ='" + _itemno.ToString() + "'";
                        SqlCommand sda = new SqlCommand(query, cn);
                        sda.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show(_itemno + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.Close();
                    }

                }
            }

        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteitem(getitemnumberselected().ToString());
        }


        #endregion

        #region UserLogs

        public void Log(string text)
        {
            try
            {
                DateTime datecreated = DateTime.Now;
                string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string filename = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Substring(4) + @"-logs.txt";
                string filepath = @"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\";
                (new FileInfo(filepath)).Directory.Create();
                var file = System.IO.Path.Combine(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\", filename);
                text = string.Format("{0} - {1}", sqlFormattedDate, text);
                System.IO.File.AppendAllLines(file, new string[] { text });
            }
            catch
            {

            }
        }

        public void chekin(string text)
        {
            try
            {
                DateTime datecreated = DateTime.Now;
                string sqlFormattedDate = datecreated.ToString("dd-MM-yyyy HH:mm:ss.fff");

                if (File.Exists(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\chekin.txt"))
                {
                    string filename = "chekin.txt";
                    var file = System.IO.Path.Combine(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\", filename);
                    text = string.Format("{0} , {1}", sqlFormattedDate, text);
                    System.IO.File.AppendAllLines(file, new string[] { text });
                }
                else
                {
                    var file = System.IO.Path.Combine(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\", "chekin.txt");
                    text = string.Format("{0} , {1}", sqlFormattedDate, text);
                    System.IO.File.AppendAllLines(file, new string[] { text });
                }

            }
            catch
            {

            }
         

        }

        private IEnumerable<Control> GetAllControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl)).Concat(controls);
        }

        void ButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null) Log(string.Format("{0} Clicked", button.Name));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string contents = File.ReadAllText(@"\\spm-adfs\SDBASE\SPM_Connect_User_Logs\chekin.txt");
                if (!contents.Contains(userName))
                {
                    Application.Exit();
                }
            }
            catch
            {

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

        #region ItemListView events

        private void getitemstodisplay(string Pathpart, string selecteditem)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart, "*" + selecteditem.ToString() + "*").Where(str => !str.Contains(@"\~$")).OrderByDescending(fi => fi))
                {

                    try
                    {

                        string sDocFileName = item;
                        wpfThumbnailCreator pvf;
                        pvf = new wpfThumbnailCreator();
                        System.Drawing.Size size = new Size();
                        size.Width = 256;
                        size.Height = 256;
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
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    if (listView.FocusedItem != null)
                        Process.Start(listFiles[listView.FocusedItem.Index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - List View DoubleClick");
                }

            }
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] fList = new string[1];
            fList[0] = makepathfordrag().ToString();
            DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            DragDropEffects eff = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
            // listView.DoDragDrop(listView.SelectedItems, DragDropEffects.Copy);           

        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {

            showfilesonlistview();
        }

        private void showfilesonlistview()
        {
            try
            {

                listFiles.Clear();
                listView.Clear();
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
            catch
            {
                return;
            }
        }

        //string Pathpart;

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {

                makepathfordrag();
            }

        }

        private string makepathfordrag()
        {
            string txt = listView.FocusedItem.Text;
            //string txt = listView.SelectedItems[0].Text;
            //string path = listView.FocusedItem.Text;
            string first3char = txt.Substring(0, 3) + @"\";
            // //MessageBox.Show(first3char);
            string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            string Pathpart = (spmcadpath + first3char + txt);
            // //MessageBox.Show(Pathpart);   
            return Pathpart;
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
                    MessageBox.Show(ex.Message, "SPM Connect - ListView Enterkey Pressed");
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

        #region listview menu strip

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

        private void iteminfolistviewStripMenu_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.item(txt);
                itemInfo.Show();

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

        #region Solidworks Communication

        private bool solidworks_running()
        {

            if (Process.GetProcessesByName("SLDWORKS").Length >= 1)
            {
                //mysolidworks.ActiveModelDocChangeNotify += this.mysolidworks_activedocchange;
                return true;
            }
            else if ((Process.GetProcessesByName("SLDWORKS").Length == 0))
            {

                MessageBox.Show("Soliworks application needs to be running in order for SPM Connect to perform. Thank you.", "SPM Connect - Solidworks Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                MessageBox.Show("SPM Connect encountered more than one sesssion of solidworks running. Please close other sesssions in order for SPM Connect to perform. Thank you.", "SPM Connect - Solidworks Running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public void Open_model(string filename)
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
            //swPart = (PartDoc)swModel;
            //swPart = swApp.ActiveDoc;
            //AttachEventHandlersPart();
        }

        public void Open_assy(string filename)
        {

            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocASSEMBLY, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
            //swAssembly = (AssemblyDoc)swModel;
            //AttachEventHandlers();

        }

        public void Open_drw(string filename)
        {

            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
        }


        #endregion

        #region Add new item, edit item, open item and get create entries

        private void AddNewBttn_Click(object sender, EventArgs e)
        {
            chekeditbutton = "no";
            DialogResult result = MessageBox.Show("Are you sure want to create a new item?", "SPM Connect - Add New Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (solidworks_running() == true)
                {
                    string user = get_username();
                    string activeblock = getactiveblock(user);

                    if (activeblock.ToString().Length > 0)
                    {
                        //MessageBox.Show(activeblock.ToString());     
                        if (Convert.ToInt32(columnexists("Blocks", activeblock.ToString())) == 1)
                        {
                            string lastnumber = getlastnumber(activeblock.ToString());
                            if (lastnumber.ToString().Length > 0)
                            {
                                if (validnumber(lastnumber.ToString()) == true)
                                {
                                    spmnew_idincrement(lastnumber.ToString(), activeblock.ToString());
                                    //insertinto_blocks(uniqueid, activeblock.ToString());
                                    checkitempresentoninventory(uniqueid);
                                    itempresentdecide();

                                }

                            }
                            else
                            {
                                spmnew_id(activeblock.ToString());
                                //insertinto_blocks(uniqueid, activeblock.ToString());
                                checkitempresentoninventory(uniqueid);
                                itempresentdecide();

                            }

                        }
                        else
                        {
                            // addblockcolumn(activeblock.ToString());
                            spmnew_id(activeblock.ToString());
                            //insertinto_blocks(uniqueid, activeblock.ToString());
                            checkitempresentoninventory(uniqueid);
                            itempresentdecide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("User block number has not been assigned. Please contact the admin.", "SPM Connect - Add New", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }

        }

        string uniqueid;

        private void spmnew_idincrement(string lastnumber, string blocknumber)
        {
            if (lastnumber.Substring(2) == "000")
            {
                string lastnumbergrp1 = blocknumber.Substring(0, 1).ToUpper();
                 uniqueid = lastnumbergrp1 + lastnumber.ToString();
              
            }
            else
            {
                string lastnumbergrp = blocknumber.Substring(0, 1).ToUpper();
                int lastnumbers = Convert.ToInt32(lastnumber);
                lastnumbers += 1;
                 uniqueid = lastnumbergrp + lastnumbers.ToString();
                
            }
            //string lastnumbergrp = blocknumber.Substring(0, 1).ToUpper();
            //int lastnumbers = Convert.ToInt32(lastnumber);
            //lastnumbers += 1;
            //uniqueid = lastnumbergrp + lastnumbers.ToString();
        }

        private static bool validnumber(string lastnumber)
        {
            bool valid = true;
            if (lastnumber.Substring(2) == "999")
            {
                MessageBox.Show("User block number limit has reached. Please ask the admin to asssign a new block number.", "SPM Connect - Valid Number Limit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                valid = false;
            }
            return valid;
        }

        private void spmnew_id(string blocknumber)
        {
            string letterblock = Char.ToUpper(blocknumber[0]) + blocknumber.Substring(1);
            uniqueid = letterblock + "000";
            //insertinto_blocks(uniqueid, blocknumber.ToString());
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

        private bool columnexists(string tableName, string columnName)
        {
            var tblQuery = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName";
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = tblQuery;
            var tblNameParam = new SqlParameter("@tableName", SqlDbType.NVarChar, 128);

            tblNameParam.Value = tableName;
            cmd.Parameters.Add(tblNameParam);
            var colNameParam = new SqlParameter("@columnName", SqlDbType.NVarChar, 128);

            colNameParam.Value = columnName;
            cmd.Parameters.Add(colNameParam);
            object objvalid = cmd.ExecuteScalar(); // will return 1 or null     
            cn.Close();
            return objvalid != null;


        }

        private void addblockcolumn(string blocknumber)
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "ALTER TABLE  [SPM_Database].[dbo].[Blocks] add " + Char.ToUpper(blocknumber[0]) + blocknumber.Substring(1) + " nvarchar(50) NULL ";
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Block", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private string getlastnumberold(string blocknumber)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX(RIGHT(" + blocknumber.ToString() + ",5)) AS " + blocknumber.ToString() + " FROM [SPM_Database].[dbo].[Blocks]";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string lastnumber = dr[blocknumber].ToString();
                    return lastnumber;
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
            return "";
        }

        private string getlastnumber(string blocknumber)
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

                MessageBox.Show(ex.Message, "SPM Connect - Get Full User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        private void insertinto_blocks(string uniqueid, string blocknumber)
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Blocks] (" + blocknumber.ToString() + ") VALUES('" + uniqueid.ToString() + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Insert Into Blocks Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        bool itempresent = false;

        private void checkitempresentoninventory(string itemid)
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemid.ToString() + "'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

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
                    MessageBox.Show(ex.Message, "SPM Connect - Check Item Present On SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();

                }

            }


        }

        private void itempresentdecide()
        {
            if (itempresent == true)
            {
                openiteminfo(uniqueid);
            }
            else
            {
                string username = getuserfullname(get_username().ToString()).ToString();
                createentryoninventory(uniqueid, username);
                openiteminfo(uniqueid);
            }
        }

        private void createentryoninventory(string uniqueid, string user)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Inventory] (ItemNumber, DesignedBy, DateCreated, LastSavedBy, LastEdited, JobPlanning) VALUES('" + uniqueid.ToString() + "','" + user.ToString() + "','" + sqlFormattedDate + "','" + user.ToString() + "','" + sqlFormattedDate + "','1'  )";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create Entry On SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void openiteminfo(string itemid)
        {
            NewItem newItem = new NewItem();
            newItem.item(itemid);
            newItem.editbtn(chekeditbutton.ToString());
            chekeditbutton = "";
            newItem.ShowDialog();
            newItem.Dispose();

        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        public string chekeditbutton = "";

        private void processeditbutton()
        {
            showfilesonlistview();
            Cursor.Current = Cursors.WaitCursor;
            chekeditbutton = "yes";
            this.Enabled = false;
            if (solidworks_running() == true)
            {
                string item = getitemnumberselected().ToString();

                checkitempresentoninventory(item);

                if (itempresent == true)
                {
                    if (listView.Items.Count > 0)
                    {
                        getitemopenforedit(item);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Item does not contain a model. Do you wish to create a  model to edit the properties?", "SPM Connect - Process Edit button", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {

                            processmodelcreattion(item);

                        }
                        else
                        {

                        }
                    }

                }
                else
                {
                    //MessageBox.Show("Item does not exist on SPM Connect Server", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    getitemonconnectsql(item);
                }

            }
            this.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        private void getitemonconnectsql(string item)
        {

            additemtosqlinventory(item);
            updateitemtosqlinventory(item);
            processeditbutton();
        }

        private void getitemopenforedit(string item)
        {
            //new Thread(() => new Engineering.WaitFormOpening().ShowDialog()).Start();
            Thread t = new Thread(new ThreadStart(Splashopening));
            t.Start();

            if (getfilename().ToString() != item)
            {
                checkforspmfile(item);
                if (checkforreadonly() == true)
                {
                    chekeditbutton = "yes";
                    //Thread.Sleep(3000);
                    t.Abort();
                    openiteminfo(item);

                }

            }
            else
            {
                //timer1.Start();
                //Thread.Sleep(3000);
                t.Abort();
                openiteminfo(item);

            }
        }

        void Splashopening()
        {
            Engineering.WaitFormOpening waitFormOpening = new Engineering.WaitFormOpening();
            Application.Run(waitFormOpening);
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

        private static String getfilename()
        {
            ModelDoc2 swModel;
            var progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId.ToString()) as SolidWorks.Interop.sldworks.SldWorks;


            int count;
            count = swApp.GetDocumentCount();

            if (count > 0)
            {
                // MessageBox.Show("Number of open documents in this SOLIDWORKS session: " + count);
                swModel = swApp.ActiveDoc as ModelDoc2;

                string filename = swModel.GetTitle();
                return filename;

            }
            else
            {
                return "";
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

        public static bool checkforreadonly()
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int count;
            count = swApp.GetDocumentCount();

            if (count > 0)
            {

                ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                if (swModel.IsOpenedReadOnly())
                {

                    MessageBox.Show("Model is open read only. Please get write access from the associated user in order to edit the properties.", "SPM Connect - Check For Read Only", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Engineering.WaitFormOpening f = new Engineering.WaitFormOpening();
                    f = (Engineering.WaitFormOpening)Application.OpenForms["WaitFormOpening"];
                    f.Invoke(new ThreadStart(delegate { f.Close(); }));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }



        }

        private void additemtosqlinventory(string uniqueid)
        {

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT [SPM_Database].[dbo].[Inventory] (ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber) SELECT * FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber = '" + uniqueid + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Item To SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void updateitemtosqlinventory(string uniqueid)
        {
            NewItem newItem = new NewItem();
            string familycategory = newItem.getfamilycategory(getfamilycode().ToString());
            //MessageBox.Show(familycategory);
            string rupture = "ALWAYS";

            if (familycategory.ToLower() == "purchased")
            {
                rupture = "NEVER";
            }
            string username = getuserfullname(get_username().ToString()).ToString();

            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Inventory] SET FamilyType = '" + familycategory + "',Rupture = '" + rupture + "',JobPlanning = '1',LastSavedBy = '" + username + "',DateCreated = '" + sqlFormattedDate + "',LastEdited = '" + sqlFormattedDate + "'  WHERE ItemNumber = '" + uniqueid + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update Item SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private String getfamilycode()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string familycode;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                familycode = Convert.ToString(slectedrow.Cells[2].Value);
                //MessageBox.Show(familycode);
                return familycode;
            }
            else
            {
                familycode = "";
                return familycode;
            }
        }

        private void processmodelcreattion(string item)
        {
            if (listView.Items.Count == 0 && getfamilycode().ToString() == "")
            {
                openiteminfo(item);
            }
            else
            {
                NewItem newItem = new NewItem();
                string category = newItem.getfamilycategory(getfamilycode().ToString());
                string path = Makepath(item).ToString();
                if (category.ToLower() == "manufactured")
                {
                    //MessageBox.Show(path);
                    System.IO.Directory.CreateDirectory(path);
                    string filename = path + (item) + ".sldprt";
                    newItem.createmodel(filename);
                    string draw = path + (item) + ".slddrw";
                    newItem.createdrawingpart(draw, item);
                    getitemopenforedit(item);

                }
                else if (category.ToLower() == "assembly")
                {
                    System.IO.Directory.CreateDirectory(path);
                    string filename = path + (item) + ".sldasm";
                    newItem.createassy(filename);
                    string draw = path + (item) + ".slddrw";
                    newItem.createdrawingpart(draw, item);
                    getitemopenforedit(item);
                }
                else
                {
                    //DialogResult result = MessageBox.Show("Do you want to create a blank model for a purchased part?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result == DialogResult.Yes)
                    //{
                    //    System.IO.Directory.CreateDirectory(path);
                    //    string filename = path + (item) + ".sldprt";
                    //    newItem.createmodel(filename);
                    //}
                    //else
                    //{

                    //}
                    //return;
                    openiteminfo(item);
                }
            }

        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        #endregion

        #region Copy Item

        private void saveascopybttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to copy this item to a new item?", "SPM Connect - Copy Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {


                if (solidworks_running() == true)
                {
                    string user = get_username();
                    string activeblock = getactiveblock(user);
                    if (activeblock.ToString().Length > 0)
                    {
                        new Thread(() => new Engineering.WaitFormCopying().ShowDialog()).Start();
                        //Thread.Sleep(3000);
                        prepareforcopy(activeblock.ToString());
                    }
                    else
                    {
                        MessageBox.Show("User block number has not been assigned. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }

        }

        private void prepareforcopy(string activeblock)
        {
            string lastnumber = getlastnumber(activeblock.ToString());
            string first3char = getitemnumberselected().Substring(0, 3) + @"\";
            string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            string Pathpart = (spmcadpath + first3char);

            if (Convert.ToInt32(columnexists("Blocks", activeblock.ToString())) == 1)
            {
                if (lastnumber.ToString().Length > 0)
                {
                    if (validnumber(lastnumber.ToString()) == true)
                    {
                        spmnew_idincrement(lastnumber.ToString(), activeblock.ToString());
                        checkitempresentoninventory(uniqueid);
                        if (itempresent == true)
                        {
                            //insertinto_blocks(uniqueid, activeblock.ToString());
                            openiteminfo(uniqueid);
                        }
                        else
                        {
                            copy(Pathpart);
                            aftercopy(activeblock);
                        }

                    }

                }
                else
                {
                    spmnew_id(activeblock.ToString());
                    copy(Pathpart);
                    aftercopy(activeblock);
                }

            }
            else
            {
                //addblockcolumn(activeblock.ToString());
                spmnew_id(activeblock.ToString());
                copy(Pathpart);
                aftercopy(activeblock);
            }
        }

        private void aftercopy(string activeblock)
        {
            if (sucessreplacingreference == true)
            {
                //insertinto_blocks(uniqueid, activeblock.ToString());
                checkitempresentoninventory(getitemnumberselected().ToString());
                if (itempresent == true)
                {
                    addcpoieditemtosqltable();
                }
                else
                {
                    addcpoieditemtosqltablefromgenius(uniqueid, getitemnumberselected().ToString());
                    updateitemtosqlinventory(uniqueid);
                }
                Engineering.WaitFormCopying f = new Engineering.WaitFormCopying();
                f = (Engineering.WaitFormCopying)Application.OpenForms["WaitFormCopying"];
                f.Invoke(new ThreadStart(delegate { f.Close(); }));
                getitemopenforedit(uniqueid);
                //NewItem newItem = new NewItem();
                //newItem.chekbeforefillingcustomproperties(uniqueid);
            }
            else
            {
                MessageBox.Show("SPM Connect failed to update drawing references.! Please manually update drawing references.", "SPM Connect - Copy References", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkitempresentoninventory(getitemnumberselected().ToString());
                if (itempresent == true)
                {
                    addcpoieditemtosqltable();
                }
                else
                {
                    addcpoieditemtosqltablefromgenius(uniqueid, getitemnumberselected().ToString());
                    updateitemtosqlinventory(uniqueid);
                }
                Engineering.WaitFormCopying f = new Engineering.WaitFormCopying();
                f = (Engineering.WaitFormCopying)Application.OpenForms["WaitFormCopying"];
                f.Invoke(new ThreadStart(delegate { f.Close(); }));
                getitemopenforedit(uniqueid);
            }
        }

        private void copy(string Pathpart)
        {
            string type = "";
            string drawingfound = "no";
            string oldpath = "";
            string newfirst3char = uniqueid.Substring(0, 3) + @"\";
            string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";


            string[] s = Directory.GetFiles(Pathpart, "*" + getitemnumberselected().ToString() + "*", SearchOption.TopDirectoryOnly).Where(str => !str.Contains(@"\~$")).ToArray();

            for (int i = 0; i < s.Length; i++)
            {
                //MessageBox.Show(s[i]);
                //MessageBox.Show(Path.GetFileName(s[i]));

                if (s[i].Contains(".sldprt"))
                {
                    //MessageBox.Show("found part");
                    type = "part";
                    oldpath = s[i];

                }
                else if (s[i].Contains(".sldasm"))
                {
                    //MessageBox.Show("found assy");
                    type = "assy";
                    oldpath = s[i];
                }
                else if (s[i].Contains(".slddrw"))
                {
                    //MessageBox.Show("found assy");
                    drawingfound = "yes";

                }
                string filename = Path.GetFileName(s[i]);
                string extension = filename.Substring(filename.IndexOf('.'));

                string newfilepathdir = spmcadpath + newfirst3char;
                System.IO.Directory.CreateDirectory(newfilepathdir);

                string newfileexits = spmcadpath + newfirst3char + uniqueid + extension;

                if (File.Exists(newfileexits))
                {
                    if (MessageBox.Show(newfileexits + " already exists\r\nDo you want to overwrite it?", "Overwrite File - SPM Connect - Copy File Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        File.Copy(s[i], newfileexits, true);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {

                    File.Copy(s[i], newfileexits, false);
                }

            }

            if (drawingfound == "yes")
            {
                string newdraw = spmcadpath + newfirst3char + uniqueid + ".slddrw";
                string newpath = "";
                if (type == "part")
                {
                    newpath = spmcadpath + newfirst3char + uniqueid + ".sldprt";
                }
                else if (type == "assy")
                {
                    newpath = spmcadpath + newfirst3char + uniqueid + ".sldasm";
                }

                replacereference(newdraw, oldpath, newpath);
            }
            else
            {
                sucessreplacingreference = true;
            }



        }

        bool sucessreplacingreference = false;

        private void replacereference(string newdraw, string oldpath, string newpath)
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            sucessreplacingreference = swApp.ReplaceReferencedDocument(newdraw, oldpath, newpath);

        }

        public void addcpoieditemtosqltablefromgenius(string newid, string activeid)
        {


            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT [SPM_Database].[dbo].[Inventory] (ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber) SELECT '" + newid + "',Description,FamilyCode,Manufacturer,ManufacturerItemNumber FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber = '" + activeid + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Copied Item To Inventory From Genius", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void addcpoieditemtosqltable()
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT [SPM_Database].[dbo].[Inventory](ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,Material,Spare,DesignedBy,FamilyType,SurfaceProtection,HeatTreatment,Rupture,JobPlanning,Notes,DateCreated) SELECT '" + uniqueid + "',Description,FamilyCode,Manufacturer,ManufacturerItemNumber,Material,Spare,DesignedBy,FamilyType,SurfaceProtection,HeatTreatment,Rupture,JobPlanning,Notes,'" + sqlFormattedDate + "' FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber = '" + getitemnumberselected().ToString() + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Copied Item To Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        #endregion

        #region solidworks events manager

        //public AssemblyDoc swAssembly;
        //public PartDoc swPart;

        //private int swAssembly_RegenNotify()
        //{
        //    // Display message before rebuild 
        //    //System.Windows.Forms.MessageBox.Show("A rebuild pre-notification event was fired.");
        //    return 0;
        //}

        //public void AttachEventHandlers()
        //{
        //    AttachSWEvents();
        //}

        //public void AttachSWEvents()
        //{
        //    swAssembly.RegenNotify += this.swAssembly_RegenNotify;
        //    swAssembly.RegenPostNotify2 += this.swAssembly_RegenPostNotify2;
        //}

        //private int swAssembly_RegenPostNotify2(object stopFeature)
        //{
        //    // Display message after rebuild 
        //    if ((stopFeature != null))
        //    {
        //        Feature feature = default(Feature);
        //        feature = (Feature)stopFeature;
        //        Debug.Print("The rollback bar is above " + feature.Name + " in the FeatureManager design tree.");
        //    }
        //    //System.Windows.Forms.MessageBox.Show("A rebuild post-notification event was fired.");
        //    return 0;
        //}

        //private int swPart_RegenNotify()
        //{
        //    // Display message before rebuild 
        //    System.Windows.Forms.MessageBox.Show("rebuild started");
        //    return 0;
        //}

        //public void AttachEventHandlersPart()
        //{
        //    AttachSWEventsPart();
        //}

        //SldWorks mysolidworks = Marshal.GetActiveObject("SldWorks.Application") as SldWorks;

        //public void AttachSWEventsPart()
        //{
        //    swPart.RegenNotify += this.swPart_RegenNotify;
        //    swPart.RegenPostNotify2 += this.swPart_RegenPostNotify2;
        //    swPart.FileSaveNotify += this.swPart_FileSaveNotify;

        //}

        //private int swPart_RegenPostNotify2(object stopFeature)
        //{
        //    // Display message after rebuild 
        //    if ((stopFeature != null))
        //    {
        //        Feature feature = default(Feature);
        //        feature = (Feature)stopFeature;
        //        Debug.Print("The rollback bar is above " + feature.Name + " in the FeatureManager design tree.");
        //    }
        //    System.Windows.Forms.MessageBox.Show("rebuild completed");
        //    return 0;
        //}

        //private int swPart_FileSaveNotify(string FileName)
        //{
        //    //MessageBox.Show("file saved");
        //    return 1;
        //}

        //private int mysolidworks_activedocchange()
        //{
        //    //MessageBox.Show("active document changed");
        //    ModelDoc2 swmodel = mysolidworks.ActiveDoc;
        //    int type = swmodel.GetType();

        //    if (type == (int)swDocumentTypes_e.swDocPART)
        //    {
        //        //MessageBox.Show("partfound");
        //        swPart = (PartDoc)swmodel;
        //        AttachEventHandlersPart();
        //    }
        //    else if (type == (int)swDocumentTypes_e.swDocASSEMBLY)
        //    {
        //        //MessageBox.Show("Assembly found");
        //        swAssembly = (AssemblyDoc)swmodel;
        //        AttachEventHandlers();
        //    }

        //    //AttachEventHandlersPart();


        //    return 1;
        // }

        #endregion

        private void prorcessreportbom(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(itemvalue);
            form1.getreport(Reportname);
            form1.Show();

        }

        private void billsOfMaunfacturingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(),"BOM");
        }

        private void sparePartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "SPAREPARTS");
        }
    }

}



