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

        private string UserName()
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

        private string getassyversionnumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = "V" + assembly.GetName().Version.ToString(3);
            return version;
        }

        private string getuserfullname()
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

        private string getdepartment()
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

        public DataTable GetWoStatus(string wo)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT WO,Status FROM [SPM_Database].[dbo].[WO_Tracking] WHERE [WO] = '" + wo + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public void scanworkorder(string wo)
        {
            if (WoExistsOnWotrack(wo))
            {
                string department = getdepartment();

                if(department == "Eng")
                {
                    MessageBox.Show("Work order has already been entered into the system", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DoWorkOrderTacking(wo, department);
                }
            }
            else
            {
                // workorder not started into the system
                if (WOReleased(wo))
                    enterwototrack(wo);
                else
                {
                    MessageBox.Show("Please check the work order number.", "Work Order not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool WoExistsOnWotrack(string wo)
        {
            bool wopresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WO_Tracking] WHERE [WO]='" + wo + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        wopresent = true;
                    }
                    cn.Close();
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

        public bool WOReleased(string wo)
        {
            bool wopresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WorkOrderManagement] WHERE [WorkOrder]='" + wo + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        wopresent = true;
                    }
                    cn.Close();
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

        private void enterwototrack(string wo)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = getuserfullname();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WO_Tracking] (WO, Engin, EngWho, EngWhen, Status) VALUES('" + wo + "','1','" + username + "','" + sqlFormattedDatetime + "','OutEngineering')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Work Order Checked in.", "SPM Connect  - Work Order In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create New WO to track", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private bool UpdateWOTracking(string wo, string parameter1, string parameter2, string parameter3, string para1value1, string para1value2, string status)
        {
            bool success = false;
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[WO_Tracking] SET " + parameter1 + " = '" + para1value1 + "'," + parameter2 + " = '" + para1value2 + "'," + parameter3 + " = '" + sqlFormattedDatetime + "',Status = '" + status + "'  WHERE WO = '" + wo + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update WO Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        private DataTable GetWOInfo(string wo)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WO_Tracking] WHERE [WO] = '" + wo + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get WO Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        private void DoWorkOrderTacking(string wo, string department)
        {
            DataTable woinfo = new DataTable();
            woinfo.Clear();
            woinfo = GetWOInfo(wo);
            DataRow r = woinfo.Rows[0];
            string engin = r["Engin"].ToString();
            string Prodin = r["Prodin"].ToString();
            string Prodout = r["Prodout"].ToString();
            string Purin = r["Purin"].ToString();
            string Purout = r["Purout"].ToString();
            string Cribin = r["Cribin"].ToString();
            string Cribout = r["Cribout"].ToString();
            findwhatstagetoinsert(engin, Prodin, Prodout, Purin, Purout, Cribin, Cribout, department, wo);
        }

        private void findwhatstagetoinsert(string engin, string prodin, string prodout, string purin, string purout, string cribin, string cribout, string department, string wo)
        {
            switch (department)
            {
                case "Production":
                    checkprodtrack(engin, prodin, prodout, wo);
                    break;

                case "Purchasing":
                    checkpurtrack(prodout, purin, purout, wo);
                    break;

                case "Crib":
                    checkCribtrack(prodout, cribin, cribout, wo);
                    break;
            }
        }

        private void checkprodtrack(string engin, string prodin, string prodout, string wo)
        {
            if (engin == "1")
            {
                if (prodin == "0")
                {
                    // insert into production
                    UpdateWOTracking(wo, "Prodin", "ProdinWho", "ProdinWhen", "1", getuserfullname(), "InProduction");
                    MessageBox.Show("Work Order Checked into Production.", "SPM Connect  - Work Order In Production", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (prodout == "0")
                    {
                        // insert into production checkout
                        UpdateWOTracking(wo, "Prodout", "ProdoutWho", "ProdoutWhen", "1", getuserfullname(), "OutProduction");
                        MessageBox.Show("Work Order Checked out of Production.", "SPM Connect  - Work Order out Production", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // this workorder has been already checkout from your system
                        MessageBox.Show("Work order has been already checked out from Production department.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                //Work order not has been initialized by eng 
                MessageBox.Show("Work order has not been initialized in the system by Engineering", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkpurtrack(string prodout, string purin, string purout, string wo)
        {
            if (prodout == "1")
            {
                if (purin == "0")
                {
                    // insert into purchasing
                    UpdateWOTracking(wo, "Purin", "PurinWho", "PurinWhen", "1", getuserfullname(), "InPurchase");
                    MessageBox.Show("Work Order Checked into Purchasing.", "SPM Connect  - Work Order In Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (purout == "0")
                    {
                        // insert into pruchase checkout
                        UpdateWOTracking(wo, "Purout", "PuroutWho", "PuroutWhen", "1", getuserfullname(), "OutPurchase");
                        MessageBox.Show("Work Order Checked out of Purchasing.", "SPM Connect  - Work Order out Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // this wo has already been checkout of system
                        MessageBox.Show("Work order has been already checkout from Purchasing department.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                //Work order not has been initialized by production 
                MessageBox.Show("Work order has not been checkout from production", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkCribtrack(string prodout, string cribin, string cribout, string wo)
        {
            if (prodout == "1")
            {
                if (cribin == "0")
                {
                    // insert into crib
                    UpdateWOTracking(wo, "Cribin", "CribinWho", "CribinWhen", "1", getuserfullname(), "InCrib");
                    MessageBox.Show("Work Order Checked into Crib.", "SPM Connect  - Work Order In Crib", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (cribout == "0")
                    {
                        // insert into crib checkout
                        UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", getuserfullname(), "Completed");
                        MessageBox.Show("Work Order Checked out of Crib.", "SPM Connect  - Work Order out Crib", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // this wo has already been checkout of system
                        MessageBox.Show("Work order has been closed from the system.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                //Work order not has been initialized by production 
                MessageBox.Show("Work order has not been checkout from production", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
                
    }
}
