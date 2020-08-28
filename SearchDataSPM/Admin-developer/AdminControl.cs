using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SearchDataSPM.Admin_developer
{
    public partial class AdminControl : Form
    {
        #region steupvariables

        private string controluseraction;
        private int selectedindex;
        private log4net.ILog log;
        private List<UserInfo> users = new List<UserInfo>();
        private readonly ConnectAPI connectapi = new ConnectAPI();

        #endregion steupvariables

        #region loadtree

        public AdminControl()
        {
            InitializeComponent();
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            Connect_SPMSQL(0);
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Admin Control");
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void Connect_SPMSQL(int index)
        {
            try
            {
                Userlistbox.DataSource = null;
                Userlistbox.Items.Clear();
                users.Clear();
                users = connectapi.GetConnectUsersList();
                Userlistbox.DataSource = users;
                Userlistbox.DisplayMember = "Name";
                if (Userlistbox.Items.Count > 0)
                {
                    Userlistbox.SelectedItem = Userlistbox.Items[index];
                }
                Fillsupervisor();
                FillECRsupervisor();
                FillShippingsupervisor();
                Selectionchanged(users[index]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void Fillsupervisor()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT CONCAT(id, ' ', Name) as Supervisors  FROM [SPM_Database].[dbo].[Users]  WHERE PurchaseReqApproval = '1'  OR PurchaseReqApproval2 ='1'", connectapi.cn))
            {
                try
                {
                    supervisorcombox.DataSource = null;
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    supervisorcombox.AutoCompleteCustomSource = MyCollection;
                    supervisorcombox.DataSource = MyCollection;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill supervisor items Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void FillECRsupervisor()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT CONCAT(id, ' ', Name) as ECRSupervisors  FROM [SPM_Database].[dbo].[Users]  WHERE [ECRApproval] = '1'  OR [ECRApproval2] ='1'", connectapi.cn))
            {
                try
                {
                    ecrSupervisorcomboBox.DataSource = null;
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    ecrSupervisorcomboBox.AutoCompleteCustomSource = MyCollection;
                    ecrSupervisorcomboBox.DataSource = MyCollection;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill ECR supervisors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void FillShippingsupervisor()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT CONCAT(id, ' ', Name) as ShipSupervisors  FROM [SPM_Database].[dbo].[Users]  WHERE [ShipSupervisor] = '1'", connectapi.cn))
            {
                try
                {
                    shippingSupervisorcomboBox.DataSource = null;
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    shippingSupervisorcomboBox.AutoCompleteCustomSource = MyCollection;
                    shippingSupervisorcomboBox.DataSource = MyCollection;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Shipping supervisors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        #endregion loadtree

        #region Fillinfo

        private void Userlistbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Userlistbox.SelectedIndex > -1)
            {
                Selectionchanged(users[Userlistbox.SelectedIndex]);
            }
        }

        private void Selectionchanged(UserInfo user)
        {
            nametextbox.Text = user.Name;
            domaintxtbox.Text = user.UserName;
            activecadblocktxt.Text = user.ActiveBlockNumber;
            useremailtxt.Text = user.Email;
            sharepathtxt.Text = user.SharesFolder;
            idlabel.Text = "Id : " + user.ConnectId;
            empidtxt.Text = user.Emp_Id.ToString();

            if (user.Supervisor.ToString().Length > 0)
            {
                string MyString = user.Supervisor.ToString();
                MyString += " ";
                MyString += users.Find(i => i.ConnectId == user.Supervisor).Name;
                supervisorcombox.SelectedItem = MyString;
            }

            if (user.ECRSup != 0)
            {
                string MyString = user.ECRSup.ToString();
                MyString += " ";
                MyString += users.Find(i => i.ConnectId == user.ECRSup).Name;
                ecrSupervisorcomboBox.SelectedItem = MyString;
            }

            if (user.ShipSup != 0)
            {
                string MyString = user.ShipSup.ToString();
                MyString += " ";
                MyString += users.Find(i => i.ConnectId == user.ShipSup).Name;
                shippingSupervisorcomboBox.SelectedItem = MyString;
            }

            if (user.Dept.ToString().Length > 0)
            {
                deptcombobox.SelectedItem = user.Dept.ToString();
            }

            admintoggle.Checked = user.Admin;
            developertoggle.Checked = user.Developer;
            managementtoggle.Checked = user.Management;
            quotetoggle.Checked = user.Quote;
            papprovalchk.Checked = user.PurchaseReqApproval;
            pbuyerchk.Checked = user.PurchaseReqBuyer;
            papproval2chk.Checked = user.PurchaseReqApproval2;
            purchasereqtoggle.Checked = user.PurchaseReq;
            pricetoggle.Checked = user.PriceRight;

            shiptoggle.Checked = user.Shipping;

            shippingsupchk.Checked = user.ShipSupervisor;

            shippingmanagerchk.Checked = user.ShippingManager;

            cribouttoggle.Checked = user.CribCheckout;

            scanwotoggle.Checked = user.WOScan;

            cribshorttoggle.Checked = user.CribShort;
            //ECR
            ecrtoggle.Checked = user.ECR;

            woreleasetoggle.Checked = user.WORelease;

            itmdeptoggle.Checked = user.ItemDependencies;

            ecrapprovalchk.Checked = user.ECRApproval;

            ecrapproval2chk.Checked = user.ECRApproval2;

            ecrhandlerchk.Checked = user.ECRHandler;

            chkdrwtoggle.Checked = user.CheckDrawing;

            appdrwtoggle.Checked = user.ApproveDrawing;

            rptoggle.Checked = user.ReleasePackage;

            cntrlsapptoggle.Checked = user.ControlsApprovalDrawing;

            Runalltoggle();
        }

        #endregion Fillinfo

        #region Perfrom CRUD

        private void Addnewbttn_Click(object sender, EventArgs e)
        {
            Performaddnewbutton();
        }

        private void Performaddnewbutton()
        {
            selectedindex = Userlistbox.SelectedIndex;
            nametextbox.ReadOnly = false;
            empidtxt.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            //sharepathtxt.ReadOnly = false;
            selectfolder.Enabled = true;
            domaintxtbox.ReadOnly = false;
            delbttn.Visible = false;
            updatebttn.Visible = false;
            updatesavebttn.Visible = true;
            domaintxtbox.Text = @"SPM\";
            activecadblocktxt.Text = "";
            useremailtxt.Text = "";
            sharepathtxt.Text = @"\\SPM-ADFS\Shares\";
            idlabel.Text = "";
            empidtxt.Text = "";

            papprovalchk.Enabled = true;
            papproval2chk.Enabled = true;
            pbuyerchk.Enabled = true;

            shippingSupervisorcomboBox.Enabled = true;
            shippingsupchk.Enabled = true;
            shippingmanagerchk.Enabled = true;

            supervisorcombox.Enabled = true;
            ecrSupervisorcomboBox.Enabled = true;
            deptcombobox.Enabled = true;
            nametextbox.Text = "";
            cnclbttn.Visible = true;
            addnewbttn.Visible = false;
            nametextbox.Focus();
            nametextbox.SelectAll();
            controluseraction = "";
            controluseraction = "addnew";
            Userlistbox.Enabled = false;
            button1.Enabled = false;
            reluanchbttn.Enabled = false;
            parametersbttn.Enabled = false;
            custbttn.Enabled = false;
            matbttn.Enabled = false;
            UserStats.Enabled = false;

            papproval2chk.Checked = false;
            papprovalchk.Checked = false;
            pbuyerchk.Checked = false;

            shippingsupchk.Checked = false;
            shippingmanagerchk.Checked = false;

            ecrapprovalchk.Enabled = true;
            ecrapproval2chk.Enabled = true;
            ecrhandlerchk.Enabled = true;
            ecrapprovalchk.Checked = false;
            ecrapproval2chk.Checked = false;
            ecrhandlerchk.Checked = false;
            Enablealltoggles();
        }

        private void Enablealltoggles()
        {
            admintoggle.Enabled = true;
            quotetoggle.Enabled = true;
            pricetoggle.Enabled = true;
            shiptoggle.Enabled = true;
            managementtoggle.Enabled = true;
            developertoggle.Enabled = true;
            cribouttoggle.Enabled = true;
            purchasereqtoggle.Enabled = true;
            scanwotoggle.Enabled = true;
            cribshorttoggle.Enabled = true;
            ecrtoggle.Enabled = true;
            woreleasetoggle.Enabled = true;
            itmdeptoggle.Enabled = true;
            chkdrwtoggle.Enabled = true;
            appdrwtoggle.Enabled = true;
            cntrlsapptoggle.Enabled = true;
            rptoggle.Enabled = true;
        }

        private void Disablealltoggles()
        {
            admintoggle.Enabled = false;
            quotetoggle.Enabled = false;
            pricetoggle.Enabled = false;
            shiptoggle.Enabled = false;
            managementtoggle.Enabled = false;
            developertoggle.Enabled = false;
            cribouttoggle.Enabled = false;
            purchasereqtoggle.Enabled = false;
            scanwotoggle.Enabled = false;
            cribshorttoggle.Enabled = false;
            ecrtoggle.Enabled = false;
            woreleasetoggle.Enabled = false;
            itmdeptoggle.Enabled = false;
            chkdrwtoggle.Enabled = false;
            appdrwtoggle.Enabled = false;
            cntrlsapptoggle.Enabled = false;
            rptoggle.Enabled = false;
        }

        private void Delbttn_Click(object sender, EventArgs e)
        {
            Performdeletebttn();
        }

        private void Performdeletebttn()
        {
            DialogResult result = MessageBox.Show(
                 "Name = " + nametextbox.Text + Environment.NewLine +
                 @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
                 "Email = " + useremailtxt.Text + Environment.NewLine +
                 "Department = " + deptcombobox.SelectedItem.ToString() + Environment.NewLine +
                 "Admin = " + (admintoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Developer = " + (developertoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "QuoteAccess = " + (quotetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "PurchaseReq Access = " + (purchasereqtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Price Access = " + (pricetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Shipping Access = " + (shiptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
                 "ECR Supervisor = " + ecrSupervisorcomboBox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
                 "CribCheckOut = " + (cribouttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "CribShortNotfi = " + (cribshorttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Scan WorkOrder = " + (scanwotoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "ECR Access = " + (ecrtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "WO Release Access = " + (woreleasetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Item Dependencies Access = " + (itmdeptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "ECR Supervisor = " + (ecrapprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "ECR Approval = " + (ecrapproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "ECR Handler = " + (ecrhandlerchk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Shipping Supervisor = " + (shippingsupchk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Shipping Manager = " + (shippingmanagerchk.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Check Drawings = " + (chkdrwtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Approve Drawings = " + (appdrwtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Approve Control Drawings = " + (cntrlsapptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Release Package = " + (rptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Management = " + (managementtoggle.Checked ? "Yes" : "No"), "Remove user from system?",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                selectedindex = Userlistbox.SelectedIndex;
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                try
                {
                    SqlCommand cmd = connectapi.cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[Users] WHERE UserName = '" + domaintxtbox.Text + "'";
                    cmd.ExecuteNonQuery();
                    connectapi.cn.Close();
                    MessageBox.Show("User deleted successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    domaintxtbox.Text = "";
                    nametextbox.Text = "";

                    domaintxtbox.Text = @"SPM\";
                    activecadblocktxt.Text = "";
                    useremailtxt.Text = "";
                    sharepathtxt.Text = "";
                    idlabel.Text = "";
                    empidtxt.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                    Connect_SPMSQL(0);
                }
            }
        }

        private void Updatebttn_Click(object sender, EventArgs e)
        {
            Performupdateuserdet();
        }

        private void Performupdateuserdet()
        {
            selectedindex = Userlistbox.SelectedIndex;
            nametextbox.ReadOnly = false;
            empidtxt.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            selectfolder.Enabled = true;
            papprovalchk.Enabled = true;
            papproval2chk.Enabled = true;
            pbuyerchk.Enabled = true;

            ecrapprovalchk.Enabled = true;
            ecrapproval2chk.Enabled = true;
            ecrhandlerchk.Enabled = true;

            Enablealltoggles();

            shippingSupervisorcomboBox.Enabled = true;
            shippingsupchk.Enabled = true;
            shippingmanagerchk.Enabled = true;

            supervisorcombox.Enabled = true;
            ecrSupervisorcomboBox.Enabled = true;
            deptcombobox.Enabled = true;
            delbttn.Visible = false;
            addnewbttn.Visible = false;
            updatesavebttn.Visible = true;
            cnclbttn.Visible = true;
            updatebttn.Visible = false;
            controluseraction = "";
            controluseraction = "update";
            Userlistbox.Enabled = false;
            button1.Enabled = false;
            reluanchbttn.Enabled = false;
            parametersbttn.Enabled = false;
            custbttn.Enabled = false;
            matbttn.Enabled = false;
            UserStats.Enabled = false;
        }

        private void Updatesavebttn_Click(object sender, EventArgs e)
        {
            Saveuserdetails();
        }

        private void Saveuserdetails()
        {
            if (controluseraction == "update")
            {
                Updateuser();
            }
            else
            {
                Addnewuser();
            }
        }

        private void Updateuser()
        {
            DialogResult result = MessageBox.Show(
               "Name = " + nametextbox.Text + Environment.NewLine +
               @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
               "Email = " + useremailtxt.Text + Environment.NewLine +
               "Department = " + deptcombobox.SelectedItem.ToString() + Environment.NewLine +
               "Admin = " + (admintoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (developertoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quotetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReq Access = " + (purchasereqtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Price Access = " + (pricetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Access = " + (shiptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
               "ECR Supervisor = " + ecrSupervisorcomboBox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
               "CribCheckOut = " + (cribouttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "CribShortNotif = " + (cribshorttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Scan WorkOrder = " + (scanwotoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Access = " + (ecrtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "WO Release Access = " + (woreleasetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Item Dependencies Access = " + (itmdeptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Supervisor = " + (ecrapprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Approval = " + (ecrapproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Handler = " + (ecrhandlerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Supervisor = " + (shippingsupchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Manager = " + (shippingmanagerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Check Drawing = " + (chkdrwtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Approve Drawing = " + (appdrwtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Approve Controls Drawing = " + (cntrlsapptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Release Package = " + (rptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Management = " + (managementtoggle.Checked ? "Yes" : "No"), "Update User Information?",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string activeblocknumber = "";

                if (activecadblocktxt.Text.Length > 0)
                {
                    activeblocknumber = Char.ToUpper(activecadblocktxt.Text[0]) + activecadblocktxt.Text.Substring(1);
                }

                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                try
                {
                    SqlCommand cmd = connectapi.cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Users] SET Department = '" + deptcombobox.SelectedItem.ToString() + "'," +
                        "Admin = '" + (admintoggle.Checked ? "1" : "0") + "',Name = '" + nametextbox.Text.Trim() + "',ActiveBlockNumber = '" + activeblocknumber + "'," +
                        "Developer = '" + (developertoggle.Checked ? "1" : "0") + "',Management = '" + (managementtoggle.Checked ? "1" : "0") + "'," +
                        "Quote = '" + (quotetoggle.Checked ? "1" : "0") + "',PurchaseReq = '" + (purchasereqtoggle.Checked ? "1" : "0") + "'," +
                        "PurchaseReqApproval = '" + (papprovalchk.Checked ? "1" : "0") + "',PurchaseReqApproval2 = '" + (papproval2chk.Checked ? "1" : "0") + "'," +
                        "PurchaseReqBuyer = '" + (pbuyerchk.Checked ? "1" : "0") + "',ECRSup = '" + ecrSupervisorcomboBox.SelectedItem.ToString().Substring(0, 2) + "',Supervisor = '" + supervisorcombox.SelectedItem.ToString().Substring(0, 2) + "'," +
                        "Email = '" + useremailtxt.Text + "',PriceRight = '" + (pricetoggle.Checked ? "1" : "0") + "',CheckDrawing = '" + (chkdrwtoggle.Checked ? "1" : "0") + "',ApproveDrawing = '" + (appdrwtoggle.Checked ? "1" : "0") + "',ControlsApprovalDrawing = '" + (cntrlsapptoggle.Checked ? "1" : "0") + "',ReleasePackage = '" + (rptoggle.Checked ? "1" : "0") + "',Shipping = '" + (shiptoggle.Checked ? "1" : "0") + "'," +
                        "CribCheckout = '" + (cribouttoggle.Checked ? "1" : "0") + "',ShipSup = '" + (shippingSupervisorcomboBox.SelectedItem != null ? shippingSupervisorcomboBox.SelectedItem.ToString().Substring(0, 2) : "") + "',CribShort = '" + (cribshorttoggle.Checked ? "1" : "0") + "'," +
                        "ECR = '" + (ecrtoggle.Checked ? "1" : "0") + "',ShipSupervisor = '" + (shippingsupchk.Checked ? "1" : "0") + "',ShippingManager = '" + (shippingmanagerchk.Checked ? "1" : "0") + "',WORelease = '" + (woreleasetoggle.Checked ? "1" : "0") + "',ItemDependencies = '" + (itmdeptoggle.Checked ? "1" : "0") + "',ECRApproval = '" + (ecrapprovalchk.Checked ? "1" : "0") + "',ECRApproval2 = '" + (ecrapproval2chk.Checked ? "1" : "0") + "',ECRHandler = '" + (ecrhandlerchk.Checked ? "1" : "0") + "'," +
                        "WOScan = '" + (scanwotoggle.Checked ? "1" : "0") + "',SharesFolder = '" + sharepathtxt.Text.Trim() + "'," +
                        "Emp_Id = '" + empidtxt.Text.Trim() + "' WHERE UserName = '" + domaintxtbox.Text + "' ";

                    cmd.ExecuteNonQuery();
                    connectapi.cn.Close();
                    MessageBox.Show("User credentials updated successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                    Performcancelbutton();
                }
            }
            else if (result == DialogResult.No)
            {
                Performcancelbutton();
            }
        }

        private void Addnewuser()
        {
            DialogResult result = MessageBox.Show(
               "Name = " + nametextbox.Text + Environment.NewLine +
               @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
               "Email = " + useremailtxt.Text + Environment.NewLine +
               "Department = " + deptcombobox.SelectedItem.ToString() + Environment.NewLine +
               "Admin = " + (admintoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (developertoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quotetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReq Access = " + (purchasereqtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Price Access = " + (pricetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Access = " + (shiptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
               "ECR Supervisor = " + ecrSupervisorcomboBox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
               "CribCheckOut = " + (cribouttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "CribShortNotif = " + (cribshorttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Scan WorkOrder = " + (scanwotoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Access = " + (ecrtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "WO Release Access = " + (woreleasetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Item Dependencies Access = " + (itmdeptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Supervisor = " + (ecrapprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Approval = " + (ecrapproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "ECR Handler = " + (ecrhandlerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Supervisor = " + (shippingsupchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Manager = " + (shippingmanagerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Check Drawing = " + (chkdrwtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Approve Drawing = " + (appdrwtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Approve Controls Drawing = " + (cntrlsapptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Release Package = " + (rptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Management = " + (managementtoggle.Checked ? "Yes" : "No"), "Update User Information?",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string activeblocknumber = "";

                if (activecadblocktxt.Text.Length > 0)
                {
                    activeblocknumber = Char.ToUpper(activecadblocktxt.Text[0]) + activecadblocktxt.Text.Substring(1);
                }

                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                try
                {
                    SqlCommand cmd = connectapi.cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Users]([Emp_Id], [UserName], [Department], [Name],[ActiveBlockNumber],[Admin],[Developer],[Management]," +
                        "[Quote],[PurchaseReq],[PurchaseReqApproval],[PurchaseReqApproval2],[PurchaseReqBuyer],[PriceRight],[CheckDrawing],[ApproveDrawing],[ControlsApprovalDrawing],[ReleasePackage],[ShipSupervisor],[ShipSup],[ShippingManager],[CribCheckout],[CribShort],[WOScan],[Shipping],[Supervisor],[ECRSup]," +
                        "[Email],[SharesFolder],[ECR],[WORelease],[ItemDependencies],[ECRApproval],[ECRApproval2],[ECRHandler]) " +
                        "VALUES('" + empidtxt.Text.Trim() + "','" + domaintxtbox.Text.Trim() + "','" + deptcombobox.SelectedItem.ToString() + "'," +
                        "'" + nametextbox.Text.Trim() + "','" + activeblocknumber + "','" + (admintoggle.Checked ? "1" : "0") + "','" + (developertoggle.Checked ? "1" : "0") + "'," +
                        "'" + (managementtoggle.Checked ? "1" : "0") + "','" + (quotetoggle.Checked ? "1" : "0") + "','" + (purchasereqtoggle.Checked ? "1" : "0") + "'," +
                        "'" + (papprovalchk.Checked ? "1" : "0") + "','" + (papproval2chk.Checked ? "1" : "0") + "','" + (pbuyerchk.Checked ? "1" : "0") + "'," +
                        "'" + (pricetoggle.Checked ? "1" : "0") + "','" + (chkdrwtoggle.Checked ? "1" : "0") + "','" + (appdrwtoggle.Checked ? "1" : "0") + "','" + (cntrlsapptoggle.Checked ? "1" : "0") + "','" + (rptoggle.Checked ? "1" : "0") + "','" + (shippingSupervisorcomboBox.SelectedItem != null ? shippingSupervisorcomboBox.SelectedItem.ToString().Substring(0, 2).TrimEnd() : "") + "','" + (shippingmanagerchk.Checked ? "1" : "0") + "','" + (cribouttoggle.Checked ? "1" : "0") + "','" + (cribouttoggle.Checked ? "1" : "0") + "','" + (cribshorttoggle.Checked ? "1" : "0") + "','" + (scanwotoggle.Checked ? "1" : "0") + "','" + (shiptoggle.Checked ? "1" : "0") + "'," +
                        "'" + supervisorcombox.SelectedItem.ToString().Substring(0, 2).TrimEnd() + "','" + ecrSupervisorcomboBox.SelectedItem.ToString().Substring(0, 2).TrimEnd() + "','" + useremailtxt.Text + "','" + sharepathtxt.Text + "'," +
                        "'" + (ecrtoggle.Checked ? "1" : "0") + "','" + (woreleasetoggle.Checked ? "1" : "0") + "','" + (itmdeptoggle.Checked ? "1" : "0") + "','" + (ecrapprovalchk.Checked ? "1" : "0") + "','" + (ecrapproval2chk.Checked ? "1" : "0") + "','" + (ecrhandlerchk.Checked ? "1" : "0") + "')";
                    cmd.ExecuteNonQuery();
                    connectapi.cn.Close();
                    MessageBox.Show("New user added successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                    Performcancelbutton();
                }
            }
            else if (result == DialogResult.No)
            {
                Performcancelbutton();
            }
        }

        private void Cnclbttn_Click(object sender, EventArgs e)
        {
            Performcancelbutton();
        }

        private void Performcancelbutton()
        {
            Disablealltoggles();
            selectfolder.Enabled = false;
            domaintxtbox.Text = "";
            useremailtxt.Text = "";
            sharepathtxt.Text = "";
            nametextbox.Text = "";
            empidtxt.Text = "";
            activecadblocktxt.Text = "";
            papprovalchk.Enabled = false;
            papproval2chk.Enabled = false;
            pbuyerchk.Enabled = false;

            ecrapprovalchk.Enabled = false;
            ecrapproval2chk.Enabled = false;
            ecrhandlerchk.Enabled = false;

            shippingSupervisorcomboBox.Enabled = false;
            shippingsupchk.Enabled = false;
            shippingmanagerchk.Enabled = false;

            supervisorcombox.Enabled = false;
            ecrSupervisorcomboBox.Enabled = false;
            deptcombobox.Enabled = false;
            nametextbox.ReadOnly = true;
            empidtxt.ReadOnly = true;
            activecadblocktxt.ReadOnly = true;
            useremailtxt.ReadOnly = true;
            selectfolder.Enabled = false;
            domaintxtbox.ReadOnly = true;
            updatesavebttn.Visible = false;
            cnclbttn.Visible = false;
            addnewbttn.Visible = true;
            delbttn.Visible = true;
            updatebttn.Visible = true;
            Userlistbox.Enabled = true;
            button1.Enabled = true;
            reluanchbttn.Enabled = true;
            parametersbttn.Enabled = true;
            custbttn.Enabled = true;
            matbttn.Enabled = true;
            UserStats.Enabled = true;
            Connect_SPMSQL(selectedindex);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (papprovalchk.Checked)
            {
                pbuyerchk.Checked = false;
                papproval2chk.Checked = false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (pbuyerchk.Checked)
            {
                papprovalchk.Checked = false;
                papproval2chk.Checked = false;
            }
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (papproval2chk.Checked)
            {
                pbuyerchk.Checked = false;
                //papprovalchk.Checked = false;
            }
        }

        private void Selectfolder_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sharepathtxt.Text = Path.GetDirectoryName(openFileDialog1.FileName);
            }
        }

        private void Activecadblocktxt_TextChanged(object sender, EventArgs e)
        {
            if (activecadblocktxt.Text.Length > 0)
            {
                if (activecadblocktxt.Text.Length > 3)
                {
                    activecadblocktxt.Clear();
                }
            }
        }

        private void Activecadblocktxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (activecadblocktxt.Text.Length > 0)
            {
                if (!Char.IsLetter(activecadblocktxt.Text[0]))
                {
                    activecadblocktxt.Clear();
                }
            }
            if (activecadblocktxt.Text.Length > 1)
            {
                if (Char.IsLetter(activecadblocktxt.Text[1]))
                {
                    activecadblocktxt.Clear();
                }
            }
            if (activecadblocktxt.Text.Length > 2)
            {
                if (Char.IsLetter(activecadblocktxt.Text[2]))
                {
                    activecadblocktxt.Clear();
                }
            }
        }

        #endregion Perfrom CRUD

        #region Button Click Events

        private void Button1_Click(object sender, EventArgs e)
        {
            Admin_developer.UserLogs sPM_Connect = new Admin_developer.UserLogs
            {
                Owner = this
            };
            sPM_Connect.ShowDialog();
        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        private void Reluanchbttn_Click(object sender, EventArgs e)
        {
            Admin_developer.BlockedForms userStatus = new Admin_developer.BlockedForms
            {
                Owner = this
            };
            userStatus.ShowDialog();
        }

        private void UserStats_Click(object sender, EventArgs e)
        {
            Admin_developer.UserStatus userStatus = new Admin_developer.UserStatus
            {
                Owner = this
            };
            userStatus.ShowDialog(this);
        }

        private void Custbttn_Click(object sender, EventArgs e)
        {
            ManageCustomers customers = new ManageCustomers
            {
                Owner = this
            };
            customers.Show();
        }

        private void Matbttn_Click(object sender, EventArgs e)
        {
            Materials materials = new Materials
            {
                Owner = this
            };
            materials.Show();
        }

        private void Spmadmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Admin Control");
            this.Dispose();
        }

        private void Parametersbttn_Click(object sender, EventArgs e)
        {
            ConnectParameters parameters = new ConnectParameters();
            parameters.ShowDialog();
        }

        #endregion Button Click Events

        #region Toggle Events

        private void Admintoggle_CheckChanged(object sender, EventArgs e)
        {
            ToggleAction(sender as ToggleSlider.ToggleSliderComponent);
        }

        private void Runalltoggle()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() == typeof(ToggleSlider.ToggleSliderComponent))
                {
                    ToggleAction(c as ToggleSlider.ToggleSliderComponent);
                }
            }
            foreach (Control c in groupBox5.Controls)
            {
                if (c.GetType() == typeof(ToggleSlider.ToggleSliderComponent))
                {
                    ToggleAction(c as ToggleSlider.ToggleSliderComponent);
                }
            }
        }

        private void ToggleAction(ToggleSlider.ToggleSliderComponent tg)
        {
            if (tg.Checked)
            {
                tg.ToggleBarText = "Yes";
                tg.ToggleCircleColor = Color.Green;
                tg.ToggleColorBar = Color.White;
            }
            else
            {
                tg.ToggleBarText = "No";
                tg.ToggleCircleColor = Color.Red;
                tg.ToggleColorBar = Color.LightGray;
            }
        }

        #endregion Toggle Events

        #region shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                Saveuserdetails();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.X))
            {
                Performcancelbutton();
                return true;
            }

            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                Performaddnewbutton();
                return true;
            }

            if (keyData == (Keys.Control | Keys.E))
            {
                Performupdateuserdet();
                return true;
            }

            if (keyData == Keys.Delete)
            {
                if (updatebttn.Visible)
                {
                    Performdeletebttn();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        private void Spmadmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (updatesavebttn.Visible)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect - Save User Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //this.Close();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = result == DialogResult.No;
                }
            }
        }

        private void Ecrapprovalchk_CheckedChanged(object sender, EventArgs e)
        {
            if (ecrapprovalchk.Checked)
            {
                ecrhandlerchk.Checked = false;
                ecrapproval2chk.Checked = false;
            }
        }

        private void Ecrhandlerchk_CheckedChanged(object sender, EventArgs e)
        {
            if (ecrhandlerchk.Checked)
            {
                ecrapprovalchk.Checked = false;
                ecrapproval2chk.Checked = false;
            }
        }

        private void Ecrapproval2chk_CheckedChanged(object sender, EventArgs e)
        {
            if (ecrapproval2chk.Checked)
            {
                ecrhandlerchk.Checked = false;
                ecrapprovalchk.Checked = false;
            }
        }

        private void Supervisorcombox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                supervisorcombox.Focus();
            }
        }

        private void EcrSupervisorcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ecrSupervisorcomboBox.Focus();
            }
        }

        private void Shippingmanagerchk_CheckedChanged(object sender, EventArgs e)
        {
            if (shippingmanagerchk.Checked)
            {
                shippingsupchk.Checked = false;
            }
        }

        private void Shippingsupchk_CheckedChanged(object sender, EventArgs e)
        {
            if (shippingsupchk.Checked)
            {
                shippingmanagerchk.Checked = false;
            }
        }
    }
}