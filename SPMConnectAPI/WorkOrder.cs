using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class WorkOrder
    {
        #region User Details and connections

        SqlConnection cn;

        public void SPM_Connect()
        {
            string connection = "Data Source=spm-sql;Initial Catalog=SPM_Database;User ID=SPM_Agent;password=spm5445";
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

        #endregion


        public bool WoExistsOnWotrack(string wo)
        {
            bool wopresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WO_Tracking] WHERE [WO]='" + wo + "'", cn))
            {
                try
                {
                    cn.Open();

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        wopresent = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check WO Present on Tracking", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();

                }
            }
            return wopresent;
        }

        public void scanworkorder(string wo)
        {
            if (WoExistsOnWotrack(wo))
            {
                string department = getdepartment();

                switch (department)
                {
                    case "Eng":

                        break;
                    case "Prodcution":

                        break;
                    case "Purchasing":

                        break;
                    case "Crib":

                        break;
                    default:

                        break;
                }
            }
        }

        private void performengscanwo(string wo)
        {

        }


    }
}
