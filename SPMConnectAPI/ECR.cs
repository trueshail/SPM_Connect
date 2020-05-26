using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SPMConnectAPI
{
    public class ECR : ConnectAPI
    {
        #region Settting up Connetion and Get User

        private readonly log4net.ILog log;

        public ECR()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Accessed ECR Class " + Getassyversionnumber());
        }

        #endregion Settting up Connetion and Get User

        #region Datatables to pull out values or records

        public string GetJobName(string jobno)
        {
            string jobname = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[SPMJobs] WHERE [Job]='" + jobno + "' ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    jobname = dr["Description"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve job name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return jobname;
        }

        public string GetSAName(string sano)
        {
            string subassyname = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Inventory] WHERE [ItemNumber] = '" + sano + "'";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    subassyname = dr["Description"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Unable to retrieve sub assy name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return subassyname;
        }

        public DataTable ShowAllECRInvoices()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT *,CONCAT([ECRNo], ' ',[JobNo],' ',[JobName],' ',[SANo],' ',[SAName],' ',RequestedBy) AS FullSearch FROM [SPM_Database].[dbo].[ECR]", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Show all shipping Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GetECRInfo(string invoicenumber)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ECR] WHERE ECRNo = '" + invoicenumber + "'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    dt.Clear();
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Get ECR Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error(ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        #endregion Datatables to pull out values or records

        #region Generating New Ids

        private string GetnewECRNo()
        {
            string newincoiveno = "";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MAX([ECRNo]) + 1 as NextQuoteNo FROM [SPM_Database].[dbo].[ECR]";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    newincoiveno = dr["NextQuoteNo"].ToString();
                }
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Get New ECR Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

            if (string.IsNullOrEmpty(newincoiveno))
            {
                newincoiveno = "1001";
            }

            return newincoiveno;
        }

        #endregion Generating New Ids

        #region FillComboBoxes

        public AutoCompleteStringCollection FillDepartments()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Departments] from [dbo].[Departments] where[Departments] is not null order by [Departments]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Departments To Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRApprovedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ApprovedBy] from [dbo].[ECR] where ApprovedBy is not null order by ApprovedBy", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Approved By Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRCompletedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [CompletedBy] from [dbo].[ECR] where [CompletedBy] is not null order by [CompletedBy]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Completed By Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRDeptRequested()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Department] from [dbo].[ECR] where Department is not null order by Department", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Dept Requested Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRJobNumber()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [JobNo] from [dbo].[ECR] where JobNo is not null order by JobNo", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill JobNo Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRProjectManagers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ProjectManager] from [dbo].[ECR] where [ProjectManager] is not null order by [ProjectManager]", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Porject Managers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRRequestedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [RequestedBy] from [dbo].[ECR] where RequestedBy is not null order by RequestedBy", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Requested by Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRStatus()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Status] from [dbo].[ECR] where Status is not null order by Status", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill ECR Status Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillECRSupervisors()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [SupApprovalBy] from [dbo].[ECR] where SupApprovalBy is not null order by SupApprovalBy", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Supervisors Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        #endregion FillComboBoxes

        #region Perfrom CRUD on invoice details and shipping items

        public string CreatenewECR(string empid)
        {
            string success = "";
            DateTime datecreated = DateTime.Now;
            string sqlFormattedDatetime = datecreated.ToString("yyyy-MM-dd HH:mm:ss");
            string username = GetNameByEmpId(empid);
            string newinvoiceno = GetnewECRNo();

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[ECR] (ECRNo, DateCreated, RequestedBy, DateLastSaved, LastSavedBy, CreatedBy, Status) VALUES('" + newinvoiceno + "','" + sqlFormattedDatetime + "','" + username + "','" + sqlFormattedDatetime + "','" + username + "','" + username + "','ECR Initiated')";
                cmd.ExecuteNonQuery();
                cn.Close();
                success = newinvoiceno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Create Entry on ECR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        public bool UpdateECRDetsToSql(string typeofSave, string ecrno, string jobnumber,
            string subassyno, string partno, string jobname, string subassyname, string projectmanager,
            string requestedby, string department, string description, string notes, int supsubmit, int managersubmit, int ecrhandlersubmit, int completed, string ecrmanagerid,
            string ecrhandlerid, bool reject
            )
        {
            bool success = false;
            string username = ConnectUser.Name;
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            int supervisorid = ConnectUser.ECRSup;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (typeofSave == "Creator")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                   "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                   "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                   "[SANo] =  '" + subassyno + "',[Status] = 'ECR Initiated',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                   "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "Submitted")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                   "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                   "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                   "[SANo] =  '" + subassyno + "',[Status] = 'Submitted To Supervisor',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                   "[Submitted] = '" + supsubmit + "',[Submittedby] =  '" + username + "',[SubmittedOn] = '" + sqlFormattedDate + "',[SupervisorId] = '" + supervisorid + "'," +
                   "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "SubmittedFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                   "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                   "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                   "[SANo] =  '" + subassyno + "',[Status] = 'Req Supervisor Approval',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                   "[Submitted] = '" + supsubmit + "',[Submittedby] =  ' ',[SubmittedOn] = ' ',[SupervisorId] = ' '," +
                   "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "Supervisor")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                   "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                   "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                   "[SANo] =  '" + subassyno + "',[Status] = 'Supervisor Acknowledged',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                   "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "SupSubmit")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                   "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                   "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                   "[SANo] =  '" + subassyno + "',[Status] = 'Submitted To ECR Manager',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                   "[SupApproval] = '" + managersubmit + "',[SupApprovalBy] =  '" + username + "',[SupApprovedOn] = '" + sqlFormattedDate + "',[SubmitToId] = '" + ecrmanagerid + "'," +
                   "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "SupSubmitFalse")
                {
                    if (reject)
                    {
                        cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                       "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                       "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                       "[SANo] =  '" + subassyno + "',[Status] = 'Req ECR Manager Approval',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                       "[SupApproval] = '" + managersubmit + "',[SupApprovalBy] =  '" + username + "',[SupApprovedOn] = '" + sqlFormattedDate + "',[SubmitToId] = ' '," +
                       "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                       "[LastSavedBy] = '" + username + "',[JobNo] = '" + jobnumber + "',[ProjectManager] = '" + projectmanager + "'," +
                       "[RequestedBy] = '" + requestedby + "',[Department] = '" + department + "',[Description] = '" + description + "'," +
                       "[SANo] =  '" + subassyno + "',[Status] = 'Req ECR Manager Approval',[PartNo] = '" + partno + "',[JobName] = '" + jobname + "',[Comments] = '" + notes + "'," +
                       "[SupApproval] = '" + managersubmit + "',[SupApprovalBy] =  ' ',[SupApprovedOn] = ' ',[SubmitToId] = ' '," +
                       "[SAName] = '" + subassyname + "' WHERE [ECRNo] = '" + ecrno + "' ";
                    }
                }
                else if (typeofSave == "Manager")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                   "[LastSavedBy] = '" + username + "',[Status] = 'ECR Manager Acknowledged'," +
                   "[Comments] = '" + notes + "'" +
                   " WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "ManagerApproved")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                    "[LastSavedBy] = '" + username + "'," +
                    "[Comments] = '" + notes + "',[Status] = 'Assigned for changes'," +
                    "[Approved] = '" + ecrhandlersubmit + "',[ApprovedBy] =  '" + username + "',[ApprovedOn] = '" + sqlFormattedDate + "',[AssignedTo] = '" + ecrhandlerid + " '" +
                    " WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "ManagerApprovedFalse")
                {
                    if (reject)
                    {
                        cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                    "[LastSavedBy] = '" + username + "'," +
                    "[Comments] = '" + notes + "',[Status] = 'Not assigned'," +
                    "[Approved] = '" + ecrhandlersubmit + "',[ApprovedBy] =  '" + username + "',[ApprovedOn] = '" + sqlFormattedDate + "',[AssignedTo] = ' '" +
                    " WHERE [ECRNo] = '" + ecrno + "' ";
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                    "[LastSavedBy] = '" + username + "'," +
                    "[Comments] = '" + notes + "',[Status] = 'Not assigned'," +
                    "[Approved] = '" + ecrhandlersubmit + "',[ApprovedBy] =  ' ',[ApprovedOn] = ' ',[AssignedTo] = ' '" +
                    " WHERE [ECRNo] = '" + ecrno + "' ";
                    }
                }
                else if (typeofSave == "Handler")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                    "[LastSavedBy] = '" + username + "',[Status] = 'Being Processed'," +
                    "[Comments] = '" + notes + "'" +
                    " WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "Completed")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                    "[LastSavedBy] = '" + username + "',[Status] = 'ECR Completed'," +
                    "[Comments] = '" + notes + "'," +
                    "[Completed] = '" + completed + "',[CompletedBy] =  '" + username + "',[CompletedOn] = '" + sqlFormattedDate + "'" +
                    " WHERE [ECRNo] = '" + ecrno + "' ";
                }
                else if (typeofSave == "CompletedFalse")
                {
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[ECR] SET [DateLastSaved] = '" + sqlFormattedDate + "'," +
                    "[LastSavedBy] = '" + username + "',[Status] = 'ECR not complete'," +
                    "[Comments] = '" + notes + "'," +
                    "[Completed] = '" + completed + "',[CompletedBy] =  ' ',[CompletedOn] = ' '" +
                    " WHERE [ECRNo] = '" + ecrno + "' ";
                }
                cmd.ExecuteNonQuery();
                cn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect Invoice Details - Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            return success;
        }

        #endregion Perfrom CRUD on invoice details and shipping items
    }
}