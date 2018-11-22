using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class Materials : Form
    {
        Material model = new Material();

        public Materials()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            custname.Text  =  "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            model.id = 0;
        }

        private void Materials_Load(object sender, EventArgs e)
        {
            Clear();
            PopulateDataGridView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            model.MaterialNames = custname.Text.Trim();
            using (SPM_DatabaseEntities1 db = new SPM_DatabaseEntities1())
            {
                if (model.id == 0)//Insert
                    db.Materials.Add(model);
                else //Update
                    db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            Clear();
            PopulateDataGridView();
            MessageBox.Show("Submitted Successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void PopulateDataGridView()
        {
            dgvCustomer.AutoGenerateColumns = false;
            using (SPM_DatabaseEntities1 db = new SPM_DatabaseEntities1())
            {
                dgvCustomer.DataSource = db.Materials.ToList<Material>();
                
            }
        }

        private void dgvCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCustomer.CurrentRow.Index != -1)
            {
                model.id = Convert.ToInt32(dgvCustomer.CurrentRow.Cells["id"].Value);
                using (SPM_DatabaseEntities1 db = new SPM_DatabaseEntities1())
                {
                    model = db.Materials.Where(x => x.id == model.id).FirstOrDefault();
                    custname.Text = model.MaterialNames;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure to Delete this Record ?", "SPM Connect", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) {
                using (SPM_DatabaseEntities1 db = new SPM_DatabaseEntities1())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                        db.Materials.Attach(model);
                    db.Materials.Remove(model);
                    db.SaveChanges();
                    PopulateDataGridView();
                    Clear();
                    MessageBox.Show("Deleted Successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Materials_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
