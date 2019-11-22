using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class WorkOrder
    {
        #region User Details and connections

        private SqlConnection cn;

        public WorkOrder()
        {
            SPM_Connect();
        }

        private void SPM_Connect()
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

        public string Getassyversionnumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = "V" + assembly.GetName().Version.ToString(3);
            return version;
        }

        public string Getuserfullname()
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

        public string Getempid()
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

        public string Getdepartment()
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

        public bool CheckRights(string Module)
        {
            bool yeswoscan = false;
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND " + Module + " = '1'", cn))
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

        public string Getsharesfolder()
        {
            string path = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + UserName() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    path = dr["SharesFolder"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Error Getting share folder path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return path;
        }

        public string Getuserfullname(string empid)
        {
            string fullname = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [Emp_Id]='" + empid + "' ";
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

        public int Getuserinputtime()
        {
            int timer = 0;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'InputTime'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get User Input Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            if (limit.Length > 0)
            {
                timer = Convert.ToInt32(limit);
            }
            return timer;
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

        #endregion User Details and connections

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

        public void Scanworkorder(string wo)
        {
            string department = Getdepartment();
            if (WoExistsOnWotrack(wo))
            {
                if (department == "Eng" || department == "Controls")
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
                {
                    if (department == "Eng")
                    {
                        Enterwototrackeng(wo, "Eng. Released");
                    }
                    else if (department == "Controls")
                    {
                        Enterwototrackcontrols(wo, "Controls Released");
                    }
                }
                else
                {
                    if (department == "Eng" || department == "Controls")
                    {
                        MessageBox.Show("Please check the work order number.", "Work Order not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Work order has not been initialized in the system by Engineering/Controls", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Enterwototrackeng(string wo, string dept)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = Getuserfullname();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WO_Tracking] (WO, Engin, EngWho, EngWhen, Status) VALUES('" + wo + "','1','" + username + "','" + sqlFormattedDatetime + "','" + dept + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Work Order Checked in.", "SPM Connect  - Work Order In Eng", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Enterwototrackcontrols(string wo, string dept)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = Getuserfullname();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WO_Tracking] (WO, Ctrlin, CtrlWho, CtrlWhen, Status) VALUES('" + wo + "','1','" + username + "','" + sqlFormattedDatetime + "','" + dept + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Work Order Checked in.", "SPM Connect  - Work Order In Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Findwhatstagetoinsert(engin, Prodin, Prodout, Purin, Purout, Cribin, Cribout, department, wo);
        }

        private void Findwhatstagetoinsert(string engin, string prodin, string prodout, string purin, string purout, string cribin, string cribout, string department, string wo)
        {
            switch (department)
            {
                case "Production":
                    Checkprodtrack(engin, prodin, prodout, wo);
                    break;

                case "Purchasing":
                    Checkpurtrack(prodout, purin, purout, wo);
                    break;

                case "Crib":
                    CheckCribtrack(prodout, cribin, cribout, purout, wo);
                    break;
            }
        }

        private void Checkprodtrack(string engin, string prodin, string prodout, string wo)
        {
            if (engin == "1")
            {
                if (prodin == "0")
                {
                    // insert into production
                    if (UpdateWOTracking(wo, "Prodin", "ProdinWho", "ProdinWhen", "1", Getuserfullname(), "InProduction"))
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
                            if (UpdateWOTracking(wo, "Prodout", "ProdoutWho", "ProdoutWhen", "1", Getuserfullname(), "OutProduction"))
                            {
                                string timespan = Calculatetimedifference(Fetchdatetime("ProdoutWhen", wo), Fetchdatetime("ProdinWhen", wo));
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
                MessageBox.Show("Work order has not been initialized in the system by Engineering or Controls department.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Checkpurtrack(string prodout, string purin, string purout, string wo)
        {
            string status = "";
            status = WOStatustostring(wo);
            if (prodout == "1")
            {
                if (purin == "0")
                {
                    // insert into purchasing

                    if (UpdateWOTracking(wo, "Purin", "PurinWho", "PurinWhen", "1", Getuserfullname(), status == "" ? "In Purchasing" : status + " & In Purchasing"))
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
                            if (UpdateWOTracking(wo, "Purout", "PuroutWho", "PuroutWhen", "1", Getuserfullname(), status == "" || status == "In Purchasing" ? "Out Purchasing" : status + " & Out Purchasing"))
                            {
                                string timespan = Calculatetimedifference(Fetchdatetime("PuroutWhen", wo), Fetchdatetime("PurinWhen", wo));
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

        private void CheckCribtrack(string prodout, string cribin, string cribout, string purout, string wo)
        {
            string status = "";
            status = WOStatustostring(wo);
            if (prodout == "1")
            {
                if (cribin == "0")
                {
                    // insert into crib
                    if (UpdateWOTracking(wo, "Cribin", "CribinWho", "CribinWhen", "1", Getuserfullname(), status == "" ? "In Crib" : status + " & In Crib"))
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
                                        if (UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", Getuserfullname(), "Completed"))
                                        {
                                            //status = RemoveStatus(status, "In Crib");
                                            // UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", getuserfullname(), status == "" ? "Out Crib" : status + " & Out Crib");
                                            string timespan = Calculatetimedifference(Fetchdatetime("CriboutWhen", wo), Fetchdatetime("CribinWhen", wo));
                                            UpdateWOTrackingTimeSpan(wo, "TimeInCrib", timespan);
                                            timespan = Calculatetimedifference(Fetchdatetime("CriboutWhen", wo), Fetchdatetime("EngWhen", wo));
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

        private DateTime Fetchdatetime(string columname, string wo)
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

        private string Calculatetimedifference(DateTime A, DateTime B)
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

        public DataTable ShowWOInOutStatus(string wo)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WOInOutStatus] WHERE WO = '" + wo + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show All Work Status In Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

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
            string username = Getuserfullname();
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
            string username = Getuserfullname();
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

        #endregion Wo In out

        #region Material ReAllocation

        private string Getnewinvoicenumber()
        {
            string newincoiveno = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX([InvoiceNo]) + 1 as NextQuoteNo FROM [SPM_Database].[dbo].[MaterialReallocation]";
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

            if (newincoiveno == "")
            {
                newincoiveno = "1001";
            }

            return newincoiveno;
        }

        public string CreateNewMatReallocation()
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = Getuserfullname();
            string newinvoiceno = Getnewinvoicenumber();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[MaterialReallocation] (InvoiceNo, DateCreated, CreatedBy, LastSavedOn, LastSavedBy) VALUES('" + newinvoiceno + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = newinvoiceno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create New Mat Reallocation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public DataTable ShowMatInvoice(string invoiceno)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[MaterialReallocation] WHERE InvoiceNo = '" + invoiceno + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Material Reallocation Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public bool UpdateInvoiceDetsToSql(string inovicenumber, string notes, string itemid, string description, string oem, string oemitem, string empid, string empname, string appid, string appname, string jobreq, string woreq, string jobtaken, string wotaken, string qty)
        {
            bool success = false;
            string username = Getuserfullname();
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[MaterialReallocation] SET [LastSavedOn] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "'," +
                    "[Notes] = '" + notes + "',[ItemId] = '" + itemid + "',[Description] = '" + description + "',[OEM] = '" + oem + "',[OEMItem] = '" + oemitem + "'," +
                    "[EmployeeId] = '" + empid + "',[EmployeeName] = '" + empname + "',[ApprovedId] = '" + appid + "',[ApprovedName] =  '" + appname + "',[JobReq] = '" + jobreq + "'," +
                    "[WOReq] = '" + woreq + "',[JobTaken] = '" + jobtaken + "',[WOTaken] = '" + wotaken + "',[Qty] = '" + qty + "'  WHERE [InvoiceNo] = '" + inovicenumber + "' ";

                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Invoice Details Material - Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
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

        public DataTable ShowAllAlocations()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[MaterialReallocation]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show all Material Allocations Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        #region Fill Comboboxes

        public AutoCompleteStringCollection FillItems()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ItemId] from [dbo].[MaterialReallocation] where [ItemId] is not null order by [ItemId]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Items ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return MyCollection;
        }

        public AutoCompleteStringCollection FillRequestedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [EmployeeName] from [dbo].[MaterialReallocation] where EmployeeName is not null order by EmployeeName", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Requested By", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillApprovedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ApprovedName] from [dbo].[MaterialReallocation] where ApprovedName is not null order by ApprovedName", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Approved By", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillWorkOrderReq()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [WOReq] from [dbo].[MaterialReallocation] where WOReq is not null order by WOReq", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill WO Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillJobTakenFrom()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [JobTaken] from [dbo].[MaterialReallocation] where JobTaken is not null order by JobTaken", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Job Taken From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection Filljobreq()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [JobReq] from [dbo].[MaterialReallocation] where JobReq is not null order by JobReq", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Job Req", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillWorkOrderTaken()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [WOTaken] from [dbo].[MaterialReallocation] where WOTaken is not null order by WOTaken", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Work Order Taken From", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        #endregion Fill Comboboxes

        #endregion Material ReAllocation

        #region BinStatusLog

        public DataTable ShowDistinctWO()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT DISTINCT WO FROM [SPM_Database].[dbo].[WO_Tracking] ORDER BY WO DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show All Work Orders On InvInOut", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable ShowWOStatusBinWithEMPName()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter(" SELECT * FROM [SPM_Database].[dbo].[WO_BinStatus] ORDER BY WO DESC, Id DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show All Work Order Status With Emp Names", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable ShowWODetails(string wo)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WOInOutStatus] WHERE WO = '" + wo + "'ORDER BY WO DESC, Id DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show Work Order Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable ShowWOtrackingProg(string wo)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WO_Tracking] WHERE WO = '" + wo + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show Work Order Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        #endregion BinStatusLog

        public void Sendemail(string emailtosend, string subject, string body, string filetoattach, string cc)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("spmautomation-com0i.mail.protection.outlook.com");
                message.From = new MailAddress("connect@spm-automation.com", "SPM Connect");
                System.Net.Mail.Attachment attachment;
                message.To.Add(emailtosend);
                if (cc == "")
                {
                }
                else
                {
                    message.CC.Add(cc);
                }
                message.Subject = subject;
                message.Body = body;

                if (filetoattach == "")
                {
                }
                else
                {
                    attachment = new System.Net.Mail.Attachment(filetoattach);
                    message.Attachments.Add(attachment);
                }
                SmtpServer.Port = 25;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Send Email", MessageBoxButtons.OK);
            }
        }

        public List<string> getcribshortemails()
        {
            List<string> Happrovalnames = new List<string>();

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [CribShort] = '1' ";
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
                MessageBox.Show(ex.Message, "SPM Connect - Get User Name and Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return Happrovalnames;
        }

        #region WorkOrderRelease

        private string GetNewReleaseLogNo()
        {
            string newincoiveno = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX([RlogNo]) + 1 as NextQuoteNo FROM [SPM_Database].[dbo].[WOReleaseLog]";
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
                MessageBox.Show(ex.Message, "SPM Connect - Get New Release Log Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

            if (newincoiveno == "")
            {
                newincoiveno = "1001";
            }

            return newincoiveno;
        }

        private int GetNextReleaseNo(string wo)
        {
            int nextreleaseno = 0;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX([NxtReleaseNo])+1 as NextQuoteNo FROM [SPM_Database].[dbo].[WOReleaseLog] WHERE WO = '" + wo + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!(dr["NextQuoteNo"] is DBNull))
                            nextreleaseno = Convert.ToInt32(dr["NextQuoteNo"]);
                    }
                }

                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get New Release Log Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

            return nextreleaseno;
        }

        private string GetNextReleaseLabel(int nxtrelno)
        {
            string nextreleaseno = "";

            if (nxtrelno == 0)
            {
                nextreleaseno = "Initial Release";
            }
            else
            {
                nextreleaseno = "Release " + nxtrelno;
            }

            return nextreleaseno;
        }

        public bool CheckWoExistsOnReleaseLog(string wo, string releasetype)
        {
            bool wopresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[WOReleaseLog] WHERE [WO]='" + wo + "' AND [ReleaseType]='" + releasetype + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Check WO Present on Release Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return wopresent;
        }

        public string EnterWOToReleaseLog(string wo, string jobno, string assyno)
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = Getuserfullname();
            string releaselogno = GetNewReleaseLogNo();
            int nxtreaseno = GetNextReleaseNo(wo);

            string releasetype = GetNextReleaseLabel(nxtreaseno);

            if (!(CheckWoExistsOnReleaseLog(wo, releasetype)))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOReleaseLog] (RlogNo, WO,JobNo, AssyNo, ReleaseType, CreatedBy, CreatedOn, LastSavedBy, LastSaved, NxtReleaseNo)" +
                        " VALUES('" + releaselogno + "','" + wo + "','" + jobno + "','" + assyno + "','" + releasetype + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + nxtreaseno + "')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Work Order Checked in for new release.", "SPM Connect  - Work Order Release Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    success = releaselogno;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Add Work Order To Release Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = "";
                }
                finally
                {
                    cn.Close();
                    if (releasetype == "Initial Release")
                    {
                        CopyInitialItems(wo, assyno, releaselogno, jobno, releasetype);
                    }
                }
                return success;
            }
            else
            {
                // return error intial release done already
                MessageBox.Show("Work Order has been already checked into this release.", "SPM Connect  - Work Order In Release", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return success = "";
            }
        }

        public DataTable GrabReleaseLogDetails(string rlogno)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WOReleaseLog] WHERE [RlogNo] = '" + rlogno + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Release Log Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GrabReleaseLogsBOM(string job)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VReleaseBOM] WHERE [JobNo]= '" + job + "' ORDER BY CreatedOn DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Release Logs BIG BOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GrabReleaseLogs(string wo)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VReleaseLogs] WHERE [WO] = '" + wo + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Release Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable ShowAllReleaseLogs()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VReleaseLogs] ORDER BY WO DESC", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show All Work Orders Release Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GrabReleaseLogItemDetails(string wo, string rlogno)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[VWOReleaseDetails] WHERE [WO] = '" + wo + "' AND [RlogNo] = '" + rlogno + "'", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Release Log Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GrabJobWOBOM(string jobno)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[SPMConnectWOBOM] WHERE Job = '" + jobno + "' ", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get Work Order BIG BOM by Job", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public void AddItemToReleaseLog(string wo, string assyno, string rlogno, string itemno, string qty, string itemnotes, string jobno, string releasetype, string isrevised)
        {
            SPMSQLCommands connectAPI = new SPMSQLCommands();
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = connectAPI.getuserfullname();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOReleaseDetails] ([RlogNo],[ReleaseType], [JobNo],[WO],[AssyNo],[ItemId],[ItemQty],[ItemNotes]," +
                    "[IsRevised],[ItemCreatedOn], [ItemCreatedBy], [ItemLastSaved], [ItemLastSavedBy])" +
                    " VALUES('" + rlogno + "','" + releasetype + "','" + jobno + "','" + wo + "','" + assyno + "','" + itemno + "','" + qty + "','" + itemnotes + "','" + isrevised + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Error on Add item to Release log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public void Deleteassy(string rlogno)
        {
            string sql;
            sql = "DELETE FROM [SPM_Database].[dbo].[WOReleaseDetails] WHERE [RlogNo] = '" + rlogno + "' ";

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Error deleting items for release log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public bool UpdateReleaseLogNotes(string rlogno, string notes)
        {
            bool success = false;
            string username = Getuserfullname();
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[WOReleaseLog] SET [LastSaved] = '" + sqlFormattedDate + "',[LastSavedBy] = '" + username + "',[ReleaseNotes] = '" + notes + "' WHERE [RlogNo] = '" + rlogno + "' ";

                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect ReleaseLog - Update Notes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool UpdateBallonRefToEst(string itemId, string ballonref, string job, string assy)
        {
            bool success = false;
            string estId = GetEstId(job);

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPMDB].[dbo].[tcBomItemVersion] SET [Ballon] = '" + ballonref + "' WHERE [Item] = '" + itemId + "' AND [BomVersionId]  = '" + estId + "' AND [Produit] = '" + assy + "'";

                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Update Ballon Ref To Estimate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool UpdateBallonRefToWorkOrder(string itemId, string ballonref, string job, string assy, string wo)
        {
            bool success = false;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPMDB].[dbo].[Mrpres] SET [Ballon] = '" + ballonref + "' WHERE [Item] = '" + itemId + "' AND [Job]  = '" + job + "' AND [Piece] = '" + assy + "' AND [Woprec]  = '" + wo + "'";

                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Update Ballon Ref To Work Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public string GetEstId(string job)
        {
            string estId = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[SPMJobs] WHERE [Job]='" + job + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        estId = dr["EstId"].ToString();
                    }
                }

                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve Job Est Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return estId;
        }

        public void CopyInitialItems(string wo, string assyno, string rlogno, string jobno, string releasetype)
        {
            SPMSQLCommands connectAPI = new SPMSQLCommands();
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = connectAPI.getuserfullname();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOReleaseDetails] ([RlogNo],[WO],[ReleaseType], [JobNo], [AssyNo],[ItemId],[ItemQty],[ItemNotes],[IsRevised]," +
                    "[ItemCreatedOn], [ItemCreatedBy], [ItemLastSaved], [ItemLastSavedBy])" +
                    "SELECT'" + rlogno + "' as RlogNo,[Woprec],'" + releasetype + "' as ReleaseType,'" + jobno + "' as JobNo,[Piece],[Item],[Qte_Ass],'' as Notes,'3' as IsRevised,'" + sqlFormattedDatetime + "' as CreatedOn," +
                    "'" + username + "' as CreatedBy,'" + sqlFormattedDatetime + "' as LastSaved,'" + username + "' as LastSavedBy " +
                    "FROM [SPMDB].[dbo].[Mrpres] where Piece = '" + assyno + "' and Job = '" + jobno + "' and Woprec = '" + wo + "' and RML_Active = '1'";
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Error on Copy Initial Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public string GetJobAssyNo(string jobno)
        {
            string assnyno = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[SPMJobs] WHERE [Job]  = '" + jobno + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    assnyno = dr["BOMItem"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get Job Assy Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

            return assnyno;
        }

        public string GetJobNoFromWO(string wo)
        {
            string assnyno = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[SPMConnectWOBOM] WHERE [WO]  = '" + wo + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    assnyno = dr["Job"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get Job Number from WO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

            return assnyno;
        }

        #endregion WorkOrderRelease

        #region Checkin Checkout Check Invoice

        public string InvoiceOpen(string invoicenumber)
        {
            string username = "";

            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[UserHolding] WHERE [ItemId]='" + invoicenumber + "'AND App = 'WorkOrderRelease'", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        username = reader["UserName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check Right Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return username;
        }

        public bool CheckinInvoice(string invoicenumber)
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
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[UserHolding] (App, UserName, ItemId,CheckInDateTime) VALUES('WorkOrderRelease','" + UserName() + "','" + invoicenumber + "','" + sqlFormattedDatetime + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Check in invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool CheckoutInvoice(string invoicenumber)
        {
            bool success = false;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[UserHolding] where App = 'WorkOrderRelease' AND UserName = '" + UserName() + "' AND ItemId = '" + invoicenumber + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Check out invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        #endregion Checkin Checkout Check Invoice
    }
}