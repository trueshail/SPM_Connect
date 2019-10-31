using SPMConnect.UserActionLog;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM.Admin_developer
{
    public partial class UserLogs : Form
    {
        log4net.ILog log;
        private UserActions _userActions;
        ErrorHandler errorHandler = new ErrorHandler();

        public UserLogs()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            InitializeComponent();
        }

        private void UserLogs_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sPM_DatabaseDataSet1.UnionLogs' table. You can move, or remove it, as needed.
            this.unionLogsTableAdapter.Fill(this.sPM_DatabaseDataSet1.UnionLogs);
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened User Action Logs by " + System.Environment.UserName);
            _userActions = new UserActions(this);
        }

        private void advancedDataGridView_SortStringChanged(object sender, EventArgs e)
        {
            this.unionLogsBindingSource.Sort = this.advancedDataGridView1.SortString;
        }

        private void advancedDataGridView_FilterStringChanged(object sender, EventArgs e)
        {
            this.unionLogsBindingSource.Filter = this.advancedDataGridView1.FilterString;
        }

        private void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, t.Exception, _userActions, this);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            errorHandler.EmailExceptionAndActionLogToSupport(sender, (Exception)e.ExceptionObject, _userActions, this);
        }

        private void UserLogs_FormClosed(object sender, FormClosedEventArgs e)
        {
            _userActions.FinishLoggingUserActions(this);
            log.Info("Closed User Actions Log by " + System.Environment.UserName);
            this.Dispose();
        }
    }
}
