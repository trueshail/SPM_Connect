using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SPMConnectAPI;

namespace SearchDataSPM
{

    public partial class ShippingHome : Form
    {
        #region Shipping Home Load

        String connection;
        SqlConnection cn;
        DataTable dt;
        DataTable invoiceitems = new DataTable();
        DataTable temptable = new DataTable();
        bool formloading = false;
        string userfullname = "";
        int _advcollapse = 0;
        SPMConnectAPI.Shipping connectapi = new Shipping();

        public ShippingHome()
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

            connectapi.SPM_Connect(connection);

        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            formloading = true;
            collapse();
            dt = new DataTable();
            checkdeptsandrights();
            userfullname = connectapi.getuserfullname();

            invoiceitemsdataGridView2.DataSource = temptable;
            FillInvoiceItems();
            Showallitems();
            txtSearch.Focus();
            formloading = false;

        }

        private void checkdeptsandrights()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            versionlabel.Text = connectapi.getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connnect " + versionlabel.Text);
        }

        private void fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            fillsalespersonship();
            fillsoldtoship();
            fillshiptoship();
            fillcreatedbyship();
            filllastsavedbyship();
            fillcarriersship();
            clearfilercombos();
            formloading = false;
            Cursor.Current = Cursors.Default;
        }

        void clearfilercombos()
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

        private void Showallitems()
        {
            dt.Clear();
            dt = connectapi.ShowshippingHomeData();
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
            dataGridView.Columns[20].Visible = false;
            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[1].Width = 250;
            dataGridView.Columns[5].Width = 120;
            dataGridView.Columns[18].Width = 280;
            dataGridView.Columns[19].Width = 280;
            dataGridView.Columns[18].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[19].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            UpdateFont();

        }

        private void FillInvoiceItems()
        {
            invoiceitems.Clear();
            invoiceitems = connectapi.ShowShippingInvoiceItems();
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            performreload();
        }

        void performreload()
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

        #endregion

        #region Public Table & variables

        // variables required outside the functions to perfrom
        // string fullsearch = ("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'");
        string fullsearch = ("FullSearch LIKE '%{0}%'");
        //string ItemNo;
        //string str;
        DataTable table0 = new DataTable();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        DataTable table4 = new DataTable();
        DataTable dataTable = new DataTable();

        #endregion

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

                if (Createdbycombobox.Text == "" && lastsavedbycombo.Text == "" && Salespersoncomboxbox.Text == "" && Shiptocomboxbox.Text == "" && Soldtocombobox.Text == "" && custvendcombobox.Text == "" && CarrierscomboBox.Text == "")
                {
                    Showallitems();
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

        #endregion

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

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            showfilterdata();

        }

        void showfilterdata()
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
                    DataView dv = new DataView(invoiceitems);
                    dv.RowFilter = string.Format("InvoiceNo = {0}", item);
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

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 1)
            {
                showshippinginvoice(getselectedinvoicenumber(), "1");
            }
        }

        #endregion

        #region Highlight Search Results 

        bool IsSelected = false;

        public void SearchStringPosition(string Searchstring)
        {
            IsSelected = true;

        }
        string sw;

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

        #region Closing SPMConnect

        private void SPM_Connect_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void SPM_Connect_FormClosing(object sender, FormClosingEventArgs e)
        {

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


        #endregion        

        #region Advance Filters

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

        void collapse()
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
                if (Createdbycombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND CreatedBy = '{0}'", Createdbycombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("CreatedBy = '{0}'", Createdbycombobox.Text.ToString());
                    }

                }
                if (Soldtocombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND SoldToName = '{0}'", Soldtocombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("SoldToName = '{0}'", Soldtocombobox.Text.ToString());
                    }

                }
                if (lastsavedbycombo.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND LastSavedBy = '{0}'", lastsavedbycombo.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("LastSavedBy = '{0}'", lastsavedbycombo.Text.ToString());
                    }

                }
                if (Salespersoncomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND SalesPerson = '{0}'", Salespersoncomboxbox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("SalesPerson = '{0}'", Salespersoncomboxbox.Text.ToString());
                    }

                }
                if (Shiptocomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ShipToName LIKE '%{0}%'", Shiptocomboxbox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("ShipToName LIKE '%{0}%'", Shiptocomboxbox.Text.ToString());
                    }

                }
                if (custvendcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Vendor_Cust = {0}", custvendcombobox.Text.ToString().Substring(0, 1));
                    }
                    else
                    {
                        filter += string.Format("Vendor_Cust = {0}", custvendcombobox.Text.ToString().Substring(0, 1));
                    }

                }
                if (CarrierscomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Carrier LIKE '%{0}%'", CarrierscomboBox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("Carrier LIKE '%{0}%'", CarrierscomboBox.Text.ToString());
                    }

                }

                if (Createdbycombobox.SelectedItem == null && lastsavedbycombo.SelectedItem == null && Salespersoncomboxbox.SelectedItem == null && Shiptocomboxbox.SelectedItem == null && Soldtocombobox.SelectedItem == null && custvendcombobox.SelectedItem == null && CarrierscomboBox.SelectedItem == null)
                {

                }
                advfiltertables(filter);
            }


        }

        void advfiltertables(string filter)
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

        private void fillsoldtoship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillSoldToShip();
            Soldtocombobox.AutoCompleteCustomSource = MyCollection;
            Soldtocombobox.DataSource = MyCollection;

        }

        private void fillshiptoship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillShipToShip();
            Shiptocomboxbox.AutoCompleteCustomSource = MyCollection;
            Shiptocomboxbox.DataSource = MyCollection;

        }

        private void fillsalespersonship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillSalesPersonShip();
            Salespersoncomboxbox.AutoCompleteCustomSource = MyCollection;
            Salespersoncomboxbox.DataSource = MyCollection;
        }

        private void fillcarriersship()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillCarrierShip();
            CarrierscomboBox.AutoCompleteCustomSource = MyCollection;
            CarrierscomboBox.DataSource = MyCollection;
        }

        #endregion

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

        #endregion

        #endregion

        #region Invoice

        private void addnewbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new shipping invoice?", "SPM Connect - Create New Invoice?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string ValueIWantFromProptForm = "";
                this.Enabled = false;
                InvoiceFor invoiceFor = new InvoiceFor();
                if (invoiceFor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                        showshippinginvoice(status, vendorcust);
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Inovice for not selected. System cannot create new shipping invoice.", "SPM Connect - Create New Invoice?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
        }

        void showshippinginvoice(string invoice, string vendorcust)
        {
            using (InvoiceDetails invoiceDetails = new InvoiceDetails())
            {
                invoiceDetails.invoicenumber(invoice);
                invoiceDetails.setcustvendor(vendorcust);
                invoiceDetails.ShowDialog();
                this.Enabled = true;
                performreload();
                this.Show();
                this.Activate();
                this.Focus();
            }
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

        private void copyshippinginvoice()
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to copy this invoice to a new shipping invoice?", "SPM Connect - Copy Invoice?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Enabled = false;
                string status = connectapi.CopyShippingInvoice(getselectedinvoicenumber());
                
                if (status.Length > 1)
                {
                    showshippinginvoice(status, "1");
                }
               

            }

        }

        private void invoiceinfostripmenu_Click(object sender, EventArgs e)
        {
            showshippinginvoice(getselectedinvoicenumber(), "1");
        }

        private void copyinvoicestrip_Click(object sender, EventArgs e)
        {
            copyshippinginvoice();
        }

        private void ContextMenuStripShipping_Opening(object sender, CancelEventArgs e)
        {
            if (!(dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count == 1)) e.Cancel = true;
        }

        #endregion
    }

}