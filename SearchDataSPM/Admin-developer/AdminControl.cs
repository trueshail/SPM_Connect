using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class spmadmin : Form
    {
        #region steupvariables
        String connection;
        SqlConnection cn;
        string controluseraction;
        int selectedindex = 0;

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
                {

                    Userlistbox.SelectedItem = Userlistbox.Items[index];
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
                    sharepathtxt.Text = dr["SharesFolder"].ToString();
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
                        preqyes.Checked = true;
                    }
                    else
                    {
                        preqno.Checked = true;
                    }
                    if (dr["PriceRight"].ToString().Equals("1"))
                    {
                        priceyes.Checked = true;
                    }
                    else
                    {
                        priceno.Checked = true;
                    }
                    if (dr["Shipping"].ToString().Equals("1"))
                    {
                        shipyes.Checked = true;
                    }
                    else
                    {
                        shipno.Checked = true;
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

        #endregion

        #region Perfrom CRUD

        private void addnewbttn_Click(object sender, EventArgs e)
        {
            selectedindex = Userlistbox.SelectedIndex;
            nametextbox.ReadOnly = false;
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
            papprovalchk.Enabled = true;
            papproval2chk.Enabled = true;
            pbuyerchk.Enabled = true;

            preqyes.Enabled = true;
            preqno.Enabled = true;
            priceyes.Enabled = true;
            priceno.Enabled = true;

            shipyes.Enabled = true;
            shipno.Enabled = true;

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

            radioButton2.Checked = true;
            manageradioButtonNo.Checked = true;
            preqno.Checked = true;
            DevradioButtonNo.Checked = true;
            priceno.Checked = true;
            quoteno.Checked = true;
            papproval2chk.Checked = false;
            papprovalchk.Checked = false;
            pbuyerchk.Checked = false;
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
                "PurchaseReq Access = " + (preqyes.Checked ? "Yes" : "No") + Environment.NewLine +
                "Price Access = " + (priceyes.Checked ? "Yes" : "No") + Environment.NewLine +
                 "Shipping Access = " + (shipyes.Checked ? "Yes" : "No") + Environment.NewLine +
                "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
                "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
                "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
                "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
                "Management = " + (manageradioButtonyes.Checked ? "Yes" : "No"), "Update User Information?",
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
                    Connect_SPMSQL(selectedindex);
                    domaintxtbox.Text = "";
                    nametextbox.Text = "";

                    domaintxtbox.Text = @"SPM\";
                    activecadblocktxt.Text = "";
                    useremailtxt.Text = "";
                    sharepathtxt.Text = "";
                    idlabel.Text = "";

                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton4.Checked = false;
                    engradio.Checked = false;

                    manageradioButtonyes.Checked = false;
                    manageradioButtonNo.Checked = false;
                    DevradioButtonYes.Checked = false;
                    DevradioButtonNo.Checked = false;
                    quoteyes.Checked = false;
                    quoteno.Checked = false;
                    papprovalchk.Checked = false;
                    papproval2chk.Checked = false;
                    pbuyerchk.Checked = false;

                    preqyes.Checked = false;
                    preqno.Checked = false;
                    priceyes.Checked = false;
                    priceno.Checked = false;
                    shipyes.Checked = false;
                    shipno.Checked = false;

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
            selectedindex = Userlistbox.SelectedIndex;
            nametextbox.ReadOnly = false;
            activecadblocktxt.ReadOnly = false;
            useremailtxt.ReadOnly = false;
            //sharepathtxt.ReadOnly = false;
            selectfolder.Enabled = true;
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
            papprovalchk.Enabled = true;
            papproval2chk.Enabled = true;
            pbuyerchk.Enabled = true;

            preqyes.Enabled = true;
            preqno.Enabled = true;
            priceyes.Enabled = true;
            priceno.Enabled = true;
            shipyes.Enabled = true;
            shipno.Enabled = true;

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
               "PurchaseReq Access = " + (preqyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "Price Access = " + (priceyes.Checked ? "Yes" : "No") + Environment.NewLine +
                "Shipping Access = " + (shipyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
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
                    cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Users] SET Department = '" + (engradio.Checked ? "Eng" : radioButton4.Checked ? "Controls" : "Production") + "',Admin = '" + (radioButton1.Checked ? "1" : "0") + "',Name = '" + nametextbox.Text + "',ActiveBlockNumber = '" + activeblocknumber + "',Developer = '" + (DevradioButtonYes.Checked ? "1" : "0") + "',Management = '" + (manageradioButtonyes.Checked ? "1" : "0") + "',Quote = '" + (quoteyes.Checked ? "1" : "0") + "',PurchaseReq = '" + (preqyes.Checked ? "1" : "0") + "',PurchaseReqApproval = '" + (papprovalchk.Checked ? "1" : "0") + "',PurchaseReqApproval2 = '" + (papproval2chk.Checked ? "1" : "0") + "',PurchaseReqBuyer = '" + (pbuyerchk.Checked ? "1" : "0") + "',Supervisor = '" + supervisorcombox.SelectedItem.ToString().Substring(0, 2) + "',Email = '" + useremailtxt.Text + "',PriceRight = '" + (priceyes.Checked ? "1" : "0") + "',Shipping = '" + (shipyes.Checked ? "1" : "0") + "',SharesFolder = '" + sharepathtxt.Text + "' WHERE UserName = '" + domaintxtbox.Text + "' ";

                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("User credentials updated successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Connect_SPMSQL(selectedindex);
                    updatesavebttn.Visible = false;
                    delbttn.Visible = true;
                    updatebttn.Visible = true;
                    addnewbttn.Visible = true;
                    cnclbttn.Visible = false;
                    selectfolder.Enabled = false;
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
                    papprovalchk.Enabled = false;
                    papproval2chk.Enabled = false;
                    pbuyerchk.Enabled = false;

                    preqyes.Enabled = false;
                    preqno.Enabled = false;
                    priceyes.Enabled = false;
                    priceno.Enabled = false;
                    shipyes.Enabled = false;
                    shipno.Enabled = false;

                    supervisorcombox.Enabled = false;
                    nametextbox.ReadOnly = true;
                    activecadblocktxt.ReadOnly = true;
                    domaintxtbox.ReadOnly = true;
                    Userlistbox.Enabled = true;
                    useremailtxt.ReadOnly = true;
                    //sharepathtxt.ReadOnly = true;
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
                sharepathtxt.Text = "";
                selectfolder.Enabled = false;
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
                papprovalchk.Enabled = false;
                papproval2chk.Enabled = false;
                pbuyerchk.Enabled = false;

                preqyes.Enabled = false;
                preqno.Enabled = false;
                priceyes.Enabled = false;
                priceno.Enabled = false;
                shipyes.Enabled = false;
                shipno.Enabled = false;


                supervisorcombox.Enabled = false;
                nametextbox.Text = "";
                nametextbox.ReadOnly = true;
                activecadblocktxt.Text = "";
                activecadblocktxt.ReadOnly = true;
                useremailtxt.ReadOnly = true;
                sharepathtxt.ReadOnly = true;
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
               "PurchaseReq Access = " + (preqyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "Price Access = " + (priceyes.Checked ? "Yes" : "No") + Environment.NewLine +
                "Shipping Access = " + (shipyes.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqAdmin = " + (papprovalchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqHigher Approval = " + (papproval2chk.Checked ? "Yes" : "No") + Environment.NewLine +
               "PurchaseReqBuyer = " + (pbuyerchk.Checked ? "Yes" : "No") + Environment.NewLine +
               "Supervisor = " + supervisorcombox.SelectedItem.ToString().Substring(2) + Environment.NewLine +
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
                    cmd.CommandText = "INSERT INTO [SPM_Database].[dbo].[Users] VALUES('" + domaintxtbox.Text + "','" + (engradio.Checked ? "Eng" : radioButton4.Checked ? "Controls" : "Production") + "','" + nametextbox.Text + "','" + activeblocknumber + "','" + (radioButton1.Checked ? "1" : "0") + "','" + (DevradioButtonYes.Checked ? "1" : "0") + "','" + (manageradioButtonyes.Checked ? "1" : "0") + "','" + (quoteyes.Checked ? "1" : "0") + "','" + (preqyes.Checked ? "1" : "0") + "','" + (papprovalchk.Checked ? "1" : "0") + "','" + (papproval2chk.Checked ? "1" : "0") + "','" + (pbuyerchk.Checked ? "1" : "0") + "','" + supervisorcombox.SelectedItem.ToString().Substring(0, 2).TrimEnd() + "','" + useremailtxt.Text + "','" + (priceyes.Checked ? "1" : "0") + "','" + (shipyes.Checked ? "1" : "0") + "','" + sharepathtxt.Text + "')";
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New user added successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Connect_SPMSQL(selectedindex);
                    delbttn.Visible = true;
                    updatebttn.Visible = true;
                    addnewbttn.Visible = true;
                    cnclbttn.Visible = false;
                    updatesavebttn.Visible = false;
                    selectfolder.Enabled = false;
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
                    papprovalchk.Enabled = false;
                    papproval2chk.Enabled = false;
                    pbuyerchk.Enabled = false;

                    preqyes.Enabled = false;
                    preqno.Enabled = false;
                    priceyes.Enabled = false;
                    priceno.Enabled = false;
                    shipyes.Enabled = false;
                    shipno.Enabled = false;


                    supervisorcombox.Enabled = false;
                    nametextbox.ReadOnly = true;
                    useremailtxt.ReadOnly = true;
                    //sharepathtxt.ReadOnly = true;
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
                papprovalchk.Enabled = false;
                papproval2chk.Enabled = false;
                pbuyerchk.Enabled = false;
                selectfolder.Enabled = false;
                preqyes.Enabled = false;
                preqno.Enabled = false;

                priceyes.Enabled = false;
                priceno.Enabled = false;
                shipyes.Enabled = false;
                shipno.Enabled = false;

                supervisorcombox.Enabled = false;
                nametextbox.Text = "";
                nametextbox.ReadOnly = true;
                useremailtxt.ReadOnly = true;
                sharepathtxt.ReadOnly = true;
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
            papprovalchk.Enabled = false;
            papproval2chk.Enabled = false;
            pbuyerchk.Enabled = false;

            preqyes.Enabled = false;
            preqno.Enabled = false;

            priceyes.Enabled = false;
            priceno.Enabled = false;
            shipyes.Enabled = false;
            shipno.Enabled = false;

            supervisorcombox.Enabled = false;
            // nametextbox.Text = "";
            nametextbox.ReadOnly = true;
            activecadblocktxt.ReadOnly = true;
            useremailtxt.ReadOnly = true;
            //sharepathtxt.ReadOnly = true;
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
            Userlistbox.SelectedIndex = 0;
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
    }
}
