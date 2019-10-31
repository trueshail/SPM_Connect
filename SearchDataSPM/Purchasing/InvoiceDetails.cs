using SPMConnect.UserActionLog;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class InvoiceDetails : Form
    {
        #region Load Invoice Details and setting Parameters

        string connection;
        DataTable dt = new DataTable();
        SqlConnection cn;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        string Invoice_Number = "";
        string custvendor = "";
        string shiptoid = "";
        string soldtoid = "";
        bool formloading = false;
        SPMConnectAPI.Shipping connectapi = new Shipping();
        log4net.ILog log;
        private UserActions _userActions;
        ErrorHandler errorHandler = new ErrorHandler();

        public InvoiceDetails()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
            connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //connectapi.SPM_Connect();

            dt = new DataTable();
            _command = new SqlCommand();
        }

        public string invoicenumber(string number)
        {
            if (number.Length > 0)
                return Invoice_Number = number;
            return null;
        }

        public string setcustvendor(string gcustvend)
        {
            if (gcustvend.Length > 0)
                return custvendor = gcustvend;
            return null;
        }

        private void QuoteDetails_Load(object sender, EventArgs e)
        {
            formloading = true;
            this.Text = "Invoice Details - " + Invoice_Number;
            FillFobPoint();
            FillSalesPerson();
            FillCarriers();
            FillTerms();
            FillRequistioners();

            if (GetShippingBaseInfo(Invoice_Number))
            {
                FillShippingBaseInfo();
                processeditbutton();
                PopulateDataGridView(Invoice_Number);

            }

            formloading = false;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Shipping Invoice Detail " + Invoice_Number + " by " + System.Environment.UserName);
            _userActions = new UserActions(this);
        }

        private bool GetShippingBaseInfo(string invoicenumber)
        {
            bool fillled = false;
            string sql = "SELECT * FROM [SPM_Database].[dbo].[ShippingBase] WHERE InvoiceNo = '" + invoicenumber + "'";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                _adapter = new SqlDataAdapter(sql, cn);
                dt.Clear();
                _adapter.Fill(dt);

                fillled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect  - Shipping Get Shipping Base Info From SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }
            return fillled;
        }

        #endregion

        #region Fill information on controls

        private void FillShippingBaseInfo()
        {
            DataRow r = dt.Rows[0];

            invoicetxtbox.Text = r["InvoiceNo"].ToString();
            notestxt.Text = r["Notes"].ToString();
            jobtxt.Text = r["JobNumber"].ToString();


            Createdon.Text = "Created On : " + r["DateCreated"].ToString();

            CreatedBy.Text = "Created By : " + r["CreatedBy"].ToString();

            LastSavedOn.Text = "Last Saved By : " + r["LastSavedby"].ToString();

            LastSavedBy.Text = "Last Saved On : " + r["DateLastSaved"].ToString();



            string vendorcust = r["Vendor_Cust"].ToString();
            if (r["Vendor_Cust"].ToString() == "1")
            {
                VendorCust.Text = "Invoice For Customer";
                FillcustomersShipto();
                FillcustomersSoldto();
                custvendor = "1";

            }
            else
            {
                VendorCust.Text = "Invoice For Vendor";
                FillVendorsShipto();
                FillVendorsSoldto();
                custvendor = "0";
            }

            string Carrier = r["Carrier"].ToString();
            if (Carrier.Length > 0)
            {
                Carriercombox.SelectedItem = Carrier;
            }

            string terms = r["Terms"].ToString();

            if (terms.Length > 0)
            {
                Termscombobox.SelectedItem = terms;
            }

            string salesperson = r["SalesPerson"].ToString();

            if (salesperson.Length > 0)
            {
                Salespersoncombobox.SelectedItem = salesperson;
            }

            string Requistioner = r["Requistioner"].ToString();

            if (Requistioner.Length > 0)
            {
                requestcomboBox.SelectedItem = Requistioner;
            }

            string fobpoint = r["FobPoint"].ToString();

            if (fobpoint.Length > 0)
            {
                FOBPointcombox.SelectedItem = fobpoint;
            }

            string currency = r["Currency"].ToString().Trim();

            if (currency.Length > 0)
            {
                currencycombox.SelectedItem = currency;
            }

            soldtoid = r["SoldTo"].ToString();
            if (soldtoid.Length > 0)
            {
                FillSoldToInformation(soldtoid, vendorcust);
            }

            shiptoid = r["ShipTo"].ToString();

            if (shiptoid.Length > 0)
            {
                FillShipToInformation(shiptoid, vendorcust);
            }

            if (r["Collect_Prepaid"].ToString() == "1")
            {
                prepaidchkbox.Checked = true;
            }
            else
            {
                collectchkbox.Checked = true;
                carrriercodetxt.Text = r["CarrierCode"].ToString();
            }

        }

        private void FillSoldToInformation(string custid, string vendorcust)
        {
            if (vendorcust == "1")
            {
                DataTable dtsoldto = new DataTable();
                dtsoldto.Clear();
                dtsoldto = connectapi.GetCustomerSoldShipToInfo(custid);
                DataRow r = dtsoldto.Rows[0];

                sld2name.Text = r["Nom"].ToString();
                soldtocombobox.SelectedItem = r["Nom"].ToString();
                sld2add.Text = r["Adresse"].ToString();
                sld2add2.Text = r["CTR_Address2"].ToString();
                sld2city.Text = r["Ville"].ToString();
                sld2province.Text = r["Province"].ToString();
                sld2country.Text = r["Pays"].ToString();
                sld2zip.Text = r["Codepostal"].ToString();
                sld2phone.Text = r["Telephone"].ToString();
                sld2fax.Text = r["Fax"].ToString();
            }
            else
            {
                DataTable dtsoldto = new DataTable();
                dtsoldto.Clear();
                dtsoldto = connectapi.GetVendorShipSoldToInfo(custid);
                DataRow r = dtsoldto.Rows[0];

                sld2name.Text = r["Name"].ToString();
                soldtocombobox.SelectedItem = r["Name"].ToString();
                sld2add.Text = r["Address1"].ToString();
                sld2add2.Text = r["Address2"].ToString();
                sld2city.Text = r["City"].ToString();
                sld2province.Text = r["Province"].ToString();
                sld2country.Text = r["Country"].ToString();
                sld2zip.Text = r["ZipCode"].ToString();
                sld2phone.Text = r["Phone"].ToString();
                sld2fax.Text = r["Fax"].ToString();
            }


        }

        private void FillShipToInformation(string custid, string vendorcust)
        {
            if (vendorcust == "1")
            {
                DataTable dtsoldto = new DataTable();
                dtsoldto.Clear();
                dtsoldto = connectapi.GetCustomerSoldShipToInfo(custid);
                DataRow r = dtsoldto.Rows[0];

                ship2name.Text = r["Nom"].ToString();
                shiptocombobox.SelectedItem = r["Nom"].ToString();
                ship2add.Text = r["Adresse"].ToString();
                ship2add2.Text = r["CTR_Address2"].ToString();
                ship2city.Text = r["Ville"].ToString();
                ship2province.Text = r["Province"].ToString();
                ship2country.Text = r["Pays"].ToString();
                ship2zip.Text = r["Codepostal"].ToString();
                ship2phone.Text = r["Telephone"].ToString();
                ship2fax.Text = r["Fax"].ToString();
            }
            else
            {
                DataTable dtsoldto = new DataTable();
                dtsoldto.Clear();
                dtsoldto = connectapi.GetVendorShipSoldToInfo(custid);
                DataRow r = dtsoldto.Rows[0];

                ship2name.Text = r["Name"].ToString();
                shiptocombobox.SelectedItem = r["Name"].ToString();
                ship2add.Text = r["Address1"].ToString();
                ship2add2.Text = r["Address2"].ToString();
                ship2city.Text = r["City"].ToString();
                ship2province.Text = r["Province"].ToString();
                ship2country.Text = r["Country"].ToString();
                ship2zip.Text = r["ZipCode"].ToString();
                ship2phone.Text = r["Phone"].ToString();
                ship2fax.Text = r["Fax"].ToString();
            }


        }

        #endregion

        #region Filling Up Comboboxes

        private void FillcustomersSoldto()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillShipToSoldToCustomers();
            soldtocombobox.AutoCompleteCustomSource = MyCollection;
            soldtocombobox.DataSource = MyCollection;
        }

        private void FillcustomersShipto()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillShipToSoldToCustomers();
            shiptocombobox.AutoCompleteCustomSource = MyCollection;
            shiptocombobox.DataSource = MyCollection;
        }

        private void FillVendorsSoldto()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillShipToSoldToVendors();
            soldtocombobox.AutoCompleteCustomSource = MyCollection;
            soldtocombobox.DataSource = MyCollection;
        }

        private void FillVendorsShipto()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillShipToSoldToVendors();
            shiptocombobox.AutoCompleteCustomSource = MyCollection;
            shiptocombobox.DataSource = MyCollection;
        }

        private void FillFobPoint()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillFobPoint();
            FOBPointcombox.AutoCompleteCustomSource = MyCollection;
            FOBPointcombox.DataSource = MyCollection;
        }

        private void FillSalesPerson()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillSalesPersonShip();
            Salespersoncombobox.AutoCompleteCustomSource = MyCollection;
            Salespersoncombobox.DataSource = MyCollection;
        }

        private void FillTerms()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillTerms();
            Termscombobox.AutoCompleteCustomSource = MyCollection;
            Termscombobox.DataSource = MyCollection;
        }

        private void FillCarriers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillCarrierShip();
            Carriercombox.AutoCompleteCustomSource = MyCollection;
            Carriercombox.DataSource = MyCollection;
        }

        private void FillRequistioners()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillRequistioner();
            requestcomboBox.AutoCompleteCustomSource = MyCollection;
            requestcomboBox.DataSource = MyCollection;
        }

        #endregion

        #region shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();

                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                perfromsavebttn();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        #endregion

        #region DataGridView

        private bool PopulateDataGridView(string invoicenumber)
        {
            DataTable shippingitems = new DataTable();
            bool fillled = false;
            string sql = "SELECT * FROM [SPM_Database].[dbo].[ShippingItems] WHERE InvoiceNo = '" + invoicenumber + "' ORDER BY InvoiceNo,OrderId";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                _adapter = new SqlDataAdapter(sql, cn);
                shippingitems.Clear();
                _adapter.Fill(shippingitems);
                dataGridView1.DataSource = shippingitems;
                UpdateFont();
                calculatetotal();
                fillled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect  - Shipping Get Shipping Base Items From SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }
            return fillled;

        }

        private void UpdateFont()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 80;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].DefaultCellStyle.Format = "0.00##";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "0.00##";

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("TimesNewRoman", 9.0F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.Azure;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridView1.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[columnindex].Selected = true;
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                PrintToolStrip.Enabled = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!editbttn.Visible)
                updateitem();
        }

        #endregion

        #region FormClosing

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible == true)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect - Save Invoice Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connectapi.CheckoutInvoice(invoicetxtbox.Text.Trim());
                    this.Dispose();
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
            else
            {
                connectapi.CheckoutInvoice(invoicetxtbox.Text.Trim());
            }
        }

        #endregion

        #region Process Save

        private void perfromlockdown()
        {
            editbttn.Visible = true;
            savbttn.Enabled = false;
            savbttn.Visible = false;
            jobtxt.ReadOnly = true;
            carrriercodetxt.ReadOnly = true;
            notestxt.ReadOnly = true;
            FormSelector.Enabled = false;
            soldtogroupBox.Enabled = false;
            ShiptogroupBox.Enabled = false;
            shippinggroupBox.Enabled = false;
        }

        List<string> list = new List<string>();

        private void graballinfor()
        {
            list.Clear();
            Regex reg = new Regex("['\",_^]");
            list.Add(invoicetxtbox.Text);
            list.Add(reg.Replace(jobtxt.Text, "''"));
            list.Add(reg.Replace(Salespersoncombobox.Text, "''"));
            list.Add(reg.Replace(requestcomboBox.Text, "''"));
            list.Add(reg.Replace(Carriercombox.Text, "''"));
            list.Add(collectchkbox.Checked ? "0" : "1");
            list.Add(reg.Replace(FOBPointcombox.Text, "''"));
            list.Add(reg.Replace(Termscombobox.Text, "''"));
            list.Add(reg.Replace(currencycombox.Text, "''"));
            list.Add(totalvalue);
            list.Add(soldtoid);
            list.Add(shiptoid);
            list.Add(reg.Replace(notestxt.Text, "''"));
            list.Add(reg.Replace(carrriercodetxt.Text, "''"));

        }

        private void savbttn_Click(object sender, EventArgs e)
        {
            perfromsavebttn();
        }

        void perfromsavebttn()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            perfromlockdown();
            graballinfor();
            if (connectapi.UpdateInvoiceDetsToSql(list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(), list[10].ToString(), list[11].ToString(), list[12].ToString(), list[13].ToString()))
            {
                if (GetShippingBaseInfo(list[0].ToString()))
                {
                    FillShippingBaseInfo();
                    SaveReport(invoicetxtbox.Text);
                }
            }
            this.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        #endregion

        #region Process Edit

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        private void processeditbutton()
        {
            editbttn.Visible = false;
            savbttn.Enabled = true;
            savbttn.Visible = true;
            jobtxt.ReadOnly = false;
            notestxt.ReadOnly = false;
            carrriercodetxt.ReadOnly = false;
            FormSelector.Enabled = true;
            soldtogroupBox.Enabled = true;
            ShiptogroupBox.Enabled = true;
            shippinggroupBox.Enabled = true;
        }

        #endregion

        #region Calculate Total

        string totalvalue = "";

        void calculatetotal()
        {
            totalvalue = "";
            if (dataGridView1.Rows.Count > 0)
            {
                decimal total = 0.00m;
                decimal price = 0.00m;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        if (row.Cells[8].Value.ToString() != null && row.Cells[8].Value.ToString().Length > 0)
                        {
                            price = Convert.ToDecimal(row.Cells[8].Value.ToString());
                        }
                        else
                        {
                            price = 0;
                        }
                        total += price;
                        totalcostlbl.Text = "Total Cost : $" + string.Format("{0:n}", Convert.ToDecimal(total.ToString()));
                        totalvalue = string.Format("{0:#.00}", total.ToString());
                    }

                    catch (Exception ex)
                    {
                        MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect -  Error Getting Total", MessageBoxButtons.OK);
                    }


                }
            }
            else
            {
                totalcostlbl.Text = "";
            }

        }

        #endregion

        #region Events

        private void collectchkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (collectchkbox.Checked)
            {
                prepaidchkbox.Checked = false;
                carriercodelbl.Visible = true;
                carrriercodetxt.Visible = true;
                carrriercodetxt.ReadOnly = false;
            }
            else
            {
                prepaidchkbox.Checked = true;
                carriercodelbl.Visible = false;
                carrriercodetxt.Visible = false;
                carrriercodetxt.ReadOnly = true;
                carrriercodetxt.Text = "";
            }
        }

        private void prepaidchkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (prepaidchkbox.Checked)
            {
                collectchkbox.Checked = false;
            }
            else
            {
                collectchkbox.Checked = true;
            }
        }

        private void soldtocombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                string soldtoname = soldtocombobox.Text;

                if (custvendor == "1")
                {
                    DataTable dtsoldto = new DataTable();
                    dtsoldto.Clear();
                    dtsoldto = connectapi.GetCustomerSoldShipToInfoname(soldtoname);
                    DataRow r = dtsoldto.Rows[0];
                    sld2name.Text = r["Nom"].ToString();
                    soldtocombobox.SelectedItem = r["Nom"].ToString();
                    sld2add.Text = r["Adresse"].ToString();
                    sld2city.Text = r["Ville"].ToString();
                    sld2province.Text = r["Province"].ToString();
                    sld2country.Text = r["Pays"].ToString();
                    sld2zip.Text = r["Codepostal"].ToString();
                    sld2phone.Text = r["Telephone"].ToString();
                    sld2fax.Text = r["Fax"].ToString();
                    soldtoid = r["C_No"].ToString();
                }
                else
                {
                    DataTable dtsoldto = new DataTable();
                    dtsoldto.Clear();
                    dtsoldto = connectapi.GetVendorShipSoldToInfoname(soldtoname);
                    DataRow r = dtsoldto.Rows[0];

                    sld2name.Text = r["Name"].ToString();
                    soldtocombobox.SelectedItem = r["Name"].ToString();
                    sld2add.Text = r["Address1"].ToString();
                    sld2city.Text = r["City"].ToString();
                    sld2province.Text = r["Province"].ToString();
                    sld2country.Text = r["Country"].ToString();
                    sld2zip.Text = r["ZipCode"].ToString();
                    sld2phone.Text = r["Phone"].ToString();
                    sld2fax.Text = r["Fax"].ToString();
                    soldtoid = r["Code"].ToString();
                }
            }

        }

        private void shiptocombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                string shiptoname = shiptocombobox.Text;
                if (custvendor == "1")
                {
                    DataTable dtsoldto = new DataTable();
                    dtsoldto.Clear();
                    dtsoldto = connectapi.GetCustomerSoldShipToInfoname(shiptoname);
                    DataRow r = dtsoldto.Rows[0];

                    ship2name.Text = r["Nom"].ToString();
                    shiptocombobox.SelectedItem = r["Nom"].ToString();
                    ship2add.Text = r["Adresse"].ToString();
                    ship2city.Text = r["Ville"].ToString();
                    ship2province.Text = r["Province"].ToString();
                    ship2country.Text = r["Pays"].ToString();
                    ship2zip.Text = r["Codepostal"].ToString();
                    ship2phone.Text = r["Telephone"].ToString();
                    ship2fax.Text = r["Fax"].ToString();
                    shiptoid = r["C_No"].ToString();
                }
                else
                {
                    DataTable dtsoldto = new DataTable();
                    dtsoldto.Clear();
                    dtsoldto = connectapi.GetVendorShipSoldToInfoname(shiptoname);
                    DataRow r = dtsoldto.Rows[0];

                    ship2name.Text = r["Name"].ToString();
                    shiptocombobox.SelectedItem = r["Name"].ToString();
                    ship2add.Text = r["Address1"].ToString();
                    ship2city.Text = r["City"].ToString();
                    ship2province.Text = r["Province"].ToString();
                    ship2country.Text = r["Country"].ToString();
                    ship2zip.Text = r["ZipCode"].ToString();
                    ship2phone.Text = r["Phone"].ToString();
                    ship2fax.Text = r["Fax"].ToString();
                    shiptoid = r["Code"].ToString();
                }
            }

        }

        #endregion

        #region ContextMenuStrip

        private void FormSelector_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FormSelector.Items[1].Visible = true;
                FormSelector.Items[1].Enabled = true;
                FormSelector.Items[2].Visible = true;
                FormSelector.Items[2].Enabled = true;

            }
            else
            {
                FormSelector.Items[1].Enabled = false;
                FormSelector.Items[1].Visible = false;
                FormSelector.Items[2].Enabled = false;
                FormSelector.Items[2].Visible = false;
            }
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateitem();
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (InvoiceAddItem invoiceAddItem = new InvoiceAddItem())
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                invoiceAddItem.invoicenumber(invoicetxtbox.Text);
                invoiceAddItem.command("Add");
                invoiceAddItem.setcustvendor(custvendor);
                invoiceAddItem.ShowDialog();
                invoiceAddItem.Dispose();
                connectapi.UpdateShippingItemsOrderId(Invoice_Number);
                PopulateDataGridView(Invoice_Number);
                this.Enabled = true;
                this.Show();
                this.Activate();
                this.Focus();
            }
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectapi.DeleteItemFromInvoice(invoicetxtbox.Text, getselecteditemnumber()))
            {
                connectapi.UpdateShippingItemsOrderId(Invoice_Number);
                if (custvendor == "1")
                {
                    connectapi.UpdateShippingItemIdCopy(Invoice_Number);
                }
                PopulateDataGridView(Invoice_Number);

            }
        }

        #endregion

        #region Perform Update

        void updateitem()
        {
            using (InvoiceAddItem invoiceAddItem = new InvoiceAddItem())
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                invoiceAddItem.invoicenumber(invoicetxtbox.Text);
                invoiceAddItem.itemnumber(getselecteditemnumber());
                invoiceAddItem.command("Update");
                invoiceAddItem.setcustvendor(custvendor);
                invoiceAddItem.ShowDialog();
                invoiceAddItem.Dispose();
                connectapi.UpdateShippingItemsOrderId(Invoice_Number);
                PopulateDataGridView(Invoice_Number);
                this.Enabled = true;
                this.Show();
                this.Activate();
                this.Focus();
            }

        }

        #endregion

        private string getselecteditemnumber()
        {
            string item;
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[2].Value);
                return item;
            }
            else
            {
                item = "";
                return item;
            }
        }

        #region Print Reports

        private void print1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(invoicetxtbox.Text);
            form1.getreport("ShippingInvPack");
            form1.Show();
        }

        private void print2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(invoicetxtbox.Text);
            form1.getreport("ShippingInvCom");
            form1.Show();
        }

        #endregion

        #region Save Report

        private void SaveReport(string reqno)
        {
            string fileName = "";
            string filepath = connectapi.getsharesfolder() + @"\SPM_Connect\ShippingInvoices\";
            System.IO.Directory.CreateDirectory(filepath);
            fileName = filepath + reqno + " - CI.pdf";
            filepath += reqno + " - PL.pdf";
            SaveReport(reqno, fileName);
            SaveReport(reqno, filepath);

        }

        public void SaveReport(string invoiceno, string fileName)
        {

            RS2005.ReportingService2005 rs;
            RE2005.ReportExecutionService rsExec;

            // Create a new proxy to the web service
            rs = new RS2005.ReportingService2005();
            rsExec = new RE2005.ReportExecutionService();

            // Authenticate to the Web service using Windows credentials
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.Url = "http://spm-sql/reportserver/reportservice2005.asmx";
            rsExec.Url = "http://spm-sql/reportserver/reportexecution2005.asmx";

            string historyID = null;
            string deviceInfo = null;
            string format = "PDF";
            Byte[] results;
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            RE2005.Warning[] warnings = null;
            string[] streamIDs = null;
            string _reportName = "";
            if (fileName.Substring(fileName.Length - 6) == "CI.pdf")
            {
                _reportName = @"/GeniusReports/PurchaseOrder/SPM_ShippingInvoice";
            }
            else
            {
                _reportName = @"/GeniusReports/PurchaseOrder/SPM_ShippingInvoicePacking";
            }
            string _historyID = null;
            bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            RS2005.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                RE2005.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);
                RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];

                if (_parameters.Length > 0)
                {
                    parameters[0] = new RE2005.ParameterValue
                    {
                        //parameters[0].Label = "";
                        Name = "pInvno",
                        Value = invoiceno
                    };
                }
                rsExec.SetExecutionParameters(parameters, "en-us");

                results = rsExec.Render(format, deviceInfo,
                          out extension, out encoding,
                          out mimeType, out warnings, out streamIDs);

                try
                {

                    File.WriteAllBytes(fileName, results);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "SPM Connect - Save Report", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        #endregion

        private void Createdon_Click(object sender, EventArgs e)
        {
            if (editbttn.Visible)
            {
                string pdate = "";
                General.InvoiceDateChange pODetails = new SearchDataSPM.General.InvoiceDateChange();

                if (pODetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pdate = pODetails.podate;
                }

                if (connectapi.UpdateInvoiceDateCreatedToSql(invoicetxtbox.Text, pdate))
                {
                    if (GetShippingBaseInfo(invoicetxtbox.Text))
                    {
                        FillShippingBaseInfo();
                        SaveReport(invoicetxtbox.Text);
                    }
                }

            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Please save the invoice first in order to change date created.", "SPM Connect - Save Invoice Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void InvoiceDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            _userActions.FinishLoggingUserActions(this);
            log.Info("Closed Shipping Invoice Detail " + Invoice_Number + " by " + System.Environment.UserName);
            this.Dispose();
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, _userActions, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, _userActions, this);
        }
    }
}