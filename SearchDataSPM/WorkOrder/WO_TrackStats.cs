using SPMConnectAPI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class WO_TrackStats : Form
    {
        WorkOrder connectapi = new WorkOrder();
        bool formloading = false;
        DataTable workorderstatus = new DataTable();

        public WO_TrackStats()
        {
            InitializeComponent();
            //connectapi.SPM_Connect();
        }

        private void PopulateWorkOrders()
        {
            wolistbox.Items.Clear();
            DataTable workorderlist = new DataTable();
            workorderlist = connectapi.ShowDistinctWO();

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
                //string columnName = dataGridView.Columns[0].Name;
                //string filter = string.Format("{0} = '{1}'", columnName, selectedText);
                //dv.RowFilter = filter;
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
           
        }


        private void woid_txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wolistbox.Focus();
                          
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

            if (dr[0]["Inbuilt"].ToString().Trim() == "Yes")
            {
                inbuiltlabel.BackColor = Color.Yellow;
                inbuiltlabel.ForeColor = Color.Black;
            }
            else
            {
                inbuiltlabel.BackColor = Color.Red;
                inbuiltlabel.ForeColor = Color.White;
            }

            completelabel.Text ="Completed : " + dr[0]["Completed"].ToString();
            if (dr[0]["Completed"].ToString().Trim() == "Yes")
            {
                completelabel.BackColor = Color.Green;
                completelabel.ForeColor = Color.White;
            }
            else
            {
                completelabel.BackColor = Color.Yellow;
                completelabel.ForeColor = Color.Black;
            }
        }

        private void reloadbttn_Click(object sender, EventArgs e)
        {
            loadform();
        }

        private void lineShape4_Click(object sender, EventArgs e)
        {

        }
    }
}
