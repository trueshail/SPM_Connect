using SPMConnectAPI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class BinLog : Form
    {
        WorkOrder connectapi = new WorkOrder();
        bool formloading = false;
        DataTable workorderstatus = new DataTable();

        public BinLog()
        {
            InitializeComponent();
            connectapi.SPM_Connect();
        }

        private void PopulateWorkOrders()
        {
            wolistbox.Items.Clear();
            DataTable workorderlist = new DataTable();
            workorderlist = connectapi.ShowWoOnInOut();

            foreach (DataRow dr in workorderlist.Rows)
            {
                wolistbox.Items.Add(dr["WO"].ToString());
            }
            if (wolistbox.Items.Count > 0)
            {
                wolistbox.SelectedItem = wolistbox.Items[0];
            }

        }

        private void BinLog_Load(object sender, EventArgs e)
        {
            loadform();
        }

        private void loadform()
        {
            formloading = true;
            PopulateWorkOrders();
            FillDgDt();
            formloading = false;
            wolistbox.SelectedIndex = 0;
        }

        private void wolistbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                string selectedText = wolistbox.SelectedItem.ToString();
                DataView dv = workorderstatus.DefaultView;
                string columnName = dataGridView.Columns[0].Name;
                string filter = string.Format("{0} = '{1}'", columnName, selectedText);
                dv.RowFilter = filter;
                dataGridView.DataSource = dv;
                FillWoDetails(selectedText);
            }

        }

        private void FillDgDt()
        {
            workorderstatus.Clear();
            workorderstatus = connectapi.ShowWOStatusBinWithEMPName();
        }

        private void wolistbox_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource == null)
            {
                dataGridView.DataSource = workorderstatus;
                UpdateFont();
            }
        }

        private void UpdateFont()
        {

            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[8].Width = 80;
            dataGridView.Columns[9].Width = 80;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.0F, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 8.5F, FontStyle.Regular);
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
        }

        private void woid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wolistbox.Focus();
                if (dataGridView.DataSource == null)
                {
                    dataGridView.DataSource = workorderstatus;
                    UpdateFont();
                }                    
                wolistbox.SelectedItem = woid_txtbox.Text.Trim();
                woid_txtbox.Clear();
                woid_txtbox.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void FillWoDetails(string wo)
        {
            DataTable dtb1 = new DataTable();
            dtb1 = connectapi.ShowWODetails(wo);
            DataRow r = dtb1.Rows[0];
            itemlabel.Text = "SPM Item No : " + r["Item"].ToString();
            descriptionlabel.Text = "Description : " + r["Description"].ToString();
            qtylabel.Text = "Qty :" + r["Qty"].ToString();
            DataRow[] dr = workorderstatus.Select("WO = '" + wo + "'");

            inbuiltlabel.Text ="In-Built : " + dr[0]["Inbuilt"].ToString();
            if (dr[0]["Inbuilt"].ToString() == "Yes")
            {
                inbuiltlabel.BackColor = Color.Yellow;
                inbuiltlabel.ForeColor = Color.White;
            }
            else
            {
                inbuiltlabel.BackColor = Color.Red;
                inbuiltlabel.ForeColor = Color.White;
            }

            completelabel.Text ="Completed : " + dr[0]["Completed"].ToString();
            if (dr[0]["Completed"].ToString() == "Yes")
            {
                completelabel.BackColor = Color.Green;
                completelabel.ForeColor = Color.White;
            }
            else
            {
                completelabel.BackColor = Color.Yellow;
                completelabel.ForeColor = Color.White;
            }
        }

        private void reloadbttn_Click(object sender, EventArgs e)
        {
            loadform();
        }
    }
}
