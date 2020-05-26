using SPMConnectAPI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.Purchasing
{
    public partial class ShippingHome : Form
    {
        #region Shipping Home Load

        private readonly SPMConnectAPI.Shipping connectapi = new Shipping();
        private int _advcollapse;
        private string datarequest = "normal";
        private DataTable dt;
        private bool formloading;
        private DataTable invoiceitems = new DataTable();
        private log4net.ILog log;
        private DataTable temptable = new DataTable();

        public ShippingHome()
        {
            InitializeComponent();
            formloading = true;
        }

        private void Checkdeptsandrights()
        {
            versionlabel.Text = Getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connnect " + versionlabel.Text);
            if (connectapi.ConnectUser.ShipSupervisor)
            {
                attnbttn.Visible = true;
            }
            else if (connectapi.ConnectUser.ShippingManager)
            {
                attnbttn.Visible = true;
            }
        }

        private void Clearfilercombos()
        {
            Createdbycombobox.SelectedItem = null;
            Soldtocombobox.SelectedItem = null;
            Shiptocomboxbox.SelectedItem = null;
            lastsavedbycombo.SelectedItem = null;
            Salespersoncomboxbox.SelectedItem = null;
            Createdbycombobox.SelectedItem = null;
            custvendcombobox.SelectedItem = null;
            Createdbycombobox.Text = null;
            Soldtocombobox.Text = null;
            Shiptocomboxbox.Text = null;
            lastsavedbycombo.Text = null;
            Salespersoncomboxbox.Text = null;
            Createdbycombobox.Text = null;
            custvendcombobox.Text = null;
            CarrierscomboBox.Text = null;
        }

        private void Fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            fillsalespersonship();
            fillsoldtoship();
            fillshiptoship();
            fillcreatedbyship();
            filllastsavedbyship();
            fillcarriersship();
            Clearfilercombos();
            formloading = false;
            Cursor.Current = Cursors.Default;
        }

        private void FillInvoiceItems()
        {
            invoiceitems.Clear();
            invoiceitems = connectapi.ShowShippingInvoiceItems();
        }

        private void Performreload()
        {
            clearandhide();
            datarequest = "normal";
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dataGridView.Refresh();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Performreload();
        }

        private void Showallitems(string showdatafor)
        {
            try
            {
                dt.Clear();
                datarequest = showdatafor;
                dt = showdatafor == "normal" ? connectapi.ShowshippingHomeData() : showdatafor == "supervisor" ? connectapi.ShowshippingHomeDataforSupervisors(connectapi.ConnectUser.ConnectId.ToString()) : connectapi.ShowshippingHomeDataforManagers();
                if (dt.Rows.Count > 0)
                {
                    dataGridView.DataSource = dt;
                    DataView dv = dt.DefaultView;
                    dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[4].Visible = false;
                    dataGridView.Columns[6].Visible = false;
                    dataGridView.Columns[7].Visible = false;
                    dataGridView.Columns[8].Visible = false;
                    dataGridView.Columns[9].Visible = false;
                    dataGridView.Columns[10].Visible = false;
                    dataGridView.Columns[11].Visible = false;
                    dataGridView.Columns[12].Visible = false;
                    dataGridView.Columns[13].Visible = false;
                    dataGridView.Columns[14].Visible = false;
                    dataGridView.Columns[15].Visible = false;
                    dataGridView.Columns[16].Visible = false;
                    dataGridView.Columns[17].Visible = false;
                    dataGridView.Columns[18].Visible = false;
                    dataGridView.Columns[21].Visible = false;
                    dataGridView.Columns[22].Visible = false;
                    dataGridView.Columns[23].Visible = false;
                    dataGridView.Columns[24].Visible = false;
                    dataGridView.Columns[25].Visible = false;
                    dataGridView.Columns[0].Width = 80;
                    dataGridView.Columns[1].Width = 250;
                    dataGridView.Columns[5].Width = 120;
                    dataGridView.Columns[19].Width = 280;
                    dataGridView.Columns[20].Width = 280;
                    dataGridView.Columns[19].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[20].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    UpdateFont();
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            formloading = true;
            collapse();
            dt = new DataTable();
            Checkdeptsandrights();
            invoiceitemsdataGridView2.DataSource = temptable;
            FillInvoiceItems();
            Showallitems("normal");
            txtSearch.Focus();
            formloading = false;

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Shipping Home ");
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
        // string fullsearch = ("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'");
        private readonly string fullsearch = ("FullSearch LIKE '%{0}%'");

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

        public void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                formloading = true;

                if (Descrip_txtbox.Visible)
                {
                    clearandhide();
                }

                if (string.IsNullOrEmpty(Createdbycombobox.Text) && string.IsNullOrEmpty(lastsavedbycombo.Text) && string.IsNullOrEmpty(Salespersoncomboxbox.Text) && string.IsNullOrEmpty(Shiptocomboxbox.Text) && string.IsNullOrEmpty(Soldtocombobox.Text) && string.IsNullOrEmpty(custvendcombobox.Text) && string.IsNullOrEmpty(CarrierscomboBox.Text))
                {
                    Showallitems(datarequest);
                    FillInvoiceItems();
                }
                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");
                    mainsearch();
                }
                else
                {
                    SearchStringPosition();
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

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fifthfilter;
                    else
                        dv.RowFilter += " AND " + fifthfilter;
                    table4 = dv.ToTable();
                    dataGridView.DataSource = table4;
                    SearchStringPosition();
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
                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = thirdFilter;
                    else
                        dv.RowFilter += " AND " + thirdFilter;

                    table2 = dv.ToTable();
                    dataGridView.DataSource = table2;
                    SearchStringPosition();
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

                    if (string.IsNullOrEmpty(dv.RowFilter))
                        dv.RowFilter = fourthfilter;
                    else
                        dv.RowFilter += " AND " + fourthfilter;

                    table3 = dv.ToTable();
                    dataGridView.DataSource = table3;
                    SearchStringPosition();
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

        private void mainsearch()
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
            }

            formloading = false;
        }

        #endregion Search Parameters

        #region datagridview events

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
            {
                Showshippinginvoice(getselectedinvoicenumber());
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
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 230, 247);
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

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            showfilterdata();
        }

        private void showfilterdata()
        {
            if (!formloading)
            {
                if (dataGridView.Rows.Count > 0 && dataGridView.SelectedCells.Count == 1)
                {
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    string item = Convert.ToString(slectedrow.Cells[0].Value);
                    InvoiceItemsgrp.Text = "Showing items for InvoiceNo: " + item;
                    temptable.Clear();
                    DataView dv = new DataView(invoiceitems)
                    {
                        RowFilter = string.Format("InvoiceNo = {0}", item)
                    };
                    temptable = dv.ToTable();
                    invoiceitemsdataGridView2.DataSource = temptable;
                    invoiceitemsdataGridView2.Columns[0].Visible = false;
                    invoiceitemsdataGridView2.Columns[9].Visible = false;
                    invoiceitemsdataGridView2.Columns[1].Width = 65;
                    invoiceitemsdataGridView2.Columns[2].Width = 80;
                    invoiceitemsdataGridView2.Columns[4].Width = 70;
                    invoiceitemsdataGridView2.Columns[5].Width = 100;
                    invoiceitemsdataGridView2.Columns[6].Width = 50;
                    invoiceitemsdataGridView2.Columns[7].Width = 80;
                    invoiceitemsdataGridView2.Columns[8].Width = 80;
                    invoiceitemsdataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    invoiceitemsdataGridView2.Columns[7].DefaultCellStyle.Format = "0.00##";
                    invoiceitemsdataGridView2.Columns[8].DefaultCellStyle.Format = "0.00##";
                    invoiceitemsdataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    invoiceitemsdataGridView2.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
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

        public void searchtext(string searchkey)
        {
            sw = searchkey;
        }

        private void dataGridView_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
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
            log.Info("Closed Shipping Home ");
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
                    advsearchbttnclick();
                }
                return true;
            }

            if (keyData == (Keys.Shift | Keys.Oemcomma))
            {
                if (!splitContainer1.Panel2Collapsed)
                {
                    advsearchbttnclick();
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region Advance Filters

        private void advfiltertables(string filter)
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

        private void advsearchbttn_Click(object sender, EventArgs e)
        {
            advsearchbttnclick();
        }

        private void advsearchbttnclick()
        {
            if (_advcollapse == 0)
            {
                Fillinfo();
                _advcollapse = 1;
            }
            collapse();
        }

        private void collapse()
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
                if (Createdbycombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND CreatedBy = '{0}'", Createdbycombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("CreatedBy = '{0}'", Createdbycombobox.Text);
                    }
                }
                if (Soldtocombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND SoldToName = '{0}'", Soldtocombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("SoldToName = '{0}'", Soldtocombobox.Text);
                    }
                }
                if (lastsavedbycombo.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND LastSavedBy = '{0}'", lastsavedbycombo.Text);
                    }
                    else
                    {
                        filter += string.Format("LastSavedBy = '{0}'", lastsavedbycombo.Text);
                    }
                }
                if (Salespersoncomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND SalesPerson = '{0}'", Salespersoncomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("SalesPerson = '{0}'", Salespersoncomboxbox.Text);
                    }
                }
                if (Shiptocomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ShipToName LIKE '%{0}%'", Shiptocomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("ShipToName LIKE '%{0}%'", Shiptocomboxbox.Text);
                    }
                }
                if (custvendcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Vendor_Cust = {0}", custvendcombobox.Text.Substring(0, 1));
                    }
                    else
                    {
                        filter += string.Format("Vendor_Cust = {0}", custvendcombobox.Text.Substring(0, 1));
                    }
                }
                if (CarrierscomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Carrier LIKE '%{0}%'", CarrierscomboBox.Text);
                    }
                    else
                    {
                        filter += string.Format("Carrier LIKE '%{0}%'", CarrierscomboBox.Text);
                    }
                }

                if (Createdbycombobox.SelectedItem == null && lastsavedbycombo.SelectedItem == null && Salespersoncomboxbox.SelectedItem == null && Shiptocomboxbox.SelectedItem == null && Soldtocombobox.SelectedItem == null && custvendcombobox.SelectedItem == null && CarrierscomboBox.SelectedItem == null)
                {
                }
                advfiltertables(filter);
            }
        }

        #region fillcomboboxes

        private void fillcarriersship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillCarrierShip();
            CarrierscomboBox.AutoCompleteCustomSource = MyCollection;
            CarrierscomboBox.DataSource = MyCollection;
        }

        private void fillcreatedbyship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillCreatedbyShip();
            Createdbycombobox.AutoCompleteCustomSource = MyCollection;
            Createdbycombobox.DataSource = MyCollection;
        }

        private void filllastsavedbyship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillLastSavedByShip();
            lastsavedbycombo.AutoCompleteCustomSource = MyCollection;
            lastsavedbycombo.DataSource = MyCollection;
        }

        private void fillsalespersonship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillSalesPersonShip();
            Salespersoncomboxbox.AutoCompleteCustomSource = MyCollection;
            Salespersoncomboxbox.DataSource = MyCollection;
        }

        private void fillshiptoship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillShipToShip();
            Shiptocomboxbox.AutoCompleteCustomSource = MyCollection;
            Shiptocomboxbox.DataSource = MyCollection;
        }

        private void fillsoldtoship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillSoldToShip();
            Soldtocombobox.AutoCompleteCustomSource = MyCollection;
            Soldtocombobox.DataSource = MyCollection;
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

        private void clrfiltersbttn_Click(object sender, EventArgs e)
        {
            Performreload();
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

        private void familycomboxbox_KeyDown(object sender, KeyEventArgs e)
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

        private void MaterialcomboBox_KeyDown(object sender, KeyEventArgs e)
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

        #endregion advance filters events

        #endregion Advance Filters

        #region Invoice

        private void addnewbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new shipping invoice?", "SPM Connect - Create New Invoice?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string ValueIWantFromProptForm = "";
                this.Enabled = false;
                InvoiceFor invoiceFor = new InvoiceFor();
                if (invoiceFor.ShowDialog() == DialogResult.OK)
                {
                    ValueIWantFromProptForm = invoiceFor.ValueIWant;
                }
                if (ValueIWantFromProptForm.Length > 0)
                {
                    string vendorcust = "0";
                    if (ValueIWantFromProptForm == "Customer")
                    {
                        vendorcust = "1";
                    }
                    string status = connectapi.Createnewshippinginvoice(vendorcust);
                    if (status.Length > 1)
                    {
                        Showshippinginvoice(status);
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Inovice for not selected. System cannot create new shipping invoice.", "SPM Connect - Create New Invoice?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ContextMenuStripShipping_Opening(object sender, CancelEventArgs e)
        {
            if (!(dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count == 1)) e.Cancel = true;
        }

        private void copyinvoicestrip_Click(object sender, EventArgs e)
        {
            Copyshippinginvoice();
        }

        private void Copyshippinginvoice()
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to copy this invoice to a new shipping invoice?", "SPM Connect - Copy Invoice?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Enabled = false;
                string status = connectapi.CopyShippingInvoice(getselectedinvoicenumber());

                if (status.Length > 1)
                {
                    Showshippinginvoice(status);
                }
            }
        }

        private string getselectedinvoicenumber()
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

        private void invoiceinfostripmenu_Click(object sender, EventArgs e)
        {
            Showshippinginvoice(getselectedinvoicenumber());
        }

        private void Showshippinginvoice(string invoice)
        {
            string invoiceopen = connectapi.InvoiceOpen(invoice, CheckInModules.ShipInv);
            if (invoiceopen.Length > 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Inovice is opened for edit by " + invoiceopen + ". ", "SPM Connect - Open Invoice Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (connectapi.CheckinInvoice(invoice, CheckInModules.ShipInv))
                {
                    using (InvoiceDetails invoiceDetails = new InvoiceDetails(invoice))
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
        }

        #endregion Invoice

        private void attnbttn_Click(object sender, EventArgs e)
        {
            if (connectapi.ConnectUser.ShipSupervisor)
            {
                Showallitems("supervisor");
            }
            else if (connectapi.ConnectUser.ShippingManager)
            {
                Showallitems("manager");
            }
        }
    }
}