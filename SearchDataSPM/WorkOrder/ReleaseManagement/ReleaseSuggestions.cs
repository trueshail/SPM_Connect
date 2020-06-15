using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM.WorkOrder.ReleaseManagement
{
    public partial class ReleaseSuggestions : Form
    {
        private readonly SPMConnectAPI.WorkOrder connectapi = new SPMConnectAPI.WorkOrder();
        private DataTable dt;
        private string formlabel = "";
        private log4net.ILog log;

        public ReleaseSuggestions()
        {
            InitializeComponent();
        }

        public string ValueIWant { get; set; }

        public string formtext(string formname)
        {
            if (formname.Length > 0)
                return formlabel = formname;
            return "ECR Select available to user";
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string item;
            if (dataGridView.SelectedRows.Count == 1 || dataGridView.SelectedCells.Count == 1)
            {
                int selectedrowindex = dataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow slectedrow = dataGridView.Rows[selectedrowindex];
                item = Convert.ToString(slectedrow.Cells[0].Value);
                //MessageBox.Show(item);
            }
            else
            {
                item = "";
            }
            ValueIWant = item;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ECR_Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Closed ECR Users Available ");
            this.Dispose();
        }

        private void ECR_Users_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();
            this.Text = formlabel;
            dt = new DataTable();
            dt = connectapi.GrabReleaseSuggestions("21761", "50198", "A05831");
            dataGridView.DataSource = dt;
            _ = dt.DefaultView;
            dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Ascending);
            UpdateFont();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Opened ECR Users Available ");
            // Resume the layout logic
            this.ResumeLayout();
        }

        private void UpdateFont()
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }
    }
}