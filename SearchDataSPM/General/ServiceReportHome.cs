using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ServiceReportHome : Form
    {
        #region Shipping Home Load

        private string connection;
        private SqlConnection cn;
        private DataTable dt;
        private bool formloading = false;
        private string userfullname = "";
        private int _advcollapse = 0;
        private SPMConnectAPI.Service connectapi = new SPMConnectAPI.Service();
        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public ServiceReportHome()
        {
            InitializeComponent();
            formloading = true;

            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error Connecting to SQL Server.....", "SPM Connect - Shipping Home Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            formloading = true;
            collapse();
            dt = new DataTable();
            Checkdeptsandrights();
            userfullname = connectapi.getuserfullname();
            Showallitems(true);
            txtSearch.Focus();
            formloading = false;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Service Report Home ");
        }

        private void Checkdeptsandrights()
        {
            string userName = SPMConnectAPI.Helper.GetUserName();
            versionlabel.Text = connectapi.getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connnect " + versionlabel.Text);
        }

        private void fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            FillCustomers();
            FillProjectManagers();
            FillTechnicians();
            FillJobNumbers();
            FillAuthorizedBy();
            FillEquipment();
            FillRefJob();
            clearfilercombos();
            formloading = false;
            Cursor.Current = Cursors.Default;
        }

        private void clearfilercombos()
        {
            jobnumbercombobox.SelectedItem = null;
            projectmangercombobox.SelectedItem = null;
            technamecomboxbox.SelectedItem = null;
            authorizedbycombo.SelectedItem = null;
            customercomboxbox.SelectedItem = null;
            jobnumbercombobox.SelectedItem = null;
            refjobcombobox.SelectedItem = null;
            jobnumbercombobox.Text = null;
            projectmangercombobox.Text = null;
            technamecomboxbox.Text = null;
            authorizedbycombo.Text = null;
            customercomboxbox.Text = null;
            jobnumbercombobox.Text = null;
            refjobcombobox.Text = null;
            equipmentcomboBox.Text = null;
        }

        private void Showallitems(bool showall)
        {
            if (showall)
            {
                dt.Clear();
                dt = connectapi.ShowAllServiceReports();
            }

            dataGridView.DataSource = dt;
            DataView dv = dt.DefaultView;
            //

            dataGridView.Columns[2].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[11].Visible = false;

            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[1].Width = 70;
            dataGridView.Columns[3].Width = 110;
            dataGridView.Columns[9].Width = 140;
            dataGridView.Columns[10].Width = 120;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
            UpdateFont();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            performreload();
        }

        private void performreload()
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
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        #endregion Shipping Home Load

        #region Public Table & variables

        // variables required outside the functions to perfrom
        // string fullsearch = ("ECRNo LIKE '%{0}%' ");
        private string fullsearch = ("FullSearch LIKE '%{0}%'");

        //string ItemNo;
        //string str;
        private DataTable table0 = new DataTable();

        private DataTable table1 = new DataTable();
        private DataTable table2 = new DataTable();
        private DataTable table3 = new DataTable();
        private DataTable table4 = new DataTable();
        private DataTable dataTable = new DataTable();

        #endregion Public Table & variables

        #region Search Parameters

        public void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                formloading = true;

                if (Descrip_txtbox.Visible == true)
                {
                    clearandhide();
                }

                if (jobnumbercombobox.Text == "" && authorizedbycombo.Text == "" && customercomboxbox.Text == "" && technamecomboxbox.Text == "" && projectmangercombobox.Text == "" && refjobcombobox.Text == "" && equipmentcomboBox.Text == "")
                {
                    Showallitems(true);
                }
                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");
                    mainsearch();
                }
                else
                {
                    SearchStringPosition(txtSearch.Text);
                    searchtext(txtSearch.Text);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;

                formloading = false;
            }
        }

        private void clearandhide()
        {
            formloading = true;
            clearfilercombos();

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
            table4.Clear();
            dataTable.Clear();
            table0.Dispose();
            table1.Dispose();
            table2.Dispose();
            dt.Dispose();
            table3.Dispose();
            table4.Dispose();
            dataTable.Dispose();
            recordlabel.Text = "";
            formloading = false;
        }

        private void mainsearch()
        {
            formloading = true;
            //DataView dv = dt.DefaultView;
            DataView dv = (dataGridView.DataSource as DataTable).DefaultView;
            dt = dv.ToTable();
            string search1 = txtSearch.Text;
            try
            {
                search1 = search1.Replace("'", "''");
                search1 = search1.Replace("[", "[[]");
                dv.RowFilter = string.Format(fullsearch, search1);

                table0 = dv.ToTable();
                dataGridView.DataSource = table0;
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

            formloading = false;
        }

        private void Descrip_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table0.DefaultView;
            table0 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                formloading = true;
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
                    table1 = dv.ToTable();
                    dataGridView.DataSource = table1;
                    SearchStringPosition(Descrip_txtbox.Text);
                    searchtext(Descrip_txtbox.Text);
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
                formloading = false;
            }
        }

        private void filteroem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table1.DefaultView;
            table1 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                formloading = true;
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

                    table2 = dv.ToTable();
                    dataGridView.DataSource = table2;
                    SearchStringPosition(filteroem_txtbox.Text);
                    searchtext(filteroem_txtbox.Text);
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
                if (splitContainer1.Panel2Collapsed == false && this.Width <= 800)
                {
                    this.Size = new Size(1200, this.Height);
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
                formloading = false;
            }
        }

        private void filteroemitem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table2.DefaultView;
            table2 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                formloading = true;
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

                    table3 = dv.ToTable();
                    dataGridView.DataSource = table3;
                    SearchStringPosition(filteroemitem_txtbox.Text);
                    searchtext(filteroemitem_txtbox.Text);
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
                if (splitContainer1.Panel2Collapsed == false && this.Width <= 800)
                {
                    this.Size = new Size(1200, this.Height);
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
                formloading = false;
            }
        }

        private void filter4_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table3.DefaultView;
            table3 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                formloading = true;
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
                    table4 = dv.ToTable();
                    dataGridView.DataSource = table4;
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
                formloading = false;
            }
        }

        #endregion Search Parameters

        #region datagridview events

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

        private void dataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
            }
        }

        private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
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

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
            {
            }
        }

        #endregion datagridview events

        #region Highlight Search Results

        private bool IsSelected = false;

        public void SearchStringPosition(string Searchstring)
        {
            IsSelected = true;
        }

        private string sw;

        public void searchtext(string searchkey)
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

        #endregion Highlight Search Results

        #region Closing SPMConnect

        private void SPM_Connect_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Service Report Home ");
            this.Dispose();
        }

        private void SPM_Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        #endregion Closing SPMConnect

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

            if (keyData == (Keys.Shift | Keys.OemPeriod))
            {
                if (splitContainer1.Panel2Collapsed == true)
                {
                    advsearchbttnclick();
                }
                return true;
            }

            if (keyData == (Keys.Shift | Keys.Oemcomma))
            {
                if (splitContainer1.Panel2Collapsed == false)
                {
                    advsearchbttnclick();
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region Advance Filtersf

        private void advsearchbttn_Click(object sender, EventArgs e)
        {
            advsearchbttnclick();
        }

        private void advsearchbttnclick()
        {
            if (_advcollapse == 0)
            {
                fillinfo();
                _advcollapse = 1;
            }
            collapse();
        }

        private void collapse()
        {
            if (splitContainer1.Panel2Collapsed == true)
            {
                advsearchbttn.Text = "<<";
                splitContainer1.Panel2Collapsed = false;
                this.Size = new Size(1060, this.Height);
                splitContainer1.SplitterDistance = this.Width - 170;
            }
            else
            {
                advsearchbttn.Text = ">>";

                this.Size = new Size(900, this.Height);

                splitContainer1.Panel2Collapsed = true;
                splitContainer1.SplitterDistance = this.Width - 170;
            }
        }

        private void FilterProducts()
        {
            if (formloading)
            {
            }
            else
            {
                string filter = "";
                if (jobnumbercombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND [Job No] = '{0}'", jobnumbercombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("[Job No] = '{0}'", jobnumbercombobox.Text.ToString());
                    }
                }
                if (projectmangercombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ProjectManager = '{0}'", projectmangercombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("ProjectManager = '{0}'", projectmangercombobox.Text.ToString());
                    }
                }
                if (authorizedbycombo.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Authorizedby = '{0}'", authorizedbycombo.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("Authorizedby = '{0}'", authorizedbycombo.Text.ToString());
                    }
                }
                if (customercomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Customer = '{0}'", customercomboxbox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("Customer = '{0}'", customercomboxbox.Text.ToString());
                    }
                }
                if (technamecomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND [Tech Name] LIKE '%{0}%'", technamecomboxbox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("[Tech Name] LIKE '%{0}%'", technamecomboxbox.Text.ToString());
                    }
                }
                if (refjobcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND RefJob  LIKE '%{0}%'", refjobcombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("RefJob  LIKE '%{0}%'", refjobcombobox.Text.ToString());
                    }
                }
                if (equipmentcomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Equipment LIKE '%{0}%'", equipmentcomboBox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("Equipment LIKE '%{0}%'", equipmentcomboBox.Text.ToString());
                    }
                }

                if (jobnumbercombobox.SelectedItem == null && authorizedbycombo.SelectedItem == null && customercomboxbox.SelectedItem == null && technamecomboxbox.SelectedItem == null && projectmangercombobox.SelectedItem == null && refjobcombobox.SelectedItem == null && equipmentcomboBox.SelectedItem == null)
                {
                }
                advfiltertables(filter);
            }
        }

        private void advfiltertables(string filter)
        {
            if (!Descrip_txtbox.Visible)
            {
                dataGridView.DataSource = dt;
                dataTable.Clear();
                (dataGridView.DataSource as DataTable).DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (Descrip_txtbox.Visible)
            {
                dataGridView.DataSource = table0;
                dataTable.Clear();
                dataGridView.Refresh();
                table0.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filteroem_txtbox.Visible)
            {
                dataGridView.DataSource = table1;
                dataTable.Clear();
                table1.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filteroemitem_txtbox.Visible)
            {
                dataGridView.DataSource = table2;
                dataTable.Clear();
                table2.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filter4.Visible)
            {
                dataGridView.DataSource = table3;
                dataTable.Clear();
                table3.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            else
            {
                dataGridView.DataSource = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataTable.Clear();
                (dataGridView.DataSource as DataTable).DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable).DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
        }

        #region fillcomboboxes

        private void FillJobNumbers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillReportJobNumbers();
            jobnumbercombobox.AutoCompleteCustomSource = MyCollection;
            jobnumbercombobox.DataSource = MyCollection;
        }

        private void FillAuthorizedBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillAuthorizedBy();
            authorizedbycombo.AutoCompleteCustomSource = MyCollection;
            authorizedbycombo.DataSource = MyCollection;
        }

        private void FillProjectManagers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillProjectManagers();
            projectmangercombobox.AutoCompleteCustomSource = MyCollection;
            projectmangercombobox.DataSource = MyCollection;
        }

        private void FillTechnicians()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillTechnicians();
            technamecomboxbox.AutoCompleteCustomSource = MyCollection;
            technamecomboxbox.DataSource = MyCollection;
        }

        private void FillCustomers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillCustomers();
            customercomboxbox.AutoCompleteCustomSource = MyCollection;
            customercomboxbox.DataSource = MyCollection;
        }

        private void FillEquipment()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillEquipment();
            equipmentcomboBox.AutoCompleteCustomSource = MyCollection;
            equipmentcomboBox.DataSource = MyCollection;
        }

        private void FillRefJob()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillRefJob();
            refjobcombobox.AutoCompleteCustomSource = MyCollection;
            refjobcombobox.DataSource = MyCollection;
        }

        #endregion fillcomboboxes

        #region advance filters events

        private void clrfiltersbttn_Click(object sender, EventArgs e)
        {
            performreload();
        }

        private void ActiveCadblockcombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void oemitemcombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void designedbycombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lastsavedbycombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Manufactureritemcomboxbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void familycomboxbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void MaterialcomboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion advance filters events

        #endregion Advance Filtersf

        #region Invoice

        private void invoiceinfostripmenu_Click(object sender, EventArgs e)
        {
            string report = getselectedinvoicenumber();

            ReportViewer form1 = new ReportViewer(_reportname: "Service", _item: report);

            form1.Show();
        }

        private string getselectedinvoicenumber()
        {
            string item;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(item);
                return item;
            }
            else
            {
                item = "";
                return item;
            }
        }

        private void ContextMenuStripShipping_Opening(object sender, CancelEventArgs e)
        {
            if (!(dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count == 1)) e.Cancel = true;
        }

        #endregion Invoice

        private void jobnumbercombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                jobnumbercombobox.Focus();
            }
        }

        private void deptrequestedcomboxbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                customercomboxbox.Focus();
            }
        }

        private void ecrstatuscombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                projectmangercombobox.Focus();
            }
        }

        private void requestedbycomboxbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                technamecomboxbox.Focus();
            }
        }

        private void supervicsorcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                equipmentcomboBox.Focus();
            }
        }

        private void approvedbycombo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                authorizedbycombo.Focus();
            }
        }

        private void completedbycombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                refjobcombobox.Focus();
            }
        }
    }
}