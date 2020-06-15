using SearchDataSPM.Report;
using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM.Accounting
{
    public partial class EFTHome : MetroFramework.Forms.MetroForm
    {
        private readonly SPMSQLCommands connectapi = new SPMSQLCommands();
        private DataTable dt;
        private List<string> rejectedemailslist;
        private bool bademails;
        private log4net.ILog log;
        private bool splashWorkDone;

        public EFTHome()
        {
            InitializeComponent();
        }

        private void BatchProcess()
        {
            List<Vendor> vendorlist = new List<Vendor>();

            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Vendor vendor = new Vendor
                    {
                        EFTId = Convert.ToString(row["Id"].ToString()),
                        PaymentDate = Convert.ToString(row["PaymentDate"].ToString()),
                        PaymentNo = Convert.ToString(row["PaymentNo"].ToString()),
                        VendorName = Convert.ToString(row["VendorName"].ToString()),
                        VendorId = Convert.ToString(row["VendorId"].ToString()),
                        Amount = Convert.ToString(row["TotalAmount"].ToString()),
                        FirstName = Convert.ToString(row["FirstName"].ToString()),
                        LastName = Convert.ToString(row["LastName"].ToString()),
                        Email = Convert.ToString(row["Email"].ToString()),
                        EmailSent = Convert.ToString(row["EmailSent"].ToString()),
                        PaymentType = Convert.ToString(row["PaymentType"].ToString()),
                    };
                    vendorlist.Add(vendor);
                }
            }
            else
            {
                splashWorkDone = true;
                return;
            }
            foreach (Vendor vendor in vendorlist)
            {
                ProcessReportSending(vendor);
            }

            splashWorkDone = true;
            vendorlist.Clear();
        }

        private void Bttnshowapproved_Click(object sender, EventArgs e)
        {
            Showallitems("");
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            filterbttn.BackColor = Color.Transparent;

            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.LightSkyBlue;
        }

        private void Bttnshowmydept_Click(object sender, EventArgs e)
        {
            Showallitems("No");
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            filterbttn.BackColor = Color.Transparent;
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.LightSkyBlue;
        }

        private void Calculatetotal()
        {
            if (dataGridView.Rows.Count > 0)
            {
                decimal total = 0.00m;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    try
                    {
                        decimal price = !string.IsNullOrEmpty(row.Cells[6].Value.ToString()) ? Convert.ToDecimal(row.Cells[6].Value.ToString()) : 0;
                        total += price;
                        totallbl.Text = "Total Value : $" + string.Format("{0:n}", Convert.ToDecimal(total.ToString()));

                        string totalvalue = string.Format("{0:#.00}", total.ToString());
                    }
                    catch (Exception ex)
                    {
                        MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect -  Error Getting Total", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                totallbl.Text = "";
            }
        }

        private void CheckInEFT(Vendor vendor)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss");

            if (connectapi.cn.State == ConnectionState.Closed)
                connectapi.cn.Open();
            try
            {
                SqlCommand cmd = connectapi.cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[EFTEmailTracker] ([ID],[PaymentNo],[EmailSent], [DateSent]) VALUES('" + vendor.EFTId + "', '" + vendor.PaymentNo + "', '1', '" + sqlFormattedDate + "')";
                cmd.ExecuteNonQuery();
                connectapi.cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Check In EFT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectapi.cn.Close();
            }
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            _ = dataGridView.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView.ClearSelection();
                dataGridView.Rows[columnindex].Selected = true;
            }
        }

        private void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Calculatetotal();
        }

        private void DataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Calculatetotal();
        }

        private async void Emailallbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send email EFT attachments to all the vendors listed below?", "SPM Connect - Send Email EFT?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bademails = false;
                rejectedemailslist.Clear();

                await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                BatchProcess();
                if (bademails)
                    PrintBadEmail();
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                this.Focus();
                this.Activate();
                splashWorkDone = true;
                bademails = false;
            }
        }

        private async void Emailtoolstrip_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send email EFT attachment to selected vendor?", "SPM Connect - Send Email EFT?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bademails = false;
                    rejectedemailslist.Clear();
                    DataGridViewRow row = this.dataGridView.SelectedRows[0];
                    Vendor vendor_ = new Vendor
                    {
                        EFTId = Convert.ToString(row.Cells[0].Value),
                        PaymentDate = Convert.ToString(row.Cells[2].Value),
                        PaymentNo = Convert.ToString(row.Cells[1].Value),
                        VendorName = Convert.ToString(row.Cells[3].Value),
                        VendorId = Convert.ToString(row.Cells[4].Value),
                        Amount = Convert.ToString(row.Cells[6].Value),
                        FirstName = Convert.ToString(row.Cells[10].Value),
                        LastName = Convert.ToString(row.Cells[11].Value),
                        Email = Convert.ToString(row.Cells[12].Value),
                        EmailSent = Convert.ToString(row.Cells[9].Value),
                        PaymentType = Convert.ToString(row.Cells[5].Value),
                    };
                    await Task.Run(() => SplashDialog("Sending Email...")).ConfigureAwait(true);
                    Cursor.Current = Cursors.WaitCursor;
                    this.Enabled = false;
                    ProcessReportSending(vendor_);
                    if (bademails)
                        PrintBadEmail();
                    Cursor.Current = Cursors.Default;
                    this.Enabled = true;
                    this.Focus();
                    this.Activate();
                    splashWorkDone = true;
                    bademails = false;
                }
            }
        }

        private void EvictionsHome_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            dt = new DataTable();
            rejectedemailslist = new List<string>();
            Showallitems("No");
            bttnshowwaiting.BackColor = Color.LightSkyBlue;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened SPM Connect EFT ");
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void Filterbttn_Click(object sender, EventArgs e)
        {
            Loaddatabetween(frmdatepick.Value.ToString("yyyy-MM-dd"), todatepic.Value.ToString("yyyy-MM-dd"));
            foreach (Control c in managergroupbox.Controls)
            {
                c.BackColor = Color.Transparent;
            }
            reloadbttn.BackColor = Color.Transparent;
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.LightSkyBlue;
        }

        private Vendor GetselectedReport()
        {
            DataGridViewRow row = this.dataGridView.SelectedRows[0];
            return new Vendor
            {
                EFTId = Convert.ToString(row.Cells[0].Value),
                PaymentDate = Convert.ToString(row.Cells[2].Value),
                PaymentNo = Convert.ToString(row.Cells[1].Value),
                VendorName = Convert.ToString(row.Cells[3].Value),
                VendorId = Convert.ToString(row.Cells[4].Value),
                Amount = Convert.ToString(row.Cells[6].Value),
                FirstName = Convert.ToString(row.Cells[10].Value),
                LastName = Convert.ToString(row.Cells[11].Value),
                Email = Convert.ToString(row.Cells[12].Value),
                EmailSent = Convert.ToString(row.Cells[9].Value),
                PaymentType = Convert.ToString(row.Cells[5].Value),
            };
        }

        private void GetWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                Vendor vendor = GetselectedReport();
                ReportViewer form1 = new ReportViewer("EFT", vendor.EFTId, vendor.PaymentType);
                form1.Show();
            }
        }

        private void Loaddatabetween(string datefrom, string dateto)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("ShowAllRecordsBetween", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@datestart", datefrom);
                    sda.SelectCommand.Parameters.AddWithValue("@dateto", dateto);
                    dt.Clear();
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void PrintBadEmail()
        {
            string listemails = "";
            if (rejectedemailslist.Count > 0)
            {
                foreach (string email in rejectedemailslist)
                {
                    listemails += email + "<br>";
                }
                SendEmailToAccountants(listemails);
            }
            else
            {
                return;
            }
        }

        private void ProcessReportSending(Vendor _vendor)
        {
            // save report
            string filename = SaveReport(_vendor.EFTId);
            SaveReport(_vendor.EFTId, filename, _vendor.PaymentType);
            // get the file name
            //get the vendor email
            //send email with attachment
            if (Sendemailyesno())
            {
                bool success = connectapi.TriggerEmail(_vendor.Email,
                    (_vendor.PaymentType == "EFTTD" ? "EFT" : "ACH") + " Remittance from SPM Automation (Canada) Inc.", "",
                    _vendor.PaymentType == "EFTTD" ? "EFT" : "ACH", filename, "", "", "EFT");
                // write back to database
                //reload the datagrid

                if (success && string.Equals(_vendor.EmailSent, "no", StringComparison.CurrentCultureIgnoreCase))
                {
                    CheckInEFT(_vendor);
                }
                else
                {
                    bademails = true;
                    RejectedEmail(_vendor.Email, _vendor.VendorName);
                }

                Showallitems("No");
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Emails are turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RejectedEmail(string email, string vendorname)
        {
            rejectedemailslist.Add(vendorname + " - " + email);
        }

        private void Reloadbttn_Click(object sender, EventArgs e)
        {
            Showallitems("No");
            filterbttn.BackColor = Color.Transparent;
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.LightSkyBlue;
        }

        private void ReportAllRecords_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed SPM Connect EFT ");
            this.Dispose();
        }

        private string SaveReport(string reqno)
        {
            string filepath = connectapi.ConnectUser.SharesFolder + @"\SPM_Connect\EFTReports\";
            Directory.CreateDirectory(filepath);
            return _ = filepath + reqno + ".pdf";
        }

        private void SaveReport(string invoiceno, string fileName, string paymenttype)
        {
            const string _reportName = "/GeniusReports/Accounting/SPM_EFT";
            RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[2];
            parameters[0] = new RE2005.ParameterValue
            {
                //parameters[0].Label = "";
                Name = "pCode",
                Value = invoiceno
            };
            parameters[1] = new RE2005.ParameterValue
            {
                //parameters[0].Label = "";
                Name = "pTransNo",
                Value = paymenttype
            };
            ReportHelper.SaveReport(fileName, _reportName, parameters);
        }

        private void SendEmailToAccountants(string body)
        {
            foreach (NameEmail item in connectapi.GetNameEmailByParaValue(UserFields.Department, "Accounting"))
                connectapi.TriggerEmail(item.email, "Unable to deliver EFT reports ", item.name, Environment.NewLine + "Here is the list of vendors with email address whom report cannot be delivered." + Environment.NewLine + Environment.NewLine + body, "", "", "", "Normal");
        }

        private bool Sendemailyesno()
        {
            bool sendemail = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailEFT'", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    limit = (string)cmd.ExecuteScalar();
                    connectapi.cn.Close();
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Email access for EFT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            if (limit == "1")
            {
                sendemail = true;
            }
            return sendemail;
        }

        private void Showallitems(string type)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("ShowAllRecordsByType", connectapi.cn))
            {
                try
                {
                    if (connectapi.cn.State == ConnectionState.Closed)
                        connectapi.cn.Open();
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@filterby", type);
                    dt.Clear();
                    sda.Fill(dt);
                    dataGridView.DataSource = dt;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Width = 100;
                    dataGridView.Columns[2].Width = 120;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[4].Width = 100;
                    dataGridView.Columns[5].Width = 120;
                    dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[9].Width = 100;
                    dataGridView.Columns[10].Width = 100;
                    dataGridView.Columns[11].Width = 100;
                    dataGridView.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void SplashDialog(string message)
        {
            splashWorkDone = false;
            ThreadPool.QueueUserWorkItem((_) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + ((this.Width - splashForm.Width) / 2), this.Location.Y + ((this.Height - splashForm.Height) / 2));
                    splashForm.Show();
                    while (!splashWorkDone)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
        }
    }
}