using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SPMConnectAPI
{
    public class Shipping : ConnectAPI
    {
        #region Settting up Connetion and Get User

        private readonly log4net.ILog log;

        public Shipping()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Accessed Shipping Class " + Getassyversionnumber());
        }

        public List<string> GetManagersNameandEmail()
        {
            List<string> Happrovalnames = new List<string>();

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [ShippingManager] = '1' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Happrovalnames.Add(dr["Email"].ToString() + "][" + dr["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get Shipping Manager User Name and Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return Happrovalnames;
        }

        #endregion Settting up Connetion and Get User

        #region Datatables to pull out values or records

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

        public DataTable GetCustomerSoldShipToInfoname(string custId)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[CustomersShipTo] WHERE [C_No] = '" + custId.Replace("'", "''") + "'", cn))
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

        public DataTable Getpriceforitem(string itemnumber)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP (1) * FROM [SPM_Database].[dbo].[PriceItemsFromPO] WHERE [Item] = '" + itemnumber + "' order by LastUpdate Desc", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Item Price From PriceItemsPo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public DataTable ShowshippingHomeDataforManagers()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ShippingBaseWithNames] WHERE [IsApproved] = '1' AND [IsSubmitted] = '1' AND [IsShipped] = '0'", cn))
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

        public DataTable ShowshippingHomeDataforSupervisors(string myid)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ShippingBaseWithNames] WHERE [IsSubmitted] = '1' AND [SubmittedTo] = '" + myid + "' AND [IsApproved] = '0'", cn))
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

        #endregion Datatables to pull out values or records

        #region Generating New Ids

        private string GetNewCustItemId(string invoiceno)
        {
            string newcustitemid = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                int length = invoiceno.Length + 4;
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT  MAX(CAST(substring(Item, 8,3)AS INT))+1 as NextQuoteNo FROM [SPM_Database].[dbo].[ShippingItems] where substring(Item, 1, 2)  =  'CT' and InvoiceNo = '" + invoiceno + "'";
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
            if (string.IsNullOrEmpty(newcustitemid))
            {
                return "CT" + invoiceno + "-1";
            }
            else
            {
                return "CT" + invoiceno + "-" + newcustitemid;
            }
        }

        private string Getnewinvoicenumber()
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

            if (string.IsNullOrEmpty(newincoiveno))
            {
                newincoiveno = "1001";
            }

            return newincoiveno;
        }

        #endregion Generating New Ids

        #region FillComboBoxes

        public AutoCompleteStringCollection FillCarrierShip()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * from [dbo].[Carriers] UNION ALL SELECT DISTINCT [Carrier] from [dbo].[ShippingBase]where [Carrier] is not null", cn))
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
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM [SPM_Database].[dbo].[VendorsShipTo]", cn))
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

        public DataTable GetCustomerSoldShipToData()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[CustomersShipTo] where [Nom] is not null order by [Nom]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Customer Sold - Ship to Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GetVendorShipSoldToData()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VendorsShipTo] order by [Name]", cn))
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

        #endregion FillComboBoxes

        #region Perfrom CRUD on invoice details and shipping items

        public string Createnewshippinginvoice(string vendorcust)
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            string newinvoiceno = Getnewinvoicenumber();

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

        public bool InsertShippingItems(string invoicenumber, string itemnumber, string Description1, string Description2, string Description3, string origin, string tariff, string qty, decimal cost, decimal total)
        {
            bool success = false;
            if (itemnumber.Length == 0)
            {
                itemnumber = GetNewCustItemId(invoicenumber);
            }

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[ShippingItems] (Item, Description, Origin, TarriffCode, Qty, Cost,Total, InvoiceNo, OrderId) VALUES('" + itemnumber + "','" + Description1 + "'+ CHAR(10) + '" + Description2 + "'+ CHAR(10) +'" + Description3 + "','" + origin + "','" + tariff + "','" + qty + "','" + cost + "','" + total + "','" + invoicenumber + "','1')";
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

        public bool UpdateInvoiceDateCreatedToSql(string inovicenumber, string datecreated)
        {
            bool success = false;
            string username = ConnectUser.Name;
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[DateCreated] = '" + datecreated + "' WHERE [InvoiceNo] = '" + inovicenumber + "' ";

                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Invoice DateCreated - Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool UpdateInvoiceDetsToSql(string inovicenumber, string jobnumber, string salesperson, string requestedby, string carrier, string collectprepaid, string fobpoint, string terms, string currency, string total, string soldto, string shipto, string notes, string carriercode)
        {
            bool success = false;
            string username = ConnectUser.Name;
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[JobNumber] = '" + jobnumber + "',[SalesPerson] = '" + salesperson + "',[Requistioner] = '" + requestedby + "',[Carrier] = '" + carrier + "',[Collect_Prepaid] = '" + collectprepaid + "',[FobPoint] = '" + fobpoint + "',[Terms] = '" + terms + "',[Currency] = '" + currency + "',[Total] =  '" + total + "',[SoldTo] = '" + soldto + "',[ShipTo] = '" + shipto + "',[Notes] = '" + notes + "',[CarrierCode] = '" + carriercode + "' WHERE [InvoiceNo] = '" + inovicenumber + "' ";

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

        public bool UpdateInvoiceDetsToSqlforAuthorisation(string inovicenumber, string typeofsave, int supervisorid)
        {
            bool success = false;
            string username = ConnectUser.Name;
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (typeofsave == "Submitted")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[IsSubmitted] = '1',[SubmittedBy] = '" + username + "',[SubmittedOn] = '" + sqlFormattedDate + "', [SubmittedTo] = '" + supervisorid + "' WHERE [InvoiceNo] = '" + inovicenumber + "' ";
                }
                else if (typeofsave == "SubmittedFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[IsSubmitted] = '0',[SubmittedBy] = '',[SubmittedOn] = '',[SubmittedTo] = '0'  WHERE [InvoiceNo] = '" + inovicenumber + "' ";
                }
                else if (typeofsave == "SupSubmit")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[IsApproved] = '1',[ApprovedBy] = '" + username + "',[ApprovedOn] = '" + sqlFormattedDate + "'WHERE [InvoiceNo] = '" + inovicenumber + "' ";
                }
                else if (typeofsave == "SupSubmitFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[IsApproved] = '0',[ApprovedBy] = '',[ApprovedOn] = '' WHERE [InvoiceNo] = '" + inovicenumber + "' ";
                }
                else if (typeofsave == "Completed")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[ShippedOn] = '" + sqlFormattedDate + "',[IsShipped] = '1',[ShippedBy] = '" + username + "' WHERE [InvoiceNo] = '" + inovicenumber + "' ";
                }
                else if (typeofsave == "CompletedFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingBase] SET [DateLastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[ShippedOn] = '',[IsShipped] = '0',[ShippedBy] = '' WHERE [InvoiceNo] = '" + inovicenumber + "' ";
                }

                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Invoice Details - Update Authorizations", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ShippingItems] SET [Description] = '" + Description1 + "'+ CHAR(10) + '" + Description2 + "'+ CHAR(10) +'" + Description3 + "',[Origin] = '" + origin + "',[TarriffCode] = '" + tariff + "',[Qty] = '" + qty + "',[Cost] = '" + cost + "',[Total] = '" + total + "' WHERE [InvoiceNo] = '" + inovicenumber + "' AND Item = '" + itemnumber + "' ";

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

        #endregion Perfrom CRUD on invoice details and shipping items

        #region Perform Copy and CRUD

        public bool Checkitemexistsbeforeadding(string itemid, string invoiceno)
        {
            bool itempresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[ShippingItems] WHERE [Item]='" + itemid + "' AND InvoiceNo = '" + invoiceno + "'", cn))
            {
                try
                {
                    cn.Open();

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        //MessageBox.Show("item already exists");
                        itempresent = true;
                    }
                    else
                    {
                        //MessageBox.Show(" move forward");
                        itempresent = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check Item Present On SQL Shipping Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return itempresent;
        }

        public string CopyShippingInvoice(string oldinvoiceno)
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            string newinvoiceno = Getnewinvoicenumber();

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO[SPM_Database].[dbo].[ShippingBase]([InvoiceNo],[DateCreated],[CreatedBy],[DateLastSaved],[LastSavedBy],[JobNumber],[SalesPerson],[Requistioner],[Carrier],[Collect_Prepaid],[FobPoint],[Terms],[Currency],[Total],[Vendor_Cust],[SoldTo],[ShipTo],[Notes]) SELECT  '" + newinvoiceno + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "',[JobNumber],[SalesPerson],[Requistioner],[Carrier],[Collect_Prepaid],[FobPoint],[Terms],[Currency],[Total],[Vendor_Cust],[SoldTo],[ShipTo],[Notes] FROM [SPM_Database].[dbo].[ShippingBase] WHERE InvoiceNo = '" + oldinvoiceno + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = newinvoiceno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Copy Shipping invoice to new number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
                Copyshippingitems(newinvoiceno, oldinvoiceno);
                UpdateShippingItemsOrderId(newinvoiceno);
                UpdateShippingItemIdCopy(newinvoiceno);
            }
            return success;
        }

        public string GetCustVend(string invoicenumber)
        {
            string vendorcust = "0";
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ShippingBase] WHERE [InvoiceNo]='" + invoicenumber + "' ", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        vendorcust = reader["Vendor_Cust"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get CustVend", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return vendorcust;
        }

        public int Getqty(string itemid, string invoiceno)
        {
            int qty = 0;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ShippingItems] WHERE [Item]='" + itemid + "' AND InvoiceNo = '" + invoiceno + "'", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        qty = Convert.ToInt32(reader["Qty"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Qty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return qty;
        }

        public void UpdateShippingItemIdCopy(string newinvoicenumber)
        {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE [SPM_Database].[dbo].[ShippingItems] SET Item =  SUBSTRING(Item,1,2) + '" + newinvoicenumber + "-' + CAST(OrderId as varchar(10)) WHERE InvoiceNo = '" + newinvoicenumber + "' AND SUBSTRING(Item,1,2) = 'CT'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update Items Id - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                    UpdateShippingItemsCTOrderId(newinvoicenumber);
                }
            }
        }

        public void UpdateShippingItemsCTOrderId(string invoicenumber)
        {
            using (SqlCommand sqlCommand = new SqlCommand("with cte as(select *, new_row_id = row_number() over(partition by InvoiceNo order by InvoiceNo)from[dbo].[ShippingItems] where InvoiceNo = @itemnumber AND  SUBSTRING(Item,1,2) = 'CT')update cte set Item = SUBSTRING(Item,1,7) + CAST(new_row_id as varchar(10)) ", cn))
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

        private void Copyshippingitems(string newinvoiceno, string oldinvoiceno)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[ShippingItems] (OrderId,Item, Description, Origin, TarriffCode, Qty, Cost, Total, InvoiceNo)SELECT  OrderId,Item, Description, Origin, TarriffCode, Qty, Cost, Total,'" + newinvoiceno + "'FROM  [SPM_Database].[dbo].[ShippingItems] WHERE  InvoiceNo = '" + oldinvoiceno + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Copy shipping items to new invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        #endregion Perform Copy and CRUD
    }
}