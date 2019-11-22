using SPMConnectAPI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class SPM_ConnectWM : Form
    {
        #region SPM Connect Load

        private WorkOrder connectapi = new WorkOrder();
        private DataTable dt;
        private log4net.ILog log;
        private ErrorHandler errorHandler = new ErrorHandler();
        private string jobnumber;

        public SPM_ConnectWM(string jobno = "")
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
            //connectapi.SPM_Connect();
            dt = new DataTable();
            this.jobnumber = jobno;
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            Showallitems();
            txtSearch.Text = jobnumber;
            if (txtSearch.Text.Trim().Length > 0)
                SendKeys.Send("~");
            Checkdeptsandrights();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Work Order Management by " + System.Environment.UserName);
        }

        private void Checkdeptsandrights()
        {
            versionlabel.Text = connectapi.Getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connnect " + versionlabel.Text);
            if (connectapi.CheckRights("WORelease"))
            {
                contextMenuStrip1.Items[1].Enabled = true;
                contextMenuStrip1.Items[1].Visible = true;
            }
        }

        private void Showallitems()
        {
            try
            {
                dt.Clear();
                dt = connectapi.ShowAllWorkOrders();
                dataGridView.DataSource = dt;
                dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
                dataGridView.Columns[0].Width = 60;
                dataGridView.Columns[1].Width = 60;
                dataGridView.Columns[2].Width = 60;
                dataGridView.Columns[3].Width = 60;
                dataGridView.Columns[4].Width = 180;
                dataGridView.Columns[5].Width = 60;
                dataGridView.Columns[6].Visible = false;
                UpdateFont();
            }
            catch (Exception)
            {
                MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER WM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Clearandhide();
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

        #endregion SPM Connect Load

        #region Public Table & variables

        // variables required outside the functions to perfrom
        private string fullsearch = ("FullSearch LIKE '%{0}%'");

        public static string ItemNo;
        public static string description;
        public static string Manufacturer;
        public static string oem;
        public static string family;

        private DataTable table0 = new DataTable();
        private DataTable table1 = new DataTable();
        private DataTable table2 = new DataTable();
        private DataTable table3 = new DataTable();

        #endregion Public Table & variables

        #region Search Parameters

        public void TxtSearch_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Return)

            {
                Performjobsearch(txtSearch.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Performjobsearch(string job)
        {
            if (Descrip_txtbox.Visible == true)
            {
                Clearandhide();
            }
            Showallitems();
            Mainsearch(job);
            if (txtSearch.Text.Length > 0)
            {
                Descrip_txtbox.Show();
                SendKeys.Send("{TAB}");
            }
        }

        private void Clearandhide()
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

        private void Mainsearch(string jobnumber)
        {
            DataView dv = dt.DefaultView;

            try
            {
                jobnumber = jobnumber.Replace("'", "''");
                jobnumber = jobnumber.Replace("[", "[[]");
                dv.RowFilter = string.Format(fullsearch, jobnumber);
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

        private void Filteroem_txtbox_KeyDown(object sender, KeyEventArgs e)
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

        private void Filteroemitem_txtbox_KeyDown(object sender, KeyEventArgs e)
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

        private void Filter4_KeyDown(object sender, KeyEventArgs e)
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

        #endregion Search Parameters

        #region Highlight Search Results

        private bool IsSelected = false;

        private void SearchStringPosition(string Searchstring)
        {
            IsSelected = true;
        }

        private string sw;

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

        #region AdminControlLabel

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        #endregion AdminControlLabel

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

        #endregion datagridview events

        private void getBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string item;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[2].Value);
            }
            else
            {
                item = "";
            }
            Processbom(item);
        }

        private void ProrcessreportWorkOrder(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(itemvalue);
            form1.getreport(Reportname);
            form1.Show();
        }

        private string Getselectedworkorder()
        {
            string wo = "";
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                wo = Convert.ToString(slectedrow.Cells[1].Value);
            }
            return wo;
        }

        private void SPM_ConnectWM_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Work Order Management by " + System.Environment.UserName);
            this.Dispose();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!(dataGridView.SelectedRows.Count == 1))
            {
                e.Cancel = true;
            }
        }

        private void cribbttn_Click(object sender, EventArgs e)
        {
            if (connectapi.EmployeeExitsWithCribRights(connectapi.Getempid()))
            {
                InvInOut invInOut = new InvInOut();
                invInOut.Show();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Your request can't be completed based on your security settings.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void scanwobttn_Click(object sender, EventArgs e)
        {
            if (connectapi.CheckRights("[WOScan]"))
            {
                ScanWO scanWO = new ScanWO();
                scanWO.Show();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Your request can't be completed based on your security settings.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Get BOM

        // public static string jobtree;

        private void Processbom(string itemvalue)
        {
            TreeView treeView = new TreeView(item: itemvalue);
            treeView.Show();
        }

        #endregion Get BOM

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Home))
            {
                Reload.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.B))
            {
                string item;
                if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
                {
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    item = Convert.ToString(slectedrow.Cells[2].Value);
                }
                else
                {
                    item = "";
                }
                Processbom(item);

                return true;
            }

            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();
                return true;
            }

            if (keyData == (Keys.Control | Keys.D))
            {
                ProrcessreportWorkOrder(Getselectedworkorder(), "WorkOrder");
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

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log.Error(sender, t.Exception); errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(sender, (Exception)e.ExceptionObject); errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, this);
        }

        private void AddNewRelease()
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new release?", "SPM Connect - Create New Release?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (IsAssembly(GetSelectedAssyNo()))
                {
                    this.Enabled = false;

                    string rlogno = connectapi.EnterWOToReleaseLog(Getselectedworkorder(), GetSelectedJobNo(), GetSelectedAssyNo());
                    if (rlogno.Length > 1)
                    {
                        ShowReleaseLogDetails(rlogno);
                    }

                    this.Enabled = true;
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Item family must be a \"AS\" or \"AG\" or \"ASEL\" or \"ASPN\". In order to release into the system.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void ShowReleaseLogDetails(string invoice)
        {
            string invoiceopen = connectapi.InvoiceOpen(invoice);
            if (invoiceopen.Length > 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "Release Document is opened for edit by " + invoiceopen + ". ", "SPM Connect - Open Release Document Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (connectapi.CheckinInvoice(invoice))
                {
                    AddRelease addrelease = new AddRelease(releaseLogNo: invoice);
                    addrelease.Show();
                }
            }
        }

        private string GetSelectedJobNo()
        {
            string jobno = "";
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                jobno = Convert.ToString(slectedrow.Cells[0].Value);
            }
            return jobno;
        }

        private string GetSelectedAssyNo()
        {
            string assyno = "";
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                assyno = Convert.ToString(slectedrow.Cells[3].Value);
            }
            return assyno;
        }

        private void AddNewReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewRelease();
        }

        private bool IsAssembly(string assyno)
        {
            bool assy = false;
            DataTable iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetIteminfo(assyno);
            DataRow ra = iteminfo.Rows[0];
            string family = ra["FamilyCode"].ToString();
            iteminfo.Clear();

            switch (family.ToLower())
            {
                case "as":
                    assy = true;
                    break;

                case "ag":
                    assy = true;
                    break;

                case "asel":
                    assy = true;
                    break;

                case "aspn":
                    assy = true;
                    break;

                default:
                    assy = false;
                    break;
            }

            return assy;
        }

        private void ViewAllReleaseLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLogs releaseLogs = new ReleaseLogs();
            releaseLogs.Show();
        }

        private void getWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProrcessreportWorkOrder(Getselectedworkorder(), "WorkOrder");
        }

        private void viewCurrentJobReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewRelease viewRelease = new ViewRelease(wrkorder: GetSelectedJobNo(), job: true, jobassyno: connectapi.GetJobAssyNo(GetSelectedJobNo()), jobno: GetSelectedJobNo());
            viewRelease.Show();
        }

        private void viewReleasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewRelease viewRelease = new ViewRelease(wrkorder: Getselectedworkorder(), job: false, jobassyno: GetSelectedAssyNo(), jobno: GetSelectedJobNo());
            viewRelease.Show();
        }
    }
}