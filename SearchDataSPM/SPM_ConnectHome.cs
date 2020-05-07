using SearchDataSPM.Engineering;
using SPMConnectAPI;
using System;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class SPMConnectHome : Form
    {
        private log4net.ILog log;
        private int time = 0;
        private ErrorHandler errorHandler = new ErrorHandler();
        private ConnectAPI connectapi = new ConnectAPI();

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
            string ca = ConnectAPI.ConnectUser.ActiveBlockNumber;
            if (ConnectAPI.ConnectUser.UserName != null || ConnectAPI.ConnectUser.UserName.Length > 0)
            {
                if (!Checkmaintenance())
                {
                    if (ConnectAPI.ConnectUser.Dept == ConnectAPI.Department.Accounting)
                    {
                        var loadspmconnect = new EFTHome();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                    else
                    {
                        var loadspmconnect = new SpmConnect();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                }
                else if (Checkmaintenance() && ConnectAPI.ConnectUser.Developer)
                {
                    if (ConnectAPI.ConnectUser.Dept == ConnectAPI.Department.Accounting)
                    {
                        var loadspmconnect = new EFTHome();
                        loadspmconnect.Closed += (s, args) => this.Close();
                        loadspmconnect.Show();
                    }
                    else
                    {
                        var loadspmconnect = new SpmConnect();
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
            string limit = connectapi.GetConnectParameterValue("Maintenance");
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