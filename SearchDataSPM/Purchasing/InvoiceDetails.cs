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

namespace SearchDataSPM
{
    public partial class InvoiceDetails : Form
    {
        #region Load Invoice Details and setting Parameters

        private DataTable dt = new DataTable();
        private DataTable dtsoldtoCust = new DataTable();
        private DataTable dtsoldtoVend = new DataTable();
        private string Invoice_Number = "";
        private string custvendor = "";
        private string shiptoid = "";
        private string soldtoid = "";
        private bool formloading = false;
        private SPMConnectAPI.Shipping connectapi = new Shipping();
        private log4net.ILog log;
        private string userfullname = "";
        private string createdbyname = "";
        private bool ecrcreator = false;
        private bool shippingsup = false;
        private bool shippingmanager = false;
        private int myid = 0;
        private int supervisorid = 0;
        private ErrorHandler errorHandler = new ErrorHandler();
        private bool splashWorkDone = false;

        public InvoiceDetails(string number)
        {
            InitializeComponent();
            dt = new DataTable();
            dtsoldtoCust = new DataTable();
            dtsoldtoVend = new DataTable();
            this.Invoice_Number = number;
        }

        private void QuoteDetails_Load(object sender, EventArgs e)
        {
            formloading = true;
            userfullname = connectapi.user.Name;
            GetUserCreds();
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

        private void GetUserCreds()
        {
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + connectapi.GetUserName() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    supervisorid = Convert.ToInt32(dr["ShipSup"].ToString());
                    myid = Convert.ToInt32(dr["id"].ToString());
                    string ecrsupstring = dr["ShipSupervisor"].ToString();
                    string ecrmanagerstring = dr["ShippingManager"].ToString();

                    if (ecrsupstring == "1")
                    {
                        shippingsup = true;
                    }
                    if (ecrmanagerstring == "1")
                    {
                        shippingmanager = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Error Getting User credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
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

            if (invoiceCreatedBy == userfullname)
            {
                ecrcreator = true;
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

            handleCheckBoxes(submittedtosup, submittedtomanager, ecrcomplete, invoiceCreatedBy, r["ApprovedBy"].ToString(),
               r["ShippedBy"].ToString(), r["SubmittedOn"].ToString(), r["ApprovedOn"].ToString(), r["ShippedOn"].ToString());

            CheckEditButtonRights(Convert.ToInt32(r["SubmittedTo"].ToString()) == myid);
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

        #endregion Fill information on controls

        #region Filling Up Comboboxes

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
                calculatetotal();
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

        #endregion DataGridView

        #region FormClosing

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible == true)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect - Save Invoice Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connectapi.CheckoutInvoice(invoicetxtbox.Text.Trim(), ConnectAPI.CheckInModules.ShipInv);
                    this.Dispose();
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
            else
            {
                connectapi.CheckoutInvoice(invoicetxtbox.Text.Trim(), ConnectAPI.CheckInModules.ShipInv);
            }
        }

        #endregion FormClosing

        #region Process Save

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

        private List<string> list = new List<string>();

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

        private async void savbttn_Click(object sender, EventArgs e)
        {
            await Perfromsavebttn("normal");
        }

        private async Task Perfromsavebttn(string typeofsave)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            await Task.Run(() => SplashDialog("Saving Data..."));
            Perfromlockdown();
            graballinfor();
            SaveReport(invoicetxtbox.Text);
            if (connectapi.UpdateInvoiceDetsToSql(list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(), list[10].ToString(), list[11].ToString(), list[12].ToString(), list[13].ToString()))
            {
                if (typeofsave != "normal")
                {
                    connectapi.UpdateInvoiceDetsToSqlforAuthorisation(list[0].ToString(), typeofsave, supervisorid);
                }
                if (GetShippingBaseInfo(list[0].ToString()))
                {
                    FillShippingBaseInfo();
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Error occured while saving data.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (GetShippingBaseInfo(list[0].ToString()))
                {
                    FillShippingBaseInfo();
                    SaveReport(invoicetxtbox.Text);
                }
            }
            splashWorkDone = true;
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void SplashDialog(string message)
        {
            splashWorkDone = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + (this.Width - splashForm.Width) / 2, this.Location.Y + (this.Height - splashForm.Height) / 2);
                    splashForm.Show();
                    while (!splashWorkDone)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
        }

        #endregion Process Save

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
            submissiongroupBox.Enabled = true;
        }

        #endregion Process Edit

        #region Calculate Total

        private string totalvalue = "";

        private void calculatetotal()
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

        #endregion Calculate Total

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

        private void shiptocombobox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void soldtocombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                soldtocombobox.Focus();
            }
        }

        private void shiptocombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                shiptocombobox.Focus();
            }
        }

        private void Carriercombox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Carriercombox.Focus();
            }
        }

        private void Termscombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Termscombobox.Focus();
            }
        }

        private void Salespersoncombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Salespersoncombobox.Focus();
            }
        }

        private void requestcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                requestcomboBox.Focus();
            }
        }

        private void FOBPointcombox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FOBPointcombox.Focus();
            }
        }

        #endregion Events

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

        #endregion ContextMenuStrip

        #region Perform Update

        private void updateitem()
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

        private string Getselecteditemnumber()
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
            ReportViewer form1 = new ReportViewer("ShippingInvPack", invoicetxtbox.Text);
            form1.Show();
        }

        private void print2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer("ShippingInvCom", invoicetxtbox.Text);
            form1.Show();
        }

        #endregion Print Reports

        #region Save Report

        private void SaveReport(string reqno)
        {
            string fileName = "";
            string filepath = connectapi.user.SharesFolder + @"\SPM_Connect\ShippingInvoices\";
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
                catch (Exception)
                {
                    //throw e;
                    // MessageBox.Show(e.Message, "SPM Connect - Save Report", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                //throw ex;
            }
            finally
            {
            }
        }

        #endregion Save Report

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
            log.Info("Closed Shipping Invoice Detail " + Invoice_Number + " ");
            this.Dispose();
        }

        private void handleCheckBoxes(string submittedtosup, string sumbittedtomanager, string complete,
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

        private void CheckEditButtonRights(bool mine)
        {
            if (ecrcreator && !shipsupervisorheckBox.Checked)
            {
                supcheckBox.Enabled = true;
                editbttn.Visible = true;
            }
            else
            {
                supcheckBox.Enabled = false;
                editbttn.Visible = false;
            }
            if (shippingsup && mine && supcheckBox.Checked && !shipmanagercheckBox.Checked)
            {
                shipsupervisorheckBox.Enabled = true;
                editbttn.Visible = true;
            }

            if (shippingmanager && shipsupervisorheckBox.Checked)
            {
                shipmanagercheckBox.Enabled = true;
                editbttn.Visible = true;
            }
            else
            {
                if (shippingsup)
                    shipsupervisorheckBox.Enabled = true;
                else
                    shipsupervisorheckBox.Enabled = false;

                if (shippingmanager && supcheckBox.Checked)
                {
                    shipsupervisorheckBox.Enabled = true;
                }
                shipmanagercheckBox.Enabled = false;
            }

            if (shipmanagercheckBox.Checked && !shippingmanager)
            {
                Perfromlockdown();
                editbttn.Visible = false;
            }
        }

        private async void managercheckBox_Click(object sender, EventArgs e)
        {
            if (shipsupervisorheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this shipping request from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    shipsupervisorheckBox.Checked = false;
                    shipsupervisorheckBox.Text = "Submit to Shipping Manager";
                    await Perfromsavebttn("SupSubmitFalse");
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
                    await Perfromsavebttn("SupSubmit");
                    Preparetosendemail("SupSubmit");
                }
                else
                {
                    shipsupervisorheckBox.Checked = false;
                }
            }
        }

        private async void ecrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (shipmanagercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this shipping request not completed?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    shipmanagercheckBox.Checked = false;
                    shipmanagercheckBox.Text = "Mark Shipping Request Complete";
                    await Perfromsavebttn("CompletedFalse");
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
                    await Perfromsavebttn("Completed");
                    Preparetosendemail("Completed");
                }
                else
                {
                    shipmanagercheckBox.Checked = false;
                }
            }
        }

        private async void supcheckBox_Click(object sender, EventArgs e)
        {
            if (jobtxt.Text.Trim().Length > 0 && shiptocombobox.Text.Trim().Length > 0)
            {
                if (ecrcreator)
                {
                    if (supcheckBox.Checked == false)
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this shipping request from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            supcheckBox.Text = "Submit to Supervisor";
                            supcheckBox.Checked = false;
                            await Perfromsavebttn("SubmittedFalse");
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
                            await Perfromsavebttn("Submitted");
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
                    if (supcheckBox.Checked == false)
                    {
                        supcheckBox.Checked = true;
                    }
                    else
                    {
                        supcheckBox.Checked = false;
                    }
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

        private void Preparetosendemail(string typeofSave)
        {
            string fileName = "";
            string filepath = connectapi.user.SharesFolder + @"\SPM_Connect\ShippingInvoices\";
            fileName = filepath + invoicetxtbox.Text + " - CI.pdf";
            filepath += invoicetxtbox.Text + " - PL.pdf";
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

        private string GetUserNameEmail(int id)
        {
            string Email = "";
            string name = "";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + id + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Email = dr["Email"].ToString();
                    name = dr["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Supervisor Name and Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            if (Email.Length > 0)
            {
                return Email + "][" + name;
            }
            else if (name.Length > 0)
            {
                return Email + "][" + name;
            }
            else
            {
                return "][";
            }
        }

        private string Getusernameandemail(string requestby)
        {
            string Email = "";
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [Name]='" + requestby.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Email = dr["Email"].ToString();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get User Name and Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
            if (Email.Length > 0)
            {
                return Email;
            }
            else
            {
                return "";
            }
        }

        private void Sendemailtosupervisor(string fileName)
        {
            string nameemail = GetUserNameEmail(connectapi.user.ShipSup);

            string[] values = nameemail.Replace("][", "~").Split('~');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            string email = values[0];
            string name = values[1];

            string[] names = name.Replace(" ", "~").Split('~');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim();
            }
            name = names[0];
            Sendemail(email, invoicetxtbox.Text + " Shipping Request Approval Required", name, Environment.NewLine + userfullname + " sent this shipping request for approval.", fileName, "", "");
        }

        private void SendemailtoManager(string fileName)
        {
            string[] nameemail = connectapi.GetManagersNameandEmail().ToArray();
            for (int i = 0; i < nameemail.Length; i++)
            {
                string[] values = nameemail[i].Replace("][", "~").Split('~');

                for (int a = 0; a < values.Length; a++)
                {
                    values[a] = values[a].Trim();
                }
                string email = values[0];
                string name = values[1];

                string[] names = name.Replace(" ", "~").Split('~');
                for (int b = 0; b < names.Length; b++)
                {
                    names[b] = names[b].Trim();
                }
                name = names[0];
                Sendemail(email, invoicetxtbox.Text.Trim() + " Shipment to be shipped", name, Environment.NewLine + userfullname + " sent this shipping request for shipping.", fileName, "", "");
            }
        }

        private void Sendemailtouser(string fileName, string triggerby)
        {
            DataRow r = dt.Rows[0];
            string userreqemail = Getusernameandemail(createdbyname);
            if (userreqemail == "")
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
                string supnameemail = GetUserNameEmail(Convert.ToInt32(r["SubmittedTo"].ToString()));
                string[] values = supnameemail.Replace("][", "~").Split('~');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                string supervisoremail = values[0];
                string name = values[1];

                string[] names = name.Replace(" ", "~").Split('~');
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = names[i].Trim();
                }
                name = names[0];

                Sendemail(userreqemail, invoicetxtbox.Text.Trim() + " Shipping Request Completed ", createdbyname, Environment.NewLine + " Your shipping request has been completed and being processed for shipping.", fileName, supervisoremail, "");
            }
        }

        private void Sendemail(string emailtosend, string subject, string name, string body, string filetoattach, string cc, string extracc)
        {
            if (Sendemailyesno())
            {
                SPMConnectAPI.SPMSQLCommands connectapi = new SPMConnectAPI.SPMSQLCommands();
                connectapi.TriggerEmail(emailtosend, subject, name, body, filetoattach, cc, extracc, "Normal");
            }
            else
            {
                MessageBox.Show("Email turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool Sendemailyesno()
        {
            bool sendemail = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailShipping'", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    limit = (string)cmd.ExecuteScalar();
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Email access for shipping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            if (limit == "1")
            {
                sendemail = true;
            }
            return sendemail;
        }
    }
}