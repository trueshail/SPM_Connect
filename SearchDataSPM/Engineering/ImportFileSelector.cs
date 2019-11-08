using System;
using System.Windows.Forms;

namespace SearchDataSPM.Engineering
{
    public partial class ImportFileSelector : Form
    {
        public ImportFileSelector()
        {
            InitializeComponent();
        }

        public string ValueIWant { get; set; }

        private void ImportFileSelector_Load(object sender, EventArgs e)
        {
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            ValueIWant = "STEP";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            ValueIWant = "IGES";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            ValueIWant = "PARASOLID";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}