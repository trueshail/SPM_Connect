using SearchDataSPM.Miscellaneous;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SearchDataSPM
{
    public partial class MatReAlloc : Form
    {
        #region Load Invoice Details and setting Parameters

        private readonly List<char> _barcode = new List<char>(10);
        private readonly WorkOrder connectapi = new WorkOrder();
        private DateTime _lastKeystroke = new DateTime(0);
        private bool developer;
        private DataTable dt = new DataTable();
        private string Invoice_Number = "";
        private log4net.ILog log;
        private int userinputtime = 100;

        public MatReAlloc()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        public string invoicenumber(string number)
        {
            if (number.Length > 0)
                return Invoice_Number = number;
            return null;
        }

        private bool GetMatReInfo()
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

        private void QuoteDetails_Load(object sender, EventArgs e)
        {
            if (GetMatReInfo())
            {
                FillInfo();
                processeditbutton();
                developer = ConnectUser.Developer;
                userinputtime = connectapi.Getuserinputtime();
            }
            else
            {
                this.Close();
            }
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Material Re-Alloc Invoice " + Invoice_Number + " ");
        }

        #endregion Load Invoice Details and setting Parameters

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

        #endregion Fill information on controls

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

        #endregion shortcuts

        #region FormClosing

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible)
            {
                errorProvider1.Clear();
                e.Cancel = true;
                if (!(appnametxt.Text.Trim().Length > 0 && qtytxt.Text.Trim().Length > 0))
                {
                    errorProvider1.SetError(qtytxt, "Qty cannot be null");
                    errorProvider1.SetError(appidtxt, "Scan Emp Id");
                }
                else
                {
                    errorProvider1.SetError(savbttn, "Save before closing");
                }
            }
            else
            {
            }
        }

        #endregion FormClosing

        #region Process Save

        private readonly List<string> list = new List<string>();

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

        private void perfromsavebttn()
        {
            errorProvider1.Clear();
            if (!(appnametxt.Text.Trim().Length > 0 && qtytxt.Text.Trim().Length > 0))
            {
                errorProvider1.SetError(qtytxt, "Qty cannot be null");
                errorProvider1.SetError(appidtxt, "It has to be approved");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                perfromlockdown();
                graballinfor();
                if (connectapi.UpdateInvoiceDetsToSql(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14]))
                {
                    if (GetMatReInfo())
                    {
                        FillInfo();
                        SaveReport(invoicetxtbox.Text);
                    }
                }
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void savbttn_Click(object sender, EventArgs e)
        {
            perfromsavebttn();
        }

        #endregion Process Save

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

        #endregion Process Edit

        #region Print Reports

        private void print1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer("ShippingInvPack", invoicetxtbox.Text);
            form1.Show();
        }

        private void print2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewer form1 = new ReportViewer("ShippingInvCom", invoicetxtbox.Text);
            form1.Show();
        }

        #endregion Print Reports

        #region Save Report

        private void SaveReport(string Invoiceno)
        {
            string filepath = @"\\spm-adfs\SDBASE\Reports\MaterialReallocations\";
            Directory.CreateDirectory(filepath);
            filepath += Invoiceno + ".pdf";
            Savereporttodir(Invoiceno, filepath);
        }

        private void Savereporttodir(string invoiceno, string fileName)
        {
            const string _reportName = "/GeniusReports/WorkOrder/SPM_Connect_MatReAloc";
            RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[1];
            parameters[0] = new RE2005.ParameterValue
            {
                Name = "pInvno",
                Value = invoiceno
            };

            ReportHelper.SaveReport(fileName, _reportName, parameters);
        }

        private void Sendemailtomanagers(string reqno, string fileName)
        {
            //connectapi.SPM_Connect();
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.CribShort, "1"))
                connectapi.TriggerEmail(item.email, reqno + " Material Re-Allocation", item.name, " Please see attached invoice regarding crib shortage", fileName, "", "", "Normal");
        }

        #endregion Save Report

        private void appidtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check timing (keystrokes within 100 ms)
            TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            if (elapsed.TotalMilliseconds > userinputtime)
                _barcode.Clear();

            // record keystroke & timestamp
            _barcode.Add(e.KeyChar);
            _lastKeystroke = DateTime.Now;

            // process barcode
            if (e.KeyChar == 13 && _barcode.Count > 0)
            {
                string msg = new string(_barcode.ToArray());

                _barcode.Clear();
                if (msg != "\r" || developer)
                {
                    if (e.KeyChar == 13)
                    {
                        if (connectapi.EmployeeExitsWithCribRights(appidtxt.Text.Trim()))
                        {
                            appnametxt.Text = connectapi.GetNameByEmpId(appidtxt.Text.Trim());
                            appnametxt.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(appnametxt.Text.ToLower());
                            jobreqtxt.Focus();
                        }
                        else
                        {
                            appidtxt.Clear();
                            appnametxt.Clear();
                            appidtxt.Focus();
                            MessageBox.Show("Your request for approving material reallocation can't be completed based on your security settings." + Environment.NewLine + "Please scan in ID with correct privileges.", "SPM Connect - Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        e.Handled = true;
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    appidtxt.Clear();
                    appidtxt.Focus();
                }
            }
        }

        private void appidtxt_TextChanged(object sender, EventArgs e)
        {
            if (appidtxt.Text.Length == 0) appnametxt.Text = "";
        }

        private void empidtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check timing (keystrokes within 100 ms)
            TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            if (elapsed.TotalMilliseconds > userinputtime)
                _barcode.Clear();

            // record keystroke & timestamp
            _barcode.Add(e.KeyChar);
            _lastKeystroke = DateTime.Now;

            // process barcode
            if (e.KeyChar == 13 && _barcode.Count > 0)
            {
                string msg = new string(_barcode.ToArray());

                _barcode.Clear();
                if (msg != "\r" || developer)
                {
                    if (e.KeyChar == 13)
                    {
                        if (connectapi.EmployeeExits(empidtxt.Text.Trim()))
                        {
                            empname.Text = connectapi.GetNameByEmpId(empidtxt.Text.Trim());
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
                    }
                }
                else
                {
                    MessageBox.Show("System cannot accept keyboard inputs. Scan with barcode reader", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    empidtxt.Clear();
                    empidtxt.Focus();
                }
            }
        }

        private void empidtxt_TextChanged(object sender, EventArgs e)
        {
            if (empidtxt.Text.Length == 0) empname.Text = "";
        }

        private void empname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                appidtxt.Focus();
                e.Handled = true;
            }
        }

        private void Fillselectediteminfo(string item)
        {
            DataTable iteminfo = new DataTable();
            iteminfo.Clear();
            iteminfo = connectapi.GetIteminfo(item);
            DataRow r = iteminfo.Rows[0];
            Descriptiontxtbox.Text = r["Description"].ToString();
            oemtxt.Text = r["Manufacturer"].ToString();
            oemitemnotxt.Text = r["ManufacturerItemNumber"].ToString();
        }

        private void ItemTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (ItemTxtBox.Text.Length >= 6)
                {
                    string item = ItemTxtBox.Text.Trim().Substring(0, 6);
                    Fillselectediteminfo(item);
                    notestxt.Focus();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void jobreqtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                woreqtxt.Focus();
                e.Handled = true;
            }
        }

        private void MatReAlloc_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Material Re-Alloc Invoice " + Invoice_Number + " ");
            this.Dispose();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void PrintToolStrip_Click(object sender, EventArgs e)
        {
            reportpurchaereq(Invoice_Number, "MatReAloc");
        }

        private void qtytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(qtytxt.Text) || qtytxt.Text == "0")
                {
                    qtytxt.Clear();
                    qtytxt.Focus();
                }
                else
                {
                    ItemTxtBox.Focus();
                }

                e.Handled = true;
            }
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[0-9+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
            }
            else
            {
                e.Handled = true;
            }
        }

        private void qtytxt_Leave(object sender, EventArgs e)
        {
        }

        private void reportpurchaereq(string itemvalue, string Reportname)
        {
            ReportViewer form1 = new ReportViewer(Reportname, itemvalue);
            form1.Show();
        }

        private void woreqtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                jobtakenfrom.Focus();
                e.Handled = true;
            }
        }

        private void wotakentxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                qtytxt.Focus();
                e.Handled = true;
            }
        }
    }
}