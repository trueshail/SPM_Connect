using System;

namespace SearchDataSPM
{
    public partial class ReleaseType : MetroFramework.Forms.MetroForm
    {
        public ReleaseType()
        {
            InitializeComponent();
        }

        public string ValueIWant { get; set; }

        private void JobType_Load(object sender, EventArgs e)
        {
            metroComboBox1.Focus();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ValueIWant = metroComboBox1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}