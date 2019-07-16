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
using System.Threading;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using SPMConnectAPI;

namespace SearchDataSPM
{

    public partial class SPM_Connect : Form
    {
        #region SPM Connect Load

        string connection;
        string cntrlconnection;
        SqlConnection cn;
        DataTable dt;
        bool formloading = false;
        string userfullname = "";
        string department = "";
        bool eng = false;
        bool production = false;
        bool controls = false;
        int _advcollapse = 0;
        bool purchasereqnotification = false;
        bool showingfavorites = false;
        SearchDataSPM.pnotifier purchaseReq = new SearchDataSPM.pnotifier();
        SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();
        SPMConnectAPI.Controls connectapicntrls = new SPMConnectAPI.Controls();
        //AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        public SPM_Connect()
        {
            InitializeComponent();
            formloading = true;

            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            cntrlconnection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cntrlscn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error Connecting to SQL Server.....", "SPM Connect - Engineering Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            //connectapi.SPM_Connect();

        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            this.Hide();
            collapse();
            loadusersettings();
            dt = new DataTable();
            checkdeptsandrights();
            userfullname = connectapi.getuserfullname();
            sqlnotifier();
            if (purchasereqnotification = connectapi.CheckPurchaseReqNotification())
            {
                watchpreqtable();
                purchaseReq.currentusercreds();
            }
            //assignhistory();
            Showallitems();
            txtSearch.Focus();
            formloading = false;
            this.Show();
        }

        private void checkdeptsandrights()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            department = connectapi.getdepartment();

            if (department == "Controls")
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
                this.Text = "SPM Connect Controls - " + userName.ToString().Substring(4);
                connectapi.chekin("SPM Connect Controls");

                //connectapicntrls.SPM_Connect();
                //connectapicntrls.SPM_Connectconnectsql();

            }
            else if (department == "Eng")
            {
                listView.ContextMenuStrip = Listviewcontextmenu;
                dataGridView.ContextMenuStrip = FormSelectorEng;
                AddNewBttn.Enabled = true;
                getnewitembttn.Enabled = true;
                this.Text = "SPM Connect Engineering - " + userName.ToString().Substring(4);
                connectapi.chekin("SPM Connect Eng");
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
                this.Text = "SPM Connect " + department + " - " + userName.ToString().Substring(4);
                connectapi.chekin("SPM Connect " + department);
                production = true;
            }

            if (connectapi.CheckAdmin())
            {
                admin_bttn.Enabled = true;
            }

            if (connectapi.Checkdeveloper())
            {
                FormSelectorEng.Items[7].Enabled = true;
                FormSelectorEng.Items[7].Visible = true;

                Listviewcontextmenu.Items[7].Enabled = true;
                Listviewcontextmenu.Items[7].Visible = true;
            }

            versionlabel.Text = connectapi.getassyversionnumber();
            TreeViewToolTip.SetToolTip(versionlabel, "SPM Connnect " + versionlabel.Text);
        }

        private void loadusersettings()
        {
            this.Size = new Size(900, 750);
            this.CenterToScreen();
            if (Properties.Settings.Default.F1Size.Width == 0) Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.F1Size.Width == 0 || Properties.Settings.Default.F1Size.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.F1State;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.F1Location;
                this.Size = Properties.Settings.Default.F1Size;
            }
        }

        private void fillinfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            formloading = true;
            fillfamilycodes();
            fillmanufacturers();
            filloem();
            filldesignedby();
            filllastsavedby();
            filluserwithblock();
            FillMaterials();
            clearfilercombos();
            formloading = false;
            Cursor.Current = Cursors.Default;
        }

        void clearfilercombos()
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
            DataView dv = dt.DefaultView;
            dataGridView.Sort(itemNumberDataGridViewTextBoxColumn, ListSortDirection.Descending);
            UpdateFont();
            showingfavorites = false;
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            performreload();
        }

        void performreload()
        {
            clearandhide();
            //clearfilercombos();
            txtSearch.Clear();
            txtSearch.Focus();
            SendKeys.Send("~");
            dataGridView.Refresh();
            showingfavorites = false;
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

        //private void assignhistory()
        //{
        //    txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    txtSearch.AutoCompleteCustomSource = autoComplete;
        //    Descrip_txtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    Descrip_txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    Descrip_txtbox.AutoCompleteCustomSource = autoComplete;
        //    filteroem_txtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    filteroem_txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    filteroem_txtbox.AutoCompleteCustomSource = autoComplete;
        //    filteroemitem_txtbox.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    filteroemitem_txtbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    filteroemitem_txtbox.AutoCompleteCustomSource = autoComplete;
        //    filter4.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    filter4.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    filter4.AutoCompleteCustomSource = autoComplete;
        //}

        #endregion

        #region Monitor Purchase Req

        SqlTableDependency<PurhcaseReqSQL> _preqdependency;

        public void watchpreqtable()
        {
            var mapper = new ModelToTableMapper<PurhcaseReqSQL>();
            mapper.AddMapping(c => c.reqnumber, "ReqNumber");
            mapper.AddMapping(c => c.requestname, "RequestedBy");
            mapper.AddMapping(c => c.supervisorid, "SupervisorId");
            mapper.AddMapping(c => c.validate, "Validate");
            mapper.AddMapping(c => c.approved, "Approved");
            mapper.AddMapping(c => c.happroval, "HApproval");
            mapper.AddMapping(c => c.happroved, "Happroved");
            mapper.AddMapping(c => c.papproval, "PApproval");
            mapper.AddMapping(c => c.papproved, "Papproved");



            _preqdependency = new SqlTableDependency<PurhcaseReqSQL>(connection, tableName: "PurchaseReqBase", mapper: mapper);
            _preqdependency.OnChanged += _preqdependency_OnChanged;
            _preqdependency.OnError += _preqdependency_OnError;
            _preqdependency.Start();

        }

        private void _preqdependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<PurhcaseReqSQL> e)
        {
            var changedEntity = e.Entity;
            string type = e.ChangeType.ToString();

            if (type == "Update")
            {
                string requestname = "";
                int reqno = 0, validate = 0, approved = 0, happroval = 0, happroved = 0, papproval = 0, papproved = 0, supervisoridfromreq = 0;
                reqno = changedEntity.reqnumber;
                validate = changedEntity.validate;
                approved = changedEntity.approved;
                happroval = changedEntity.happroval;
                happroved = changedEntity.happroved;
                papproval = changedEntity.papproval;
                papproved = changedEntity.papproved;
                supervisoridfromreq = changedEntity.supervisorid;
                requestname = string.Format((changedEntity.requestname));


                if (requestname == userfullname && validate == 1)
                {

                    string message = purchaseReq.showpopupnotifation(reqno, validate, approved, happroval, happroved, papproval, papproved, requestname, supervisoridfromreq).ToString();
                    if (message != "no")
                    {
                        notifyIcon1.ShowBalloonTip(1000, reqno.ToString(), message + " \r\nClick to know more", ToolTipIcon.Info);
                    }


                }
            }
            // 
        }

        private void _preqdependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            throw e.Error;
        }

        #endregion

        #region Monitorusertable

        SqlTableDependency<UserControl> _dependency;

        public void sqlnotifier()
        {

            // The mapper object is used to map model properties 
            // that do not have a corresponding table column name.
            // In case all properties of your model have same name 
            // of table columns, you can avoid to use the mapper.
            var mapper = new ModelToTableMapper<UserControl>();
            mapper.AddMapping(c => c.username, "User Name");
            mapper.AddMapping(c => c.computername, "Computer Name");

            // Here - as second parameter - we pass table name: 
            // this is necessary only if the model name is different from table name 
            // (in our case we have Customer vs Customers). 
            // If needed, you can also specifiy schema name.

            _dependency = new SqlTableDependency<UserControl>(connection, tableName: "Checkin", mapper: mapper);
            _dependency.OnChanged += _dependency_OnChanged;
            _dependency.OnError += _dependency_OnError;
            _dependency.Start();
            //using (var dep = new SqlTableDependency<cust>(_con, tableName: "USER", mapper: mapper))
            //{
            //    dep.OnChanged += Changed;
            //    dep.Start();

            //    Console.WriteLine("Press a key to exit");
            //    Console.ReadKey();

            //    dep.Stop();
            //}
        }

        private void _dependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<UserControl> e)
        {
            string changed;
            var changedEntity = e.Entity;
            //Console.WriteLine("DML operation: " + e.ChangeType);
            // //Console.WriteLine("ID: " + changedEntity.Id);
            //Console.WriteLine("Name: " + changedEntity.Name);
            //Console.WriteLine("Surame: " + changedEntity.Surname);
            string type = e.ChangeType.ToString();
            changed = string.Format((changedEntity.username));
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            // MessageBox.Show(changed);
            if (changed == userName && type == "Delete")
            {
                //MessageBox.Show(this,"Developer has kicked you out due to maintenance issues.");
                _dependency.Stop();
                System.Environment.Exit(0);

            }
        }

        private void _dependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            throw e.Error;
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
                string searchtexttxt = txtSearch.Text;
                formloading = true;
                //autoComplete.Add(txtSearch.Text);
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
                if (!showingfavorites)
                {
                    if (designedbycombobox.Text == "" && lastsavedbycombo.Text == "" && familycomboxbox.Text == "" && Manufactureritemcomboxbox.Text == "" && oemitemcombobox.Text == "" && ActiveCadblockcombobox.Text == "" && MaterialcomboBox.Text == "")
                    {
                        Showallitems();
                    }
                }
                else
                {
                    showfavorites();
                    txtSearch.Text = searchtexttxt;
                }


                if (txtSearch.Text.Length > 0)
                {

                    Descrip_txtbox.Show();
                    SendKeys.Send("{TAB}");

                }
                if (splitContainer1.Panel2Collapsed == false && this.Width <= 800)
                {
                    this.Size = new Size(1200, this.Height);
                }

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
            listFiles.Clear();
            listView.Clear();
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
                        dataGridView.DataSource = table0;
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
                if (splitContainer1.Panel2Collapsed == false && this.Width <= 800)
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
                //autoComplete.Add(search3);
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
                //autoComplete.Add(search4);
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
                //autoComplete.Add(search5);
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

        #region OpenModel and datagridview events

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.item(itemnumber);
                    itemInfo.Show();
                }

            }
            else
            {

                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                itemnumber = Convert.ToString(slectedrow.Cells[0].Value);
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.item(itemnumber);
                itemInfo.Show();

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
            if (production)
            {
                connectapi.checkforspmfileprod(Item);
            }
            else if (eng)
            {
                connectapi.checkforspmfile(Item);
            }

        }

        private void openDrawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
            string item = Convert.ToString(slectedrow.Cells[0].Value);
            //MessageBox.Show(str);
            // checkforprocess();
            if (production)
            {
                connectapi.checkforspmdrwfileprod(item);
            }
            else if (eng)
            {
                connectapi.checkforspmdrwfile(item);
            }

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

        #region AdminControl

        private void label1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        private void admin_bttn_Click(object sender, EventArgs e)
        {
            spmadmin spmadmin = new spmadmin();
            spmadmin.Show();

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

                    closeallconnections();
                }

            }
            else
            {
                closeallconnections();
            }
        }


        private void closeallconnections()
        {
            Cursor.Current = Cursors.WaitCursor;
            _dependency.Stop();
            if (purchasereqnotification)
            {
                _preqdependency.Stop();
            }

            Properties.Settings.Default.F1State = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.F1Location = this.Location;
                Properties.Settings.Default.F1Size = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.F1Location = this.RestoreBounds.Location;
                Properties.Settings.Default.F1Size = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();
            connectapi.checkout();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region DELETE Item

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectapi.deleteitem(getitemnumberselected().ToString());
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
                processbom(item);

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

                processwhereused(item);
                return true;
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();

                return true;
            }
            if (keyData == (Keys.Shift | Keys.F))
            {
                if (showingfavorites)
                {
                    performreload();
                }
                else
                {
                    showfavorites();
                }

                return true;
            }
            if (keyData == (Keys.Control | Keys.J))
            {
                openjobmodule();

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

            if (keyData == (Keys.Control | Keys.Q))
            {
                if (dataGridView.SelectedCells.Count == 1)
                {
                    Compare sPM_ConnectJobs = new Compare();
                    sPM_ConnectJobs.item(getitemnumberselected());
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
                showhelp();
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

            }
        }

        //string Pathpart;

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {

                // makepathfordrag();
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
            //MessageBox.Show(Pathpart);   
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
            if (department == "Eng")
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

        #region Add new item, edit item, open item and get create entries

        private void AddNewBttn_Click(object sender, EventArgs e)
        {
            chekeditbutton = "no";
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to create a new item?", "SPM Connect - Add New Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (connectapi.solidworks_running() == true)
                {
                    string user = connectapi.UserName();
                    string activeblock = connectapi.getactiveblock();

                    if (activeblock.ToString().Length > 0)
                    {
                        string lastnumber = connectapi.getlastnumber();
                        if (lastnumber.ToString().Length > 0)
                        {
                            if (connectapi.validnumber(lastnumber.ToString()) == true)
                            {
                                uniqueid = connectapi.spmnew_idincrement(lastnumber.ToString(), activeblock.ToString());
                                //insertinto_blocks(uniqueid, activeblock.ToString());
                                itempresentdecide(connectapi.checkitempresentoninventory(uniqueid));

                            }

                        }
                        else
                        {
                            spmnew_id(activeblock.ToString());
                            //insertinto_blocks(uniqueid, activeblock.ToString());
                            itempresentdecide(connectapi.checkitempresentoninventory(uniqueid));

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

        private void spmnew_id(string blocknumber)
        {
            string letterblock = Char.ToUpper(blocknumber[0]) + blocknumber.Substring(1);
            uniqueid = letterblock + "000";
        }

        private void itempresentdecide(bool itempresent)
        {
            if (itempresent == true)
            {
                openiteminfo(uniqueid);
            }
            else
            {
                string username = connectapi.getuserfullname();
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
            newItem.ShowDialog(this);
            newItem.Dispose();

        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton(getitemnumberselected().ToString());
        }

        public string chekeditbutton = "";

        private void processeditbutton(string item)
        {
            showfilesonlistview();
            Cursor.Current = Cursors.WaitCursor;
            chekeditbutton = "yes";
            this.Enabled = false;
            if (connectapi.solidworks_running() == true)
            {
                //string item = getitemnumberselected().ToString();

                if (connectapi.checkitempresentoninventory(item) == true)
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
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (additemtosqlinventory(item))
                {
                    updateitemtosqlinventory(item);
                    processeditbutton(item);
                }
            }
            else
            {
                MessageBox.Show("Only one item can be selected at a time.", "SPM Connect - Get Item on Connect SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void getitemopenforedit(string item)
        {
            //new Thread(() => new Engineering.WaitFormOpening().ShowDialog()).Start();
            Thread t = new Thread(new ThreadStart(Splashopening));
            t.Start();

            if (connectapi.getfilename().ToString() != item)
            {
                connectapi.checkforspmfile(item);
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
                if (checkforreadonly() == true)
                {
                    t.Abort();
                    openiteminfo(item);
                }
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

        public bool checkforreadonly()
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

        private bool additemtosqlinventory(string uniqueid)
        {
            bool success = false;


            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT  [SPM_Database].[dbo].[Inventory] (ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited) SELECT ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber = '" + uniqueid + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
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
            return success;
        }

        private void updateitemtosqlinventory(string uniqueid)
        {

            string familycategory = connectapi.getfamilycategory(getfamilycode().ToString());
            //MessageBox.Show(familycategory);
            string rupture = "ALWAYS";

            if (familycategory.ToLower() == "purchased")
            {
                rupture = "NEVER";
            }
            string username = connectapi.getuserfullname();

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
                string category = connectapi.getfamilycategory(getfamilycode().ToString());
                string path = connectapi.Makepath(item).ToString();
                if (category != "")
                {
                    if (category.ToLower() == "manufactured")
                    {
                        //MessageBox.Show(path);
                        System.IO.Directory.CreateDirectory(path);
                        string filename = path + (item) + ".sldprt";
                        connectapi.createmodel(filename);
                        string draw = path + (item) + ".slddrw";
                        connectapi.createdrawingpart(draw, item);
                        getitemopenforedit(item);

                    }
                    else if (category.ToLower() == "assembly")
                    {
                        System.IO.Directory.CreateDirectory(path);
                        string filename = path + (item) + ".sldasm";
                        connectapi.createassy(filename);
                        string draw = path + (item) + ".slddrw";
                        connectapi.createdrawingpart(draw, item);
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
                else
                {
                    MessageBox.Show("Please check on Family Code", "SPM Connect - Family Category Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processeditbutton(getitemnumberselected().ToString());
        }

        #endregion

        #region Copy Item

        private void saveascopybttn_Click(object sender, EventArgs e)
        {
            perfomcopybuttonclick(getitemnumberselected().ToString());
        }

        void perfomcopybuttonclick(string item)
        {
            DialogResult result = MessageBox.Show("Are you sure want to copy item no. " + item + " to a new item?", "SPM Connect - Copy Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (connectapi.solidworks_running() == true)
                {
                    string user = connectapi.UserName();
                    string activeblock = connectapi.getactiveblock();
                    string lastnumber = connectapi.getlastnumber();
                    if (activeblock.ToString().Length > 0)
                    {
                        if (connectapi.validnumber(lastnumber.ToString()) == true)
                        {
                            new Thread(() => new Engineering.WaitFormCopying().ShowDialog()).Start();
                            //Thread.Sleep(3000);
                            prepareforcopy(activeblock.ToString(), item, lastnumber);
                        }
                    }
                    else
                    {
                        MessageBox.Show("User block number has not been assigned. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void prepareforcopy(string activeblock, string selecteditem, string lastnumber)
        {

            string first3char = selecteditem.Substring(0, 3) + @"\";
            string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
            string Pathpart = (spmcadpath + first3char);

            if (lastnumber.ToString().Length > 0)
            {

                uniqueid = connectapi.spmnew_idincrement(lastnumber.ToString(), activeblock.ToString());

                if (connectapi.checkitempresentoninventory(uniqueid) == true)
                {
                    //insertinto_blocks(uniqueid, activeblock.ToString());
                    openiteminfo(uniqueid);
                }
                else
                {
                    copy(Pathpart, selecteditem);
                    aftercopy(activeblock, selecteditem);
                }
            }
            else
            {
                spmnew_id(activeblock.ToString());
                copy(Pathpart, selecteditem);
                aftercopy(activeblock, selecteditem);
            }

        }

        private void aftercopy(string activeblock, string selecteditem)
        {
            if (sucessreplacingreference == true)
            {
                //insertinto_blocks(uniqueid, activeblock.ToString());

                if (connectapi.checkitempresentoninventory(selecteditem) == true)
                {
                    connectapi.addcpoieditemtosqltable(selecteditem, uniqueid);
                }
                else
                {
                    connectapi.addcpoieditemtosqltablefromgenius(uniqueid, selecteditem);
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

                if (connectapi.checkitempresentoninventory(selecteditem) == true)
                {
                    connectapi.addcpoieditemtosqltable(selecteditem, uniqueid);
                }
                else
                {
                    connectapi.addcpoieditemtosqltablefromgenius(uniqueid, selecteditem);
                    updateitemtosqlinventory(uniqueid);
                }
                Engineering.WaitFormCopying f = new Engineering.WaitFormCopying();
                f = (Engineering.WaitFormCopying)Application.OpenForms["WaitFormCopying"];
                f.Invoke(new ThreadStart(delegate { f.Close(); }));
                getitemopenforedit(uniqueid);
            }
        }

        private void copy(string Pathpart, string selecteditem)
        {
            string type = "";
            string drawingfound = "no";
            string oldpath = "";
            string newfirst3char = uniqueid.Substring(0, 3) + @"\";
            string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";


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

        #endregion

        #region ToolStripMenu

        private void prorcessreportbom(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(itemvalue);
            form1.getreport(Reportname);
            form1.Show();

        }

        private void billsOfMaunfacturingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "BOM");
        }

        private void sparePartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "SPAREPARTS");
        }

        private void copySelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            perfomcopybuttonclick(getitemnumberselected().ToString());
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                processeditbutton(txt);

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                txt = txt.Substring(0, 6);
                perfomcopybuttonclick(txt);

            }

        }

        private void eModelViewerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            emodelviewtoolstrp();
        }

        private void emodelviewtoolstrp()
        {
            if (listView.Items.Count > 0)
            {
                string itemnumber = getitemnumberselected().ToString();
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
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
            if (formloading)
            {

            }
            else
            {
                string filter = "";
                if (designedbycombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format("AND DesignedBy = '{0}'", designedbycombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("DesignedBy = '{0}'", designedbycombobox.Text.ToString());
                    }

                }
                if (oemitemcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Manufacturer = '{0}'", oemitemcombobox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("Manufacturer = '{0}'", oemitemcombobox.Text.ToString());
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
                if (familycomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND FamilyCode = '{0}'", familycomboxbox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("FamilyCode = '{0}'", familycomboxbox.Text.ToString());
                    }

                }
                if (Manufactureritemcomboxbox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ManufacturerItemNumber LIKE '%{0}%'", Manufactureritemcomboxbox.Text.ToString());
                    }
                    else
                    {
                        filter += string.Format("ManufacturerItemNumber LIKE '%{0}%'", Manufactureritemcomboxbox.Text.ToString());
                    }

                }
                if (ActiveCadblockcombobox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND ItemNumber LIKE '%{0}%'", ActiveCadblockcombobox.Text.ToString().Substring(0, 3));
                    }
                    else
                    {
                        filter += string.Format("ItemNumber LIKE '%{0}%'", ActiveCadblockcombobox.Text.ToString().Substring(0, 3));
                    }

                }
                if (MaterialcomboBox.Text.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        //filter += "AND";
                        filter += string.Format(" AND Material LIKE '%{0}%'", MaterialcomboBox.Text.ToString().Substring(0, 3));
                    }
                    else
                    {
                        filter += string.Format("Material LIKE '%{0}%'", MaterialcomboBox.Text.ToString().Substring(0, 3));
                    }

                }

                if (designedbycombobox.SelectedItem == null && lastsavedbycombo.SelectedItem == null && familycomboxbox.SelectedItem == null && Manufactureritemcomboxbox.SelectedItem == null && oemitemcombobox.SelectedItem == null && ActiveCadblockcombobox.SelectedItem == null && MaterialcomboBox.SelectedItem == null)
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

        private void filldesignedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.filldesignedby();
            designedbycombobox.AutoCompleteCustomSource = MyCollection;
            designedbycombobox.DataSource = MyCollection;
        }

        private void filllastsavedby()
        {
            AutoCompleteStringCollection MyCollection = connectapi.filllastsavedby();
            lastsavedbycombo.AutoCompleteCustomSource = MyCollection;
            lastsavedbycombo.DataSource = MyCollection;
        }

        private void fillmanufacturers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.fillmanufacturers();
            oemitemcombobox.AutoCompleteCustomSource = MyCollection;
            oemitemcombobox.DataSource = MyCollection;

        }

        private void filloem()
        {
            AutoCompleteStringCollection MyCollection = connectapi.filloem();
            Manufactureritemcomboxbox.AutoCompleteCustomSource = MyCollection;
            Manufactureritemcomboxbox.DataSource = MyCollection;

        }

        private void fillfamilycodes()
        {
            AutoCompleteStringCollection MyCollection = connectapi.fillfamilycodes();
            familycomboxbox.AutoCompleteCustomSource = MyCollection;
            familycomboxbox.DataSource = MyCollection;
        }

        private void filluserwithblock()
        {
            AutoCompleteStringCollection MyCollection = connectapi.filluserwithblock();
            ActiveCadblockcombobox.AutoCompleteCustomSource = MyCollection;
            ActiveCadblockcombobox.DataSource = MyCollection;
        }

        private void FillMaterials()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillMaterials();
            MaterialcomboBox.AutoCompleteCustomSource = MyCollection;
            MaterialcomboBox.DataSource = MyCollection;
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

        #region notification ballon

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            int openforms = Application.OpenForms.Count;
            if (openforms > 2)
            {
                bool purchasereqopen = false;

                foreach (Form frm in Application.OpenForms)
                {

                    if (frm.Name.ToString() == "PurchaseReqform")
                    {
                        purchasereqopen = true;
                        frm.Show();
                        frm.Activate();
                        frm.BringToFront();
                        frm.Focus();
                        frm.WindowState = FormWindowState.Normal;
                    }

                }
                if (purchasereqopen)
                {

                }
                else
                {
                    PurchaseReqform purchaseReq = new PurchaseReqform();
                    purchaseReq.Show();
                }
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
            this.Focus();
            this.WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

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
                connectapicntrls.CheckAutoCad(ItemNo, description, family, Manufacturer, oem);
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


            if (family.ToString() == "ASEL" || family.ToString() == "AS" || family.ToString() == "ASPN")
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
                    int checkutocadforassy = connectapicntrls.CheckAutoCadforassy(ItemNo, description, Manufacturer, oem);

                    if (checkutocadforassy == 1)
                    {
                        openassytocatalog(ItemNo);
                    }
                    else if (checkutocadforassy == 3)
                    {
                        if (connectapicntrls.CheckAssyOnGenius(ItemNo))
                        {
                            createassycatalog(ItemNo, description, Manufacturer, oem, family);
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

        private void createassycatalog(string ItemNo, string description, string Manufacturer, string oem, string family)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                CreateAssyToCatalog treeView = new CreateAssyToCatalog();
                treeView.getallitems(ItemNo, description, family, Manufacturer, oem);
                treeView.ShowDialog();
            }

        }

        private void openassytocatalog(string ItemNo)
        {
            AutocadAssembly treeView = new AutocadAssembly();
            treeView.item(ItemNo);
            treeView.Show();
        }

        #endregion

        #region Controls ToolStrip

        private void addToCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRowInfo();
        }

        private void createAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRowInfoforassy();
        }

        private void autoCadCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openassytocatalog(getitemnumberselected().ToString());
        }

        private void geniusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processbom(getitemnumberselected().ToString());
        }

        private void autoCadCatalogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AutocadWhereUsed treeView = new AutocadWhereUsed();
            treeView.item(getitemnumberselected().ToString());
            treeView.Show();
        }

        private void geniusJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processwhereused(getitemnumberselected().ToString());
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "BOM");
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            prorcessreportbom(getitemnumberselected(), "SPAREPARTS");
        }

        private void eModelViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            emodelviewtoolstrp();
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


        #endregion

        private void getnewitembttn_Click(object sender, EventArgs e)
        {
            if (connectapi.validnumber(connectapi.getlastnumber()))
            {
                string newid = connectapi.spmnew_idincrement(connectapi.getlastnumber(), connectapi.getactiveblock()).ToString();
                MessageBox.Show("Your next ItemNumber to use is :- " + newid, "SPM Connect - New Item Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clipboard.SetText(newid);
            }
        }

        private void versionlabel_DoubleClick(object sender, EventArgs e)
        {
            this.Size = new Size(900, 750);
            this.CenterToScreen();
        }

        #region AddtoFavorites

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectapi.addtofavorites(getitemnumberselected());
        }

        private void showFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showfavorites();
        }

        private void showfavorites()
        {
            clearandhide();
            txtSearch.Clear();
            txtSearch.Focus();
            dt.Clear();
            dt = connectapi.ShowFavorites();
            dataGridView.DataSource = dt;
            DataView dv = dt.DefaultView;
            dataGridView.Sort(itemNumberDataGridViewTextBoxColumn, ListSortDirection.Descending);
            UpdateFont();
            showingfavorites = true;
            recordlabel.Text = "Showing " + dataGridView.Rows.Count + " favorite items.";
        }

        private void removeFromFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectapi.removefromfavorites(getitemnumberselected());
            showfavorites();
        }

        #endregion

        private void aboutbtn_Click(object sender, EventArgs e)
        {
            showhelp();
        }

        private void showhelp()
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        private void toolStripMenuupdateitem_Click(object sender, EventArgs e)
        {
            connectapicntrls.updateitempropertiesfromgenius(getitemnumberselected());

        }

        private void revelInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = makepathfordrag();
            if (!File.Exists(filePath))
            {
                return;
            }
            string argument = "/select, \"" + filePath + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to delete this file?", "SPM Connect - Delete File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string filePath = makepathfordrag();
                if (!File.Exists(filePath))
                {
                    return;
                }
                try
                {
                    File.Delete(filePath);
                    showfilesonlistview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }
    }

}