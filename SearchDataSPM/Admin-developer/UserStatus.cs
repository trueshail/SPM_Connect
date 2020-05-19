using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectConstants;

namespace SearchDataSPM.Admin_developer
{
    public partial class UserStatus : Form
    {
        private readonly SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
        private readonly DataTable dt;
        private log4net.ILog log;

        public UserStatus()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        private void Checkdeveloper()
        {
            if (ConnectUser.Developer)
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

        private void Deleteuser(string username)
        {
            if (username.Length > 0)
            {
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                try
                {
                    string query = "DELETE FROM [SPM_Database].[dbo].[Checkin] WHERE [User Name] ='" + username + "'";
                    SqlCommand sda = new SqlCommand(query, connectapi.cn);
                    sda.ExecuteNonQuery();
                    connectapi.cn.Close();
                    // MetroFramework.MetroMessageBox.Show(this, username + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Delete User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void Freeuser_Click(object sender, EventArgs e)
        {
            string userName = Getuserselected().Trim();

            if (userName != ConnectUser.UserName)
            {
                Deleteuser(userName);
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
                return Convert.ToString(slectedrow.Cells[2].Value);
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

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[Checkin] ORDER BY [Last Login] DESC", connectapi.cn);

                dt.Clear();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 140;
                dataGridView1.Columns[1].Width = 160;
                dataGridView1.Columns[2].Width = 110;
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

        private void ShutDownAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> intList = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["User Name"].ToString() != ConnectUser.UserName)
                {
                    intList.Add(row["User Name"].ToString());
                }
            }

            foreach (string user in intList)
            {
                if (user != System.Security.Principal.WindowsIdentity.GetCurrent().Name)
                {
                    Deleteuser(user);
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
            }
        }

        private void UpdateAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> intList = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["User Name"].ToString() != ConnectUser.UserName)
                {
                    intList.Add(row["User Name"].ToString());
                }
            }
            foreach (string user in intList)
            {
                if (user != System.Security.Principal.WindowsIdentity.GetCurrent().Name)
                {
                    UpdateUser(user);
                    try
                    {
                        dt.Rows.Clear();
                        dataGridView1.Refresh();
                        Loaddata();
                    }
                    catch (Exception ex)
                    {
                        MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Update User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            this.Text = "User Status - " + count + " users live";
        }

        private void UpdateUser(string username)
        {
            if (username.Length > 0)
            {
                DateTime datecreated = DateTime.Now;
                string sqlFormattedDate = datecreated.ToString("dd-MM-yyyy HH:mm tt");
                if (connectapi.cn.State == ConnectionState.Closed)
                    connectapi.cn.Open();
                try
                {
                    string query = "UPDATE [SPM_Database].[dbo].[Checkin] SET [Last Login] = '" + sqlFormattedDate + "' WHERE [User Name] ='" + username + "'";
                    SqlCommand sda = new SqlCommand(query, connectapi.cn);
                    sda.ExecuteNonQuery();
                    connectapi.cn.Close();
                    // MetroFramework.MetroMessageBox.Show(this, username + " - Is removed from the system now!", "SPM Connect - Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect - Update User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
        }

        private void UserStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed User Status ");
            this.Dispose();
        }

        private void UserStatus_Load(object sender, EventArgs e)
        {
            Checkdeveloper();
            Loaddata();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened User Status ");
        }
    }
}