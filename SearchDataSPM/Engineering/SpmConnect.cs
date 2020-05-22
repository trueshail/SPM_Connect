using SearchDataSPM.Admin_developer;
using SearchDataSPM.Controls;
using SearchDataSPM.General;
using SearchDataSPM.Miscellaneous;
using SearchDataSPM.Purchasing;
using SearchDataSPM.Report;
using SolidWorks.Interop.sldworks;
using SPMConnectAPI;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using wpfPreviewFlowControl;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.Engineering
{
    public partial class SpmConnect : Form
    {
        #region SPM Connect Load

        private log4net.ILog log;
        private DataTable dt;
        private bool formloading;
        private bool eng;
        private bool production;
        private bool controls;
        private int _advcollapse;
        private bool showingduplicates;
        private bool doneshowingSplash;
        private bool showingfavorites;
        private readonly SPMSQLCommands connectapi = new SPMSQLCommands();

        private readonly SPMConnectAPI.Controls connectapicntrls = new SPMConnectAPI.Controls();

        public SpmConnect()
        {
            InitializeComponent();
            formloading = true;
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            this.Hide();
            Collapse();
            Loadusersettings();
            dt = new DataTable();
            Checkdeptsandrights();
            Sqlnotifier();
            Showallitems();
            txtSearch.Focus();
            pictureBox1.Visible = DateTime.Now.Month == 12;
            formloading = false;
            this.Show();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened SPM Connect Eng ");
        }

        private void Checkdeptsandrights()
        {
            if (ConnectUser.Dept == Department.Controls)
            {
                controls = true;
                listView.ContextMenuStrip = Listviewcontextmenu;
                Listviewcontextmenu.Items[3].Enabled = false;
                Listviewcontextmenu.Items[3].Visible = false;
                Listviewcontextmenu.Items[4].Enabled = false;
                Listviewcontextmenu.Items[4].Visible = false;

                AddNewBttn.Enabled = false;
                getnewitembttn.Enabled = true;

                dataGridView.ContextMenuStrip = FormSelectorControls;
                this.Text = "SPM Connect Controls - " + ConnectUser.Name;
                connectapi.Chekin("SPM Connect Controls");

                //connectapicntrls.SPM_Connect();
                //connectapicntrls.SPM_Connectconnectsql();
            }
            else if (ConnectUser.Dept == Department.Eng)
            {
                listView.ContextMenuStrip = Listviewcontextmenu;
                dataGridView.ContextMenuStrip = FormSelectorEng;
                AddNewBttn.Enabled = true;
                getnewitembttn.Enabled = true;
                this.Text = "SPM Connect Engineering - " + ConnectUser.Name;
                connectapi.Chekin("SPM Connect Eng");
                eng = true;
            }
            else
            {
                listView.ContextMenuStrip = Listviewcontextmenu;
                Listviewcontextmenu.Items[3].Enabled = false;
                Listviewcontextmenu.Items[3].Visible = false;
                Listviewcontextmenu.Items[4].Enabled = false;
                Listviewcontextmenu.Items[4].Visible = false;
                AddNewBttn.Enabled = false;
                getnewitembttn.Enabled = true;
                dataGridView.ContextMenuStrip = FormSelectorEng;
                FormSelectorEng.Items[4].Enabled = false;
                FormSelectorEng.Items[4].Visible = false;
                FormSelectorEng.Items[5].Enabled = false;
                FormSelectorEng.Items[5].Visible = false;
                this.Text = "SPM Connect " + ConnectUser.Dept + " - " + ConnectUser.Name;
                connectapi.Chekin("SPM Connect " + ConnectUser.Dept);
                production = true;
            }

            if (ConnectUser.Admin)
            {
                admin_bttn.Enabled = true;
            }

            if (ConnectUser.Developer)
            {
                FormSelectorEng.Items[7].Enabled = true;
                FormSelectorEng.Items[7].Visible = true;

                Listviewcontextmenu.Items[7].Enabled = true;
                Listviewcontextmenu.Items[7].Visible = true;
            }

            versionlabel.Text = Getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connnect " + versionlabel.Text);
        }

        private void Loadusersettings()
        {
            this.Size = new Size(900, 750);
            this.CenterToScreen();
        }

        private void Fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            Fillfamilycodes();
            Fillmanufacturers();
            Filloem();
            Filldesignedby();
            Filllastsavedby();
            Filluserwithblock();
            FillMaterials();
            Clearfilercombos();
            formloading = false;
            Cursor.Current = Cursors.Default;
        }

        private void Clearfilercombos()
        {
            designedbycombobox.SelectedItem = null;
            oemitemcombobox.SelectedItem = null;
            Manufactureritemcomboxbox.SelectedItem = null;
            lastsavedbycombo.SelectedItem = null;
            familycomboxbox.SelectedItem = null;
            designedbycombobox.SelectedItem = null;
            ActiveCadblockcombobox.SelectedItem = null;
            designedbycombobox.Text = null;
            oemitemcombobox.Text = null;
            Manufactureritemcomboxbox.Text = null;
            lastsavedbycombo.Text = null;
            familycomboxbox.Text = null;
            designedbycombobox.Text = null;
            ActiveCadblockcombobox.Text = null;
            MaterialcomboBox.Text = null;
        }

        private void Showallitems()
        {
            dt.Clear();
            dt = connectapi.Showallitems();
            dataGridView.DataSource = dt;
            _ = dt.DefaultView;
            //dataGridView.Sort(itemNumberDataGridViewTextBoxColumn, ListSortDirection.Descending);
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
            dataGridView.Columns[5].Visible = false;
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
            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[2].Width = 55;
            dataGridView.Columns[1].Width = 300;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            UpdateFont();
            showingfavorites = false;
            showingduplicates = false;
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            Performreload();
        }

        private void Performreload()
        {
            Clearandhide();
            //clearfilercombos();
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dataGridView.Refresh();
            showingfavorites = false;
            showingduplicates = false;
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

        #region Monitorusertable

        private SqlTableDependency<UserControl> _dependency;

        public void Sqlnotifier()
        {
            var mapper = new ModelToTableMapper<UserControl>();
            mapper.AddMapping(c => c.Username, "User Name");
            mapper.AddMapping(c => c.Computername, "Computer Name");
            mapper.AddMapping(c => c.Application, "Application Running");

            _dependency = new SqlTableDependency<UserControl>(ConnectConnectionString(), tableName: "Checkin", mapper: mapper);
            _dependency.OnChanged += Dependency_OnChanged;
            _dependency.OnError += Dependency_OnError;
            _dependency.Start();
        }

        private void Dependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<UserControl> e)
        {
            string changed;
            var changedEntity = e.Entity;
            string type = e.ChangeType.ToString();
            changed = string.Format(changedEntity.Username);
            string application = string.Format(changedEntity.Application);
            string userName = GetUserName();
            // MessageBox.Show(changed);
            if (changed == userName && type == "Update" && (application == "SPM Connect " + ConnectUser.Dept))
            {
                //MessageBox.Show(this,"Developer has kicked you out due to maintenance issues.");
                _dependency.Stop();
                try
                {
                    Process.Start(Application.ExecutablePath);
                    System.Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
            }
            else if (changed == userName && type == "Delete" && (application == "SPM Connect " + ConnectUser.Dept))
            {
                System.Environment.Exit(0);
            }
        }

        private void Dependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            log.Error(e);
        }

        #endregion Monitorusertable

        #region Public Table & variables

        // variables required outside the functions to perfrom
        private readonly string fullsearch = ("Description LIKE '%{0}%' OR Manufacturer LIKE '%{0}%' OR ManufacturerItemNumber LIKE '%{0}%' OR ItemNumber LIKE '%{0}%'");

        //private readonly string fullsearch = ("FullSearch LIKE '%{0}%'");

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

        public void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Cursor.Current = Cursors.WaitCursor;
                string searchtexttxt = txtSearch.Text;
                formloading = true;
                //autoComplete.Add(txtSearch.Text);
                if (txtSearch.Text == "playgame")
                {
                    Lettergame lettergame = new Lettergame();
                    lettergame.Show();
                    return;
                }
                else if (string.Equals(txtSearch.Text, "showduplicates", StringComparison.CurrentCultureIgnoreCase))
                {
                    Showduplicates();
                    return;
                }
                if (Descrip_txtbox.Visible)
                {
                    Clearandhide();
                }
                if (!showingfavorites && !showingduplicates)
                {
                    if (string.IsNullOrEmpty(designedbycombobox.Text) && string.IsNullOrEmpty(lastsavedbycombo.Text) && string.IsNullOrEmpty(familycomboxbox.Text) && string.IsNullOrEmpty(Manufactureritemcomboxbox.Text) && string.IsNullOrEmpty(oemitemcombobox.Text) && string.IsNullOrEmpty(ActiveCadblockcombobox.Text) && string.IsNullOrEmpty(MaterialcomboBox.Text) && txtSearch.Text.Length == 0)
                    {
                        Showallitems();
                    }
                }
                else if (showingfavorites && !showingduplicates)
                {
                    Showfavorites();
                    txtSearch.Text = searchtexttxt;
                }
                else
                {
                    Showduplicates();
                    txtSearch.Text = searchtexttxt;
                }

                if (txtSearch.Text.Length > 0)
                {
                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");
                }
                if (!splitContainer1.Panel2Collapsed && this.Width <= 800)
                {
                    this.Size = new Size(1200, this.Height);
                }

                if (txtSearch.Text.Length > 0)
                {
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
                Cursor.Current = Cursors.Default;
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
            listFiles.Clear();
            listView.Clear();
            recordlabel.Text = "";
            formloading = false;
        }

        private void Mainsearch()
        {
            formloading = true;
            string search1 = txtSearch.Text;
            if (search1.Length > 3)
            {
                if (Char.IsLetter(search1.FirstOrDefault()) && search1.Substring(3, 1) == "%")
                {
                    try
                    {
                        search1 = search1.Replace("'", "''");
                        search1 = search1.Replace("[", "[[]");
                        const string fullsearch1 = "ItemNumber LIKE '%{0}%'";
                        string s = string.Format(fullsearch1, search1);
                        table0 = connectapi.ShowFilterallitems(s, true);
                        dataGridView.DataSource = table0;
                        dataGridView.Update();
                        SearchStringPosition();
                        Searchtext(txtSearch.Text.Substring(0, txtSearch.Text.Length - 1));
                        dataGridView.Refresh();
                        recordlabel.Text = "Found " + table0.Rows.Count.ToString() + " Matching Items.";
                    }
                    catch (Exception)

                    {
                        MessageBox.Show("Invalid Search Criteria Operator.", "SPM Connect - Search1");
                        txtSearch.Clear();
                        SendKeys.Send("~");
                    }
                }
                else
                {
                    try
                    {
                        search1 = search1.Replace("'", "''");
                        search1 = search1.Replace("[", "[[]");
                        table0 = connectapi.ShowFilterallitems(search1, false);
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
                }
            }
            else
            {
                try
                {
                    search1 = search1.Replace("'", "''");
                    search1 = search1.Replace("[", "[[]");
                    table0 = connectapi.ShowFilterallitems(search1, false);
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
                //autoComplete.Add(search2);
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

                if (!splitContainer1.Panel2Collapsed && this.Width <= 800)
                {
                    this.Size = new Size(1200, this.Height);
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

        private void Filteroem_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table1.DefaultView;
            table1 = dv.ToTable();

            if (e.KeyCode == Keys.Return)
            {
                formloading = true;
                string search3 = filteroem_txtbox.Text;
                //autoComplete.Add(search3);
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
                //autoComplete.Add(search4);
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

        private void Filter4_KeyDown(object sender, KeyEventArgs e)
        {
            DataView dv = table3.DefaultView;
            table3 = dv.ToTable();
            if (e.KeyCode == Keys.Return)
            {
                formloading = true;
                string search5 = filter4.Text;
                //autoComplete.Add(search5);
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

        #endregion Search Parameters

        #region OpenModel and datagridview events

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string itemnumber;

            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);

            if (dataGridView.SelectedCells.Count == 1 && c == "0")
            {
                if (controls)
                {
                    GetRowInfo();
                }
                else
                {
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    itemnumber = Convert.ToString(slectedrow.Cells[0].Value);
                    ItemInfo itemInfo = new ItemInfo(itemno: itemnumber);
                    itemInfo.Show();
                }
            }
            else
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                itemnumber = Convert.ToString(slectedrow.Cells[0].Value);
                ItemInfo itemInfo = new ItemInfo(itemno: itemnumber);
                itemInfo.Show();
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

        private void OpenModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string Item = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(ItemNo);
            //  checkforprocess();
            if (production)
            {
                connectapi.Checkforspmfileprod(Item);
            }
            else if (eng)
            {
                connectapi.Checkforspmfile(Item);
            }
        }

        private void OpenDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string item = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(str);
            // checkforprocess();
            if (production)
            {
                connectapi.Checkforspmdrwfileprod(item);
            }
            else if (eng)
            {
                connectapi.Checkforspmdrwfile(item);
            }
        }

        private void DataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
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

        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            }
        }

        private void Jobsbttn_Click(object sender, EventArgs e)
        {
            Openjobmodule();
        }

        private void Openjobmodule()
        {
            SPM_ConnectJobs sPM_ConnectJobs = new SPM_ConnectJobs();
            sPM_ConnectJobs.Show();
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

        #endregion OpenModel and datagridview events

        #region Highlight Search Results

        private bool IsSelected;

        public void SearchStringPosition()
        {
            IsSelected = true;
        }

        private string sw;

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

        #region AdminControl

        private void Label1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("http://www.spm-automation.com/");
        }

        private void Admin_bttn_Click(object sender, EventArgs e)
        {
            AdminControl spmadmin = new AdminControl
            {
                Owner = this
            };
            spmadmin.Show();
        }

        #endregion AdminControl

        #region ParentView

        //public static string whereused;

        private void ParentView_Click(object sender, EventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
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

            Processwhereused(item);
        }

        private void Processwhereused(string item)
        {
            //whereused = item;
            WhereUsed whereUsed = new WhereUsed(item: item);
            whereUsed.Show();
            //whereused = null;
        }

        private void WhereUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
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

            Processwhereused(item);
        }

        #endregion ParentView

        #region TreeView

        private void TreeView_Bttn_Click(object sender, EventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
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

            Processbom(item);
        }

        private void Processbom(string itemvalue)
        {
            TreeView treeView = new TreeView(item: itemvalue);
            treeView.Show();
        }

        private void BOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
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

            Processbom(item);
            //TreeView_Bttn.PerformClick();
            // TreeView_Bttn_Click(sender, e);
        }

        #endregion TreeView

        #region Closing SPMConnect

        private void SPM_Connect_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed SPM Connect Eng ");
            this.Dispose();
        }

        private void SPM_Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
            int openforms = Application.OpenForms.Count;
            if (openforms >= 2)
            {
                e.Cancel = true;
                ListOpenWindows frm2 = new ListOpenWindows();
                bool purchasereqopen = false;

                foreach (Form frm in Application.OpenForms)
                {
                    //if (frm.Name.ToString() == "SPM_Connect" || frm.Name.ToString() == "SPM_ConnectHome")
                    //{
                    //}
                    if (frm.Name == "PurchaseReqform")
                    {
                        frm2.listBox1.Items.Add(frm.Text);
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

                    Closeallconnections();
                }
            }
            else
            {
                Closeallconnections();
            }
        }

        private void Closeallconnections()
        {
            Cursor.Current = Cursors.WaitCursor;
            _dependency.Stop();
            connectapi.Checkout();
            Cursor.Current = Cursors.Default;
        }

        #endregion Closing SPMConnect

        #region DELETE Item

        private void DeleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectapi.Deleteitem(Getitemnumberselected());
        }

        #endregion DELETE Item

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
                string item;
                if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
                {
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    item = Convert.ToString(slectedrow.Cells[0].Value);
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
                string item;
                if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
                {
                    int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                    item = Convert.ToString(slectedrow.Cells[0].Value);
                }
                else
                {
                    item = "";
                }

                Processwhereused(item);
                return true;
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();

                return true;
            }
            if (keyData == (Keys.Alt | Keys.F))
            {
                if (showingfavorites)
                {
                    Performreload();
                }
                else
                {
                    Showfavorites();
                }

                return true;
            }
            if (keyData == (Keys.Control | Keys.J))
            {
                Openjobmodule();

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

            if (keyData == (Keys.Control | Keys.Q))
            {
                if (dataGridView.SelectedCells.Count == 1)
                {
                    Compare sPM_ConnectJobs = new Compare();
                    sPM_ConnectJobs.item(Getitemnumberselected());
                    sPM_ConnectJobs.Show();
                    return true;
                }
                else
                {
                    MessageBox.Show("No item found to run the compare tool", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (keyData == Keys.F1)
            {
                Showhelp();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region ItemListView events

        private void Getitemstodisplay(string Pathpart, string selecteditem)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart, "*" + selecteditem + "*").Where(str => !str.Contains(@"\~$")).OrderByDescending(fi => fi))
                {
                    try
                    {
                        string sDocFileName = item;
                        wpfThumbnailCreator pvf = new wpfThumbnailCreator();
                        System.Drawing.Size size = new Size
                        {
                            Width = 256,
                            Height = 256
                        };
                        pvf.DesiredSize = size;
                        System.Drawing.Bitmap pic = pvf.GetThumbNail(sDocFileName);
                        imageList.Images.Add(pic);
                        //axEModelViewControl1 = new EModelViewControl();
                        //axEModelViewControl1.OpenDoc(item, false, false, true, "");
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message);

                        const ShellEx.IconSizeEnum size = ShellEx.IconSizeEnum.ExtraLargeIcon;
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

        private readonly List<string> listFiles = new List<string>();
        //private EModelViewControl axEModelViewControl1;

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView.FocusedItem != null)
            // Process.Start(listFiles[listView.FocusedItem.Index]);
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        public static Icon GetIconOldSchool(string fileName)
        {
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out _);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        public static Icon GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                const ShellEx.IconSizeEnum ExtraLargeIcon = default;
                const ShellEx.IconSizeEnum size = (ShellEx.IconSizeEnum)ExtraLargeIcon;

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

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] fList = new string[1];
            fList[0] = Makepathfordrag();
            DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            _ = DoDragDrop(dataObj, DragDropEffects.Move | DragDropEffects.Copy);
            // listView.DoDragDrop(listView.SelectedItems, DragDropEffects.Copy);
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (!formloading)
            if (dataGridView.Rows.Count > 0)
            {
                Showfilesonlistview();
            }
            else
            {
                listFiles.Clear();
                listView.Clear();
            }
        }

        private void Showfilesonlistview()
        {
            try
            {
                if (dataGridView.SelectedCells.Count == 1)
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

                    const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                    string Pathpart = (spmcadpath + first3char);
                    Directory.CreateDirectory(Pathpart);
                    var fileCount = (from file in Directory.EnumerateFiles(Pathpart, "*" + item + "*", SearchOption.AllDirectories)
                                     select file).Count();

                    if (fileCount > 0)
                    {
                        Getitemstodisplay(Pathpart, item);
                    }
                }
            }
            catch
            {
            }
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                // makepathfordrag();
            }
        }

        private string Makepathfordrag()
        {
            string txt = listView.FocusedItem.Text;
            string first3char = txt.Substring(0, 3) + @"\";
            const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            return spmcadpath + first3char + txt;
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
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

        private void ListView_Enter(object sender, EventArgs e)
        {
            if (ConnectUser.Dept == Department.Eng)
            {
                listView.ContextMenuStrip = listView.Items.Count > 0 ? Listviewcontextmenu : null;
            }
        }

        #endregion ItemListView events

        #region listview menu strip

        private void Bomlistviewmenustrpc_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                Processbom(txt);
            }
        }

        private void WhereusedlistviewStripMenu_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                Processwhereused(txt);
            }
        }

        private void IteminfolistviewStripMenu_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                ItemInfo itemInfo = new ItemInfo(itemno: txt);
                itemInfo.Show();
            }
        }

        private void Listviewcontextmenu_Opening(object sender, CancelEventArgs e)
        {
            Listviewcontextmenu.Enabled = listView.SelectedItems.Count == 1;
        }

        #endregion listview menu strip

        #region Add new item, edit item, open item and get create entries

        private void AddNewBttn_Click(object sender, EventArgs e)
        {
            chekeditbutton = "no";
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new item?", "SPM Connect - Add New Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (connectapi.Solidworks_running())
                {
                    string activeblock = connectapi.Getactiveblock();

                    if (activeblock.Length > 0)
                    {
                        string lastnumber = connectapi.Getlastnumber();
                        if (lastnumber.Length > 0)
                        {
                            if (connectapi.Validnumber(lastnumber))
                            {
                                uniqueid = connectapi.Spmnew_idincrement(lastnumber, activeblock);
                                //insertinto_blocks(uniqueid, activeblock.ToString());
                                Itempresentdecide(connectapi.Checkitempresentoninventory(uniqueid));
                            }
                        }
                        else
                        {
                            Spmnew_id(activeblock);
                            //insertinto_blocks(uniqueid, activeblock.ToString());
                            Itempresentdecide(connectapi.Checkitempresentoninventory(uniqueid));
                        }
                    }
                    else
                    {
                        MessageBox.Show("User block number has not been assigned. Please contact the admin.", "SPM Connect - Add New", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private string uniqueid;

        private void Spmnew_id(string blocknumber)
        {
            string letterblock = Char.ToUpper(blocknumber[0]) + blocknumber.Substring(1);
            uniqueid = letterblock + "000";
        }

        private void Itempresentdecide(bool itempresent)
        {
            if (itempresent)
            {
                Openiteminfo(uniqueid);
            }
            else
            {
                string username = ConnectUser.Name;
                Createentryoninventory(uniqueid, username);
                Openiteminfo(uniqueid);
            }
        }

        private void Createentryoninventory(string uniqueid, string user)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Inventory] (ItemNumber, DesignedBy, DateCreated, LastSavedBy, LastEdited, JobPlanning) VALUES('" + uniqueid + "','" + user + "','" + sqlFormattedDate + "','" + user + "','" + sqlFormattedDate + "','1'  )";
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create Entry On SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void Openiteminfo(string itemid)
        {
            NewItem newItem = new NewItem(itemid);
            newItem.Editbtn(chekeditbutton);
            chekeditbutton = "";
            newItem.ShowDialog(this);
            newItem.Dispose();
        }

        private void Editbttn_Click(object sender, EventArgs e)
        {
            Processeditbutton(Getitemnumberselected());
        }

        public string chekeditbutton = "";

        private void Processeditbutton(string item)
        {
            Showfilesonlistview();
            Cursor.Current = Cursors.WaitCursor;
            chekeditbutton = "yes";
            this.Enabled = false;
            if (connectapi.Solidworks_running())
            {
                //string item = getitemnumberselected().ToString();

                if (connectapi.Checkitempresentoninventory(item))
                {
                    if (listView.Items.Count > 0)
                    {
                        Getitemopenforedit(item);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Item does not contain a model. Do you wish to create a  model to edit the properties?", "SPM Connect - Process Edit button", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Processmodelcreattion(item);
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("Item does not exist on SPM Connect Server", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Getitemonconnectsql(item);
                }
            }
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void Getitemonconnectsql(string item)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (Additemtosqlinventory(item))
                {
                    Updateitemtosqlinventory(item);
                    Processeditbutton(item);
                }
            }
            else
            {
                MessageBox.Show("Only one item can be selected at a time.", "SPM Connect - Get Item on Connect SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Getitemopenforedit(string item)
        {
            await Task.Run(() => SplashDialog("Opening Model....")).ConfigureAwait(true);
            Thread.Sleep(2000);
            if (connectapi.Getfilename() != item)
            {
                connectapi.Checkforspmfile(item);
                if (Checkforreadonly())
                {
                    chekeditbutton = "yes";
                    doneshowingSplash = true;
                    Openiteminfo(item);
                }
            }
            else
            {
                if (Checkforreadonly())
                {
                    doneshowingSplash = true;
                    Openiteminfo(item);
                }
            }
        }

        private void SplashDialog(string message)
        {
            doneshowingSplash = false;
            ThreadPool.QueueUserWorkItem((_) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Focus();
                    splashForm.Activate();
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + ((this.Width - splashForm.Width) / 2), this.Location.Y + ((this.Height - splashForm.Height) / 2));
                    splashForm.Show();
                    while (!doneshowingSplash)
                        Application.DoEvents();
                }
            });
        }

        private string Getitemnumberselected()
        {
            int selectedclmindex = dataGridView.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];

                //MessageBox.Show(ItemNo);
                return Convert.ToString(slectedrow.Cells[0].Value);
            }
            else
            {
                return "";
            }
        }

        public bool Checkforreadonly()
        {
            const string progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
            swApp.Visible = true;
            int count = swApp.GetDocumentCount();

            if (count > 0)
            {
                ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                if (swModel.IsOpenedReadOnly())
                {
                    MessageBox.Show("Model is open read only. Please get write access from the associated user in order to edit the properties.", "SPM Connect - Check For Read Only", MessageBoxButtons.OK, MessageBoxIcon.Stop);

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

        private bool Additemtosqlinventory(string uniqueid)
        {
            bool success = false;

            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT  [SPM_Database].[dbo].[Inventory] (ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited) SELECT ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber = '" + uniqueid + "'";
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                success = true;
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Item To SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            return success;
        }

        private void Updateitemtosqlinventory(string uniqueid)
        {
            string familycategory = connectapi.Getfamilycategory(Getfamilycode());
            //MessageBox.Show(familycategory);
            string rupture = "ALWAYS";

            if (string.Equals(familycategory, "purchased", StringComparison.CurrentCultureIgnoreCase))
            {
                rupture = "NEVER";
            }
            string username = ConnectUser.Name;

            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Inventory] SET FamilyType = '" + familycategory + "',Rupture = '" + rupture + "',JobPlanning = '1',LastSavedBy = '" + username + "',DateCreated = '" + sqlFormattedDate + "',LastEdited = '" + sqlFormattedDate + "'  WHERE ItemNumber = '" + uniqueid + "'";
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update Item SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private string Getfamilycode()
        {
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];

                //MessageBox.Show(familycode);
                return Convert.ToString(slectedrow.Cells[2].Value);
            }
            else
            {
                return "";
            }
        }

        private void Processmodelcreattion(string item)
        {
            if (listView.Items.Count == 0 && string.IsNullOrEmpty(Getfamilycode()))
            {
                Openiteminfo(item);
            }
            else
            {
                string category = connectapi.Getfamilycategory(Getfamilycode());
                string path = connectapi.Makepath(item);
                if (category != "")
                {
                    if (string.Equals(category, "manufactured", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //MessageBox.Show(path);
                        Directory.CreateDirectory(path);
                        string filename = path + item + ".sldprt";
                        connectapi.Createmodel(filename);
                        string draw = path + item + ".slddrw";
                        connectapi.Createdrawingpart(draw, item);
                        Getitemopenforedit(item);
                    }
                    else if (string.Equals(category, "assembly", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Directory.CreateDirectory(path);
                        string filename = path + item + ".sldasm";
                        connectapi.CreateAssy(filename);
                        string draw = path + item + ".slddrw";
                        connectapi.Createdrawingpart(draw, item);
                        Getitemopenforedit(item);
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
                        Openiteminfo(item);
                    }
                }
                else
                {
                    MessageBox.Show("Please check on Family Code", "SPM Connect - Family Category Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EditItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processeditbutton(Getitemnumberselected());
        }

        #endregion Add new item, edit item, open item and get create entries

        #region Copy Item

        private void Saveascopybttn_Click(object sender, EventArgs e)
        {
            Perfomcopybuttonclick(Getitemnumberselected());
        }

        private async void Perfomcopybuttonclick(string item)
        {
            DialogResult result = MessageBox.Show("Are you sure want to copy item no. " + item + " to a new item?", "SPM Connect - Copy Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (connectapi.Solidworks_running())
                {
                    string user = GetUserName();
                    string activeblock = connectapi.Getactiveblock();
                    string lastnumber = connectapi.Getlastnumber();
                    if (activeblock.Length > 0)
                    {
                        if (connectapi.Validnumber(lastnumber))
                        {
                            await Task.Run(() => SplashDialog("Copying Model.....")).ConfigureAwait(true);
                            Thread.Sleep(3000);
                            Prepareforcopy(activeblock, item, lastnumber);
                        }
                    }
                    else
                    {
                        MessageBox.Show("User block number has not been assigned. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void Prepareforcopy(string activeblock, string selecteditem, string lastnumber)
        {
            string first3char = selecteditem.Substring(0, 3) + @"\";
            const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            string Pathpart = (spmcadpath + first3char);

            if (lastnumber.Length > 0)
            {
                uniqueid = connectapi.Spmnew_idincrement(lastnumber, activeblock);

                if (connectapi.Checkitempresentoninventory(uniqueid))
                {
                    //insertinto_blocks(uniqueid, activeblock.ToString());
                    Openiteminfo(uniqueid);
                }
                else
                {
                    Copy(Pathpart, selecteditem);
                    Aftercopy(selecteditem);
                }
            }
            else
            {
                Spmnew_id(activeblock);
                Copy(Pathpart, selecteditem);
                Aftercopy(selecteditem);
            }
        }

        private void Aftercopy(string selecteditem)
        {
            if (sucessreplacingreference)
            {
                //insertinto_blocks(uniqueid, activeblock.ToString());

                if (connectapi.Checkitempresentoninventory(selecteditem))
                {
                    connectapi.Addcpoieditemtosqltable(selecteditem, uniqueid);
                }
                else
                {
                    connectapi.Addcpoieditemtosqltablefromgenius(uniqueid, selecteditem);
                    Updateitemtosqlinventory(uniqueid);
                }
                doneshowingSplash = true;
                Getitemopenforedit(uniqueid);
                //NewItem newItem = new NewItem();
                //newItem.chekbeforefillingcustomproperties(uniqueid);
            }
            else
            {
                MessageBox.Show("SPM Connect failed to update drawing references.! Please manually update drawing references.", "SPM Connect - Copy References", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (connectapi.Checkitempresentoninventory(selecteditem))
                {
                    connectapi.Addcpoieditemtosqltable(selecteditem, uniqueid);
                }
                else
                {
                    connectapi.Addcpoieditemtosqltablefromgenius(uniqueid, selecteditem);
                    Updateitemtosqlinventory(uniqueid);
                }
                doneshowingSplash = true;
                Getitemopenforedit(uniqueid);
            }
        }

        private void Copy(string Pathpart, string selecteditem)
        {
            string type = "";
            string drawingfound = "no";
            string oldpath = "";
            string newfirst3char = uniqueid.Substring(0, 3) + @"\";
            const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

            string[] s = Directory.GetFiles(Pathpart, "*" + selecteditem + "*", SearchOption.TopDirectoryOnly).Where(str => !str.Contains(@"\~$")).ToArray();

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
                Directory.CreateDirectory(newfilepathdir);

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

                Replacereference(newdraw, oldpath, newpath);
            }
            else
            {
                sucessreplacingreference = true;
            }
        }

        private bool sucessreplacingreference;

        private void Replacereference(string newdraw, string oldpath, string newpath)
        {
            const string progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
            sucessreplacingreference = swApp.ReplaceReferencedDocument(newdraw, oldpath, newpath);
        }

        #endregion Copy Item

        #region ToolStripMenu

        private void Prorcessreportbom(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer(Reportname, itemvalue);
            form1.Show();
        }

        private void BillsOfMaunfacturingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prorcessreportbom(Getitemnumberselected(), "BOM");
        }

        private void SparePartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prorcessreportbom(Getitemnumberselected(), "SPAREPARTS");
        }

        private void CopySelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Perfomcopybuttonclick(Getitemnumberselected());
        }

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("http://www.spm-automation.com/");
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                Processeditbutton(txt);
            }
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                Perfomcopybuttonclick(txt);
            }
        }

        private void EModelViewerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Emodelviewtoolstrp();
        }

        private void Emodelviewtoolstrp()
        {
            if (listView.Items.Count > 0)
            {
                string itemnumber = Getitemnumberselected();
                string Pathpart = connectapi.Makepath(itemnumber);
                string filename = Pathpart + itemnumber;

                string[] s = Directory.GetFiles(Pathpart, "*" + itemnumber + "*", SearchOption.TopDirectoryOnly).Where(str => !str.Contains(@"\~$")).ToArray();

                for (int i = 0; i < s.Length; i++)
                {
                    //MessageBox.Show(s[i]);
                    //MessageBox.Show(Path.GetFileName(s[i]));

                    if (s[i].Contains(".sldprt"))
                    {
                        filename += ".sldprt";
                    }
                    else if (s[i].Contains(".sldasm"))
                    {
                        filename += ".sldasm";
                    }
                }
                Edrawings.EModelViewer modelViewer = new Edrawings.EModelViewer();
                modelViewer.filetoopen(filename);
                modelViewer.Show();
            }
        }

        private void FormSelector_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (showingfavorites)
                {
                    FormSelectorEng.Items[11].Enabled = true;
                    FormSelectorEng.Items[11].Visible = true;
                    FormSelectorEng.Items[9].Enabled = false;
                    FormSelectorEng.Items[9].Visible = false;
                }
                else
                {
                    FormSelectorEng.Items[11].Enabled = false;
                    FormSelectorEng.Items[11].Visible = false;
                    FormSelectorEng.Items[9].Enabled = true;
                    FormSelectorEng.Items[9].Visible = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                try
                {
                    string file = listFiles[listView.FocusedItem.Index];
                    Edrawings.EModelViewer modelViewer = new Edrawings.EModelViewer();
                    modelViewer.filetoopen(file);
                    modelViewer.Show();
                }
                catch
                {
                }
            }
        }

        #endregion ToolStripMenu

        #region Advance Filters

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
                //if (formWidth <= 1000)
                //{
                //    this.Size = new Size(formWidth + 200, formHeight);
                //}

                //else
                //{
                //    splitContainer1.SplitterDistance = 800;
                //    this.Size = new Size(1200, formHeight);
                //}
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
                if (designedbycombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND DesignedBy = '{0}'", designedbycombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("DesignedBy = '{0}'", designedbycombobox.Text);
                    }
                }
                if (oemitemcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Manufacturer = '{0}'", oemitemcombobox.Text);
                    }
                    else
                    {
                        filter += string.Format("Manufacturer = '{0}'", oemitemcombobox.Text);
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
                if (familycomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND FamilyCode = '{0}'", familycomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("FamilyCode = '{0}'", familycomboxbox.Text);
                    }
                }
                if (Manufactureritemcomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ManufacturerItemNumber LIKE '%{0}%'", Manufactureritemcomboxbox.Text);
                    }
                    else
                    {
                        filter += string.Format("ManufacturerItemNumber LIKE '%{0}%'", Manufactureritemcomboxbox.Text);
                    }
                }
                if (ActiveCadblockcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ItemNumber LIKE '%{0}%'", ActiveCadblockcombobox.Text.Substring(0, 3));
                    }
                    else
                    {
                        filter += string.Format("ItemNumber LIKE '%{0}%'", ActiveCadblockcombobox.Text.Substring(0, 3));
                    }
                }
                if (MaterialcomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Material LIKE '%{0}%'", MaterialcomboBox.Text.Substring(0, 3));
                    }
                    else
                    {
                        filter += string.Format("Material LIKE '%{0}%'", MaterialcomboBox.Text.Substring(0, 3));
                    }
                }

                if (designedbycombobox.SelectedItem == null && lastsavedbycombo.SelectedItem == null && familycomboxbox.SelectedItem == null && Manufactureritemcomboxbox.SelectedItem == null && oemitemcombobox.SelectedItem == null && ActiveCadblockcombobox.SelectedItem == null && MaterialcomboBox.SelectedItem == null)
                {
                }
                Advfiltertables(filter);
            }
        }

        private void Advfiltertables(string filter)
        {
            if (!Descrip_txtbox.Visible)
            {
                dataGridView.DataSource = dt;
                dataTable.Clear();
                dt.DefaultView.RowFilter = filter;
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

        #region fillcomboboxes

        private void Filldesignedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filldesignedby();
            designedbycombobox.AutoCompleteCustomSource = MyCollection;
            designedbycombobox.DataSource = MyCollection;
        }

        private void Filllastsavedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filllastsavedby();
            lastsavedbycombo.AutoCompleteCustomSource = MyCollection;
            lastsavedbycombo.DataSource = MyCollection;
        }

        private void Fillmanufacturers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillmanufacturers();
            oemitemcombobox.AutoCompleteCustomSource = MyCollection;
            oemitemcombobox.DataSource = MyCollection;
        }

        private void Filloem()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filloem();
            Manufactureritemcomboxbox.AutoCompleteCustomSource = MyCollection;
            Manufactureritemcomboxbox.DataSource = MyCollection;
        }

        private void Fillfamilycodes()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Fillfamilycodes();
            familycomboxbox.AutoCompleteCustomSource = MyCollection;
            familycomboxbox.DataSource = MyCollection;
        }

        private void Filluserwithblock()
        {
            AutoCompleteStringCollection MyCollection = connectapi.Filluserwithblock();
            ActiveCadblockcombobox.AutoCompleteCustomSource = MyCollection;
            ActiveCadblockcombobox.DataSource = MyCollection;
        }

        private void FillMaterials()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillMaterials();
            MaterialcomboBox.AutoCompleteCustomSource = MyCollection;
            MaterialcomboBox.DataSource = MyCollection;
        }

        #endregion fillcomboboxes

        #region advance filters events

        private void Clrfiltersbttn_Click(object sender, EventArgs e)
        {
            Performreload();
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

        private void Oemitemcombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FilterProducts();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
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

        private void Familycomboxbox_KeyDown(object sender, KeyEventArgs e)
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

        private void Designedbycombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                designedbycombobox.Focus();
            }
        }

        private void Familycomboxbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                familycomboxbox.Focus();
            }
        }

        private void Oemitemcombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                oemitemcombobox.Focus();
            }
        }

        private void Manufactureritemcomboxbox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Manufactureritemcomboxbox.Focus();
            }
        }

        private void MaterialcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                MaterialcomboBox.Focus();
            }
        }

        private void Lastsavedbycombo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                lastsavedbycombo.Focus();
            }
        }

        private void ActiveCadblockcombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ActiveCadblockcombobox.Focus();
            }
        }

        #endregion advance filters events

        #endregion Advance Filters

        #region notification ballon

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            int openforms = Application.OpenForms.Count;
            if (openforms > 2)
            {
                bool purchasereqopen = false;

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "PurchaseReqform")
                    {
                        purchasereqopen = true;
                        frm.Show();
                        frm.Activate();
                        frm.BringToFront();
                        frm.Focus();
                        frm.WindowState = FormWindowState.Normal;
                    }
                }
                if (!purchasereqopen)
                {
                    PurchaseReqform purchaseReq = new PurchaseReqform();
                    purchaseReq.Show();
                }
            }
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
            this.Focus();
            this.WindowState = FormWindowState.Normal;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion notification ballon

        #region CONTROLS

        private void GetRowInfo()
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
            string description = Convert.ToString(slectedrow.Cells[1].Value);
            string family = Convert.ToString(slectedrow.Cells[2].Value);
            string Manufacturer = Convert.ToString(slectedrow.Cells[3].Value);
            string oem = Convert.ToString(slectedrow.Cells[4].Value);
            DialogResult result = MessageBox.Show(
                "ItemNumber = " + ItemNo + System.Environment.NewLine +
                "Description = " + description + System.Environment.NewLine +
                "Family = " + family + System.Environment.NewLine +
                "Manufacturer = " + Manufacturer + System.Environment.NewLine +
                "OEM = " + oem, "Add To AutoCad Catalog?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //code for Yes
                connectapicntrls.CheckAutoCad(ItemNo, description, Manufacturer, oem);
            }
            else if (result == DialogResult.No)
            {
                //code for No
            }
        }

        private void GetRowInfoforassy()
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string ItemNo = Convert.ToString(slectedrow.Cells[0].Value);
            string description = Convert.ToString(slectedrow.Cells[1].Value);
            string family = Convert.ToString(slectedrow.Cells[2].Value);
            string Manufacturer = Convert.ToString(slectedrow.Cells[3].Value);
            string oem = Convert.ToString(slectedrow.Cells[4].Value);

            if (family == "ASEL" || family == "AS" || family == "ASPN")
            {
                DialogResult result = MessageBox.Show(
                "ItemNumber = " + ItemNo + System.Environment.NewLine +
                "Description = " + description + System.Environment.NewLine +
                "Manufacturer = " + Manufacturer + System.Environment.NewLine +
                "Family = " + family + System.Environment.NewLine +
                "OEM = " + oem, "Create an Assembly To AutoCad Catalog?",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //code for Yes
                    int checkutocadforassy = connectapicntrls.CheckAutoCadforassy(ItemNo);

                    if (checkutocadforassy == 1)
                    {
                        Openassytocatalog(ItemNo);
                    }
                    else if (checkutocadforassy == 3)
                    {
                        if (connectapicntrls.CheckAssyOnGenius(ItemNo))
                        {
                            Createassycatalog(ItemNo, description, Manufacturer, oem, family);
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    //code for No
                }
            }
            else
            {
                MessageBox.Show("Item family must be a \"ASEL\" or \"ASPN\" or \"AS\". In order to create an assembly on the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Createassycatalog(string ItemNo, string description, string Manufacturer, string oem, string family)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                CreateAssyToCatalog treeView = new CreateAssyToCatalog();
                treeView.getallitems(ItemNo, description, family, Manufacturer, oem);
                treeView.ShowDialog();
            }
        }

        private void Openassytocatalog(string ItemNo)
        {
            AutocadAssembly treeView = new AutocadAssembly();
            treeView.Item(ItemNo);
            treeView.Show();
        }

        #endregion CONTROLS

        #region Controls ToolStrip

        private void AddToCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRowInfo();
        }

        private void CreateAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRowInfoforassy();
        }

        private void AutoCadCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Openassytocatalog(Getitemnumberselected());
        }

        private void GeniusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processbom(Getitemnumberselected());
        }

        private void AutoCadCatalogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutocadWhereUsed treeView = new AutocadWhereUsed();
            treeView.item(Getitemnumberselected());
            treeView.Show();
        }

        private void GeniusJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processwhereused(Getitemnumberselected());
        }

        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Prorcessreportbom(Getitemnumberselected(), "BOM");
        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Prorcessreportbom(Getitemnumberselected(), "SPAREPARTS");
        }

        private void EModelViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Emodelviewtoolstrp();
        }

        private void FormSelectorControls_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (showingfavorites)
                {
                    FormSelectorControls.Items[9].Enabled = true;
                    FormSelectorControls.Items[9].Visible = true;
                    FormSelectorControls.Items[7].Enabled = false;
                    FormSelectorControls.Items[7].Visible = false;
                }
                else
                {
                    FormSelectorControls.Items[9].Enabled = false;
                    FormSelectorControls.Items[9].Visible = false;
                    FormSelectorControls.Items[7].Enabled = true;
                    FormSelectorControls.Items[7].Visible = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion Controls ToolStrip

        private void Getnewitembttn_Click(object sender, EventArgs e)
        {
            if (connectapi.Validnumber(connectapi.Getlastnumber()))
            {
                string newid = connectapi.Spmnew_idincrement(connectapi.Getlastnumber(), connectapi.Getactiveblock());
                MessageBox.Show("Your next ItemNumber to use is :- " + newid, "SPM Connect - New Item Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    Clipboard.SetText(newid);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
            }
        }

        private void Versionlabel_DoubleClick(object sender, EventArgs e)
        {
            this.Size = new Size(900, 750);
            this.CenterToScreen();
        }

        #region AddtoFavorites

        private void AddToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectapi.Addtofavorites(Getitemnumberselected());
        }

        private void ShowFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Showfavorites();
        }

        private void Showfavorites()
        {
            Clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            dt.Clear();
            dt = connectapi.ShowFavorites();
            dataGridView.DataSource = dt;
            _ = dt.DefaultView;
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Descending);
            UpdateFont();
            showingfavorites = true;
            recordlabel.Text = "Showing " + dataGridView.Rows.Count + " favorite items.";
        }

        private void RemoveFromFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectapi.Removefromfavorites(Getitemnumberselected());
            Showfavorites();
        }

        #endregion AddtoFavorites

        private void Aboutbtn_Click(object sender, EventArgs e)
        {
            Showhelp();
        }

        private void Showhelp()
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        private void ToolStripMenuupdateitem_Click(object sender, EventArgs e)
        {
            connectapicntrls.Updateitempropertiesfromgenius(Getitemnumberselected());
        }

        private void RevelInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = Makepathfordrag();
            if (!File.Exists(filePath))
            {
                return;
            }
            string argument = "/select, \"" + filePath + "\"";
            Process.Start("explorer.exe", argument);
        }

        private void DELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to delete this file?", "SPM Connect - Delete File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string filePath = Makepathfordrag();
                if (!File.Exists(filePath))
                {
                    return;
                }
                try
                {
                    File.Delete(filePath);
                    Showfilesonlistview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Showduplicates()
        {
            Clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            dt.Clear();
            dt = connectapi.ShowDuplicates();
            dataGridView.DataSource = dt;
            _ = dt.DefaultView;
            UpdateFont();
            showingduplicates = true;
            recordlabel.Text = "Showing " + dataGridView.Rows.Count + " duplicate items.";
        }

        private void DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void DataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                int openforms = Application.OpenForms.Count;
                if (openforms >= 2)
                {
                    _ = new ListOpenWindows();
                    bool allowdragdrop = false;

                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm.Name == "ItemInfo" || frm.Name == "AddRelease")
                            allowdragdrop = true;
                    }
                    if (allowdragdrop)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            dataGridView.DoDragDrop(dataGridView.Rows[dataGridView.SelectedCells[0].RowIndex], DragDropEffects.All);
                        }
                    }
                }
            }
        }

        private void DataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}