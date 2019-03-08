using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class InvoiceDetails : Form
    {
        String connection;
        DataTable dt = new DataTable();        
        SqlConnection cn;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        string Invoice_Number = "";
        SPMConnectAPI.SPMSQLCommands connectapi = new SPMSQLCommands();

        public InvoiceDetails()
        {
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
            connectapi.SPM_Connect(connection);

            dt = new DataTable();
            _command = new SqlCommand();
        }

        public string invoicenumber(string number)
        {
            if (number.Length > 0)
                return Invoice_Number = number;
            return null;
        }

        private void QuoteDetails_Load(object sender, EventArgs e)
        {
          
            this.Text = "Invoice Details - " + Invoice_Number;
           
            FillFobPoint();
            FillSalesPerson();
            FillCarriers();
            FillTerms();

            if (GetShippingBaseInfo(Invoice_Number))
                FillShippingBaseInfo();
            //processeditbutton();

        }

        private bool GetShippingBaseInfo(string invoicenumber)
        {
            bool fillled = false;
            string sql = "SELECT * FROM [SPM_Database].[dbo].[ShippingBase] WHERE InvoiceNo = '"+invoicenumber+"'";
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }
            return fillled;
        }

        #region Fill information on controls

        private void FillShippingBaseInfo()
        {
            DataRow r = dt.Rows[0];

            invoicetxtbox.Text = r["InvoiceNo"].ToString();
            notestxt.Text = r["Notes"].ToString();
            string DateCreated = r["DateCreated"].ToString();
            string CreatedBy = r["CreatedBy"].ToString();
            string lastsavedby = r["LastSavedby"].ToString();
            string lastsaved = r["DateLastSaved"].ToString();


            Createdon.Text = "Created On : " + DateCreated;

            this.CreatedBy.Text = "Created By : " + CreatedBy;

            LastSavedOn.Text = "Last Saved By : " + lastsavedby;

            LastSavedBy.Text = "Last Saved On : " + lastsaved;

            string vendorcust = r["Vendor_Cust"].ToString();
            if (r["Vendor_Cust"].ToString() == "1")
            {
                VendorCust.Text = "Invoice For Customer";
                FillcustomersShipto();
                FillcustomersSoldto();
              
            }
            else
            {
                VendorCust.Text = "Invoice For Vendor";
                FillVendorsShipto();
                FillVendorsSoldto();
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

            string SoldTo = r["SoldTo"].ToString();
            if (SoldTo.Length > 0)
            {
                FillSoldToInformation(SoldTo,vendorcust);
            }

            string ShipTo = r["ShipTo"].ToString();
            if (ShipTo.Length > 0)
            {
                FillShipToInformation(ShipTo,vendorcust);
            }

            if (r["Collect_Prepaid"].ToString() == "1")
            {
                prepaidchkbox.Checked = true;
            }
            else
            {
                collectchkbox.Checked = true;
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

        #endregion



        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        private static String get_username()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (userName.Length > 0)
            {
                return userName;
            }
            else
            {
                return null;
            }

        }

        private string getuserfullname(string username)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string fullname = dr["Name"].ToString();
                    return fullname;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get Full User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible == true)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
        }

        private void processeditbutton()
        {
            editbttn.Visible = false;
            savbttn.Enabled = true;
            savbttn.Visible = true;
            jobtxt.ReadOnly = false;
            soldtogroupBox.Enabled = true;
            ShiptogroupBox.Enabled = true;
            shippinggroupBox.Enabled = true;

        }

        private void perfromlockdown()
        {
            editbttn.Visible = true;
            savbttn.Enabled = false;
            savbttn.Visible = false;
            jobtxt.ReadOnly = true;
            soldtogroupBox.Enabled = false;
            ShiptogroupBox.Enabled = false;
            shippinggroupBox.Enabled = false;

        }

        List<string> list = new List<string>();

        private void graballinfor()
        {
            list.Clear();
            string quote = invoicetxtbox.Text;
            string customer = soldtocombobox.Text;
            string category = Carriercombox.Text;
            string rating = Salespersoncombobox.Text;
            string howfound = FOBPointcombox.Text;
            string currency = currencycombox.Text;

            string notes = notestxt.Text;
            list.Add(quote);
            list.Add(customer);
            list.Add(category);
            list.Add(rating);
            list.Add(howfound);
            list.Add(currency);
            list.Add(notes);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace("'", "''");
            }

        }

        private void savbttn_Click(object sender, EventArgs e)
        {
            perfromsavebttn(true);
        }

        void perfromsavebttn(bool createfolder)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            perfromlockdown();
            graballinfor();

            //string lastsavedby = getuserfullname(get_username().ToString()).ToString();
            //createnewitemtosql(list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(), list[10].ToString(), lastsavedby);

            this.Enabled = true;
            Cursor.Current = Cursors.Default;
            GetShippingBaseInfo(list[0].ToString());
        }

        private void createnewitemtosql(string quote, string description, string customer, string category, string rating, string howfound, string quotedprice, string currency, string closed, string convertedtojob, string notes, string user)
        {
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Opportunities] SET Title = '" + description + "',Company_Name = '" + customer + "',Category = '" + category + "',Rating = '" + rating + "',How_Found = '" + howfound + "',Est_Revenue = '" + quotedprice.ToString() + "',Currency = '" + currency + "',Closed = '" + closed + "',Converted_to_Job = '" + convertedtojob + "',Comments = '" + notes + "',LastSavedby = '" + user + "',LastSaved = @value2,FolderPath = '' WHERE Quote = '" + quote + "' ";

                cmd.Parameters.AddWithValue("@value2", sqlFormattedDate);
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("Item sucessfully saved SPM Connect Server.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        private void collectchkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (collectchkbox.Checked)
            {
                prepaidchkbox.Checked = false;
            }
        }

        private void prepaidchkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (prepaidchkbox.Checked)
            {
                collectchkbox.Checked = false;
            }
        }

   
    }
}
