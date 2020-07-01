using SearchDataSPM.WorkOrder;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.ECR
{
    public partial class ECRHome : Form
    {
        #region Shipping Home Load

        private readonly SPMConnectAPI.ECR connectapi = new SPMConnectAPI.ECR();
        private int _advcollapse;
        private DataTable dt;
        private bool formloading;
        private log4net.ILog log;

        public ECRHome()
        {
            InitializeComponent();
            formloading = true;
        }

        private void Checkdeptsandrights()
        {
            versionlabel.Text = Getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connect " + versionlabel.Text);
            if (connectapi.ConnectUser.ECR)
            {
                addnewbttn.Visible = true;
            }
            attentionbttn.Visible = connectapi.ConnectUser.ECRApproval || connectapi.ConnectUser.ECRApproval2 || connectapi.ConnectUser.ECRHandler;
        }

        private void Clearfilercombos()
        {
            jobnumbercombobox.SelectedItem = null;
            ecrstatuscombobox.SelectedItem = null;
            requestedbycomboxbox.SelectedItem = null;
            approvedbycombo.SelectedItem = null;
            deptrequestedcomboxbox.SelectedItem = null;
            jobnumbercombobox.SelectedItem = null;
            completedbycombobox.SelectedItem = null;
            jobnumbercombobox.Text = null;
            ecrstatuscombobox.Text = null;
            requestedbycomboxbox.Text = null;
            approvedbycombo.Text = null;
            deptrequestedcomboxbox.Text = null;
            jobnumbercombobox.Text = null;
            completedbycombobox.Text = null;
            supervicsorcomboBox.Text = null;
        }

        private void Fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            Filldeptrequested();
            Fillecrstatus();
            Fillrequestedby();
            Filljobnumber();
            Fillapprovedby();
            Fillsupervisors();
            Fillcompletedby();
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
            dataGridView.Refresh();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Performreload();
        }

        private void Showallitems(bool showall)
        {
            if (showall)
            {
                dt.Clear();
                dt = connectapi.ShowAllECRInvoices();
            }

            dataGridView.DataSource = dt;
            _ = dt.DefaultView;
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);

            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
            dataGridView.Columns[13].Visible = false;
            dataGridView.Columns[14].Visible = false;
            dataGridView.Columns[15].Visible = false;
            dataGridView.Columns[16].Visible = false;
            dataGridView.Columns[17].Visible = false;
            dataGridView.Columns[18].Visible = false;
            dataGridView.Columns[19].Visible = false;
            dataGridView.Columns[20].Visible = false;
            dataGridView.Columns[21].Visible = false;
            dataGridView.Columns[22].Visible = false;
            dataGridView.Columns[23].Visible = false;
            dataGridView.Columns[24].Visible = false;
            dataGridView.Columns[25].Visible = false;
            dataGridView.Columns[26].Visible = false;
            dataGridView.Columns[27].Visible = false;
            dataGridView.Columns[28].Visible = false;
            dataGridView.Columns[29].Visible = false;
            dataGridView.Columns[30].Visible = false;
            dataGridView.Columns[31].Visible = false;
            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[3].Width = 80;
            dataGridView.Columns[11].Width = 150;
            dataGridView.Columns[12].Width = 150;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            UpdateFont();
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            formloading = true;
            Collapse();
            dt = new DataTable();
            Checkdeptsandrights();
            Showallitems(true);
            txtSearch.Focus();
            formloading = false;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened ECR Home ");
            // Resume the layout logic
            this.ResumeLayout();
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
        private readonly string fullsearch = "FullSearch LIKE '%{0}%'";

        private DataTable dataTable = new DataTable();

        //string ItemNo;
        //string str;
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

                if (string.IsNullOrEmpty(jobnumbercombobox.Text) && string.IsNullOrEmpty(approvedbycombo.Text) && string.IsNullOrEmpty(deptrequestedcomboxbox.Text) && string.IsNullOrEmpty(requestedbycomboxbox.Text) && string.IsNullOrEmpty(ecrstatuscombobox.Text) && string.IsNullOrEmpty(completedbycombobox.Text) && string.IsNullOrEmpty(supervicsorcomboBox.Text))
                {
                    Showallitems(true);
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
                    dataGridView.DataSource = table1;
                    SearchStringPosition();
                    Searchtext(Descrip_txtbox.Text);
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
                    dataGridView.DataSource = table4;
                    SearchStringPosition();
                    Searchtext(filter4.Text);
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
                    dataGridView.DataSource = table2;
                    SearchStringPosition();
                    Searchtext(filteroem_txtbox.Text);
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
                    dataGridView.DataSource = table3;
                    SearchStringPosition();
                    Searchtext(filteroemitem_txtbox.Text);
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
            DataView dv = (dataGridView.DataSource as DataTable)?.DefaultView;
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
                SearchStringPosition();
                Searchtext(search1);
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
            }

            formloading = false;
        }

        #endregion Search Parameters

        #region datagridview events

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
            {
                Showecrinvoice(Getselectedinvoicenumber(), false, connectapi.ConnectUser.Emp_Id.ToString());
            }
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            }
        }

        private void DataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                dataGridView.Enabled = false;
                dataGridView.GetNextControl(dataGridView, true).Focus();
                dataGridView.Enabled = true;
                e.Handled = true;
            }
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
            log.Info("Closed ECR Home ");
            this.Dispose();
        }

        private void SPM_Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        #endregion Closing SPMConnect

        #region shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Home)
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

        #region Advance Filtersf

        private void Advfiltertables(string filter)
        {
            if (!Descrip_txtbox.Visible)
            {
                dataGridView.DataSource = dt;
                dataTable.Clear();
                ((dataGridView.DataSource as DataTable)?.DefaultView.ToTable()).DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (Descrip_txtbox.Visible)
            {
                dataGridView.DataSource = table0;
                dataTable.Clear();
                dataGridView.Refresh();
                table0.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filteroem_txtbox.Visible)
            {
                dataGridView.DataSource = table1;
                dataTable.Clear();
                table1.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filteroemitem_txtbox.Visible)
            {
                dataGridView.DataSource = table2;
                dataTable.Clear();
                table2.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            if (filter4.Visible)
            {
                dataGridView.DataSource = table3;
                dataTable.Clear();
                table3.DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
            }
            else
            {
                dataGridView.DataSource = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataTable.Clear();
                ((dataGridView.DataSource as DataTable)?.DefaultView.ToTable()).DefaultView.RowFilter = filter;
                dataTable = (dataGridView.DataSource as DataTable)?.DefaultView.ToTable();
                dataGridView.DataSource = dataTable;
                recordlabel.Text = "Found " + dataGridView.Rows.Count.ToString() + " Matching Items.";
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
                if (jobnumbercombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND JobNo = '{0}'", jobnumbercombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("JobNo = '{0}'", jobnumbercombobox.Text);
                    }
                }
                if (ecrstatuscombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Status = '{0}'", ecrstatuscombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("Status = '{0}'", ecrstatuscombobox.Text);
                    }
                }
                if (approvedbycombo.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ApprovedBy = '{0}'", approvedbycombo.Text);
                    }
                    else
                    {
                        filter += string.Format("ApprovedBy = '{0}'", approvedbycombo.Text);
                    }
                }
                if (deptrequestedcomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Department = '{0}'", deptrequestedcomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("Department = '{0}'", deptrequestedcomboxbox.Text);
                    }
                }
                if (requestedbycomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND RequestedBy LIKE '%{0}%'", requestedbycomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("RequestedBy LIKE '%{0}%'", requestedbycomboxbox.Text);
                    }
                }
                if (completedbycombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND CompletedBy  LIKE '%{0}%'", completedbycombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("CompletedBy  LIKE '%{0}%'", completedbycombobox.Text);
                    }
                }
                if (supervicsorcomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND SupApprovalBy LIKE '%{0}%'", supervicsorcomboBox.Text);
                    }
                    else
                    {
                        filter += string.Format("SupApprovalBy LIKE '%{0}%'", supervicsorcomboBox.Text);
                    }
                }

                if (jobnumbercombobox.SelectedItem == null && approvedbycombo.SelectedItem == null && deptrequestedcomboxbox.SelectedItem == null && requestedbycomboxbox.SelectedItem == null && ecrstatuscombobox.SelectedItem == null && completedbycombobox.SelectedItem == null && supervicsorcomboBox.SelectedItem == null)
                {
                }
                Advfiltertables(filter);
            }
        }

        #region fillcomboboxes

        private void Fillapprovedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRApprovedBy();
            approvedbycombo.AutoCompleteCustomSource = MyCollection;
            approvedbycombo.DataSource = MyCollection;
        }

        private void Fillcompletedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRCompletedBy();
            completedbycombobox.AutoCompleteCustomSource = MyCollection;
            completedbycombobox.DataSource = MyCollection;
        }

        private void Filldeptrequested()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRDeptRequested();
            deptrequestedcomboxbox.AutoCompleteCustomSource = MyCollection;
            deptrequestedcomboxbox.DataSource = MyCollection;
        }

        private void Fillecrstatus()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRStatus();
            ecrstatuscombobox.AutoCompleteCustomSource = MyCollection;
            ecrstatuscombobox.DataSource = MyCollection;
        }

        private void Filljobnumber()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRJobNumber();
            jobnumbercombobox.AutoCompleteCustomSource = MyCollection;
            jobnumbercombobox.DataSource = MyCollection;
        }

        private void Fillrequestedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRRequestedBy();
            requestedbycomboxbox.AutoCompleteCustomSource = MyCollection;
            requestedbycomboxbox.DataSource = MyCollection;
        }

        private void Fillsupervisors()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRSupervisors();
            supervicsorcomboBox.AutoCompleteCustomSource = MyCollection;
            supervicsorcomboBox.DataSource = MyCollection;
        }

        #endregion fillcomboboxes

        #region advance filters events

        private void ActiveCadblockcombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Clrfiltersbttn_Click(object sender, EventArgs e)
        {
            Performreload();
        }

        private void Designedbycombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Familycomboxbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
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

        private void Manufactureritemcomboxbox_KeyDown(object sender, KeyEventArgs e)
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

        private void Oemitemcombobox_KeyDown(object sender, KeyEventArgs e)
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

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new engineering change request?", "SPM Connect - Create New ECR?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (connectapi.ConnectUser.Emp_Id != 0)
                {
                    string status = connectapi.CreatenewECR(connectapi.ConnectUser.Emp_Id.ToString());
                    if (status.Length > 1)
                    {
                        Showecrinvoice(status, true, connectapi.ConnectUser.Emp_Id.ToString());
                    }
                }
                else
                {
                    // scan the employee barcode and grab the user name
                    string scanEmployeeId = "";
                    ScanEmpId invoiceFor = new ScanEmpId();
                    if (invoiceFor.ShowDialog() == DialogResult.OK)
                    {
                        scanEmployeeId = invoiceFor.ValueIWant;
                    }
                    if (scanEmployeeId.Length > 0)
                    {
                        if (connectapi.EmployeeExits(scanEmployeeId))
                        {
                            string status = connectapi.CreatenewECR(scanEmployeeId);
                            if (status.Length > 1)
                            {
                                Showecrinvoice(status, true, scanEmployeeId);
                            }
                        }
                        else
                        {
                            MetroFramework.MetroMessageBox.Show(this, "Employee not found. Please contact the admin", "SPM Connect - Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Please try again. Employee not found.", "SPM Connect - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ContextMenuStripShipping_Opening(object sender, CancelEventArgs e)
        {
            if (!(dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count == 1)) e.Cancel = true;
        }

        private string Getselectedinvoicenumber()
        {
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

        private void Invoiceinfostripmenu_Click(object sender, EventArgs e)
        {
            Showecrinvoice(Getselectedinvoicenumber(), false, connectapi.ConnectUser.Emp_Id.ToString());
        }

        private void Showecrinvoice(string invoice, bool newcreated, string empid)
        {
            string invoiceopen = connectapi.InvoiceOpen(invoice, CheckInModules.ECR);
            if (invoiceopen.Length > 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Inovice is opened for edit by " + invoiceopen + ". ", "SPM Connect - Open Invoice Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (connectapi.ConnectUser.Emp_Id != 0)
                {
                    if (connectapi.CheckinInvoice(invoice, CheckInModules.ECR))
                    {
                        using (ECRDetails invoiceDetails = new ECRDetails(connectapi.ConnectUser.Name, invoice))
                        {
                            invoiceDetails.ShowDialog();
                            this.Enabled = true;
                            Performreload();
                            this.Show();
                            this.Activate();
                            this.Focus();
                        }
                    }
                }
                else
                {
                    // scan the employee barcode and grab the user name
                    string scanEmployeeId = "";
                    if (!newcreated)
                    {
                        ScanEmpId invoiceFor = new ScanEmpId();
                        if (invoiceFor.ShowDialog() == DialogResult.OK)
                        {
                            scanEmployeeId = invoiceFor.ValueIWant;
                        }
                    }
                    else
                    {
                        scanEmployeeId = empid;
                    }

                    if (scanEmployeeId.Length > 0)
                    {
                        if (connectapi.CheckinInvoice(invoice, CheckInModules.ECR))
                        {
                            using (ECRDetails invoiceDetails = new ECRDetails(connectapi.GetNameByEmpId(scanEmployeeId), invoice))
                            {
                                invoiceDetails.ShowDialog();
                                this.Enabled = true;
                                Performreload();
                                this.Show();
                                this.Activate();
                                this.Focus();
                            }
                        }
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Please try again. Employee not found.", "SPM Connect - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion Invoice

        private void Approvedbycombo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                approvedbycombo.Focus();
            }
        }

        private void Attentionbttn_Click(object sender, EventArgs e)
        {
            int empidtomach = connectapi.ConnectUser.ConnectId;
            if (connectapi.ConnectUser.ECRApproval)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT *,CONCAT([ECRNo], ' ',[JobNo],' ',[JobName],' ',[SANo],' ',[SAName],' ',RequestedBy) AS FullSearch FROM [SPM_Database].[dbo].[ECR] WHERE Submitted = '1' AND SupApproval = '0' AND Approved = '0' AND Completed = '0' AND SupervisorId = '" + empidtomach + "' ORDER BY ECRNo DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (connectapi.ConnectUser.ECRApproval2)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT *,CONCAT([ECRNo], ' ',[JobNo],' ',[JobName],' ',[SANo],' ',[SAName],' ',RequestedBy) AS FullSearch FROM [SPM_Database].[dbo].[ECR] WHERE Submitted = '1' AND SupApproval = '1' AND Approved = '0' AND Completed = '0' AND SubmitToId = '" + empidtomach + "' ORDER BY ECRNo DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            else if (connectapi.ConnectUser.ECRHandler)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT *,CONCAT([ECRNo], ' ',[JobNo],' ',[JobName],' ',[SANo],' ',[SAName],' ',RequestedBy) AS FullSearch FROM [SPM_Database].[dbo].[ECR] WHERE Submitted = '1' AND SupApproval = '1' AND Approved = '1' AND Completed = '0' AND AssignedTo = '" + empidtomach + "' ORDER BY ECRNo DESC", connectapi.cn))
                {
                    try
                    {
                        if (connectapi.cn.State == ConnectionState.Closed)
                            connectapi.cn.Open();

                        dt.Clear();
                        sda.Fill(dt);
                    }
                    catch (Exception)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Data cannot be retrieved from database server. Please contact the admin.", "SPM Connect - SHow Waiting For Approval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectapi.cn.Close();
                    }
                }
            }
            Showallitems(false);
        }

        private void Completedbycombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                completedbycombobox.Focus();
            }
        }

        private void Deptrequestedcomboxbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                deptrequestedcomboxbox.Focus();
            }
        }

        private void Ecrstatuscombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ecrstatuscombobox.Focus();
            }
        }

        private void Jobnumbercombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                jobnumbercombobox.Focus();
            }
        }

        private void Requestedbycomboxbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                requestedbycomboxbox.Focus();
            }
        }

        private void SupervicsorcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                supervicsorcomboBox.Focus();
            }
        }
    }
}