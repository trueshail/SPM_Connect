using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM.General
{
    public partial class PODetails : Form
    {
        public PODetails()
        {
            InitializeComponent();
        }
        public string ValueIWant { get; set; }
        public string podate { get; set; }

        private void ImportFileSelector_Load(object sender, EventArgs e)
        {

        }

      

        private void savebttn_Click(object sender, EventArgs e)
        {

            ValueIWant = ponumbertxt.Text.Trim();
            podate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void ponumbertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
