
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;



namespace SearchDataSPM
{

    public partial class SPM_ConnectLoad : Form

    {
        #region SPM Connect Load

        String connection;
        SqlConnection cn;
        string userName;

        public SPM_ConnectLoad()

        {
            InitializeComponent();
            connection = System.Configuration.ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;
            try
            {
                cn = new SqlConnection(connection);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         
            this.SendToBack();
            this.ShowInTaskbar = false;
            this.Visible = false;
            checkadmin();
            
        }

        private void SPM_Connect_Load(object sender, EventArgs e)
        {
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Userlabel.Text = userName.ToString();
            Userlabel.Visible = true;
            Connect_SPMSQL();

            // userName = userName.Substring(4);
        }

        private void Connect_SPMSQL()

        {
            try
            {
                cn = new SqlConnection(connection);

               chekusercredentialscontrols();

            }
            catch (Exception)
            {

                MessageBox.Show("User does not exists in the system.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
            finally
            {
                cn.Close();
            }
        }

        private void chekusercredentialscontrols()
        {

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Department = 'Controls'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", userName);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        
                        SPM_ConnectControls sPM_Connect = new SPM_ConnectControls();
                        
                        sPM_Connect.ShowDialog();
                        

                        //insert to autocad catalog

                    }
                    else
                    {
                        cn.Close();
                        chekusercredentialseng();
                 
                    }
                }
                catch(Exception)
                {
                   // MessageBox.Show("Not Licensed User", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                }
                finally
                {
                    cn.Close();
                }
                

            }

        }

        private void chekusercredentialseng()
        {

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Department = 'Eng'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", userName);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        SPM_Connect sPM_Connect = new SPM_Connect();
                      
                        sPM_Connect.ShowDialog();
                        
                    }
                    else
                    {
                        
                        MessageBox.Show("User not found. Please contact the administrator.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.ExitThread();
                    }
                   
                }
                catch (Exception)
                {
                    //MessageBox.Show("Not Licensed User", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void checkadmin()
        {
            string useradmin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM [SPM_Database].[dbo].[USers] WHERE UserName = @username AND Admin = '1'", cn))
            {
                try
                {
                    cn.Open();
                    sqlCommand.Parameters.AddWithValue("@username", useradmin);

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount == 1)
                    {
                        admin_bttn.Visible = true;

                    }
                    else
                    {

                        admin_bttn.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void admin_bttn_Click(object sender, EventArgs e)
        {
            spmadmin admin = new spmadmin();
            admin.ShowDialog();
        }

        #endregion

    }
}

