using SPMConnect.UserActionLog;
using SPMConnectAPI;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class BinLog : Form
    {
        WorkOrder connectapi = new WorkOrder();
        bool formloading = false;
        DataTable workorderstatus = new DataTable();
        log4net.ILog log;
        private UserActions _userActions;
        ErrorHandler errorHandler = new ErrorHandler();

        public BinLog()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
            //connectapi.SPM_Connect();
        }

        private void PopulateWorkOrders()
        {
            wolistbox.Items.Clear();
            DataTable workorderlist = new DataTable();
            workorderlist = connectapi.ShowDistinctWO();

            foreach (DataRow dr in workorderlist.Rows)
            {
                wolistbox.Items.Add(dr["WO"].ToString());
            }
            if (wolistbox.Items.Count > 0)
            {
                wolistbox.SelectedItem = wolistbox.Items[0];
            }

        }

        private void BinLog_Load(object sender, EventArgs e)
        {
            loadform();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Inventory Bin Status by " + System.Environment.UserName);
            _userActions = new UserActions(this);
        }

        private void loadform()
        {
            formloading = true;
            PopulateWorkOrders();
            FillDgDt();
            formloading = false;
            wolistbox.SelectedIndex = 0;
            wolistbox.Focus();
            wolistselectionchanged();

        }

        private void wolistbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                wolistselectionchanged();
            }

        }

        private void wolistselectionchanged()
        {
            string selectedText = wolistbox.SelectedItem.ToString();
            DataView dv = workorderstatus.DefaultView;
            string columnName = workorderstatus.Columns[0].ColumnName;
            string filter = string.Format("{0} = '{1}'", columnName, selectedText);
            dv.RowFilter = filter;
            dataGridView.DataSource = dv;
            UpdateFont();
            FillWoDetails(selectedText);
            filltrackingprogress(selectedText);
        }

        private void FillDgDt()
        {
            workorderstatus.Clear();
            workorderstatus = connectapi.ShowWOStatusBinWithEMPName();
        }

        private void wolistbox_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource == null)
            {
                dataGridView.DataSource = workorderstatus;
                UpdateFont();
            }
        }

        private void UpdateFont()
        {

            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[8].Width = 80;
            dataGridView.Columns[9].Width = 80;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        private void woid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wolistbox.Focus();
                if (dataGridView.DataSource == null)
                {
                    dataGridView.DataSource = workorderstatus;
                    UpdateFont();
                }
                wolistbox.SelectedItem = woid_txtbox.Text.Trim();
                woid_txtbox.Clear();
                woid_txtbox.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void FillWoDetails(string wo)
        {
            DataTable dtb1 = new DataTable();
            dtb1 = connectapi.ShowWODetails(wo);

            if (dtb1.Rows.Count > 0)
            {
                DataRow r = dtb1.Rows[0];
                itemlabel.Text = "SPM Item No : " + r["Item"].ToString();
                descriptionlabel.Text = "Description : " + r["Description"].ToString();
                qtylabel.Text = "Qty :" + r["Qty"].ToString();

                inbuiltlabel.Text = "In-Mech-Build : " + r["Inbuilt"].ToString();
                joblbl.Text = "Job : " + r["Job"].ToString();

                if (r["Inbuilt"].ToString().Trim() == "Yes")
                {
                    inbuiltlabel.BackColor = Color.Yellow;
                    inbuiltlabel.ForeColor = Color.Black;
                }
                else
                {
                    inbuiltlabel.BackColor = Color.Red;
                    inbuiltlabel.ForeColor = Color.White;
                }

                completelabel.Text = "Build Completed : " + r["Completed"].ToString();
                if (r["Completed"].ToString().Trim() == "Yes")
                {
                    completelabel.BackColor = Color.Green;
                    completelabel.ForeColor = Color.White;
                }
                else
                {
                    completelabel.BackColor = Color.Yellow;
                    completelabel.ForeColor = Color.Black;
                }
            }
            else
            {
                itemlabel.Text = "SPM Item No : ";
                descriptionlabel.Text = "Description : ";
                qtylabel.Text = "Qty :";
                inbuiltlabel.Text = "In-Mech-Build : ";
                joblbl.Text = "Job : ";
                completelabel.Text = "Build Completed : ";
                completelabel.BackColor = SystemColors.ButtonFace;
                inbuiltlabel.BackColor = SystemColors.ButtonFace;
                inbuiltlabel.ForeColor = Color.Black;
                completelabel.ForeColor = Color.Black;
            }

        }

        private void reloadbttn_Click(object sender, EventArgs e)
        {
            loadform();
        }

        private void filltrackingprogress(string wo)
        {
            DataTable dtb1 = new DataTable();
            dtb1 = connectapi.ShowWOtrackingProg(wo);
            DataRow r = dtb1.Rows[0];

            wostatuslbl.Text = "Overall Progress : " + r["Status"].ToString();

            wostatuslbl.BackColor = Color.White;
            wostatuslbl.ForeColor = Color.Red;

            wogrpdesign.Text = "WO :- " + wo + " Tracking Progress :";

            if (r["Engin"].ToString() == "1")
            {
                label1.Text = "Engineering :";
                engchkbox.SetItemCheckState(0, CheckState.Checked);
                engchkbox.SetItemCheckState(1, CheckState.Checked);
                engchkbox.SetItemCheckState(2, CheckState.Checked);
                engchkbox.Items[1] = "Check In By : " + r["EngWho"].ToString();
                engchkbox.Items[2] = "Check In On : " + r["EngWhen"].ToString();
                engchkbox.BackColor = Color.Green;

                if (r["Engin"].ToString() == "1" && r["Prodin"].ToString() == "0")
                {
                    prodwaiting.BackColor = Color.Yellow;
                }
                else if (r["Engin"].ToString() == "1" && r["Prodin"].ToString() == "1")
                {
                    prodwaiting.BackColor = Color.Green;
                }
                else
                {
                    prodwaiting.BackColor = Color.Red;
                }

            }
            else if (r["Ctrlin"].ToString() == "1")
            {
                label1.Text = "Controls :";
                engchkbox.SetItemCheckState(0, CheckState.Checked);
                engchkbox.SetItemCheckState(1, CheckState.Checked);
                engchkbox.SetItemCheckState(2, CheckState.Checked);
                engchkbox.Items[1] = "Check In By : " + r["CtrlWho"].ToString();
                engchkbox.Items[2] = "Check In On : " + r["CtrlWhen"].ToString();
                engchkbox.BackColor = Color.Green;
                if (r["Ctrlin"].ToString() == "1" && r["Prodin"].ToString() == "0")
                {
                    prodwaiting.BackColor = Color.Yellow;
                }
                else if (r["Ctrlin"].ToString() == "1" && r["Prodin"].ToString() == "1")
                {
                    prodwaiting.BackColor = Color.Green;
                }
                else
                {
                    prodwaiting.BackColor = Color.Red;
                }
            }
            else
            {
                engchkbox.SetItemCheckState(0, CheckState.Unchecked);
                engchkbox.SetItemCheckState(1, CheckState.Unchecked);
                engchkbox.SetItemCheckState(2, CheckState.Unchecked);
            }




            if (r["Prodin"].ToString() == "1")
            {
                prodchkbox.SetItemCheckState(0, CheckState.Checked);
                prodchkbox.SetItemCheckState(1, CheckState.Checked);
                prodchkbox.SetItemCheckState(2, CheckState.Checked);
                prodchkbox.Items[1] = "Check In By : " + r["ProdinWho"].ToString();
                prodchkbox.Items[2] = "Check In On : " + r["ProdinWhen"].ToString();
                prodout.BackColor = Color.Red;
                prodchkbox.BackColor = Color.Orange;
            }
            else
            {
                prodchkbox.SetItemCheckState(0, CheckState.Unchecked);
                prodchkbox.SetItemCheckState(1, CheckState.Unchecked);
                prodchkbox.SetItemCheckState(2, CheckState.Unchecked);
                prodchkbox.Items[1] = "Check In By : ";
                prodchkbox.Items[2] = "Check In On : ";
                prodout.BackColor = Color.Red;
                prodchkbox.BackColor = Color.Silver;
            }

            if (r["Prodout"].ToString() == "1")
            {
                prodchkbox.SetItemCheckState(3, CheckState.Checked);
                prodchkbox.SetItemCheckState(4, CheckState.Checked);
                prodchkbox.SetItemCheckState(5, CheckState.Checked);
                prodchkbox.Items[4] = "Check Out By : " + r["ProdoutWho"].ToString();
                prodchkbox.Items[5] = "Check Out On : " + r["ProdoutWhen"].ToString();
                prodout.BackColor = Color.Green;
                prodchkbox.BackColor = Color.Green;
                timespentprodlbl.Text = "Time Spent In Production :" + Environment.NewLine + r["TimeInProd"].ToString();
                timespentprodlbl.Visible = true;
            }
            else
            {
                prodchkbox.SetItemCheckState(3, CheckState.Unchecked);
                prodchkbox.SetItemCheckState(4, CheckState.Unchecked);
                prodchkbox.SetItemCheckState(5, CheckState.Unchecked);
                prodchkbox.Items[4] = "Check Out By : ";
                prodchkbox.Items[5] = "Check Out On : ";
                timespentprodlbl.Visible = false;
            }


            ////////////////////////////////////////////////////////////////////



            if (r["Purin"].ToString() == "1")
            {
                purchkbox.SetItemCheckState(0, CheckState.Checked);
                purchkbox.SetItemCheckState(1, CheckState.Checked);
                purchkbox.SetItemCheckState(2, CheckState.Checked);
                purchkbox.Items[1] = "Check In By : " + r["PurinWho"].ToString();
                purchkbox.Items[2] = "Check In On : " + r["PurinWhen"].ToString();
                purwait.BackColor = Color.Orange;
                purchkbox.BackColor = Color.Orange;
            }
            else
            {
                purchkbox.SetItemCheckState(0, CheckState.Unchecked);
                purchkbox.SetItemCheckState(1, CheckState.Unchecked);
                purchkbox.SetItemCheckState(2, CheckState.Unchecked);
                purchkbox.Items[1] = "Check In By : ";
                purchkbox.Items[2] = "Check In On : ";
                if (r["Prodout"].ToString() == "1")
                {
                    purwait.BackColor = Color.Yellow;
                }
                else
                {
                    purwait.BackColor = Color.Red;
                }
                purchkbox.BackColor = Color.Silver;
            }

            if (r["Purout"].ToString() == "1")
            {
                purchkbox.SetItemCheckState(3, CheckState.Checked);
                purchkbox.SetItemCheckState(4, CheckState.Checked);
                purchkbox.SetItemCheckState(5, CheckState.Checked);
                purchkbox.Items[4] = "Check Out By : " + r["PuroutWho"].ToString();
                purchkbox.Items[5] = "Check Out On : " + r["PuroutWhen"].ToString();
                purwait.BackColor = Color.Green;
                purchkbox.BackColor = Color.Green;
                timespentpurlbl.Text = "Time Spent In Purchasing :" + Environment.NewLine + r["TimeInPur"].ToString();
                timespentpurlbl.Visible = true;
            }
            else
            {
                purchkbox.SetItemCheckState(3, CheckState.Unchecked);
                purchkbox.SetItemCheckState(4, CheckState.Unchecked);
                purchkbox.SetItemCheckState(5, CheckState.Unchecked);
                purchkbox.Items[4] = "Check Out By : ";
                purchkbox.Items[5] = "Check Out On : ";
                timespentpurlbl.Visible = false;
            }


            ///////////////////////////////////////////////////

            if (r["Cribin"].ToString() == "1")
            {
                cribchkbox.SetItemCheckState(0, CheckState.Checked);
                cribchkbox.SetItemCheckState(1, CheckState.Checked);
                cribchkbox.SetItemCheckState(2, CheckState.Checked);
                cribchkbox.Items[1] = "Check In By : " + r["CribinWho"].ToString();
                cribchkbox.Items[2] = "Check In On : " + r["CribinWhen"].ToString();
                cribwait.BackColor = Color.Orange;
                cribchkbox.BackColor = Color.Orange;
            }
            else
            {
                cribchkbox.SetItemCheckState(0, CheckState.Unchecked);
                cribchkbox.SetItemCheckState(1, CheckState.Unchecked);
                cribchkbox.SetItemCheckState(2, CheckState.Unchecked);
                cribchkbox.Items[1] = "Check In By : ";
                cribchkbox.Items[2] = "Check In On : ";
                if (r["Prodout"].ToString() == "1")
                {
                    cribwait.BackColor = Color.Yellow;
                }
                else
                {
                    cribwait.BackColor = Color.Red;
                }
                cribchkbox.BackColor = Color.Silver;
            }

            if (r["Cribout"].ToString() == "1")
            {
                cribchkbox.SetItemCheckState(3, CheckState.Checked);
                cribchkbox.SetItemCheckState(4, CheckState.Checked);
                cribchkbox.SetItemCheckState(5, CheckState.Checked);
                cribchkbox.Items[4] = "Check Out By : " + r["CriboutWho"].ToString();
                cribchkbox.Items[5] = "Check Out On : " + r["CriboutWhen"].ToString();
                cribwait.BackColor = Color.Green;
                cribchkbox.BackColor = Color.Green;
                timespentcriblbl.Text = "Time Spent In Crib :" + Environment.NewLine + r["TimeInCrib"].ToString();
                timespentcriblbl.Visible = true;
                overalltimelbl.Text = "Overall Time Required :" + Environment.NewLine + r["TotalTimeSpent"].ToString();
                overalltimelbl.Visible = true;
            }
            else
            {
                cribchkbox.SetItemCheckState(3, CheckState.Unchecked);
                cribchkbox.SetItemCheckState(4, CheckState.Unchecked);
                cribchkbox.SetItemCheckState(5, CheckState.Unchecked);
                cribchkbox.Items[4] = "Check Out By : ";
                cribchkbox.Items[5] = "Check Out On : ";
                timespentcriblbl.Visible = false;
                overalltimelbl.Visible = false;

            }

        }

        private void BinLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            _userActions.FinishLoggingUserActions(this);
            log.Info("Closed Inventory Bin Status by " + System.Environment.UserName);
            this.Dispose();
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, _userActions, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, _userActions, this);
        }
    }
}
