using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMConnectAPI
{
    public class SPMSQLCommands
    {
        SqlConnection cn;

        public String UserName()
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

        public void SPM_Connect(string connection)
        {

            connection = System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception)
            {

            }

        }

        public string Userfullname(string connection)
        {

            SPM_Connect(connection);
            string username = UserName();
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

                return ex.ToString();


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        public void DeleteItem(string _itemno)
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

                }
                catch (Exception)
                {

                }
                finally
                {
                    cn.Close();
                }

            }

        }

        public int CheckAdmin(string connection)
        {
            SPM_Connect(connection);
            int usercount = 0;
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
                        usercount = 1;
                    }

                }
                catch (Exception)
                {
                }
                finally
                {
                    cn.Close();
                }

            }
            return usercount;
        }

        public int Checkdeveloper(string connection)
        {
            SPM_Connect(connection);
            int count = 0;
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
                        count = 1;
                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            return count;

        }
    }
}
