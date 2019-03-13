using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class SPMSQLCommands
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

        public string getassyversionnumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = "V" + assembly.GetName().Version.ToString(3);
            return version;
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

        public string getdepartment()
        {
            string Department = "";
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
                    Department = dr["Department"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user department", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return Department;
        }

        public void deleteitem(string _itemno)
        {
            DialogResult result = MessageBox.Show("Are you sure want to delete " + _itemno + "?", "SPM Connect - Delete Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_itemno.Length > 0)
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    try
                    {
                        string query = "DELETE FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber ='" + _itemno.ToString() + "'";
                        SqlCommand sda = new SqlCommand(query, cn);
                        sda.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show(_itemno + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.Close();
                    }

                }
            }

        }

        #region UserRights

        public bool CheckAdmin()
        {

            bool admin = false;
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Admin = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        admin = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve admin rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return admin;
        }

        public bool Checkdeveloper()
        {
            bool developer = false;
            string useradmin = UserName();

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Developer = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        developer = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve developer rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return developer;

        }

        public bool CheckManagement()
        {
            bool management = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Management = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", UserName());

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        management = true;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check management rights", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
            return management;
        }

        public bool checkpruchasereqrights()
        {
            bool purchasereq = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND PurchaseReq = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", UserName());

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        purchasereq = true;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check purchase rights", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
            return purchasereq;
        }

        public bool checkquoterights()
        {
            bool quoterights = false;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Quote = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", UserName());

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        quoterights = true;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check quote rights", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
            return quoterights;
        }

        public bool checkShippingrights()
        {
            bool quoterights = false;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Shipping = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", UserName());

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        quoterights = true;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check Shipping rights", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    cn.Close();
                }

            }
            return quoterights;
        }

        #endregion

        public void chekin(string applicationname)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("dd-MM-yyyy HH:mm tt");
            string computername = System.Environment.MachineName;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Checkin] ([Last Login],[Application Running],[User Name], [Computer Name], [Version]) VALUES('" + sqlFormattedDate + "', '" + applicationname + "', '" + UserName() + "', '" + computername + "','" + getassyversionnumber() + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - User Checkin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        public void checkout()
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[Checkin] WHERE [User Name] ='" + UserName().ToString() + "'";
                SqlCommand sda = new SqlCommand(query, cn);
                sda.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Checkout User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        public DataTable Showallitems()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UnionInventory] ORDER BY ItemNumber DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show all items Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable ShowFavorites()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT[ItemNumber],[Description],[FamilyCode],[Manufacturer],[ManufacturerItemNumber],[DesignedBy],[DateCreated],[LastSavedBy],[LastEdited],[Material],[FullSearch] FROM [SPM_Database].[dbo].[SPMConnectFavorites] where UserName like'%" + UserName() + "%'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show all items Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public string getfilename()
        {
            ModelDoc2 swModel;
            var progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId.ToString()) as SolidWorks.Interop.sldworks.SldWorks;
            string filename = "";

            int count;
            count = swApp.GetDocumentCount();

            if (count > 0)
            {
                // MessageBox.Show("Number of open documents in this SOLIDWORKS session: " + count);
                swModel = swApp.ActiveDoc as ModelDoc2;

                filename = swModel.GetTitle();


            }
            return filename;
        }

        public string get_pathname()
        {
            ModelDoc2 swModel;
            var progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId.ToString()) as SolidWorks.Interop.sldworks.SldWorks;


            int count;
            string pathName = "";
            count = swApp.GetDocumentCount();

            if (count > 0)
            {

                swModel = swApp.ActiveDoc as ModelDoc2;

                pathName = swModel.GetPathName();



            }
            return pathName;
        }

        public string getfamilycategory(string familycode)
        {
            string category = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Category FROM [SPM_Database].[dbo].[FamilyCodes] WHERE [FamilyCodes]='" + familycode.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    category = dr["Category"].ToString();
                    //MessageBox.Show(category);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get Family Category", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return category;
        }

        public string Makepath(string itemnumber)
        {
            string Pathpart = "";

            if (itemnumber.Length > 0)
            {
                string first3char = itemnumber.Substring(0, 3) + @"\";
                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                Pathpart = (spmcadpath + first3char);

            }
            return Pathpart;
        }

        public bool checkforreadonly()
        {
            bool notreadonly = true;
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
            if (swModel.IsOpenedReadOnly())
            {
                MessageBox.Show("Model is open read only. Please get write access from the associated user in order to edit the properties.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                notreadonly = false;
            }

            return notreadonly;

        }

        public string getcustomeralias(string customerid)
        {
            string customername = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Alias FROM [SPM_Database].[dbo].[Customers] WHERE [CustomerID]='" + customerid.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    customername = dr["Alias"].ToString();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get Customer Alias", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return customername;
        }

        #region GetNewItemNumber or copy items

        public string getactiveblock()
        {
            string useractiveblock = "";

            try
            {

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] where UserName ='" + UserName().ToString() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    useractiveblock = dr["ActiveBlockNumber"].ToString();
                    if (useractiveblock == "")
                    {
                        MessageBox.Show("User has not been assigned a block number. Please contact the admin.", "SPM Connect - Get Active Block Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get User Active Block", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();

            }
            finally
            {
                cn.Close();
            }

            return useractiveblock;

        }

        public string getlastnumber()
        {
            string blocknumber = getactiveblock().ToString();

            if (blocknumber == "")
            {

                return "";
            }
            else
            {
                string lastnumber = "";
                try
                {

                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT MAX(RIGHT(ItemNumber,5)) AS " + blocknumber.ToString() + " FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber like '" + blocknumber.ToString() + "%' AND LEN(ItemNumber)=6";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        lastnumber = dr[blocknumber].ToString();
                        if (lastnumber == "")
                        {
                            lastnumber = blocknumber.Substring(1) + "000";
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "SPM Connect - Get Last Number", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                finally
                {
                    cn.Close();

                }
                return lastnumber;
            }

        }

        public string spmnew_idincrement(string lastnumber, string blocknumber)
        {
            if (lastnumber.Substring(2) == "000")
            {
                string lastnumbergrp1 = blocknumber.Substring(0, 1).ToUpper();
                string newid1 = lastnumbergrp1 + lastnumber.ToString();
                return newid1;
            }
            else
            {
                string lastnumbergrp = blocknumber.Substring(0, 1).ToUpper();
                int lastnumbers = Convert.ToInt32(lastnumber);
                lastnumbers += 1;
                string newid = lastnumbergrp + lastnumbers.ToString();
                return newid;
            }

        }

        public bool validnumber(string lastnumber)
        {
            bool valid = true;
            if (lastnumber.ToString() != "")
            {
                if (lastnumber.Substring(2) == "999")
                {
                    MessageBox.Show("User block number limit has reached. Please ask the admin to assign a new block number.", "SPM Connect - Valid Number Limit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    valid = false;
                }

            }
            else
            {
                valid = false;
            }
            return valid;
        }

        public bool checkitempresentoninventory(string itemid)
        {
            bool itempresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemid.ToString() + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Check Item Present On SQL Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();

                }

            }
            return itempresent;

        }

        public void addcpoieditemtosqltablefromgenius(string newid, string activeid)
        {

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Inventory] (ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited) SELECT '" + newid + "',Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited FROM [SPM_Database].[dbo].[UnionInventory] WHERE ItemNumber = '" + activeid + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Copied Item To Inventory From Genius", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        public void addcpoieditemtosqltable(string selecteditem, string uniqueid)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss.fff");

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Inventory](ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,Material,Spare,DesignedBy,FamilyType,SurfaceProtection,HeatTreatment,Rupture,JobPlanning,Notes,DateCreated) SELECT '" + uniqueid + "',Description,FamilyCode,Manufacturer,ManufacturerItemNumber,Material,Spare,DesignedBy,FamilyType,SurfaceProtection,HeatTreatment,Rupture,JobPlanning,Notes,'" + sqlFormattedDate + "' FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber = '" + selecteditem + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add Copied Item To Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        #endregion

        #region Advance Search Fill Comboxes

        public AutoCompleteStringCollection fillfamilycodes()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT FamilyCodes FROM [SPM_Database].[dbo].[FamilyCodes] order by FamilyCodes", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill FamilyCodes Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return MyCollection;
        }

        public AutoCompleteStringCollection filluserwithblock()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT CONCAT(ActiveBlockNumber,' ',Name) AS UserWithCad FROM [SPM_Database].[dbo].[Users] WHERE ActiveBlockNumber is not null AND ActiveBlockNumber <> '' Order by ActiveBlockNumber", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill FamilyCodes Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return MyCollection;

        }

        public AutoCompleteStringCollection filldesignedby()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT DesignedBy from [dbo].[UnionInventory] where DesignedBy is not null order by DesignedBy", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Designedby Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection filllastsavedby()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT LastSavedBy from [dbo].[UnionInventory] where LastSavedBy is not null order by LastSavedBy", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill LastSavedBy Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection fillmanufacturers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[Manufacturers] order by Manufacturer", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Manufacturers Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection filloem()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[ManufacturersItemNumbers] order by [ManufacturerItemNumber]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill oem items Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillMaterials()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[FilterMaterialsMerged] order by [Material]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill material Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillnewitemMaterials()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT MaterialNames FROM [SPM_Database].[dbo].[Materials] ORDER BY MaterialNames", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill material Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;
        }

        public AutoCompleteStringCollection fillheattreats()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[HeatTreatments]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill material Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection fillsurface()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[SurfaceProtections]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill material Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        public AutoCompleteStringCollection filldescriptionsource()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[Descriptions]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill material Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return MyCollection;

        }

        #endregion

        #region OpenModel & Drawing

        public void checkforspmfile(string Item_No)
        {
            string ItemNumbero;
            ItemNumbero = Item_No + "-0";

            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)
            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char + Item_No + ".sldprt");
                string Pathassy = (spmcadpath + first3char + Item_No + ".sldasm");
                string PathPartNo = (spmcadpath + first3char + ItemNumbero + ".sldprt");
                string PathAssyNo = (spmcadpath + first3char + ItemNumbero + ".sldasm");



                if (File.Exists(Pathassy) && File.Exists(Pathpart))
                {

                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo." + Item_No + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo. " + ItemNumbero + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part file " + Item_No + "and Assembly file " + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(Pathassy) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNumbero + "and Assembly file" + Item_No + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathPartNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part two files " + Item_No + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathassy))
                {
                    MessageBox.Show($"System has found a assembly files " + Item_No + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else if (File.Exists(Pathassy))
                {
                    //Process.Start("explorer.exe", Pathassy);
                    //fName = Pathassy;
                    if (solidworks_running() == true)
                    {
                        Open_assy(Pathassy);
                    }

                }
                else if (File.Exists(PathAssyNo))
                {

                    //Process.Start("explorer.exe", PathAssyNo);
                    // fName = PathAssyNo;
                    if (solidworks_running() == true)
                    {
                        Open_assy(PathAssyNo);
                    }

                }
                else if (File.Exists(Pathpart))
                {

                    //Process.Start("explorer.exe", Pathpart);
                    //fName = Pathpart;
                    if (solidworks_running() == true)
                    {
                        Open_model(Pathpart);
                    }
                }
                else if (File.Exists(PathPartNo))
                {

                    //Process.Start("explorer.exe", PathPartNo);
                    //fName = PathPartNo;
                    if (solidworks_running() == true)
                    {
                        Open_model(PathPartNo);
                    }

                }
                else
                {
                    MessageBox.Show($"A file with the part number " + Item_No + " does not have Solidworks CAD Model. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //fName = "";
                }

            }

        }

        public void checkforspmdrwfile(string Item_No)
        {
            string ItemNumbero = Item_No + "-0";


            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)

            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + Item_No + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");


                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + Item_No + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                else if (File.Exists(Drawpath))
                {

                    //Process.Start("explorer.exe", Drawpath);
                    if (solidworks_running() == true)
                    {
                        Open_drw(Drawpath);
                    }

                }
                else if (File.Exists(drawpathno))
                {

                    //Process.Start("explorer.exe", drawpathno);
                    if (solidworks_running() == true)
                    {
                        Open_drw(drawpathno);
                    }

                }
                else
                {

                    MessageBox.Show($"A file with the part number" + Item_No + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

            }

        }

        public void checkforspmfileprod(string ItemNo)
        {
            string ItemNumbero;
            ItemNumbero = ItemNo + "-0";

            if (!String.IsNullOrWhiteSpace(ItemNo) && ItemNo.Length == 6)

            {
                string first3char = ItemNo.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char + ItemNo + ".sldprt");
                string Pathassy = (spmcadpath + first3char + ItemNo + ".sldasm");
                string PathPartNo = (spmcadpath + first3char + ItemNumbero + ".sldprt");
                string PathAssyNo = (spmcadpath + first3char + ItemNumbero + ".sldasm");



                if (File.Exists(Pathassy) && File.Exists(Pathpart))
                {

                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo." + ItemNo + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file and Assembly file with the same PartNo. " + ItemNumbero + "." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNo + "and Assembly file " + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(Pathassy) && File.Exists(PathPartNo))
                {
                    MessageBox.Show($"System has found a Part file " + ItemNumbero + "and Assembly file" + ItemNo + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathPartNo) && File.Exists(Pathpart))
                {
                    MessageBox.Show($"System has found a Part two files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (File.Exists(PathAssyNo) && File.Exists(Pathassy))
                {
                    MessageBox.Show($"System has found a assembly files " + ItemNo + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else if (File.Exists(Pathassy))
                {
                    Process.Start("explorer.exe", Pathassy);


                }
                else if (File.Exists(PathAssyNo))
                {

                    Process.Start("explorer.exe", PathAssyNo);


                }
                else if (File.Exists(Pathpart))
                {

                    Process.Start("explorer.exe", Pathpart);

                }
                else if (File.Exists(PathPartNo))
                {

                    Process.Start("explorer.exe", PathPartNo);


                }
                else
                {
                    MessageBox.Show($"A file with the part number" + ItemNo + " does not have Solidworks CAD Model. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

            }

        }

        public void checkforspmdrwfileprod(string str)
        {
            string ItemNumbero = str + "-0";


            if (!String.IsNullOrWhiteSpace(str) && str.Length == 6)

            {
                string first3char = str.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Drawpath = (spmcadpath + first3char + str + ".SLDDRW");

                string drawpathno = (spmcadpath + first3char + ItemNumbero + ".SLDDRW");


                if (File.Exists(drawpathno) && File.Exists(Drawpath))
                {
                    MessageBox.Show($"System has found a Part two files " + str + "," + ItemNumbero + " with the same PartNo." +
                        " So please contact the administrator.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                else if (File.Exists(Drawpath))
                {

                    Process.Start("explorer.exe", Drawpath);

                }
                else if (File.Exists(drawpathno))
                {

                    Process.Start("explorer.exe", drawpathno);

                }
                else
                {

                    MessageBox.Show($"A file with the part number" + str + " does not have Solidworks Drawing File. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {

            }

        }

        public bool solidworks_running()
        {

            if (Process.GetProcessesByName("SLDWORKS").Length >= 1)
            {
                //mysolidworks.ActiveModelDocChangeNotify += this.mysolidworks_activedocchange;
                return true;
            }
            else if ((Process.GetProcessesByName("SLDWORKS").Length == 0))
            {

                MessageBox.Show("Soliworks application needs to be running in order for SPM Connect to perform. Thank you.", "SPM Connect - Solidworks Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                MessageBox.Show("SPM Connect encountered more than one sesssion of solidworks running. Please close other sesssions in order for SPM Connect to perform. Thank you.", "SPM Connect - Solidworks Running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public void Open_model(string filename)
        {
            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
            //swPart = (PartDoc)swModel;
            //swPart = swApp.ActiveDoc;
            //AttachEventHandlersPart();
        }

        public void Open_assy(string filename)
        {

            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocASSEMBLY, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
            //swAssembly = (AssemblyDoc)swModel;
            //AttachEventHandlers();

        }

        public void Open_drw(string filename)
        {

            var progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            ModelDoc2 swModel = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            swModel = swApp.ActiveDoc as ModelDoc2;
        }

        #endregion

        #region solidworks createmodels and open models

        public void createmodel(string filename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string PartPath = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
                ModelDoc2 swModel = swApp.NewDocument(PartPath, 0, 0, 0) as ModelDoc2;
                swApp.Visible = true;
                swModel = swApp.ActiveDoc as ModelDoc2;
                ModelDocExtension swExt;
                swExt = swModel.Extension;
                bool boolstatus = false;
                boolstatus = swExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.ActivateDoc(filename);
                swModel = swApp.ActiveDoc as ModelDoc2;

                if (boolstatus == true)
                {
                    //MessageBox.Show("new part created");
                    get_pathname();
                    getfilename();

                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public void createassy(string filename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string assytemplate = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateAssembly);
                ModelDoc2 swModel = swApp.NewDocument(assytemplate, 0, 0, 0) as ModelDoc2;
                swApp.Visible = true;
                swModel = swApp.ActiveDoc as ModelDoc2;
                ModelDocExtension swExt;
                swExt = swModel.Extension;
                bool boolstatus = false;
                //boolstatus = swExt.SaveAs(filename, 0, (int)swDocumentTypes_e.swDocASSEMBLY, 0, 0, 0);
                boolstatus = swExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.ActivateDoc(filename);
                swModel = swApp.ActiveDoc as ModelDoc2;

                if (boolstatus == true)
                {
                    //MessageBox.Show("new assy created");
                    get_pathname();
                    getfilename();
                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }

        }

        public void createdrawingpart(string filename, string _itemnumber)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string template = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                ModelDoc2 swModel;
                DrawingDoc swDrawing;
                ModelDocExtension swModelDocExt;

                swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperBsize, 0, 0);
                swDrawing = (DrawingDoc)swModel;
                swDrawing = swApp.ActiveDoc as DrawingDoc;
                swModelDocExt = (ModelDocExtension)swModel.Extension;

                string Pathpart = Makepath(_itemnumber).ToString() + _itemnumber + ".sldprt";

                swDrawing.Create3rdAngleViews2(Pathpart);

                //Sheet cursheet;
                //cursheet = swDrawing.GetCurrentSheet();
                //double sheetwidth = 0;
                //double sheethieght = 0;
                //int lRetVal;
                //lRetVal= cursheet.GetSize(sheetwidth,sheethieght);                
                //SolidWorks.Interop.sldworks.View swView;

                //swView = (SolidWorks.Interop.sldworks.View)swDrawing.CreateDrawViewFromModelView3(Pathpart, "*Isometric",sheetwidth, sheethieght, 0);
                //swDrawing.InsertModelAnnotations3(0, 327663, true, true, false, false);
                //int nNumView = 0;
                //var Voutline;
                //var Vpostion;
                //double viewweidth = 0;
                //double viewheight = 0;


                //Voutline(nNumView) = swView.GetOutline();
                //Vpostion(nNumView) = swView.Position();
                //viewweidth = Voutline(2) - Voutline(0);

                //swView.Position(6, 5);


                bool boolstatus = false;
                boolstatus = swModelDocExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.QuitDoc(swModel.GetTitle());


                if (boolstatus == true)
                {
                    //MessageBox.Show("new part created");
                    //get_pathname();
                    //getfilename();

                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public void createdrwaingassy(string filename, string _itemnumber)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                swApp.Visible = true;
                string template = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                ModelDoc2 swModel;
                DrawingDoc swDrawing;
                ModelDocExtension swModelDocExt;

                swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperBsize, 0, 0);
                swDrawing = (DrawingDoc)swModel;
                swDrawing = swApp.ActiveDoc as DrawingDoc;
                swModelDocExt = (ModelDocExtension)swModel.Extension;

                string Pathpart = Makepath(_itemnumber).ToString() + _itemnumber + ".sldasm";

                swDrawing.Create3rdAngleViews2(Pathpart);

                //Sheet cursheet;
                //cursheet = swDrawing.GetCurrentSheet();
                //double sheetwidth = 0;
                //double sheethieght = 0;
                //int lRetVal;
                //lRetVal= cursheet.GetSize(sheetwidth,sheethieght);                
                //SolidWorks.Interop.sldworks.View swView;

                //swView = (SolidWorks.Interop.sldworks.View)swDrawing.CreateDrawViewFromModelView3(Pathpart, "*Isometric",sheetwidth, sheethieght, 0);
                //swDrawing.InsertModelAnnotations3(0, 327663, true, true, false, false);
                //int nNumView = 0;
                //var Voutline;
                //var Vpostion;
                //double viewweidth = 0;
                //double viewheight = 0;


                //Voutline(nNumView) = swView.GetOutline();
                //Vpostion(nNumView) = swView.Position();
                //viewweidth = Voutline(2) - Voutline(0);

                //swView.Position(6, 5);


                bool boolstatus = false;
                boolstatus = swModelDocExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.QuitDoc(swModel.GetTitle());


                if (boolstatus == true)
                {
                    //MessageBox.Show("new part created");
                    //get_pathname();
                    //getfilename();

                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public bool importstepfile(string stepFileName, string savefilename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                PartDoc swPart = default(PartDoc);
                AssemblyDoc swassy = default(AssemblyDoc);
                ModelDoc2 swModel = default(ModelDoc2);
                ModelDocExtension swModelDocExt = default(ModelDocExtension);
                ImportStepData swImportStepData = default(ImportStepData);

                bool status = false;
                int errors = 0;

                //Get import information
                swImportStepData = (ImportStepData)swApp.GetImportFileData(stepFileName);

                //If ImportStepData::MapConfigurationData is not set, then default to
                //the environment setting swImportStepConfigData; otherwise, override
                //swImportStepConfigData with ImportStepData::MapConfigurationData
                swImportStepData.MapConfigurationData = true;

                //Import the STEP file.
                try
                {
                    swPart = (PartDoc)swApp.LoadFile4(stepFileName, "r", swImportStepData, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch
                {

                }
                try
                {
                    swassy = (AssemblyDoc)swApp.LoadFile4(stepFileName, "r", swImportStepData, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch
                {

                }

                return status;


            }
            return false;
        }

        public bool importigesfile(string igesfilename, string savefilename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                PartDoc swPart = default(PartDoc);
                AssemblyDoc swassy = default(AssemblyDoc);
                ModelDoc2 swModel = default(ModelDoc2);
                ModelDocExtension swModelDocExt = default(ModelDocExtension);
                ImportIgesData swImportIgesdata = default(ImportIgesData);

                bool status = false;
                int errors = 0;
                swImportIgesdata = (ImportIgesData)swApp.GetImportFileData(igesfilename);
                swImportIgesdata.IncludeSurfaces = true;
                try
                {
                    swPart = (PartDoc)swApp.LoadFile4(igesfilename, "r", swImportIgesdata, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch
                {

                }
                try
                {
                    swassy = (AssemblyDoc)swApp.LoadFile4(igesfilename, "r", swImportIgesdata, ref errors);
                    swModel = (ModelDoc2)swassy;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);

                }
                catch
                {

                }

                return status;
            }
            return false;
        }

        public bool importparasolidfile(string parasolidfilename, string savefilename)
        {
            if (solidworks_running() == true)
            {
                var progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId.ToString()) as SldWorks;
                PartDoc swPart = default(PartDoc);
                AssemblyDoc swassy = default(AssemblyDoc);
                ModelDoc2 swModel = default(ModelDoc2);
                ModelDocExtension swModelDocExt = default(ModelDocExtension);


                bool status = false;
                int errors = 0;
                try
                {
                    swPart = (PartDoc)swApp.LoadFile4(parasolidfilename, "r", null, ref errors);
                    swModel = (ModelDoc2)swPart;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);

                }
                catch
                {

                }
                try
                {
                    swassy = (AssemblyDoc)swApp.LoadFile4(parasolidfilename, "r", null, ref errors);
                    swModel = (ModelDoc2)swassy;
                    swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);

                }
                catch
                {

                }

                return status;
            }
            return false;
        }

        #endregion

        #region GetConnectParameters

        public bool CheckPurchaseReqNotification()
        {
            bool maintenance = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'MonitorReqBase'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    limit = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - CheckPurchaseReq Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            if (limit == "1")
            {
                maintenance = true;
            }
            return maintenance;

        }

        #region GetFolderPaths for Connect Job Module

        public string GetProjectEngSp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ProjectEngSp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Project Eng Source Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetProjectEngDp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ProjectEngDp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Project Eng Destination Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetSpareEngSp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'SpareEngSp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Spare Eng Source Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetSpareEngDp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'SpareEngDp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Spare Eng Destination Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetServiceEngSp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ServiceEngSp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Service Eng Source Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetServiceEngDp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ServiceEngDp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Service Eng Destination Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        ///////////////////////////////////////////////////////////////////////////

        public string GetProjectSalesSp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ProjectSalesSp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Project Eng Source Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetProjectSalesDp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ProjectSalesDp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Project Eng Destination Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetSpareSalesSp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'SpareSalesSp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Spare Eng Source Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetSpareSalesDp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'SpareSalesDp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Spare Eng Destination Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetServiceSalesSp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ServiceSalesSp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Service Eng Source Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        public string GetServiceSalesDp()
        {
            string path = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ServiceSalesDp'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    path = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Service Eng Destination Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return path;
        }

        #endregion

        #region ReportViewer Paths

        public string GetReportBOM()
        {
            string report = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ReportBOM'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    report = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Report BOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return report;
        }

        public string GetReportSpareParts()
        {
            string report = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ReportSpareParts'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    report = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Report Spare Parts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return report;
        }

        public string GetReportWorkOrder()
        {
            string report = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ReportWorkOrder'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    report = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Report Work Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return report;
        }

        public string GetReportPurchaseReq()
        {
            string report = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ReportPurchaseReq'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    report = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Report Purchase Req", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return report;
        }

        public string GetReportShippingInvCom()
        {
            string report = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ReportShippingCommercial'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    report = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Report Shipping Invoice Commercial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return report;
        }

        public string GetReportShippingInvPack()
        {
            string report = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'ReportShippingPacking'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    report = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Report Shipping Invoice Packing List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

            return report;
        }

        #endregion

        #endregion

        #region Favorites

        public bool addtofavorites(string itemid)
        {
            bool completed = false;
            if (checkitempresentonFavorites(itemid))
            {
                string usernamesfromitem = Getusernamesfromfavorites(itemid);
                if (!userexists(usernamesfromitem))
                {
                    string newuseradded = usernamesfromitem + UserName() + ",";
                    updateusernametoitemonfavorites(itemid, newuseradded);

                }
                else
                {
                    MessageBox.Show("Item already exists on your favorite list", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                additemtofavoritessql(itemid);
            }

            return completed;
        }

        public bool removefromfavorites(string itemid)
        {
            bool completed = false;

            string usernamesfromitem = Getusernamesfromfavorites(itemid);
           
            updateusernametoitemonfavorites(itemid, removeusername(usernamesfromitem));

            MessageBox.Show("Item removed from your favorite list", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);


            return completed;
        }

        private bool checkitempresentonFavorites(string itemid)
        {
            bool itempresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[favourite] WHERE [Item]='" + itemid.ToString() + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Check Item Present On SQL Favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();

                }

            }
            return itempresent;

        }

        private void additemtofavoritessql(string itemid)
        {
            string userid = UserName();
            userid += ",";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[favourite] (Item,UserName) VALUES('" + itemid + "','" + userid + " ')";
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Item Added To your Favorites", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Add  Item To Favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void updateusernametoitemonfavorites(string itemid, string updatedusername)
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (updatedusername != "")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[favourite] SET UserName = '" + updatedusername + "'  WHERE Item = '" + itemid + "'";
                }
                else
                {
                    cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[favourite] WHERE Item = '" + itemid + "'";
                }
                
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update Item on Favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private string Getusernamesfromfavorites(string itemid)
        {
            string usersfav = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[favourite] WHERE [Item]='" + itemid + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    usersfav = dr["UserName"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user names from favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return usersfav;
        }

        private bool userexists(string usernames)
        {
            bool exists = false;
            string userid = UserName();
            // Split string on spaces (this will separate all the words).
            string[] words = usernames.Split(',');
            foreach (string word in words)
            {
                if (word == userid)
                    exists = true;
            }

            return exists;
        }

        private string removeusername(string usernames)
        {
            string removedusername = "";
            string userid = UserName();
            // Split string on spaces (this will separate all the words).
            string[] words = usernames.Split(',');
            foreach (string word in words)
            {
                if (word.Trim() == userid)
                {

                }
                else
                {
                    removedusername += word.Trim();
                    if(word.Trim() != "")
                    removedusername += ",";                
                }
            }
            return removedusername.Trim();
        }

        #endregion       

    }
}

