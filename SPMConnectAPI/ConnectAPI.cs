using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;
using System.Reflection;

namespace SPMConnectAPI
{
    public class ConnectAPI : IDisposable
    {
        #region SQL Connection / Connection Strings

        public SqlConnection cn;
        public UserInfo ConnectUser { get; set; } = new UserInfo();

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
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.GlobalContext.Properties["User"] = System.Environment.UserName;
            log.Info("Accessed Connect API Base Class " + Getassyversionnumber());
            ConnectUser = GetUserDetails(GetUserName());
        }

        #endregion SQL Connection / Connection Strings

        public List<ConnectParameters> GetCustomObjects()
        {
            return new ClassMappers(ConnectConnectionString()).SqlQuery<ConnectParameters>("SELECT * FROM [SPM_Database].[dbo].[ConnectParamaters]");
        }

        #region UserInfo/Rights

        public bool CheckRights(string Module)
        {
            bool rightsAllowed = false;
            string useradmin = ConnectUser.UserName;

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

        public string GetNameByConnectEmpId(string empid)
        {
            string fullname = "";
            using (SqlCommand cmd = new SqlCommand("SELECT [Name] FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + empid + "' ", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    fullname = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user full name by connect id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return fullname;
        }

        public string GetNameByEmpId(string empid)
        {
            string fullname = "";
            using (SqlCommand cmd = new SqlCommand("SELECT [Name] FROM [SPM_Database].[dbo].[Users] WHERE [Emp_Id]='" + empid + "' ", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    fullname = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user full name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return fullname;
        }

        public List<NameEmail> GetNameEmailByParaValue(string parameter, string value)
        {
            List<NameEmail> nameEmail = new List<NameEmail>();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[User_GetNameEmail]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@column", parameter);
                sda.SelectCommand.Parameters.AddWithValue("@value", value);
                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
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
                    MessageBox.Show(ex.Message, "SPM Connect - Get User Name and Email " + parameter, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return nameEmail;
        }

        public UserInfo GetUserDetails(string userName)
        {
            UserInfo userDet = new UserInfo();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[User_ByUsername]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@username", userName);
                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        userDet = GetUserFilled(dr);
                    }
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(null, ex.Message, "SPM Connect - Error Getting User Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    Environment.Exit(0);
                }
                finally
                {
                    cn.Close();
                }
            }
            return userDet;
        }

        public List<UserInfo> GetConnectUsersList()
        {
            List<UserInfo> userlist = new List<UserInfo>();
            using (SqlDataAdapter sda = new SqlDataAdapter("[dbo].[User_GetAll]", cn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                try
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            userlist.Add(GetUserFilled(dr));
                        }
                    }
                    dt.Clear();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(null, ex.Message, "SPM Connect - Error Getting User List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return userlist;
        }

        private UserInfo GetUserFilled(DataRow dr)
        {
            return new UserInfo
            {
                ActiveBlockNumber = dr["ActiveBlockNumber"].ToString(),
                Admin = Convert.ToBoolean(dr["Admin"]),
                ApproveDrawing = Convert.ToBoolean(dr["ApproveDrawing"]),
                CheckDrawing = Convert.ToBoolean(dr["CheckDrawing"]),
                CribCheckout = Convert.ToBoolean(dr["CribCheckout"]),
                CribShort = Convert.ToBoolean(dr["CribShort"]),
                Dept = AttachDepartment(dr["Department"].ToString()),
                Developer = Convert.ToBoolean(dr["Developer"]),
                ECR = Convert.ToBoolean(dr["ECR"]),
                ECRApproval = Convert.ToBoolean(dr["ECRApproval"]),
                ECRApproval2 = Convert.ToBoolean(dr["ECRApproval2"]),
                ECRHandler = Convert.ToBoolean(dr["ECRHandler"]),
                ECRSup = Convert.ToInt32(dr["ECRSup"].ToString()),
                Email = dr["Email"].ToString(),
                Emp_Id = Convert.ToInt32(dr["Emp_Id"].ToString()),
                ConnectId = Convert.ToInt32(dr["id"].ToString()),
                ItemDependencies = Convert.ToBoolean(dr["ItemDependencies"]),
                Management = Convert.ToBoolean(dr["Management"]),
                Name = dr["Name"].ToString(),
                PriceRight = Convert.ToBoolean(dr["PriceRight"]),
                PurchaseReq = Convert.ToBoolean(dr["PurchaseReq"]),
                PurchaseReqApproval = Convert.ToBoolean(dr["PurchaseReqApproval"]),
                PurchaseReqApproval2 = Convert.ToBoolean(dr["PurchaseReqApproval2"]),
                PurchaseReqBuyer = Convert.ToBoolean(dr["PurchaseReqBuyer"]),
                Quote = Convert.ToBoolean(dr["Quote"]),
                ReadWhatsNew = Convert.ToBoolean(dr["ReadWhatsNew"]),
                ReleasePackage = Convert.ToBoolean(dr["ReleasePackage"]),
                SharesFolder = dr["SharesFolder"].ToString(),
                Shipping = Convert.ToBoolean(dr["Shipping"]),
                ShippingManager = Convert.ToBoolean(dr["ShippingManager"]),
                ShipSup = Convert.ToInt32(dr["ShipSup"].ToString()),
                ShipSupervisor = Convert.ToBoolean(dr["ShipSupervisor"]),
                Supervisor = Convert.ToInt32(dr["Supervisor"].ToString()),
                UserName = dr["UserName"].ToString(),
                WORelease = Convert.ToBoolean(dr["WORelease"]),
                WOScan = Convert.ToBoolean(dr["WOScan"])
            };
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

        #endregion UserInfo/Rights

        #region Checkin Checkout Check Invoice

        public void CheckinApp(string applicationname)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("dd-MM-yyyy HH:mm tt");
            string computername = System.Environment.MachineName;

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Checkin] ([Last Login],[Application Running],[User Name], [Computer Name], [Version]) VALUES('" + sqlFormattedDate + "', '" + applicationname + "', '" + ConnectUser.UserName + "', '" + computername + "','" + Getassyversionnumber() + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - User Checkin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public void CheckoutApp(string applicationname)
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                string query = "DELETE FROM [SPM_Database].[dbo].[Checkin] WHERE [User Name] ='" + ConnectUser.UserName + "' AND [Application Running] = '" + applicationname + "' ";
                SqlCommand sda = new SqlCommand(query, cn);
                sda.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Checkout User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
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
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[UserHolding] (App, UserName, ItemId,CheckInDateTime) VALUES('" + app + "','" + ConnectUser.UserName + "','" + invoicenumber + "','" + sqlFormattedDatetime + "')";
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
                cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[UserHolding] where App = '" + app + "' AND UserName = '" + ConnectUser.UserName + "' AND ItemId = '" + invoicenumber + "'";
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

        #endregion Checkin Checkout Check Invoice

        #region EMAIL

        public void Dispose()
        {
            cn.Dispose();
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
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }
                message.Subject = subject;
                message.Body = body;

                if (filetoattach.Count != 0)
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

        public bool TriggerEmail(string emailtosend, string subject, string user, string body, string filetoattach, string cc, string extracc, string msgtype, List<string> filestoattach = null)
        {
            bool success;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("spmautomation-com0i.mail.protection.outlook.com");

                System.Net.Mail.Attachment attachment;

                message.To.Add(emailtosend);
                if (cc != "")
                {
                    message.CC.Add(cc);
                }
                if (!string.IsNullOrEmpty(extracc))
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
                if (!string.IsNullOrEmpty(filetoattach))
                {
                    attachment = new System.Net.Mail.Attachment(filetoattach);
                    message.Attachments.Add(attachment);
                }
                if (filestoattach != null)
                {
                    if (filestoattach.Count != 0)
                    {
                        foreach (string file in filestoattach)
                        {
                            attachment = new System.Net.Mail.Attachment(file);
                            message.Attachments.Add(attachment);
                        }
                    }
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

        #endregion EMAIL

        #region Favorites

        public bool Addtofavorites(string itemid, bool addin)
        {
            log.Info("Addtofavorites");
            if (string.IsNullOrEmpty(itemid))
            {
                return false;
            }
            const bool completed = false;
            if (addin && !ValidfileName(itemid))
            {
                MessageBox.Show("A file with the part number " + itemid + " does not have Solidworks CAD Model or SPM item number assigned. Cannot add or remove from favorites. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (CheckitempresentonFavorites(itemid, addin))
            {
                string usernamesfromitem = Getusernamesfromfavorites(itemid);
                if (!Userexists(usernamesfromitem))
                {
                    string newuseradded = usernamesfromitem + ConnectUser.UserName + ",";
                    Updateusernametoitemonfavorites(itemid, newuseradded);
                }
                else
                {
                    MessageBox.Show("Item no " + itemid + " already exists on your favorite list.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Additemtofavoritessql(itemid);
            }

            return completed;
        }

        public bool Removefromfavorites(string itemid, bool addin)
        {
            log.Info("Removefromfavorites");
            if (string.IsNullOrEmpty(itemid))
            {
                return false;
            }
            const bool completed = false;
            if (addin && !ValidfileName(itemid))
            {
                MessageBox.Show("A file with the part number " + itemid + " does not have Solidworks CAD Model or SPM item number assigned. Cannot add or remove from favorites. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string usernamesfromitem = Getusernamesfromfavorites(itemid);
            Updateusernametoitemonfavorites(itemid, Removeusername(usernamesfromitem));
            MessageBox.Show("Item no " + itemid + " has been removed from your favorite list.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return completed;
        }

        public bool CheckitempresentonFavorites(string itemid, bool addin)
        {
            bool itempresent = false;
            if (string.IsNullOrEmpty(itemid))
            {
                return false;
            }
            if (addin && !ValidfileName(itemid))
            {
                MessageBox.Show("A file with the part number " + itemid + " does not have Solidworks CAD Model or SPM item number assigned. Cannot add or remove from favorites. Please Try Again.", "SPM-Automation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[favourite] WHERE [Item]='" + itemid + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    itempresent = userCount == 1;
                    cn.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    MessageBox.Show(ex.Message, "SPM Connect - Check Item Present On SQL Favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return itempresent;
        }

        public bool ValidfileName(string Item_No)
        {
            bool validitem = true;
            string ItemNumbero = Item_No + "-0";

            if (!String.IsNullOrWhiteSpace(Item_No) && Item_No.Length == 6)
            {
                string first3char = Item_No.Substring(0, 3) + @"\";
                //MessageBox.Show(first3char);

                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";

                string Pathpart = (spmcadpath + first3char + Item_No + ".sldprt");
                string Pathassy = (spmcadpath + first3char + Item_No + ".sldasm");
                string PathPartNo = (spmcadpath + first3char + ItemNumbero + ".sldprt");
                string PathAssyNo = (spmcadpath + first3char + ItemNumbero + ".sldasm");

                if ((!File.Exists(Pathassy) || !File.Exists(Pathpart)) && (!File.Exists(PathAssyNo) || !File.Exists(PathPartNo)) && (!File.Exists(PathAssyNo) || !File.Exists(Pathpart)) && (!File.Exists(Pathassy) || !File.Exists(PathPartNo)) && (!File.Exists(PathPartNo) || !File.Exists(Pathpart)) && (!File.Exists(PathAssyNo) || !File.Exists(Pathassy)) && !File.Exists(Pathassy) && !File.Exists(PathAssyNo) && !File.Exists(Pathpart) && !File.Exists(PathPartNo))
                {
                    validitem = false;
                }
            }
            else
            {
                validitem = false;
            }
            return validitem;
        }

        private void Additemtofavoritessql(string itemid)
        {
            string userid = ConnectUser.UserName;
            userid += ",";
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[favourite] (Item,UserName) VALUES('" + itemid + "','" + userid + " ')";
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Item no " + itemid + " has been added to your favorites.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                MessageBox.Show(ex.Message, "SPM Connect - Add  Item To Favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void Updateusernametoitemonfavorites(string itemid, string updatedusername)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (updatedusername != "")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[favourite] SET UserName = '" + updatedusername + "'  WHERE Item = '" + itemid + "'";
                }
                else
                {
                    cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[favourite] WHERE Item = '" + itemid + "'";
                }

                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                MessageBox.Show(ex.Message, "SPM Connect - Update Item on Favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private string Getusernamesfromfavorites(string itemid)
        {
            string usersfav = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[favourite] WHERE [Item]='" + itemid + "' ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    usersfav = dr["UserName"].ToString();
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve user names from favorites", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return usersfav;
        }

        private bool Userexists(string usernames)
        {
            bool exists = false;
            string userid = ConnectUser.UserName;
            // Split string on spaces (this will separate all the words).
            foreach (string word in usernames.Split(','))
            {
                if (word == userid)
                {
                    exists = true;
                }
            }

            return exists;
        }

        private string Removeusername(string usernames)
        {
            string removedusername = "";
            string userid = ConnectUser.UserName;
            // Split string on spaces (this will separate all the words).
            foreach (string word in usernames.Split(','))
            {
                if (word.Trim() != userid)
                {
                    removedusername += word.Trim();
                    if (word.Trim() != "")
                    {
                        removedusername += ",";
                    }
                }
            }
            return removedusername.Trim();
        }

        #endregion Favorites
    }
}
