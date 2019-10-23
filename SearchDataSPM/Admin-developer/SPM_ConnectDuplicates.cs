using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;



namespace SearchDataSPM
{

    public partial class SPM_ConnectDuplicates : Form

    {
        #region SPM Connect Load

        String connection;
        SqlConnection cn;
        DataTable dt;

        public SPM_ConnectDuplicates()

        {
            InitializeComponent();
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            dt = new DataTable();
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            //Text = SearchDataSPM.Properties.Settings.Default.Title;          
            Connect_SPMSQL();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            // MessageBox.Show(userName);
        }

        private void Connect_SPMSQL()

        {
            try
            {
                cn = new SqlConnection(connection);
                //Showallitems();
                showduplicates();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Not Licensed User", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();

            }
            finally
            {
                cn.Close();
            }
        }

        private void Showallitems()
        {

            if (cn.State == ConnectionState.Closed)
                cn.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UnionInventory] ORDER BY ItemNumber DESC", cn);

            sda.Fill(dt);
            dataGridView.DataSource = dt;
            UpdateFont();
            //dataGridView.Location = new Point(0, 40);

        }


        private void Reload_Click(object sender, EventArgs e)
        {
            //Stopwatch sw = Stopwatch.StartNew();		
            filteroemitem_txtbox.Clear();
            filteroem_txtbox.Clear();
            Descrip_txtbox.Clear();
            Descrip_txtbox.Focus();
            SendKeys.Send("~");
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dt.Rows.Clear();
            dataGridView.Refresh();
            // Showallitems();
            showduplicates();
            //TimeSpan elapsed = sw.Elapsed;
            //MessageBox.Show("Query took: " + elapsed.TotalSeconds);

        }

        private void UpdateFont()
        {
            /*//Change cell font
			foreach (DataGridViewColumn c in dataGridView.Columns)
			{
				c.DefaultCellStyle.Font = new Font("Arial", 10.0F, GraphicsUnit.Pixel);
			}
			*/
            //dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        #endregion

        #region Public Table & variables

        public void shailsvariables()

        {
            // define all the filter string for the colums
            //string Rowoemitem = ("ManufacturerItemNumber LIKE '%{0}%'");
            //string Rowoem = ("Manufacturer LIKE '%{0}%'");
            //string Rowdescription = ("Description LIKE '%{0}%'");
            //string full2Filter =("Description LIKE '%{1}%' OR Manufacturer LIKE '%{1}%' OR ManufacturerItemNumber LIKE '%{1}%' OR ItemNumber LIKE '%{1}%'");		
        }

        // variables required outside the functions to perfrom
        string fullsearch = ("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'");
        string ItemNo;
        string str;
        DataTable table0 = new DataTable();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();


        #endregion

        #region Search Parameters

        private void txtSearch_DoubleClick(object sender, EventArgs e)
        {
            txtSearch.Clear();
            SendKeys.Send("~");

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            /*DataView dv = dt.DefaultView;
			dv.RowFilter = string.Format("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'", txtSearch.Text);
			dataGridView.DataSource = dv;
			dataGridView.Update();
			dataGridView.Refresh();
	*/
        }

        public void txtSearch_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Return)

            {
                //Stopwatch sw = Stopwatch.StartNew();
                //var result = fullsearch.ToList();				
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
                    SendKeys.Send("~");
                }


                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    Descrip_txtbox.Hide();
                    Descrip_txtbox.Clear();
                    filteroem_txtbox.Hide();
                    filteroem_txtbox.Clear();
                    filteroemitem_txtbox.Hide();
                    filteroemitem_txtbox.Clear();
                    filter4.Hide();
                    filter4.Clear();
                    //TimeSpan elapsed = sw.Elapsed;
                    //MessageBox.Show("Query took: " + elapsed.TotalSeconds);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
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
            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();
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
            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();
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

            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();
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
            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        #endregion

        #region OpenModel and datagridview events

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //ItemNo = dataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
            //ItemNo = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            //myValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //MessageBox.Show(ItemNo);

            //int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            //DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            //string c = Convert.ToString(columnchk.Index);
            ////MessageBox.Show(c);

            //if (dataGridView.SelectedCells.Count == 1 && c == "1")
            //{

            //	int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            //	DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            //	ItemNo = Convert.ToString(slectedrow.Cells[1].Value);
            //	//MessageBox.Show(ItemNo);
            //	checkforprocess();
            //	checkforspmfile(ItemNo);
            //}
            //else if (dataGridView.SelectedCells.Count == 1 && c == "0")
            //{
            //	int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            //	DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            //	str = Convert.ToString(slectedrow.Cells[0].Value);
            //	//MessageBox.Show(str);
            //	checkforprocess();
            //	checkforspmdrwfile(str);

            //}
            //str = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            //str = dataGridView[itemNumberDataGridViewTextBoxColumn, e.RowIndex].Value;

        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedCells.Count == 1 && c == "1")
            {

                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                ItemNo = Convert.ToString(slectedrow.Cells[1].Value);
                //MessageBox.Show(ItemNo);
                //  checkforprocess();
                checkforspmfile(ItemNo);
            }
            else if (dataGridView.SelectedCells.Count == 1 && c == "0")
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                str = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(str);
                // checkforprocess();
                checkforspmdrwfile(str);

            }
        }

        private void checkforprocess()
        {
            //if (checkBox1.Checked)

            //{
            //    Process[] pname = Process.GetProcessesByName("eDrawings");
            //    if (pname.Length == 0)
            //    {
            //        Process.Start(@"C:\Program Files\SOLIDWORKS Corp\eDrawings\EModelViewer.exe");

            //    }

            //    else if (pname.Length == 0)
            //    {
            //        Process.Start(@"C:\Program Files\Common Files\eDrawings2017\EModelViewer.exe");
            //    }
            //}

            //else
            //{
            //    Process[] pname = Process.GetProcessesByName("SLDWORKS");
            //    if (pname.Length == 0)
            //    {
            //        Process.Start(@"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\SLDWORKS.exe");

            //    }

            //}

            //checkforspmfile(ItemNo);
        }

        private void checkforspmfile(string first3char)
        {
            string ItemNumbero;
            ItemNumbero = ItemNo + "-0";

            if (!String.IsNullOrWhiteSpace(ItemNo) && ItemNo.Length == 6)

            {
                first3char = ItemNo.Substring(0, 3) + @"\";
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

        private void checkforspmdrwfile(string first3char)
        {
            string ItemNumbero = str + "-0";


            if (!String.IsNullOrWhiteSpace(str) && str.Length == 6)

            {
                first3char = str.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + str + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");


                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
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

                    MessageBox.Show($"A file with the part number" + ItemNo + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

            }

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
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
            ItemNo = Convert.ToString(slectedrow.Cells[1].Value);
            //MessageBox.Show(ItemNo);
            //  checkforprocess();
            checkforspmfile(ItemNo);
        }

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            str = Convert.ToString(slectedrow.Cells[0].Value);
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


            if (e.RowIndex >= 0 & e.ColumnIndex >= 1 & IsSelected)
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

        #endregion

        #region Duplicates

        private void showduplicates()
        {
            dataGridView.DataSource = null;
            dt.Rows.Clear();

            if (cn.State == ConnectionState.Closed)
                cn.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ManufactureItemDuplicatesView]", cn);

            sda.Fill(dt);
            dataGridView.DataSource = dt;
            UpdateFont();

        }

        #endregion

        #region ParentView

        // public static string whereused;

        private void ParentView_Click(object sender, EventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                ItemNo = Convert.ToString(slectedrow.Cells[1].Value);
                //MessageBox.Show(ItemNo);
                //whereused = ItemNo;
                //ParentView parentView = new ParentView();
                //parentView.Show();
                WhereUsed whereUsed = new WhereUsed();
                whereUsed.item(ItemNo);
                whereUsed.Show();

            }
            else
            {
                //whereused = "";
                //ParentView parentView = new ParentView();
                //parentView.Show();
                WhereUsed whereUsed = new WhereUsed();
                whereUsed.Show();
            }

        }

        private void whereUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParentView_Click(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ParentView_Click(sender, e);
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

            if (dataGridView.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                ItemNo = Convert.ToString(slectedrow.Cells[1].Value);
                //MessageBox.Show(ItemNo);
                //assytree = ItemNo;
                TreeView treeView = new TreeView();
                treeView.item(ItemNo);
                treeView.Show();
            }
            else
            {
                //assytree = "";
                TreeView treeView = new TreeView();
                treeView.Show();
            }
        }

        private void bOMToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //TreeView_Bttn.PerformClick();
            TreeView_Bttn_Click(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeView_Bttn_Click(sender, e);
        }

        #endregion

        #region Closing SPMConnect

        public static bool checkboxshop = false;

        private void SPM_Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void SPM_Connect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode.ToString() == "A")
            {

                Reload.PerformClick();

            }
        }

        #endregion

        #region miscellaneous Testing DELETE

        private void delete_Click(object sender, EventArgs e)
        {
            //var delitem =  Microsoft.VisualBasic.Interaction.InputBox("What's the ItemNumber?", "Delete ItemNumber From SPM Connect", "A");

            //string itemvalue = delitem.ToString();

            //itemvalue = "\'" + itemvalue + "\'";
            if (txtSearch.Text.Length > 0)
            {

                if (this.cn.State == ConnectionState.Closed)
                    this.cn.Open();
                string query = "DELETE FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber ='" + txtSearch.Text.ToString() + "'";
                SqlCommand sda = new SqlCommand(query, cn);
                sda.ExecuteNonQuery();

                MessageBox.Show(txtSearch.Text + " - Is deleted Now!");

                cn.Close();
            }


            //try
            //{
            //    using (var sc = new SqlConnection(connection))
            //    using (var cmd = sc.CreateCommand())
            //    {
            //        sc.Open();
            //        cmd.CommandText = "delete from Inventory where ItemNumber = '" + txtSearch.Text.ToString() + "'";

            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show( "Deleted  "+ txtSearch.Text + "");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}



            //SqlCommand Cmd = new SqlCommand("delete from Inventory where ItemNumber=@delitem", cn);
            ////Cmd.Parameters.Add(new SqlParameter("delitem", delitem));
            //Cmd.Parameters.AddWithValue("delitem", delitem);
            //SqlDataAdapter dt = new SqlDataAdapter(Cmd);
            //SqlDataReader reader = Cmd.ExecuteReader();
            //while (reader.Read())
            //{

            //}
            //SqlDataAdapter dt = new SqlDataAdapter(Cmd);
            //DataSet ds = new DataSet();

            //SqlCommand cm = new SqlCommand("", cn);
            //cm.CommandText = "delete from Inventory where ItemNumber=@delitem";
            //cm.Parameters.Add("@delitem", SqlDbType.VarChar).Value = delitem;
        }





        #endregion

    }
}

