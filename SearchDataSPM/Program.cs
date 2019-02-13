using SearchDataSPM.General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]


		static void Main()
		{
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SPM_ConnectHome());

            #region splash
            // Splash splash = new Splash();
            // splash.ShowSplashScreen();
            // while (!splash.Ready())
            // {
            // }
            // SPM_ConnectHome sPM_ConnectHome = new SPM_ConnectHome();

            // splash.UpdateProgress("Connecting to SQL Server...");
            // sPM_ConnectHome.Connect_SPMSQL();
            // Thread.Sleep(1000);

            //string  userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            // splash.UpdateProgress("Checking User Credentials...");

            // bool eng = sPM_ConnectHome.chekusercredentialseng(userName);
            // bool controls = sPM_ConnectHome.chekusercredentialscontrols(userName);
            // bool prod = sPM_ConnectHome.chekusercredentialsproduction(userName);
            // Thread.Sleep(2000);

            // splash.UpdateProgress("Setting up the module");
            // Thread.Sleep(2000);

            // splash.UpdateProgress("Opening Form");
            // Thread.Sleep(1000);
            // if (eng)
            // {

            //     //Application.Run(new SPM_Connect());
            //    // splash.CloseForm();
            //     var form2 = new SPM_Connect();
            //     form2.Closed += (s, args) => splash.CloseForm();
            //     form2.Show();
            // }
            // else if (controls)
            // {
            //     splash.CloseForm();
            //     Application.Run(new SPM_ConnectControls());
            // }
            // else if (prod)
            // {
            //     splash.CloseForm();
            //     Application.Run(new SPM_ConnectProduction());
            // }
            // else
            // {
            //     splash.CloseForm();
            //     MessageBox.Show("UserName " + userName + " is not a licensed user. Please contact the admin.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //     Application.ExitThread();
            //     System.Environment.Exit(0);
            // }

            #endregion

        }
    }

}
