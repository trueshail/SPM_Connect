using SPMConnectAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

internal enum Level
{
    All,
    complete,
    Waiting
}

namespace SearchDataSPM
{
    public partial class ReportAllRecords : MetroFramework.Forms.MetroForm
    {
        private String connection;
        private SqlConnection cn;
        private DataTable dt;
        private Level myVar = Level.All;
        private SPMSQLCommands connectapi = new SPMSQLCommands();
        private bool splashWorkDone = false;

        public ReportAllRecords()
        {
            InitializeComponent();

            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(this, "Error Connecting to SQL Server.....", "SPM Connect - EFT Home Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            dt = new DataTable();
        }

        private void EvictionsHome_Load(object sender, EventArgs e)
        {
            Showallitems("No");
            bttnshowwaiting.BackColor = Color.LightSkyBlue;
        }

        private void Showallitems(string type)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("ShowAllRecordsByType", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
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
                    cn.Close();
                }
            }
        }

        private void Loaddatabetween(string datefrom, string dateto)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("ShowAllRecordsBetween", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
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
                    cn.Close();
                }
            }
        }

        private void Bttnshowapproved_Click(object sender, EventArgs e)
        {
            myVar = Level.All;
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

        private void Bttnshowmydept_Click(object sender, EventArgs e)
        {
            myVar = Level.Waiting;
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

        private void GetWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                Accounting.Vendor vendor = GetselectedReport();
                ReportViewer form1 = new ReportViewer("EFT", vendor.EFTId, vendor.PaymentType);
                form1.Show();
            }

        }

        private Accounting.Vendor GetselectedReport()
        {
            DataGridViewRow row = this.dataGridView.SelectedRows[0];
            return new Accounting.Vendor
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

        private string SaveReport(string reqno)
        {
            string fileName = "";
            string filepath = connectapi.Getsharesfolder() + @"\SPM_Connect\EFTReports\";
            System.IO.Directory.CreateDirectory(filepath);
            return fileName = filepath + reqno + ".pdf";
        }

        private async Task SaveReport(string invoiceno, string fileName, string paymenttype)
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
            string _reportName = @"/GeniusReports/Accounting/SPM_EFT";

            string _historyID = null;
            bool _forRendering = false;
            RS2005.ParameterValue[] _values = null;
            RS2005.DataSourceCredentials[] _credentials = null;
            RS2005.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                RE2005.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);
                RE2005.ParameterValue[] parameters = new RE2005.ParameterValue[2];

                if (_parameters.Length > 0)
                {
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
                Debug.Print(ex.Message);
                //throw ex;
            }
            finally
            {
            }
        }

        private bool Sendemailyesno()
        {
            bool sendemail = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'EmailEFT'", cn))
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
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Get Email access for EFT", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async Task ProcessReportSendingAsync(Accounting.Vendor vendor)
        {
            // save report
            string filename = SaveReport(vendor.EFTId);
            await SaveReport(vendor.EFTId, filename, vendor.PaymentType);
            // get the file name
            //get the vendor email
            //send email with attachment
            if (Sendemailyesno())
            {
                connectapi.SendemailAccounting(vendor.Email, (vendor.PaymentType == "EFTTD" ? "EFT" : "ACH") + " Remittance from SPM Automation (Canada) Inc.", (vendor.PaymentType == "EFTTD" ? "EFT" : "ACH"), filename, "");
                // write back to database
                //reload the datagrid
                if (vendor.EmailSent.ToLower() == "no")
                    CheckInEFT(vendor);
                Showallitems("No");
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Emails are turned off.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SplashDialog(string message)
        {
            splashWorkDone = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Dialog())
                {
                    splashForm.TopMost = true;
                    splashForm.Message = message;
                    splashForm.Location = new Point(this.Location.X + (this.Width - splashForm.Width) / 2, this.Location.Y + (this.Height - splashForm.Height) / 2);
                    splashForm.Show();
                    while (!splashWorkDone)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
        }

        private void CheckInEFT(Accounting.Vendor vendor)
        {
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDate = datecreated.ToString("yyyy-MM-dd HH:mm:ss");

            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[EFTEmailTracker] ([ID],[PaymentNo],[EmailSent], [DateSent]) VALUES('" + vendor.EFTId + "', '" + vendor.PaymentNo + "', '1', '" + sqlFormattedDate + "')";
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("New entry created", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Check In EFT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private async void emailtoolstrip_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send email EFT attachment to selected vendor?", "SPM Connect - Send Email EFT?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow row = this.dataGridView.SelectedRows[0];
                    Accounting.Vendor vendor = new Accounting.Vendor
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
                    if (vendor != null)
                    {
                        await Task.Run(() => SplashDialog("Sending Email..."));
                        Cursor.Current = Cursors.WaitCursor;
                        this.Enabled = false;
                        await ProcessReportSendingAsync(vendor);
                        Cursor.Current = Cursors.Default;
                        this.Enabled = true;
                        this.Focus();
                        this.Activate();
                        splashWorkDone = true;
                    }
                }
            }
        }

        private async void emailallbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroFramework.MetroMessageBox.Show(this, "Are you sure want to send email EFT attachments to all the vendors listed below?", "SPM Connect - Send Email EFT?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await Task.Run(() => SplashDialog("Sending Email..."));
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                BatchProcess();
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                this.Focus();
                this.Activate();
                splashWorkDone = true;
            }
        }

        private async void BatchProcess()
        {
            List<Accounting.Vendor> vendorlist = new List<Accounting.Vendor>();

            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Accounting.Vendor vendor = new Accounting.Vendor
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
            foreach (Accounting.Vendor vendor in vendorlist)
            {
                await ProcessReportSendingAsync(vendor);
            }
            splashWorkDone = true;
        }

        private void Reloadbttn_Click(object sender, EventArgs e)
        {
            myVar = Level.Waiting;
            Showallitems("No");
            filterbttn.BackColor = Color.Transparent;
            //set the clicked control to a different color
            Control o = (Control)sender;
            o.BackColor = Color.LightSkyBlue;
        }

        private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            if (e.Button == MouseButtons.Right)
            {
                int columnindex = e.RowIndex;
                dataGridView.ClearSelection();
                dataGridView.Rows[columnindex].Selected = true;
            }
        }
    }
}