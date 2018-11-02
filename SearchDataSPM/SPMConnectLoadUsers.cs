using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SearchDataSPM
{
    class SPMConnectLoadUsers
    {
        #region SPM Connect Load

        String connection;
        SqlConnection cn;
        string userName;


        public void Connect_SPMSQL()
        {
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            try
            {
                cn = new SqlConnection(connection);
                cn.Open();

            }
            catch (Exception)
            {

                MessageBox.Show("Cannot connect through the server. Please check the network connection.", "SPM Connect - SQL Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                System.Environment.Exit(0);
                


            }
            finally
            {
                cn.Close();

            }

            if (chekusercredentialscontrols() == true)
            {
                Application.Run(new SPM_ConnectControls());
            }
            else if(chekusercredentialseng() == true)
            {
                Application.Run(new SPM_Connect());
            }
            else if (chekusercredentialsproduction() == true)
            {
                Application.Run(new SPM_ConnectProduction());
            }
            else
            {
                MessageBox.Show("UserName " + userName + " is not a licensed user. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                System.Environment.Exit(0);
            }
        }

        private bool chekusercredentialscontrols()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Department = 'Controls'", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@username", userName);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    cn.Close();

                    return true;
                }
                else
                {
                    cn.Close();


                }
            }
            return false;
        }

        private bool chekusercredentialseng()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Department = 'Eng'", cn))
            {
                cn.Open();

                sqlCommand.Parameters.AddWithValue("@username", userName);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    cn.Close();

                    return true;
                }
                else
                {
                    cn.Close();
                    

                }
            }
            return false;
        }

        private bool chekusercredentialsproduction()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Department = 'Production'", cn))
            {
                cn.Open();
                sqlCommand.Parameters.AddWithValue("@username", userName);

                int userCount = (int)sqlCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    cn.Close();
                    return true;

                    

                }
                else
                {
                    cn.Close();

                }

            }
            return false;
        }

        #endregion
    }


}
