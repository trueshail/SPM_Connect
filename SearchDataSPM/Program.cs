using Microsoft.Win32;
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
#if DEBUG

#else

            AddRegistry();
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
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WorkOrder.ReleaseManagement.ReleaseLog());
        }

        private static void AddRegistry()
        {
            string appPath = Application.ExecutablePath;
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", appPath, "~ GDIDPISCALING DPIUNAWARE");
            //string str2 = "\"" + appPath + "\",0";
            //RegistryKey icon = Registry.ClassesRoot.CreateSubKey("spmconnect", true);
            //Registry.ClassesRoot.CreateSubKey(@"spmconnect\DefaultIcon", true).SetValue("", str2);
            //icon.SetValue("", "URL:spmconnect");
            //icon.SetValue("URL Protocol", "");
            //Registry.ClassesRoot.CreateSubKey(@"spmconnect\shell", true).SetValue("", "open");
            //Registry.ClassesRoot.CreateSubKey(@"spmconnect\shell\open", true).SetValue("", "");
            //string str = "\"" + appPath + "\" ";
            //str += "\"%1\"";
            //Registry.ClassesRoot.CreateSubKey(@"spmconnect\shell\open\command", true).SetValue("", str);
        }
    }
}