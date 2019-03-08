using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class InvoiceFor : MetroFramework.Forms.MetroForm
    {
        public InvoiceFor()
        {
            InitializeComponent();
        }

        public string ValueIWant { get; set; }


        private void metroTile2_Click(object sender, EventArgs e)
        {
            ValueIWant = "Vendor";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            ValueIWant = "Customer";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void JobType_Load(object sender, EventArgs e)
        {
            label1.Focus();
        }
    }
}
