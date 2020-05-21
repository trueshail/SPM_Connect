using ExtractLargeIconFromFile;
using SearchDataSPM.ECR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SearchDataSPM
{
    public partial class ECRDetails : Form
    {
        #region Load Invoice Details and setting Parameters

        private readonly SPMConnectAPI.ECR connectapi = new SPMConnectAPI.ECR();
        private DataTable dt;
        private readonly string Invoice_Number = "";
        private bool ecrcreator;
        private log4net.ILog log;
        private readonly string userfullname = "";
        private List<string> filestoAttach = new List<string>();

        public ECRDetails(string username, string invoiceno)
        {
            InitializeComponent();
            dt = new DataTable();
            userfullname = username;
            this.Invoice_Number = invoiceno;
        }

        private void ECRDetails_Load(object sender, EventArgs e)
        {
            this.Text = "SPM Connect ECR Details - " + Invoice_Number;
            FillProjectManagers();
            FillRequestedBy();
            FillDepartments();

            if (GetECRInfo(Invoice_Number))
            {
                FillECRDetails();
                if (!supcheckBox.Checked)
                {
                    Processeditbutton();
                }
            }
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened ECR Detail " + Invoice_Number + " ");
        }

        private bool GetECRInfo(string invoicenumber)
        {
            dt.Clear();
            dt = connectapi.GetECRInfo(invoicenumber);
            return dt.Rows.Count > 0;
        }

        #endregion Load Invoice Details and setting Parameters

        #region Fill information on controls

        private void CheckEditButtonRights(int supervisorid, int managerid, int assignedto)
        {
            //if ((!ecrcreator) && (!supcheckBox.Checked))
            //{
            //    return;
            //}
            if (ecrcreator && (!managercheckBox.Checked))
            {
                supcheckBox.Enabled = true;
            }

            if (supervisorid == ConnectUser.ConnectId && ConnectUser.ECRApproval && !submitecrhandlercheckBox.Checked)
            {
                managercheckBox.Enabled = true;
                rejectbttn.Visible = !managercheckBox.Checked;
            }
            else if (managerid == ConnectUser.ConnectId && ConnectUser.ECRApproval2 && !ecrhandlercheckBox.Checked)
            {
                submitecrhandlercheckBox.Enabled = true;
                rejectbttn.Visible = !submitecrhandlercheckBox.Checked;
                attachlbl.Visible = false;
                browsebttn.Visible = false;
                delbttn.Visible = false;
                iteminfogroupBox.Enabled = false;
                descriptiontxtbox.Enabled = false;
            }
            else if (assignedto == ConnectUser.ConnectId && ConnectUser.ECRHandler)
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
                if (!ecrcreator)
                    PerfromEditlockdown();
                if (ecrcreator && submitecrhandlercheckBox.Checked)
                    PerfromEditlockdown();
            }
        }

        private void FetchJobSaNames(string jobno, string sano)
        {
            jobnamelbl.Text = jobno.Length == 5 ? connectapi.GetJobName(jobno) : "Job Name :";

            subassylbl.Text = sano.Length == 6 ? connectapi.GetSAName(sano) : "Sub Assy Name :";
        }

        private void FillECRDetails()
        {
            DataRow r = dt.Rows[0];

            ecrnotxtbox.Text = r["ECRNo"].ToString();
            descriptiontxtbox.Text = r["Description"].ToString();
            notestxt.Text = r["Comments"].ToString();
            jobtxt.Text = r["JobNo"].ToString();
            satxt.Text = r["SANo"].ToString();
            partnotxt.Text = r["PartNo"].ToString();

            FetchJobSaNames(r["JobNo"].ToString(), r["SANo"].ToString());

            Createdon.Text = "Created On : " + r["DateCreated"].ToString();

            CreatedBy.Text = "Created By : " + r["CreatedBy"].ToString();

            lastsavedby.Text = "Last Saved By : " + r["LastSavedBy"].ToString();

            lastsavedon.Text = "Last Saved On : " + r["DateLastSaved"].ToString();

            statuslbl.Text = "Status : " + r["Status"].ToString();

            string submittedtosup = r["Submitted"].ToString();
            string submittedtomanager = r["SupApproval"].ToString();
            string submittedtoecrhandler = r["Approved"].ToString();
            string ecrcomplete = r["Completed"].ToString();

            HandleCheckBoxes(submittedtosup, submittedtomanager, submittedtoecrhandler, ecrcomplete, r["SupervisorId"].ToString(),
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

            CheckEditButtonRights(Convert.ToInt32(r["SupervisorId"].ToString()), Convert.ToInt32(r["SubmitToId"].ToString()), Convert.ToInt32(r["AssignedTo"].ToString()));
            Filllistview(ecrnotxtbox.Text);
        }

        private void HandleCheckBoxes(string submittedtosup, string submittedtomanager, string submittedtoecrhandler, string ecrcomplete,
            string supervisorid, string managerid, string assignedto, string completedby, string submittedon, string SupApprovedOn, string approvedon, string completedon)
        {
            if (submittedtosup == "1")
            {
                supcheckBox.Checked = true;
                supcheckBox.Text = "Submitted to " + connectapi.GetNameByConnectEmpId(supervisorid) + " on " + submittedon + "";
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
                managercheckBox.Text = "Submitted to " + connectapi.GetNameByConnectEmpId(managerid) + " on " + SupApprovedOn + "";
            }
            else if (submittedtomanager == "3")
            {
                managercheckBox.Checked = true;
                managercheckBox.Text = "Rejected by " + connectapi.GetNameByConnectEmpId(supervisorid) + " on " + SupApprovedOn + "";
            }
            else
            {
                managercheckBox.Checked = false;
            }

            if (submittedtoecrhandler == "1")
            {
                submitecrhandlercheckBox.Checked = true;
                submitecrhandlercheckBox.Text = "Assigned to " + connectapi.GetNameByConnectEmpId(assignedto) + " on " + approvedon + "";
            }
            else if (submittedtoecrhandler == "3")
            {
                submitecrhandlercheckBox.Checked = true;
                submitecrhandlercheckBox.Text = "Rejected by " + connectapi.GetNameByConnectEmpId(managerid) + " on " + approvedon + "";
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

        private void PerfromEditlockdown()
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
                Perfromsavebttn("", true, false);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion shortcuts

        #region FormClosing

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to close without saving changes?", "SPM Connect - Save Invoice Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connectapi.CheckoutInvoice(ecrnotxtbox.Text.Trim(), CheckInModules.ECR);
                    this.Dispose();
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
            else
            {
                connectapi.CheckoutInvoice(ecrnotxtbox.Text.Trim(), CheckInModules.ECR);
            }
        }

        #endregion FormClosing

        #region Process Save

        private readonly List<string> list = new List<string>();

        private void Graballinfor()
        {
            list.Clear();
            Regex reg = new Regex("['\",_^]");
            list.Add(reg.Replace(ecrnotxtbox.Text, "''"));
            list.Add(reg.Replace(jobtxt.Text, "''"));
            list.Add(reg.Replace(satxt.Text, "''"));
            list.Add(reg.Replace(partnotxt.Text, "''"));
            if (jobnamelbl.Text.Length > 0 && jobnamelbl.Text.Substring(0, 3) == "Job")
            {
                list.Add(reg.Replace("", "''"));
            }
            else
            {
                list.Add(reg.Replace(jobnamelbl.Text, "''"));
            }
            if (subassylbl.Text.Length > 0 && subassylbl.Text.Substring(0, 3) == "Sub")
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

        private void Perfromlockdown()
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

        private void Perfromsavebttn(string typeofSave, bool buttonclick, bool rejectbutton)
        {
            errorProvider1.Clear();
            if (IsFreeFromErrors())
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                this.BackColor = Color.SteelBlue;
                Perfromlockdown();
                Graballinfor();
                if (ProcessSaveType(typeofSave, buttonclick, rejectbutton))
                {
                    if (GetECRInfo(list[0]))
                    {
                        FillECRDetails();
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Error occured while saving data.", "SPM Connect?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (GetECRInfo(list[0]))
                    {
                        FillECRDetails();
                    }
                }
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ProcessSaveType(string typeofSave, bool savebttn, bool rejectbttn)
        {
            bool success = false;
            if (savebttn)
            {
                if (ecrcreator)
                {
                    success = connectapi.UpdateECRDetsToSql("Creator", list[0], list[1],
                     list[2], list[3], list[4], list[5],
                     list[6], list[7], list[8], list[9],
                     list[10], 0, 0, 0, 0, "", "", rejectbttn);
                }
                else if (ConnectUser.ECRApproval)
                {
                    success = connectapi.UpdateECRDetsToSql("Supervisor", list[0], list[1],
                    list[2], list[3], list[4], list[5],
                    list[6], list[7], list[8], list[9],
                    list[10], 0, 0, 0, 0, "", "", rejectbttn);
                }
                else if (ConnectUser.ECRApproval2)
                {
                    success = connectapi.UpdateECRDetsToSql("Manager", list[0], list[1],
                    list[2], list[3], list[4], list[5],
                    list[6], list[7], list[8], list[9],
                    list[10], 0, 0, 0, 0, "", "", rejectbttn);
                }
                else if (ConnectUser.ECRHandler)
                {
                    success = connectapi.UpdateECRDetsToSql("Handler", list[0], list[1],
                    list[2], list[3], list[4], list[5],
                    list[6], list[7], list[8], list[9],
                    list[10], 0, 0, 0, 0, "", "", rejectbttn);
                }
            }
            else
            {
                if (typeofSave == "Submitted")
                {
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                       list[2], list[3], list[4], list[5],
                       list[6], list[7], list[8], list[9],
                       list[10], 1, 0, 0, 0, "", "", rejectbttn);
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

                        if (ecrUser.ShowDialog() == DialogResult.OK)
                        {
                            managerid = ecrUser.ValueIWant;
                        }

                        if (managerid.Length > 0)
                        {
                            success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                            list[2], list[3], list[4], list[5],
                            list[6], list[7], list[8], list[9],
                            list[10], 0, 1, 0, 0, managerid, "", rejectbttn);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                       list[2], list[3], list[4], list[5],
                       list[6], list[7], list[8], list[9],
                       list[10], 0, 1, 0, 0, managerid, "", rejectbttn);
                    }
                }
                else if (typeofSave == "SupSubmitFalse")
                {
                    // Get the option to select the available managers

                    const string managerid = "";
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                    list[2], list[3], list[4], list[5],
                    list[6], list[7], list[8], list[9],
                    list[10], 0, rejectbttn ? 3 : 0, 0, 0, managerid, "", rejectbttn);
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

                        if (ecrUser.ShowDialog() == DialogResult.OK)
                        {
                            ecrhandler = ecrUser.ValueIWant;
                        }
                        if (ecrhandler.Length > 0)
                        {
                            success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                            list[2], list[3], list[4], list[5],
                            list[6], list[7], list[8], list[9],
                            list[10], 0, 0, 1, 0, "", ecrhandler, rejectbttn);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                        list[2], list[3], list[4], list[5],
                        list[6], list[7], list[8], list[9],
                        list[10], 0, 0, 1, 0, "", ecrhandler, rejectbttn);
                    }
                }
                else if (typeofSave == "ManagerApprovedFalse")
                {
                    // Get the option to select the available ecr handlers
                    const string ecrhandler = "";
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                    list[2], list[3], list[4], list[5],
                    list[6], list[7], list[8], list[9],
                    list[10], 0, 0, rejectbttn ? 3 : 0, 0, "", ecrhandler, rejectbttn);
                }
                else if (typeofSave == "Completed")
                {
                    success = connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                    list[2], list[3], list[4], list[5],
                    list[6], list[7], list[8], list[9],
                    list[10], 0, 0, 0, 1, "", "", rejectbttn);
                }
                else
                {
                    success = typeofSave == "CompletedFalse"
                        ? connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                                        list[2], list[3], list[4], list[5],
                                        list[6], list[7], list[8], list[9],
                                        list[10], 0, 0, 0, 0, "", "", rejectbttn)
                        : connectapi.UpdateECRDetsToSql(typeofSave, list[0], list[1],
                                        list[2], list[3], list[4], list[5],
                                        list[6], list[7], list[8], list[9],
                                        list[10], 0, 0, 0, 0, "", "", rejectbttn);
                }
                SaveReport(ecrnotxtbox.Text);
            }
            return success;
        }

        private void Savbttn_Click(object sender, EventArgs e)
        {
            Perfromsavebttn("", true, false);
        }

        #endregion Process Save

        #region Process Edit

        private void Editbttn_Click(object sender, EventArgs e)
        {
            Processeditbutton();
        }

        private void Processeditbutton()
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
            CheckEditButtonRights(Convert.ToInt32(r["SupervisorId"].ToString()), Convert.ToInt32(r["SubmitToId"].ToString()), Convert.ToInt32(r["AssignedTo"].ToString()));
        }

        #endregion Process Edit

        #region Print Reports

        private void ToolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer("ECR", ecrnotxtbox.Text);
            form1.Show();
        }

        #endregion Print Reports

        #region Save Report

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

            const string historyID = null;
            const string deviceInfo = null;
            const string format = "PDF";
            Byte[] results;
            const string _reportName = "/GeniusReports/Job/SPM_ECR";

            const string _historyID = null;
            const bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            try
            {
                RS2005.ReportParameter[] _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
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
                          out string extension, out string encoding,
                          out string mimeType, out RE2005.Warning[] warnings, out string[] streamIDs);

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
                log.Error(ex.Message, ex);
            }
        }

        private void SaveReport(string reqno)
        {
            const string filepath = @"\\spm-adfs\SDBASE\Reports\ECR_Reports\";
            Directory.CreateDirectory(filepath);
            string fileName = filepath + reqno + ".pdf";
            SaveReport(reqno, fileName);
        }

        #endregion Save Report

        private void DepartmentcomboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                departmentcomboBox.Focus();
            }
        }

        private void ECRDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed ECR Detail " + Invoice_Number + " ");
            this.Dispose();
        }

        private void EcrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (!ecrhandlercheckBox.Checked)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to mark this ECR not completed?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ecrhandlercheckBox.Checked = false;
                    ecrhandlercheckBox.Text = "Close ECR Request";
                    Perfromsavebttn("CompletedFalse", false, false);
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
                    if (IsFreeFromErrors())
                    {
                        ecrhandlercheckBox.Text = "Completed";
                        Perfromsavebttn("Completed", false, false);
                        Preparetosendemail("Completed", false);
                    }
                    else
                    {
                        ecrhandlercheckBox.Checked = false;
                    }
                }
                else
                {
                    ecrhandlercheckBox.Checked = false;
                }
            }
        }

        private void Jobtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Jobtxt_TextChanged(object sender, EventArgs e)
        {
            if (jobtxt.Text.Length == 5)
            {
                FetchJobSaNames(jobtxt.Text.Trim(), satxt.Text.Trim());
            }
            else
            {
                jobnamelbl.Text = "Job Name :";
            }
        }

        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                var failedToUploads = new List<string>();
                var uploads = new List<string>();
                string str = @"\\spm-adfs\SDBASE\Reports\ECR_Attachments\" + ecrnotxtbox.Text + "\\";
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }

                foreach (string file in files)
                {
                    if (CopyFile(file, str + Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(file)))
                        uploads.Add(file);
                    else
                        failedToUploads.Add(file);
                }
                var message = string.Format("Files Attached: \n {0}", string.Join("\n", uploads.ToArray()));
                if (failedToUploads.Count > 0)
                {
                    message += string.Format("\nFailed to Attach: \n {0}", string.Join("\n", failedToUploads.ToArray()));
                    MessageBox.Show(message);
                }
                Filllistview(ecrnotxtbox.Text);
                Cursor.Current = Cursors.Default;
            }
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void ManagercheckBox_Click(object sender, EventArgs e)
        {
            if (!managercheckBox.Checked)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    managercheckBox.Checked = false;
                    managercheckBox.Text = "Submit to ECR Manager";
                    Perfromsavebttn("SupSubmitFalse", false, false);
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
                    if (IsFreeFromErrors())
                    {
                        managercheckBox.Text = "Submitted to ECR Manager";
                        Perfromsavebttn("SupSubmit", false, false);
                        Preparetosendemail("SupSubmit", false);
                    }
                    else
                    {
                        managercheckBox.Checked = false;
                    }
                }
                else
                {
                    managercheckBox.Checked = false;
                }
            }
        }

        private bool IsFreeFromErrors()
        {
            bool errorfree = true;
            errorProvider1.Clear();
            if (satxt.Text.Length == 0)
            {
                errorProvider1.SetError(satxt, "Sub-Assy number cannot be empty");
                errorfree = false;
            }

            if (!(satxt.Text.Length > 0 && satxt.Text.Length == 6 && !String.IsNullOrEmpty(satxt.Text) && Char.IsLetter(satxt.Text[0]) && satxt.Text.Substring(1).All(char.IsNumber)))
            {
                errorfree = false;
                errorProvider1.SetError(satxt, "Not a valid part number. Please enter a valid six digit SPM item number (starting with 'A', 'B', 'C') as valid sub-assy number.");
            }
            if (jobtxt.Text.Length == 0)
            {
                errorfree = false;
                errorProvider1.SetError(jobtxt, "Job Number cannot be empty");
            }
            if (jobtxt.Text.Length != 5)
            {
                errorfree = false;
                errorProvider1.SetError(jobtxt, "Enter a valid job number");
            }
            if (descriptiontxtbox.Text.Length == 0)
            {
                errorfree = false;
                errorProvider1.SetError(descriptiontxtbox, "Description cannot be empty");
            }
            if (errorfree)
            {
                errorProvider1.Clear();
            }
            return errorfree;
        }

        private void Projectmanagercombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                projectmanagercombobox.Focus();
            }
        }

        private void Rejectbttn_Click(object sender, EventArgs e)
        {
            if (ConnectUser.ECRApproval)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to reject this ECR?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to associated people with this ECR.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Perfromsavebttn("SupSubmitFalse", false, true);
                    Preparetosendemail("SupSubmitFalse", true);
                }
            }
            else if (ConnectUser.ECRApproval2)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to reject this ECR?" + Environment.NewLine +
                    " " + Environment.NewLine +
                    "This will send email to associated people with this ECR.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Perfromsavebttn("ManagerApprovedFalse", false, true);
                    Preparetosendemail("ManagerApprovedFalse", true);
                }
            }
        }

        private void Requestedbycombobox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                requestedbycombobox.Focus();
            }
        }

        private void Satxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (sender as TextBox)?.SelectionStart == 0 && e.KeyChar == (char)Keys.Space;
        }

        private void Satxt_TextChanged(object sender, EventArgs e)
        {
            if (satxt.Text.Length == 6 && Char.IsLetter(satxt.Text[0]))
            {
                FetchJobSaNames(jobtxt.Text.Trim(), satxt.Text.Trim());
            }
            else
            {
                subassylbl.Text = "Sub Assy Name :";
            }
        }

        private void SubmitecrhandlercheckBox_Click(object sender, EventArgs e)
        {
            if (!submitecrhandlercheckBox.Checked)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from process?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    submitecrhandlercheckBox.Checked = false;
                    submitecrhandlercheckBox.Text = "Submit to ECR Handler";
                    Perfromsavebttn("ManagerApprovedFalse", false, false);
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
                    if (IsFreeFromErrors())
                    {
                        submitecrhandlercheckBox.Text = "Submitted to ECR Handler";
                        Perfromsavebttn("ManagerApproved", false, false);
                        Preparetosendemail("ManagerApproved", false);
                    }
                    else
                    {
                        submitecrhandlercheckBox.Checked = false;
                    }
                }
                else
                {
                    submitecrhandlercheckBox.Checked = false;
                }
            }
        }

        private void SupcheckBox_Click(object sender, EventArgs e)
        {
            if (!supcheckBox.Checked)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove this ECR from approval?", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    supcheckBox.Text = "Submit to Supervisor";
                    supcheckBox.Checked = false;
                    Perfromsavebttn("SubmittedFalse", false, false);
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
                    if (IsFreeFromErrors())
                    {
                        supcheckBox.Text = "Submitted to Supervisor";
                        Perfromsavebttn("Submitted", false, false);
                        Preparetosendemail("Submitted", false);
                    }
                    else
                    {
                        supcheckBox.Checked = false;
                    }
                }
                else
                {
                    supcheckBox.Checked = false;
                }
            }
        }

        #region Sending Email

        private void Preparetosendemail(string typeofSave, bool rejectbttn)
        {
            const string filepath = @"\\spm-adfs\SDBASE\Reports\ECR_Reports\";
            string fileName = filepath + ecrnotxtbox.Text.Trim() + ".pdf";
            if (typeofSave == "Submitted")
            {
                Sendemailtosupervisor(fileName);
            }
            else if (typeofSave == "SupSubmit")
            {
                if (rejectbttn)
                {
                    // send email to user notifying rejected ecr
                    Sendemailtouser(fileName, "supervisor", rejectbttn);
                }
                else
                {
                    //send email to manager and cc requestedby
                    Sendemailtouser(fileName, "supervisor", rejectbttn);
                    SendemailtoManager(fileName);
                }
            }
            else if (typeofSave == "SupSubmitFalse")
            {
                // ecr out of system
                Sendemailtouser(fileName, "supervisor", rejectbttn);
            }
            else if (typeofSave == "ManagerApproved")
            {
                if (rejectbttn)
                {
                    //send email to user and cc supervisor regarding the ecr is rejected
                    Sendemailtouser(fileName, "manager", rejectbttn);
                }
                else
                {
                    // send ecr to ecr handler and cc user and supervisor
                    Sendemailtouser(fileName, "manager", rejectbttn);
                    SendemailtoHandler(fileName);
                }
            }
            else if (typeofSave == "ManagerApprovedFalse")
            {
                Sendemailtouser(fileName, "manager", rejectbttn);
            }
            else if (typeofSave == "Completed")
            {
                // send email to user and cc manager and supervisor
                Sendemailtouser(fileName, "ecrhandler", rejectbttn);
            }
            else if (typeofSave == "CompletedFalse")
            {
                //
            }
        }

        private void Sendemail(string emailtosend, string subject, string name, string body, string filetoattach, string cc, string extracc)
        {
            if (Sendemailyesno())
            {
                connectapi.TriggerEmail(emailtosend, subject, name, body, filetoattach, cc, extracc, "Normal", filestoAttach);
            }
            else
            {
                MessageBox.Show("Email turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SendemailtoHandler(string fileName)
        {
            DataRow r = dt.Rows[0];
            NameEmail supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, r["AssignedTo"].ToString())[0];
            Sendemail(supnameemail.email, ecrnotxtbox.Text.Trim() + " ECR Completion Required", supnameemail.name, Environment.NewLine + r["ApprovedBy"].ToString() + " sent this engineering change request for completion and changes to be made.", fileName, "", "");
        }

        private void SendemailtoManager(string fileName)
        {
            DataRow r = dt.Rows[0];
            NameEmail supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, r["SubmitToId"].ToString())[0];
            Sendemail(supnameemail.email, ecrnotxtbox.Text.Trim() + " ECR Approval Required", supnameemail.name, Environment.NewLine + r["SupApprovalBy"].ToString() + " sent this engineering change request for approval.", fileName, "", "");
        }

        private void Sendemailtosupervisor(string fileName)
        {
            NameEmail supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, ConnectUser.ECRSup.ToString())[0];
            Sendemail(supnameemail.email, ecrnotxtbox.Text + " ECR Approval Required", supnameemail.name, Environment.NewLine + userfullname + " sent this engineering change request for approval.", fileName, "", "");
        }

        private void Sendemailtouser(string fileName, string triggerby, bool rejected)
        {
            DataRow r = dt.Rows[0];
            string userreqemail = connectapi.GetNameEmailByParaValue(UserFields.Name, requestedbycombobox.Text)[0].email;
            if (rejected)
            {
                if (triggerby == "supervisor")
                {
                    Sendemail(userreqemail, ecrnotxtbox.Text + " ECR Rejected ", requestedbycombobox.Text, Environment.NewLine + " Your engineering change request is rejected.", fileName, "", "");
                }
                else if (triggerby == "manager")
                {
                    NameEmail supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, r["SupervisorId"].ToString())[0];

                    Sendemail(userreqemail, ecrnotxtbox.Text.Trim() + "ECR Rejected ", supnameemail.name, requestedbycombobox.Text + "," + Environment.NewLine + " Your engineering change request got rejected by " + departmentcomboBox.Text + ".", fileName, supnameemail.email, "");
                }
            }
            else
            {
                if (triggerby == "supervisor")
                {
                    Sendemail(userreqemail, ecrnotxtbox.Text + " ECR Approved ", requestedbycombobox.Text, Environment.NewLine + " Your engineering change request is sent out for approval.", fileName, "", "");
                }
                else if (triggerby == "manager")
                {
                    NameEmail supnameemail = connectapi.GetNameEmailByParaValue(UserFields.id, r["SupervisorId"].ToString())[0];
                    Sendemail(supnameemail.email, ecrnotxtbox.Text.Trim() + " ECR Approved ", supnameemail.name, Environment.NewLine + " Your engineering change request has been approved and being assigned to " + connectapi.GetNameByConnectEmpId(r["AssignedTo"].ToString()) + ".", fileName, userreqemail, "");
                }
                else if (triggerby == "ecrhandler")
                {
                    string supervisoremail = connectapi.GetNameEmailByParaValue(UserFields.id, r["SupervisorId"].ToString())[0].email;
                    string manageremail = connectapi.GetNameEmailByParaValue(UserFields.id, r["SubmitToId"].ToString())[0].email;

                    Sendemail(userreqemail, ecrnotxtbox.Text.Trim() + " ECR Approved ", requestedbycombobox.Text, Environment.NewLine + " Your engineering change request has been approved and being assigned to " + connectapi.GetNameByConnectEmpId(r["AssignedTo"].ToString()) + ".", fileName, supervisoremail, manageremail);
                }
            }
        }

        private bool Sendemailyesno()
        {
            return connectapi.GetConnectParameterValue("EmailECR") == "1";
        }

        #endregion Sending Email

        #region Attachments

        private readonly List<string> listFiles = new List<string>();

        private string Pathpart;

        public static Icon GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                const ShellEx.IconSizeEnum ExtraLargeIcon = default;
                const ShellEx.IconSizeEnum size = (ShellEx.IconSizeEnum)ExtraLargeIcon;

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

        public static Icon GetIconOldSchool(string fileName)
        {
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out _);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractAssociatedIcon(IntPtr hInst,
        StringBuilder lpIconPath, out ushort lpiIcon);

        private void Browsebttn_Click(object sender, EventArgs e)
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
                    if (CopyFile(file, str + Path.GetFileName(file)))
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
                Filllistview(ecrnotxtbox.Text);
                Cursor.Current = Cursors.Default;
            }
        }

        private bool CopyFile(string file, string destfile)
        {
            bool success;
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

        private void Delbttn_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to remove all the files attached from this ECR removed?" + Environment.NewLine +
                     "This action cannot be reversed.", "SPM Connect?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (result == DialogResult.Yes)
                {
                    string str = @"\\spm-adfs\SDBASE\Reports\ECR_Attachments\" + ecrnotxtbox.Text + "\\";
                    Array.ForEach(Directory.GetFiles(str), File.Delete);
                    Filllistview(ecrnotxtbox.Text);
                }
            }
        }

        private void Filllistview(string item)
        {
            try
            {
                listFiles.Clear();
                listView.Items.Clear();
                string first3char = item.Substring(0, 3) + @"\";

                string spmcadpath = @"\\spm-adfs\SDBASE\Reports\ECR_Attachments\" + ecrnotxtbox.Text + "\\";

                Getitemstodisplay(spmcadpath);

                fileslabel.Text = listView.Items.Count > 0 ? "Files attached : " + listView.Items.Count : "No files attached";
            }
            catch
            {
                return;
            }
        }

        private void Getitemstodisplay(string Pathpart)
        {
            if (Directory.Exists(Pathpart))
            {
                filestoAttach.Clear();
                foreach (string item in Directory.GetFiles(Pathpart))
                {
                    try
                    {
                        string sDocFileName = item;
                        const ShellEx.IconSizeEnum size = ShellEx.IconSizeEnum.ExtraLargeIcon;
                        imageList.Images.Add(ShellEx.GetBitmapFromFilePath(item, size));
                        filestoAttach.Add(item);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);
                    }

                    // imageList.Images.Add(GetIcon(item));
                    FileInfo fi = new FileInfo(item);
                    listFiles.Add(fi.FullName);
                    listView.Items.Add(fi.Name, imageList.Images.Count - 1);
                }
            }
        }

        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] fList = new string[1];
            fList[0] = Pathpart;
            DataObject dataObj = new DataObject(DataFormats.FileDrop, fList);
            _ = DoDragDrop(dataObj, DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                string txt = listView.FocusedItem.Text;
                string first3char = txt.Substring(0, 3) + @"\";
                const string spmcadpath = @"\\spm-adfs\CAD Data\AAACAD\";
                Pathpart = (spmcadpath + first3char + txt);
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
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

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
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

        #endregion Attachments
    }
}