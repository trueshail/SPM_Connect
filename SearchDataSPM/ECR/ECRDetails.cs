using ExtractLargeIconFromFile;
using SearchDataSPM.ECR;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using wpfPreviewFlowControl;

namespace SearchDataSPM
{
    public partial class ECRDetails : Form
    {
        #region Load Invoice Details and setting Parameters

        private string connection;
        private DataTable dt = new DataTable();
        private SqlConnection cn;
        private SqlCommand _command;
        private SqlDataAdapter _adapter;
        private string Invoice_Number = "";
        private SPMConnectAPI.ECR connectapi = new SPMConnectAPI.ECR();

        private bool ecrcreator = false;
        private bool ecrsup = false;
        private bool ecrmanager = false;
        private bool ecrhandler = false;
        private int myid = 0;
        private int supervisorid = 0;
        private string userfullname = "";

        private log4net.ILog log;

        private ErrorHandler errorHandler = new ErrorHandler();

        public ECRDetails(string username, string invoiceno)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
            connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dt = new DataTable();
            _command = new SqlCommand();
            this.userfullname = username;
            this.Invoice_Number = invoiceno;
        }

        private void ECRDetails_Load(object sender, EventArgs e)
        {
            this.Text = "SPM Connect ECR Details - " + Invoice_Number;
            GetUserCreds();

            FillProjectManagers();
            FillRequestedBy();
            FillDepartments();

            if (GetECRInfo(Invoice_Number))
            {
                FillECRDetails();
                if (!supcheckBox.Checked)
                {
                    processeditbutton();
                }
            }
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened ECR Detail " + Invoice_Number + " by " + System.Environment.UserName);
        }

        private string Get_username()
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

        private void GetUserCreds()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + Get_username().ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    supervisorid = Convert.ToInt32(dr["Supervisor"].ToString());
                    myid = Convert.ToInt32(dr["id"].ToString());
                    string ecrsupstring = dr["ECRApproval"].ToString();
                    string ecrmanagerstring = dr["ECRApproval2"].ToString();
                    string ecrhandlerstring = dr["ECRHandler"].ToString();

                    if (ecrsupstring == "1")
                    {
                        ecrsup = true;
                    }
                    if (ecrmanagerstring == "1")
                    {
                        ecrmanager = true;
                    }
                    if (ecrhandlerstring == "1")
                    {
                        ecrhandler = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Error Getting Full User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private bool GetECRInfo(string invoicenumber)
        {
            bool fillled = false;
            string sql = "SELECT * FROM [SPM_Database].[dbo].[ECR] WHERE ECRNo = '" + invoicenumber + "'";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                _adapter = new SqlDataAdapter(sql, cn);
                dt.Clear();
                _adapter.Fill(dt);

                fillled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get ECR Base Info From SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return fillled;
        }

        #endregion Load Invoice Details and setting Parameters

        #region Fill information on controls

        private void FillECRDetails()
        {
            DataRow r = dt.Rows[0];

            ecrnotxtbox.Text = r["ECRNo"].ToString();
            descriptiontxtbox.Text = r["Description"].ToString();
            notestxt.Text = r["Comments"].ToString();
            jobtxt.Text = r["JobNo"].ToString();
            satxt.Text = r["SANo"].ToString();
            partnotxt.Text = r["PartNo"].ToString();

            fetchJobSaNames(r["JobNo"].ToString(), r["SANo"].ToString());

            Createdon.Text = "Created On : " + r["DateCreated"].ToString();

            CreatedBy.Text = "Created By : " + r["CreatedBy"].ToString();

            lastsavedby.Text = "Last Saved By : " + r["LastSavedBy"].ToString();

            lastsavedon.Text = "Last Saved On : " + r["DateLastSaved"].ToString();

            statuslbl.Text = "Status : " + r["Status"].ToString();

            string submittedtosup = r["Submitted"].ToString();
            string submittedtomanager = r["SupApproval"].ToString();
            string submittedtoecrhandler = r["Approved"].ToString();
            string ecrcomplete = r["Completed"].ToString();

            handleCheckBoxes(submittedtosup, submittedtomanager, submittedtoecrhandler, ecrcomplete, r["SupervisorId"].ToString(),
                r["SubmitToId"].ToString(), r["AssignedTo"].ToString(), r["CompletedBy"].ToString(), r["SubmittedOn"].ToString(), r["SupApprovedOn"].ToString(), r["ApprovedOn"].ToString(), r["CompletedOn"].ToString());

            string projectmanager = r["ProjectManager"].ToString();

            if (projectmanager.Length > 0)
            {
                projectmanagercombobox.SelectedItem = projectmanager;
            }

            string department = r["Department"].ToString();

            if (department.Length > 0)
            {
                departmentcomboBox.SelectedItem = department;
            }

            string requestedby = r["RequestedBy"].ToString();

            if (requestedby.Length > 0)
            {
                requestedbycombobox.SelectedItem = requestedby;
            }

            if (userfullname == requestedbycombobox.Text)
            {
                ecrcreator = true;
            }

            checkEditButtonRights(Convert.ToInt32(r["SupervisorId"].ToString()), Convert.ToInt32(r["SubmitToId"].ToString()), Convert.ToInt32(r["AssignedTo"].ToString()));
            filllistview(ecrnotxtbox.Text);
        }

        private void fetchJobSaNames(string jobno, string sano)
        {
            if (jobno.Length == 5)
            {
                jobnamelbl.Text = connectapi.GetJobName(jobno);
            }
            else
            {
                jobnamelbl.Text = "Job Name :";
            }

            if (sano.Length == 6)
            {
                subassylbl.Text = connectapi.GetSAName(sano);
            }
            else
            {
                subassylbl.Text = "Sub Assy Name :";
            }
        }

        private void handleCheckBoxes(string submittedtosup, string submittedtomanager, string submittedtoecrhandler, string ecrcomplete,
            string supervisorid, string managerid, string assignedto, string completedby, string submittedon, string SupApprovedOn, string approvedon, string completedon)
        {
            if (submittedtosup == "1")
            {
                supcheckBox.Checked = true;
                supcheckBox.Text = "Submitted to " + connectapi.getNameByConnectEmpId(supervisorid) + " on " + submittedon + "";
            }
            else if (submittedtosup == "3")
            {
                supcheckBox.Checked = true;
            }
            else
            {
                supcheckBox.Checked = false;
                supcheckBox.Text = "Submit to Supervisor";
            }

            if (submittedtomanager == "1")
            {
                managercheckBox.Checked = true;
                managercheckBox.Text = "Submitted to " + connectapi.getNameByConnectEmpId(managerid) + " on " + SupApprovedOn + "";
            }
            else if (submittedtomanager == "3")
            {
                managercheckBox.Checked = true;
                managercheckBox.Text = "Rejected by " + connectapi.getNameByConnectEmpId(supervisorid) + " on " + SupApprovedOn + "";
            }
            else
            {
                managercheckBox.Checked = false;
            }

            if (submittedtoecrhandler == "1")
            {
                submitecrhandlercheckBox.Checked = true;
                submitecrhandlercheckBox.Text = "Assigned to " + connectapi.getNameByConnectEmpId(assignedto) + " on " + approvedon + "";
            }
            else if (submittedtoecrhandler == "3")
            {
                submitecrhandlercheckBox.Checked = true;
                submitecrhandlercheckBox.Text = "Rejected by " + connectapi.getNameByConnectEmpId(managerid) + " on " + approvedon + "";
            }
            else
            {
                submitecrhandlercheckBox.Checked = false;
                submitecrhandlercheckBox.Text = "Submit to ECR Handler";
            }

            if (ecrcomplete == "1")
            {
                ecrhandlercheckBox.Checked = true;
                ecrhandlercheckBox.Text = "Completed by " + completedby + " on " + completedon + "";
            }
            else if (ecrcomplete == "3")
            {
                ecrhandlercheckBox.Checked = true;
            }
            else
            {
                ecrhandlercheckBox.Checked = false;
                ecrhandlercheckBox.Text = "Close ECR Request";
            }
        }

        private void checkEditButtonRights(int supervisorid, int managerid, int assignedto)
        {
            if (ecrcreator && !managercheckBox.Checked)
            {
                supcheckBox.Enabled = true;
            }
            else if (supervisorid == myid && ecrsup && !submitecrhandlercheckBox.Checked)
            {
                managercheckBox.Enabled = true;
                if (managercheckBox.Checked)
                {
                    rejectbttn.Visible = false;
                }
                else
                {
                    rejectbttn.Visible = true;
                }
            }
            else if (managerid == myid && ecrmanager && !ecrhandlercheckBox.Checked)
            {
                submitecrhandlercheckBox.Enabled = true;
                if (submitecrhandlercheckBox.Checked)
                {
                    rejectbttn.Visible = false;
                }
                else
                {
                    rejectbttn.Visible = true;
                }
                attachlbl.Visible = false;
                browsebttn.Visible = false;
                delbttn.Visible = false;
                iteminfogroupBox.Enabled = false;
                descriptiontxtbox.Enabled = false;
            }
            else if (assignedto == myid && ecrhandler)
            {
                attachlbl.Visible = false;
                browsebttn.Visible = false;
                delbttn.Visible = false;
                ecrhandlercheckBox.Enabled = true;
                iteminfogroupBox.Enabled = false;
                descriptiontxtbox.Enabled = false;
            }
            else
            {
                perfromEditlockdown();
            }
        }

        private void perfromEditlockdown()
        {
            editbttn.Visible = false;
            savbttn.Enabled = false;
            savbttn.Visible = false;
            notestxt.ReadOnly = true;
            descriptiontxtbox.ReadOnly = true;
            iteminfogroupBox.Enabled = false;
            submissiongroupBox.Enabled = false;
        }

        #endregion Fill information on controls

        #region Filling Up Comboboxes

        private void FillDepartments()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillDepartments();
            departmentcomboBox.AutoCompleteCustomSource = MyCollection;
            departmentcomboBox.DataSource = MyCollection;
        }

        private void FillProjectManagers()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRProjectManagers();
            projectmanagercombobox.AutoCompleteCustomSource = MyCollection;
            projectmanagercombobox.DataSource = MyCollection;
        }

        private void FillRequestedBy()
        {
            AutoCompleteStringCollection MyCollection = connectapi.FillECRRequestedBy();
            requestedbycombobox.AutoCompleteCustomSource = MyCollection;
            requestedbycombobox.DataSource = MyCollection;
        }

        #endregion Filling Up Comboboxes

        #region shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                this.Close();

                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                perfromsavebttn("", true, false);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region FormClosing

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible == true)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect - Save Invoice Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connectapi.CheckoutInvoice(ecrnotxtbox.Text.Trim());
                    this.Dispose();
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
            else
            {
                connectapi.CheckoutInvoice(ecrnotxtbox.Text.Trim());
            }
        }

        #endregion FormClosing

        #region Process Save

        private void perfromlockdown()
        {
            editbttn.Visible = true;
            savbttn.Enabled = false;
            savbttn.Visible = false;
            notestxt.ReadOnly = true;
            descriptiontxtbox.ReadOnly = true;
            iteminfogroupBox.Enabled = false;
            submissiongroupBox.Enabled = false;
            browsebttn.Enabled = false;
            delbttn.Enabled = false;
            browsebttn.Visible = false;
            delbttn.Visible = false;
            attachlbl.Visible = false;
        }

        private List<string> list = new List<string>();

        private void graballinfor()
        {
            list.Clear();
            Regex reg = new Regex("['\",_^]");
            list.Add(reg.Replace(ecrnotxtbox.Text, "''"));
            list.Add(reg.Replace(jobtxt.Text, "''"));
            list.Add(reg.Replace(satxt.Text, "''"));
            list.Add(reg.Replace(partnotxt.Text, "''"));
            if (jobnamelbl.Text.Substring(0, 3) == "Job")
            {
                list.Add(reg.Replace("", "''"));
            }
            else
            {
                list.Add(reg.Replace(jobnamelbl.Text, "''"));
            }

            if (subassylbl.Text.Substring(0, 3) == "Sub")
            {
                list.Add(reg.Replace("", "''"));
            }
            else
            {
                list.Add(reg.Replace(subassylbl.Text, "''"));
            }

            list.Add(reg.Replace(projectmanagercombobox.Text, "''"));
            list.Add(reg.Replace(requestedbycombobox.Text, "''"));
            list.Add(reg.Replace(departmentcomboBox.Text, "''"));
            list.Add(reg.Replace(descriptiontxtbox.Text, "''"));
            list.Add(reg.Replace(notestxt.Text, "''"));
        }

        private void savbttn_Click(object sender, EventArgs e)
        {
            perfromsavebttn("", true, false);
        }

        private void perfromsavebttn(string typeofSave, bool buttonclick, bool rejectbutton)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            this.BackColor = Color.SteelBlue;
            perfromlockdown();
            graballinfor();
            if (processSaveType(typeofSave, buttonclick, rejectbutton))
            {
                if (GetECRInfo(list[0].ToString()))
                {
                    FillECRDetails();
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Error occured while saving data.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (GetECRInfo(list[0].ToString()))
                {
                    FillECRDetails();
                }
            }
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private bool processSaveType(string typeofSave, bool savebttn, bool rejectbttn)
        {
            bool success = false;
            if (savebttn)
            {
                if (ecrcreator)
                {
                    success = connectapi.UpdateECRDetsToSql("Creator", list[0].ToString(), list[1].ToString(),
                     list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                     list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                     list[10].ToString(), 0, 0, 0, 0, "", "", rejectbttn);
                }
                else if (ecrsup)
                {
                    success = connectapi.UpdateECRDetsToSql("Supervisor", list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, 0, 0, "", "", rejectbttn);
                }
                else if (ecrmanager)
                {
                    success = connectapi.UpdateECRDetsToSql("Manager", list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, 0, 0, "", "", rejectbttn);
                }
                else if (ecrhandler)
                {
                    success = connectapi.UpdateECRDetsToSql("Handler", list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, 0, 0, "", "", rejectbttn);
                }
            }
            else
            {
                if (typeofSave == "Submitted")
                {
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                       list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                       list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                       list[10].ToString(), 1, 0, 0, 0, "", "", rejectbttn);
                }
                else if (typeofSave == "SupSubmit")
                {
                    string managerid = "";
                    if (!rejectbttn)
                    {
                        // Get the option to select the available managers

                        ECR_Users ecrUser = new ECR_Users();
                        ecrUser.IsSupervisor(true);
                        ecrUser.formtext("ECR - Select available user to send this ECR for approval.");

                        if (ecrUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            managerid = ecrUser.ValueIWant;
                        }

                        if (managerid.Length > 0)
                        {
                            success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                            list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                            list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                            list[10].ToString(), 0, 1, 0, 0, managerid, "", rejectbttn);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                       list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                       list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                       list[10].ToString(), 0, 1, 0, 0, managerid, "", rejectbttn);
                    }
                }
                else if (typeofSave == "SupSubmitFalse")
                {
                    // Get the option to select the available managers

                    string managerid = "";
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, rejectbttn ? 3 : 0, 0, 0, managerid, "", rejectbttn);
                }
                else if (typeofSave == "ManagerApproved")
                {
                    string ecrhandler = "";

                    if (!rejectbttn)
                    {
                        // Get the option to select the available ecr handlers

                        ECR_Users ecrUser = new ECR_Users();
                        ecrUser.IsSupervisor(false);
                        ecrUser.formtext("ECR - Select available user to send this ECR for changes to be completed.");

                        if (ecrUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            ecrhandler = ecrUser.ValueIWant;
                        }
                        if (ecrhandler.Length > 0)
                        {
                            success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                            list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                            list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                            list[10].ToString(), 0, 0, 1, 0, "", ecrhandler, rejectbttn);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                        list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                        list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                        list[10].ToString(), 0, 0, 1, 0, "", ecrhandler, rejectbttn);
                    }
                }
                else if (typeofSave == "ManagerApprovedFalse")
                {
                    // Get the option to select the available ecr handlers
                    string ecrhandler = "";
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, rejectbttn ? 3 : 0, 0, "", ecrhandler, rejectbttn);
                }
                else if (typeofSave == "Completed")
                {
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, 0, 1, "", "", rejectbttn);
                }
                else if (typeofSave == "CompletedFalse")
                {
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, 0, 0, "", "", rejectbttn);
                }
                else
                {
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0].ToString(), list[1].ToString(),
                    list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(),
                    list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(),
                    list[10].ToString(), 0, 0, 0, 0, "", "", rejectbttn);
                }
                SaveReport(ecrnotxtbox.Text);
            }
            return success;
        }

        #endregion Process Save

        #region Process Edit

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        private void processeditbutton()
        {
            this.BackColor = Color.FromArgb(62, 69, 76);
            editbttn.Visible = false;
            savbttn.Enabled = true;
            savbttn.Visible = true;
            notestxt.ReadOnly = false;
            descriptiontxtbox.ReadOnly = false;
            iteminfogroupBox.Enabled = true;
            submissiongroupBox.Enabled = true;
            browsebttn.Enabled = true;
            delbttn.Enabled = true;
            browsebttn.Visible = true;
            delbttn.Visible = true;
            attachlbl.Visible = true;
            DataRow r = dt.Rows[0];
            checkEditButtonRights(Convert.ToInt32(r["SupervisorId"].ToString()), Convert.ToInt32(r["SubmitToId"].ToString()), Convert.ToInt32(r["AssignedTo"].ToString()));
        }

        #endregion Process Edit

        #region Print Reports

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(ecrnotxtbox.Text);
            form1.getreport("ECR");
            form1.Show();
        }

        #endregion Print Reports

        #region Save Report

        private void SaveReport(string reqno)
        {
            string fileName = "";
            string filepath = @"\\spm-adfs\SDBASE\Reports\ECR_Reports\";
            System.IO.Directory.CreateDirectory(filepath);
            fileName = filepath + reqno + ".pdf";
            SaveReport(reqno, fileName);
        }

        public void SaveReport(string invoiceno, string fileName)
        {
            RS2005.ReportingService2005 rs;
            RE2005.ReportExecutionService rsExec;

            // Create a new proxy to the web service
            rs = new RS2005.ReportingService2005();
            rsExec = new RE2005.ReportExecutionService();

            // Authenticate to the Web service using Windows credentials
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.Url = "http://spm-sql/reportserver/reportservice2005.asmx";
            rsExec.Url = "http://spm-sql/reportserver/reportexecution2005.asmx";

            string historyID = null;
            string deviceInfo = null;
            string format = "PDF";
            Byte[] results;
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            RE2005.Warning[] warnings = null;
            string[] streamIDs = null;
            string _reportName = @"/GeniusReports/Job/SPM_ECR";

            string _historyID = null;
            bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            RS2005.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                RE2005.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);
                RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];

                if (_parameters.Length > 0)
                {
                    parameters[0] = new RE2005.ParameterValue
                    {
                        //parameters[0].Label = "";
                        Name = "pInvno",
                        Value = invoiceno
                    };
                }
                rsExec.SetExecutionParameters(parameters, "en-us");

                results = rsExec.Render(format, deviceInfo,
                          out extension, out encoding,
                          out mimeType, out warnings, out streamIDs);

                try
                {
                    File.WriteAllBytes(fileName, results);
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                    //MessageBox.Show(e.Message, "SPM Connect - Save Report", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        #endregion Save Report

        private void SupcheckBox_Click(object sender, EventArgs e)
        {
            if (supcheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    supcheckBox.Text = "Submit to Supervisor";
                    supcheckBox.Checked = false;
                    perfromsavebttn("SubmittedFalse", false, false);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);
                }
                else
                {
                    supcheckBox.Checked = true;
                }
            }
            else
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this ECR for approval?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send an email to respective supervisor for approval.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    errorProvider1.Clear();
                    if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                    {
                        supcheckBox.Text = "Submitted to Supervisor";
                        perfromsavebttn("Submitted", false, false);
                        preparetosendemail("Submitted", false);
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (satxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(satxt, "Sub Assy No cannot be empty");
                        }
                        if (jobtxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(jobtxt, "Job Number cannot be empty");
                        }
                        if (descriptiontxtbox.Text.Length == 0)
                        {
                            errorProvider1.SetError(descriptiontxtbox, "Description cannot be empty");
                        }
                        if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                        {
                            errorProvider1.Clear();
                        }
                        supcheckBox.Checked = false;
                    }
                }
                else
                {
                    supcheckBox.Checked = false;
                }
            }
        }

        private void ManagercheckBox_Click(object sender, EventArgs e)
        {
            if (managercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    managercheckBox.Checked = false;
                    managercheckBox.Text = "Submit to ECR Manager";
                    perfromsavebttn("SupSubmitFalse", false, false);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);
                }
                else
                {
                    managercheckBox.Checked = true;
                }
            }
            else
            {
                errorProvider1.Clear();
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this ECR for approval?" + Environment.NewLine +
                    "Please select available manager on your next step." + Environment.NewLine +
                    "This will send an email to respective ECR manager for approval.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                    {
                        managercheckBox.Text = "Submitted to ECR Manager";
                        perfromsavebttn("SupSubmit", false, false);
                        preparetosendemail("SupSubmit", false);
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (satxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(satxt, "Sub Assy No cannot be empty");
                        }
                        if (jobtxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(jobtxt, "Job Number cannot be empty");
                        }
                        if (descriptiontxtbox.Text.Length == 0)
                        {
                            errorProvider1.SetError(descriptiontxtbox, "Description cannot be empty");
                        }
                        if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                        {
                            errorProvider1.Clear();
                        }
                        managercheckBox.Checked = false;
                    }
                }
                else
                {
                    managercheckBox.Checked = false;
                }
            }
        }

        private void SubmitecrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (submitecrhandlercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from process?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    submitecrhandlercheckBox.Checked = false;
                    submitecrhandlercheckBox.Text = "Submit to ECR Handler";
                    perfromsavebttn("ManagerApprovedFalse", false, false);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);
                }
                else
                {
                    submitecrhandlercheckBox.Checked = true;
                }
            }
            else
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send this ECR for approval?" + Environment.NewLine +
                    "Please select available ECR Handler on your next step." + Environment.NewLine +
                    "This will notify respective ECR handler.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    errorProvider1.Clear();
                    if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                    {
                        submitecrhandlercheckBox.Text = "Submitted to ECR Handler";
                        perfromsavebttn("ManagerApproved", false, false);
                        preparetosendemail("ManagerApproved", false);
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (satxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(satxt, "Sub Assy No cannot be empty");
                        }
                        if (jobtxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(jobtxt, "Job Number cannot be empty");
                        }
                        if (descriptiontxtbox.Text.Length == 0)
                        {
                            errorProvider1.SetError(descriptiontxtbox, "Description cannot be empty");
                        }
                        if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                        {
                            errorProvider1.Clear();
                        }
                        submitecrhandlercheckBox.Checked = false;
                    }
                }
                else
                {
                    submitecrhandlercheckBox.Checked = false;
                }
            }
        }

        private void EcrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (ecrhandlercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this ECR not completed?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ecrhandlercheckBox.Checked = false;
                    ecrhandlercheckBox.Text = "Close ECR Request";
                    perfromsavebttn("CompletedFalse", false, false);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);
                }
                else
                {
                    ecrhandlercheckBox.Checked = true;
                }
            }
            else
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this ECR as complete?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to associated people with this ECR.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    errorProvider1.Clear();
                    if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                    {
                        ecrhandlercheckBox.Text = "Completed";
                        perfromsavebttn("Completed", false, false);
                        preparetosendemail("Completed", false);
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (satxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(satxt, "Sub Assy No cannot be empty");
                        }
                        if (jobtxt.Text.Length == 0)
                        {
                            errorProvider1.SetError(jobtxt, "Job Number cannot be empty");
                        }
                        if (descriptiontxtbox.Text.Length == 0)
                        {
                            errorProvider1.SetError(descriptiontxtbox, "Description cannot be empty");
                        }
                        if (jobtxt.Text.Length > 0 && satxt.Text.Length > 0 && descriptiontxtbox.Text.Length > 0)
                        {
                            errorProvider1.Clear();
                        }
                        ecrhandlercheckBox.Checked = false;
                    }
                }
                else
                {
                    ecrhandlercheckBox.Checked = false;
                }
            }
        }

        private void jobtxt_TextChanged(object sender, EventArgs e)
        {
            if (jobtxt.Text.Length == 5)
            {
                fetchJobSaNames(jobtxt.Text.Trim(), satxt.Text.Trim());
            }
            else
            {
                jobnamelbl.Text = "Job Name :";
            }
        }

        private void satxt_TextChanged(object sender, EventArgs e)
        {
            if (satxt.Text.Length == 6 && Char.IsLetter(satxt.Text[0]))
            {
                fetchJobSaNames(jobtxt.Text.Trim(), satxt.Text.Trim());
            }
            else
            {
                subassylbl.Text = "Sub Assy Name :";
            }
        }

        private void jobtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
            }
            else
            {
                e.Handled = true;
            }
        }

        private void satxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }

        private void rejectbttn_Click(object sender, EventArgs e)
        {
            if (ecrsup)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to reject this ECR?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to associated people with this ECR.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    perfromsavebttn("SupSubmitFalse", false, true);
                    preparetosendemail("SupSubmitFalse", true);
                }
            }
            else if (ecrmanager)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to reject this ECR?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to associated people with this ECR.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    perfromsavebttn("ManagerApprovedFalse", false, true);
                    preparetosendemail("ManagerApprovedFalse", true);
                }
            }
        }

        #region Sending Email

        private void sendemail(string emailtosend, string subject, string body, string filetoattach, string cc, string extracc)
        {
            if (sendemailyesno())
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
                    if (extracc == "")
                    {
                    }
                    else
                    {
                        message.CC.Add(extracc);
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
                    Debug.Print(ex.ToString());
                    //MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Send Email", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Email turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool sendemailyesno()
        {
            bool sendemail = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailReq'", cn))
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
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Limit for purchasing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            if (limit == "1")
            {
                sendemail = true;
            }
            return sendemail;
        }

        private void preparetosendemail(string typeofSave, bool rejectbttn)
        {
            string fileName = "";
            string filepath = @"\\spm-adfs\SDBASE\Reports\ECR_Reports\";
            fileName = filepath + ecrnotxtbox.Text.Trim() + ".pdf";
            if (typeofSave == "Submitted")
            {
                sendemailtosupervisor(fileName);
            }
            else if (typeofSave == "SupSubmit")
            {
                if (rejectbttn)
                {
                    // send email to user notifying rejected ecr
                    sendemailtouser(fileName, "supervisor", rejectbttn);
                }
                else
                {
                    //send email to manager and cc requestedby
                    sendemailtouser(fileName, "supervisor", rejectbttn);
                    sendemailtoManager(fileName);
                }
            }
            else if (typeofSave == "SupSubmitFalse")
            {
                // ecr out of system
                sendemailtouser(fileName, "supervisor", rejectbttn);
            }
            else if (typeofSave == "ManagerApproved")
            {
                if (rejectbttn)
                {
                    //send email to user and cc supervisor regarding the ecr is rejected
                    sendemailtouser(fileName, "manager", rejectbttn);
                }
                else
                {
                    // send ecr to ecr handler and cc user and supervisor
                    sendemailtouser(fileName, "manager", rejectbttn);
                    sendemailtoHandler(fileName);
                }
            }
            else if (typeofSave == "ManagerApprovedFalse")
            {
                sendemailtouser(fileName, "manager", rejectbttn);
            }
            else if (typeofSave == "Completed")
            {
                // send email to user and cc manager and supervisor
                sendemailtouser(fileName, "ecrhandler", rejectbttn);
            }
            else if (typeofSave == "CompletedFalse")
            {
                //
            }
        }

        private string getUserNameEmail(int id)
        {
            string Email = "";
            string name = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + id + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Email = dr["Email"].ToString();
                    name = dr["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Supervisor Name and Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            if (Email.Length > 0)
            {
                return Email + "][" + name;
            }
            else if (name.Length > 0)
            {
                return Email + "][" + name;
            }
            else
            {
                return "][";
            }
        }

        private string getusernameandemail(string requestby)
        {
            string Email = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [Name]='" + requestby.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Email = dr["Email"].ToString();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get User Name and Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            if (Email.Length > 0)
            {
                return Email;
            }
            else
            {
                return "";
            }
        }

        private void sendemailtosupervisor(string fileName)
        {
            string nameemail = getUserNameEmail(connectapi.getsupervisorId());

            string[] values = nameemail.Replace("][", "~").Split('~');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            string email = values[0];
            string name = values[1];

            string[] names = name.Replace(" ", "~").Split('~');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim();
            }
            name = names[0];
            sendemail(email, ecrnotxtbox.Text + " ECR Approval Required", "Hello " + name + "," + Environment.NewLine + userfullname + " sent this engineering change request for approval.", fileName, "", "");
        }

        private void sendemailtoManager(string fileName)
        {
            DataRow r = dt.Rows[0];
            string supnameemail = getUserNameEmail(Convert.ToInt32(r["SubmitToId"].ToString()));
            string[] values = supnameemail.Replace("][", "~").Split('~');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            string manageremail = values[0];
            string name = values[1];

            string[] names = name.Replace(" ", "~").Split('~');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim();
            }
            name = names[0];

            sendemail(manageremail, ecrnotxtbox.Text.Trim() + " ECR Approval Required", "Hello " + name + "," + Environment.NewLine + r["SupApprovalBy"].ToString() + " sent this engineering change request for approval.", fileName, "", "");
        }

        private void sendemailtoHandler(string fileName)
        {
            DataRow r = dt.Rows[0];
            string supnameemail = getUserNameEmail(Convert.ToInt32(r["AssignedTo"].ToString()));
            string[] values = supnameemail.Replace("][", "~").Split('~');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            string manageremail = values[0];
            string name = values[1];

            string[] names = name.Replace(" ", "~").Split('~');
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim();
            }
            name = names[0];

            sendemail(manageremail, ecrnotxtbox.Text.Trim() + " ECR Complettion Required", "Hello " + name + "," + Environment.NewLine + r["ApprovedBy"].ToString() + " sent this engineering change request for completion and changes to be made.", fileName, "", "");
        }

        private void sendemailtouser(string fileName, string triggerby, bool rejected)
        {
            DataRow r = dt.Rows[0];
            string userreqemail = getusernameandemail(requestedbycombobox.Text);
            if (rejected)
            {
                if (triggerby == "supervisor")
                {
                    sendemail(userreqemail, ecrnotxtbox.Text + " ECR Rejected ", "Hello " + requestedbycombobox.Text + "," + Environment.NewLine + " Your engineering change request is rejected.", fileName, "", "");
                }
                else if (triggerby == "manager")
                {
                    string supnameemail = getUserNameEmail(Convert.ToInt32(r["SupervisorId"].ToString()));
                    string[] values = supnameemail.Replace("][", "~").Split('~');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }
                    string supervisoremail = values[0];
                    string name = values[1];

                    string[] names = name.Replace(" ", "~").Split('~');
                    for (int i = 0; i < names.Length; i++)
                    {
                        names[i] = names[i].Trim();
                    }
                    name = names[0];

                    sendemail(userreqemail, ecrnotxtbox.Text.Trim() + "ECR Rejected ", "Hello " + name + ", " + requestedbycombobox.Text + "," + Environment.NewLine + " Your engineering change request got rejected by " + departmentcomboBox.Text + ".", fileName, supervisoremail, "");
                }
            }
            else
            {
                if (triggerby == "supervisor")
                {
                    sendemail(userreqemail, ecrnotxtbox.Text + " ECR Approved ", "Hello " + requestedbycombobox.Text + "," + Environment.NewLine + " Your engineering change request is sent out for approval.", fileName, "", "");
                }
                else if (triggerby == "manager")
                {
                    string supnameemail = getUserNameEmail(Convert.ToInt32(r["SupervisorId"].ToString()));
                    string[] values = supnameemail.Replace("][", "~").Split('~');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }
                    string supervisoremail = values[0];
                    string name = values[1];

                    string[] names = name.Replace(" ", "~").Split('~');
                    for (int i = 0; i < names.Length; i++)
                    {
                        names[i] = names[i].Trim();
                    }
                    name = names[0];

                    sendemail(supervisoremail, ecrnotxtbox.Text.Trim() + " ECR Approved ", "Hello " + name + "," + Environment.NewLine + " Your engineering change request has been approved and being assigned to " + connectapi.getNameByConnectEmpId(r["AssignedTo"].ToString()) + ".", fileName, userreqemail, "");
                }
                else if (triggerby == "ecrhandler")
                {
                    string supnameemail = getUserNameEmail(Convert.ToInt32(r["SupervisorId"].ToString()));
                    string[] values = supnameemail.Replace("][", "~").Split('~');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }
                    string supervisoremail = values[0];
                    string name = values[1];

                    string[] names = name.Replace(" ", "~").Split('~');
                    for (int i = 0; i < names.Length; i++)
                    {
                        names[i] = names[i].Trim();
                    }
                    name = names[0];

                    string managernameemail = getUserNameEmail(Convert.ToInt32(r["SubmitToId"].ToString()));
                    string[] managervalues = managernameemail.Replace("][", "~").Split('~');
                    for (int i = 0; i < managervalues.Length; i++)
                    {
                        managervalues[i] = managervalues[i].Trim();
                    }
                    string manageremail = managervalues[0];

                    sendemail(userreqemail, ecrnotxtbox.Text.Trim() + " ECR Approved ", "Hello " + requestedbycombobox.Text + "," + Environment.NewLine + " Your engineering change request has been approved and being assigned to " + connectapi.getNameByConnectEmpId(r["AssignedTo"].ToString()) + ".", fileName, supervisoremail, manageremail);
                }
            }
        }

        #endregion Sending Email

        #region Attachments

        private void filllistview(string item)
        {
            try
            {
                listFiles.Clear();
                listView.Items.Clear();
                string first3char = item.Substring(0, 3) + @"\";

                string spmcadpath = @"\\spm-adfs\SDBASE\Reports\ECR_Attachments\" + ecrnotxtbox.Text + "\\";

                getitemstodisplay(spmcadpath, item);

                if (listView.Items.Count > 0)
                {
                    fileslabel.Text = "Files attached : " + listView.Items.Count;
                }
                else
                {
                    fileslabel.Text = "No files attached";
                }
            }
            catch
            {
                return;
            }
        }

        private void getitemstodisplay(string Pathpart, string ItemNo)
        {
            if (Directory.Exists(Pathpart))
            {
                foreach (string item in Directory.GetFiles(Pathpart))
                {
                    try
                    {
                        string sDocFileName = item;
                        wpfThumbnailCreator pvf;
                        pvf = new wpfThumbnailCreator();
                        System.Drawing.Size size = new Size
                        {
                            Width = 128,
                            Height = 128
                        };
                        pvf.DesiredSize = size;
                        System.Drawing.Bitmap pic = pvf.GetThumbNail(sDocFileName);
                        imageList.Images.Add(pic);
                        //axEModelViewControl1 = new EModelViewControl();
                        //axEModelViewControl1.OpenDoc(item, false, false, true, "");
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message);

                        var size = ShellEx.IconSizeEnum.ExtraLargeIcon;
                        imageList.Images.Add(ShellEx.GetBitmapFromFilePath(item, size));
                        // imageList.Images.Add(GetIcon(item));
                    }

                    // imageList.Images.Add(GetIcon(item));

                    FileInfo fi = new FileInfo(item);
                    listFiles.Add(fi.FullName);
                    listView.Items.Add(fi.Name, imageList.Images.Count - 1);
                }
            }
        }

        private List<string> listFiles = new List<string>();

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        public static Icon GetIconOldSchool(string fileName)
        {
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out ushort uicon);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        public static Icon GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                ShellEx.IconSizeEnum ExtraLargeIcon = default(ShellEx.IconSizeEnum);
                var size = (ShellEx.IconSizeEnum)ExtraLargeIcon;

                ShellEx.GetBitmapFromFilePath(fileName, size);

                return icon;
            }
            catch
            {
                try
                {
                    Icon icon2 = GetIconOldSchool(fileName);
                    return icon2;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] fList = new string[1];
            fList[0] = Pathpart;
            DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            DragDropEffects eff = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (listView.FocusedItem != null)
                        Process.Start(listFiles[listView.FocusedItem.Index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect");
                }
            }
        }

        private string Pathpart;

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                //string txt = listView.SelectedItems[0].Text;
                //string path = listView.FocusedItem.Text;
                string first3char = txt.Substring(0, 3) + @"\";
                // //MessageBox.Show(first3char);
                string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                Pathpart = (spmcadpath + first3char + txt);
                // //MessageBox.Show(Pathpart);
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    if (listView.FocusedItem != null)
                        Process.Start(listFiles[listView.FocusedItem.Index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect");
                }
            }
        }

        private void browsebttn_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                Cursor.Current = Cursors.WaitCursor;
                var failedToUploads = new List<string>();
                var uploads = new List<string>();
                string str = @"\\spm-adfs\SDBASE\Reports\ECR_Attachments\" + ecrnotxtbox.Text + "\\";
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }

                openFileDialog1.FileNames.ToList().ForEach(file =>
                {
                    if (copyFile(file, str + Path.GetFileName(file)))
                        uploads.Add(file);
                    else
                        failedToUploads.Add(file);
                });
                var message = string.Format("Files Attached: \n {0}", string.Join("\n", uploads.ToArray()));
                if (failedToUploads.Count > 0)
                {
                    message += string.Format("\nFailed to Attach: \n {0}", string.Join("\n", failedToUploads.ToArray()));
                    MessageBox.Show(message);
                }
                filllistview(ecrnotxtbox.Text);
                Cursor.Current = Cursors.Default;
            }
        }

        private bool copyFile(string file, string destfile)
        {
            bool success = false;

            try
            {
                File.Copy(file, destfile, true);
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }

        private void delbttn_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove all the files attached from this ECR removed?" + Environment.NewLine +
                     "This action cannot be reversed.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (result == DialogResult.Yes)
                {
                    string str = @"\\spm-adfs\SDBASE\Reports\ECR_Attachments\" + ecrnotxtbox.Text + "\\";
                    Array.ForEach(Directory.GetFiles(str), File.Delete);
                    filllistview(ecrnotxtbox.Text);
                }
                else
                {
                }
            }
        }

        #endregion Attachments

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log.Error(sender, t.Exception); errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(sender, (Exception)e.ExceptionObject); errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, this);
        }

        private void ECRDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed ECR Detail " + Invoice_Number + " by " + System.Environment.UserName);
            this.Dispose();
        }
    }
}