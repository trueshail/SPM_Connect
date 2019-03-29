using SPMConnectAPI;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class HelpForm : Form
    {
        SPMSQLCommands connectapi = new SPMSQLCommands();

        public HelpForm()
        {
            InitializeComponent();            
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = assembly.GetName().Version.ToString();
            versionlbl.Text = string.Format("SPM Connect Version - {0}", version);
        }

        private void shrtcutbttn_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"\\spm-adfs\SDBASE\SPM Connect SQL\ConnectHotKeys.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                  
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/spmconnect/SPM_Connect");
        }

        string file = "";

        private void importfilename()
        {
            file = "";
            openFileDialog1.FileName = "";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                file = openFileDialog1.FileName;
                
            }
        }

        private void browsebttn_Click(object sender, EventArgs e)
        {
           importfilename();

            if (file.Length > 0)
            {
                label5.Text = "File attached : " + file;
                browsebttn.Visible = false;
            }
            else
            {
                label5.Text = "Attach file : ";
                browsebttn.Visible = true;
            }
        }

        private void clearall()
        {
            nametxt.Clear();
            subtxt.Clear();
            notestxt.Clear();
            label5.Text = "Attach file : ";
            browsebttn.Visible = true;
        }

        private void sendemailbttn_Click(object sender, EventArgs e)
        {
            sendemailtodevelopers(nametxt.Text, file, subtxt.Text, notestxt.Text);
            clearall();
            MessageBox.Show("Email successfully sent to developer.", "SPM Connect - Developer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void sendemailtodevelopers(string requser, string fileName , string subject, string notes)
        {
            //connectapi.SPM_Connect();
            string[] nameemail = connectapi.getdevelopersnamesandemail().ToArray();
            for (int i = 0; i < nameemail.Length; i++)
            {
                string[] values = nameemail[i].Replace("][", "~").Split('~');

                for (int a = 0; a < values.Length; a++)
                {
                    values[a] = values[a].Trim();

                }
                string email = values[0];
                string name = values[1];

                string[] names = name.Replace(" ", "~").Split('~');
                for (int b = 0; b < names.Length; b++)
                {
                    names[b] = names[b].Trim();

                }
                name = names[0];
                connectapi.sendemail(email,"Connect Error Occured - " + subject, "Hello " + name + "," + Environment.NewLine + requser + " sent this error report." + Environment.NewLine + notes + Environment.NewLine + "Triggered by "+connectapi.UserName(), fileName,"");
            }

        }

        private void nametxt_TextChanged(object sender, EventArgs e)
        {
            if (nametxt.Text.Length > 0 && notestxt.Text.Length>0)
            {
                sendemailbttn.Enabled = true;
            }
            else
            {
                sendemailbttn.Enabled = false;
            }
        }

        private void notestxt_TextChanged(object sender, EventArgs e)
        {
            if (notestxt.Text.Length > 0 && nametxt.Text.Length>0)
            {
                sendemailbttn.Enabled = true;
            }
            else
            {
                sendemailbttn.Enabled = false;
            }
        }
    }
}
