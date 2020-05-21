using System;
using System.Windows.Forms;

namespace SearchDataSPM.Purchasing
{
    public partial class InvoiceDateChange : Form
    {
        public InvoiceDateChange()
        {
            InitializeComponent();
        }

        public string podate { get; set; }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ImportFileSelector_Load(object sender, EventArgs e)
        {
        }

        private void PODetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dateTimePicker1.Text.Length == 0) e.Cancel = true;
        }

        private void savebttn_Click(object sender, EventArgs e)
        {
            podate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}