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

        public string getempid()
        {
            string empid = "";
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
                    empid = dr["Emp_Id"].ToString();

                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve employee id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return empid;
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

        public bool CheckScanRights()
        {
            bool yeswoscan = false;
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND WOScan = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        yeswoscan = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve Wo Scan rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return yeswoscan;
        }

        #endregion

        public DataTable ShowAllWorkOrders()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WorkOrderManagement] ORDER BY Job DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show All Work Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        public DataTable ShowWOTrackingStatus(string wo)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT WO, Status FROM [SPM_Database].[dbo].[WO_Tracking] WHERE [WO] = '" + wo + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Tracking Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }

        private string WOStatustostring(string wo)
        {
            string status = "";
            try
            {
                DataTable dt = new DataTable();
                dt = GetWOInfo(wo);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Purin"].ToString() == "1" || dr["Cribin"].ToString() == "1")
                        status = dr["Status"].ToString();

                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve WO Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return status;
        }

        public void scanworkorder(string wo)
        {
            string department = getdepartment();
            if (WoExistsOnWotrack(wo))
            {
                if (department == "Eng")
                {
                    MessageBox.Show("Work order has already been entered into the system", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (department == "Controls")
                {
                    MessageBox.Show("Your Department does not belong to the work order tracking module.", "SPM Connect - Department Not Found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    DoWorkOrderTacking(wo, department);
                }
            }
            else
            {
                // workorder not started into the system
                if (WOReleased(wo) && department == "Eng")
                    enterwototrack(wo);
                else
                {
                    if (department == "Eng")
                    {
                        MessageBox.Show("Please check the work order number.", "Work Order not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Work order has not been initialized in the system by Engineering", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
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
                    checkCribtrack(prodout, cribin, cribout, purout, wo);
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
                    if (UpdateWOTracking(wo, "Prodin", "ProdinWho", "ProdinWhen", "1", getuserfullname(), "InProduction"))
                        MessageBox.Show("Work Order Checked into Production.", "SPM Connect  - Work Order In Production", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 385", "SPM Connect - Error Work Order In Production", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (prodout == "0")
                    {
                        // insert into production checkout
                        DialogResult result = MessageBox.Show("Check out this work order from production?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {

                            if (UpdateWOTracking(wo, "Prodout", "ProdoutWho", "ProdoutWhen", "1", getuserfullname(), "OutProduction"))
                            {
                                string timespan = calculatetimedifference(fetchdatetime("ProdoutWhen", wo), fetchdatetime("ProdinWhen", wo));
                                UpdateWOTrackingTimeSpan(wo, "TimeInProd", timespan);
                                MessageBox.Show("Work Order Checked out of Production.", "SPM Connect  - Work Order out Production", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 404", "SPM Connect - Error Work Order Out Production", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

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
                MessageBox.Show("Work order has not been initialized in the system by Engineering", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void checkpurtrack(string prodout, string purin, string purout, string wo)
        {
            string status = "";
            status = WOStatustostring(wo);
            if (prodout == "1")
            {
                if (purin == "0")
                {
                    // insert into purchasing

                    if (UpdateWOTracking(wo, "Purin", "PurinWho", "PurinWhen", "1", getuserfullname(), status == "" ? "In Purchasing" : status + " & In Purchasing"))
                    {
                        MessageBox.Show("Work Order Checked into Purchasing.", "SPM Connect  - Work Order In Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 439", "SPM Connect - Error Work Order In Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (purout == "0")
                    {
                        // insert into pruchase checkout
                        DialogResult result = MessageBox.Show("Check out this work order from Purchasing?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            status = RemoveStatus(status, "In Purchasing");
                            if (UpdateWOTracking(wo, "Purout", "PuroutWho", "PuroutWhen", "1", getuserfullname(), status == "" || status == "In Purchasing" ? "Out Purchasing" : status + " & Out Purchasing"))
                            {
                                string timespan = calculatetimedifference(fetchdatetime("PuroutWhen", wo), fetchdatetime("PurinWhen", wo));
                                UpdateWOTrackingTimeSpan(wo, "TimeInPur", timespan);
                                MessageBox.Show("Work Order Checked out of Purchasing.", "SPM Connect  - Work Order out Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 459", "SPM Connect - Error Work Order Out Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

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
                MessageBox.Show("Work order has not been checked out from production", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void checkCribtrack(string prodout, string cribin, string cribout, string purout, string wo)
        {
            string status = "";
            status = WOStatustostring(wo);
            if (prodout == "1")
            {
                if (cribin == "0")
                {
                    // insert into crib
                    if (UpdateWOTracking(wo, "Cribin", "CribinWho", "CribinWhen", "1", getuserfullname(), status == "" ? "In Crib" : status + " & In Crib"))
                    {
                        MessageBox.Show("Work Order Checked into Crib.", "SPM Connect  - Work Order In Crib", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 493", "SPM Connect - Error Work Order In Crib", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (cribout == "0")
                    {
                        // insert into crib checkout
                        DialogResult result = MessageBox.Show("Check out this work order from Crib?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {

                            if (purout == "1")
                            {
                                if (CheckWoExistsOnInvInOut(wo))
                                {
                                    if (IsCompletedInvInOut(wo))
                                    {
                                        if (UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", getuserfullname(), "Completed"))
                                        {
                                            //status = RemoveStatus(status, "In Crib");
                                            // UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", getuserfullname(), status == "" ? "Out Crib" : status + " & Out Crib");      
                                            string timespan = calculatetimedifference(fetchdatetime("CriboutWhen", wo), fetchdatetime("CribinWhen", wo));
                                            UpdateWOTrackingTimeSpan(wo, "TimeInCrib", timespan);
                                            timespan = calculatetimedifference(fetchdatetime("CriboutWhen", wo), fetchdatetime("EngWhen", wo));
                                            UpdateWOTrackingTimeSpan(wo, "TotalTimeSpent", timespan);
                                            MessageBox.Show("Work Order Checked out of Crib.", "SPM Connect  - Work Order out Crib", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 459", "SPM Connect - Error Work Order Out Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Work order built is not completed. Make sure the work order build is completed in order to close the workorder", "SPM Connect - Error Work Order Build Not Completed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Work order never got built. Add to build to close the workorder", "SPM Connect - Error Work Order Build Not Started", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("System cannot check out work order from crib as it has not been scanned out of purchasing.", "SPM Connect  - Work Order Not Scanned Out Of Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
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
                MessageBox.Show("Work order has not been checked out from production", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string RemoveStatus(string status, string requestby)
        {
            string setstatus = "";
            string incrib = requestby;
            // Split string on spaces (this will separate all the words).
            string[] words = status.Split('&');
            foreach (string word in words)
            {
                if (word.Trim() == incrib)
                {

                }
                else
                {
                    setstatus += word.Trim();
                    if (word.Trim() != "")
                        setstatus += "";
                }
            }
            return setstatus.Trim();
        }

        private DateTime fetchdatetime(string columname, string wo)
        {
            DateTime dateTime;

            dateTime = DateTime.Now;
            try
            {
                DataTable dt = new DataTable();
                dt = GetWOInfo(wo);

                foreach (DataRow dr in dt.Rows)
                {
                    dateTime = Convert.ToDateTime(dr[columname].ToString());
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to extract DateTime", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dateTime;
        }

        private string calculatetimedifference(DateTime A, DateTime B)
        {
            string timediff;
            TimeSpan span = (A - B);

            timediff = string.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
               span.Days, span.Hours, span.Minutes, span.Seconds);

            return timediff;
        }

        private bool UpdateWOTrackingTimeSpan(string wo, string parameter1, string para1value1)
        {
            bool success = false;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[WO_Tracking] SET " + parameter1 + " = '" + para1value1 + "'  WHERE WO = '" + wo + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update WO Tracking Timespent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        #region Wo In out

        public bool CheckWoExistsOnInvInOut(string wo)
        {
            bool wopresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WOInOut] WHERE [WO]='" + wo + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        wopresent = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check WO Present on Inv CheckInOut", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();

                }
            }
            return wopresent;
        }

        public bool IsCompletedInvInOut(string wo)
        {
            bool completed = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WOInOut] WHERE [WO]='" + wo + "' and [Completed] = '1'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        completed = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check WO Completed Build", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return completed;
        }

        public bool EmployeeExits(string empid)
        {
            bool empexists = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE [Emp_Id]='" + empid + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        empexists = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check EmployeeExists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return empexists;
        }

        public bool EmployeeExitsWithCribRights(string empid)
        {
            bool empexists = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE [Emp_Id]='" + empid + "' and [CribCheckout] = '1' ", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        empexists = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check Employee With Crib Rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return empexists;
        }

        public bool CheckWOIntoCrib(string wo)
        {
            bool wopresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WO_Tracking] WHERE [WO]='" + wo + "' and [Cribin] = '1'", cn))
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

        public void ChekOutWOOutForBuilt(string wo, string empid, string approvalid)
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
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOInOut] (WO, Emp_idCheckOut, PunchIn, CheckOutApprovedBy,InBuilt) VALUES('" + wo + "','" + empid + "','" + sqlFormattedDatetime + "','" + approvalid + "','1')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Work Order Checked out for built.", "SPM Connect  - Work Order Checked Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Check out wo for built", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public void CheckWOInFromBuilt(string wo, string empid, string approvalid, string completed)
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
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[WOInOut] SET [Emp_idCheckIn] = '" + empid + "', [PunchOut] = '" + sqlFormattedDatetime + "',[CheckInApprovedBy] = '" + approvalid + "',[InBuilt] = '0', [Completed] = '" + completed + "'  WHERE WO = '" + wo + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Work Order Checked into Crib.", "SPM Connect  - Work Order Checked In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Check in WO into Crib", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public bool InBuiltInvInOut(string wo)
        {
            bool completed = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WOInOut] WHERE [WO]='" + wo + "' and [InBuilt] = '1'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        completed = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check WO Completed Build", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return completed;
        }

        #endregion

    }
}
