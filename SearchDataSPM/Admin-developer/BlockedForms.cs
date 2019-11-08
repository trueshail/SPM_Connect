using SPMConnect.UserActionLog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM.Admin_developer
{
    public partial class BlockedForms : Form
    {
        private SqlConnection cn;
        private string connection;
        private DataTable dt;
        private log4net.ILog log;
        private UserActions _userActions;
        private ErrorHandler errorHandler = new ErrorHandler();

        public BlockedForms()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();

            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Connecting to SQL Server.....", "SPM Connect - ENG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            dt = new DataTable();
        }

        private void UserStatus_Load(object sender, EventArgs e)
        {
            Checkdeveloper();
            loaddata();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Blocked Forms by " + System.Environment.UserName);
            _userActions = new UserActions(this);
        }

        private void loaddata()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UserHolding] ORDER BY [CheckInDateTime] DESC", cn);

                dt.Clear();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 140;
                dataGridView1.Columns[1].Width = 160;
                dataGridView1.Columns[2].Width = 110;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                UpdateFont();
            }
            catch (Exception)
            {
                MessageBox.Show("Data cannot be retrieved from server. Please contact the admin.", "SPM Connect - SQL SERVER UserStatus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                cn.Close();
            }
        }

        private void UpdateFont()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
            string count = dataGridView1.Rows.Count.ToString();
            this.Text = "Blocked Forms - " + count;
        }

        private void Checkdeveloper()
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Developer = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        dataGridView1.ContextMenuStrip = Listviewcontextmenu;
                        Listviewcontextmenu.Enabled = true;
                        Listviewcontextmenu.Visible = true;
                    }
                    else
                    {
                        dataGridView1.ContextMenuStrip = null;
                        Listviewcontextmenu.Enabled = false;
                        Listviewcontextmenu.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void freeuser_Click(object sender, EventArgs e)
        {
            string username = getuserselected().Trim();

            freeuserform(username, getapplicaitonrunning().Trim(), getItemSelected().Trim());

            try
            {
                dt.Rows.Clear();
                dataGridView1.Refresh();
                loaddata();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Delete User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void freeuserform(string username, string app, string itemid)
        {
            if (username.Length > 0)
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                try
                {
                    string query = "DELETE FROM [SPM_Database].[dbo].[UserHolding] WHERE [UserName] ='" + username.ToString() + "'" +
                        " and [App] ='" + app.ToString() + "' and [ItemId] = '" + itemid.ToString() + "' ";
                    SqlCommand sda = new SqlCommand(query, cn);
                    sda.ExecuteNonQuery();
                    cn.Close();
                    // MetroFramework.MetroMessageBox.Show(this, username + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Free Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void UserStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            _userActions.FinishLoggingUserActions(this);
            log.Info("Closed Blocked Forms by " + System.Environment.UserName);
            this.Dispose();
        }

        private string getuserselected()
        {
            int selectedclmindex = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView1.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string item;
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[1].Value);
                //MessageBox.Show(ItemNo);
                return item;
            }
            else
            {
                item = "";
                return item;
            }
        }

        private string getItemSelected()
        {
            int selectedclmindex = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView1.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string item;
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[2].Value);
                //MessageBox.Show(ItemNo);
                return item;
            }
            else
            {
                item = "";
                return item;
            }
        }

        private string getapplicaitonrunning()
        {
            int selectedclmindex = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView1.Columns[selectedclmindex];
            string c = Convert.ToString(columnchk.Index);
            //MessageBox.Show(c);
            string item;
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(ItemNo);
                return item;
            }
            else
            {
                item = "";
                return item;
            }
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