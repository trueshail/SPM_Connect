using ExceptionReporting;
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
            //this is dev branch
            string appPath = Application.ExecutablePath;
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", appPath, "~ GDIDPISCALING DPIUNAWARE");
            ErrorHandler errorHandler = new ErrorHandler();
            log4net.ILog log;
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            Application.ThreadException += (sender, args) =>
            {
                log.Error(sender, args.Exception);
                errorHandler.EmailExceptionAndActionLogToSupport(sender, args.Exception);
                ExceptionReporter er = new ExceptionReporter
                {
                    Config =
                       {
                         AppName = "SPM Connect",
                         ShowFullDetail =false,
                         CompanyName = "SPM Automation",
                         TitleText = "SPM Connect Error Report",
                         EmailReportAddress = "shail@spm-automation.com",
                         TakeScreenshot = true,   // attached if sending email
                         SendMethod = ReportSendMethod.SMTP,  // also WebService/SimpleMAPI
                         SmtpServer = "spmautomation-com0i.mail.protection.outlook.com",
                         SmtpPort = 25,
                         SmtpUseDefaultCredentials = true,
                         SmtpFromAddress = "connect@spm-automation.com",
                         ReportTemplateFormat = TemplateFormat.Markdown,
                         SmtpUseSsl = true,
                         UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                         RegionInfo = "Windsor",
                         ShowAssembliesTab =false,
                         TopMost = true,
                       }
                };
                er.Show(args.Exception);
                er.Send();
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                    {
                        log.Error(sender, (Exception)args.ExceptionObject);
                        errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)args.ExceptionObject);
                        ExceptionReporter er = new ExceptionReporter
                        {
                            Config =
                       {
                         AppName = "SPM Connect",
                         ShowFullDetail =false,
                         CompanyName = "SPM Automation",
                         TitleText = "SPM Connect Error Report",
                         EmailReportAddress = "shail@spm-automation.com",
                         TakeScreenshot = true,   // attached if sending email
                         SendMethod = ReportSendMethod.SMTP,  // also WebService/SimpleMAPI
                         SmtpServer = "spmautomation-com0i.mail.protection.outlook.com",
                         SmtpPort = 25,
                         SmtpUseDefaultCredentials = true,
                         SmtpFromAddress = "connect@spm-automation.com",
                         ReportTemplateFormat = TemplateFormat.Markdown,
                         SmtpUseSsl = true,
                         UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                         RegionInfo = "Windsor",
                         ShowAssembliesTab =false,
                         TopMost = true,
                       }
                        };
                        er.Show((Exception)args.ExceptionObject);
                        er.Send();
                    };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SPMConnectHome());
        }
    }
}