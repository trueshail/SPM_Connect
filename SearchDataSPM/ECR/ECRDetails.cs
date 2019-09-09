using SearchDataSPM.ECR;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ECRDetails : Form
    {
        #region Load Invoice Details and setting Parameters

        string connection;
        DataTable dt = new DataTable();
        SqlConnection cn;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        string Invoice_Number = "";
        SPMConnectAPI.ECR connectapi = new SPMConnectAPI.ECR();

        bool ecrcreator = false;
        bool ecrsup = false;
        bool ecrmanager = false;
        bool ecrhandler = false;
        int myid = 0;
        int supervisorid = 0;
        string userfullname = "";

        public ECRDetails()
        {
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
            //connectapi.SPM_Connect();

            dt = new DataTable();
            _command = new SqlCommand();
        }

        public string invoicenumber(string number)
        {
            if (number.Length > 0)
                return Invoice_Number = number;
            return null;
        }

        public string username(string name)
        {
            if (name.Length > 0)
                return userfullname = name;
            return null;
        }


        private void ECRDetails_Load(object sender, EventArgs e)
        {
            this.Text = "SPM Connect ECR Details - " + Invoice_Number;
            getUserCreds();

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

        }

        private string get_username()
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

        private void getUserCreds()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + get_username().ToString() + "' ";
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

        #endregion

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

                iteminfogroupBox.Enabled = false;
                descriptiontxtbox.ReadOnly = true;
            }
            else if (assignedto == myid && ecrhandler)
            {
                ecrhandlercheckBox.Enabled = true;
                iteminfogroupBox.Enabled = false;
                descriptiontxtbox.ReadOnly = true;
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

        #endregion

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

        #endregion

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


        #endregion

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

        #endregion

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
        }

        List<string> list = new List<string>();

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

        void perfromsavebttn(string typeofSave, bool buttonclick, bool rejectbutton)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            this.BackColor = Color.SkyBlue;
            perfromlockdown();
            graballinfor();
            if (processSaveType(typeofSave, buttonclick, rejectbutton))
            {
                if (GetECRInfo(list[0].ToString()))
                {
                    FillECRDetails();
                    //SaveReport(ecrnotxtbox.Text);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Error occured while saving data.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (GetECRInfo(list[0].ToString()))
                {
                    FillECRDetails();
                    //SaveReport(ecrnotxtbox.Text);
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

            }
            return success;
        }

        #endregion

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

            DataRow r = dt.Rows[0];
            checkEditButtonRights(Convert.ToInt32(r["SupervisorId"].ToString()), Convert.ToInt32(r["SubmitToId"].ToString()), Convert.ToInt32(r["AssignedTo"].ToString()));
        }

        #endregion

        #region Print Reports

        private void print1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(ecrnotxtbox.Text);
            form1.getreport("ShippingInvPack");
            form1.Show();
        }

        private void print2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(ecrnotxtbox.Text);
            form1.getreport("ShippingInvCom");
            form1.Show();
        }

        #endregion

        #region Save Report

        private void SaveReport(string reqno)
        {
            string fileName = "";
            string filepath = connectapi.getsharesfolder() + @"\SPM_Connect\ShippingInvoices\";
            System.IO.Directory.CreateDirectory(filepath);
            fileName = filepath + reqno + " - CI.pdf";
            filepath += reqno + " - PL.pdf";
            SaveReport(reqno, fileName);
            SaveReport(reqno, filepath);

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
            string _reportName = "";
            if (fileName.Substring(fileName.Length - 6) == "CI.pdf")
            {
                _reportName = @"/GeniusReports/PurchaseOrder/SPM_ShippingInvoice";
            }
            else
            {
                _reportName = @"/GeniusReports/PurchaseOrder/SPM_ShippingInvoicePacking";
            }
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
                    parameters[0] = new RE2005.ParameterValue();
                    //parameters[0].Label = "";
                    parameters[0].Name = "pInvno";
                    parameters[0].Value = invoiceno;
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
                    MessageBox.Show(e.Message, "SPM Connect - Save Report", MessageBoxButtons.OK);
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

        #endregion


        private void supcheckBox_Click(object sender, EventArgs e)
        {
            if (supcheckBox.Checked == false)
            {

                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    supcheckBox.Text = "Submit to Supervisor";
                    supcheckBox.Checked = false;
                    perfromsavebttn("SubmittedFalse", false, false);
                    // SaveReport(reqno, filename);
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
                        // SaveReport(reqno, filename);
                        //preparetosendemail(reqno, true, "", filename, false, "user", false);

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

        private void managercheckBox_Click(object sender, EventArgs e)
        {
            if (managercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    managercheckBox.Checked = false;
                    managercheckBox.Text = "Submit to ECR Manager";
                    perfromsavebttn("SupSubmitFalse", false, false);
                    // SaveReport(reqno, filename);
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
                        // SaveReport(reqno, filename);
                        //preparetosendemail(reqno, true, "", filename, false, "user", false);

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

        private void submitecrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (submitecrhandlercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from process?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    submitecrhandlercheckBox.Checked = false;
                    submitecrhandlercheckBox.Text = "Submit to ECR Handler";
                    perfromsavebttn("ManagerApprovedFalse", false, false);
                    // SaveReport(reqno, filename);
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
                        // SaveReport(reqno, filename);
                        //preparetosendemail(reqno, true, "", filename, false, "user", false);

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

        private void ecrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (ecrhandlercheckBox.Checked == false)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this ECR not completed?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ecrhandlercheckBox.Checked = false;
                    ecrhandlercheckBox.Text = "Close ECR Request";
                    perfromsavebttn("CompletedFalse", false, false);
                    // SaveReport(reqno, filename);
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
                        // SaveReport(reqno, filename);
                        //preparetosendemail(reqno, true, "", filename, false, "user", false);

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
                    // SaveReport(reqno, filename);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);

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
                    // SaveReport(reqno, filename);
                    //preparetosendemail(reqno, true, "", filename, false, "user", false);

                }
            }
        }
    }
}