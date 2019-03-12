using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;

namespace SPMConnectAPI
{
    public class Shipping
    {
        SqlConnection cn;

        public void SPM_Connect(string connection)
        {

            // connection = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        public string UserName()
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

        public string getuserfullname()
        {
            string fullname = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + UserName().ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    fullname = dr["Name"].ToString();

                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user full name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return fullname;
        }

        public string getassyversionnumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = "V" + assembly.GetName().Version.ToString(3);
            return version;
        }

        public DataTable ShowshippingHomeData()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ShippingBaseWithNames]", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Show all shipping Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable ShowShippingInvoiceItems()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ShippingItems]", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Show all shipping Inovice Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetCustomerSoldShipToInfo(string custid)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[CustomersShipTo] WHERE [C_No] = '" + custid + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Customer Sold - Ship to Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetVendorShipSoldToInfo(string custid)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VendorsShipTo] WHERE [Code] = '" + custid + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Vendor Sold - Ship to Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetCustomerSoldShipToInfoname(string name)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[CustomersShipTo] WHERE [Nom] = '" + name.Replace("'", "''") + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Customer Sold - Ship to Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetVendorShipSoldToInfoname(string name)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VendorsShipTo] WHERE [Name] = '" + name.Replace("'", "''") + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Vendor Sold - Ship to Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetIteminfo(string itemnumber)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UnionInventory] WHERE [ItemNumber] = '" + itemnumber + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Item Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetShippingIteminfo(string itemnumber, string invoicenumber)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ShippingItems] WHERE [Item] = '" + itemnumber + "' and [InvoiceNo] = '" + invoicenumber + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Item Information from shippingbase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable GetOriginTarriffFound(string itemnumber)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP (1) * FROM [SPM_Database].[dbo].[ShippingItems] WHERE [Item] = '" + itemnumber + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Item Tarriff and origin from shippingbase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        private string getnewinvoicenumber()
        {
            string newincoiveno = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX([InvoiceNo]) + 1 as NextQuoteNo FROM [SPM_Database].[dbo].[ShippingBase]";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    newincoiveno = dr["NextQuoteNo"].ToString();

                }
                dt.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get New Invoice Number", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }


            return newincoiveno;

        }

        public string Createnewshippinginvoice(string vendorcust)
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = getuserfullname();
            string newinvoiceno = getnewinvoicenumber();


            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[ShippingBase] (InvoiceNo, DateCreated, CreatedBy, DateLastSaved, LastSavedBy, Vendor_Cust) VALUES('" + newinvoiceno + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "','" + vendorcust + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = newinvoiceno;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create Entry on shipping base", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();

            }
            return success;

        }

        public void UpdateShippingItemsOrderId(string invoicenumber)
        {
            using (SqlCommand sqlCommand = new SqlCommand("with cte as(select *, new_row_id = row_number() over(partition by InvoiceNo order by InvoiceNo)from[dbo].[ShippingItems] where InvoiceNo = @itemnumber)update cte set OrderId = new_row_id", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@itemnumber", invoicenumber);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update Order Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public bool DeleteItemFromInvoice(string invoicenumber, string itemnumber)
        {
            bool done = false;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[ShippingItems] WHERE Item ='" + itemnumber + "' AND InvoiceNo ='" + invoicenumber + "' ";
                SqlCommand sda = new SqlCommand(query, cn);
                sda.ExecuteNonQuery();
                cn.Close();

                done = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Remove Item from Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return done;
        }

        public AutoCompleteStringCollection FillShipToShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ShipToName] from [dbo].[ShippingBaseWithNames] where [ShipToName] is not null order by [ShipToName]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Ship To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return MyCollection;

        }

        public AutoCompleteStringCollection FillSoldToShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [SoldToName] from [dbo].[ShippingBaseWithNames] where SoldToName is not null order by SoldToName", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Sold To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection FillCarrierShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * from [dbo].[Carriers]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Carriers Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection FillCreatedbyShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [CreatedBy] from [dbo].[ShippingBaseWithNames] where CreatedBy is not null order by CreatedBy", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Createdby Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection FillLastSavedByShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [LastSavedBy] from [dbo].[ShippingBaseWithNames] where LastSavedBy is not null order by LastSavedBy", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Last Saved By Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillSalesPersonShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [SalesPerson] from [dbo].[ShippingBaseWithNames] where SalesPerson is not null order by SalesPerson", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill SalesPerson Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillShipToSoldToCustomers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Nom FROM[SPM_Database].[dbo].[CustomersShipTo]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Soldto - Shipto Customers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillShipToSoldToVendors()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM[SPM_Database].[dbo].[VendorsShipTo]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Soldto - Shipto Vendors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillTerms()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Terms] from [dbo].[ShippingBaseWithNames] where [Terms] is not null order by [Terms]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Terms To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return MyCollection;

        }

        public AutoCompleteStringCollection FillFobPoint()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [FobPoint] from [dbo].[ShippingBaseWithNames] where [FobPoint] is not null order by [FobPoint]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill FOB Point To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return MyCollection;

        }

        public AutoCompleteStringCollection FillRequistioner()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Requistioner] from [dbo].[ShippingBaseWithNames] where [Requistioner] is not null order by [Requistioner]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill FOB Point To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return MyCollection;

        }

        public AutoCompleteStringCollection FillitemsShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ItemsToSelect]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Items To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public bool UpdateInvoiceDetsToSql(string inovicenumber, string jobnumber, string salesperson, string requestedby, string carrier, string collectprepaid, string fobpoint, string terms, string currency, string total, string soldto, string shipto, string notes)
        {
            bool success = false;
            string username = getuserfullname();
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[JobNumber] = '" + jobnumber + "',[SalesPerson] = '" + salesperson + "',[Requistioner] = '" + requestedby + "',[Carrier] = '" + carrier + "',[Collect_Prepaid] = '" + collectprepaid + "',[FobPoint] = '" + fobpoint + "',[Terms] = '" + terms + "',[Currency] = '" + currency + "',[Total] =  '" + total + "',[SoldTo] = '" + soldto + "',[ShipTo] = '" + shipto + "',[Notes] = '" + notes + "' WHERE [InvoiceNo] = '" + inovicenumber + "' ";


                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Invoice Details - Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool UpdateShippingItems(string inovicenumber, string itemnumber, string Description1, string Description2, string Description3, string origin, string tariff, string qty, decimal cost, decimal total)
        {
            bool success = false;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingItems] SET [Description] = '" + Description1 + ",'+ CHAR(10) + '" + Description2 + ",'+ CHAR(10) +'" + Description3 + ",',[Origin] = '" + origin + "',[TarriffCode] = '" + tariff + "',[Qty] = '" + qty + "',[Cost] = '" + cost + "',[Total] = '" + total + "' WHERE [InvoiceNo] = '" + inovicenumber + "' AND Item = '" + itemnumber + "' ";


                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Invoice Item - Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool InsertShippingItems(string inovicenumber, string itemnumber, string Description1, string Description2, string Description3, string origin, string tariff, string qty, decimal cost, decimal total, string invoice)
        {
            bool success = false;
            if (itemnumber.Length > 0)
            {

            }
            else
            {
                itemnumber = getNewCustItemId();
            }

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[ShippingItems] (Item, Description, Origin, TarriffCode, Qty, Cost,Total, InvoiceNo, OrderId) VALUES('" + itemnumber + "','" + Description1 + ",'+ CHAR(10) + '" + Description2 + ",'+ CHAR(10) +'" + Description3 + ",','" + origin + "','" + tariff + "','" + qty + "','" + cost + "','" + total + "','" + invoice + "','1')";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add new shipping item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();

            }
            return success;
        }

        private string getNewCustItemId()
        {
            string newcustitemid = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX(substring(Item, 3, 10)) + 1 as NextQuoteNo FROM [SPM_Database].[dbo].[ShippingItems] where substring(Item, 1, 2)  =  'CT'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    newcustitemid = dr["NextQuoteNo"].ToString();

                }
                dt.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get New Customer Item Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }
            if (newcustitemid == "")
            {
                newcustitemid = "CT1001";
            }
            else
            {
                newcustitemid = "CT" + newcustitemid;
            }

            return newcustitemid;

        }
    }
}
