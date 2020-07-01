using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM.Admin_developer
{
    public partial class BlockedForms : Form
    {
        private readonly SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
        private readonly DataTable dt;
        private log4net.ILog log;

        public BlockedForms()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        private void Checkdeveloper()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE UserName = @username AND Developer = '1'", connectapi.cn))
            {
                try
                {
                    connectapi.cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", connectapi.ConnectUser.UserName);

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
                    connectapi.cn.Close();
                }
            }
        }

        private void Freeuser_Click(object sender, EventArgs e)
        {
            string username = Getuserselected().Trim();

            Freeuserform(username, Getapplicaitonrunning().Trim(), GetItemSelected().Trim());

            try
            {
                dt.Rows.Clear();
                dataGridView1.Refresh();
                Loaddata();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Delete User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Freeuserform(string username, string app, string itemid)
        {
            if (username.Length > 0)
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                try
                {
                    string query = "DELETE FROM [SPM_Database].[dbo].[UserHolding] WHERE [UserName] ='" + username + "'" +
                        " and [App] ='" + app + "' and [ItemId] = '" + itemid + "' ";
                    SqlCommand sda = new SqlCommand(query, connectapi.cn);
                    sda.ExecuteNonQuery();
                    connectapi.cn.Close();
                    // MetroFramework.MetroMessageBox.Show(this, username + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Free Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private string Getapplicaitonrunning()
        {
            int selectedclmindex = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView1.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];

                //MessageBox.Show(ItemNo);
                return Convert.ToString(slectedrow.Cells[0].Value);
            }
            else
            {
                return "";
            }
        }

        private string GetItemSelected()
        {
            int selectedclmindex = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView1.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];

                //MessageBox.Show(ItemNo);
                return Convert.ToString(slectedrow.Cells[2].Value);
            }
            else
            {
                return "";
            }
        }

        private string Getuserselected()
        {
            int selectedclmindex = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewColumn columnchk = dataGridView1.Columns[selectedclmindex];
            _ = Convert.ToString(columnchk.Index);
            if (dataGridView1.SelectedRows.Count == 1 || dataGridView1.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView1.Rows[selectedrowindex];

                //MessageBox.Show(ItemNo);
                return Convert.ToString(slectedrow.Cells[1].Value);
            }
            else
            {
                return "";
            }
        }

        private void Loaddata()
        {
            try
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[UserHolding] ORDER BY [CheckInDateTime] DESC", connectapi.cn);

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
                connectapi.cn.Close();
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

        private void UserStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed Blocked Forms");
            this.Dispose();
        }

        private void UserStatus_Load(object sender, EventArgs e)
        {
            Checkdeveloper();
            Loaddata();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened Blocked Forms");
        }
    }
}