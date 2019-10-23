using SPMConnect.UserActionLog;
using SPMConnectAPI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class InvoiceAddItem : Form
    {
        string connection;
        SqlConnection cn;
        SqlCommand _command;
        string Invoice_Number = "";
        string custvendor = "";
        string _operation = "";
        string _itemnumber = "";
        SPMConnectAPI.Shipping connectapi = new Shipping();
        log4net.ILog log;
        private UserActions _userActions;
        ErrorHandler errorHandler = new ErrorHandler();

        public InvoiceAddItem()
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
                MessageBox.Show(ex.Message, "SPM Connect - SQL Connection Error Invoice Add items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // connectapi.SPM_Connect();


            _command = new SqlCommand();
        }

        #region Setting Parameters

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

        public string command(string operation)
        {
            if (operation.Length > 0)
                return _operation = operation;
            return null;
        }

        public string itemnumber(string item)
        {
            if (item.Length > 0)
                return _itemnumber = item;
            return null;
        }

        #endregion

        private void QuoteDetails_Load(object sender, EventArgs e)
        {

            this.Text = _operation + " Item - " + Invoice_Number;
            //clearaddnewtextboxes();
            if (custvendor == "0")
            {
                ItemsCombobox.Visible = false;
                Descriptiontxtbox.ReadOnly = false;
                oemtxt.ReadOnly = false;
                oemitemnotxt.ReadOnly = false;

                if (_operation == "Update")
                {
                    Addnewbttn.Text = "Update";
                    Addnewbttn.Enabled = true;
                    ItemsCombobox.Visible = false;
                    //fillselectediteminfo(_itemnumber);
                    FillShippingItemInfo(_itemnumber, Invoice_Number, true);
                    FillOriginTarriff(_itemnumber);
                }
                else
                {
                    //fillitemscombobox();
                    //ItemsCombobox.Text = null;
                    //showspmchk.Visible = true;

                    showspmchk.Visible = true;
                    ItemTxtBox.Text = "New item id will be generated on save";

                }
            }
            else
            {
                ItemsCombobox.Visible = false;
                Descriptiontxtbox.ReadOnly = false;
                oemtxt.ReadOnly = false;
                oemitemnotxt.ReadOnly = false;
                if (_operation == "Update")
                {
                    Addnewbttn.Text = "Update";
                    Addnewbttn.Enabled = true;
                    FillShippingItemInfo(_itemnumber, Invoice_Number, false);
                }
                else
                {
                    showspmchk.Visible = true;
                    ItemTxtBox.Text = "New item id will be generated on save";

                }
            }
            Cursor.Current = Cursors.Default;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Shipping Invoice Add Item " + Invoice_Number + " by " + System.Environment.UserName);
            _userActions = new UserActions(this);
        }

        #region Fill information on controls

        private void fillitemscombobox()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillitemsShip();
            ItemsCombobox.AutoCompleteCustomSource = MyCollection;
            ItemsCombobox.DataSource = MyCollection;
        }

        private void fillselectediteminfo(string item)
        {
            DataTable iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetIteminfo(item);
            DataRow r = iteminfo.Rows[0];
            ItemTxtBox.Text = r["ItemNumber"].ToString();
            Descriptiontxtbox.Text = r["Description"].ToString();
            oemtxt.Text = r["Manufacturer"].ToString();
            oemitemnotxt.Text = r["ManufacturerItemNumber"].ToString();
        }

        private void FillShippingItemInfo(string item, string invoicenumber, bool vendor)
        {
            DataTable iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetShippingIteminfo(item, invoicenumber);
            DataRow r = iteminfo.Rows[0];
            //if (!vendor)
            //{
            ItemTxtBox.Text = r["Item"].ToString();
            extractdescription(r["Description"].ToString());
            //}
            qtytxt.Text = r["Qty"].ToString();
            pricetxt.Text = string.Format("{0:c2}", Convert.ToDecimal(r["Cost"].ToString()));
            origintxt.Text = r["Origin"].ToString();
            tarifftxt.Text = r["TarriffCode"].ToString();
            totaltxt.Text = string.Format("{0:c2}", Convert.ToDecimal(r["Total"].ToString()));


        }

        private void FillOriginTarriff(string item)
        {
            if (!(origintxt.Text.Length > 0 || tarifftxt.Text.Length > 0))
            {
                DataTable iteminfo = new DataTable();
                iteminfo.Clear();
                iteminfo = connectapi.GetOriginTarriffFound(item);
                if (iteminfo.Rows.Count > 0)
                {
                    DataRow r = iteminfo.Rows[0];
                    string price = string.Format("{0:c2}", Convert.ToDecimal(r["Cost"].ToString()));
                    string origin = r["Origin"].ToString();
                    string tariff = r["TarriffCode"].ToString();
                    if (price.Length > 0 || origin.Length > 0 || tariff.Length > 0)
                    {
                        DialogResult result = MetroFramework.MetroMessageBox.Show(this, "System has found below values for the selected item. Would you like to use these values?" + Environment.NewLine + " Price = " + price + Environment.NewLine +
                                                "Origin = " + origin + Environment.NewLine +
                                                "Tarriff Code = " + tariff + Environment.NewLine + "", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            pricetxt.Text = price;
                            origintxt.Text = origin;
                            tarifftxt.Text = tariff;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    iteminfo.Clear();
                    DataTable priceinfo = new DataTable();
                    priceinfo.Clear();
                    priceinfo = connectapi.getpriceforitem(item);
                    if (priceinfo.Rows.Count > 0)
                    {
                        DataRow r = priceinfo.Rows[0];
                        string price = string.Format("{0:c2}", Convert.ToDecimal(r["PriceItem"].ToString()));
                        string Currency = r["Currency"].ToString();
                        string PurchaseOrder = r["PurchaseOrder"].ToString();
                        if (price != "$0.00")
                        {
                            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "System has found below values for the selected item. Would you like to use these values?" + Environment.NewLine + " Price = " + price + Environment.NewLine +
                                                    "Currency = " + Currency + Environment.NewLine +
                                                    "PO No. = " + PurchaseOrder + Environment.NewLine + "", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                dontstop = false;
                                pricetxt.Text = price;
                            }
                            else
                            {

                            }
                        }

                    }

                }
            }
            dontstop = true;

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

            return base.ProcessCmdKey(ref msg, keyData);
        }


        #endregion        

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Descriptiontxtbox.Text.Length > 0 || origintxt.Text.Length > 0 || tarifftxt.Text.Length > 0)
            {
                if (_operation == "Update")
                {

                }
                else
                {
                    DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        this.Dispose();
                    }
                    else
                    {
                        e.Cancel = (result == DialogResult.No);
                    }
                }

            }

        }

        #region Calculate Total

        void calculatetotal()
        {

            if (qtytxt.Text.Length > 0 && pricetxt.Text != "0.00")
            {
                decimal total = 0.00m;
                int qty = 1;
                decimal price = 0.00m;

                try
                {
                    qty = Convert.ToInt32(qtytxt.Text);
                    price = Convert.ToDecimal(pricetxt.Text.Substring(1));

                    total += (qty * price);

                    totaltxt.Text = string.Format("{0:#.00}", total.ToString());
                }

                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect -  Error Getting Total", MessageBoxButtons.OK);
                }
            }
            else
            {
                totaltxt.Text = "";
            }

        }

        private decimal calculatetotal(int qty)
        {
            decimal total = 0.00m;
            decimal price = 0.00m;
            try
            {
                price = Convert.ToDecimal(pricetxt.Text.Substring(1));
                total += (qty * price);
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect -  Error Getting Total", MessageBoxButtons.OK);
            }

            return total;
        }

        #endregion

        void clearaddnewtextboxes()
        {
            ItemTxtBox.Clear();
            Descriptiontxtbox.Clear();
            oemitemnotxt.Clear();
            oemtxt.Clear();
            pricetxt.Clear();
            pricetxt.Text = "$0.00";
            qtytxt.Clear();
            totaltxt.Clear();
            totaltxt.Text = "$0.00";
            origintxt.Clear();
            tarifftxt.Clear();
            Addnewbttn.Text = "Add";
            Addnewbttn.Enabled = false;
            if (custvendor == "1")
            {
                ItemTxtBox.Text = "New item id will be generated on save";
            }

        }

        bool dontstop = true;

        private void pricetxt_TextChanged(object sender, EventArgs e)
        {
            if (dontstop)
            {
                string value = pricetxt.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0');
                decimal ul;
                //Check we are indeed handling a number
                if (decimal.TryParse(value, out ul))
                {
                    ul /= 100;
                    //Unsub the event so we don't enter a loop
                    pricetxt.TextChanged -= pricetxt_TextChanged;
                    //Format the text as currency
                    pricetxt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                    pricetxt.TextChanged += pricetxt_TextChanged;
                    pricetxt.Select(pricetxt.Text.Length, 0);
                }
            }
            bool goodToGo = TextisValid(pricetxt.Text);

            if (!goodToGo)
            {
                pricetxt.Text = "$0.00";
                pricetxt.Select(pricetxt.Text.Length, 0);
            }
            calculatetotal();
        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        private void extractdescription(string description)
        {
            string[] words = description.Split('\n');
            if (words.Length == 1)
            {
                Descriptiontxtbox.Text = words[0];
            }
            else if (words.Length == 2)
            {
                Descriptiontxtbox.Text = words[0];
                oemtxt.Text = words[1];
            }
            else
            {
                Descriptiontxtbox.Text = words[0];
                oemtxt.Text = words[1];
                oemitemnotxt.Text = words[2];
            }
        }

        private void qtytxt_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        private void qtytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.

            }
            else
            {
                e.Handled = true;
            }
        }

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (qtytxt.Text.Length > 0 && qtytxt.Text != "0" && pricetxt.Text != "$0.00")
            {
                decimal.TryParse(pricetxt.Text.Replace(",", "").Replace("$", ""), out decimal result12);
                decimal.TryParse(totaltxt.Text.Replace(",", "").Replace("$", ""), out decimal result11);
                cleantextboxesdata();
                if (_operation == "Update")
                {
                    if (connectapi.UpdateShippingItems(Invoice_Number, ItemTxtBox.Text.Trim(), Descriptiontxtbox.Text.Trim(), oemtxt.Text.Trim(), oemitemnotxt.Text.Trim(), origintxt.Text.Trim(), tarifftxt.Text.Trim(), qtytxt.Text.Trim(), result12, result11))
                    {
                        this.Close();
                    }

                }
                else
                {
                    if (custvendor == "1")
                    {
                        if (connectapi.InsertShippingItems(Invoice_Number, "", Descriptiontxtbox.Text.Trim(), oemtxt.Text.Trim(), oemitemnotxt.Text.Trim(), origintxt.Text.Trim(), tarifftxt.Text.Trim(), qtytxt.Text.Trim(), result12, result11))
                        {
                            perfromcancel();
                            ItemTxtBox.Text = "New item id will be generated on save";
                        }
                    }
                    else
                    {
                        if (connectapi.checkitemexistsbeforeadding(ItemTxtBox.Text.Trim(), Invoice_Number))
                        {
                            int qty = connectapi.Getqty(ItemTxtBox.Text.Trim(), Invoice_Number);
                            if (qty > 0)
                            {
                                qty += Convert.ToInt32(qtytxt.Text);

                                if (connectapi.UpdateShippingItems(Invoice_Number, ItemTxtBox.Text.Trim(), Descriptiontxtbox.Text.Trim(), oemtxt.Text.Trim(), oemitemnotxt.Text.Trim(), origintxt.Text.Trim(), tarifftxt.Text.Trim(), qty.ToString(), result12, calculatetotal(qty)))
                                {
                                    perfromcancel();
                                }
                            }

                        }
                        else
                        {
                            if (ItemsCombobox.Visible)
                            {
                                if (connectapi.InsertShippingItems(Invoice_Number, ItemTxtBox.Text.Trim(), Descriptiontxtbox.Text.Trim(), oemtxt.Text.Trim(), oemitemnotxt.Text.Trim(), origintxt.Text.Trim(), tarifftxt.Text.Trim(), qtytxt.Text.Trim(), result12, result11))
                                {
                                    perfromcancel();

                                }
                            }
                            else
                            {
                                if (connectapi.InsertShippingItems(Invoice_Number, "", Descriptiontxtbox.Text.Trim(), oemtxt.Text.Trim(), oemitemnotxt.Text.Trim(), origintxt.Text.Trim(), tarifftxt.Text.Trim(), qtytxt.Text.Trim(), result12, result11))
                                {
                                    perfromcancel();
                                    ItemTxtBox.Text = "New item id will be generated on save";
                                }
                            }

                        }

                    }

                }
            }
            else
            {
                if (qtytxt.Text.Length > 0 && qtytxt.Text != "0")
                    errorProvider1.SetError(pricetxt, "Price cannot be null");

                else if (pricetxt.Text != "$0.00" && qtytxt.Text.Length != 1)
                    errorProvider1.SetError(qtytxt, "Cannot be null");
                else if (qtytxt.Text == "0")
                    errorProvider1.SetError(qtytxt, "Qty cannot be zero");
                else
                {
                    errorProvider1.SetError(pricetxt, "Price cannot be null");
                    errorProvider1.SetError(qtytxt, "Cannot be null");
                }
            }
        }

        void cleantextboxesdata()
        {
            Regex reg = new Regex("[*'\",_&#^@]");
            ItemTxtBox.Text = reg.Replace(ItemTxtBox.Text, "''");
            Descriptiontxtbox.Text = reg.Replace(Descriptiontxtbox.Text, "''");
            oemitemnotxt.Text = reg.Replace(oemitemnotxt.Text, "''");
            oemtxt.Text = reg.Replace(oemtxt.Text, "''");
            origintxt.Text = reg.Replace(origintxt.Text, "''");
            tarifftxt.Text = reg.Replace(tarifftxt.Text, "''");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_operation == "Update") this.Close();
            else perfromcancel();
        }

        void perfromcancel()
        {
            clearaddnewtextboxes();
            ItemsCombobox.Text = null;
            if (!showspmchk.Checked)
            {
                ItemTxtBox.Text = "New item id will be generated on save";
            }

        }

        private void ItemsCombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (ItemsCombobox.Text.Length >= 6)
                {
                    clearaddnewtextboxes();
                    string item = ItemsCombobox.Text.Trim().Substring(0, 6);
                    fillselectediteminfo(item);
                    FillOriginTarriff(item);
                    Addnewbttn.Enabled = true;
                    this.Focus();
                    this.Activate();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void Descriptiontxtbox_TextChanged(object sender, EventArgs e)
        {
            if (Descriptiontxtbox.Text.Length > 0) Addnewbttn.Enabled = true;
        }

        private void Descriptiontxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                oemtxt.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void oemtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                oemitemnotxt.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void oemitemnotxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                qtytxt.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void qtytxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                pricetxt.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void pricetxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                origintxt.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void origintxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                tarifftxt.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void showspmchk_CheckedChanged(object sender, EventArgs e)
        {
            if (showspmchk.Checked)
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                perfromcancel();
                fillitemscombobox();
                ItemsCombobox.Text = null;
                ItemsCombobox.Visible = true;
                ItemTxtBox.Text = "";
                custvendor = "0";
                Descriptiontxtbox.ReadOnly = true;
                oemtxt.ReadOnly = true;
                oemitemnotxt.ReadOnly = true;
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
            }
            else
            {
                this.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                perfromcancel();
                ItemsCombobox.Visible = false;
                ItemTxtBox.Text = "New item id will be generated on save";
                custvendor = "1";
                Descriptiontxtbox.ReadOnly = false;
                oemtxt.ReadOnly = false;
                oemitemnotxt.ReadOnly = false;
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
            }
        }

        private void ItemsCombobox_Leave(object sender, EventArgs e)
        {
            if (ItemsCombobox.Text.Length == 6)
            {
                clearaddnewtextboxes();
                string item = ItemsCombobox.Text.Trim().Substring(0, 6);
                fillselectediteminfo(item);
                FillOriginTarriff(item);
                Addnewbttn.Enabled = true;
                this.Focus();
                this.Activate();
            }
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, _userActions, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, _userActions, this);
        }

        private void InvoiceAddItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            _userActions.FinishLoggingUserActions(this);
            log.Info("Closed Shipping Invoice Add Item " + Invoice_Number + " by " + System.Environment.UserName);
            this.Dispose();
        }
    }
}