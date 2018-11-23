using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM.General
{
    public partial class QuoteDetails : Form
    {
        String connection;
        DataTable dt = new DataTable();
        SqlConnection cn;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        string quoteno2 ="";

        public QuoteDetails()
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
            this.Text = "Quote Details - " + quoteno2;
            Fillcustomers();
            HowFound();
            Rating();
            Category();
            filldatatable(quoteno2);
        }

        private void filldatatable(string quotenumber)
        {
            String sql = "SELECT *  FROM [SPM_Database].[dbo].[Opportunities] WHERE [Quote]='" + quotenumber.ToString() + "'";
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                _adapter = new SqlDataAdapter(sql, cn);
                _adapter.Fill(dt);
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
            ItemTxtBox.Text = r["Quote"].ToString();
            Descriptiontxtbox.Text = r["Title"].ToString();           
            notestxt.Text = r["Comments"].ToString();
            textBox1.Text = r["Est_Revenue"].ToString();

            string quoteddate = r["Quote_Date"].ToString();
            string quotedby = r["Employee"].ToString();
            string lastsavedby = r["LastSavedby"].ToString();
            string lastsaved = r["Lastsaved"].ToString();

            quotedatelbl.Text = "Quote Date : " + quoteddate.Substring(0,10);
            Quotedbylbl.Text = "Quoted By : " + quotedby;
            Lsatsavedbylbl.Text = "Last Saved By : " + lastsavedby;
            Lastsavedlbl.Text = "Last Saved : " + lastsaved;

            string category = r["Category"].ToString();
            if (category.Length > 0)
            {
                Categorycombox.SelectedItem = category;
            }

            string customer = r["Customer"].ToString();
            if (customer.Length > 0)
            {
                familycombobox.SelectedItem = customer;
            }

            string rating = r["Rating"].ToString();
            if (rating.Length > 0)
            {
                Ratingcombobox.SelectedItem = rating;
            }

            string howfound = r["How_Found"].ToString();
            if (howfound.Length > 0)
            {
                Howfndcombox.SelectedItem = howfound;
            }

            string currency = r["Currency"].ToString();
            if (currency.Length > 0)
            {
                currencycombox.SelectedItem = currency;
            }



            if (r["Converted_to_Job"].ToString().Equals("1"))
            {
                cvttojobchkbox.Checked = true;

            }
            if (r["Closed"].ToString().Equals("1"))
            {
                closedchkbox.Checked = true;

            }


        }

        private void Fillcustomers()
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM [SPM_Database].[dbo].[Customers]", cn))
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
                    familycombobox.AutoCompleteCustomSource = MyCollection;
                    familycombobox.DataSource = MyCollection;

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
                    Howfndcombox.AutoCompleteCustomSource = MyCollection;
                    Howfndcombox.DataSource = MyCollection;

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
                    Ratingcombobox.AutoCompleteCustomSource = MyCollection;
                    Ratingcombobox.DataSource = MyCollection;

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
                    Categorycombox.AutoCompleteCustomSource = MyCollection;
                    Categorycombox.DataSource = MyCollection;

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = textBox1.Text.Replace(",", "")
                .Replace("$", "").Replace(".", "").TrimStart('0');
            decimal ul;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                textBox1.TextChanged -= textBox1_TextChanged;
                //Format the text as currency
                textBox1.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                textBox1.TextChanged += textBox1_TextChanged;
                textBox1.Select(textBox1.Text.Length, 0);
            }
            bool goodToGo = TextisValid(textBox1.Text);
           
        }

        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

    }
}
