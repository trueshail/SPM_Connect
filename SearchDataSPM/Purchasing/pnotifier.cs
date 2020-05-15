using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SearchDataSPM
{
    public class Pnotifier
    {
        private readonly SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
        private SqlConnection cn;
        private string connection;
        private bool higherauthority;
        private bool pbuyer;

        // current user creds
        private bool supervisor;

        private string userfullname = "";

        //

        // purchase req details
        //string reqrequestname = "";
        //int reqno = 0, validate = 0, approved = 0, happroval = 0, happroved = 0, papproval = 0, papproved = 0, supervisoridfromreq = 0;
        //double total = 0.00;
        ////

        public void Currentusercreds()
        {
            SPM_Connect();
            string username = connectapi.GetUserName();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string fullname = dr["Name"].ToString();
                    userfullname = fullname;
                    string manager = dr["PurchaseReqApproval"].ToString();
                    string hauthority = dr["PurchaseReqApproval2"].ToString();
                    string PurchaseReqBuyer = dr["PurchaseReqBuyer"].ToString();

                    if (manager == "1")
                    {
                        supervisor = true;
                    }
                    if (hauthority == "1")
                    {
                        higherauthority = true;
                    }
                    if (PurchaseReqBuyer == "1")
                    {
                        pbuyer = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public int getsupervisorid(string username)
        {
            int fullname = 0;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    fullname = Convert.ToInt32(dr["Supervisor"]);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return fullname;
        }

        public string getsupervisorname(int id)
        {
            string fullname = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + id.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    fullname = dr["Name"].ToString();
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
            return fullname;
        }

        public string showpopupnotifation(int validate, int approved, int happroval, int happroved, int papproval, int papproved, string requestname)
        {
            // SPM_Connect();
            //currentusercreds();
            string message = "no";

            string supervisorname = getsupervisorname(getsupervisorid(connectapi.GetUserName()));

            if (validate == 1 && approved == 0)
            {
                if (supervisor && supervisorname == userfullname)
                {
                    message = "New purchase req issued for approval.";
                }
            }
            else if (validate == 1 && approved == 1 && happroval == 0 && papproval == 1 && papproved == 0)
            {
                if (pbuyer)
                {
                    message = "New purchase req issued for purchase.";
                }
                else
                {
                    if (userfullname == requestname)
                    {
                        message = "Purchase req approved.";
                    }
                }
            }
            else if (validate == 1 && approved == 1 && happroval == 1 && happroved == 0 && papproval == 0 && papproved == 0)
            {
                if (higherauthority)
                {
                    message = "New purchase req issued for higher approval.";
                }
            }
            else if (validate == 1 && approved == 1 && happroval == 1 && happroved == 1 && papproval == 1 && papproved == 0)
            {
                if (pbuyer)
                {
                    message = "New purchase req issued for purchase.";
                }
                else
                {
                    if (userfullname == requestname)
                    {
                        message = "Purchase req approved.";
                    }
                    if (supervisor && supervisorname == userfullname)
                    {
                        message = "Purchase req approved.";
                    }
                }
            }
            else if (validate == 1 && approved == 1 && papproval == 1 && papproved == 1)
            {
                if (supervisor && supervisorname == userfullname)
                {
                    message = "Purchase req sent out for purchase.";
                }
                else
                {
                    if (userfullname == requestname)
                    {
                        message = "Purchase req sent out for purchase";
                    }
                }
            }
            else
            {
                message = "no";
            }

            return message;
        }

        public void SPM_Connect()
        {
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}