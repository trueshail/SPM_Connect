using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class MatReAlloc : Form
    {
        #region Load Invoice Details and setting Parameters

        DataTable dt = new DataTable();
        string Invoice_Number = "";
        WorkOrder connectapi = new WorkOrder();

        public MatReAlloc()
        {
            InitializeComponent();

            connectapi.SPM_Connect();
            dt = new DataTable();
        }

        public string invoicenumber(string number)
        {
            if (number.Length > 0)
                return Invoice_Number = number;
            return null;
        }

        private void QuoteDetails_Load(object sender, EventArgs e)
        {
            if (GetMatReInfo(Invoice_Number))
            {
                FillInfo();
                processeditbutton();
            }

        }

        private bool GetMatReInfo(string id)
        {
            bool fillled = false;
            try
            {
                dt.Clear();
                dt = connectapi.ShowMatInvoice(Invoice_Number);
                fillled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect  - Get Material Reallocation Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fillled;
        }

        #endregion

        #region Fill information on controls

        private void FillInfo()
        {
            if (dt.Rows.Count > 0)
            {
                try
                {
                    DataRow r = dt.Rows[0];

                    invoicetxtbox.Text = r["InvoiceNo"].ToString();

                    notestxt.Text = r["Notes"].ToString();

                    Createdon.Text = "Created On : " + r["DateCreated"].ToString();

                    CreatedBy.Text = "Created By : " + r["CreatedBy"].ToString();

                    LastSavedBy.Text = "Last Saved By : " + r["LastSavedBy"].ToString();

                    LastSavedOn.Text = "Last Saved On : " + r["LastSavedOn"].ToString();

                    ItemTxtBox.Text = r["ItemId"].ToString();
                    Descriptiontxtbox.Text = r["Description"].ToString();
                    oemtxt.Text = r["OEM"].ToString();
                    oemitemnotxt.Text = r["OEMItem"].ToString();

                    empidtxt.Text = r["EmployeeId"].ToString();
                    empname.Text = r["EmployeeName"].ToString();
                    appidtxt.Text = r["ApprovedId"].ToString();
                    appnametxt.Text = r["ApprovedName"].ToString();

                    jobreqtxt.Text = r["JobReq"].ToString();
                    woreqtxt.Text = r["WOReq"].ToString();
                    jobtakenfrom.Text = r["JobTaken"].ToString();
                    wotakentxt.Text = r["WOTaken"].ToString();
                    qtytxt.Text = r["Qty"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect  - Fill Mat Inv Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("System has encountered an error. Please contact the admin", "SPM Connect Error - Fill Mat Inv Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            

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
                perfromsavebttn();
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
                    this.Dispose();
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
            else
            {

            }
        }

        #endregion

        #region Process Save

        private void perfromlockdown()
        {
            editbttn.Visible = true;
            savbttn.Enabled = false;
            savbttn.Visible = false;
            ItemsGrpBox.Enabled = false;
            wogroupbox.Enabled = false;
            empgrpbox.Enabled = false;
            notesgroupbox.Enabled = false;
        }

        List<string> list = new List<string>();

        private void graballinfor()
        {
            list.Clear();
            Regex reg = new Regex("[*'\",_&#^@]");
            // invoice notes itemid description oem oemitem empid empname appid appname jobreq woreq jobtaken wotaken qty

            list.Add(invoicetxtbox.Text);

            list.Add(reg.Replace(notestxt.Text.Trim(), "''"));

            list.Add(reg.Replace(ItemTxtBox.Text, "''"));
            list.Add(reg.Replace(Descriptiontxtbox.Text, "''"));
            list.Add(reg.Replace(oemtxt.Text, "''"));
            list.Add(reg.Replace(oemitemnotxt.Text, "''"));

            list.Add(reg.Replace(empidtxt.Text, "''"));
            list.Add(reg.Replace(empname.Text, "''"));
            list.Add(reg.Replace(appidtxt.Text, "''"));
            list.Add(reg.Replace(appnametxt.Text, "''"));

            list.Add(reg.Replace(jobreqtxt.Text, "''"));
            list.Add(reg.Replace(woreqtxt.Text, "''"));
            list.Add(reg.Replace(jobtakenfrom.Text, "''"));
            list.Add(reg.Replace(wotakentxt.Text, "''"));
            list.Add(reg.Replace(qtytxt.Text, "''"));

        }

        private void savbttn_Click(object sender, EventArgs e)
        {
            perfromsavebttn();
        }

        void perfromsavebttn()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            perfromlockdown();
            graballinfor();
            if (connectapi.UpdateInvoiceDetsToSql(list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(), list[10].ToString(), list[11].ToString(), list[12].ToString(), list[13].ToString(), list[14].ToString()))
            {
                if (GetMatReInfo(list[0].ToString()))
                {
                    FillInfo();
                    //SaveReport(invoicetxtbox.Text);
                }
            }
            PrintToolStrip.Enabled = true;
            this.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        #endregion

        #region Process Edit

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        private void processeditbutton()
        {
            editbttn.Visible = false;
            savbttn.Enabled = true;
            savbttn.Visible = true;
            notestxt.ReadOnly = false;
            ItemsGrpBox.Enabled = true;
            wogroupbox.Enabled = true;
            empgrpbox.Enabled = true;
            notesgroupbox.Enabled = true;
        }

        #endregion

        #region Print Reports

        private void print1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(invoicetxtbox.Text);
            form1.getreport("ShippingInvPack");
            form1.Show();
        }

        private void print2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer();
            form1.item(invoicetxtbox.Text);
            form1.getreport("ShippingInvCom");
            form1.Show();
        }

        #endregion

        #region Save Report

        private void SaveReport(string Invoiceno)
        {
            string filepath = connectapi.getsharesfolder() + @"\SPM_Connect\MaterialReallocations\";
            System.IO.Directory.CreateDirectory(filepath);
            filepath += Invoiceno + ".pdf";
            SaveReport(Invoiceno, filepath);
        }

        private void SaveReport(string invoiceno, string fileName)
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

            _reportName = @"/GeniusReports/PurchaseOrder/SPM_ShippingInvoicePacking";
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

        private void empidtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (connectapi.EmployeeExits(empidtxt.Text.Trim()))
                {
                    empname.Text = connectapi.getuserfullname(empidtxt.Text.Trim());
                    empname.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(empname.Text.ToLower());
                    appidtxt.Focus();                    
                }
                else
                {
                    empidtxt.Clear();
                    empname.Clear();
                    empidtxt.Focus();
                    MessageBox.Show("Employee not found. Please contact the admin", "SPM Connect - Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
          
        }

        private void appidtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (connectapi.EmployeeExitsWithCribRights(appidtxt.Text.Trim()))
                {
                    appnametxt.Text = connectapi.getuserfullname(appidtxt.Text.Trim());
                    appnametxt.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(appnametxt.Text.ToLower());
                    jobreqtxt.Focus();
                }
                else
                {
                    appidtxt.Clear();
                    appnametxt.Clear();
                    appidtxt.Focus();
                    MessageBox.Show("Your request for approving material reallocation can't be completed based on your security settings."+ Environment.NewLine+ "Please scan in ID with correct privileges.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void ItemTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (ItemTxtBox.Text.Length >= 6)
                {                   
                    string item = ItemTxtBox.Text.Trim().Substring(0, 6);
                    fillselectediteminfo(item);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void fillselectediteminfo(string item)
        {
            DataTable iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetIteminfo(item);
            DataRow r = iteminfo.Rows[0];
            Descriptiontxtbox.Text = r["Description"].ToString();
            oemtxt.Text = r["Manufacturer"].ToString();
            oemitemnotxt.Text = r["ManufacturerItemNumber"].ToString();
        }

        private void empidtxt_TextChanged(object sender, EventArgs e)
        {
            if (empidtxt.Text.Length == 0) empname.Text = "";
        }

        private void appidtxt_TextChanged(object sender, EventArgs e)
        {
            if (appidtxt.Text.Length == 0) appnametxt.Text = "";
        }
    }
}