using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class spmadmin : Form
    {
        #region steupvariables

        string connection;
        SqlConnection cn;
        string controluseraction;
        int selectedindex = 0;
        DataTable dt;

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
          dt = new DataTable();
        }

        private void ParentView_Load(object sender, EventArgs e)
        {
            fillsupervisor();
           
            Connect_SPMSQL(0);
        }

        private void Connect_SPMSQL(int index)

        {
            try
            {
                Userlistbox.Items.Clear();
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Name FROM [SPM_Database].[dbo].[Users] order by Name";
                cmd.ExecuteNonQuery();
              
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt.Clear();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Userlistbox.Items.Add(dr["Name"].ToString());
                }
                if (Userlistbox.Items.Count > 0)
                {
                    Userlistbox.SelectedItem = Userlistbox.Items[index];
                }

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

        private void fillsupervisor()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT CONCAT(id, ' ', Name) as Supervisors  FROM [SPM_Database].[dbo].[Users]  WHERE PurchaseReqApproval = '1'  OR PurchaseReqApproval2 ='1'", cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
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

        #region Fillinfo

        private void Userlistbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectionchanged(selectedindex);
        }

        private void selectionchanged(int index)
        {
            try
            {

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] where Name ='" + Userlistbox.SelectedItem.ToString() + "'";
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    nametextbox.Text = dr["Name"].ToString();
                    domaintxtbox.Text = dr["UserName"].ToString();
                    activecadblocktxt.Text = dr["ActiveBlockNumber"].ToString();
                    useremailtxt.Text = dr["Email"].ToString();
                    sharepathtxt.Text = dr["SharesFolder"].ToString();
                    idlabel.Text = "Id : " + dr["id"].ToString();
                    empidtxt.Text =  dr["Emp_Id"].ToString();

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

                    if (dr["Department"].ToString().Length > 0)
                    {
                        string department = dr["Department"].ToString();

                        deptcombobox.SelectedItem = department;
                    }
                    else
                    {

                    }

                    if (dr["Admin"].ToString().Equals("1"))
                    {
                        admintoggle.Checked = true;
                    }
                    else
                    {
                        admintoggle.Checked = false;

                    }
                    if (dr["Developer"].ToString().Equals("1"))
                    {
                        developertoggle.Checked = true;
                    }
                    else
                    {
                        developertoggle.Checked = false;
                    }
                    if (dr["Management"].ToString().Equals("1"))
                    {
                        managementtoggle.Checked = true;
                    }
                    else
                    {
                        managementtoggle.Checked = false;
                    }
                    if (dr["Quote"].ToString().Equals("1"))
                    {
                        quotetoggle.Checked = true;
                    }
                    else
                    {
                        quotetoggle.Checked = false;
                    }
                    if (dr["PurchaseReqApproval"].ToString().Equals("1"))
                    {

                        papprovalchk.Checked = true;
                    }
                    else
                    {

                        papprovalchk.Checked = false;
                    }
                    if (dr["PurchaseReqBuyer"].ToString().Equals("1"))
                    {

                        pbuyerchk.Checked = true;
                    }
                    else
                    {

                        pbuyerchk.Checked = false;
                    }
                    if (dr["PurchaseReqApproval2"].ToString().Equals("1"))
                    {

                        papproval2chk.Checked = true;
                    }
                    else
                    {

                        papproval2chk.Checked = false;
                    }
                    if (dr["PurchaseReq"].ToString().Equals("1"))
                    {
                        purchasereqtoggle.Checked = true;
                    }
                    else
                    {
                        purchasereqtoggle.Checked = false;
                    }
                    if (dr["PriceRight"].ToString().Equals("1"))
                    {
                        pricetoggle.Checked = true;
                    }
                    else
                    {
                        pricetoggle.Checked = false;
                    }
                    if (dr["Shipping"].ToString().Equals("1"))
                    {
                        shiptoggle.Checked = true;
                    }
                    else
                    {
                        shiptoggle.Checked = false;
                    }

                    if (dr["CribCheckout"].ToString().Equals("1"))
                    {
                        cribouttoggle.Checked = true;
                    }
                    else
                    {
                        cribouttoggle.Checked = false;
                    }

                    if (dr["WOScan"].ToString().Equals("1"))
                    {
                        scanwotoggle.Checked = true;
                    }
                    else
                    {
                        scanwotoggle.Checked = false;
                    }

                    runalltoggle();


                }

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

        #endregion

        #region Perfrom CRUD

        private void addnewbttn_Click(object sender, EventArgs e)
        {
            selectedindex = Userlistbox.SelectedIndex;
            nametextbox.ReadOnly = false;
            empidtxt.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            //sharepathtxt.ReadOnly = false;
            selectfolder.Enabled = true;
            domaintxtbox.ReadOnly = false;
            delbttn.Visible = false;
            updatebttn.Visible = false;
            updatesavebttn.Visible = true;
            domaintxtbox.Text = @"SPM\";
            activecadblocktxt.Text = "";
            useremailtxt.Text = "";
            sharepathtxt.Text = @"\\SPM-ADFS\Shares\";
            idlabel.Text = "";
            empidtxt.Text = "";

            papprovalchk.Enabled = true;
            papproval2chk.Enabled = true;
            pbuyerchk.Enabled = true;           

            supervisorcombox.Enabled = true;
            deptcombobox.Enabled = true;
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
            
            papproval2chk.Checked = false;
            papprovalchk.Checked = false;
            pbuyerchk.Checked = false;
            enablealltoggles();
        }

        private void enablealltoggles()
        {
            admintoggle.Enabled = true;
            quotetoggle.Enabled = true;
            pricetoggle.Enabled = true;
            shiptoggle.Enabled = true;
            managementtoggle.Enabled = true;
            developertoggle.Enabled = true;
            cribouttoggle.Enabled = true;
            purchasereqtoggle.Enabled = true;
            scanwotoggle.Enabled = true;
        }

        private void disablealltoggles()
        {
            admintoggle.Enabled = false;
            quotetoggle.Enabled = false;
            pricetoggle.Enabled = false;
            shiptoggle.Enabled = false;
            managementtoggle.Enabled = false;
            developertoggle.Enabled = false;
            cribouttoggle.Enabled = false;
            purchasereqtoggle.Enabled = false;
            scanwotoggle.Enabled = false;
        }

        private void delbttn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                  "Name = " + nametextbox.Text + Environment.NewLine +
                  @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
                  @"Email = " + useremailtxt.Text + Environment.NewLine +
                  "Department = " + deptcombobox.SelectedItem.ToString() + Environment.NewLine +
                  "Admin = " + (admintoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "Developer = " + (developertoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "QuoteAccess = " + (quotetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "PurchaseReq Access = " + (purchasereqtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "Price Access = " + (pricetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "Shipping Access = " + (shiptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
                  "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
                  "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
                  "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
                  "CribCheckOut = " + (cribouttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "Scan WorkOrder = " + (scanwotoggle.Checked ? "Yes" : "No") + Environment.NewLine +
                  "Management = " + (managementtoggle.Checked ? "Yes" : "No"), "Update User Information?",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                selectedindex = Userlistbox.SelectedIndex;
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
                    
                    domaintxtbox.Text = "";
                    nametextbox.Text = "";

                    domaintxtbox.Text = @"SPM\";
                    activecadblocktxt.Text = "";
                    useremailtxt.Text = "";
                    sharepathtxt.Text = "";
                    idlabel.Text = "";
                    empidtxt.Text = "";

                    papprovalchk.Checked = false;
                    papproval2chk.Checked = false;
                    pbuyerchk.Checked = false;
                 

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                    Connect_SPMSQL(0);
                }

            }

        }

        private void updatebttn_Click(object sender, EventArgs e)
        {
            selectedindex = Userlistbox.SelectedIndex;
            nametextbox.ReadOnly = false;
            empidtxt.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            selectfolder.Enabled = true;
            papprovalchk.Enabled = true;
            papproval2chk.Enabled = true;
            pbuyerchk.Enabled = true;

            enablealltoggles();

            supervisorcombox.Enabled = true;
            deptcombobox.Enabled = true;
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
               "Department = " + deptcombobox.SelectedItem.ToString() + Environment.NewLine +
               "Admin = " + (admintoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (developertoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quotetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReq Access = " + (purchasereqtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Price Access = " + (pricetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Access = " + (shiptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
               "CribCheckOut = " + (cribouttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Sacn WorkOrder = " + (scanwotoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Management = " + (managementtoggle.Checked ? "Yes" : "No"), "Update User Information?",
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
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Users] SET Department = '" + deptcombobox.SelectedItem.ToString() + "'," +
                        "Admin = '" + (admintoggle.Checked ? "1" : "0") + "',Name = '" + nametextbox.Text.Trim() + "',ActiveBlockNumber = '" + activeblocknumber + "'," +
                        "Developer = '" + (developertoggle.Checked ? "1" : "0") + "',Management = '" + (managementtoggle.Checked ? "1" : "0") + "'," +
                        "Quote = '" + (quotetoggle.Checked ? "1" : "0") + "',PurchaseReq = '" + (purchasereqtoggle.Checked ? "1" : "0") + "'," +
                        "PurchaseReqApproval = '" + (papprovalchk.Checked ? "1" : "0") + "',PurchaseReqApproval2 = '" + (papproval2chk.Checked ? "1" : "0") + "'," +
                        "PurchaseReqBuyer = '" + (pbuyerchk.Checked ? "1" : "0") + "',Supervisor = '" + supervisorcombox.SelectedItem.ToString().Substring(0, 2) + "'," +
                        "Email = '" + useremailtxt.Text + "',PriceRight = '" + (pricetoggle.Checked ? "1" : "0") + "',Shipping = '" + (shiptoggle.Checked ? "1" : "0") + "'," +
                        "CribCheckout = '" + (cribouttoggle.Checked ? "1" : "0") + "',WOScan = '" + (scanwotoggle.Checked ? "1" : "0") + "',SharesFolder = '" + sharepathtxt.Text.Trim() + "'," +
                        "Emp_Id = '" + empidtxt.Text.Trim() + "' WHERE UserName = '" + domaintxtbox.Text + "' ";

                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("User credentials updated successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect - Update User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                    performcancelbutton();
                }

            }
            else if (result == DialogResult.No)
            {
                performcancelbutton();
            }

        }

        private void addnewuser()
        {
            DialogResult result = MessageBox.Show(
               "Name = " + nametextbox.Text + Environment.NewLine +
               @"Domain\Username = " + domaintxtbox.Text + Environment.NewLine +
               @"Email = " + useremailtxt.Text + Environment.NewLine +
               "Department = " + deptcombobox.SelectedItem.ToString() + Environment.NewLine +
               "Admin = " + (admintoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Developer = " + (developertoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "QuoteAccess = " + (quotetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReq Access = " + (purchasereqtoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Price Access = " + (pricetoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Shipping Access = " + (shiptoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
               "CribCheckOut = " + (cribouttoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Scan WorkOrder = " + (scanwotoggle.Checked ? "Yes" : "No") + Environment.NewLine +
               "Management = " + (managementtoggle.Checked ? "Yes" : "No"), "Update User Information?",
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
                    cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Users]([Emp_Id], [UserName], [Department], [Name],[ActiveBlockNumber],[Admin],[Developer],[Management]," +
                        "[Quote],[PurchaseReq],[PurchaseReqApproval],[PurchaseReqApproval2],[PurchaseReqBuyer],[PriceRight],[CribCheckout],[WOScan],[Shipping],[Supervisor],[Email],[SharesFolder]) " +
                        "VALUES('" + empidtxt.Text.Trim() + "','" + domaintxtbox.Text.Trim() + "','" + deptcombobox.SelectedItem.ToString() + "'," +
                        "'" + nametextbox.Text.Trim() + "','" + activeblocknumber + "','" + (admintoggle.Checked ? "1" : "0") + "','" + (developertoggle.Checked ? "1" : "0") + "'," +
                        "'" + (managementtoggle.Checked ? "1" : "0") + "','" + (quotetoggle.Checked ? "1" : "0") + "','" + (purchasereqtoggle.Checked ? "1" : "0") + "'," +
                        "'" + (papprovalchk.Checked ? "1" : "0") + "','" + (papproval2chk.Checked ? "1" : "0") + "','" + (pbuyerchk.Checked ? "1" : "0") + "'," +
                        "'" + (pricetoggle.Checked ? "1" : "0") + "','" + (cribouttoggle.Checked ? "1" : "0") + "','" + (scanwotoggle.Checked ? "1" : "0") + "','" + (shiptoggle.Checked ? "1" : "0") + "'," +
                        "'" + supervisorcombox.SelectedItem.ToString().Substring(0, 2).TrimEnd() + "','" + useremailtxt.Text + "','" + sharepathtxt.Text + "')";
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New user added successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                    performcancelbutton();                    
                }

            }
            else if (result == DialogResult.No)
            {
                performcancelbutton();

            }
        }

        private void cnclbttn_Click(object sender, EventArgs e)
        {
            performcancelbutton();          
        }

        private void performcancelbutton()
        {
            disablealltoggles();
            selectfolder.Enabled = false;
            domaintxtbox.Text = "";
            useremailtxt.Text = "";
            sharepathtxt.Text = "";
            nametextbox.Text = "";
            empidtxt.Text = "";
            activecadblocktxt.Text = "";
            papprovalchk.Enabled = false;
            papproval2chk.Enabled = false;
            pbuyerchk.Enabled = false;
            supervisorcombox.Enabled = false;
            deptcombobox.Enabled = false;
            nametextbox.ReadOnly = true;
            empidtxt.ReadOnly = true;
            activecadblocktxt.ReadOnly = true;
            useremailtxt.ReadOnly = true;
            selectfolder.Enabled = false;
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
            Connect_SPMSQL(selectedindex);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (papprovalchk.Checked)
            {
                pbuyerchk.Checked = false;
                papproval2chk.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (pbuyerchk.Checked)
            {
                papprovalchk.Checked = false;
                papproval2chk.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (papproval2chk.Checked)
            {
                pbuyerchk.Checked = false;
                //papprovalchk.Checked = false;
            }
        }

        private void selectfolder_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sharepathtxt.Text = Path.GetDirectoryName(openFileDialog1.FileName);

            }
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

        #endregion

        #region Button Click Events

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

        #endregion

        #region Toggle Events

        private void toggleSliderComponent1_CheckChanged(object sender, EventArgs e)
        {
            toggleadmin();
        }

        private void runalltoggle()
        {
            toggleadmin();
            togglecribout();
            togglequote();
            toggleprice();
            toggleship();
            togglemangement();
            toggledeveloper();
            togglepurchasereq();
            toggleScanwo();
        }

        private void toggleadmin()
        {
            if (admintoggle.Checked)
            {
                admintoggle.ToggleBarText = "Yes";
                admintoggle.ToggleCircleColor = Color.Green;
                admintoggle.ToggleColorBar = Color.White;
            }
            else
            {
                admintoggle.ToggleBarText = "No";
                admintoggle.ToggleCircleColor = Color.Red;
                admintoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void togglequote()
        {
            if (quotetoggle.Checked)
            {
                quotetoggle.ToggleBarText = "Yes";
                quotetoggle.ToggleCircleColor = Color.Green;
                quotetoggle.ToggleColorBar = Color.White;
            }
            else
            {
                quotetoggle.ToggleBarText = "No";
                quotetoggle.ToggleCircleColor = Color.Red;
                quotetoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void toggleprice()
        {
            if (pricetoggle.Checked)
            {
                pricetoggle.ToggleBarText = "Yes";
                pricetoggle.ToggleCircleColor = Color.Green;
                pricetoggle.ToggleColorBar = Color.White;
            }
            else
            {
                pricetoggle.ToggleBarText = "No";
                pricetoggle.ToggleCircleColor = Color.Red;
                pricetoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void toggleship()
        {
            if (shiptoggle.Checked)
            {
                shiptoggle.ToggleBarText = "Yes";
                shiptoggle.ToggleCircleColor = Color.Green;
                shiptoggle.ToggleColorBar = Color.White;
            }
            else
            {
                shiptoggle.ToggleBarText = "No";
                shiptoggle.ToggleCircleColor = Color.Red;
                shiptoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void togglemangement()
        {
            if (managementtoggle.Checked)
            {
                managementtoggle.ToggleBarText = "Yes";
                managementtoggle.ToggleCircleColor = Color.Green;
                managementtoggle.ToggleColorBar = Color.White;
            }
            else
            {
                managementtoggle.ToggleBarText = "No";
                managementtoggle.ToggleCircleColor = Color.Red;
                managementtoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void toggledeveloper()
        {
            if (developertoggle.Checked)
            {
                developertoggle.ToggleBarText = "Yes";
                developertoggle.ToggleCircleColor = Color.Green;
                developertoggle.ToggleColorBar = Color.White;
            }
            else
            {
                developertoggle.ToggleBarText = "No";
                developertoggle.ToggleCircleColor = Color.Red;
                developertoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void togglepurchasereq()
        {
            if (purchasereqtoggle.Checked)
            {
                purchasereqtoggle.ToggleBarText = "Yes";
                purchasereqtoggle.ToggleCircleColor = Color.Green;
                purchasereqtoggle.ToggleColorBar = Color.White;
            }
            else
            {
                purchasereqtoggle.ToggleBarText = "No";
                purchasereqtoggle.ToggleCircleColor = Color.Red;
                purchasereqtoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void togglecribout()
        {
            if (cribouttoggle.Checked)
            {
                cribouttoggle.ToggleBarText = "Yes";
                cribouttoggle.ToggleCircleColor = Color.Green;
                cribouttoggle.ToggleColorBar = Color.White;
            }
            else
            {
                cribouttoggle.ToggleBarText = "No";
                cribouttoggle.ToggleCircleColor = Color.Red;
                cribouttoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void toggleScanwo()
        {
            if (scanwotoggle.Checked)
            {
                scanwotoggle.ToggleBarText = "Yes";
                scanwotoggle.ToggleCircleColor = Color.Green;
                scanwotoggle.ToggleColorBar = Color.White;
            }
            else
            {
                scanwotoggle.ToggleBarText = "No";
                scanwotoggle.ToggleCircleColor = Color.Red;
                scanwotoggle.ToggleColorBar = Color.LightGray;
            }
        }

        private void quotetoggle_CheckChanged(object sender, EventArgs e)
        {
            togglequote();
        }

        private void pricetoggle_CheckChanged(object sender, EventArgs e)
        {
            toggleprice();
        }

        private void shiptoggle_CheckChanged(object sender, EventArgs e)
        {
            toggleship();
        }

        private void cribouttoggle_CheckChanged(object sender, EventArgs e)
        {
            togglecribout();
        }

        private void developertoggle_CheckChanged(object sender, EventArgs e)
        {
            toggledeveloper();
        }

        private void purchasereqtoggle_CheckChanged(object sender, EventArgs e)
        {
            togglepurchasereq();
        }

        private void managementtoggle_CheckChanged(object sender, EventArgs e)
        {
            togglemangement();
        }

        private void scanwotoggle_CheckChanged(object sender, EventArgs e)
        {
            toggleScanwo();
        }

        #endregion


    }
}
