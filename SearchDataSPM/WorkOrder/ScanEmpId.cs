using System;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ScanEmpId : MetroFramework.Forms.MetroForm
    {
        public ScanEmpId()
        {
            InitializeComponent();
        }

        public string ValueIWant { get; set; }

        private void JobType_Load(object sender, EventArgs e)
        {
            empid_txtbox.Focus();
        }

        private void empid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                ValueIWant = empid_txtbox.Text.Trim();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
           
        }
    }
}
