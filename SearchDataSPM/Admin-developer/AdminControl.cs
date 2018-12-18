using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SearchDataSPM
{

    public partial class spmadmin : Form

    {
        #region steupvariables
        String connection;
        SqlConnection cn;
        string controluseraction;

        #endregion

        #region loadtree

        public spmadmin()

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
            finally
            {
                cn.Close();
            }


        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            fillsupervisor();
            Connect_SPMSQL();
        }

        private void Connect_SPMSQL()

        {
            try
            {
                Userlistbox.Items.Clear();
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Name FROM [SPM_Database].[dbo].[Users]";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Userlistbox.Items.Add(dr["Name"].ToString());
                }
                if (Userlistbox.Items.Count > 0)
                    Userlistbox.SelectedItem = Userlistbox.Items[0];
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


        private void fillsupervisor()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT CONCAT(id, ' ', Name) as Supervisors  FROM [SPM_Database].[dbo].[Users]  WHERE PurchaseReqApproval = '1'", cn))
            {
                try
                {
                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        MyCollection.Add(reader.GetString(0));
                    }
                    supervisorcombox.AutoCompleteCustomSource = MyCollection;
                    supervisorcombox.DataSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill supervisor items Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        #endregion

        private void Userlistbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] where Name ='" + Userlistbox.SelectedItem.ToString() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                string controls = "Controls";
                foreach (DataRow dr in dt.Rows)
                {
                    nametextbox.Text = dr["Name"].ToString();
                    domaintxtbox.Text = dr["UserName"].ToString();
                    activecadblocktxt.Text = dr["ActiveBlockNumber"].ToString();
                    useremailtxt.Text = dr["Email"].ToString();
                    idlabel.Text = "Id : " + dr["id"].ToString();

                    if (dr["Supervisor"].ToString().Length > 0)
                    {
                        string MyString = dr["Supervisor"].ToString();
                        MyString += " ";
                        MyString += getuserfullname(dr["Supervisor"].ToString());
                        supervisorcombox.SelectedItem = MyString;
                    }
                    else
                    {

                    }
                    if (dr["Department"].ToString().Equals("Eng"))
                    {
                        engradio.Checked = true;

                    }

                    else if (dr["Department"].ToString().Equals(controls.ToString()))
                    {
                        radioButton4.Checked = true;
                    }
                    else
                    {
                        radioButton3.Checked = true;
                    }

                    if (dr["Admin"].ToString().Equals("1"))
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    if (dr["Developer"].ToString().Equals("1"))
                    {
                        DevradioButtonYes.Checked = true;
                    }
                    else
                    {
                        DevradioButtonNo.Checked = true;
                    }
                    if (dr["Management"].ToString().Equals("1"))
                    {
                        manageradioButtonyes.Checked = true;
                    }
                    else
                    {
                        manageradioButtonNo.Checked = true;
                    }
                    if (dr["Quote"].ToString().Equals("1"))
                    {
                        quoteyes.Checked = true;
                    }
                    else
                    {
                        quoteno.Checked = true;
                    }
                    if (dr["PurchaseReqApproval"].ToString().Equals("1"))
                    {
                        reqyes.Checked = true;
                    }
                    else
                    {
                        reqno.Checked = true;
                    }
                    if (dr["PurchaseReqBuyer"].ToString().Equals("1"))
                    {
                        reqbuyeryes.Checked = true;
                    }
                    else
                    {
                        reqbuyerno.Checked = true;
                    }

                    
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

        private string getuserfullname(string supervisor)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [id]='" + supervisor.ToString() + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string fullname = dr["Name"].ToString();
                    return fullname;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "SPM Connect - Get Full Supervisor Name", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        private void addnewbttn_Click(object sender, EventArgs e)
        {
            nametextbox.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            domaintxtbox.ReadOnly = false;
            delbttn.Visible = false;
            updatebttn.Visible = false;
            updatesavebttn.Visible = true;
            domaintxtbox.Text = @"SPM\";
            activecadblocktxt.Text = "";
            useremailtxt.Text = "";
            idlabel.Text = "";
            engradio.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton4.Enabled = true;
            radioButton3.Enabled = true;
            manageradioButtonyes.Enabled = true;
            manageradioButtonNo.Enabled = true;
            DevradioButtonYes.Enabled = true;
            DevradioButtonNo.Enabled = true;
            quoteyes.Enabled = true;
            quoteno.Enabled = true;
            reqyes.Enabled = true;
            reqno.Enabled = true;
            reqbuyeryes.Enabled = true;
            reqbuyerno.Enabled = true;
            supervisorcombox.Enabled = true;
            nametextbox.Text = "";
            cnclbttn.Visible = true;
            addnewbttn.Visible = false;
            nametextbox.Focus();
            nametextbox.SelectAll();
            controluseraction = "";
            controluseraction = "addnew";
            Userlistbox.Enabled = false;
            button1.Enabled = false;
            reluanchbttn.Enabled = false;
            custbttn.Enabled = false;
            matbttn.Enabled = false;
            UserStats.Enabled = false;
        }

        private void delbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Name = " + nametextbox.Text + Environment.NewLine +
               @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
               @"Email = " + useremailtxt.Text + Environment.NewLine +
               "Department = " + (engradio.Checked ? "Engineering" : "Controls") + Environment.NewLine +
               "Admin = " + (radioButton1.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (DevradioButtonYes.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quoteyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (reqyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (reqbuyeryes.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString() + Environment.NewLine +
               "Management = " + (manageradioButtonyes.Checked ? "Yes" : "No"), "Delete User?",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                try
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [SPM_Database].[dbo].[Users] WHERE UserName = '" + domaintxtbox.Text.ToString() + "'";
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("User deleted successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Connect_SPMSQL();
                    domaintxtbox.Text = "";
                    nametextbox.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton4.Checked = false;
                    engradio.Checked = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void updatebttn_Click(object sender, EventArgs e)
        {
            nametextbox.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            engradio.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton4.Enabled = true;
            radioButton3.Enabled = true;
            manageradioButtonyes.Enabled = true;
            manageradioButtonNo.Enabled = true;
            DevradioButtonYes.Enabled = true;
            DevradioButtonNo.Enabled = true;
            quoteyes.Enabled = true;
            quoteno.Enabled = true;
            reqyes.Enabled = true;
            reqno.Enabled = true;
            reqbuyeryes.Enabled = true;
            reqbuyerno.Enabled = true;
            supervisorcombox.Enabled = true;
            delbttn.Visible = false;
            addnewbttn.Visible = false;
            updatesavebttn.Visible = true;
            cnclbttn.Visible = true;
            updatebttn.Visible = false;
            controluseraction = "";
            controluseraction = "update";
            Userlistbox.Enabled = false;
            button1.Enabled = false;
            reluanchbttn.Enabled = false;
            custbttn.Enabled = false;
            matbttn.Enabled = false;
            UserStats.Enabled = false;
        }

        private void updatesavebttn_Click(object sender, EventArgs e)
        {
            if (controluseraction == "update")
            {
                updateuser();
            }
            else
            {
                addnewuser();
            }
        }

        private void updateuser()
        {
            DialogResult result = MessageBox.Show(
               "Name = " + nametextbox.Text + Environment.NewLine +
               @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
               @"Email = " + useremailtxt.Text + Environment.NewLine +
               "Department = " + (engradio.Checked ? "Engineering" : "Controls") + Environment.NewLine +
               "Admin = " + (radioButton1.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (DevradioButtonYes.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quoteyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (reqyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (reqbuyeryes.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString() + Environment.NewLine +
               "Management = " + (manageradioButtonyes.Checked ? "Yes" : "No"), "Update User Information?",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string activeblocknumber = "";

                if (activecadblocktxt.Text.Length > 0)
                {
                    activeblocknumber = Char.ToUpper(activecadblocktxt.Text[0]) + activecadblocktxt.Text.Substring(1);
                }

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                try
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Users] SET Department = '" + (engradio.Checked ? "Eng" : radioButton4.Checked ? "Controls" : "Production") + "',Admin = '" + (radioButton1.Checked ? "1" : "0") + "',Name = '" + nametextbox.Text + "',ActiveBlockNumber = '" + activeblocknumber + "',Developer = '" + (DevradioButtonYes.Checked ? "1" : "0") + "',Management = '" + (manageradioButtonyes.Checked ? "1" : "0") + "',Quote = '" + (quoteyes.Checked ? "1" : "0") + "',PurchaseReqApproval = '" + (reqyes.Checked ? "1" : "0") + "',PurchaseReqBuyer = '" + (reqbuyeryes.Checked ? "1" : "0") + "',Supervisor = '" + supervisorcombox.SelectedItem.ToString().Substring(0,2) + "',Email = '" + useremailtxt.Text + "' WHERE UserName = '" + domaintxtbox.Text + "' ";

                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("User credentials updated successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Connect_SPMSQL();
                    updatesavebttn.Visible = false;
                    delbttn.Visible = true;
                    updatebttn.Visible = true;
                    addnewbttn.Visible = true;
                    cnclbttn.Visible = false;

                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton4.Enabled = false;
                    radioButton3.Enabled = false;
                    engradio.Enabled = false;
                    manageradioButtonyes.Enabled = false;
                    manageradioButtonNo.Enabled = false;
                    DevradioButtonYes.Enabled = false;
                    DevradioButtonNo.Enabled = false;
                    quoteyes.Enabled = false;
                    quoteno.Enabled = false;
                    reqyes.Enabled = false;
                    reqno.Enabled = false;
                    reqbuyeryes.Enabled = false;
                    reqbuyerno.Enabled = false;
                    supervisorcombox.Enabled = false;
                    nametextbox.ReadOnly = true;
                    activecadblocktxt.ReadOnly = true;
                    domaintxtbox.ReadOnly = true;
                    Userlistbox.Enabled = true;
                    useremailtxt.ReadOnly = true;
                    button1.Enabled = true;
                    reluanchbttn.Enabled = true;
                    custbttn.Enabled = true;
                    matbttn.Enabled = true;
                    UserStats.Enabled = true;
                    //Userlistbox_SelectedIndexChanged(sender,e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            else if (result == DialogResult.No)
            {
                domaintxtbox.Text = "";
                useremailtxt.Text = "";
                idlabel.Text = "";
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton4.Enabled = false;
                engradio.Enabled = false;
                manageradioButtonyes.Enabled = false;
                manageradioButtonNo.Enabled = false;
                DevradioButtonYes.Enabled = false;
                DevradioButtonNo.Enabled = false;
                quoteyes.Enabled = false;
                quoteno.Enabled = false;
                reqno.Enabled = false;
                reqyes.Enabled = false;
                reqbuyeryes.Enabled = false;
                reqbuyerno.Enabled = false;
                supervisorcombox.Enabled = false;
                nametextbox.Text = "";
                nametextbox.ReadOnly = true;
                activecadblocktxt.Text = "";
                activecadblocktxt.ReadOnly = true;
                useremailtxt.ReadOnly = true;
                domaintxtbox.ReadOnly = true;
                updatesavebttn.Visible = false;
                performcancelbutton();


            }

        }

        private void addnewuser()
        {
            DialogResult result = MessageBox.Show(
               "Name = " + nametextbox.Text + Environment.NewLine +
               @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
               @"Email = " + useremailtxt.Text + Environment.NewLine +
               "Department = " + (engradio.Checked ? "Engineering" : "Controls") + Environment.NewLine +
               "Admin = " + (radioButton1.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (DevradioButtonYes.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quoteyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (reqyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (reqbuyeryes.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString() + Environment.NewLine +
               "Management = " + (manageradioButtonyes.Checked ? "Yes" : "No"), "Add New User?",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string activeblocknumber = "";

                if (activecadblocktxt.Text.Length > 0)
                {
                    activeblocknumber = Char.ToUpper(activecadblocktxt.Text[0]) + activecadblocktxt.Text.Substring(1);
                }

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                try
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Users] VALUES('" + domaintxtbox.Text + "','" + (engradio.Checked ? "Eng" : radioButton4.Checked ? "Controls" : "Production") + "','" + (radioButton1.Checked ? "1" : "0") + "','" + nametextbox.Text + "','" + activeblocknumber + "','" + (DevradioButtonYes.Checked ? "1" : "0") + "','" + (manageradioButtonyes.Checked ? "1" : "0") + "','" + (quoteyes.Checked ? "1" : "0") + "','" + (reqyes.Checked ? "1" : "0") + "','" + (reqbuyeryes.Checked ? "1" : "0") + "','" + supervisorcombox.SelectedItem.ToString().Substring(0,2).TrimEnd() + "','" + useremailtxt.Text + "')";
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New user added successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Connect_SPMSQL();
                    delbttn.Visible = true;
                    updatebttn.Visible = true;
                    addnewbttn.Visible = true;
                    cnclbttn.Visible = false;
                    updatesavebttn.Visible = false;
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton4.Enabled = false;
                    radioButton3.Enabled = false;
                    engradio.Enabled = false;
                    manageradioButtonyes.Enabled = false;
                    manageradioButtonNo.Enabled = false;
                    DevradioButtonYes.Enabled = false;
                    DevradioButtonNo.Enabled = false;
                    quoteyes.Enabled = false;
                    quoteno.Enabled = false;
                    reqyes.Enabled = false;
                    reqno.Enabled = false;
                    reqbuyeryes.Enabled = false;
                    reqbuyerno.Enabled = false;
                    supervisorcombox.Enabled = false;
                    nametextbox.ReadOnly = true;
                    useremailtxt.ReadOnly = true;
                    activecadblocktxt.ReadOnly = true;
                    domaintxtbox.ReadOnly = true;
                    Userlistbox.Enabled = true;
                    button1.Enabled = true;
                    reluanchbttn.Enabled = true;
                    custbttn.Enabled = true;
                    matbttn.Enabled = true;
                    UserStats.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }
            else if (result == DialogResult.No)
            {
                domaintxtbox.Text = "";
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton4.Enabled = false;
                engradio.Enabled = false;
                manageradioButtonyes.Enabled = false;
                manageradioButtonNo.Enabled = false;
                DevradioButtonYes.Enabled = false;
                DevradioButtonNo.Enabled = false;
                quoteyes.Enabled = false;
                quoteno.Enabled = false;
                reqyes.Enabled = false;
                reqno.Enabled = false;
                reqbuyeryes.Enabled = false;
                reqbuyerno.Enabled = false;
                supervisorcombox.Enabled = false;
                nametextbox.Text = "";
                nametextbox.ReadOnly = true;
                useremailtxt.ReadOnly = true;
                activecadblocktxt.ReadOnly = true;
                domaintxtbox.ReadOnly = true;
                updatesavebttn.Visible = false;
                performcancelbutton();

            }
        }

        private void cnclbttn_Click(object sender, EventArgs e)
        {
            performcancelbutton();
        }

        private void performcancelbutton()
        {

            //domaintxtbox.Text = "";
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton4.Enabled = false;
            radioButton3.Enabled = false;
            engradio.Enabled = false;
            manageradioButtonyes.Enabled = false;
            manageradioButtonNo.Enabled = false;
            DevradioButtonYes.Enabled = false;
            DevradioButtonNo.Enabled = false;
            quoteyes.Enabled = false;
            quoteno.Enabled = false;
            reqyes.Enabled = false;
            reqno.Enabled = false;
            reqbuyeryes.Enabled = false;
            reqbuyerno.Enabled = false;
            supervisorcombox.Enabled = false;
            // nametextbox.Text = "";
            nametextbox.ReadOnly = true;
            activecadblocktxt.ReadOnly = true;
            useremailtxt.ReadOnly = true;
            domaintxtbox.ReadOnly = true;
            updatesavebttn.Visible = false;

            cnclbttn.Visible = false;
            addnewbttn.Visible = true;
            delbttn.Visible = true;
            updatebttn.Visible = true;
            Userlistbox.Enabled = true;
            button1.Enabled = true;
            reluanchbttn.Enabled = true;
            custbttn.Enabled = true;
            matbttn.Enabled = true;
            UserStats.Enabled = true;
            Userlistbox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SPM_ConnectDuplicates sPM_Connect = new SPM_ConnectDuplicates();
            sPM_Connect.ShowDialog();

        }

        private void SPM_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.spm-automation.com/");
        }

        private void reluanchbttn_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void UserStats_Click(object sender, EventArgs e)
        {
            Admin_developer.UserStatus userStatus = new Admin_developer.UserStatus();
            userStatus.ShowDialog();
        }

        private void activecadblocktxt_TextChanged(object sender, EventArgs e)
        {
            if (activecadblocktxt.Text.Length > 0)
            {
                if (activecadblocktxt.Text.Length > 3)
                {
                    activecadblocktxt.Clear();
                }
            }

        }

        private void activecadblocktxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (activecadblocktxt.Text.Length > 0)
            {
                if (Char.IsLetter(activecadblocktxt.Text[0]) == false)
                {
                    activecadblocktxt.Clear();
                }

            }
            if (activecadblocktxt.Text.Length > 1)
            {
                if (Char.IsLetter(activecadblocktxt.Text[1]))
                {
                    activecadblocktxt.Clear();
                }
            }
            if (activecadblocktxt.Text.Length > 2)
            {
                if (Char.IsLetter(activecadblocktxt.Text[2]))
                {
                    activecadblocktxt.Clear();
                }
            }

        }

        private void custbttn_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
        }

        private void matbttn_Click(object sender, EventArgs e)
        {
            Materials materials = new Materials();
            materials.Show();
        }

        private void spmadmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }

}
