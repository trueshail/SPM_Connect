using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SPMConnect.UserActionLog
{
    public class Logger : ILog
    {
        #region "Private Member Variables"

        private const string ACTIONLOGFILEIDENTIFIER = "ActionLog_";
        private const int _maxNumerOfLogsInMemory = 512;
        private static List<string> _theUserActions = new List<string>();
        private static string _actionLoggerDirectory = string.Empty;

        #endregion "Private Member Variables"

        #region "Methods"

        public Logger(int maxNumerOfLogsInMemory = 512, string actionLoggerDirectory = "")
        {
            if (string.IsNullOrEmpty(actionLoggerDirectory))
            {
                DateTime datecreated = DateTime.Now;
                string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd");
                actionLoggerDirectory = @"\\spm-adfs\SDBASE\SPMConnectLogs\" + System.Environment.UserName + "\\" + sqlFormattedDatetime + "\\";
                if (!Directory.Exists(actionLoggerDirectory))
                {
                    Directory.CreateDirectory(actionLoggerDirectory);
                }
            }

            _actionLoggerDirectory = actionLoggerDirectory;
        }

        public void LogAction(DateTime timeStamp, string frmName, string ctrlName, string eventName, string value)
        {
            _theUserActions.Add(string.Format("{0},{1},{2},{3},{4},{5}", timeStamp.ToString("H:mm:ss"), frmName, ctrlName, eventName, value, System.Environment.UserName));
            if (_theUserActions.Count > _maxNumerOfLogsInMemory) WriteLogActionsToSQL();
        }

        public void WriteLogActionsToSQL()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("timeStamp", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("formName", typeof(string)));
            dt.Columns.Add(new DataColumn("ctrlName", typeof(string)));
            dt.Columns.Add(new DataColumn("eventName", typeof(string)));
            dt.Columns.Add(new DataColumn("value", typeof(string)));
            dt.Columns.Add(new DataColumn("UserName", typeof(string)));

            for (int i = 0; i < _theUserActions.Count; i++)
            {
                string[] values = _theUserActions[i].Split(',');
                for (int j = 0; j < values.Length; j++)
                {
                    values[j] = values[j].Trim();
                }
                dt.Rows.Add(new object[] { values[0], values[1], values[2], values[3], values[4], values[5] });
                values = null;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=spm-sql;Initial Catalog=SPM_Database;User ID=SPM_Agent;password=spm5445"))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("inset_user_actions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@mytable", SqlDbType.Structured)
                    {
                        Value = dt
                    };

                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                }
            }

            _theUserActions = new List<string>();
            dt.Clear();
        }

        #endregion "Methods"
    }
}