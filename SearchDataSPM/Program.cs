using System;
using System.Windows.Forms;

namespace SearchDataSPM
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ErrorHandler errorHandler = new ErrorHandler();
            log4net.ILog log;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            Application.ThreadException += (sender, args) =>
            {
                log.Error(sender, args.Exception);
                errorHandler.EmailExceptionAndActionLogToSupport(sender, args.Exception);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                log.Error(sender, (Exception)args.ExceptionObject);
                errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)args.ExceptionObject);
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SPM_ConnectHome());
        }
    }
}