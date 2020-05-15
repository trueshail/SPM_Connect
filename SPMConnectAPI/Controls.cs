using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class Controls : ConnectAPI
    {
        private SqlCommand _command;
        private SqlConnection _connection;
        private log4net.ILog log;

        public Controls()
        {
            SPM_Connect();
        }

        public bool CheckAssyOnGenius(string ItemNo)
        {
            bool createasy = false;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPMDB].[dbo].[Edb] WHERE Item = @item ", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    // MessageBox.Show("items exists");
                    cn.Close();
                    //insert to autocad catalog
                    // InsertToAutocad();
                    createasy = true;

                    // show the new form
                }
                else
                {
                    //call Genius check item
                    _ = MessageBox.Show(
                                "ItemNumber = " + ItemNo + ". Does not exist on Genius. Please create the item on Genius in order to add to catalog."
                                , "Item Not Found On Genius!",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                }
            }

            return createasy;
        }

        public void CheckAutoCad(string ItemNo, string description, string Manufacturer, string oem)
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Count(*) from [SPMControlCatalog].[dbo].[SPM-Catalog] where (QUERY2 = @item) AND (ASSEMBLYCODE IS NULL OR ASSEMBLYCODE = '') AND (ASSEMBLYLIST IS NULL OR ASSEMBLYLIST = '') ", _connection))
            {
                _connection.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);
                //  sqlCommand.Parameters.AddWithValue("@code", null);
                // sqlCommand.Parameters.AddWithValue("@list", null);
                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    //MessageBox.Show("Item already exists on the catalog." + Environment.NewLine + "Item properties updated!","SPM Connect",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _connection.Close();
                    //call for update
                    UpdateToAutocad(ItemNo, description, Manufacturer, oem);
                }
                else
                {
                    //call Genius check item
                    // MessageBox.Show("not exists");
                    _connection.Close();
                    if (CheckItemOnGenius(ItemNo))
                    {
                        InsertToAutocad(ItemNo, description, Manufacturer, oem);
                    }
                }
            }
        }

        public int CheckAutoCadforassy(string ItemNo)
        {
            int openassycatalog = 0;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Count(*) from [SPMControlCatalog].[dbo].[SPM-Catalog] where (QUERY2 = @item) AND (ASSEMBLYCODE = @item) AND (ASSEMBLYLIST IS NULL OR ASSEMBLYLIST = '') ", _connection))
            {
                _connection.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);
                //  sqlCommand.Parameters.AddWithValue("@code", null);
                // sqlCommand.Parameters.AddWithValue("@list", null);
                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    _connection.Close();
                    //call for update
                    MessageBox.Show("Assembly already exists on the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    openassycatalog = 1;
                }
                else if (userCount > 1)
                {
                    openassycatalog = 2;
                    _connection.Close();
                    MessageBox.Show("Technical error while looking up for assembly in autocad catalog. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    openassycatalog = 3;
                    //call Genius check item
                    _connection.Close();
                }
            }
            return openassycatalog;
        }

        public void updateitempropertiesfromgenius(string item)
        {
            _ = new DataTable();
            DataTable dtb1 = GetGeniusInfoOnItem(item);
            DataRow r = dtb1.Rows[0];

            string description = r["Des"].ToString();
            string family = r["Famille"].ToString();
            string manufacturer = r["Des4"].ToString();
            string oem = r["Des2"].ToString();

            if (checkitemexists(item))
            {
                if (UpdateItemOnConnect(item, description, family, manufacturer, oem))
                {
                    MessageBox.Show("Item Details Updated successfully. Please refresh in order to see the changes", "SPM Connect - Items Properties Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occured while updating item properties. Please contact the admin", "SPM Connect - Update Item Properties", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Item already up to date with genius ", "SPM Connect - Items Properties", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool checkitemexists(string itemnumber)
        {
            bool exists = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber]='" + itemnumber + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        exists = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return exists;
        }

        private bool CheckItemOnGenius(string ItemNo)
        {
            bool exists = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPMDB].[dbo].[Edb] WHERE Item = @item ", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@item", ItemNo);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    // MessageBox.Show("items exists");
                    cn.Close();
                    exists = true;
                    //insert to autocad catalog
                }
                else
                {
                    cn.Close();
                    //call Genius check item
                    DialogResult result = MessageBox.Show(
                                "ItemNumber = " + ItemNo + ". Does not exist on Genius. Please create the item on Genius in order to add to catalog."
                                , "Item Not Found On Genius!",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    exists = false;
                }
            }
            return exists;
        }

        private DataTable GetGeniusInfoOnItem(string itemno)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [Des],[Famille],[Des4],[Des2] FROM [SPMDB].[dbo].[Edb] where Item ='" + itemno + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - get item info from genius", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        private void InsertToAutocad(string ItemNo, string description, string Manufacturer, string oem)
        {
            string sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2])" +
                "VALUES(LEFT( '" + oem + "',50),'" + oem + "','" + ItemNo + "',  LEFT('" + Manufacturer + "',20)," +
                "'" + Manufacturer + "','" + description + "',SUBSTRING('" + oem + "',51,100),SUBSTRING('" + oem + "',151,100))";

            try
            {
                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                MessageBox.Show("Item added to the catalog.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Technical error while inserting to autocad catalog. Please contact the admin.", "InsertToAutoCad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void SPM_Connect()
        {
            string connection = ConnectCntrlsConnectionString();
            try
            {
                _connection = new SqlConnection(connection);
                _command = new SqlCommand
                {
                    Connection = _connection
                };
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect autocad catalog sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Accessed Controls Class " + Getassyversionnumber());
        }

        private bool UpdateItemOnConnect(string item, string Description, string Family, string Manufacturer, string ManufacturerItemNumber)
        {
            bool status = false;
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Inventory] SET Description = '" + Description + "',FamilyCode = '" + Family + "',Manufacturer = '" + Manufacturer + "',ManufacturerItemNumber = '" + ManufacturerItemNumber + "' WHERE ItemNumber = '" + item + "' ";
                cmd.ExecuteNonQuery();
                cn.Close();
                status = true;
                //MessageBox.Show("Item sucessfully saved SPM Connect Server.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update item to connect db", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return status;
        }

        private void UpdateToAutocad(string ItemNo, string description, string Manufacturer, string oem)
        {
            string sql = "UPDATE [SPMControlCatalog].[dbo].[SPM-Catalog] SET [CATALOG] = LEFT( '" + oem + "',50),[TEXTVALUE] = '" + oem + "'," +
                "[MANUFACTURER] = LEFT('" + Manufacturer + "',20),[USER3] = '" + Manufacturer + "' ," +
                "[DESCRIPTION] = '" + description + "',[MISC1] = SUBSTRING('" + oem + "',51,100), [MISC2] = SUBSTRING('" + oem + "',151,100)" +
                " WHERE [QUERY2] = '" + ItemNo + "'";
            try
            {
                _connection.Open();
                _command.CommandText = sql;
                _command.ExecuteNonQuery();
                MessageBox.Show("Item already exists on the catalog." + Environment.NewLine + "Item properties updated!", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException)
            {
                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Incorrect Data Enrty Found While Upadting. Please contact the system Admin", "UpdateToAutoCad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}