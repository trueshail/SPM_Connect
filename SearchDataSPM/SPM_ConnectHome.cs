using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class SPM_ConnectHome : Form
    {
        public SPM_ConnectHome()
        {
            InitializeComponent();
        }

        int time = 0;

        string connection;
        SqlConnection cn;

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time + 10;
            rectangleShape2.Width += 11;

            if (time == 240)
            {
                Connect_SPMSQL();
            }

            if (time >= 250)
            {
                timer1.Stop();
                this.Hide();
            }
        }

        private void SPM_ConnectHome_Load(object sender, EventArgs e)
        {

            timer1.Start();
        }

        public void Connect_SPMSQL()
        {
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            try
            {
                cn = new SqlConnection(connection);
                cn.Open();

            }
            catch (Exception)
            {

                MetroFramework.MetroMessageBox.Show(this, "Cannot connect through the server. Please check the network connection.", "SPM Connect Home - SQL Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                System.Environment.Exit(0);
            }
            finally
            {
                cn.Close();
                if (userexists(userName))
                {
                    if (!checkmaintenance())
                    {
                        var loadspmconnect = new SPM_Connect();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                    else if (checkmaintenance() && Checkdeveloper())
                    {
                        var loadspmconnect = new SPM_Connect();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "SPM Connect is under maintenance. Cannot start the application. Please check back soon. Sorry for the inconvenience.", "System Under Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.ExitThread();
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "User name " + userName + " does not exists. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    System.Environment.Exit(0);
                }
            }
        }

        private bool checkmaintenance()
        {
            bool maintenance = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'Maintenance'", cn))
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
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect Error connecting to server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            if (limit == "1")
            {
                maintenance = true;
            }
            return maintenance;

        }

        public bool Checkdeveloper()
        {
            bool developer = false;
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
                        developer = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve developer rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            return developer;

        }

        private bool userexists(string username)
        {
            bool userpresent = false;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        userpresent = true;
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Check User Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();

                }
            }
            return userpresent;
        }
    }
}
