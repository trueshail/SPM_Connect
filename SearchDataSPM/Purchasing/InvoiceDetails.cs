using SearchDataSPM.Miscellaneous;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SearchDataSPM
{
    public partial class InvoiceDetails : Form
    {
        #region Load Invoice Details and setting Parameters

        private readonly SPMConnectAPI.Shipping connectapi = new Shipping();
        private readonly DataTable dt = new DataTable();
        private readonly string Invoice_Number = "";
        private string createdbyname = "";
        private string custvendor = "";
        private DataTable dtsoldtoCust = new DataTable();
        private DataTable dtsoldtoVend = new DataTable();
        private bool shipInvCreator;
        private bool formloading;
        private log4net.ILog log;
        private string shiptoid = "";
        private string soldtoid = "";
        private bool splashWorkDone;

        public InvoiceDetails(string number)
        {
            InitializeComponent();
            dt = new DataTable();
            dtsoldtoCust = new DataTable();
            dtsoldtoVend = new DataTable();
            this.Invoice_Number = number;
        }

        private bool GetShippingBaseInfo(string invoicenumber)
        {
            bool fillled = false;
            string sql = "SELECT * FROM [SPM_Database].[dbo].[ShippingBase] WHERE InvoiceNo = '" + invoicenumber + "'";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlDataAdapter _adapter = new SqlDataAdapter(sql, connectapi.cn);
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
                connectapi.cn.Close();
            }
            return fillled;
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
            dtsoldtoCust = connectapi.GetCustomerSoldShipToData();
            dtsoldtoVend = connectapi.GetVendorShipSoldToData();

            if (GetShippingBaseInfo(Invoice_Number))
            {
                // processeditbutton();
                FillShippingBaseInfo();
                PopulateDataGridView(Invoice_Number);
                Perfromlockdown();
            }
            formloading = false;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Shipping Invoice Detail " + Invoice_Number + " ");
        }

        #endregion Load Invoice Details and setting Parameters

        #region Fill information on controls

        private void FillShippingBaseInfo()
        {
            DataRow r = dt.Rows[0];

            invoicetxtbox.Text = r["InvoiceNo"].ToString();
            notestxt.Text = r["Notes"].ToString();
            jobtxt.Text = r["JobNumber"].ToString();

            Createdon.Text = "Created On : " + r["DateCreated"].ToString();

            CreatedBy.Text = "Created By : " + r["CreatedBy"].ToString();

            string invoiceCreatedBy = r["CreatedBy"].ToString();

            createdbyname = invoiceCreatedBy;

            if (invoiceCreatedBy == ConnectUser.Name)
            {
                shipInvCreator = true;
            }

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

            string submittedtosup = r["IsSubmitted"].ToString();
            string submittedtomanager = r["IsApproved"].ToString();
            string ecrcomplete = r["IsShipped"].ToString();

            HandleCheckBoxes(submittedtosup, submittedtomanager, ecrcomplete, invoiceCreatedBy, r["ApprovedBy"].ToString(),
               r["ShippedBy"].ToString(), r["SubmittedOn"].ToString(), r["ApprovedOn"].ToString(), r["ShippedOn"].ToString());

            CheckEditButtonRights(Convert.ToInt32(r["SubmittedTo"].ToString()) == ConnectUser.ConnectId);
        }

        private void FillShipToInformation(string custid, string vendorcust)
        {
            if (vendorcust == "1")
            {
                string searchExpression = "C_No = '" + custid + "'";
                DataRow[] foundRows = dtsoldtoCust.Select(searchExpression);
                if (foundRows.Length > 0)
                {
                    DataRow r = foundRows[0];

                    ship2name.Text = r["Nom"].ToString();
                    shiptocombobox.Text = r["Nom"].ToString();
                    ship2add.Text = r["Adresse"].ToString();
                    ship2add2.Text = r["CTR_Address2"].ToString();
                    ship2city.Text = r["Ville"].ToString();
                    ship2province.Text = r["Province"].ToString();
                    ship2country.Text = r["Pays"].ToString();
                    ship2zip.Text = r["Codepostal"].ToString();
                    ship2phone.Text = r["Telephone"].ToString();
                    ship2fax.Text = r["Fax"].ToString();
                }
            }
            else
            {
                string searchExpression = "Code = '" + custid + "'";
                DataRow[] foundRows = dtsoldtoVend.Select(searchExpression);
                if (foundRows.Length > 0)
                {
                    DataRow r = foundRows[0];

                    ship2name.Text = r["Name"].ToString();
                    shiptocombobox.Text = r["Name"].ToString();
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
        }

        private void FillSoldToInformation(string custid, string vendorcust)
        {
            if (vendorcust == "1")
            {
                string searchExpression = "C_No = '" + custid + "'";
                DataRow[] foundRows = dtsoldtoCust.Select(searchExpression);
                if (foundRows.Length > 0)
                {
                    DataRow r = foundRows[0];

                    sld2name.Text = r["Nom"].ToString();
                    soldtocombobox.Text = r["Nom"].ToString();
                    sld2add.Text = r["Adresse"].ToString();
                    sld2add2.Text = r["CTR_Address2"].ToString();
                    sld2city.Text = r["Ville"].ToString();
                    sld2province.Text = r["Province"].ToString();
                    sld2country.Text = r["Pays"].ToString();
                    sld2zip.Text = r["Codepostal"].ToString();
                    sld2phone.Text = r["Telephone"].ToString();
                    sld2fax.Text = r["Fax"].ToString();
                }
            }
            else
            {
                string searchExpression = "Code = '" + custid + "'";
                DataRow[] foundRows = dtsoldtoVend.Select(searchExpression);
                if (foundRows.Length > 0)
                {
                    DataRow r = foundRows[0];

                    sld2name.Text = r["Name"].ToString();
                    soldtocombobox.Text = r["Name"].ToString();
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
        }

        #endregion Fill information on controls

        #region Filling Up Comboboxes

        private void FillCarriers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillCarrierShip();
            Carriercombox.AutoCompleteCustomSource = MyCollection;
            Carriercombox.DataSource = MyCollection;
        }

        private void FillcustomersShipto()
        {
            shiptocombobox.BindingContext = new BindingContext();   //create a new context
            shiptocombobox.DataSource = dtsoldtoCust;
            shiptocombobox.DisplayMember = "Nom";
            shiptocombobox.ValueMember = "C_No";

            foreach (DataRow r in dtsoldtoCust.Rows)
            {
                string rw = r["Nom"].ToString();
                shiptocombobox.AutoCompleteCustomSource.Add(rw);
            }
        }

        private void FillcustomersSoldto()
        {
            soldtocombobox.BindingContext = new BindingContext();   //create a new context
            soldtocombobox.DataSource = dtsoldtoCust;
            soldtocombobox.DisplayMember = "Nom";
            soldtocombobox.ValueMember = "C_No";

            foreach (DataRow r in dtsoldtoCust.Rows)
            {
                string rw = r["Nom"].ToString();
                soldtocombobox.AutoCompleteCustomSource.Add(rw);
            }
        }

        private void FillFobPoint()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillFobPoint();
            FOBPointcombox.AutoCompleteCustomSource = MyCollection;
            FOBPointcombox.DataSource = MyCollection;
        }

        private void FillRequistioners()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillRequistioner();
            requestcomboBox.AutoCompleteCustomSource = MyCollection;
            requestcomboBox.DataSource = MyCollection;
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

        private void FillVendorsShipto()
        {
            shiptocombobox.BindingContext = new BindingContext();   //create a new context
            shiptocombobox.DataSource = dtsoldtoVend;
            shiptocombobox.DisplayMember = "Name";
            shiptocombobox.ValueMember = "Code";

            foreach (DataRow r in dtsoldtoVend.Rows)
            {
                string rw = r["Name"].ToString();
                shiptocombobox.AutoCompleteCustomSource.Add(rw);
            }
        }

        private void FillVendorsSoldto()
        {
            soldtocombobox.BindingContext = new BindingContext();   //create a new context
            soldtocombobox.DataSource = dtsoldtoVend;
            soldtocombobox.DisplayMember = "Name";
            soldtocombobox.ValueMember = "Code";

            foreach (DataRow r in dtsoldtoVend.Rows)
            {
                string rw = r["Name"].ToString();
                soldtocombobox.AutoCompleteCustomSource.Add(rw);
            }
        }

        #endregion Filling Up Comboboxes

        #region shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region DataGridView

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!editbttn.Visible)
                Updateitem();
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            _ = dataGridView1.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[columnindex].Selected = true;
            }
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                PrintToolStrip.Enabled = true;
        }

        private bool PopulateDataGridView(string invoicenumber)
        {
            DataTable shippingitems = new DataTable();
            bool fillled = false;
            string sql = "SELECT * FROM [SPM_Database].[dbo].[ShippingItems] WHERE InvoiceNo = '" + invoicenumber + "' ORDER BY InvoiceNo,OrderId";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlDataAdapter _adapter = new SqlDataAdapter(sql, connectapi.cn);
                shippingitems.Clear();
                _adapter.Fill(shippingitems);
                dataGridView1.DataSource = shippingitems;
                UpdateFont();
                Calculatetotal();
                fillled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect  - Shipping Get Shipping Base Items From SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
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

        #endregion DataGridView

        #region FormClosing

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect - Save Invoice Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connectapi.CheckoutInvoice(invoicetxtbox.Text.Trim(), CheckInModules.ShipInv);
                    this.Dispose();
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
            else
            {
                connectapi.CheckoutInvoice(invoicetxtbox.Text.Trim(), CheckInModules.ShipInv);
            }
        }

        #endregion FormClosing

        #region Process Save

        private readonly List<string> list = new List<string>();

        private void Graballinfor()
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

        private void Perfromlockdown()
        {
            //editbttn.Visible = true;
            savbttn.Enabled = false;
            savbttn.Visible = false;
            jobtxt.ReadOnly = true;
            carrriercodetxt.ReadOnly = true;
            notestxt.ReadOnly = true;
            FormSelector.Enabled = false;
            soldtogroupBox.Enabled = false;
            ShiptogroupBox.Enabled = false;
            shippinggroupBox.Enabled = false;
            submissiongroupBox.Enabled = false;
        }

        private async Task Perfromsavebttn(string typeofsave)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            await Task.Run(() => SplashDialog("Saving Data...")).ConfigureAwait(true);
            Perfromlockdown();
            Graballinfor();
            SaveReport(invoicetxtbox.Text);
            if (connectapi.UpdateInvoiceDetsToSql(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13]))
            {
                if (typeofsave != "normal")
                {
                    connectapi.UpdateInvoiceDetsToSqlforAuthorisation(list[0], typeofsave, ConnectUser.ShipSup);
                }
                if (GetShippingBaseInfo(list[0]))
                {
                    FillShippingBaseInfo();
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Error occured while saving data.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (GetShippingBaseInfo(list[0]))
                {
                    FillShippingBaseInfo();
                    SaveReport(invoicetxtbox.Text);
                }
            }
            splashWorkDone = true;
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private async void Savbttn_Click(object sender, EventArgs e)
        {
            await Perfromsavebttn("normal").ConfigureAwait(false);
        }

        private void SplashDialog(string message)
        {
            splashWorkDone = false;
            ThreadPool.QueueUserWorkItem((_) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + ((this.Width - splashForm.Width) / 2), this.Location.Y + ((this.Height - splashForm.Height) / 2));
                    splashForm.Show();
                    while (!splashWorkDone)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
        }

        #endregion Process Save

        #region Process Edit

        private void Editbttn_Click(object sender, EventArgs e)
        {
            Processeditbutton();
        }

        private void Processeditbutton()
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
            submissiongroupBox.Enabled = true;
        }

        #endregion Process Edit

        #region Calculate Total

        private string totalvalue = "";

        private void Calculatetotal()
        {
            totalvalue = "";
            if (dataGridView1.Rows.Count > 0)
            {
                decimal total = 0.00m;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        decimal price = !string.IsNullOrEmpty(row.Cells[8].Value.ToString()) ? Convert.ToDecimal(row.Cells[8].Value.ToString()) : 0;
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

        #endregion Calculate Total

        #region Events

        private void Carriercombox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Carriercombox.Focus();
            }
        }

        private void Collectchkbox_CheckedChanged(object sender, EventArgs e)
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

        private void FOBPointcombox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FOBPointcombox.Focus();
            }
        }

        private void Prepaidchkbox_CheckedChanged(object sender, EventArgs e)
        {
            collectchkbox.Checked = !prepaidchkbox.Checked;
        }

        private void RequestcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                requestcomboBox.Focus();
            }
        }

        private void Salespersoncombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Salespersoncombobox.Focus();
            }
        }

        private void Shiptocombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                shiptocombobox.Focus();
            }
        }

        private void Shiptocombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                if (custvendor == "1")
                {
                    if (dtsoldtoCust.Rows.Count > 0)
                    {
                        ship2name.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Nom"].ToString();
                        shiptocombobox.SelectedItem = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Nom"].ToString();
                        ship2add.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Adresse"].ToString();
                        ship2city.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Ville"].ToString();
                        ship2province.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Province"].ToString();
                        ship2country.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Pays"].ToString();
                        ship2zip.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Codepostal"].ToString();
                        ship2phone.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Telephone"].ToString();
                        ship2fax.Text = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["Fax"].ToString();
                        shiptoid = dtsoldtoCust.Rows[shiptocombobox.SelectedIndex]["C_No"].ToString();
                    }
                }
                else
                {
                    if (dtsoldtoVend.Rows.Count > 0)
                    {
                        ship2name.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Name"].ToString();
                        shiptocombobox.SelectedItem = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Name"].ToString();
                        ship2add.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Address1"].ToString();
                        ship2city.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["City"].ToString();
                        ship2province.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Province"].ToString();
                        ship2country.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Country"].ToString();
                        ship2zip.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["ZipCode"].ToString();
                        ship2phone.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Phone"].ToString();
                        ship2fax.Text = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Fax"].ToString();
                        shiptoid = dtsoldtoVend.Rows[shiptocombobox.SelectedIndex]["Code"].ToString();
                    }
                }
            }
        }

        private void Soldtocombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                soldtocombobox.Focus();
            }
        }

        private void Soldtocombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (custvendor == "1")
            {
                if (dtsoldtoCust.Rows.Count > 0)
                {
                    sld2name.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Nom"].ToString();
                    soldtocombobox.SelectedItem = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Nom"].ToString();
                    sld2add.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Adresse"].ToString();
                    sld2city.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Ville"].ToString();
                    sld2province.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Province"].ToString();
                    sld2country.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Pays"].ToString();
                    sld2zip.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Codepostal"].ToString();
                    sld2phone.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Telephone"].ToString();
                    sld2fax.Text = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["Fax"].ToString();
                    soldtoid = dtsoldtoCust.Rows[soldtocombobox.SelectedIndex]["C_No"].ToString();
                }
            }
            else
            {
                if (dtsoldtoVend.Rows.Count > 0)
                {
                    sld2name.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Name"].ToString();
                    soldtocombobox.SelectedItem = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Name"].ToString();
                    sld2add.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Address1"].ToString();
                    sld2city.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["City"].ToString();
                    sld2province.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Province"].ToString();
                    sld2country.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Country"].ToString();
                    sld2zip.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["ZipCode"].ToString();
                    sld2phone.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Phone"].ToString();
                    sld2fax.Text = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Fax"].ToString();
                    soldtoid = dtsoldtoVend.Rows[soldtocombobox.SelectedIndex]["Code"].ToString();
                }
            }
        }

        private void Termscombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Termscombobox.Focus();
            }
        }

        #endregion Events

        #region ContextMenuStrip

        private void AddItemToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void DeleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectapi.DeleteItemFromInvoice(invoicetxtbox.Text, Getselecteditemnumber()))
            {
                connectapi.UpdateShippingItemsOrderId(Invoice_Number);
                if (custvendor == "1")
                {
                    connectapi.UpdateShippingItemIdCopy(Invoice_Number);
                }
                PopulateDataGridView(Invoice_Number);
            }
        }

        private void EditItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Updateitem();
        }

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

        #endregion ContextMenuStrip

        #region Perform Update

        private void Updateitem()
        {
            using (InvoiceAddItem invoiceAddItem = new InvoiceAddItem())
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                invoiceAddItem.invoicenumber(invoicetxtbox.Text);
                invoiceAddItem.itemnumber(Getselecteditemnumber());
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

        #endregion Perform Update

        private void CheckEditButtonRights(bool mine)
        {
            if (shipInvCreator && !shipsupervisorheckBox.Checked)
            {
                supcheckBox.Enabled = true;
                editbttn.Visible = true;
            }
            else
            {
                supcheckBox.Enabled = false;
                editbttn.Visible = false;
            }

            if (ConnectUser.ShipSupervisor && mine && supcheckBox.Checked && !shipmanagercheckBox.Checked)
            {
                shipsupervisorheckBox.Enabled = true;
                editbttn.Visible = true;
            }
            else
            {
                shipsupervisorheckBox.Enabled = false;
            }

            if (ConnectUser.ShippingManager && shipsupervisorheckBox.Checked)
            {
                shipmanagercheckBox.Enabled = true;
                editbttn.Visible = true;
                if (mine && supcheckBox.Checked && !shipmanagercheckBox.Checked)
                    shipsupervisorheckBox.Enabled = true;
            }
            else
            {
                if (supcheckBox.Checked)
                    shipsupervisorheckBox.Enabled = ConnectUser.ShipSupervisor;

                if (ConnectUser.ShippingManager && supcheckBox.Checked)
                {
                    shipsupervisorheckBox.Enabled = true;
                }
                shipmanagercheckBox.Enabled = false;
            }

            if (shipmanagercheckBox.Checked && !ConnectUser.ShippingManager)
            {
                Perfromlockdown();
                editbttn.Visible = false;
            }
        }

        private void Createdon_Click(object sender, EventArgs e)
        {
            if (editbttn.Visible)
            {
                string pdate = "";
                General.InvoiceDateChange pODetails = new SearchDataSPM.General.InvoiceDateChange();

                if (pODetails.ShowDialog() == DialogResult.OK)
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

        private async void EcrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (!shipmanagercheckBox.Checked)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this shipping request not completed?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    shipmanagercheckBox.Checked = false;
                    shipmanagercheckBox.Text = "Mark Shipping Request Complete";
                    await Perfromsavebttn("CompletedFalse").ConfigureAwait(false);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);
                }
                else
                {
                    shipmanagercheckBox.Checked = true;
                }
            }
            else
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this shipping request as complete?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to associated people with this shipping request.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    shipmanagercheckBox.Text = "Completed";
                    await Perfromsavebttn("Completed").ConfigureAwait(false);
                    Preparetosendemail("Completed");
                }
                else
                {
                    shipmanagercheckBox.Checked = false;
                }
            }
        }

        private string Getselecteditemnumber()
        {
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];
                return Convert.ToString(slectedrow.Cells[2].Value);
            }
            else
            {
                return "";
            }
        }

        #region Print Reports

        private void Print1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer("ShippingInvPack", invoicetxtbox.Text);
            form1.Show();
        }

        private void Print2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer("ShippingInvCom", invoicetxtbox.Text);
            form1.Show();
        }

        #endregion Print Reports

        #region Save Report

        public void SaveReport(string invoiceno, string fileName)
        {
            string _reportName = fileName.Substring(fileName.Length - 6) == "CI.pdf"
                ? "/GeniusReports/PurchaseOrder/SPM_ShippingInvoice"
                : "/GeniusReports/PurchaseOrder/SPM_ShippingInvoicePacking";

            RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];
            parameters[0] = new RE2005.ParameterValue
            {
                Name = "pInvno",
                Value = invoiceno
            };

            ReportHelper.SaveReport(fileName, _reportName, parameters);
        }

        private void SaveReport(string reqno)
        {
            string filepath = ConnectUser.SharesFolder + @"\SPM_Connect\ShippingInvoices\";
            Directory.CreateDirectory(filepath);
            string fileName = filepath + reqno + " - CI.pdf";
            filepath += reqno + " - PL.pdf";
            SaveReport(reqno, fileName);
            SaveReport(reqno, filepath);
        }

        #endregion Save Report

        private void HandleCheckBoxes(string submittedtosup, string sumbittedtomanager, string complete,
            string createdby, string approvedby, string completedby, string submittedon, string SupApprovedOn, string completedon)
        {
            if (submittedtosup == "1")
            {
                supcheckBox.Checked = true;
                supcheckBox.Text = "Submitted by " + createdby + " on " + submittedon + "";
            }
            else
            {
                supcheckBox.Checked = false;
                supcheckBox.Text = "Submit to Supervisor";
            }

            if (sumbittedtomanager == "1")
            {
                shipsupervisorheckBox.Checked = true;
                shipsupervisorheckBox.Text = "Approved by " + approvedby + " on " + SupApprovedOn + "";
            }
            else
            {
                shipsupervisorheckBox.Checked = false;
                shipsupervisorheckBox.Text = "Submit to Shipping Manager";
            }

            if (complete == "1")
            {
                shipmanagercheckBox.Checked = true;
                shipmanagercheckBox.Text = "Completed by " + completedby + " on " + completedon + "";
            }
            else
            {
                shipmanagercheckBox.Checked = false;
                shipmanagercheckBox.Text = "Mark Shipping Request Complete";
            }
        }

        private void InvoiceDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Shipping Invoice Detail " + Invoice_Number + " ");
            this.Dispose();
        }

        private async void ManagercheckBox_Click(object sender, EventArgs e)
        {
            if (!shipsupervisorheckBox.Checked)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this shipping request from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    shipsupervisorheckBox.Checked = false;
                    shipsupervisorheckBox.Text = "Submit to Shipping Manager";
                    await Perfromsavebttn("SupSubmitFalse").ConfigureAwait(false);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);
                }
                else
                {
                    shipsupervisorheckBox.Checked = true;
                }
            }
            else
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this shipping request for approval?" + Environment.NewLine +
                    "This will send an email to Shipping manager for approval.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    shipsupervisorheckBox.Text = "Submitted to Shipping Manager";
                    await Perfromsavebttn("SupSubmit").ConfigureAwait(false);
                    Preparetosendemail("SupSubmit");
                }
                else
                {
                    shipsupervisorheckBox.Checked = false;
                }
            }
        }

        private void Preparetosendemail(string typeofSave)
        {
            string filepath = ConnectUser.SharesFolder + @"\SPM_Connect\ShippingInvoices\";
            string fileName = filepath + invoicetxtbox.Text + " - CI.pdf";
            _ = invoicetxtbox.Text + " - PL.pdf";
            if (typeofSave == "Submitted")
            {
                Sendemailtosupervisor(fileName);
            }
            else if (typeofSave == "SupSubmit")
            {
                //send email to manager and cc requestedby
                Sendemailtouser(fileName, "supervisor");
                SendemailtoManager(fileName);
            }
            else if (typeofSave == "Completed")
            {
                // send email to user and cc manager and supervisor
                Sendemailtouser(fileName, "manager");
            }
        }

        private void Sendemail(string emailtosend, string subject, string name, string body, string filetoattach, string cc, string extracc)
        {
            if (Sendemailyesno())
            {
                connectapi.TriggerEmail(emailtosend, subject, name, body, filetoattach, cc, extracc, "Normal");
            }
            else
            {
                MessageBox.Show("Email turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SendemailtoManager(string fileName)
        {
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.ShippingManager, "1"))
                Sendemail(item.email, invoicetxtbox.Text.Trim() + " Shipment to be shipped", item.name, Environment.NewLine + ConnectUser.Name + " sent this shipping request for shipping.", fileName, "", "");
        }

        private void Sendemailtosupervisor(string fileName)
        {
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.ShipSup, ConnectUser.ShipSup.ToString()))
                Sendemail(item.email, invoicetxtbox.Text + " Shipping Request Approval Required", item.name, Environment.NewLine + ConnectUser.Name + " sent this shipping request for approval.", fileName, "", "");
        }

        private void Sendemailtouser(string fileName, string triggerby)
        {
            DataRow r = dt.Rows[0];
            string userreqemail = connectapi.GetNameEmailByParaValue(UserFields.Name, createdbyname)[0].email;
            if (string.IsNullOrEmpty(userreqemail))
            {
                MessageBox.Show("Email not found for user. Cannot notify the user.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (triggerby == "supervisor")
            {
                Sendemail(userreqemail, invoicetxtbox.Text + " Shipping Request Approved ", createdbyname, Environment.NewLine + " Your shipping request is approved and submmited to shipping manager.", fileName, "", "");
            }
            else if (triggerby == "manager")
            {
                Sendemail(userreqemail, invoicetxtbox.Text.Trim() + " Shipping Request Completed ", createdbyname, Environment.NewLine + " Your shipping request has been completed and being processed for shipping.", fileName, connectapi.GetNameEmailByParaValue(UserFields.id, r["SubmittedTo"].ToString())[0].email, "");
            }
        }

        private bool Sendemailyesno()
        {
            return connectapi.GetConnectParameterValue("EmailShipping") == "1";
        }

        private async void SupcheckBox_Click(object sender, EventArgs e)
        {
            if (jobtxt.Text.Trim().Length > 0 && shiptocombobox.Text.Trim().Length > 0)
            {
                if (shipInvCreator)
                {
                    if (!supcheckBox.Checked)
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this shipping request from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            supcheckBox.Text = "Submit to Supervisor";
                            supcheckBox.Checked = false;
                            await Perfromsavebttn("SubmittedFalse").ConfigureAwait(false);
                        }
                        else
                        {
                            supcheckBox.Checked = true;
                        }
                    }
                    else
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this shipping invoice for approval?" + Environment.NewLine +
                            " " + Environment.NewLine +
                            "This will send an email to respective supervisor for approval.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            supcheckBox.Text = "Submitted to Supervisor";
                            await Perfromsavebttn("Submitted").ConfigureAwait(false);
                            Preparetosendemail("Submitted");
                        }
                        else
                        {
                            supcheckBox.Checked = false;
                        }
                    }
                }
                else
                {
                    supcheckBox.Checked = !supcheckBox.Checked;
                    MetroFramework.MetroMessageBox.Show(this, "Shipping invoice can only be submitted by the person who created it.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                supcheckBox.Checked = false;
                supcheckBox.Text = "Submit to Supervisor";
                MetroFramework.MetroMessageBox.Show(this, "Job number and ShipTo fields need to be filled in before submitting the shipping request.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}