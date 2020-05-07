using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace SPMConnectAPI
{
    public class ConnectAPI
    {
        #region Enum, Helpers

        public enum Department
        {
            Eng,
            Controls,
            Production,
            Accounting,
            Purchasing,
            Crib
        }

        public class CheckInModules
        {
            public static string ShipInv = "ShipInv";
            public static string WO = "Work Order";
            public static string ECR = "ECR";
        }

        public class UserFields
        {
            public static string CribShortEmp_Id = "CribShortEmp_Id";
            public static string UserName = "UserName";
            public static string Department = "Department";
            public static string Name = "Name";
            public static string ActiveBlockNumber = "ActiveBlockNumber";
            public static string Admin = "Admin";
            public static string Developer = "Developer";
            public static string Management = "Management";
            public static string Quote = " Quote";
            public static string PurchaseReq = "PurchaseReq";
            public static string PurchaseReqApproval = "PurchaseReqApproval";
            public static string PurchaseReqApproval2 = "PurchaseReqApproval2";
            public static string PurchaseReqBuyer = "PurchaseReqBuyer";
            public static string PriceRight = "PriceRight";
            public static string Shipping = "Shipping";
            public static string WOScan = "WOScan";
            public static string CribShort = "CribShort";
            public static string ECR = "ECR";
            public static string ECRApproval = " ECRApproval";
            public static string ECRApproval2 = "ECRApproval2";
            public static string ECRHandler = "ECRHandler";
            public static string ECRSup = "ECRSup";
            public static string ItemDependencies = "ItemDependencies";
            public static string WORelease = "WORelease";
            public static string ShipSupervisor = " ShipSupervisor";
            public static string ShipSup = "ShipSup";
            public static string ShippingManager = "ShippingManager";
            public static string CheckDrawing = " CheckDrawing";
            public static string ApproveDrawing = "ApproveDrawing";
            public static string ReleasePackage = "ReleasePackage";
            public static string Supervisor = "Supervisor";
            public static string Email = "Email";
            public static string SharesFolder = "SharesFolder";
            public static string ReadWhatsNew = "ReadWhatsNew";
            public static string id = "id";
        }

        public struct NameEmail
        {
            public string name;
            public string email;
        }

        #endregion Enum, Helpers

        #region SQL Connection / Connection Strings

        public SqlConnection cn;

        public static UserInfo ConnectUser { get; set; } = new UserInfo();

        private log4net.ILog log;

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
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.GlobalContext.Properties["User"] = System.Environment.UserName;
            log.Info("Accessed Connect API Base Class " + Getassyversionnumber());
            ConnectUser = GetUserDetails(GetUserName());
        }

        public string ConnectConnectionString()
        {
            return "Data Source=spm-sql;Initial Catalog=SPM_Database;User ID=SPM_Agent;password=spm5445";
        }

        public string ConnectCntrlsConnectionString()
        {
            return "Data Source=spm-sql;Initial Catalog=SPMControlCatalog;User ID=SPM_Controls;password=eyBzJehFP*uO";
        }

        #endregion SQL Connection / Connection Strings

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
                return userName;
            else
                return null;
        }

        #region UserInfo/Rights

        public bool CheckRights(string Module)
        {
            bool rightsAllowed = false;
            string useradmin = GetUserName();

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND " + Module + " = '1'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        rightsAllowed = true;
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
            return rightsAllowed;
        }

        public UserInfo GetUserDetails(string userName)
        {
            UserInfo userDet = new UserInfo();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + userName + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    userDet.ActiveBlockNumber = dr["ActiveBlockNumber"].ToString();
                    userDet.Admin = Convert.ToInt32(dr["Admin"].ToString()) == 1 ? true : false;
                    userDet.ApproveDrawing = Convert.ToInt32(dr["ApproveDrawing"].ToString()) == 1 ? true : false;
                    userDet.CheckDrawing = Convert.ToInt32(dr["CheckDrawing"].ToString()) == 1 ? true : false;
                    userDet.CribCheckout = Convert.ToInt32(dr["CribCheckout"].ToString()) == 1 ? true : false;
                    userDet.CribShort = Convert.ToInt32(dr["CribShort"].ToString()) == 1 ? true : false;
                    userDet.Dept = AttachDepartment(dr["Department"].ToString());
                    userDet.Developer = Convert.ToInt32(dr["Developer"].ToString()) == 1 ? true : false;
                    userDet.ECR = Convert.ToInt32(dr["ECR"].ToString()) == 1 ? true : false;
                    userDet.ECRApproval = Convert.ToInt32(dr["ECRApproval"].ToString()) == 1 ? true : false;
                    userDet.ECRApproval2 = Convert.ToInt32(dr["ECRApproval2"].ToString()) == 1 ? true : false;
                    userDet.ECRHandler = Convert.ToInt32(dr["ECRHandler"].ToString()) == 1 ? true : false;
                    userDet.ECRSup = Convert.ToInt32(dr["ECRSup"].ToString());
                    userDet.Email = dr["Email"].ToString();
                    userDet.Emp_Id = Convert.ToInt32(dr["Emp_Id"].ToString());
                    userDet.ConnectId = Convert.ToInt32(dr["id"].ToString());
                    userDet.ItemDependencies = Convert.ToInt32(dr["ItemDependencies"].ToString()) == 1 ? true : false;
                    userDet.Management = Convert.ToInt32(dr["Management"].ToString()) == 1 ? true : false;
                    userDet.Name = dr["Name"].ToString();
                    userDet.PriceRight = Convert.ToInt32(dr["PriceRight"].ToString()) == 1 ? true : false;
                    userDet.PurchaseReq = Convert.ToInt32(dr["PurchaseReq"].ToString()) == 1 ? true : false;
                    userDet.PurchaseReqApproval = Convert.ToInt32(dr["PurchaseReqApproval"].ToString()) == 1 ? true : false;
                    userDet.PurchaseReqApproval2 = Convert.ToInt32(dr["PurchaseReqApproval2"].ToString()) == 1 ? true : false;
                    userDet.PurchaseReqBuyer = Convert.ToInt32(dr["PurchaseReqBuyer"].ToString()) == 1 ? true : false;
                    userDet.Quote = Convert.ToInt32(dr["Quote"].ToString()) == 1 ? true : false;
                    userDet.ReadWhatsNew = Convert.ToInt32(dr["ReadWhatsNew"].ToString()) == 1 ? true : false;
                    userDet.ReleasePackage = Convert.ToInt32(dr["ReleasePackage"].ToString()) == 1 ? true : false;
                    userDet.SharesFolder = dr["SharesFolder"].ToString();
                    userDet.Shipping = Convert.ToInt32(dr["Shipping"].ToString()) == 1 ? true : false;
                    userDet.ShippingManager = Convert.ToInt32(dr["ShippingManager"].ToString()) == 1 ? true : false;
                    userDet.ShipSup = Convert.ToInt32(dr["ShipSup"].ToString());
                    userDet.ShipSupervisor = Convert.ToInt32(dr["ShipSupervisor"].ToString()) == 1 ? true : false;
                    userDet.Supervisor = Convert.ToInt32(dr["Supervisor"].ToString());
                    userDet.UserName = dr["UserName"].ToString();
                    userDet.WORelease = Convert.ToInt32(dr["WORelease"].ToString()) == 1 ? true : false;
                    userDet.WOScan = Convert.ToInt32(dr["WOScan"].ToString()) == 1 ? true : false;
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
            return userDet;
        }

        private Department AttachDepartment(string dept)
        {
            if (dept == "Accounting")
            {
                return Department.Accounting;
            }
            else if (dept == "Eng")
            {
                return Department.Eng;
            }
            else if (dept == "Controls")
            {
                return Department.Controls;
            }
            else if (dept == "Production")
            {
                return Department.Production;
            }
            else if (dept == "Purchasing")
            {
                return Department.Purchasing;
            }
            else
            {
                return Department.Crib;
            }
        }

        public string GetNameByConnectEmpId(string empid)
        {
            string fullname = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + empid + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                fullname = dt.Rows[0]["Name"].ToString();
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

        public string GetNameByEmpId(string empid)
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
                fullname = dt.Rows[0]["Name"].ToString();
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

        public List<NameEmail> GetNameEmailByParaValue(string parameter, string value)
        {
            List<NameEmail> nameEmail = new List<NameEmail>();
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [" + parameter + "] = '" + value + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    NameEmail nameemail = new NameEmail
                    {
                        email = dr["Email"].ToString(),
                        name = dr["Name"].ToString()
                    };
                    nameEmail.Add(nameemail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get User Name and Email" + parameter, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return nameEmail;
        }

        #endregion UserInfo/Rights

        public string GetConnectParameterValue(string parameter)
        {
            string value = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = '" + parameter + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    value = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get Parameter Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return value;
        }

        #region Checkin Checkout Check Invoice

        public string InvoiceOpen(string invoicenumber, string app)
        {
            string username = "";

            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[UserHolding] WHERE [ItemId]='" + invoicenumber + "' AND App = '" + app + "'", cn))
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

        public bool CheckinInvoice(string invoicenumber, string app)
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
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[UserHolding] (App, UserName, ItemId,CheckInDateTime) VALUES('" + app + "','" + GetUserName() + "','" + invoicenumber + "','" + sqlFormattedDatetime + "')";
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

        public bool CheckoutInvoice(string invoicenumber, string app)
        {
            bool success = false;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[UserHolding] where App = '" + app + "' AND UserName = '" + GetUserName() + "' AND ItemId = '" + invoicenumber + "'";
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

        #region EMAIL

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

        public void SendemailListAttachments(string emailtosend, string subject, string body, List<string> filetoattach, string cc)
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

                if (filetoattach.Count == 0)
                {
                }
                else
                {
                    foreach (string file in filetoattach)
                    {
                        attachment = new System.Net.Mail.Attachment(file);
                        message.Attachments.Add(attachment);
                    }
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

        public bool TriggerEmail(string emailtosend, string subject, string user, string body, string filetoattach, string cc, string extracc, string msgtype)
        {
            bool success = false;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("spmautomation-com0i.mail.protection.outlook.com");

                System.Net.Mail.Attachment attachment;

                message.To.Add(emailtosend);
                if (cc == "")
                {
                }
                else
                {
                    message.CC.Add(cc);
                }
                if (extracc == "")
                {
                }
                else
                {
                    message.CC.Add(extracc);
                }
                message.Subject = subject;

                if (msgtype == "EFT")
                {
                    message.From = new MailAddress("connect@spm-automation.com", "SPM Automation");
                    message.IsBodyHtml = true;
                    using (StreamReader reader = File.OpenText(Path.Combine(Directory.GetCurrentDirectory() + @"\EmailTemplates\", "EFTEmailTemplate.html"))) // Path to your
                    {
                        message.Body = reader.ReadToEnd();  // Load the content from your file...
                    }
                    message.Body = message.Body.Replace("{type}", body);
                }
                else if (msgtype == "Normal")
                {
                    message.From = new MailAddress("connect@spm-automation.com", "SPM Connect");
                    message.IsBodyHtml = true;
                    using (StreamReader reader = File.OpenText(Path.Combine(Directory.GetCurrentDirectory() + @"\EmailTemplates\", "EmailTemplate.html"))) // Path to your
                    {
                        message.Body = reader.ReadToEnd();
                    }
                    message.Body = message.Body.Replace("{message}", body);
                    message.Body = message.Body.Replace("{username}", user);
                }
                else if (msgtype == "update")
                {
                    message.From = new MailAddress("connect@spm-automation.com", "SPM Connect");
                    message.IsBodyHtml = true;
                    using (StreamReader reader = File.OpenText(Path.Combine(Directory.GetCurrentDirectory() + @"\EmailTemplates\", "update.html"))) // Path to your
                    {
                        message.Body = reader.ReadToEnd();  // Load the content from your file...
                    }
                }
                else
                {
                    message.From = new MailAddress("connect@spm-automation.com", "SPM Connect");
                    message.Body = body;
                }

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
                try
                {
                    SmtpServer.Send(message);
                    success = true;
                }
                catch (Exception)
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Send Email", MessageBoxButtons.OK);
                success = false;
            }
            return success;
        }

        #endregion
    }
}