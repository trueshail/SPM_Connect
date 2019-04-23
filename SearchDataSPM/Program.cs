using SearchDataSPM.Admin_developer;
using System;
using System.Windows.Forms;
namespace SearchDataSPM
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        // this is me testing the new github transfered ownsership
		static void Main()
		{
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SPM_ConnectHome());
        }

    }

}
