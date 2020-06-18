using SPMConnectAPI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SearchDataSPM
{
    public class SQLNotifier : IDisposable
    {
        public SQLNotifier()
        {
            SqlDependency.Start(
                ConnectHelper.ConnectConnectionString(),
                "SPMConnnectClientsQueue");
        }

        public EventHandler<SqlNotificationEventArgs> NewMessage { get; set; }

        public void RegisterDependency()
        {
            using (SqlConnection connection = new SqlConnection(ConnectHelper.ConnectConnectionString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT [Last Login], [Application Running], [User Name], [Computer Name], [Version] FROM [dbo].[Checkin] ", connection);

                    //Monitor the Service Broker, and get notified if there is change in the query result
                    SqlDependency dependency = new SqlDependency(command, "Service=SPMConnnectClientService;local database=SPM_Database", int.MaxValue);

                    //Fire event when message is arrived
                    dependency.OnChange += this.Dependency_OnChange;

                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    Leave leave = new Leave();
                    //    leave.ID = reader["Computer Name"].ToString();
                    //    leave.ApplicantName = reader["Application Running"].ToString();
                    //    leave.Status = reader["User Name"].ToString();

                    //    Console.WriteLine(string.Format("{0}\t{1}\t{2}", leave.ID, leave.ApplicantName, leave.Status));
                    //}
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(string.Format("Error: {0}", ex.Message));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Error: {0}", ex.Message));
                }
            }
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //var info = e.Info;
            //var source = e.Source;
            //var type = e.Type;
            //SqlDependency dependency = sender as SqlDependency;

            //dependency.OnChange -= new OnChangeEventHandler(Dependency_OnChange);
            //RegisterDependency(); //Re-register dependency is required after a notification is received everytime
            //Console.WriteLine(e.Info.ToString() + ": " + e.Type.ToString() + ": " + e.Source.ToString());
            ////Do whatever you like here after message arrive
            ////Can be calling WCF service or anything supported in C#
            ///
            MessageBox.Show(e.Info.ToString() + ": " + e.Type.ToString() + ": " + e.Source.ToString());
            SqlDependency dependency = (SqlDependency)sender;
            dependency.OnChange -= Dependency_OnChange;
            RegisterDependency();
        }

        #region IDisposable Members

        public void Dispose()
        {
            SqlDependency.Stop(ConnectHelper.ConnectConnectionString(), "SPMConnnectClientsQueue");
        }

        #endregion IDisposable Members
    }
}