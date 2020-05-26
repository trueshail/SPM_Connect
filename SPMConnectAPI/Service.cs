using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static SPMConnectAPI.ConnectHelper;

namespace SPMConnectAPI
{
    public class Service : ConnectAPI
    {
        #region Settting up Connetion and Get User

        private readonly log4net.ILog log;

        public Service()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Accessed Service Class " + Getassyversionnumber());
        }

        #endregion Settting up Connetion and Get User

        #region Datatables to pull out values or records

        public DataTable ShowAllServiceReports()
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [SPM_Database].[dbo].[ServiceReports]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Show all service reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return dt;
        }

        #endregion Datatables to pull out values or records

        #region FillComboBoxes

        public AutoCompleteStringCollection FillAuthorizedBy()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Authorizedby] from [dbo].[spservicereports] where Authorizedby is not null order by Authorizedby", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Authorized By Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillCustomers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Customer] from [dbo].[spservicereports] where Customer is not null order by Customer", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Customer Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Porject Managers source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return MyCollection;
        }

        public AutoCompleteStringCollection FillEquipment()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Equipment] from [dbo].[spservicereports] where Equipment is not null order by Equipment", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Equipment Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillProjectManagers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ProjectManager] from [dbo].[spservicereports] where ProjectManager is not null order by ProjectManager", cn))
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

        public AutoCompleteStringCollection FillRefJob()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [RefJob] from [dbo].[spservicereports] where [RefJob] is not null order by [RefJob]", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Reference Job Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
            return MyCollection;
        }

        public AutoCompleteStringCollection FillReportJobNumbers()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [ProjectNo] from [dbo].[spservicereports] where ProjectNo is not null order by ProjectNo", cn))
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

        public AutoCompleteStringCollection FillTechnicians()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT DISTINCT [Techname] from [dbo].[spservicereports] where Techname is not null order by Techname", cn))
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
                    MessageBox.Show(ex.Message, "SPM Connect - Fill Technician Names Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }

            return MyCollection;
        }

        #endregion FillComboBoxes
    }
}