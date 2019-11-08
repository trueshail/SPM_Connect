using System;

namespace SPMConnect.UserActionLog
{
    public interface ILog
    {
        void LogAction(DateTime timeStamp, string frmName, string ctrlName, string eventName, string value);

        void WriteLogActionsToSQL();
    }
}