using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class ConnectAPI
    {
        public SqlConnection cn;

        public ConnectAPI()
        {
            SPM_Connect();
        }

        private void SPM_Connect()
        {
            string connection = ConnectConnectionString();
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(null, "Cannot connect through the server. Please check the network connection.", "SPM Connect Home - SQL Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                System.Environment.Exit(0);
            }
        }

        public string Getassyversionnumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = "V" + assembly.GetName().Version.ToString();
            return version;
        }

        public string GetUserName()
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

        public string ConnectConnectionString()
        {
            return "Data Source=spm-sql;Initial Catalog=SPM_Database;User ID=SPM_Agent;password=spm5445";
        }

        public string ConnectCntrlsConnectionString()
        {
            return "Data Source=spm-sql;Initial Catalog=SPMControlCatalog;User ID=SPM_Controls;password=eyBzJehFP*uO";
        }

        #region UserInfo/Rights

        public bool CheckRights(string Module)
        {
            bool yeswoscan = false;
            string useradmin = GetUserName();

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
                    MetroFramework.MetroMessageBox.Show(null, ex.Message, "SPM Connect - Unable to retrieve user rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return yeswoscan;
        }

        public UserInfo GetUserDetails()
        {
            UserInfo user = new UserInfo();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + GetUserName() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    user.ActiveBlockNumber = dr["ActiveBlockNumber"].ToString();
                    user.Admin = Convert.ToInt32(dr["Admin"].ToString()) == 1 ? true : false;
                    user.ApproveDrawing = Convert.ToInt32(dr["ApproveDrawing"].ToString()) == 1 ? true : false;
                    user.CheckDrawing = Convert.ToInt32(dr["CheckDrawing"].ToString()) == 1 ? true : false;
                    user.CribCheckout = Convert.ToInt32(dr["CribCheckout"].ToString()) == 1 ? true : false;
                    user.CribShort = Convert.ToInt32(dr["CribShort"].ToString()) == 1 ? true : false;
                    user.Department = dr["Department"].ToString();
                    user.Developer = Convert.ToInt32(dr["Developer"].ToString()) == 1 ? true : false;
                    user.ECR = Convert.ToInt32(dr["ECR"].ToString()) == 1 ? true : false;
                    user.ECRApproval = Convert.ToInt32(dr["ECRApproval"].ToString()) == 1 ? true : false;
                    user.ECRApproval2 = Convert.ToInt32(dr["ECRApproval2"].ToString()) == 1 ? true : false;
                    user.ECRHandler = Convert.ToInt32(dr["ECRHandler"].ToString()) == 1 ? true : false;
                    user.ECRSup = Convert.ToInt32(dr["ECRSup"].ToString());
                    user.Email = dr["Email"].ToString();
                    user.Emp_Id = Convert.ToInt32(dr["Emp_Id"].ToString());
                    user.Id = Convert.ToInt32(dr["id"].ToString());
                    user.ItemDependencies = Convert.ToInt32(dr["ItemDependencies"].ToString()) == 1 ? true : false;
                    user.Management = Convert.ToInt32(dr["Management"].ToString()) == 1 ? true : false;
                    user.Name = dr["Name"].ToString();
                    user.PriceRight = Convert.ToInt32(dr["PriceRight"].ToString()) == 1 ? true : false;
                    user.PurchaseReq = Convert.ToInt32(dr["PurchaseReq"].ToString()) == 1 ? true : false;
                    user.PurchaseReqApproval = Convert.ToInt32(dr["PurchaseReqApproval"].ToString()) == 1 ? true : false;
                    user.PurchaseReqApproval2 = Convert.ToInt32(dr["PurchaseReqApproval2"].ToString()) == 1 ? true : false;
                    user.PurchaseReqBuyer = Convert.ToInt32(dr["PurchaseReqBuyer"].ToString()) == 1 ? true : false;
                    user.Quote = Convert.ToInt32(dr["Quote"].ToString()) == 1 ? true : false;
                    user.ReadWhatsNew = Convert.ToInt32(dr["ReadWhatsNew"].ToString()) == 1 ? true : false;
                    user.ReleasePackage = Convert.ToInt32(dr["ReleasePackage"].ToString()) == 1 ? true : false;
                    user.SharesFolder = dr["SharesFolder"].ToString();
                    user.Shipping = Convert.ToInt32(dr["Shipping"].ToString()) == 1 ? true : false;
                    user.ShippingManager = Convert.ToInt32(dr["ShippingManager"].ToString()) == 1 ? true : false;
                    user.ShipSup = Convert.ToInt32(dr["ShipSup"].ToString());
                    user.ShipSupervisor = Convert.ToInt32(dr["ShipSupervisor"].ToString()) == 1 ? true : false;
                    user.Supervisor = Convert.ToInt32(dr["Supervisor"].ToString());
                    user.UserName = dr["UserName"].ToString();
                    user.WORelease = Convert.ToInt32(dr["WORelease"].ToString()) == 1 ? true : false;
                    user.WOScan = Convert.ToInt32(dr["WOScan"].ToString()) == 1 ? true : false;
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(null, ex.Message, "SPM Connect - Error Getting User Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return user;
        }

        #endregion UserInfo/Rights
    }
}