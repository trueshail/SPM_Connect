﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SPMConnectAPI
{
    public class WorkOrder : ConnectAPI
    {
        #region User Details and connections

        private readonly log4net.ILog log;

        public WorkOrder()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Accessed Work Order Class " + Getassyversionnumber());
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

        #endregion User Details and connections

        public string CaptureLocation()
        {
            bool verfiedloc = false;
            string location;
            do
            {
                location = Interaction.InputBox("Enter the location where this work order bin is going to be.", "SPM Connect", "");

                if (location.Length > 0)
                {
                    verfiedloc = true;
                }
                else
                {
                    MessageBox.Show("Invalid location. Please try again", "SPM Connect - Capture Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } while (!verfiedloc);

            return location;
        }

        public void Scanworkorder(string wo)
        {
            Department department = ConnectUser.Dept;
            if (WoExistsOnWotrack(wo))
            {
                if (department == Department.Eng || department == Department.Controls)
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
                    if (department == Department.Eng)
                    {
                        Enterwototrackeng(wo, "Eng. Released");
                    }
                    else if (department == Department.Controls)
                    {
                        Enterwototrackcontrols(wo, "Controls Released");
                    }
                    else
                    {
                        MessageBox.Show("Work order has to be initialized in the system by Engineering/Controls department", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (department == Department.Eng || department == Department.Controls)
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

        public bool UpdateWOBinCribLocation(string wo, string location)
        {
            bool success = false;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[WO_Tracking] SET  [CribLocation] = '" + location + "'  WHERE WO = '" + wo + "'";
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Update WO Crib location", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
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

        private string Calculatetimedifference(DateTime A, DateTime B)
        {
            TimeSpan span = (A - B);
            return string.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
                           span.Days, span.Hours, span.Minutes, span.Seconds);
        }

        private void CheckCribtrack(string prodout, string cribin, string cribout, string purout, string wo)
        {
            string location = CaptureLocation();
            string status = WOStatustostring(wo);
            if (prodout == "1")
            {
                if (cribin == "0")
                {
                    // insert into crib
                    if (UpdateWOTracking(wo, "Cribin", "CribinWho", "CribinWhen", "1", ConnectUser.Name, string.IsNullOrEmpty(status) ? "In Crib" : status + " & In Crib"))
                    {
                        UpdateWOBinCribLocation(wo, location);
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
                        if (purout == "1")
                        {
                            // insert into crib checkout
                            //DialogResult result = MessageBox.Show("Check out this work order from Crib?", "Check Out WO?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            //if (result == DialogResult.Yes)
                            //{

                            if (CheckWoExistsOnInvInOut(wo))
                            {
                                if (IsCompletedInvInOut(wo))
                                {
                                    if (UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", ConnectUser.Name, "Completed"))
                                    {
                                        //status = RemoveStatus(status, "In Crib");
                                        // UpdateWOTracking(wo, "Cribout", "CriboutWho", "CriboutWhen", "1", user.Name, status == "" ? "Out Crib" : status + " & Out Crib");
                                        string timespan = Calculatetimedifference(Fetchdatetime("CriboutWhen", wo), Fetchdatetime("CribinWhen", wo));
                                        UpdateWOTrackingTimeSpan(wo, "TimeInCrib", timespan);
                                        timespan = Calculatetimedifference(Fetchdatetime("CriboutWhen", wo), Fetchdatetime("EngWhen", wo));
                                        UpdateWOTrackingTimeSpan(wo, "TotalTimeSpent", timespan);
                                        MessageBox.Show("Work Order Checked out of Crib.", "SPM Connect  - Work Order out Crib", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error Updating WO Tracking. Please contact the admin for line 282", "SPM Connect - Error Work Order Out Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                            //}
                        }
                        else
                        {
                            MessageBox.Show("System cannot check out work order from crib as it has not been scanned out of purchasing.", "SPM Connect  - Work Order Not Scanned Out Of Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void Checkprodtrack(string engin, string cntrlin, string prodin, string prodout, string wo)
        {
            if (engin == "1" || cntrlin == "1")
            {
                if (prodin == "0")
                {
                    // insert into production
                    if (UpdateWOTracking(wo, "Prodin", "ProdinWho", "ProdinWhen", "1", ConnectUser.Name, "InProduction"))
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
                            if (UpdateWOTracking(wo, "Prodout", "ProdoutWho", "ProdoutWhen", "1", ConnectUser.Name, "OutProduction"))
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
            string status = WOStatustostring(wo);
            if (prodout == "1")
            {
                if (purin == "0")
                {
                    // insert into purchasing

                    if (UpdateWOTracking(wo, "Purin", "PurinWho", "PurinWhen", "1", ConnectUser.Name, string.IsNullOrEmpty(status) ? "In Purchasing" : status + " & In Purchasing"))
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
                            if (UpdateWOTracking(wo, "Purout", "PuroutWho", "PuroutWhen", "1", ConnectUser.Name, string.IsNullOrEmpty(status) || status == "In Purchasing" ? "Out Purchasing" : status + " & Out Purchasing"))
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

        private void DoWorkOrderTacking(string wo, Department department)
        {
            DataTable woinfo = new DataTable();
            woinfo.Clear();
            woinfo = GetWOInfo(wo);
            DataRow r = woinfo.Rows[0];
            string engin = r["Engin"].ToString();
            string cntrlin = r["Ctrlin"].ToString();
            string Prodin = r["Prodin"].ToString();
            string Prodout = r["Prodout"].ToString();
            string Purin = r["Purin"].ToString();
            string Purout = r["Purout"].ToString();
            string Cribin = r["Cribin"].ToString();
            string Cribout = r["Cribout"].ToString();
            Findwhatstagetoinsert(engin, cntrlin, Prodin, Prodout, Purin, Purout, Cribin, Cribout, department, wo);
        }

        private void Enterwototrackcontrols(string wo, string dept)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WO_Tracking] (WO, Ctrlin, CtrlWho, CtrlWhen, Status) VALUES('" + wo + "','1','" + username + "','" + sqlFormattedDatetime + "','" + dept + "')";
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Work Order Checked in.", "SPM Connect  - Work Order In Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Enterwototrackeng(string wo, string dept)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WO_Tracking] (WO, Engin, EngWho, EngWhen, Status) VALUES('" + wo + "','1','" + username + "','" + sqlFormattedDatetime + "','" + dept + "')";
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Work Order Checked in.", "SPM Connect  - Work Order In Eng", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private DateTime Fetchdatetime(string columname, string wo)
        {
            DateTime dateTime = DateTime.Now;
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

        private void Findwhatstagetoinsert(string engin, string cntrlin, string prodin, string prodout, string purin, string purout, string cribin, string cribout, Department department, string wo)
        {
            switch (department)
            {
                case Department.Production:
                    Checkprodtrack(engin, cntrlin, prodin, prodout, wo);
                    break;

                case Department.Purchasing:
                    Checkpurtrack(prodout, purin, purout, wo);
                    break;

                case Department.Crib:
                    CheckCribtrack(prodout, cribin, cribout, purout, wo);
                    break;
            }
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

        private string RemoveStatus(string status, string requestby)
        {
            string setstatus = "";
            string incrib = requestby;
            // Split string on spaces (this will separate all the words).
            string[] words = status.Split('&');
            foreach (string word in words)
            {
                if (word.Trim() != incrib)
                {
                    setstatus += word.Trim();
                    if (word.Trim() != "")
                        setstatus += "";
                }
            }
            return setstatus.Trim();
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

        public void CheckWOInFromBuilt(string wo, string empid, string approvalid, string completed)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            _ = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[WOInOut] SET [Emp_idCheckIn] = '" + empid + "', [PunchOut] = '" + sqlFormattedDatetime + "',[CheckInApprovedBy] = '" + approvalid + "',[InBuilt] = '0', [Completed] = '" + completed + "' WHERE WO = '" + wo + "'";
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

        public void ChekOutWOOutForBuilt(string wo, string empid, string approvalid, string takeoutlocation)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            _ = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOInOut] (WO, Emp_idCheckOut, PunchIn, CheckOutApprovedBy,InBuilt,TakeOutLocation) VALUES('" + wo + "','" + empid + "','" + sqlFormattedDatetime + "','" + approvalid + "','1', '" + takeoutlocation + "')";
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

        #endregion Wo In out

        #region Material ReAllocation

        public string CreateNewMatReallocation()
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
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

        public DataTable GetIteminfo(string itemnumber)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber] = '" + itemnumber + "'", cn))
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
            string username = ConnectUser.Name;
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

            if (string.IsNullOrEmpty(newincoiveno))
            {
                newincoiveno = "1001";
            }

            return newincoiveno;
        }

        #region Fill Comboboxes

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

        public AutoCompleteStringCollection FillReleaseApprovedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ApprovedBy] FROM [SPM_Database].[dbo].[RP_Base] where [ApprovedBy] is not null  AND ApprovedBy <> '' order by [ApprovedBy]", cn))
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

        public AutoCompleteStringCollection FillReleaseCheckedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [CheckedBy] FROM [SPM_Database].[dbo].[RP_Base] where [CheckedBy] is not null  AND ApprovedBy <> '' order by [CheckedBy]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Checked By", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillReleaseReleasedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ReleasedBy] FROM [SPM_Database].[dbo].[RP_Base] where [ReleasedBy] is not null  AND ApprovedBy <> '' order by [ReleasedBy]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Checked By", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillReleaseLastCreatedBy(bool createdby)
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();

            using (SqlCommand sqlCommand = new SqlCommand("[RP_BaseNames]", cn) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                sqlCommand.Parameters.AddWithValue("@createdby", createdby ? 1 : 0);
                sqlCommand.Parameters.AddWithValue("@lastsavedby", createdby ? 0 : 1);
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill LastSavedBy & CreatedBy", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region WorkOrderRelease

        public void AddItemToReleaseLog(string wo, string assyno, string rlogno, string itemno, string qty, string itemnotes, string jobno, string releasetype, string isrevised, string order)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOReleaseDetails] ([RlogNo],[ReleaseType], [JobNo],[WO],[AssyNo],[ItemId],[ItemQty],[ItemNotes]," +
                    "[IsRevised],[ItemCreatedOn], [ItemCreatedBy], [ItemLastSaved], [ItemLastSavedBy], [Order])" +
                    " VALUES('" + rlogno + "','" + releasetype + "','" + jobno + "','" + wo + "','" + assyno + "','" + itemno + "','" + qty + "','" + itemnotes + "','" + isrevised + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "','" + order + "')";
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

        public bool CheckItemExistsOnEst(string itemId, string job, string assy)
        {
            bool success = false;
            string estId = GetEstId(job);

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPMDB].[dbo].[tcBomItemVersion]  WHERE [Item] = '" + itemId + "' AND [BomVersionId]  = '" + estId + "' AND [Produit] = '" + assy + "'";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    success = true;
                }
                dt.Clear();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Check item exists on Estimate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool CheckItemExistsOnWorkOrder(string itemId, string job, string assy, string wo)
        {
            bool success = false;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPMDB].[dbo].[Mrpres] WHERE [Item] = '" + itemId + "' AND [Job]  = '" + job + "' AND [Piece] = '" + assy + "' AND [Woprec]  = '" + wo + "'";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    success = true;
                }
                dt.Clear();
                cn.Close();
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

        public void CopyInitialItems(string wo, string assyno, string rlogno, string jobno, string releasetype)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOReleaseDetails] ([RlogNo],[WO],[ReleaseType], [JobNo], [AssyNo],[ItemId],[ItemQty],[ItemNotes],[IsRevised]," +
                    "[ItemCreatedOn], [ItemCreatedBy], [ItemLastSaved], [ItemLastSavedBy],[Order])" +
                    "SELECT'" + rlogno + "' as RlogNo,[Woprec],'" + releasetype + "' as ReleaseType,'" + jobno + "' as JobNo,[Piece],[Item],[Qte_Ass],'' as Notes,'3' as IsRevised,'" + sqlFormattedDatetime + "' as CreatedOn," +
                    "'" + username + "' as CreatedBy,'" + sqlFormattedDatetime + "' as LastSaved,'" + username + "' as LastSavedBy,[Ordre] " +
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

        public void CopyNewReleaseItems(string wo, string assyno, string rlogno, string jobno, string releasetype, string itemno, string qty, string order)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[WOReleaseDetails] ([RlogNo],[WO],[ReleaseType], [JobNo], [AssyNo],[ItemId],[ItemQty],[ItemNotes],[IsRevised]," +
                    "[ItemCreatedOn], [ItemCreatedBy], [ItemLastSaved], [ItemLastSavedBy], [Order])" +
                    "VALUES('" + rlogno + "','" + wo + "','" + releasetype + "','" + jobno + "','" + assyno + "','" + itemno + "','" + qty + "','','1','" + sqlFormattedDatetime + "'," +
                    "'" + username + "','" + sqlFormattedDatetime + "','" + username + "','" + order + "')";

                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Error on Copy New Release Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public void Deleteassy(string rlogno)
        {
            string sql = "DELETE FROM [SPM_Database].[dbo].[WOReleaseDetails] WHERE [RlogNo] = '" + rlogno + "' ";

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

        public string EnterWOToReleaseLog(string wo, string jobno, string assyno)
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = ConnectUser.Name;
            string releaselogno = GetNewReleaseLogNo();
            int nxtreaseno = GetNextReleaseNo(wo);

            string releasetype = GetNextReleaseLabel(nxtreaseno);

            if (!CheckWoExistsOnReleaseLog(wo, releasetype))
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
                        Scanworkorder(wo);
                    }
                    else
                    {
                        _ = new DataTable();
                        DataTable dt = GrabReleaseSuggestions(wo, jobno, assyno);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Select())
                            {
                                CopyNewReleaseItems(wo, assyno, releaselogno, jobno, releasetype, dr["ItemId"].ToString(), dr["Qty"].ToString(), dr["Order"].ToString());
                                UpdateBallonRefToEst(dr["ItemId"].ToString(), releasetype, jobno, assyno);
                                UpdateBallonRefToWorkOrder(dr["ItemId"].ToString(), releasetype, jobno, assyno, wo, dr["Order"].ToString());
                            }
                        }
                    }
                }
                return success;
            }
            else
            {
                // return error intial release done already
                MessageBox.Show("Work Order has been already checked into this release.", "SPM Connect  - Work Order In Release", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "";
            }
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

        public string GetJobAssyNo(string jobno)
        {
            string assnyno = "";

            using (SqlCommand sqlCommand = new SqlCommand("SELECT [BOMItem] FROM [SPM_Database].[dbo].[SPMJobs] WHERE [Job]  = '" + jobno + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    assnyno = (string)sqlCommand.ExecuteScalar();

                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Job Assy Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return assnyno;
        }

        public string GetJobNoFromWO(string wo)
        {
            string assnyno = "";
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[SPMConnectWOBOM] WHERE [WO]  = '" + wo + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    assnyno = dt.Rows[0]["Job"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Job Number from WO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return assnyno;
        }

        public DataTable GrabJobWOBOM(string jobno)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[WorkOrderWithOperations] WHERE Job = '" + jobno + "' ORDER BY Priority ", cn))
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

        public DataTable GrabReleaseSuggestions(string wo, string job, string assyno)
        {
            DataTable dt = new DataTable();

            using (SqlCommand sqlCommand = new SqlCommand("GetReleaseSuggestions", cn) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                sqlCommand.Parameters.AddWithValue("@job", job);
                sqlCommand.Parameters.AddWithValue("@assyno", assyno);
                sqlCommand.Parameters.AddWithValue("@wo", wo);
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlCommand))
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
                        MessageBox.Show(ex.Message, "SPM Connect - Get Work Order Release Item Suggestions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            return dt;
        }

        public string GrabWOfromAssy(string jobno, string assyno)
        {
            string estId = "";
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[SPMConnectWOBOM] WHERE Job = '" + jobno + "' AND ItemNumber = '" + assyno + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    estId = dt.Rows[0]["Wo"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve Job workOrder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return estId;
        }

        public DataTable ShowAllReleaseLogs()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[RP_CRUDBase]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@StatementType", nameof(CRUDStatementType.SelectAll));

                try
                {
                    sda.Fill(dt);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBox.Show(ex.Message, "SPM Connect - Error Getting Release List From Solidworks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return dt;
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

        public bool UpdateBallonRefToWorkOrder(string itemId, string ballonref, string job, string assy, string wo, string order)
        {
            bool success = false;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (order == "5445")
                    cmd.CommandText = "UPDATE [SPMDB].[dbo].[Mrpres] SET [Ballon] = '" + ballonref + "' WHERE [Item] = '" + itemId + "' AND [Job]  = '" + job + "' AND [Piece] = '" + assy + "' AND [Woprec]  = '" + wo + "' AND RML_Active = '1'";
                else
                    cmd.CommandText = "UPDATE [SPMDB].[dbo].[Mrpres] SET [Ballon] = '" + ballonref + "' WHERE [Item] = '" + itemId + "' AND [Job]  = '" + job + "' AND [Piece] = '" + assy + "' AND [Woprec]  = '" + wo + "' AND [Ordre] = '" + order + "' AND RML_Active = '1'";
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

        public bool UpdateReleaseLogNotes(string rlogno, string notes)
        {
            bool success = false;
            string username = ConnectUser.Name;
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

        private string GetNewReleaseLogNo()
        {
            string newincoiveno = "";
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT MAX([RlogNo]) + 1 as NextQuoteNo FROM [SPM_Database].[dbo].[WOReleaseLog]", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    newincoiveno = dt.Rows[0]["NextQuoteNo"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get New Release Log Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            if (string.IsNullOrEmpty(newincoiveno))
            {
                newincoiveno = "1001";
            }

            return newincoiveno;
        }

        private string GetNextReleaseLabel(int nxtrelno)
        {
            if (nxtrelno == 0)
            {
                return "Initial Release";
            }
            else
            {
                return "Release " + nxtrelno;
            }
        }

        private int GetNextReleaseNo(string wo)
        {
            int nextreleaseno = 0;
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT MAX([NxtReleaseNo])+1 as NextQuoteNo FROM [SPM_Database].[dbo].[WOReleaseLog] WHERE WO = '" + wo + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
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
            }

            return nextreleaseno;
        }

        public ReleaseLog GetRelease(int relno)
        {
            ReleaseLog relLog = new ReleaseLog();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[RP_CRUDBase]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@RelNo", relno);
                sda.SelectCommand.Parameters.AddWithValue("@StatementType", nameof(CRUDStatementType.Select));

                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        relLog = GetReleaseDetails(dr, GetReleaseItemsDetails(relno), GetReleaseComments(relno));
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return relLog;
        }

        private ReleaseLog GetReleaseDetails(DataRow dr, List<ReleaseItem> releaseItems, List<ReleaseComment> releaseComments)
        {
            return new ReleaseLog
            {
                Id = Convert.ToInt32(dr["Id"].ToString()),
                RelNo = Convert.ToInt32(dr["RelNo"].ToString()),
                Job = Convert.ToInt32(dr["Job"].ToString()),
                SubAssy = dr["SubAssy"].ToString(),
                PckgQty = Convert.ToInt32(dr["PckgQty"].ToString()),
                IsSubmitted = Convert.ToBoolean(dr["IsSubmitted"]),
                SubmittedTo = Convert.ToInt32(dr["SubmittedTo"].ToString()),
                SubmittedOn = dr["SubmittedOn"].ToString().StartsWith("1900") ? "" : dr["SubmittedOn"].ToString(),
                ReqCntrlsApp = Convert.ToBoolean(dr["ReqCntrlsApp"]),
                CntrlsSubmittedTo = Convert.ToInt32(dr["CntrlsSubmittedTo"].ToString()),
                CntrlsSubmittedOn = dr["CntrlsSubmittedOn"].ToString().StartsWith("1900") ? "" : dr["CntrlsSubmittedOn"].ToString(),
                CntrlsApproved = Convert.ToBoolean(dr["CntrlsApproved"]),
                CntrlsApprovedBy = dr["CntrlsApprovedBy"].ToString(),
                CntrlsApprovedOn = dr["CntrlsApprovedOn"].ToString().StartsWith("1900") ? "" : dr["CntrlsApprovedOn"].ToString(),
                IsChecked = Convert.ToBoolean(dr["IsChecked"]),
                CheckedBy = dr["CheckedBy"].ToString(),
                CheckedOn = dr["CheckedOn"].ToString().StartsWith("1900") ? "" : dr["CheckedOn"].ToString(),
                ApprovalTo = Convert.ToInt32(dr["ApprovalTo"].ToString()),
                IsApproved = Convert.ToBoolean(dr["IsApproved"]),
                ApprovedBy = dr["ApprovedBy"].ToString(),
                ApprovedOn = dr["ApprovedOn"].ToString().StartsWith("1900") ? "" : dr["ApprovedOn"].ToString(),
                IsReleased = Convert.ToBoolean(dr["IsReleased"]),
                ReleasedBy = dr["ReleasedBy"].ToString(),
                ReleasedOn = dr["ReleasedOn"].ToString().StartsWith("1900") ? "" : dr["ReleasedOn"].ToString(),
                DateCreated = dr["DateCreated"].ToString(),
                CreatedById = Convert.ToInt32(dr["CreatedById"].ToString()),
                CreatedBy = dr["CreatedBy"].ToString(),
                LastSaved = dr["LastSaved"].ToString(),
                LastSavedById = Convert.ToInt32(dr["LastSavedById"].ToString()),
                LastSavedBy = dr["LastSavedBy"].ToString(),
                Priority = dr["Priority"].ToString(),
                IsActive = Convert.ToBoolean(dr["IsActive"]),
                ConnectRelNo = dr["ConnectRelNo"].ToString(),
                WorkOrder = dr["WorkOrder"].ToString(),
                JobDes = dr["JobDes"].ToString(),
                SubAssyDes = dr["SubAssyDes"].ToString(),
                Path = dr["Path"].ToString(),
                ReleaseItems = releaseItems,
                ReleaseComments = releaseComments,
            };
        }

        private ReleaseItem GetReleaseItemsDetails(DataRow dr)
        {
            return new ReleaseItem
            {
                RelNo = Convert.ToInt32(dr["RelNo"].ToString()),
                ItemNumber = dr["ItemNumber"].ToString(),
                QuantityPerAssembly = Convert.ToInt32(dr["QuantityPerAssembly"].ToString()),
                Description = dr["Description"].ToString(),
                ItemFamily = dr["ItemFamily"].ToString(),
                Manufacturer = dr["Manufacturer"].ToString(),
                ManufacturerItemNumber = dr["ManufacturerItemNumber"].ToString(),
                AssyNo = dr["AssyNo"].ToString(),
                AssyFamily = dr["AssyFamily"].ToString(),
                AssyDescription = dr["AssyDescription"].ToString(),
                AssyManufacturer = dr["AssyManufacturer"].ToString(),
                AssyManufacturerItemNumber = dr["AssyManufacturerItemNumber"].ToString(),
                Path = dr["Path"].ToString(),
            };
        }

        public List<ReleaseItem> GetReleaseItemsDetails(int relno)
        {
            List<ReleaseItem> relItems = new List<ReleaseItem>();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[RP_CRUDItems]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@RelNo", relno);
                sda.SelectCommand.Parameters.AddWithValue("@StatementType", nameof(CRUDStatementType.Select));
                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            relItems.Add(GetReleaseItemsDetails(dr));
                        }
                    }
                    dt.Clear();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }

            return relItems;
        }

        public List<ReleaseComment> GetReleaseComments(int relno)
        {
            List<ReleaseComment> relComments = new List<ReleaseComment>();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[RP_CRUDRemark]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@RelNo", relno);
                sda.SelectCommand.Parameters.AddWithValue("@StatementType", nameof(CRUDStatementType.Select));
                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            relComments.Add(GetReleaseCommentDetails(dr));
                        }
                    }
                    dt.Clear();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }

            return relComments;
        }

        private ReleaseComment GetReleaseCommentDetails(DataRow dr)
        {
            return new ReleaseComment
            {
                Id = Convert.ToInt32(dr["Id"].ToString()),
                RelNo = Convert.ToInt32(dr["RelNo"].ToString()),
                Comment = dr["Comment"].ToString(),
                CommentBy = dr["CommentBy"].ToString(),
                DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString()).TimeAgo(),
            };
        }

        public AutoCompleteStringCollection FillCheckingUsers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT  CONCAT(id, ' ', Name) as Checkers  FROM [SPM_Database].[dbo].[Users] where [CheckDrawing] = '1' ORDER BY Name", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    MyCollection.Add("");
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Checking Users", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillApprovingUsers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT  CONCAT(id, ' ', Name) as Checkers  FROM [SPM_Database].[dbo].[Users] where [ApproveDrawing] = '1' ORDER BY Name", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    MyCollection.Add("");
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Approving Users", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillControlsApprovingUsers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT  CONCAT(id, ' ', Name) as Checkers  FROM [SPM_Database].[dbo].[Users] where [ControlsApprovalDrawing] = '1' ORDER BY Name", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    MyCollection.Add("");
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Controls Approving Users", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public bool AddReleaseComment(ReleaseComment rc)
        {
            bool success = false;
            using (SqlCommand cmd = new SqlCommand("[dbo].[RP_CRUDRemark]", cn) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@RelNo", rc.RelNo);
                cmd.Parameters.AddWithValue("@CommentBy", rc.CommentBy);
                cmd.Parameters.AddWithValue("@Comment", rc.Comment);
                cmd.Parameters.AddWithValue("@StatementType", nameof(CRUDStatementType.Insert));

                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    cmd.ExecuteScalar();
                    cn.Close();
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Add Release Comment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return success;
        }

        public void UpdateReleaseInvoice(ReleaseLog releaseLog)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");

            using (SqlCommand cmd = new SqlCommand("[dbo].[RP_CRUDBase]", cn) { CommandType = System.Data.CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@RelNo", releaseLog.RelNo);
                cmd.Parameters.AddWithValue("@Job", releaseLog.Job);
                cmd.Parameters.AddWithValue("@SubAssy", releaseLog.SubAssy);
                cmd.Parameters.AddWithValue("@PckgQty", releaseLog.PckgQty);
                cmd.Parameters.AddWithValue("@IsSubmitted", Convert.ToInt32(releaseLog.IsSubmitted));
                cmd.Parameters.AddWithValue("@SubmittedTo", releaseLog.SubmittedTo);
                cmd.Parameters.AddWithValue("@SubmittedOn", releaseLog.SubmittedOn);
                cmd.Parameters.AddWithValue("@ReqCntrlsApp", Convert.ToInt32(releaseLog.ReqCntrlsApp));
                cmd.Parameters.AddWithValue("@CntrlsSubmittedTo", releaseLog.CntrlsSubmittedTo);
                cmd.Parameters.AddWithValue("@CntrlsSubmittedOn", releaseLog.CntrlsSubmittedOn);
                cmd.Parameters.AddWithValue("@IsCntrlsApproved", Convert.ToInt32(releaseLog.CntrlsApproved));
                cmd.Parameters.AddWithValue("@CntrlsApprovedBy", releaseLog.CntrlsApprovedBy);
                cmd.Parameters.AddWithValue("@CntrlsApprovedOn", releaseLog.CntrlsApprovedOn);
                cmd.Parameters.AddWithValue("@IsChecked", Convert.ToInt32(releaseLog.IsChecked));
                cmd.Parameters.AddWithValue("@CheckedBy", releaseLog.CheckedBy);
                cmd.Parameters.AddWithValue("@CheckedOn", releaseLog.CheckedOn);
                cmd.Parameters.AddWithValue("@ApprovalTo", releaseLog.ApprovalTo);
                cmd.Parameters.AddWithValue("@IsApproved", Convert.ToInt32(releaseLog.IsApproved));
                cmd.Parameters.AddWithValue("@ApprovedBy", releaseLog.ApprovedBy);
                cmd.Parameters.AddWithValue("@ApprovedOn", releaseLog.ApprovedOn);
                cmd.Parameters.AddWithValue("@IsReleased", Convert.ToInt32(releaseLog.IsReleased));
                cmd.Parameters.AddWithValue("@ReleasedBy", releaseLog.ReleasedBy);
                cmd.Parameters.AddWithValue("@ReleasedOn", releaseLog.ReleasedOn);
                cmd.Parameters.AddWithValue("@LastSaved", sqlFormattedDatetime);
                cmd.Parameters.AddWithValue("@LastSavedById", ConnectUser.ConnectId);
                cmd.Parameters.AddWithValue("@Priority", releaseLog.Priority);
                cmd.Parameters.AddWithValue("@IsActive", Convert.ToInt32(releaseLog.IsActive));
                cmd.Parameters.AddWithValue("@ConnectRelNo", releaseLog.ConnectRelNo);
                cmd.Parameters.AddWithValue("@WorkOrder", releaseLog.WorkOrder);
                cmd.Parameters.AddWithValue("@Path", releaseLog.Path);
                cmd.Parameters.AddWithValue("@StatementType", nameof(CRUDStatementType.Update));

                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update Release Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        #endregion WorkOrderRelease
    }
}