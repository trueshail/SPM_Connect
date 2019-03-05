using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM.General
{
    public partial class InvoiceDetails : Form
    {
        String connection;
        DataTable dt = new DataTable();
        SqlConnection cn;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        string quoteno2 ="";

        public InvoiceDetails()
        {
            InitializeComponent();
            connection = ConfigurationManager.ConnectionStrings["SearchDataSPM.Properties.Settings.cn"].ConnectionString;

            try
            {
                cn = new SqlConnection(connection);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
            dt = new DataTable();
            _command = new SqlCommand();
        }

        public string quotenubmber(string quoteno)
        {
            if (quoteno.Length > 0)
                return quoteno2 = quoteno;
            return null;
        }

        private void QuoteDetails_Load(object sender, EventArgs e)
        {
            this.Text = "Invoice Details - " + quoteno2;
            
            Fillcustomers();
            HowFound();
            Rating();
            Category();
            
            //filldatatable(quoteno2);
            //processeditbutton();
            //Open the connection
            //cn.Open();
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter da = new SqlDataAdapter();
            //// Write the query
            //cmd.CommandText = "select * from PurchaseReqBase";
            //da.SelectCommand = cmd;
            //cmd.Connection = cn;
            //da.SelectCommand = cmd;
            ////create a dataset
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            ////Close the connection
            //cn.Close();
            //// Bind data to TextBox
            //textBox1.DataBindings.Add("Text", da, "JobNumber");
            //textBox2.DataBindings.Add("Text", da, "RequestedBy");
            //textBox3.DataBindings.Add("Text", da, "ReqNumber");
            dataRepeater1.Visible = true;
            //dataRepeater1.ItemTemplate.Show();
            //dataRepeater1.DataSource = ds.Tables[0];
            dataRepeater1.AddNew();
        }

        private void filldatatable(string quotenumber)
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[Opportunities]";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                _adapter = new SqlDataAdapter(sql, cn);
                dt.Clear();
                _adapter.Fill(dt);
                textBox1.DataBindings.Add("Text", _adapter, "Title");
                textBox2.DataBindings.Add("Text", _adapter, "Customer");
                textBox3.DataBindings.Add("Text", _adapter, "Employee");
                dataRepeater1.Visible = true;
                fillinfo();                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }
        }

        private void fillinfo()
        {
            DataRow r = dt.Rows[0];
            invoicetxtbox.Text = r["Quote"].ToString();
                 
            notestxt.Text = r["Comments"].ToString();
           

            string quoteddate = r["Quote_Date"].ToString();
            string quotedby = r["Employee"].ToString();
            string lastsavedby = r["LastSavedby"].ToString();
            string lastsaved = r["Lastsaved"].ToString();
            if (quoteddate.Length > 0)
            {
                invoicedatelblb.Text = "Quote Date : " + quoteddate.Substring(0, 10);
            }
           
            invoicebylbl.Text = "Quoted By : " + quotedby;
            Lastsavedbylbl.Text = "Last Saved By : " + lastsavedby;
            Lastsavedlbl.Text = "Last Saved : " + lastsaved;

            string category = r["Category"].ToString();
            if (category.Length > 0)
            {
                Carriercombox.SelectedItem = category;
            }

            string customer = r["Company_Name"].ToString();
            if (customer.Length > 0)
            {
                soldtocombobox.SelectedItem = customer;
            }

            string rating = r["Rating"].ToString();
            if (rating.Length > 0)
            {
                Saledpersoncombobox.SelectedItem = rating;
            }

            string howfound = r["How_Found"].ToString();
            if (howfound.Length > 0)
            {
                FOBPointcombox.SelectedItem = howfound;
            }

            string currency = r["Currency"].ToString();
            if (currency.Length > 0)
            {
                currencycombox.SelectedItem = currency;
            }


        }

        private void Fillcustomers()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SPM_Database].[dbo].[Customersmerged]", cn))
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

                    //familytxtbox.AutoCompleteCustomSource = MyCollection;
                    soldtocombobox.AutoCompleteCustomSource = MyCollection;
                    soldtocombobox.DataSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Customers Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void HowFound()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT [How Found] FROM [SPM_Database].[dbo].[QuoteFilters]  where [How Found] is not null ", cn))
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

                    //familytxtbox.AutoCompleteCustomSource = MyCollection;
                    FOBPointcombox.AutoCompleteCustomSource = MyCollection;
                    FOBPointcombox.DataSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill howfound Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void Rating()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT [Rating] FROM [SPM_Database].[dbo].[QuoteFilters]  WHERE [Rating] is not null ", cn))
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

                    //familytxtbox.AutoCompleteCustomSource = MyCollection;
                    Saledpersoncombobox.AutoCompleteCustomSource = MyCollection;
                    Saledpersoncombobox.DataSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Rating Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void Category()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Category FROM [SPM_Database].[dbo].[QuoteFilters]", cn))
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

                    //familytxtbox.AutoCompleteCustomSource = MyCollection;
                    Carriercombox.AutoCompleteCustomSource = MyCollection;
                    Carriercombox.DataSource = MyCollection;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SPM Connect New Item - Fill Customers Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
           
        }

       

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        private static String get_username()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (userName.Length > 0)
            {
                return userName;
            }
            else
            {
                return null;
            }

        }

        private string getuserfullname(string username)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [SPM_Database].[dbo].[Users] WHERE [UserName]='" + username.ToString() + "' ";
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

                MessageBox.Show(ex.Message, "SPM Connect - Get Full User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                cn.Close();
            }
            return null;
        }

        private void QuoteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (savbttn.Visible == true)
            {
                DialogResult result = MetroFramework.MetroMessageBox.Show(this,"Are you sure want to close without saving changes?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                }
                else
                {
                    e.Cancel = (result == DialogResult.No);
                }
            }
        }

        private void processeditbutton()
        {
            editbttn.Visible = false;
            savbttn.Enabled = true;
            savbttn.Visible = true;
            soldtocombobox.Enabled = true;
            Carriercombox.Enabled = true;
            Saledpersoncombobox.Enabled = true;
            FOBPointcombox.Enabled = true;
            currencycombox.Enabled = true;           
            notestxt.Enabled = true;
            
        }

        private void perfromlockdown()
        {
            editbttn.Visible = true;
          
            savbttn.Enabled = false;
            savbttn.Visible = false;
            soldtocombobox.Enabled = false;
            Carriercombox.Enabled = false;
            Saledpersoncombobox.Enabled = false;
            FOBPointcombox.Enabled = false;
            currencycombox.Enabled = false;
          
            notestxt.Enabled = false;
        }

        List<string> list = new List<string>();

        private void graballinfor()
        {
            list.Clear();
            string quote = invoicetxtbox.Text;
            string customer = soldtocombobox.Text;
            string category = Carriercombox.Text;
            string rating = Saledpersoncombobox.Text;
            string howfound = FOBPointcombox.Text;
            string currency = currencycombox.Text;
          
            string notes = notestxt.Text;
            list.Add(quote);
            list.Add(customer);
            list.Add(category);
            list.Add(rating);
            list.Add(howfound);
            list.Add(currency);
            list.Add(notes);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace("'", "''");
            }

        }

        private void savbttn_Click(object sender, EventArgs e)
        {
            perfromsavebttn(true);           
        }

        void perfromsavebttn(bool createfolder)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Enabled = false;
            perfromlockdown();
            graballinfor();
           
            string lastsavedby = getuserfullname(get_username().ToString()).ToString();
            createnewitemtosql(list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), list[4].ToString(), list[5].ToString(), list[6].ToString(), list[7].ToString(), list[8].ToString(), list[9].ToString(), list[10].ToString(), lastsavedby);
           
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
            filldatatable(list[0].ToString());
        }

        private void createnewitemtosql(string quote, string description, string customer, string category, string rating, string howfound, string quotedprice, string currency, string closed, string convertedtojob, string notes, string user)
        {
            DateTime dateedited = DateTime.Now;
            string sqlFormattedDate = dateedited.ToString("yyyy-MM-dd HH:mm:ss");
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE [SPM_Database].[dbo].[Opportunities] SET Title = '" + description + "',Company_Name = '" + customer + "',Category = '" + category + "',Rating = '" + rating + "',How_Found = '" + howfound + "',Est_Revenue = '" + quotedprice.ToString() + "',Currency = '" + currency + "',Closed = '" + closed + "',Converted_to_Job = '" + convertedtojob + "',Comments = '" + notes + "',LastSavedby = '" + user + "',LastSaved = @value2,FolderPath = '' WHERE Quote = '" + quote + "' ";

                cmd.Parameters.AddWithValue("@value2", sqlFormattedDate);
                cmd.ExecuteNonQuery();
                cn.Close();
                //MessageBox.Show("Item sucessfully saved SPM Connect Server.", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this,ex.Message, "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void editbttn_Click(object sender, EventArgs e)
        {
            processeditbutton();
        }

        private void collectchkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (collectchkbox.Checked)
            {
                prepaidchkbox.Checked = false;
            }
        }

        private void prepaidchkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (prepaidchkbox.Checked)
            {
                collectchkbox.Checked = false;
            }
        }

        private void dataRepeater1_CurrentItemIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
