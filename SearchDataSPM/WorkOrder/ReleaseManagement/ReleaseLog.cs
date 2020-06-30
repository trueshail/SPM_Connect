using SPMConnectAPI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SearchDataSPM.WorkOrder.ReleaseManagement
{
    public partial class ReleaseLog : Form
    {
        #region Shipping Home Load

        private readonly SPMConnectAPI.WorkOrder connectapi = new SPMConnectAPI.WorkOrder();
        private int _advcollapse;
        private DataTable dt;
        private bool formloading;
        private log4net.ILog log;

        public ReleaseLog()
        {
            InitializeComponent();
            formloading = true;
        }

        private void Clearfilercombos()
        {
            Createdbycomboxbox.SelectedItem = null;
            ApprovedBycombobox.SelectedItem = null;
            Prioritycombox.SelectedItem = null;
            ReleasedBycombox.SelectedItem = null;
            CheckedBycomboxbox.SelectedItem = null;
            Createdbycomboxbox.SelectedItem = null;
            LastSavedcombobox.SelectedItem = null;
            Createdbycomboxbox.Text = null;
            ApprovedBycombobox.Text = null;
            Prioritycombox.Text = null;
            ReleasedBycombox.Text = null;
            CheckedBycomboxbox.Text = null;
            Createdbycomboxbox.Text = null;
            LastSavedcombobox.Text = null;
            ByStatuscomboBox.Text = null;
        }

        private void Fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            FillCheckedBy();
            FillApprovedBy();
            FillCreatedBy();
            FillReleaseBy();
            FillLastSavedBy();
            Clearfilercombos();
            formloading = false;
            Cursor.Current = Cursors.Default;
        }

        private void Performreload()
        {
            Clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            DataGridView.Refresh();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Performreload();
        }

        private void Showallitems()
        {
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
               .SetValue(DataGridView, true, null);

            dt.Clear();
            dt = connectapi.ShowAllReleaseLogs();
            DataGridView.DataSource = dt;
            _ = dt.DefaultView;
            //dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Descending);
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["PckgQty"].Visible = false;
            DataGridView.Columns["IsSubmitted"].HeaderText = "Submitted";
            DataGridView.Columns["JobDes"].HeaderText = "Job Description";
            DataGridView.Columns["SubAssyDes"].HeaderText = "Sub-Assy Description";
            DataGridView.Columns["RelNo"].HeaderText = "Rel No";
            DataGridView.Columns["SubmittedTo"].Visible = false;
            DataGridView.Columns["IsChecked"].HeaderText = "Checked";
            DataGridView.Columns["IsApproved"].HeaderText = "Approved";
            DataGridView.Columns["IsReleased"].HeaderText = "Released";
            DataGridView.Columns["ApprovalTo"].Visible = false;
            DataGridView.Columns["CreatedById"].Visible = false;
            DataGridView.Columns["LastSavedById"].Visible = false;
            DataGridView.Columns["IsActive"].HeaderText = "Active";
            DataGridView.Columns["ConnectRelNo"].HeaderText = "Release No";
            UpdateFont();
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            formloading = true;
            Collapse();
            dt = new DataTable();
            Showallitems();
            txtSearch.Focus();
            formloading = false;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Release Logs ");
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void UpdateFont()
        {
            DataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            DataGridView.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            DataGridView.DefaultCellStyle.ForeColor = Color.Black;
            DataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            DataGridView.DefaultCellStyle.SelectionForeColor = Color.Orange;
            DataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 48, 56);
        }

        #endregion Shipping Home Load

        #region Public Table & variables

        private readonly string fullsearch = ("CONVERT(RelNo, 'System.String') LIKE '%{0}%' OR CONVERT(Job, 'System.String') LIKE '%{0}%' OR JobDes LIKE '%{0}%' OR SubAssy LIKE '%{0}%' OR SubAssyDes LIKE '%{0}%'");

        private DataTable dataTable = new DataTable();
        private DataTable table0 = new DataTable();
        private DataTable table1 = new DataTable();
        private DataTable table2 = new DataTable();
        private DataTable table3 = new DataTable();
        private DataTable table4 = new DataTable();

        #endregion Public Table & variables

        #region Search Parameters

        public void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                formloading = true;

                if (Descrip_txtbox.Visible)
                {
                    Clearandhide();
                }

                if (string.IsNullOrEmpty(Createdbycomboxbox.Text) && string.IsNullOrEmpty(ReleasedBycombox.Text) && string.IsNullOrEmpty(CheckedBycomboxbox.Text) && string.IsNullOrEmpty(Prioritycombox.Text) && string.IsNullOrEmpty(ApprovedBycombobox.Text) && string.IsNullOrEmpty(LastSavedcombobox.Text) && string.IsNullOrEmpty(ByStatuscomboBox.Text))
                {
                    Showallitems();
                }
                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");
                    Mainsearch();
                }
                else
                {
                    SearchStringPosition();
                    Searchtext(txtSearch.Text);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;

                formloading = false;
            }
        }

        private void Clearandhide()
        {
            formloading = true;
            Clearfilercombos();

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
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = secondFilter;
                    else
                        dv.RowFilter += " AND " + secondFilter;
                    table1 = dv.ToTable();
                    DataGridView.DataSource = table1;
                    SearchStringPosition();
                    Searchtext(Descrip_txtbox.Text);
                    DataGridView.Refresh();
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
                formloading = false;
            }
        }

        private void Filter4_KeyDown(object sender, KeyEventArgs e)
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

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fifthfilter;
                    else
                        dv.RowFilter += " AND " + fifthfilter;
                    table4 = dv.ToTable();
                    DataGridView.DataSource = table4;
                    SearchStringPosition();
                    Searchtext(filter4.Text);
                    DataGridView.Refresh();
                    recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search5");
                    filter4.Clear();
                    SendKeys.Send("~");
                }
                finally
                {
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                formloading = false;
            }
        }

        private void Filteroem_txtbox_KeyDown(object sender, KeyEventArgs e)
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
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = thirdFilter;
                    else
                        dv.RowFilter += " AND " + thirdFilter;

                    table2 = dv.ToTable();
                    DataGridView.DataSource = table2;
                    SearchStringPosition();
                    Searchtext(filteroem_txtbox.Text);
                    DataGridView.Refresh();
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
                }
                if (!splitContainer1.Panel2Collapsed && this.Width <= 800)
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

        private void Filteroemitem_txtbox_KeyDown(object sender, KeyEventArgs e)
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

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fourthfilter;
                    else
                        dv.RowFilter += " AND " + fourthfilter;

                    table3 = dv.ToTable();
                    DataGridView.DataSource = table3;
                    SearchStringPosition();
                    Searchtext(filteroemitem_txtbox.Text);
                    DataGridView.Refresh();
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
                }
                if (!splitContainer1.Panel2Collapsed && this.Width <= 800)
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

        private void Mainsearch()
        {
            formloading = true;
            //DataView dv = dt.DefaultView;
            DataView dv = (DataGridView.DataSource as DataTable)?.DefaultView;
            dt = dv.ToTable();
            string search1 = txtSearch.Text;
            try
            {
                search1 = search1.Replace("'", "''");
                search1 = search1.Replace("[", "[[]");
                dv.RowFilter = string.Format(fullsearch, search1);

                table0 = dv.ToTable();
                DataGridView.DataSource = table0;
                DataGridView.Update();
                SearchStringPosition();
                Searchtext(search1);
                DataGridView.Refresh();
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
            }

            formloading = false;
        }

        #endregion Search Parameters

        #region datagridview events

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView.SelectedCells.Count == 1)
            {
                ShowReleaseInfo(GetselectedReleaseNo());
            }
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            _ = DataGridView.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                DataGridView.ClearSelection();
                DataGridView.Rows[columnindex].Selected = true;
            }
        }

        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            }
        }

        private void DataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                DataGridView.Enabled = false;
                DataGridView.GetNextControl(DataGridView, true).Focus();
                DataGridView.Enabled = true;
                e.Handled = true;
            }
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
        }

        #endregion datagridview events

        #region Highlight Search Results

        private bool IsSelected;

        private string sw;

        public void SearchStringPosition()
        {
            IsSelected = true;
        }

        public void Searchtext(string searchkey)
        {
            sw = searchkey;
        }

        private void DataGridView_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
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

        #endregion Highlight Search Results

        #region Closing SPMConnect

        private void SPM_Connect_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Material Re-Allocation ");
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
                if (splitContainer1.Panel2Collapsed)
                {
                    Advsearchbttnclick();
                }
                return true;
            }

            if (keyData == (Keys.Shift | Keys.Oemcomma))
            {
                if (!splitContainer1.Panel2Collapsed)
                {
                    Advsearchbttnclick();
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region Advance Filters

        private void Advfiltertables(string filter)
        {
            if (!Descrip_txtbox.Visible)
            {
                DataGridView.DataSource = dt;
                dataTable.Clear();
                DataGridView.Refresh();
                dt.DefaultView.RowFilter = filter;
                dataTable = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                DataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
                return;
            }
            if (Descrip_txtbox.Visible)
            {
                DataGridView.DataSource = table0;
                dataTable.Clear();
                DataGridView.Refresh();
                table0.DefaultView.RowFilter = filter;
                dataTable = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                DataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filteroem_txtbox.Visible)
            {
                DataGridView.DataSource = table1;
                dataTable.Clear();
                table1.DefaultView.RowFilter = filter;
                dataTable = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                DataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filteroemitem_txtbox.Visible)
            {
                DataGridView.DataSource = table2;
                dataTable.Clear();
                table2.DefaultView.RowFilter = filter;
                dataTable = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                DataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filter4.Visible)
            {
                DataGridView.DataSource = table3;
                dataTable.Clear();
                table3.DefaultView.RowFilter = filter;
                dataTable = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                DataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            else
            {
                DataGridView.DataSource = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataTable.Clear();
                ((DataGridView.DataSource as DataTable)?.DefaultView.ToTable()).DefaultView.RowFilter = filter;
                dataTable = (DataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                DataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + DataGridView.Rows.Count.ToString() + " Matching Items.";
            }
        }

        private void Advsearchbttn_Click(object sender, EventArgs e)
        {
            Advsearchbttnclick();
        }

        private void Advsearchbttnclick()
        {
            if (_advcollapse == 0)
            {
                Fillinfo();
                _advcollapse = 1;
            }
            Collapse();
        }

        private void Collapse()
        {
            if (splitContainer1.Panel2Collapsed)
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
            if (!formloading)
            {
                string filter = "";
                if (Createdbycomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND CreatedBy = '{0}'", Createdbycomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("CreatedBy = '{0}'", Createdbycomboxbox.Text);
                    }
                }
                if (ApprovedBycombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ApprovedBy = '{0}'", ApprovedBycombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("ApprovedBy = '{0}'", ApprovedBycombobox.Text);
                    }
                }
                if (ReleasedBycombox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ReleasedBy = '{0}'", ReleasedBycombox.Text);
                    }
                    else
                    {
                        filter += string.Format("ReleasedBy = '{0}'", ReleasedBycombox.Text);
                    }
                }
                if (CheckedBycomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND CheckedBy = '{0}'", CheckedBycomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("CheckedBy = '{0}'", CheckedBycomboxbox.Text);
                    }
                }
                if (Prioritycombox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Priority LIKE '%{0}%'", Prioritycombox.Text);
                    }
                    else
                    {
                        filter += string.Format("Priority LIKE '%{0}%'", Prioritycombox.Text);
                    }
                }
                if (LastSavedcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND LastSavedBy LIKE '%{0}%'", LastSavedcombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("LastSavedBy LIKE '%{0}%'", LastSavedcombobox.Text);
                    }
                }
                if (ByStatuscomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += " AND " + StatusFilter(ByStatuscomboBox.Text);
                    }
                    else
                    {
                        filter += StatusFilter(ByStatuscomboBox.Text);
                    }
                }

                if (Createdbycomboxbox.SelectedItem == null && ReleasedBycombox.SelectedItem == null && CheckedBycomboxbox.SelectedItem == null && Prioritycombox.SelectedItem == null && ApprovedBycombobox.SelectedItem == null && LastSavedcombobox.SelectedItem == null && ByStatuscomboBox.SelectedItem == null)
                {
                }
                Advfiltertables(filter);
            }
        }

        private string StatusFilter(string status)
        {
            if (status == "Not Submitted")
            {
                return "CONVERT(IsSubmitted, 'System.String') LIKE '0' AND CONVERT(IsChecked, 'System.String') LIKE '0'";
            }
            else if (status == "For Checking")
            {
                return "CONVERT(IsSubmitted, 'System.String') LIKE '1' AND CONVERT(IsChecked, 'System.String') LIKE '0'";
            }
            else if (status == "For Approval")
            {
                return "CONVERT(IsSubmitted, 'System.String') LIKE '1' AND CONVERT(IsChecked, 'System.String') LIKE '1'" +
                    " AND CONVERT(IsApproved, 'System.String') LIKE '0'";
            }
            else if (status == "For Release")
            {
                return "CONVERT(IsSubmitted, 'System.String') LIKE '1' AND CONVERT(IsChecked, 'System.String') LIKE '1' " +
                    "AND CONVERT(IsApproved, 'System.String') LIKE '1' AND CONVERT(IsReleased, 'System.String') LIKE '0' ";
            }
            else if (status == "InActive")
            {
                return "CONVERT(IsActive, 'System.String') LIKE '0'";
            }

            return "";
        }

        #region Fill Combo Boxes

        private void FillCreatedBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillReleaseLastCreatedBy(true);
            Createdbycomboxbox.AutoCompleteCustomSource = MyCollection;
            Createdbycomboxbox.DataSource = MyCollection;
        }

        private void FillApprovedBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillReleaseApprovedBy();
            ApprovedBycombobox.AutoCompleteCustomSource = MyCollection;
            ApprovedBycombobox.DataSource = MyCollection;
        }

        private void FillReleaseBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillReleaseReleasedBy();
            ReleasedBycombox.AutoCompleteCustomSource = MyCollection;
            ReleasedBycombox.DataSource = MyCollection;
        }

        private void FillLastSavedBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillReleaseLastCreatedBy(false);
            LastSavedcombobox.AutoCompleteCustomSource = MyCollection;
            LastSavedcombobox.DataSource = MyCollection;
        }

        private void FillCheckedBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillReleaseCheckedBy();
            CheckedBycomboxbox.AutoCompleteCustomSource = MyCollection;
            CheckedBycomboxbox.DataSource = MyCollection;
        }

        #endregion Fill Combo Boxes

        #region advance filters events

        private void Clrfiltersbttn_Click(object sender, EventArgs e)
        {
            Performreload();
        }

        private void Lastsavedbycombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void LastSavedcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        #endregion advance filters events

        #endregion Advance Filters

        #region Invoice

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new shipping invoice?", "SPM Connect - Create New Invoice?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Enabled = false;
                string status = connectapi.CreateNewMatReallocation();
                if (status.Length > 1)
                {
                    ShowReleaseInfo(status);
                }
            }
        }

        private string GetselectedReleaseNo()
        {
            if (DataGridView.SelectedRows.Count == 1 || DataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = DataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = DataGridView.Rows[selectedrowindex];

                //MessageBox.Show(item);
                return Convert.ToString(slectedrow.Cells["RelNo"].Value);
            }
            else
            {
                return "";
            }
        }

        private void ShowReleaseInfo(string invoice)
        {
            MessageBox.Show(invoice);
        }

        #endregion Invoice

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridView.Columns[e.ColumnIndex].Name.Equals("IsSubmitted") ||
                DataGridView.Columns[e.ColumnIndex].Name.Equals("IsChecked") ||
                DataGridView.Columns[e.ColumnIndex].Name.Equals("IsApproved") ||
                DataGridView.Columns[e.ColumnIndex].Name.Equals("IsReleased") ||
                DataGridView.Columns[e.ColumnIndex].Name.Equals("IsActive"))
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "Yes";
                        break;

                    case "0":
                        e.Value = "No";
                        break;
                }
            }
        }

        private void ViewReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowReleaseInfo(GetselectedReleaseNo());
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!(DataGridView.Rows.Count > 0 && DataGridView.SelectedRows.Count == 1)) e.Cancel = true;
        }

        private void ViewCurrentJobReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewRelease viewRelease = new ViewRelease(wrkorder: Getjobnumber(), job: true, jobassyno: connectapi.GetJobAssyNo(Getjobnumber()), jobno: Getjobnumber());
            viewRelease.Show();
        }

        private string Getjobnumber()
        {
            if (DataGridView.SelectedRows.Count == 1 || DataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = DataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = DataGridView.Rows[selectedrowindex];

                //MessageBox.Show(ItemNo);
                return Convert.ToString(slectedrow.Cells["Job"].Value);
            }
            else
            {
                return "";
            }
        }

        private string GetAssynumber()
        {
            if (DataGridView.SelectedRows.Count == 1 || DataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = DataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = DataGridView.Rows[selectedrowindex];

                //MessageBox.Show(ItemNo);
                return Convert.ToString(slectedrow.Cells["SubAssy"].Value);
            }
            else
            {
                return "";
            }
        }

        private void GetBOMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Processbom(GetAssynumber());
        }

        private void Processbom(string itemvalue)
        {
            Engineering.TreeView treeView = new Engineering.TreeView(item: itemvalue);
            treeView.Show();
        }

        private void OpenModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SPMSQLCommands sc = new SPMSQLCommands())
            {
                sc.Checkforspmfile(GetAssynumber());
            }
        }
    }
}