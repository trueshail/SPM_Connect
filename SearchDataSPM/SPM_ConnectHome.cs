using SearchDataSPM.Accounting;
using SearchDataSPM.Engineering;
using SearchDataSPM.Helper;
using System;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SearchDataSPM
{
    public partial class SPMConnectHome : Form
    {
        private readonly SPMConnectAPI.ConnectAPI connectapi = new SPMConnectAPI.ConnectAPI();
        private log4net.ILog log;
        private int time;

        public SPMConnectHome()
        {
            InitializeComponent();
        }

        public void Connect_SPMSQL()
        {
            ApplicationSettings.Load();
            if (connectapi.ConnectUser.UserName != null || connectapi.ConnectUser.UserName.Length > 0)
            {
                if (!Checkmaintenance())
                {
                    if (connectapi.ConnectUser.Dept == Department.Accounting)
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
                else if (Checkmaintenance() && connectapi.ConnectUser.Developer)
                {
                    if (connectapi.ConnectUser.Dept == Department.Accounting)
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
                    Environment.Exit(0);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "User name " + connectapi.ConnectUser.UserName + " does not exists. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                Environment.Exit(0);
            }
        }

        private bool Checkmaintenance()
        {
            bool maintenance = false;
            string limit = ApplicationSettings.GetConnectParameterValue("Maintenance");
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

        private void SPM_ConnectHome_Load(object sender, EventArgs e)
        {
            timer1.Start();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.GlobalContext.Properties["User"] = Environment.UserName;
            log.Info("Opened SPM Connect Home ");
            spmconnectlbl.Text = string.Format("SPM Connect {0}", Getassyversionnumber());
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            time += 10;
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
    }
}