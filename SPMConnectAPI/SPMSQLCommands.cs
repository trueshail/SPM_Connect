using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class SPMSQLCommands : ConnectAPI
    {
        private readonly log4net.ILog log;

        public SPMSQLCommands()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Accessed SPMSQLCommands Class " + Getassyversionnumber());
        }

        public void Deleteitem(string _itemno)
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
                        string query = "DELETE FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber ='" + _itemno + "'";
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

        public bool ReadWhatsNew()
        {
            const bool read = false;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + GetUserName() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string readnew = dr["ReadWhatsNew"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user read whats new", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return read;
        }

        public void Chekin(string applicationname)
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
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Checkin] ([Last Login],[Application Running],[User Name], [Computer Name], [Version]) VALUES('" + sqlFormattedDate + "', '" + applicationname + "', '" + GetUserName() + "', '" + computername + "','" + Getassyversionnumber() + "')";
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

        public void Checkout()
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[Checkin] WHERE [User Name] ='" + GetUserName() + "'";
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

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[Inventory] ORDER BY ItemNumber DESC", cn))
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
        public DataTable ShowFilterallitems(string filter, bool wherecond)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter(wherecond ? "SELECT * FROM [SPM_Database].[dbo].[Inventory] WHERE " + filter + " ORDER BY ItemNumber DESC" : "SELECT * FROM [SPM_Database].[dbo].[Inventory] WHERE Description LIKE '%" + filter + "%' OR Manufacturer LIKE '%" + filter + "%' OR ManufacturerItemNumber LIKE '%" + filter + "%' OR ItemNumber LIKE '%" + filter + "%' ORDER BY ItemNumber DESC", cn))
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

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT[ItemNumber],[Description],[FamilyCode],[Manufacturer],[ManufacturerItemNumber],[DesignedBy],[DateCreated],[LastSavedBy],[LastEdited],[Material],[FullSearch] FROM [SPM_Database].[dbo].[SPMConnectFavorites] where UserName like'%" + GetUserName() + "%'", cn))
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

        public DataTable ShowDuplicates()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ManufactureItemDuplicatesView]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show Duplicates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable ShowAllParameters()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ConnectParamaters] ORDER BY Id ", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show all Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public string Getfilename()
        {
            ModelDoc2 swModel;
            const string progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId) as SolidWorks.Interop.sldworks.SldWorks;
            string filename = "";

            int count = swApp.GetDocumentCount();

            if (count > 0)
            {
                // MessageBox.Show("Number of open documents in this SOLIDWORKS session: " + count);
                swModel = swApp.ActiveDoc as ModelDoc2;
                if (swModel == null)
                    return "";

                filename = swModel.GetTitle().Substring(0, 6);
            }
            return filename;
        }

        public string Get_pathname()
        {
            ModelDoc2 swModel;
            const string progId = "SldWorks.Application";
            SldWorks swApp = System.Runtime.InteropServices.Marshal.GetActiveObject(progId) as SolidWorks.Interop.sldworks.SldWorks;

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

        public string Getfamilycategory(string familycode)
        {
            string category = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Category FROM [SPM_Database].[dbo].[FamilyCodes] WHERE [FamilyCodes]='" + familycode + "' ";
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
                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                Pathpart = (spmcadpath + first3char);
            }
            return Pathpart;
        }

        public bool Checkforreadonly()
        {
            bool notreadonly = true;
            const string progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
            swApp.Visible = true;
            ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
            if (swModel.IsOpenedReadOnly())
            {
                MessageBox.Show("Model is open read only. Please get write access from the associated user in order to edit the properties.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                notreadonly = false;
            }

            return notreadonly;
        }

        public string Getcustomeralias(string customerid)
        {
            string customername = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Alias FROM [SPM_Database].[dbo].[Customers] WHERE [CustomerID]='" + customerid + "' ";
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

        public string Getactiveblock()
        {
            string useractiveblock = "";

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] where UserName ='" + GetUserName() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    useractiveblock = dr["ActiveBlockNumber"].ToString();
                    if (string.IsNullOrEmpty(useractiveblock))
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

        public string Getlastnumber()
        {
            string blocknumber = Getactiveblock();

            if (string.IsNullOrEmpty(blocknumber))
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
                    cmd.CommandText = "SELECT MAX(RIGHT(ItemNumber,5)) AS " + blocknumber + " FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber like '" + blocknumber + "%' AND LEN(ItemNumber)=6";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        lastnumber = dr[blocknumber].ToString();
                        if (string.IsNullOrEmpty(lastnumber))
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

        public bool CheckBaseBlockNumberTaken()
        {
            string blocknumber = Getactiveblock();
            bool taken = false;
            if (string.IsNullOrEmpty(blocknumber))
            {
                return taken;
            }
            else
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT MAX(RIGHT(ItemNumber,5)) AS " + blocknumber + " FROM [SPM_Database].[dbo].[Inventory] WHERE ItemNumber like '" + blocknumber + "%' AND LEN(ItemNumber)=6";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        string lastnumber = dr[blocknumber].ToString();
                        if (string.IsNullOrEmpty(lastnumber))
                        {
                            taken = false;
                        }
                        else if (lastnumber == blocknumber.Substring(1) + "000")
                        {
                            taken = true;
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
                return taken;
            }
        }

        public string Spmnew_idincrement(string lastnumber, string blocknumber)
        {
            if (!CheckBaseBlockNumberTaken() && lastnumber.Substring(2) == "000")
            {
                string lastnumbergrp1 = blocknumber.Substring(0, 1).ToUpper();
                return lastnumbergrp1 + lastnumber;
            }
            else
            {
                string lastnumbergrp = blocknumber.Substring(0, 1).ToUpper();
                int lastnumbers = Convert.ToInt32(lastnumber);
                lastnumbers++;
                return lastnumbergrp + lastnumbers.ToString();
            }
        }

        public bool Validnumber(string lastnumber)
        {
            bool valid = true;
            if (lastnumber != "")
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

        public bool Checkitempresentoninventory(string itemid)
        {
            bool itempresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemid + "'", cn))
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

        public void Addcpoieditemtosqltablefromgenius(string newid, string activeid)
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

        public void Addcpoieditemtosqltable(string selecteditem, string uniqueid)
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

        #endregion GetNewItemNumber or copy items

        #region Advance Search Fill Comboxes

        public AutoCompleteStringCollection Fillfamilycodes()
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

        public AutoCompleteStringCollection Filluserwithblock()
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

        public AutoCompleteStringCollection Filldesignedby()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT DesignedBy from [dbo].[Inventory] where DesignedBy is not null order by DesignedBy", cn))
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

        public AutoCompleteStringCollection Filllastsavedby()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT LastSavedBy from [dbo].[Inventory] where LastSavedBy is not null order by LastSavedBy", cn))
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

        public AutoCompleteStringCollection Fillmanufacturers()
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

        public AutoCompleteStringCollection Filloem()
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

        public AutoCompleteStringCollection Fillheattreats()
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

        public AutoCompleteStringCollection Fillsurface()
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

        public AutoCompleteStringCollection Filldescriptionsource()
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

        #endregion Advance Search Fill Comboxes

        #region OpenModel & Drawing

        public void Checkforspmfile(string Item_No)
        {
            string ItemNumbero = Item_No + "-0";

            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)
            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

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
                    if (Solidworks_running())
                    {
                        Open_assy(Pathassy);
                    }
                }
                else if (File.Exists(PathAssyNo))
                {
                    //Process.Start("explorer.exe", PathAssyNo);
                    // fName = PathAssyNo;
                    if (Solidworks_running())
                    {
                        Open_assy(PathAssyNo);
                    }
                }
                else if (File.Exists(Pathpart))
                {
                    //Process.Start("explorer.exe", Pathpart);
                    //fName = Pathpart;
                    if (Solidworks_running())
                    {
                        Open_model(Pathpart);
                    }
                }
                else if (File.Exists(PathPartNo))
                {
                    //Process.Start("explorer.exe", PathPartNo);
                    //fName = PathPartNo;
                    if (Solidworks_running())
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

        public void Checkforspmdrwfile(string Item_No)
        {
            string ItemNumbero = Item_No + "-0";

            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)

            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

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
                    if (Solidworks_running())
                    {
                        Open_drw(Drawpath);
                    }
                }
                else if (File.Exists(drawpathno))
                {
                    //Process.Start("explorer.exe", drawpathno);
                    if (Solidworks_running())
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

        public void Checkforspmfileprod(string ItemNo)
        {
            string ItemNumbero = ItemNo + "-0";

            if (!String.IsNullOrWhiteSpace(ItemNo) && ItemNo.Length == 6)

            {
                string first3char = ItemNo.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

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

        public void Checkforspmdrwfileprod(string str)
        {
            string ItemNumbero = str + "-0";

            if (!String.IsNullOrWhiteSpace(str) && str.Length == 6)

            {
                string first3char = str.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

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

        public bool Solidworks_running()
        {
            if (Process.GetProcessesByName("SLDWORKS").Length >= 1)
            {
                //mysolidworks.ActiveModelDocChangeNotify += this.mysolidworks_activedocchange;
                return true;
            }
            else if (Process.GetProcessesByName("SLDWORKS").Length == 0)
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
            const string progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            _ = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            _ = swApp.ActiveDoc as ModelDoc2;
            //swPart = (PartDoc)swModel;
            //swPart = swApp.ActiveDoc;
            //AttachEventHandlersPart();
        }

        public void Open_assy(string filename)
        {
            const string progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            _ = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocASSEMBLY, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            _ = swApp.ActiveDoc as ModelDoc2;
            //swAssembly = (AssemblyDoc)swModel;
            //AttachEventHandlers();
        }

        public void Open_drw(string filename)
        {
            const string progId = "SldWorks.Application";
            SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
            swApp.Visible = true;
            int err = 0;
            int warn = 0;
            _ = (ModelDoc2)swApp.OpenDoc6(filename, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref err, ref warn);
            swApp.ActivateDoc(filename);
            _ = swApp.ActiveDoc as ModelDoc2;
        }

        #endregion OpenModel & Drawing

        #region solidworks createmodels and open models

        public void Createmodel(string filename)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                swApp.Visible = true;
                string PartPath = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);
                _ = swApp.NewDocument(PartPath, 0, 0, 0) as ModelDoc2;
                swApp.Visible = true;
                ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                ModelDocExtension swExt = swModel.Extension;
                bool boolstatus = swExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.ActivateDoc(filename);
                _ = swApp.ActiveDoc as ModelDoc2;

                if (boolstatus)
                {
                    //MessageBox.Show("new part created");
                    Get_pathname();
                    //Getfilename();
                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public void CreateAssy(string filename)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                swApp.Visible = true;
                string assytemplate = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateAssembly);
                _ = swApp.NewDocument(assytemplate, 0, 0, 0) as ModelDoc2;
                swApp.Visible = true;
                ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                ModelDocExtension swExt = swModel.Extension;
                //boolstatus = swExt.SaveAs(filename, 0, (int)swDocumentTypes_e.swDocASSEMBLY, 0, 0, 0);
                bool boolstatus = swExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.ActivateDoc(filename);
                _ = swApp.ActiveDoc as ModelDoc2;

                if (boolstatus)
                {
                    //MessageBox.Show("new assy created");
                    Get_pathname();
                    //Getfilename();
                }
                else
                {
                    //MessageBox.Show("part not saved");
                }
            }
        }

        public void Createdrawingpart(string filename, string _itemnumber)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                swApp.Visible = true;
                string template = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                ModelDoc2 swModel;
                DrawingDoc swDrawing;
                ModelDocExtension swModelDocExt;

                swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperBsize, 0, 0);
                swDrawing = swApp.ActiveDoc as DrawingDoc;
                swModelDocExt = (ModelDocExtension)swModel.Extension;

                string Pathpart = Makepath(_itemnumber) + _itemnumber + ".sldprt";

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

                bool boolstatus = swModelDocExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.QuitDoc(swModel.GetTitle());

                if (boolstatus)
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

        public void Createdrwaingassy(string filename, string _itemnumber)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                swApp.Visible = true;
                string template = swApp.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
                ModelDoc2 swModel;
                DrawingDoc swDrawing;
                ModelDocExtension swModelDocExt;

                swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperBsize, 0, 0);
                swDrawing = swApp.ActiveDoc as DrawingDoc;
                swModelDocExt = (ModelDocExtension)swModel.Extension;

                string Pathpart = Makepath(_itemnumber) + _itemnumber + ".sldasm";

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

                bool boolstatus = swModelDocExt.SaveAs(filename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                swApp.QuitDoc(swModel.GetTitle());

                if (boolstatus)
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

        public bool Importstepfile(string stepFileName, string savefilename)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                bool status = false;
                int errors = 0;

                //Get import information
                ImportStepData swImportStepData = (ImportStepData)swApp.GetImportFileData(stepFileName);

                //If ImportStepData::MapConfigurationData is not set, then default to
                //the environment setting swImportStepConfigData; otherwise, override
                //swImportStepConfigData with ImportStepData::MapConfigurationData
                swImportStepData.MapConfigurationData = true;

                //Import the STEP file.
                try
                {
                    ModelDoc2 swModel = (ModelDoc2)swApp.LoadFile4(stepFileName, "r", swImportStepData, ref errors);
                    ModelDocExtension swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }

                return status;
            }
            return false;
        }

        public bool Importigesfile(string igesfilename, string savefilename)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                bool status = false;
                int errors = 0;
                ImportIgesData swImportIgesdata = (ImportIgesData)swApp.GetImportFileData(igesfilename);
                swImportIgesdata.IncludeSurfaces = true;
                try
                {
                    ModelDoc2 swModel = (ModelDoc2)swApp.LoadFile4(igesfilename, "r", swImportIgesdata, ref errors);
                    ModelDocExtension swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, 0, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
                return status;
            }
            return false;
        }

        public bool Importparasolidfile(string parasolidfilename, string savefilename)
        {
            if (Solidworks_running())
            {
                const string progId = "SldWorks.Application";
                SldWorks swApp = Marshal.GetActiveObject(progId) as SldWorks;
                bool status = false;
                int errors = 0;
                try
                {
                    //PartDoc swPart = default(PartDoc);
                    //AssemblyDoc swassy = default(AssemblyDoc);
                    ModelDoc2 swModel = (ModelDoc2)swApp.LoadFile4(parasolidfilename, "r", null, ref errors);
                    //swModel = (ModelDoc2)swPart;
                    ModelDocExtension swModelDocExt = (ModelDocExtension)swModel.Extension;
                    status = swModelDocExt.SaveAs(savefilename, (int)swSaveAsVersion_e.swSaveAsCurrentVersion, (int)swSaveAsOptions_e.swSaveAsOptions_Silent, 0, 0, 0);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }

                return status;
            }
            return false;
        }

        #endregion solidworks createmodels and open models

        #region Favorites

        public bool Addtofavorites(string itemid)
        {
            const bool completed = false;
            if (CheckitempresentonFavorites(itemid))
            {
                string usernamesfromitem = Getusernamesfromfavorites(itemid);
                if (!Userexists(usernamesfromitem))
                {
                    string newuseradded = usernamesfromitem + GetUserName() + ",";
                    Updateusernametoitemonfavorites(itemid, newuseradded);
                }
                else
                {
                    MessageBox.Show("Item no " + itemid + " already exists on your favorite list.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Additemtofavoritessql(itemid);
            }

            return completed;
        }

        public bool Removefromfavorites(string itemid)
        {
            const bool completed = false;

            string usernamesfromitem = Getusernamesfromfavorites(itemid);

            Updateusernametoitemonfavorites(itemid, Removeusername(usernamesfromitem));

            MessageBox.Show("Item no " + itemid + " has been removed from your favorite list.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return completed;
        }

        private bool CheckitempresentonFavorites(string itemid)
        {
            bool itempresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[favourite] WHERE [Item]='" + itemid + "'", cn))
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

        private void Additemtofavoritessql(string itemid)
        {
            string userid = GetUserName();
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
                MessageBox.Show("Item no " + itemid + " has been added to your favorites.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Updateusernametoitemonfavorites(string itemid, string updatedusername)
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

        private bool Userexists(string usernames)
        {
            bool exists = false;
            string userid = GetUserName();
            // Split string on spaces (this will separate all the words).
            string[] words = usernames.Split(',');
            foreach (string word in words)
            {
                if (word == userid)
                    exists = true;
            }

            return exists;
        }

        private string Removeusername(string usernames)
        {
            string removedusername = "";
            string userid = GetUserName();
            // Split string on spaces (this will separate all the words).
            string[] words = usernames.Split(',');
            foreach (string word in words)
            {
                if (word.Trim() != userid)
                {
                    removedusername += word.Trim();
                    if (word.Trim() != "")
                        removedusername += ",";
                }
            }
            return removedusername.Trim();
        }

        #endregion Favorites
    }
}