using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class Controls
    {
        SqlConnection _connection;
        SqlConnection cn;
        SqlCommand _command;

        public void SPM_Connect(string connection)
        {

            // connection = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            try
            {
                _connection = new SqlConnection(connection);
                _command = new SqlCommand();
                _command.Connection = _connection;

            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        public void SPM_Connectconnectsql(string connection)
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

        public void CheckAutoCad(string ItemNo, string description, string family, string Manufacturer, string oem)
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
                    UpdateToAutocad(ItemNo,description,Manufacturer,oem);
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

        private void InsertToAutocad(string ItemNo, string description, string Manufacturer, string oem)
        {
            string sql;
            sql = "INSERT INTO [SPMControlCatalog].[dbo].[SPM-Catalog] ([CATALOG], [TEXTVALUE],[QUERY2], [MANUFACTURER],[USER3],[DESCRIPTION],[MISC1],[MISC2])" +
                "VALUES(LEFT( '" + oem.ToString() + "',50),'" + oem.ToString() + "','" + ItemNo.ToString() + "',  LEFT('" + Manufacturer.ToString() + "',20)," +
                "'" + Manufacturer.ToString() + "','" + description.ToString() + "',SUBSTRING('" + oem.ToString() + "',51,100),SUBSTRING('" + oem.ToString() + "',151,100))";

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

        private void UpdateToAutocad(string ItemNo, string description, string Manufacturer, string oem)
        {
            string sql;
            sql = "UPDATE [SPMControlCatalog].[dbo].[SPM-Catalog] SET [CATALOG] = LEFT( '" + oem.ToString() + "',50),[TEXTVALUE] = '" + oem.ToString() + "'," +
                "[MANUFACTURER] = LEFT('" + Manufacturer.ToString() + "',20),[USER3] = '" + Manufacturer.ToString() + "' ," +
                "[DESCRIPTION] = '" + description.ToString() + "',[MISC1] = SUBSTRING('" + oem.ToString() + "',51,100), [MISC2] = SUBSTRING('" + oem.ToString() + "',151,100)" +
                " WHERE [QUERY2] = '" + ItemNo.ToString() + "'";
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

        public int CheckAutoCadforassy(string ItemNo, string description, string Manufacturer, string oem)
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
                    DialogResult result = MessageBox.Show(
                                "ItemNumber = " + ItemNo + ". Does not exist on Genius. Please create the item on Genius in order to add to catalog."
                                , "Item Not Found On Genius!",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                }

            }

            return createasy;
        }

    }
}
