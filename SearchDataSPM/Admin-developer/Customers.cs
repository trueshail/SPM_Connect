using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class ManageCustomers : Form
    {
        private Customer _model = new Customer();
        private log4net.ILog _log;

        public ManageCustomers()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            custname.Text = shortname.Text = alias.Text = custidtxt.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            _model.id = 0;
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            Clear();
            PopulateDataGridView();
            log4net.Config.XmlConfigurator.Configure();
            _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Info(message: "Opened Manage Customers");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _model.CustomerId = Convert.ToInt16(custidtxt.Text.ToString());
            _model.Name = custname.Text.Trim();
            _model.ShortName = shortname.Text.Trim();
            _model.Alias = alias.Text.Trim();
            using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
            {
                if (_model.id == 0)//Insert
                    db.Customers.Add(_model);
                else //Update
                    db.Entry(_model).State = EntityState.Modified;
                db.SaveChanges();
            }
            Clear();
            PopulateDataGridView();
            MessageBox.Show(text: "Submitted Successfully", caption: "SPM Connect", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
        }

        private void PopulateDataGridView()
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
                _model.id = Convert.ToInt32(dgvCustomer.CurrentRow.Cells["id"].Value);
                using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
                {
                    _model = db.Customers.Where(x => x.id == _model.id).FirstOrDefault();
                    custidtxt.Text = _model.CustomerId.ToString();
                    custname.Text = _model.Name;
                    shortname.Text = _model.ShortName;
                    alias.Text = _model.Alias;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure to Delete this Record ?", "SPM Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SPM_DatabaseEntities db = new SPM_DatabaseEntities())
                {
                    var entry = db.Entry(_model);
                    if (entry.State == EntityState.Detached)
                        db.Customers.Attach(_model);
                    db.Customers.Remove(_model);
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

        private void Customers_FormClosing(object sender, FormClosingEventArgs e)
        {
            _log.Info("Closed Manage Customers");
            this.Dispose();
        }
    }
}