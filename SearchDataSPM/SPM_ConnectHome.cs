using SearchDataSPM.Engineering;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class SPMConnectHome : Form
    {
        private log4net.ILog log;
        private int time = 0;
        private ErrorHandler errorHandler = new ErrorHandler();
        private SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();

        public SPMConnectHome()
        {
            InitializeComponent();
        }

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
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.GlobalContext.Properties["User"] = System.Environment.UserName;
            log.Info("Opened SPM Connect Home ");
        }

        public void Connect_SPMSQL()
        {
            SPMConnectAPI.UserInfo user = connectapi.GetUserDetails();
            if (user.UserName != null || user.UserName.Length > 0)
            {
                if (!Checkmaintenance())
                {
                    if (user.Department == "Accounting")
                    {
                        var loadspmconnect = new EFTHome();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                    else
                    {
                        var loadspmconnect = new SpmConnect(user);
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                }
                else if (Checkmaintenance() && user.Developer)
                {
                    if (user.Department == "Accounting")
                    {
                        var loadspmconnect = new EFTHome();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                    else
                    {
                        var loadspmconnect = new SpmConnect(user);
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
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
                MetroFramework.MetroMessageBox.Show(this, "User name " + connectapi.GetUserName() + " does not exists. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                System.Environment.Exit(0);
            }
        }

        private bool Checkmaintenance()
        {
            bool maintenance = false;
            string limit = "";
            using (SqlCommand cmd = new SqlCommand("SELECT ParameterValue FROM [SPM_Database].[dbo].[ConnectParamaters] WHERE Parameter = 'Maintenance'", connectapi.cn))
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
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "SPM Connect Error connecting to server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectapi.cn.Close();
                }
            }
            if (limit == "1")
            {
                maintenance = true;
            }
            return maintenance;
        }

        private void SPM_ConnectHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed SPM Connect Home ");
            this.Dispose();
        }
    }
}