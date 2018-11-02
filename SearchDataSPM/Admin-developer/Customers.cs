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
    public partial class Customers : Form
    {
        Customer model = new Customer();

        public Customers()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            custname.Text = shortname.Text = alias.Text  = custidtxt.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            model.id = 0;
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            Clear();
            PopulateDataGridView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model.CustomerId = Convert.ToInt16(custidtxt.Text.ToString());
            model.Name = custname.Text.Trim();
            model.ShortName = shortname.Text.Trim();
            model.Alias = alias.Text.Trim();
            using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
            {
                if (model.id == 0)//Insert
                    db.Customers.Add(model);
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
            using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
            {
                dgvCustomer.DataSource = db.Customers.ToList<Customer>();
                
            }
        }

        private void dgvCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCustomer.CurrentRow.Index != -1)
            {
                model.id = Convert.ToInt32(dgvCustomer.CurrentRow.Cells["id"].Value);
                using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
                {
                    model = db.Customers.Where(x => x.id == model.id).FirstOrDefault();
                    custidtxt.Text = model.CustomerId.ToString();
                    custname.Text = model.Name;
                    shortname.Text = model.ShortName;
                    alias.Text = model.Alias;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure to Delete this Record ?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                        db.Customers.Attach(model);
                    db.Customers.Remove(model);
                    db.SaveChanges();
                    PopulateDataGridView();
                    Clear();
                    MessageBox.Show("Deleted Successfully", "SPM Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void custidtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9\.+\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
